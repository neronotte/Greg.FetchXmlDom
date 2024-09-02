using FluentAssertions;
using System;

namespace Greg.FetchXmlDom.Model
{
	[TestFixture]
	public class AttributeCollectionTest
	{
		[Test]
        public void NewCollection_Should_BeEmpty()
		{
			var attributes = new AttributeCollection();

			attributes.Count.Should().Be(0, "The collection should not contain attributes when you create it");
			attributes.All.Should().Be(false, "The collection \"All\" property should be false");
		}

		[Test]
		public void Add_WithAll_Should_NotContainAttributes()
		{
			var attributes = new AttributeCollection();
			attributes.All = true;

			attributes.Count.Should().Be(0);
			attributes.All.Should().Be(true);
		}

		[Test]
		public void Add_WithAttribute_Should_AddAttribute()
		{
			var attributes = new AttributeCollection();

			var attribute = new AttributeExpression();
			attributes.Add(attribute);


			attributes.Count.Should().Be(1);
			attributes.All.Should().Be(false);

			var currentAttribute = attributes[0];
			currentAttribute.Should().Be(attribute, "The attribute added should be the same as the one retrieved");
		}


		[Test]
		public void Add_WithAttributeAndAll_Should_ClearCollectionAndSetAllToTrue()
		{
			var attributes = new AttributeCollection();

			var attribute = new AttributeExpression();
			attributes.Add(attribute);
			attributes.All = true;

			attributes.Count.Should().Be(0, "The collection should not contain attributes after you set All=true");
			attributes.All.Should().Be(true);
		}


		[Test]
		public void Add_WithAllThenAttribute_Should_ClearAllAndAddAttributeToCollection()
		{
			var attributes = new AttributeCollection();

			var attribute = new AttributeExpression();
			attributes.All = true;
			attributes.Add(attribute);

			attributes.Count.Should().Be(1);
			attributes.All.Should().Be(false);
		}

		[Test]
		public void AddList_With1Attribute_Should_Add1Attribute()
		{
			var attributes = new AttributeCollection
			{
				"attribute1"
			};

			attributes.Count.Should().Be(1);
			attributes.All.Should().Be(false);


			var currentAttribute = attributes[0];
			currentAttribute.Name.Should().Be("attribute1");
			currentAttribute.Aggregate.Should().BeNull();
			currentAttribute.Alias.Should().BeNull();
			currentAttribute.DateGrouping.Should().BeNull();
			currentAttribute.Distinct.Should().Be(false);
			currentAttribute.GroupBy.Should().Be(false);
			currentAttribute.RowAggregate.Should().Be(false);
			currentAttribute.UserTimeZone.Should().BeNull();
		}

		[Test]
		public void AddList_With3Attributes_Should_Add3Attributes()
		{
			var attributes = new AttributeCollection
			{
				"attribute0", "attribute1", "attribute2"
			};

			attributes.Count.Should().Be(3);
			attributes.All.Should().Be(false);

			for (var i = 0; i < attributes.Count; i++)
			{
				var currentAttribute = attributes[i];
				currentAttribute.Name.Should().Be("attribute" + i);
				currentAttribute.Aggregate.Should().BeNull();
				currentAttribute.Alias.Should().BeNull();
				currentAttribute.DateGrouping.Should().BeNull();
				currentAttribute.Distinct.Should().Be(false);
				currentAttribute.GroupBy.Should().Be(false);
				currentAttribute.RowAggregate.Should().Be(false);
				currentAttribute.UserTimeZone.Should().BeNull();
			}
		}

		[Test]
		public void AddList_With3Attributes_Should_Add3Attributes_2()
		{
			var attributes = new AttributeCollection();
			attributes.Add("attribute0", "attribute1", "attribute2");

			attributes.Count.Should().Be(3);
			attributes.All.Should().Be(false);

			for (var i = 0; i < attributes.Count; i++)
			{
				var currentAttribute = attributes[i];
				currentAttribute.Name.Should().Be("attribute" + i);
				currentAttribute.Aggregate.Should().BeNull();
				currentAttribute.Alias.Should().BeNull();
				currentAttribute.DateGrouping.Should().BeNull();
				currentAttribute.Distinct.Should().Be(false);
				currentAttribute.GroupBy.Should().Be(false);
				currentAttribute.RowAggregate.Should().Be(false);
				currentAttribute.UserTimeZone.Should().BeNull();
			}
		}
	}
}
