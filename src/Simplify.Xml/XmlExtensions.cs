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
		/// Gets the outer XML string of an XDocument (inner XML and itself).
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static string OuterXml(this XDocument element)
		{
			var xReader = element.CreateReader();
			xReader.MoveToContent();
			return xReader.ReadOuterXml();
		}

		/// <summary>
		/// Gets the outer XML string of an XNode (inner XML and itself).
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		public static string OuterXml(this XNode element)
		{
			var xReader = element.CreateReader();
			xReader.MoveToContent();
			return xReader.ReadOuterXml();
		}

		/// <summary>
		/// Gets the inner XML string of an XDocument.
		/// </summary>
		/// <param name="element">The inner XML stringt.</param>
		/// <returns></returns>
		public static string InnerXml(this XDocument element)
		{
			var xReader = element.CreateReader();
			xReader.MoveToContent();
			return xReader.ReadInnerXml();
		}

		/// <summary>
		/// Gets the inner XML string of an XNode.
		/// </summary>
		/// <param name="element">The inner XML stringt.</param>
		/// <returns></returns>
		public static string InnerXml(this XNode element)
		{
			var xReader = element.CreateReader();
			xReader.MoveToContent();
			return xReader.ReadInnerXml();
		}
	}
}