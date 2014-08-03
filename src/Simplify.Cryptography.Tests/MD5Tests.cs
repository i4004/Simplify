using NUnit.Framework;

namespace Simplify.Cryptography.Tests
{
	[TestFixture]
	public class MD5Tester
	{
		[Test]
		public void IsGetHashGettingCorrectly()
		{
			Assert.AreEqual("c4ca4238a0b923820dcc509a6f75849b", MD5.GetHash("1"));
		}

		[Test]
		public void IsVerifyHashCorrectly()
		{
			Assert.IsTrue(MD5.VerifyHash("1", "c4ca4238a0b923820dcc509a6f75849b"));
			Assert.IsFalse(MD5.VerifyHash("1", "c4ca4238a0b923820dcc509a6f75849c"));
		}
	}
}
