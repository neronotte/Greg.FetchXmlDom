using System.ComponentModel;
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
	}
}