using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	public class LinkEntityExpression : EntityBaseExpression
	{
        public LinkEntityExpression()
        {
        }

		public LinkEntityExpression(string relatedTableName, string relatedTableField, string sourceTableField, LinkType? linkType = Model.LinkType.Inner, string alias = null)
		{
			Name = relatedTableName;
			From = relatedTableField;
			To = sourceTableField;
			LinkType = linkType;
			Alias = alias;
		}


        /// <summary>
        /// The logical name of the related table.
        /// </summary>
        [Required(ErrorMessage = "LinkEntity name is required")]
        public string Name { get; set; }

		/// <summary>
		/// The logical name of the column in the parent element to match with the related table column specified in the from attribute. 
		/// While not technically required, this attribute is usually used.
		/// </summary>
		public string To { get; set; }

		/// <summary>
		/// The logical name of the column from the related table that matches the column specified in the to attribute. 
		/// While not technically required, this attribute is usually used.
		/// </summary>
		public string From { get; set; }

		/// <summary>
		/// Represents the name of the related table. 
		/// If you don't set an alias, one will be generated for you to ensure all columns have unique names, 
		/// but you will not be able to use that alias to reference the link entity in other parts of the fetch XML.
		/// The auto-generated aliases use the pattern {LogicalName}+{N}, where N is the sequential number 
		/// of the link-entity in the fetch XML starting from 1.
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// The type of link use. Default behavior is inner. 
		/// Learn about link-type options <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/link-entity#link-type-options"/>
		/// </summary>
		public LinkType? LinkType { get; set; }

		/// <summary>
		/// Indicates that the link-entity is used to join tables and not return any columns, typically for a many-to-many relationship. 
		/// The existence of this attribute doesn't change the query execution. 
		/// You might add this attribute to your link-entity when you join a table but don't include any attribute elements to show that this is intentional.
		/// </summary>
		public bool Intersect { get; set; }







        internal void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("link-entity");
			writer.WriteAttributeString("name", Name);
			if (!string.IsNullOrWhiteSpace(From))
			{
				writer.WriteAttributeString("from", From);
			}

			if (!string.IsNullOrWhiteSpace(To))
			{
				writer.WriteAttributeString("to", To);
			}
			if (LinkType.HasValue)
			{
				writer.WriteAttributeString("link-type", LinkType.Value.ToString().ToLower().Replace("nota", "not a"));
			}
			if (!string.IsNullOrWhiteSpace(Alias))
			{
				writer.WriteAttributeString("alias", Alias);
			}
			if (Intersect)
			{
				writer.WriteAttributeString("intersect", "true");
			}

			base.WriteXmlElements(writer);

			writer.WriteEndElement();
		}
	}
}
