using System.Xml.Linq;
using NUnit.Framework;

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XmlExtensionsTests
	{
		private const string InputString = "<test><foo>data</foo></test>";
		private const string ExpectedOuter = "<test><foo>data</foo></test>";
		private const string ExpectedInner = "<foo>data</foo>";

		[Test]
		public void XElement_GetOuterXml_GettingCorrectly()
		{
			var element = XElement.Parse(InputString);

			Assert.AreEqual(ExpectedOuter, element.OuterXml());
		}

		[Test]
		public void XElement_GetInnerXml_GettingCorrectly()
		{
			var element = XElement.Parse(InputString);

			Assert.AreEqual(ExpectedInner, element.InnerXml());
		}
	}
}