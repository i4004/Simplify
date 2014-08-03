using System.Xml.Linq;

using NUnit.Framework;

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XmlExtensionsTests
	{

		[Test]
		public void XElement_GetOuterXml_GettingCorrectly()
		{
			
			var element = XElement.Parse("<test><foo>data</foo></test>");

			Assert.AreEqual("<test><foo>data</foo></test>", element.OuterXml());
		}

		[Test]
		public void XElement_GetInnerXml_GettingCorrectly()
		{
			var element = XElement.Parse("<test><foo>data</foo></test>");

			Assert.AreEqual("<foo>data</foo>", element.InnerXml());
		}
	}
}