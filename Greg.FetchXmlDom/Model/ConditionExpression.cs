using Newtonsoft.Json.Linq;
using System;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
    /// <summary>
    /// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/condition"/>
    /// </summary>
    public class ConditionExpression
	{
		public ConditionExpression(string columnName, ConditionOperator conditionOperator)
		{
			if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;

			ValidateOperator(conditionOperator, null, null);
		}

		public ConditionExpression(string columnName, ConditionOperator conditionOperator, object value)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
			this.Values = value != null ? new[] { value } : null;

			ValidateOperator(conditionOperator, this.Values, null);
		}

		public ConditionExpression(string columnName, ConditionOperator conditionOperator, object[] values)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
			this.Values = values;

			ValidateOperator(conditionOperator, this.Values, null);
		}

		public ConditionExpression(string entityAlias, string columnName, ConditionOperator conditionOperator)
		{
			if (string.IsNullOrWhiteSpace(entityAlias))
				throw new ArgumentException("entityAlias cannot be empty", nameof(entityAlias));
			if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
			this.EntityName = entityAlias;

			ValidateOperator(conditionOperator, null, null);
		}

		public ConditionExpression(string entityAlias, string columnName, ConditionOperator conditionOperator, object value)
		{
			if (string.IsNullOrWhiteSpace(entityAlias))
				throw new ArgumentException("entityAlias cannot be empty", nameof(entityAlias));
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
			this.EntityName = entityAlias;
			this.Values = value != null ? new[] { value } : null;

			ValidateOperator(conditionOperator, this.Values, null);
		}

		public ConditionExpression(string entityAlias, string columnName, ConditionOperator conditionOperator, object[] values)
		{
			if (string.IsNullOrWhiteSpace(entityAlias))
				throw new ArgumentException("entityAlias cannot be empty", nameof(entityAlias));
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
			this.EntityName = entityAlias;
			this.Values = values;

			ValidateOperator(conditionOperator, this.Values, null);
		}

		private ConditionExpression(string columnName, ConditionOperator conditionOperator, string valueOf)
		{
			if (string.IsNullOrWhiteSpace(columnName))
				throw new ArgumentException("columnName cannot be empty", nameof(columnName));

			this.Attribute = columnName;
			this.Operator = conditionOperator;
			this.ValueOf = valueOf;

			ValidateOperator(conditionOperator, null, valueOf);
		}


		public static ConditionExpression CreateConditionToOtherColumn(string attribute, ConditionOperator conditionOperator, string otherColumn)
		{
			var condition = new ConditionExpression(attribute, conditionOperator, otherColumn);
			return condition;
		}

		private static void ValidateOperator(ConditionOperator conditionOperator, object[] values, string valueOf)
		{
			var requiresValue = conditionOperator.GetRequiresValueAttribute();

			var valueCount = (values?.Length ?? 0) + (string.IsNullOrWhiteSpace(valueOf) ? 0 : 1);

			if (requiresValue == null && valueCount > 0)
			{
				throw new ArgumentException($"Operator {conditionOperator} does not wants a value", nameof(conditionOperator));
			}

			if (requiresValue != null)
			{
				if (requiresValue.Exactly == 0 && valueCount == 0)
					throw new ArgumentException($"Operator {conditionOperator} requires one or more values", nameof(conditionOperator));


				if (requiresValue.Exactly > 0 && requiresValue.Exactly != valueCount)
					throw new ArgumentException($"Operator {conditionOperator} requires exactly {requiresValue.Exactly} value{(requiresValue.Exactly == 1 ? "" : "s")}, while {valueCount} value{(valueCount == 1 ? "" : "s")} ha{(valueCount == 1 ? "s" : "ve")} been provided", nameof(conditionOperator));
			}
		}








		/// <summary>
		/// The name of the column with the value to apply the filter.
		/// </summary>
		public string Attribute { get; }

		/// <summary>
		/// The operator to apply with the filter.
		/// </summary>
		public ConditionOperator Operator { get; }

		/// <summary>
		/// Specify the link-entity name or alias that the condition should be applied to after the outer join. 
		/// Learn more about filters on link-entity <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filters-on-link-entity"/>
		/// </summary>
		public string EntityName { get; private set; }


		/// <summary>
		/// The value to test the column value with the operator.
		/// </summary>
		private object[] _values;
		public object[] Values
		{
			get => _values;
			set  
			{
                if ( value != null )
				{
					for (int i = 0; i < value.Length; i++)
					{
						if (value[i] == null)
						{
							throw new ArgumentException($"Value[{i}] cannot be null", nameof(Values));
						}
					}
				}
				_values = value;
			}
		}

		/// <summary>
		/// The name of the column in the same table that has the value to test the column value with the operator. 
		/// Learn more about filtering on other column values. <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filter-on-column-values-in-the-same-row"/>
		/// </summary>
		public string ValueOf { get; private set; }








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