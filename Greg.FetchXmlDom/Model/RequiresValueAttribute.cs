using System;

namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// Indicates that the operation requires a value.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
	public class RequiresValueAttribute : Attribute
	{
		public int Exactly { get; set; }
	}
}
