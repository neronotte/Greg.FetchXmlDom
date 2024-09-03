namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/attribute#aggregate-functions"/>
	/// </summary>
	public enum AggregateFunction
	{
		/// <summary>
		/// The average value of the column values with data.
		/// </summary>
		Avg,

		/// <summary>
		/// The number of rows.
		/// </summary>
		Count,

		/// <summary>
		/// The number of rows with data in that column.
		/// </summary>
		CountColumn,

		/// <summary>
		/// The maximum value of the rows in that column.
		/// </summary>
		Max,

		/// <summary>
		/// The minimum value of the rows in that column.
		/// </summary>
		Min,

		/// <summary>
		/// The total value of the column values with data.
		/// </summary>
		Sum
	}
}
