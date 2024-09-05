using System;
using System.Collections.Generic;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
    /// <summary>
    /// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/filter"/>
    /// </summary>
    public class FilterExpression
	{
		public FilterExpression(FilterType type = FilterType.And)
		{
			this.Type = type;
		}


        /// <summary>
        /// Use and or or. Whether all (and) or any (or) conditions within the filter must be met.
        /// </summary>
        public FilterType? Type { get; }


		/// <summary>
		/// Use this attribute to set the union hint to get a performance benefit for a specific type of query. 
		/// Learn how to use the union hint <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/optimize-performance#union-hint"/>
		/// </summary>
		public bool Hint { get; set; }


		/// <summary>
		/// Use this attribute to tell Dataverse to execute the query as a quick find query. 
		/// About quick find queries <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/quick-find"/>
		/// </summary>
		public bool? IsQuickFindFields { get; set; }

		/// <summary>
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/quick-find#quick-find-record-limits">Quick find record limits</see> 
		/// and <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/quick-find#apply-the-quick-find-record-limit">Apply the quick find record limit</see>
		/// </summary>
		public bool? OverrideQuickFindRecordLimitEnabled { get; set; }


		/// <summary>
		/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/quick-find#quick-find-record-limits">Quick find record limits</see> 
		/// and <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/quick-find#apply-the-quick-find-record-limit">Apply the quick find record limit</see>
		/// </summary>
		public bool? OverrideQuickFindRecordLimitDisabled { get; set; }


		/// <summary>
		/// Specify conditions for entity and link-entity row values which must be true to return the row. 
		/// The condition operator attribute specifies how to evaluate the values.
		/// </summary>
		public List<ConditionExpression> Conditions { get; set; } = new List<ConditionExpression>();




		public ConditionExpression AddCondition(string columnName, ConditionOperator conditionOperator, object[] values)
		{
			var condition = new ConditionExpression(columnName, conditionOperator, values);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string columnName, ConditionOperator conditionOperator, object value)
		{
			var condition = new ConditionExpression(columnName, conditionOperator, value);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string columnName, ConditionOperator conditionOperator)
		{
			var condition = new ConditionExpression(columnName, conditionOperator);
			this.Conditions.Add(condition);
			return condition;
		}
		public ConditionExpression AddCondition(string entityAlias, string columnName, ConditionOperator conditionOperator, object[] values)
		{
			var condition = new ConditionExpression(entityAlias, columnName, conditionOperator, values);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string entityAlias, string columnName, ConditionOperator conditionOperator, object value)
		{
			var condition = new ConditionExpression(entityAlias, columnName, conditionOperator, value);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string entityAlias, string columnName, ConditionOperator conditionOperator)
		{
			var condition = new ConditionExpression(entityAlias, columnName, conditionOperator);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddConditionToOtherColumn(string columnName, ConditionOperator conditionOperator, string otherColumn)
		{
			var condition = ConditionExpression.CreateConditionToOtherColumn(columnName, conditionOperator, otherColumn);
			this.Conditions.Add(condition);
			return condition;
		}




		/// <summary>
		/// Specify complex conditions for an entity or link-entity to apply to a query.
		/// </summary>
		public List<FilterExpression> Filters { get; set; } = new List<FilterExpression>();


		/// <summary>
		/// Adds a sub filter to the current filter.
		/// </summary>
		/// <param name="filterType">The type of filter operator (default: And)</param>
		/// <returns>The filter just added</returns>
		public FilterExpression AddFilter(FilterType filterType = FilterType.And)
		{
			var filter = new FilterExpression(filterType);
			if (Filters == null ) Filters = new List<FilterExpression>();
			Filters.Add(filter);
			return filter;
		}





		/// <summary>
		/// Used when filtering on values in related records
		/// </summary>
		public List<LinkEntityExpression> LinkEntities { get; set; } = new List<LinkEntityExpression>();


		/// <summary>
		/// Adds a link-entity to the current entity.
		/// </summary>
		/// <param name="entityName">The referenced table</param>
		/// <param name="from">The name of the field the current table that references the target table</param>
		/// <param name="to">The name of the key field on the target table</param>
		/// <param name="linkType">The type of link </param>
		/// <param name="alias">
		/// Represents the name of the related table. 
		/// If you don't set an alias, one will be generated for you to ensure all columns have unique names, 
		/// but you will not be able to use that alias to reference the link entity in other parts of the fetch XML.
		/// The auto-generated aliases use the pattern {LogicalName}+{N}, where N is the sequential number 
		/// of the link-entity in the fetch XML starting from 1.
		/// </param>
		/// <param name="intersect">
		/// Indicates that the link-entity is used to join tables and not return any columns, typically for a many-to-many relationship. 
		/// The existence of this attribute doesn't change the query execution. 
		/// You might add this attribute to your link-entity when you join a table but don't include any attribute elements to show that this is intentional.
		/// </param>
		/// <returns>The link entity just created</returns>
		public LinkEntityExpression AddLink(string entityName, string from, string to, LinkType linkType = LinkType.Inner, string alias = null, bool intersect = false)
		{
			var link = new LinkEntityExpression(entityName, from, to, linkType, alias, intersect);
			if (LinkEntities == null) LinkEntities = new List<LinkEntityExpression>();
			LinkEntities.Add(link);
			return link;
		}




		internal void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("filter");

			if (Type.HasValue)
			{
				writer.WriteAttributeString("type", Type.Value.ToString().ToLowerInvariant());
			}
			if (Hint)
			{
				writer.WriteAttributeString("hint", "union");
			}
			if (IsQuickFindFields.HasValue)
			{
				writer.WriteAttributeString("isquickfindfields", IsQuickFindFields.Value ? "true" : "false");
			}
			if (OverrideQuickFindRecordLimitEnabled.GetValueOrDefault())
			{
				writer.WriteAttributeString("overridequickfindrecordlimitenabled", "1");
			}
			if (OverrideQuickFindRecordLimitDisabled.GetValueOrDefault())
			{
				writer.WriteAttributeString("overridequickfindrecordlimitdisabled", "1");
			}

			if (Conditions != null && Conditions.Count > 0)
			{
				foreach (var condition in Conditions)
				{
					condition.WriteXml(writer);
				}
			}

			if (Filters != null && Filters.Count > 0)
			{
				foreach (var filter in Filters)
				{
					filter.WriteXml(writer);
				}
			}

			if (LinkEntities != null && LinkEntities.Count > 0)
			{
				foreach (var linkEntity in LinkEntities)
				{
					linkEntity.WriteXml(writer);
				}
			}

			writer.WriteEndElement();
		}
	}
}