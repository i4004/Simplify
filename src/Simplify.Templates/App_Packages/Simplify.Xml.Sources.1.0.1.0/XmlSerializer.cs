using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Simplify.Xml
{
	/// <summary>
	/// Objects XML serialization/deserialization extensions
	/// </summary>
	internal static class XmlSerializer
	{
		/// <summary>
		/// Serializes the specified items list to a XML string.
		/// </summary>
		/// <typeparam name="T">Type of an item</typeparam>
		/// <param name="items">The items list to serialize.</param>
		/// <returns></returns>
		public static string Serialize<T>(IList<T> items)
		{
			using(var memoryStream = new MemoryStream())
			{
				var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
				serializer.Serialize(memoryStream, items);

				memoryStream.Position = 0;
				return new StreamReader(memoryStream).ReadToEnd();
			}			
		}

		/// <summary>
		/// Serialize object to XElement.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">Object to serialize.</param>
		/// <returns></returns>
		public static XElement ToXElement<T>(T obj)
		{
			using (var memoryStream = new MemoryStream())
			{
				using (TextWriter streamWriter = new StreamWriter(memoryStream))
				{
					var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
					xmlSerializer.Serialize(streamWriter, obj);
					return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
				}
			}
		}

		/// <summary>
		/// Deserialize XElement to object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xElement">The XElement to deserialize.</param>
		/// <returns></returns>
		public static T FromXElement<T>(XElement xElement)
		{
			using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xElement.ToString())))
			{
				var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
				return (T)xmlSerializer.Deserialize(memoryStream);
			}
		}
	}
}
