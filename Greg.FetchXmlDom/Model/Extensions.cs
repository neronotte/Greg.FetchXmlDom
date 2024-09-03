using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Greg.FetchXmlDom.Model
{
    internal static class Extensions
	{
		internal static string GetDescription<T>(this T source)
		{
			FieldInfo fi = source.GetType().GetField(source.ToString());

			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0) return attributes[0].Description;
			else return source.ToString();
		}



		internal static RequiresValueAttribute GetRequiresValueAttribute<T>(this T source)
		{
			var fi = source.GetType().GetField(source.ToString());
			var attributes = (RequiresValueAttribute[])fi.GetCustomAttributes(typeof(RequiresValueAttribute), false);

			if (attributes != null && attributes.Length > 0) return attributes[0];
			else return null;
		}


		public static void AddChildren(this ICollection<ValidationResult> parentValidationResults, ICollection<ValidationResult> childValidationResults, string prefix)
		{
			foreach (var childValidationResult in childValidationResults)
			{
				childValidationResult.ErrorMessage = $"{prefix}.{childValidationResult.ErrorMessage}";
				parentValidationResults.Add(childValidationResult);
			}
		}

		internal static bool TryValidateList<T>(this IReadOnlyList<T> list, string collectionName, ValidationContext parentValidationContext, ICollection<ValidationResult> validationResults)
		{
			if (list == null) return true;

			var result = true;

			for (int i = 0; i < list.Count; i++)
			{
				var item = list[i];

				var partialValidationResults = new List<ValidationResult>();
				if (!Validator.TryValidateObject(item, new ValidationContext(item, parentValidationContext.Items), partialValidationResults, true))
				{
					validationResults.AddChildren(partialValidationResults, $"{collectionName}[{i}]");
					result = false;
				}
			}

			return result;
		}
	}
}