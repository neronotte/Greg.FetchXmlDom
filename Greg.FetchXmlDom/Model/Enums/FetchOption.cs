namespace Greg.FetchXmlDom.Model
{
	/// <summary>
	/// Use these values with the fetch "options" attribute to specify SQL Server hints to apply to the query. 
	/// 
	/// See <see cref="https://learn.microsoft.com/en-us/power-apps/developer/data-platform/fetchxml/reference/fetch#options"/>
	/// </summary>
	public enum FetchOption
	{
		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#optimize-for-unknown"/>
		/// </summary>
		OptimizeForUnknown,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#force-order"/>
		/// </summary>
		ForceOrder,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#use_hint"/>
		/// </summary>
		DisableRowGoal,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#use_hint"/>
		/// </summary>
		EnableOptimizerHotfixes,
		
		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#-loop--merge--hash--join"/>
		/// </summary>
		LoopJoin,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#-loop--merge--hash--join"/>
		/// </summary>
		MergeJoin,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#-loop--merge--hash--join"/>
		/// </summary>
		HashJoin,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#no_performance_spool"/>
		/// </summary>
		NO_PERFORMANCE_SPOOL,

		/// <summary>
		/// <see cref="https://learn.microsoft.com/en-us/sql/t-sql/queries/hints-transact-sql-query#use_hint"/>
		/// </summary>
		ENABLE_HIST_AMENDMENT_FOR_ASC_KEYS
	}
}
