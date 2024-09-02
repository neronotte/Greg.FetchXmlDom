using System.ComponentModel;

namespace Greg.FetchXmlDom.Model
{
	public enum ConditionOperator
	{
		/// <summary>
		/// Returns all records in referenced record's hierarchical ancestry line.
		/// </summary>
		[Description("above")]
		[RequiresValue(Exactly = 1)]
		Above,

		/// <summary>
		/// The string occurs at the beginning of another string.
		/// </summary>
		[Description("begins-with")]
		[RequiresValue(Exactly = 1)]
		BeginsWith,

		/// <summary>
		/// The value is between two values.
		/// </summary>
		[Description("between")]
		[RequiresValue(Exactly = 2)]
		Between,

		/// <summary>
		/// The choice value is one of the specified values.
		/// </summary>
		[Description("contain-values")]
		[RequiresValue]
		ContainValues,

		/// <summary>
		/// The string ends with another string.
		/// </summary>
		[Description("ends-with")]
		[RequiresValue(Exactly = 1)]
		EndsWith,

		/// <summary>
		/// The values are compared for equality.
		/// </summary>
		[Description("eq")]
		[RequiresValue(Exactly = 1)]
		Equal,

		/// <summary>
		/// The value is equal to the specified business ID.
		/// </summary>
		[Description("eq-businessid")]
		EqualBusinessUnit,

		/// <summary>
		/// Returns the referenced record and all records above it in the hierarchy.
		/// </summary>
		[Description("eq-or-above")]
		[RequiresValue(Exactly = 1)]
		AboveOrEqual,

		/// <summary>
		/// Returns the referenced record and all child records below it in the hierarchy.
		/// </summary>
		[Description("eq-or-under")]
		[RequiresValue(Exactly = 1)]
		UnderOrEqual,

		/// <summary>
		/// The value is equal to the specified user ID.
		/// </summary>
		[Description("eq-userid")]
		EqualUser,

		/// <summary>
		/// The value is equal to the language for the user.
		/// </summary>
		[Description("eq-userlanguage")]
		EqualUserLanguage,

		/// <summary>
		/// When hierarchical security models are used, Equals current user or their reporting hierarchy.
		/// </summary>
		[Description("eq-useroruserhierarchy")]
		EqualUserOrUserHierarchy,

		/// <summary>
		/// When hierarchical security models are used, Equals current user and his teams or their reporting hierarchy and their teams.
		/// </summary>
		[Description("eq-useroruserhierarchyandteams")]
		EqualUserOrUserHierarchyAndTeams,

		/// <summary>
		/// The record is owned by a user or teams that the user is a member of.
		/// </summary>
		[Description("eq-useroruserteams")]
		EqualUserOrUserTeams,

		/// <summary>
		/// The record is owned by a user or teams that the user is a member of.
		/// </summary>
		[Description("eq-userteams")]
		EqualUserTeams,

		/// <summary>
		/// The value is greater than or equal to the compared value.
		/// </summary>
		[Description("ge")]
		[RequiresValue(Exactly = 1)]
		GreaterOrEqualThan,

		/// <summary>
		/// The value is greater than the compared value.
		/// </summary>
		[Description("gt")]
		[RequiresValue(Exactly = 1)]
		GreaterThan,

		/// <summary>
		/// The value exists in a list of values.
		/// </summary>
		[Description("in")]
		[RequiresValue]
		In,

		/// <summary>
		/// The value is within the specified fiscal period.
		/// </summary>
		[Description("in-fiscal-period")]
		[RequiresValue(Exactly = 1)]
		InFiscalPeriod,

		/// <summary>
		/// The value is within the specified fiscal period and year.
		/// </summary>
		[Description("in-fiscal-period-and-year")]
		[RequiresValue(Exactly = 1)]
		InFiscalPeriodAndYear,

		/// <summary>
		/// The value is within the specified year.
		/// </summary>
		[Description("in-fiscal-year")]
		[RequiresValue(Exactly = 1)]
		InFiscalYear,

		/// <summary>
		/// The value is within or after the specified fiscal period and year.
		/// </summary>
		[Description("in-or-after-fiscal-period-and-year")]
		[RequiresValue(Exactly = 1)]
		InOrAfterFiscalPeriodAndYear,

		/// <summary>
		/// The value is within or before the specified fiscal period and year.
		/// </summary>
		[Description("in-or-before-fiscal-period-and-year")]
		[RequiresValue(Exactly = 1)]
		InOrBeforeFiscalPeriodAndYear,

		/// <summary>
		/// The value is within the previous fiscal period.
		/// </summary>
		[Description("last-fiscal-period")]
		LastFiscalPeriod,

		/// <summary>
		/// The value is within the previous fiscal year.
		/// </summary>
		[Description("last-fiscal-year")]
		LastFiscalYear,

