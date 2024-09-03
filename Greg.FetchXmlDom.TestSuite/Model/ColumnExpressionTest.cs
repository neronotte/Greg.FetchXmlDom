using FluentAssertions;
using System.Text;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	[TestFixture]
	public class ColumnExpressionTest
	{
		[Test]
		public void CreateAggregate_WithAliasNull_ShouldThrowException()
		{
			var action = () => ColumnExpression.CreateAggregateColumn("a", null, AggregateFunction.Avg);

			action.Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CreateAggregate_WithAliasEmpty_ShouldThrowException()
		{
			var action = () => ColumnExpression.CreateAggregateColumn("a", string.Empty, AggregateFunction.Avg);

			action.Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CreateAggregate_WithAliasBlank_ShouldThrowException()
		{
			var action = () => ColumnExpression.CreateAggregateColumn("a", "        ", AggregateFunction.Avg);

			action.Should().Throw<ArgumentNullException>();
		}



		[Test]
		public void WriteTo_WithOnlyName_ShouldWork()
		{
			var a = new ColumnExpression("name");

			var buider = new StringBuilder();
			var writer = XmlWriter.Create(buider, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false });

			a.WriteXml(writer);
			writer.Flush();

			buider.ToString().Should().Be("<attribute name=\"name\" />");
		}

		[Test]
		public void WriteTo_WithNameAndAlials_ShouldWork()
		{
			var a = new ColumnExpression("name", "alias");


			var buider = new StringBuilder();
			var writer = XmlWriter.Create(buider, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false });

			a.WriteXml(writer);
			writer.Flush();

			buider.ToString().Should().Be("<attribute name=\"name\" alias=\"alias\" />");
		}
	}
}
