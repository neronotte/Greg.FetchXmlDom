using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/order"/>
	/// </summary>
	public class OrderExpression
	{
		/// <summary>
		/// The name of the attribute element to sort the data by.
		/// </summary>
		[Required(ErrorMessage = "Order attribute is required")]
		public string Attribute { get; set; }

		/// <summary>
		/// The alias of the attribute element to sort the data by
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// Whether to sort the data in descending order.
		/// </summary>
		public bool Descending { get; set; }

		/// <summary>
		/// Use this attribute to specify sort order for link-entity elements so that they aren't applied last. 
		/// In an order within an entity element, set entityname to the alias value of a link-entity. 
		/// Learn how to apply link-entity orders first
		/// </summary>
		public string EntityName { get; set; }

		internal void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("order");
			writer.WriteAttributeString("attribute", Attribute);

			if (!string.IsNullOrWhiteSpace(Alias))
			{
				writer.WriteAttributeString("alias", Alias);
			}
			if (Descending)
			{
				writer.WriteAttributeString("descending", "true");
			}
			if (!string.IsNullOrWhiteSpace(EntityName))
			{
				writer.WriteAttributeString("entityname", EntityName);
			}

			writer.WriteEndElement();
		}
	}

}
