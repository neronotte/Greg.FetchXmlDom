using FluentAssertions;
using System.Text;
using System.Xml;

namespace Greg.FetchXmlDom.Model
{
	[TestFixture]
	public class AttributeExpressionTest
	{
		[Test]
		public void WriteTo_WithOnlyName_ShouldWork()
		{
			var a = new AttributeExpression
			{
				Name = "name"
			};

			var buider = new StringBuilder();
			var writer = XmlWriter.Create(buider, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false });

			a.WriteXml(writer);
			writer.Flush();

			buider.ToString().Should().Be("<attribute name=\"name\" />");
		}

		[Test]
		public void WriteTo_WithOnlyNameInConstructor_ShouldWork()
		{
			var a = new AttributeExpression("name");

			var buider = new StringBuilder();
			var writer = XmlWriter.Create(buider, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false });

			a.WriteXml(writer);
			writer.Flush();

			buider.ToString().Should().Be("<attribute name=\"name\" />");
		}

		[Test]
		public void WriteTo_WithAllProperties_ShouldWork_1()
		{
			var a = new AttributeExpression("name");
			a.Aggregate = AggregateFunction.Count;
			a.Alias = "alias";
			a.DateGrouping = DateGrouping.Day;
			a.Distinct = true;
			a.GroupBy = true;
			a.RowAggregate = true;


			var buider = new StringBuilder();
			var writer = XmlWriter.Create(buider, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false });

			a.WriteXml(writer);
			writer.Flush();

			buider.ToString().Should().Be("<attribute name=\"name\" aggregate=\"count\" alias=\"alias\" dategrouping=\"day\" distinct=\"true\" groupby=\"true\" rowaggregate=\"CountChildren\" />");
		}

		[Test]
		public void WriteTo_WithAllProperties_ShouldWork_2()
		{
			var a = new AttributeExpression("name");
			a.Aggregate = AggregateFunction.CountColumn;
			a.Alias = "alias1";
			a.DateGrouping = DateGrouping.FiscalPeriod;
			a.Distinct = true;
			a.GroupBy = true;
			a.RowAggregate = true;


			var buider = new StringBuilder();
			var writer = XmlWriter.Create(buider, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false });

			a.WriteXml(writer);
			writer.Flush();

			buider.ToString().Should().Be("<attribute name=\"name\" aggregate=\"countcolumn\" alias=\"alias1\" dategrouping=\"fiscal-period\" distinct=\"true\" groupby=\"true\" rowaggregate=\"CountChildren\" />");
		}
	}
}
