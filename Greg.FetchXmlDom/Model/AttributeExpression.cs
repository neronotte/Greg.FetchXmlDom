using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	public class AttributeExpression
	{
        public AttributeExpression()
        {
        }

        public AttributeExpression(string name)
        {
			Name = name;
		}

        /// <summary>
        /// The logical name of the column.
        /// </summary>
        [Required(ErrorMessage = "Attribute name is required")]
		public string Name { get; set;}

		/// <summary>
		/// The aggregate function to apply. 
		/// Learn how to aggregate data with FetchXml <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data"/>
		/// </summary>
		public AggregateFunction? Aggregate { get; set; }

		/// <summary>
		/// The name of the column to return. Each column must have a unique name. 
		/// You'll use aliases when you retrieve aggregate values. 
		/// Learn more about column aliases <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/select-columns#column-aliases"/>
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// When you group data by a datetime value, this attribute specifies the date part to use. 
		/// See Date grouping options <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/attribute#date-grouping-options"/>
		/// </summary>
		public DateGrouping? DateGrouping { get; set; }


		/// <summary>
		/// When you use the aggregate countcolumn function, this attribute specifies that only unique values for the column are returned. 
		/// Learn more about distinct column values <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#distinct-column-values"/>.
		/// </summary>
		public bool Distinct { get; set; }

		/// <summary>
		/// When you aggregate data, this attribute specifies the column to use to group the data. 
		/// Learn more about grouping <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/aggregate-data#grouping"/>.
		/// </summary>
		public bool GroupBy { get; set; }

		/// <summary>
		/// When this value is set to CountChildren a value that includes the total number of child records for the record is included in the results. 
		/// Learn how to use this attribute <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/query-hierarchical-data#retrieve-the-number-of-hierarchically-related-child-records"/>.
		/// </summary>
		public bool RowAggregate { get; set; }

		/// <summary>
		/// Used by aggregate queries that group by datetime columns. 
		/// Depending on the time zone, the same datetime value can fall in different days.
		/// 
		/// Use this attribute with a false value to force the grouping to use UTC value.
		/// When you don't set this attribute, the default value is true, and the user's time zone is used.
		/// </summary>
		public bool? UserTimeZone { get; set; }


		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("attribute");

			writer.WriteAttributeString("name", Name);

			if (Aggregate.HasValue)
			{
				writer.WriteAttributeString("aggregate", Aggregate.Value.ToString().ToLowerInvariant());
			}

			if (!string.IsNullOrWhiteSpace(Alias))
			{
				writer.WriteAttributeString("alias", Alias);
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
