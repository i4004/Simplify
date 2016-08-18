using NUnit.Framework;

namespace Simplify.Extensions.Tests
{
	[TestFixture]
	public class BytesExtensionsTests
	{
		[Test]
		public void BytesArray_GetString_GettingCorrectly()
		{
			// Assign
			var bytes = new byte[] { 116, 0, 101, 0, 115, 0, 116, 0 };

			// Act & Assert
			Assert.AreEqual("test", bytes.GetString());
		}
	}
}