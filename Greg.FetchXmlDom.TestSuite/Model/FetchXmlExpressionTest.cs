using FluentAssertions;

namespace Greg.FetchXmlDom.Model
{
	[TestFixture]
	public class FetchXmlExpressionTest
	{
		[Test]
		public void ToString_Empty_ShouldWork()
		{
			var fetch = new FetchXmlExpression("account");
			var result = fetch.ToString();

			result.Should().Be(@"<fetch>
  <entity name='account' />
</fetch>".Replace("'", "\""));
		}


		[Test]
		public void ToString_WithTop_ShouldWork()
		{
			var fetch = new FetchXmlExpression("account")
			{
				Top = 10
			};
			var result = fetch.ToString();

			result.Should().Be(@"<fetch top='10'>
  <entity name='account' />
</fetch>".Replace("'", "\""));
		}

		[Test]
		public void ToString_WithBasicFilter_ShouldWork()
		{
			var fetch = new FetchXmlExpression("contact");
			var filter = fetch.AddFilter();
			filter.AddCondition("name", ConditionOperator.Equal, "John \"Doe\" Tribbiani");

			var result = fetch.ToString();

			result.Should().Be(@"<fetch>
  <entity name='contact'>
    <filter type='and'>
      <condition attribute='name' operator='eq' value='John &quot;Doe&quot; Tribbiani' />
    </filter>
  </entity>
</fetch>".Replace("'", "\""));
		}



		[Test]
		public void ToString_WithMediumComplexQuery01_ShouldWork()
		{
			var fetch = new FetchXmlExpression("contact");
			fetch.AddColumns("contactid", "fullname");

			var accountLink = new LinkEntityExpression("account", "accountid", "parentcustomerid", LinkType.Outer, "acct");
			accountLink.ColumnSet.Add("name");
			fetch.LinkEntities.Add(accountLink);

			fetch.AddConditionToOtherColumn("fullname", ConditionOperator.Equal, "acct.name");

			var result = fetch.ToString();

			result.Should().Be(@"<fetch>
  <entity name='contact'>
    <attribute name='contactid' />
    <attribute name='fullname' />
    <filter type='and'>
      <condition attribute='fullname' operator='eq' valueof='acct.name' />
    </filter>
    <link-entity name='account' from='accountid' to='parentcustomerid' link-type='outer' alias='acct'>
      <attribute name='name' />
    </link-entity>
  </entity>
</fetch>".Replace("'", "\""));
		}


		[Test]
		public void ToString_WithMediumComplexQuery02_ShouldWork()
		{
			var fetch = new FetchXmlExpression("contact");
			fetch.AddColumns("contactid", "fullname");

			var accountLink = fetch.AddLink("account", "accountid", "parentcustomerid", LinkType.Outer, "acct");
			accountLink.AddColumns("name");

			fetch.AddConditionToOtherColumn("fullname", ConditionOperator.Equal, "acct.name");

			var result = fetch.ToString();

			result.Should().Be(@"<fetch>
  <entity name='contact'>
    <attribute name='contactid' />
    <attribute name='fullname' />
    <filter type='and'>
      <condition attribute='fullname' operator='eq' valueof='acct.name' />
    </filter>
    <link-entity name='account' from='accountid' to='parentcustomerid' link-type='outer' alias='acct'>
      <attribute name='name' />
    </link-entity>
  </entity>
</fetch>".Replace("'", "\""));
		}


		[Test]
		public void ToString_WithMediumComplexQuery03_ShouldWork()
		{
			var fetch = new FetchXmlExpression("contact", aggregate:true);
			fetch.AddGroupColumn("lastname");
			fetch.AddAggregateColumn("contactid", AggregateFunction.Count, "Count");

			var result = fetch.ToString();

			result.Should().Be(@"<fetch aggregate='true'>
  <entity name='contact'>
    <attribute name='lastname' alias='lastname' groupby='true' />
    <attribute name='contactid' alias='Count' aggregate='count' />
  </entity>
</fetch>".Replace("'", "\""));
		}
	}
}