		/// <summary>
		/// The value is within the previous month including first day of the last month and last day of the last month.
		/// </summary>
		[Description("last-month")]
		LastMonth,

		/// <summary>
		/// The value is within the previous seven days including today.
		/// </summary>
		[Description("last-seven-days")]
		LastSevenDays,

		/// <summary>
		/// The value is within the previous week including Sunday through Saturday.
		/// </summary>
		[Description("last-week")]
		LastWeek,

		/// <summary>
		/// The value is within the previous specified number of days.
		/// </summary>
		[Description("last-x-days")]
		[RequiresValue(Exactly = 1)]
		LastXDays,

		/// <summary>
		/// The value is within the previous specified number of fiscal periods.
		/// </summary>
		[Description("last-x-fiscal-periods")]
		[RequiresValue(Exactly = 1)]
		LastXFiscalPeriods,

		/// <summary>
		/// The value is within the previous specified number of fiscal periods.
		/// </summary>
		[Description("last-x-fiscal-years")]
		[RequiresValue(Exactly = 1)]
		LastXFiscalYears,

		/// <summary>
		/// The value is within the previous specified number of hours.
		/// </summary>
		[Description("last-x-hours")]
		[RequiresValue(Exactly = 1)]
		LastXHours,

		/// <summary>
		/// The value is within the previous specified number of months.
		/// </summary>
		[Description("last-x-months")]
		[RequiresValue(Exactly = 1)]
		LastXMonths,

		/// <summary>
		/// The value is within the previous specified number of weeks.
		/// </summary>
		[Description("last-x-weeks")]
		[RequiresValue(Exactly = 1)]
		LastXWeeks,

		/// <summary>
		/// The value is within the previous specified number of years.
		/// </summary>
		[Description("last-x-years")]
		[RequiresValue(Exactly = 1)]
		LastXYears,

		/// <summary>
		/// The value is within the previous year.
		/// </summary>
		[Description("last-year")]
		LastYear,

		/// <summary>
		/// The value is less than or equal to the compared value.
		/// </summary>
		[Description("le")]
		[RequiresValue(Exactly = 1)]
		LessOrEqualThan,

		/// <summary>
		/// The character string is matched to the specified pattern.
		/// </summary>
		[Description("like")]
		[RequiresValue(Exactly = 1)]
		Like,

		/// <summary>
		/// The value is less than the compared value.
		/// </summary>
		[Description("lt")]
		[RequiresValue(Exactly = 1)]
		LessThan,

		/// <summary>
		/// The two values are not equal.
		/// </summary>
		[Description("ne")]
		[RequiresValue(Exactly = 1)]
		NotEqual,

		/// <summary>
		/// The value is not equal to the specified business ID.
		/// </summary>
		[Description("ne-businessid")]
		NotEqualBusinessUnit,

		/// <summary>
		/// The value is not equal to the specified user ID.
		/// </summary>
		[Description("ne-userid")]
		NotEqualUser,

		/// <summary>
		/// The value is within the next fiscal period.
		/// </summary>
		[Description("next-fiscal-period")]
		NextFiscalPeriod,

		/// <summary>
		/// The value is within the next fiscal year.
		/// </summary>
		[Description("next-fiscal-year")]
		NextFiscalYear,

		/// <summary>
		/// The value is within the next month.
		/// </summary>
		[Description("next-month")]
		NextMonth,

		/// <summary>
		/// The value is within the next seven days.
		/// </summary>
		[Description("next-seven-days")]
		NextSevenDays,

		/// <summary>
		/// The value is within the next week.
		/// </summary>
		[Description("next-week")]
		NextWeek,

		/// <summary>
		/// The value is within the next specified number of days.
		/// </summary>
		[Description("next-x-days")]
		[RequiresValue(Exactly = 1)]
		NextXDays,

		/// <summary>
		/// The value is within the next specified number of fiscal periods.
		/// </summary>
		[Description("next-x-fiscal-periods")]
		[RequiresValue(Exactly = 1)]
		NextXFiscalPeriods,

		/// <summary>
		/// The value is within the next specified number of fiscal years.
		/// </summary>
		[Description("next-x-fiscal-years")]
		[RequiresValue(Exactly = 1)]
		NextXFiscalYears,

		/// <summary>
		/// The value is within the next specified number of hours.
		/// </summary>
		[Description("next-x-hours")]
		[RequiresValue(Exactly = 1)]
		NextXHours,

		/// <summary>
		/// The value is within the next specified number of months.
		/// </summary>
		[Description("next-x-months")]
		[RequiresValue(Exactly = 1)]
		NextXMonths,

		/// <summary>
		/// The value is within the next specified number of weeks.
		/// </summary>
		[Description("next-x-weeks")]
		[RequiresValue(Exactly = 1)]
		NextXWeeks,

