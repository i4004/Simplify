using NUnit.Framework;

using Simplify.Extensions.Bytes;

namespace Simplify.Extensions.Tests
{
	[TestFixture]
	public class BytesExtensionsTester
	{
		[Test]
		public void BytesArray_GetSring_GettingCorrectly()
		{
			var bytes = new byte[] {116, 0, 101, 0, 115, 0, 116, 0};
			Assert.AreEqual("test", bytes.GetString());
		}
	}
}