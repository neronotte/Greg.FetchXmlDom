using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
    /// <summary>
    /// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/condition"/>
    /// </summary>
    public class ConditionExpression : IValidatableObject
	{

		public ConditionExpression(string columnName, ConditionOperator conditionOperator)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new System.ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
		}

		public ConditionExpression(string columnName, ConditionOperator conditionOperator, object value)
			: this(columnName, conditionOperator)
		{
			this.Values = value != null ? new[] { value } : null;
		}
		public ConditionExpression(string columnName, ConditionOperator conditionOperator, object[] values)
			: this(columnName, conditionOperator)
		{
			this.Values = values;
		}

		public ConditionExpression(string entityAlias, string columnName, ConditionOperator conditionOperator)
			: this(columnName, conditionOperator)
		{
			if (string.IsNullOrWhiteSpace(entityAlias))
				throw new System.ArgumentException("entityAlias cannot be empty", nameof(entityAlias));

			this.EntityName = entityAlias;
		}

		public ConditionExpression(string entityAlias, string columnName, ConditionOperator conditionOperator, object value)
			: this(entityAlias, columnName, conditionOperator)
		{
			this.Values = value != null ? new[] { value } : null;
		}

		public ConditionExpression(string entityAlias, string columnName, ConditionOperator conditionOperator, object[] values)
			: this(entityAlias, columnName, conditionOperator)
		{
			this.Values = values;
		}



		public static ConditionExpression CreateConditionToOtherColumn(string attribute, ConditionOperator conditionOperator, string otherColumn)
		{
			var condition = new ConditionExpression(attribute, conditionOperator) { ValueOf = otherColumn };
			return condition;
		}


		/// <summary>
		/// The name of the column with the value to apply the filter.
		/// </summary>
		public string Attribute { get; }

		/// <summary>
		/// The operator to apply with the filter.
		/// </summary>
		public ConditionOperator? Operator { get; }

		/// <summary>
		/// Specify the link-entity name or alias that the condition should be applied to after the outer join. 
		/// Learn more about filters on link-entity <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filters-on-link-entity"/>
		/// </summary>
		public string EntityName { get; private set; }


		/// <summary>
		/// The value to test the column value with the operator.
		/// </summary>
		public object[] Values { get; }

		/// <summary>
		/// The name of the column in the same table that has the value to test the column value with the operator. 
		/// Learn more about filtering on other column values. <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filter-on-column-values-in-the-same-row"/>
		/// </summary>
		public string ValueOf { get; private set; }




		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!string.IsNullOrWhiteSpace(ValueOf) && Values != null && Values.Length > 0)
			{
				yield return new ValidationResult("ValueOf and Values cannot be both set in the same condition", new[] { nameof(ValueOf), nameof(Values) });
			}

			if (Operator != null)
			{
				var requiresValue = Operator.GetRequiresValueAttribute();

				var valueCount = (Values?.Length ?? 0) + (string.IsNullOrWhiteSpace(ValueOf) ? 0 : 1);

				if (requiresValue == null && valueCount > 0)
				{
					yield return new ValidationResult($"Operator {Operator} does not wants a value", new[] { nameof(Operator), nameof(Values), nameof(ValueOf) });
				}

				if (requiresValue != null)
				{
					if (requiresValue.Exactly == 0 && valueCount == 0)
						yield return new ValidationResult($"Operator {Operator} requires one or more values", new[] { nameof(Operator), nameof(Values), nameof(ValueOf) });


					if (requiresValue.Exactly > 0 && requiresValue.Exactly != valueCount)
						yield return new ValidationResult($"Operator {Operator} requires exactly {requiresValue.Exactly} value{(requiresValue.Exactly == 1 ? "" : "s")}, while {valueCount} value{(valueCount == 1? "" : "s")} ha{(valueCount == 1 ? "s" : "ve")} been provided", new[] { nameof(Operator), nameof(Values), nameof(ValueOf) });
				}
			}

			if (Values != null && Values.Length > 0)
			{
				for (int i = 0; i < Values.Length; i++)
				{
					if (Values[i] == null)
					{
						yield return new ValidationResult($"Value[{i}] cannot be null", new[] { nameof(Values) });
					}
				}


				if (!string.IsNullOrWhiteSpace(ValueOf))
				{
					yield return new ValidationResult("Values and ValueOf must not be set simultaneously", new[] { nameof(Values), nameof(ValueOf) });
				}
			}
		}





		internal void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("condition");
			writer.WriteAttributeString("attribute", Attribute);
			writer.WriteAttributeString("operator",  Operator.GetDescription());

			if (!string.IsNullOrWhiteSpace(EntityName))
			{
				writer.WriteAttributeString("entityname", EntityName);
			}

			if (!string.IsNullOrWhiteSpace(ValueOf))
			{
				writer.WriteAttributeString("valueof", ValueOf);
			}

			if (Values != null && Values.Length > 0)
			{
				if (Values.Length == 1)
				{
					writer.WriteAttributeString("value", Values[0].ToString());
				}
				else
				{
					foreach (var value in Values)
					{
						writer.WriteElementString("value", value.ToString());
					}
				}
			}

			writer.WriteEndElement();
		}

		
	}
}