using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/filter"/>
	/// </summary>
	public class FilterExpression : IValidatableObject
	{
		public FilterExpression(FilterType type = FilterType.And)
		{
			this.Type = type;
		}


        /// <summary>
        /// Use and or or. Whether all (and) or any (or) conditions within the filter must be met.
        /// </summary>
        public FilterType? Type { get; set; }


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




		public ConditionExpression AddCondition(string attribute, ConditionOperator conditionOperator, object[] values)
		{
			var condition = new ConditionExpression(attribute, conditionOperator, values);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string attribute, ConditionOperator conditionOperator, object value)
		{
			var condition = new ConditionExpression(attribute, conditionOperator, value);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddCondition(string attribute, ConditionOperator conditionOperator)
		{
			var condition = new ConditionExpression(attribute, conditionOperator);
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddConditionToOtherColumn(string attribute, ConditionOperator conditionOperator, string otherColumn)
		{
			var condition = new ConditionExpression(attribute, conditionOperator) { ValueOf = otherColumn };
			this.Conditions.Add(condition);
			return condition;
		}

		public ConditionExpression AddConditionToOtherColumn(string attribute, ConditionOperator conditionOperator, string otherTableAlias, string otherColumn)
		{
			var condition = new ConditionExpression(attribute, conditionOperator) { ValueOf = otherTableAlias + "." + otherColumn };
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
		/// <param name="configureAction">A delegate that can be used to configure the link entity</param>
		/// <returns>The newly created link entity</returns>
		public LinkEntityExpression AddLink(Action<LinkEntityExpression> configureAction = null)
		{
			var link = new LinkEntityExpression();

			if (configureAction != null) configureAction(link);

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



		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Conditions != null && Conditions.Count > 500)
			{
				yield return new ValidationResult("The maximum number of conditions is 500.", new[] { nameof(Conditions) });
			}
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