using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Simplify.Xml
{
	/// <summary>
	/// Provides extensions for System.Xml.Linq classes
	/// </summary>
	public static class XmlExtensions
	{
		/// <summary>
		/// Gets the outer XML string of an XElement (inner XML and itself).
		/// </summary>
		/// <param name="element">The outer XML stringt.</param>
		/// <returns></returns>
		public static string OuterXml(this XElement element)
		{
			var xReader = element.CreateReader();
			xReader.MoveToContent();
			return xReader.ReadOuterXml();
		}

		/// <summary>
		/// Gets the inner XML string of an XElement.
		/// </summary>
		/// <param name="element">The inner XML stringt.</param>
		/// <returns></returns>
		public static string InnerXml(this XElement element)
		{
			var xReader = element.CreateReader();
			xReader.MoveToContent();
			return xReader.ReadInnerXml();
		}

		/// <summary>
		/// Removes all XML namespaces from string containing XML data.
		/// </summary>
		/// <param name="xmlData">The XML data.</param>
		/// <returns></returns>
		public static string RemoveAllXmlNamespaces(this string xmlData)
		{
			const string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
			var matchCol = Regex.Matches(xmlData, xmlnsPattern);

			foreach (Match m in matchCol)
				xmlData = xmlData.Replace(m.ToString(), "");

			return xmlData;
		}
	}
}