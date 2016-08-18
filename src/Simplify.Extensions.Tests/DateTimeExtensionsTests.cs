using NUnit.Framework;

namespace Simplify.Extensions.Tests
{
	[TestFixture]
	public class DateTimeExtensionsTests
	{
		[Test]
		public void TrimMilliseconds_DateTime_MillisecondsTrimmed()
		{
			// Act
			var result = new System.DateTime(2015, 02, 03, 14, 22, 13, 456).TrimMilliseconds();

			// Assert
			Assert.AreEqual(0, result.Millisecond);
		}
	}
}