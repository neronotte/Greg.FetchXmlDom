using System;
using System.Collections.Generic;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	public abstract class EntityBaseExpression
	{
		/// <summary>
		/// Specifies a column from an entity or link-entity to return with a query.
		/// </summary>
		public AttributeCollection Attributes { get; set; } = new AttributeCollection();


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




		/// <summary>
		/// Specifies a sort order for the rows of the results.
		/// </summary>
		public List<OrderExpression> Order { get; set; } = new List<OrderExpression>();



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




		protected virtual void WriteXmlElements(XmlWriter writer)
		{
			if (Attributes != null)
			{
				if (Attributes.All)
				{
					writer.WriteStartElement("all-attributes");
					writer.WriteEndElement();
				}
				else if (Attributes.Count > 0)
				{
					foreach (var attribute in Attributes)
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
	}
}
