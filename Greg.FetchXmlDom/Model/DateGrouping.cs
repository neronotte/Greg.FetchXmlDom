namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/attribute#date-grouping-options"/>
	/// </summary>
	public enum DateGrouping
	{
		/// <summary>
		/// Group by day of the month
		/// </summary>
		Day,

		/// <summary>
		/// Group by week of the year
		/// </summary>
		Week,

		/// <summary>
		/// Group by month of the year
		/// </summary>
		Month,

		/// <summary>
		/// Group by quarter of the fiscal year
		/// </summary>
		Quarter,

		/// <summary>
		/// Group by the year
		/// </summary>
		Year,

		/// <summary>
		/// Group by period of the fiscal year
		/// </summary>
		FiscalPeriod,

		/// <summary>
		/// 	Group by the fiscal year
		/// </summary>
		FiscalYear
	}
}