		/// <summary>
		/// The value is within the next specified number of years.
		/// </summary>
		[Description("next-x-years")]
		[RequiresValue(Exactly = 1)]
		NextXYears,

		/// <summary>
		/// The value is within the next X years.
		/// </summary>
		[Description("next-year")]
		NextYear,

		/// <summary>
		/// The string does not begin with another string.
		/// </summary>
		[Description("not-begin-with")]
		[RequiresValue(Exactly = 1)]
		NotBeginsWith,

		/// <summary>
		/// The value is not between two values.
		/// </summary>
		[Description("not-between")]
		[RequiresValue(Exactly = 2)]
		NotBetween,

		/// <summary>
		/// The choice value is not one of the specified values.
		/// </summary>
		[Description("not-contain-values")]
		[RequiresValue]
		NotContainValues,

		/// <summary>
		/// The string does not end with another string.
		/// </summary>
		[Description("not-end-with")]
		[RequiresValue(Exactly = 1)]
		NotEndsWith,

		/// <summary>
		/// The given value is not matched to a value in a subquery or a list.
		/// </summary>
		[Description("not-in")]
		[RequiresValue]
		NotIn,

		/// <summary>
		/// The character string does not match the specified pattern.
		/// </summary>
		[Description("not-like")]
		[RequiresValue(Exactly = 1)]
		NotLike,

		/// <summary>
		/// The value is not null.
		/// </summary>
		[Description("not-null")]
		NotNull,

		/// <summary>
		/// Returns all records not below the referenced record in the hierarchy.
		/// </summary>
		[Description("not-under")]
		[RequiresValue(Exactly = 1)]
		NotUnder,

		/// <summary>
		/// The value is null.
		/// </summary>
		[Description("null")]
		Null,

		/// <summary>
		/// The value is older than the specified number of days.
		/// </summary>
		[Description("olderthan-x-days")]
		[RequiresValue(Exactly = 1)]
		OlderThanXDays,

		/// <summary>
		/// The value is older than the specified number of hours.
		/// </summary>
		[Description("olderthan-x-hours")]
		[RequiresValue(Exactly = 1)]
		OlderThanXHours,

		/// <summary>
		/// The value is older than the specified number of minutes.
		/// </summary>
		[Description("olderthan-x-minutes")]
		[RequiresValue(Exactly = 1)]
		OlderThanXMinutes,

		/// <summary>
		/// The value is older than the specified number of months.
		/// </summary>
		[Description("olderthan-x-months")]
		[RequiresValue(Exactly = 1)]
		OlderThanXMonths,

		/// <summary>
		/// The value is older than the specified number of weeks.
		/// </summary>
		[Description("olderthan-x-weeks")]
		[RequiresValue(Exactly = 1)]
		OlderThanXWeeks,

		/// <summary>
		/// The value is older than the specified number of years.
		/// </summary>
		[Description("olderthan-x-years")]
		[RequiresValue(Exactly = 1)]
		OlderThanXYears,

		/// <summary>
		/// The value is on a specified date.
		/// </summary>
		[Description("on")]
		[RequiresValue(Exactly = 1)]
		On,

		/// <summary>
		/// The value is on or after a specified date.
		/// </summary>
		[Description("on-or-after")]
		[RequiresValue(Exactly = 1)]
		OnOrAfter,

		/// <summary>
		/// The value is on or before a specified date.
		/// </summary>
		[Description("on-or-before")]
		[RequiresValue(Exactly = 1)]
		OnOrBefore,

		/// <summary>
		/// The value is within the current fiscal period.
		/// </summary>
		[Description("this-fiscal-period")]
		ThisFiscalPeriod,

		/// <summary>
		/// The value is within the current fiscal year.
		/// </summary>
		[Description("this-fiscal-year")]
		ThisFiscalYear,

		/// <summary>
		/// The value is within the current month.
		/// </summary>
		[Description("this-month")]
		ThisMonth,

		/// <summary>
		/// The value is within the current week.
		/// </summary>
		[Description("this-week")]
		ThisWeek,

		/// <summary>
		/// The value is within the current year.
		/// </summary>
		[Description("this-year")]
		ThisYear,

		/// <summary>
		/// The value equals today's date.
		/// </summary>
		[Description("today")]
		Today,

		/// <summary>
		/// The value equals tomorrow's date.
		/// </summary>
		[Description("tomorrow")]
		Tomorrow,

		/// <summary>
		/// Returns all child records below the referenced record in the hierarchy.
		/// </summary>
		[Description("under")]
		[RequiresValue(Exactly = 1)]
		Under,

		/// <summary>
		/// The value equals yesterday's date.
		/// </summary>
		[Description("yesterday")]
		Yesterday,
	}
}