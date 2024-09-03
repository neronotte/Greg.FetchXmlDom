using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/order"/>
	/// </summary>
	public class OrderExpression : IValidatableObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OrderExpression"/> class.
		/// Constructor is made private in order to force usage of the factory methods.
		/// </summary>
		private OrderExpression()
		{
		}


		/// <summary>
		/// Creates an OrderExpression object from a column name.
		/// </summary>
		/// <param name="column">The name of the colum to order for</param>
		/// <param name="descending">Indicates whether the sort order must be ascending or descending</param>
		/// <returns>The OrderExpression just created</returns>
		/// <exception cref="System.ArgumentException">If the column name provided is blank</exception>
		public static OrderExpression FromColumn(string column, bool descending = false)
		{
			if (string.IsNullOrWhiteSpace(column))
				throw new System.ArgumentException("column cannot be blank.", nameof(column));

			return new OrderExpression
			{
				Column = column,
				Descending = descending
			};
		}

		/// <summary>
		/// Creates an OrderExpression object from a column name of a related entity.
		/// </summary>
		/// <param name="relatedTableAlias">The alias used for the link entity.</param>
		/// <param name="relatedColumnName">The name of the column in the related entity.</param>
		/// <param name="descending">Indicates whether the sort order must be ascending or descending</param>
		/// <returns>The OrderExpression just created</returns>
		/// <exception cref="System.ArgumentException">If the related table alias or the related column name provided are blank</exception>
		public static OrderExpression FromLinkEntityColumn(string relatedTableAlias, string relatedColumnName, bool descending = false)
		{
			if (string.IsNullOrWhiteSpace(relatedTableAlias))
				throw new System.ArgumentException("relatedTableAlias cannot be blank.", nameof(relatedTableAlias));
			if (string.IsNullOrWhiteSpace(relatedColumnName))
				throw new System.ArgumentException("relatedColumnName cannot be blank.", nameof(relatedColumnName));

			return new OrderExpression
			{
				EntityName = relatedTableAlias,
				Column = relatedColumnName,
				Descending = descending
			};
		}

		/// <summary>
		/// Creates an OrderExpression object from an alias (to be used in aggregate queries).
		/// </summary>
		/// <param name="alias">The column alias</param>
		/// <param name="descending">Indicates whether the sort order must be ascending or descending</param>
		/// <returns>The OrderExpression just created</returns>
		/// <exception cref="System.ArgumentException">If the alias provided is blank</exception>
		public static OrderExpression FromAlias(string alias, bool descending = false)
		{
			if (string.IsNullOrWhiteSpace(alias))
				throw new System.ArgumentException("alias cannot be blank.", nameof(alias));

			return new OrderExpression
			{
				Alias = alias,
				Descending = descending
			};
		}





		/// <summary>
		/// The name of the attribute element to sort the data by.
		/// </summary>
		public string Column { get; private set; }

		/// <summary>
		/// The alias of the attribute element to sort the data by
		/// </summary>
		public string Alias { get; private set; }

		/// <summary>
		/// Whether to sort the data in descending order.
		/// </summary>
		public bool Descending { get; private set; }

		/// <summary>
		/// Use this attribute to specify sort order for link-entity elements so that they aren't applied last. 
		/// In an order within an entity element, set entityname to the alias value of a link-entity. 
		/// Learn how to apply link-entity orders first
		/// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/order-rows"/>
		/// </summary>
		public string EntityName { get; private set; }



		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (string.IsNullOrWhiteSpace(Alias) && string.IsNullOrWhiteSpace(Column))
			{
				yield return new ValidationResult("Either Alias or Attribute must be set", new[] { nameof(Alias), nameof(Column) });
			}
		}




		internal void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("order");
			if (!string.IsNullOrWhiteSpace(Column))
			{
				writer.WriteAttributeString("attribute", Column);
			}

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
