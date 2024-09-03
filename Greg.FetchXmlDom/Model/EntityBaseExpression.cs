using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
    public abstract class EntityBaseExpression : IValidatableObject
	{
		# region Select Management
		
		/// <summary>
		/// Specifies a column from an entity or link-entity to return with a query.
		/// </summary>
		public ColumnCollection ColumnSet { get; set; } = new ColumnCollection();


		/// <summary>
		/// Adds a set of columns to the current collection
		/// </summary>
		/// <param name="columns">The columns to be added to the current collection</param>
		public void AddColumns(params string[] columns)
		{
			if (ColumnSet == null) ColumnSet = new ColumnCollection();
			ColumnSet.Add(columns);
		}

		/// <summary>
		/// Indicates that all columns should be returned
		/// AVOID USING THIS METHOD IN PRODUCTION CODE
		/// </summary>
		public void AllColumns()
		{
			if (ColumnSet == null) ColumnSet = new ColumnCollection();
			ColumnSet.All = true;
		}

		/// <summary>
		/// Adds a simple column to the current collection
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <param name="alias">An alias for the column</param>
		/// <returns>The created column</returns>
		public ColumnExpression AddColumn(string columnName, string alias = null)
		{
			var column = new ColumnExpression(columnName, alias);

			if (ColumnSet == null) ColumnSet = new ColumnCollection();
			ColumnSet.Add(column);
			return column;
		}

		/// <summary>
		/// Adds a column containing an aggregate function to the current collection
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <param name="alias">An alias for the column</param>
		/// <param name="aggregateFunction">The aggregate function to apply</param>
		/// <param name="rowAggregate">
		/// When this value is set to CountChildren a value that includes the total number of child records for the record is included in the results. 
		/// Learn how to use this attribute <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/query-hierarchical-data#retrieve-the-number-of-hierarchically-related-child-records"/>.
		/// </param>
		/// <returns>The created column</returns>
		public ColumnExpression AddAggregateColumn(string columnName, string alias, AggregateFunction aggregateFunction, bool? rowAggregate = null, bool? distinct = null)
		{
			var column = ColumnExpression.CreateAggregateColumn(columnName, alias, aggregateFunction, rowAggregate, distinct);

			if (ColumnSet == null) ColumnSet = new ColumnCollection();
			ColumnSet.Add(column);
			return column;
		}

		/// <summary>
		/// Adds a column that acts as a GROUP BY condition for the query
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <param name="alias">An alias for the column</param>
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
		/// <returns>The created column</returns>
		public ColumnExpression AddGroupColumn(string columnName, string alias, DateGrouping? dateGrouping = null, bool? userTimeZone = null)
		{
			var column = ColumnExpression.CreateGroupColumn(columnName, alias, dateGrouping, userTimeZone);

			if (ColumnSet == null) ColumnSet = new ColumnCollection();
			ColumnSet.Add(column);
			return column;
		}

		#endregion


		#region Link Management


		/// <summary>
		/// Joins a table related to the entity or link-entity to return more columns with the result.
		/// </summary>
		public List<LinkEntityExpression> LinkEntities { get; set; } = new List<LinkEntityExpression>();

		/// <summary>
		/// Adds a link-entity to the current entity.
		/// </summary>
		/// <param name="configureAction">A delegate that can be used to configure the link entity</param>
		/// <returns>The newly created link entity</returns>
		public LinkEntityExpression AddLink(Action<LinkEntityExpression> configureAction = null)
		{
			var link = new LinkEntityExpression();

			configureAction?.Invoke(link);

			if (LinkEntities == null) LinkEntities = new List<LinkEntityExpression>();
			LinkEntities.Add(link);
			return link;
		}

		/// <summary>
		/// Adds a link-entity to the current entity.
		/// </summary>
		/// <param name="entityName">The referenced table</param>
		/// <param name="from">The name of the field the current table that references the target table</param>
		/// <param name="to">The name of the key field on the target table</param>
		/// <param name="linkType">The type of link </param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public LinkEntityExpression AddLink(string entityName, string from, string to, LinkType linkType = LinkType.Inner, string alias = null)
		{
			var link = new LinkEntityExpression(entityName, from, to, linkType, alias);
			if (LinkEntities == null) LinkEntities = new List<LinkEntityExpression>();
			LinkEntities.Add(link);
			return link;
		}

		#endregion


		#region Where Management

		/// <summary>
		/// Specify complex conditions for an entity or link-entity to apply to a query.
		/// </summary>
		public FilterExpression Filter { get; set; }


		/// <summary>
		/// Adds a filter to the current entity.
		/// </summary>
		/// <param name="filterType">The type of filter operator (default: And)</param>
		/// <returns>The filter just added</returns>
		public FilterExpression AddFilter(FilterType filterType = FilterType.And)
		{
			Filter = new FilterExpression(filterType);
			return Filter;
		}


		public ConditionExpression AddCondition(ConditionExpression condition)
		{
			if (this.Filter == null) this.AddFilter();

			this.Filter?.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string attribute, ConditionOperator conditionOperator, object[] values)
		{
			if (this.Filter == null) this.AddFilter();

			var condition = new ConditionExpression(attribute, conditionOperator, values);
			this.Filter?.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string attribute, ConditionOperator conditionOperator, object value)
		{
			if (this.Filter == null) this.AddFilter();

			var condition = new ConditionExpression(attribute, conditionOperator, value);
			this.Filter?.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string attribute, ConditionOperator conditionOperator)
		{
			if (this.Filter == null) this.AddFilter();

			var condition = new ConditionExpression(attribute, conditionOperator);
			this.Filter?.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddConditionToOtherColumn(string attribute, ConditionOperator conditionOperator, string otherColumn)
		{
			if (this.Filter == null) this.AddFilter();

			var condition = new ConditionExpression(attribute, conditionOperator) { ValueOf = otherColumn };
			this.Filter?.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddConditionToOtherColumn(string attribute, ConditionOperator conditionOperator, string otherTableAlias, string otherColumn)
		{
			if (this.Filter == null) this.AddFilter();

			var condition = new ConditionExpression(attribute, conditionOperator) { ValueOf = otherTableAlias + "." + otherColumn };
			this.Filter?.Conditions.Add(condition);
			return condition;
		}


		#endregion




		/// <summary>
		/// Specifies a sort order for the rows of the results.
		/// </summary>
		public List<OrderExpression> Order { get; set; } = new List<OrderExpression>();


		public OrderExpression OrderBy(string columnName)
		{
			var order = OrderExpression.FromColumn(columnName);
			Order.Add(order);
			return order;
		}

		public OrderExpression OrderByDescending(string columnName)
		{
			var order = OrderExpression.FromColumn(columnName, true);
			Order.Add(order);
			return order;
		}

		public OrderExpression OrderByAlias(string alias)
		{
			var order = OrderExpression.FromAlias(alias);
			Order.Add(order);
			return order;
		}

		public OrderExpression OrderByAliasDescending(string alias)
		{
			var order = OrderExpression.FromAlias(alias, true);
			Order.Add(order);
			return order;
		}

		public OrderExpression OrderByLinkEntityColumn(string relatedTableAlias, string relatedColumnName)
		{
			var order = OrderExpression.FromLinkEntityColumn(relatedTableAlias, relatedColumnName);
			Order.Add(order);
			return order;
		}

		public OrderExpression OrderByLinkEntityColumnDescending(string relatedTableAlias, string relatedColumnName)
		{
			var order = OrderExpression.FromLinkEntityColumn(relatedTableAlias, relatedColumnName, true);
			Order.Add(order);
			return order;
		}


		protected virtual void WriteXmlElements(XmlWriter writer)
		{
			if (ColumnSet != null)
			{
				if (ColumnSet.All)
				{
					writer.WriteStartElement("all-attributes");
					writer.WriteEndElement();
				}
				else if (ColumnSet.Count > 0)
				{
					foreach (var attribute in ColumnSet)
					{
						attribute.WriteXml(writer);
					}
				}
			}

			Filter?.WriteXml(writer);

			if (LinkEntities != null && LinkEntities.Count > 0 )
			{
				foreach (var linkEntity in LinkEntities)
				{
					linkEntity.WriteXml(writer);
				}
			}

			if (Order != null && Order.Count > 0)
			{
				foreach (var order in Order)
				{
					order.WriteXml(writer);
				}
			}
		}




		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var validationResults = new List<ValidationResult>();
			this.ColumnSet.TryValidateList(nameof(ColumnSet), validationContext, validationResults);

			if (this.Filter != null)
			{
				var partialValidationResults = new List<ValidationResult>();
				if (!Validator.TryValidateObject(this.Filter, new ValidationContext(this.Filter, validationContext.Items), partialValidationResults, true))
				{
					validationResults.AddChildren(partialValidationResults, "Filter");
				}
			}

			this.LinkEntities.TryValidateList(nameof(LinkEntities), validationContext, validationResults);
			this.Order.TryValidateList(nameof(Order), validationContext, validationResults);

			return validationResults;
		}
	}
}
