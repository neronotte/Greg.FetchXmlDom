using System;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// Represents a column in a FetchXml query.
	/// </summary>
	public sealed class ColumnExpression : IEquatable<ColumnExpression>
	{
		/// <summary>
		/// Creates a new ColumnExpression object.
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <param name="alias">The alias of the column</param>
		/// <exception cref="ArgumentNullException">If the name of the column is not provided.</exception>
		public ColumnExpression(string columnName, string alias = null)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentNullException(nameof(columnName), "The column name is required");

			this.Name = columnName;
			this.Alias = alias;
		}

		/// <summary>
		/// Creates a new ColumnExpression object representing an aggregate column.
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <param name="aggregateFunction">The aggregate function to apply</param>
		/// <param name="alias">An alias for the column. Alias is required for aggregate queries, If not provided, the column name is used.</param>
		/// <param name="rowAggregate">
		/// When this value is set to CountChildren a value that includes the total number of child records for the record is included in the results. 
		/// Learn how to use this attribute <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/query-hierarchical-data#retrieve-the-number-of-hierarchically-related-child-records"/>.
		/// </param>
		/// <param name="distinct">
		/// When you use the aggregate countcolumn function, this attribute specifies that only unique values for the column are returned. 
		/// Learn more about distinct column values <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#distinct-column-values"/>.
		/// </param>
		/// <exception cref="ArgumentNullException"></exception>
		public static ColumnExpression CreateAggregateColumn(string columnName, AggregateFunction aggregateFunction, string alias = null, bool? rowAggregate = null, bool? distinct = null)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentNullException(nameof(columnName), "The column name is required");

			if (alias == null) alias = columnName;
			if (string.IsNullOrWhiteSpace(alias))
				throw new ArgumentNullException(nameof(alias), "The alias is required for aggregate columns");


			var column = new ColumnExpression(columnName, alias)
			{
				Aggregate = aggregateFunction,
				RowAggregate = rowAggregate.GetValueOrDefault(),
				Distinct = distinct.GetValueOrDefault()
			};
			return column;
		}


		/// <summary>
		/// Creates a new ColumnExpression object acts as a GROUP BY condition for the query
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <param name="alias">An alias for the column. Alias is required for aggregate queries, If not provided, the column name is used.</param>
		/// <param name="dateGrouping">
		/// When you group data by a datetime value, this attribute specifies the date part to use. 
		/// See Date grouping options <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/attribute#date-grouping-options"/>
		/// </param>
		/// <param name="userTimeZone">
		/// Used by aggregate queries that group by datetime columns. 
		/// Depending on the time zone, the same datetime value can fall in different days.
		/// 
		/// Use this attribute with a false value to force the grouping to use UTC value.
		/// When you don't set this attribute, the default value is true, and the user's time zone is used.
		/// </param>
		public static ColumnExpression CreateGroupColumn(string columnName, string alias = null, DateGrouping? dateGrouping = null, bool? userTimeZone = null)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentNullException(nameof(columnName), "The column name is required");

			if (alias == null) alias = columnName;
			if (string.IsNullOrWhiteSpace(alias))
				throw new ArgumentNullException(nameof(alias), "The alias is required for aggregate columns");

			var column = new ColumnExpression(columnName, alias)
			{
				GroupBy = true,
				DateGrouping = dateGrouping,
				UserTimeZone = userTimeZone
			};
			return column;
		}

		/// <summary>
		/// The logical name of the column.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The aggregate function to apply. 
		/// Learn how to aggregate data with FetchXml <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data"/>
		/// </summary>
		public AggregateFunction? Aggregate { get; private set; }

		/// <summary>
		/// The name of the column to return. Each column must have a unique name. 
		/// You'll use aliases when you retrieve aggregate values. 
		/// Learn more about column aliases <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/select-columns#column-aliases"/>
		/// </summary>
		public string Alias { get; }

		/// <summary>
		/// When you group data by a datetime value, this attribute specifies the date part to use. 
		/// See Date grouping options <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/attribute#date-grouping-options"/>
		/// </summary>
		public DateGrouping? DateGrouping { get; private set; }


		/// <summary>
		/// When you use the aggregate countcolumn function, this attribute specifies that only unique values for the column are returned. 
		/// Learn more about distinct column values <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#distinct-column-values"/>.
		/// </summary>
		public bool Distinct { get; private set; }

		/// <summary>
		/// When you aggregate data, this attribute specifies the column to use to group the data. 
		/// Learn more about grouping <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#grouping"/>.
		/// </summary>
		public bool GroupBy { get; private set; }

		/// <summary>
		/// When this value is set to CountChildren a value that includes the total number of child records for the record is included in the results. 
		/// Learn how to use this attribute <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/query-hierarchical-data#retrieve-the-number-of-hierarchically-related-child-records"/>.
		/// </summary>
		public bool RowAggregate { get; private set; }

		/// <summary>
		/// Used by aggregate queries that group by datetime columns. 
		/// Depending on the time zone, the same datetime value can fall in different days.
		/// 
		/// Use this attribute with a false value to force the grouping to use UTC value.
		/// When you don't set this attribute, the default value is true, and the user's time zone is used.
		/// </summary>
		public bool? UserTimeZone { get; private set; }



		/// <summary>
		/// Indicates weather the current column is an aggregate column
		/// </summary>
		/// <returns>True if the current column is an aggregate column, false otherwise</returns>
		public bool IsAggregateColumn()
		{
			return GroupBy || Aggregate.HasValue;
		}


		/// <summary>
		/// Returns <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
		/// </summary>
		/// <param name="other">The object to compare to the current one</param>
		/// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c></returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as ColumnExpression);
		}



		/// <summary>
		/// Returns <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
		/// </summary>
		/// <param name="other">The object to compare to the current one</param>
		/// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c></returns>
		public bool Equals(ColumnExpression other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;

			return Name == other.Name &&
				Aggregate == other.Aggregate &&
				Alias == other.Alias &&
				DateGrouping == other.DateGrouping &&
				Distinct == other.Distinct &&
				GroupBy == other.GroupBy &&
				RowAggregate == other.RowAggregate &&
				UserTimeZone == other.UserTimeZone;
		}


		/// <summary>
		/// Implements an hashcode function for all the properties of the current object
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 31 + (Name?.GetHashCode() ?? 0);
				hash = hash * 31 + (Aggregate?.GetHashCode() ?? 0);
				hash = hash * 31 + (Alias?.GetHashCode() ?? 0);
				hash = hash * 31 + (DateGrouping?.GetHashCode() ?? 0);
				hash = hash * 31 + Distinct.GetHashCode();
				hash = hash * 31 + GroupBy.GetHashCode();
				hash = hash * 31 + RowAggregate.GetHashCode();
				hash = hash * 31 + (UserTimeZone?.GetHashCode() ?? 0);
				return hash;
			}
		}



		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("attribute");

			writer.WriteAttributeString("name", Name);

			if (!string.IsNullOrWhiteSpace(Alias))
			{
				writer.WriteAttributeString("alias", Alias);
			}

			if (Aggregate.HasValue)
			{
				writer.WriteAttributeString("aggregate", Aggregate.Value.ToString().ToLowerInvariant());
			}

			if (DateGrouping.HasValue)
			{
				writer.WriteAttributeString("dategrouping", DateGrouping.Value.ToString().ToLowerInvariant().Replace("fiscal", "fiscal-"));
			}

			if (Distinct)
			{
				writer.WriteAttributeString("distinct", "true");
			}

			if (GroupBy)
			{
				writer.WriteAttributeString("groupby", "true");
			}

			if (RowAggregate)
			{
				writer.WriteAttributeString("rowaggregate", "CountChildren");
			}

			if (UserTimeZone.HasValue)
			{
				writer.WriteAttributeString("usertimezone", UserTimeZone.Value ? "true" : "false");
			}

			writer.WriteEndElement();
		}
	}
}
