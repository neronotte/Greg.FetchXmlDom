using FluentAssertions;

namespace Greg.FetchXmlDom.Model
{
	[TestFixture]
	public class FetchExpressionTest
	{
		[Test]
		public void Test01()
		{
			var fetch = new FetchXmlExpression();
			var result = fetch.ToString();

			result.Should().Be("<fetch />");
		}


		[Test]
		public void Test02()
		{
			var fetch = new FetchXmlExpression
			{
				Top = 10
			};
			var result = fetch.ToString();

			result.Should().Be("<fetch top=\"10\" />");
		}

		[Test]
		public void Test03()
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
		public void TestComplex01()
		{
			var fetch = new FetchXmlExpression("contact");
			fetch.Attributes.Add("contactid", "fullname");

			var accountLink = new LinkEntityExpression("account", "accountid", "parentcustomerid", LinkType.Outer, "acct");
			accountLink.Attributes.Add("name");
			fetch.LinkEntities.Add(accountLink);

			fetch.AddCondition(new ConditionExpression("fullname", ConditionOperator.Equal) { ValueOf = "acct.name" });

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
		public void TestComplex03()
		{
			var fetch = new FetchXmlExpression("contact");
			fetch.Attributes.Add("contactid", "fullname");

			var accountLink = fetch.AddLink("account", "accountid", "parentcustomerid", LinkType.Outer, "acct");
			accountLink.Attributes.Add("name");

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
	}
}
