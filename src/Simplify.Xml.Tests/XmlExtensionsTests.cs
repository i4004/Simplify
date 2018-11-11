using System.Xml.Linq;
using NUnit.Framework;

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XmlExtensionsTests
	{
		[Test]
		public void GetOuterXml_XElement_GettingCorrectly()
		{
			var element = XElement.Parse("<test><foo>data</foo></test>");

			Assert.AreEqual("<test><foo>data</foo></test>", element.OuterXml());
		}

		[Test]
		public void GetInnerXml_XElement_GettingCorrectly()
		{
			var element = XElement.Parse("<test><foo>data</foo></test>");

			Assert.AreEqual("<foo>data</foo>", element.InnerXml());
		}
	}
}