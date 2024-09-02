namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/link-entity#link-type-options"/>
	/// </summary>
	public enum LinkType 
	{
		/// <summary>
		/// Default. Restricts results to rows with matching values in both tables.
		/// </summary>
		Inner,

		/// <summary>
		///	Includes results from the parent element that don't have a matching value.
		/// </summary>
		Outer,

		/// <summary>
		/// Use this within a filter element. 
		/// Restricts results to parent rows with any matching rows in the linked entity. 
		/// Learn to use any to filter values on related tables <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filter-on-values-in-related-records"/>
		/// </summary>
		Any,

		/// <summary>
		/// Use this within a filter element. 
		/// Restricts results to parent rows with no matching rows in the linked entity. 
		/// Learn to use any to filter values on related tables <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filter-on-values-in-related-records"/>
		/// </summary>
		NotAny,

		/// <summary>
		/// Use this within a filter element. 
		/// Restricts results to parent rows where rows with matching from column value exist in the link entity 
		/// but none of those matching rows satisfy the additional filters defined for this link entity. 
		/// You need to invert the additional filters to find parent rows where every matching link entity row satisfies some additional criteria. 
		/// Learn to use any to filter values on related tables <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filter-on-values-in-related-records"/>
		/// </summary>
		All,

		/// <summary>
		/// Use this within a filter element. 
		/// Restricts results to parent rows with any matching rows in the linked entity. 
		/// This link type is equivalent to any despite the name. 
		/// Learn to use any to filter values on related tables <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/filter-rows#filter-on-values-in-related-records"/>
		/// </summary>
		NotAll,

		/// <summary>
		/// A variant of inner that can provide performance benefits. 
		/// Uses an EXISTS condition in the where clause. 
		/// Use this when multiple copies of the parent row are not necessary in the results. 
		/// Learn more about exists and in <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/join-tables#use-exists-or-in-link-types"/>
		/// </summary>
		Exists,

		/// <summary>
		/// A variant of inner that can provide performance benefits. 
		/// Uses an IN condition in the where clause. Use this when multiple copies of the parent row are not necessary in the results. 
		/// Learn more about exists and in <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/join-tables#use-exists-or-in-link-types"/>
		/// </summary>
		In,

		/// <summary>
		/// A variant of inner that can provide performance benefits. 
		/// Use this type when only a single example of a matching row from the linked entity is sufficient 
		/// and multiple copies of the parent row in the results aren't necessary. 
		/// Learn more about matchfirstrowusingcrossapply <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/join-tables#use-matchfirstrowusingcrossapply-link-type"/>
		/// </summary>
		MatchFirstRowUsingCrossApply
	}
}
