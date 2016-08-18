using NUnit.Framework;

namespace Simplify.Extensions.Tests
{
	[TestFixture]
	public class StringExtensionsTests
	{
		[Test]
		public void String_ToBytesArray_ConvertedCorrectly()
		{
			Assert.AreEqual(new byte[] { 116, 0, 101, 0, 115, 0, 116, 0 }, "test".ToBytesArray());
		}

		[Test]
		public void TryToDateTimeExact_WrongValue_COnvertedCorrectly()
		{
			// Assign
			const string str = "test";

			// Act & Assert
			Assert.IsNull(str.TryToDateTimeExact("dd.MM.yy"));
		}

		[Test]
		public void TryToDateTimeExact_CorrectValue_COnvertedCorrectly()
		{
			// Assign
			const string str = "12.03.13";

			// Act
			var time = str.TryToDateTimeExact("dd.MM.yy");

			// Assert

			Assert.IsNotNull(time);
			Assert.AreEqual(12, time.Value.Day);
			Assert.AreEqual(3, time.Value.Month);
			Assert.AreEqual(2013, time.Value.Year);
		}
	}
}