using System.Xml.Linq;
using NUnit.Framework;
using Simplify.Templates;

namespace Simplify.Xml.Tests
{
	[TestFixture]
	public class XmlExtensionsTests
	{
		[Test]
		public void GetOuterXml_XElement_GettingCorrectly()
		{
			// Assign
			var element = XElement.Parse("<test><foo>data</foo></test>");

			// Act & Assert
			Assert.AreEqual("<test><foo>data</foo></test>", element.OuterXml());
		}

		[Test]
		public void GetInnerXml_XElement_GettingCorrectly()
		{
			// Assign
			var element = XElement.Parse("<test><foo>data</foo></test>");

			// Act & Assert
			Assert.AreEqual("<foo>data</foo>", element.InnerXml());
		}

		[Test]
		public void RemoveAllXmlNamespaces_XmlStringWithBNamespaces_XmlStringWithoutNamespaces()
		{
			// Assign
			var str = Template.FromManifest("TestData.XmlWithNamespaces.xml").Get();

			// Act & Assert
			Assert.AreEqual(Template.FromManifest("TestData.XmlWithoutNamespaces..xml").Get(), str.RemoveAllXmlNamespaces());
		}
	}
}