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
		public void XNode_GetOuterXml_GettingCorrectly()
		{
			XNode element = XElement.Parse(InputString);

			Assert.AreEqual(ExpectedOuter, element.OuterXml());
		}

		[Test]
		public void XNode_GetInnerXml_GettingCorrectly()
		{
			XNode element = XElement.Parse(InputString);

			Assert.AreEqual(ExpectedInner, element.InnerXml());
		}
	}
}