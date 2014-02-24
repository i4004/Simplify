using System;

using NUnit.Framework;

namespace Simplify.String.Tests
{
	[TestFixture]
	public class StringHelperTests
	{
		[Test]
		public void IsMobilePhoneParsingCorrectly()
		{
			Assert.AreEqual("+77012256245", StringHelper.ParseMobilePhone("+7 701 22 56 245"));
			Assert.AreEqual("+77012256245", StringHelper.ParseMobilePhone("+7701 2256 245"));
			Assert.AreEqual("77012256245", StringHelper.ParseMobilePhone("7-701-22-56-245"));
			Assert.AreEqual("87012256245", StringHelper.ParseMobilePhone("8(701)2256245"));
		}

		[Test]
		public void IsEMailValidatingCorrectly()
		{
			Assert.IsTrue(StringHelper.ValidateEMail("testname@pupkin.org"));
			Assert.IsTrue(StringHelper.ValidateEMail("someaddress.test.company@pupkin.org"));

			Assert.IsFalse(StringHelper.ValidateEMail(null));
			Assert.IsFalse(StringHelper.ValidateEMail("someaddress"));
			Assert.IsFalse(StringHelper.ValidateEMail("someaddress.test.company.org"));
			Assert.IsFalse(StringHelper.ValidateEMail("someaddress.test@company"));
			Assert.IsFalse(StringHelper.ValidateEMail("someaddress@test@company.org"));
			Assert.IsFalse(StringHelper.ValidateEMail("someaddress.test..company.org"));
			Assert.IsFalse(StringHelper.ValidateEMail("someaddress.test@companyorg"));
		}


		[Test]
		public void IsMobilePhoneValidatingCorrectly()
		{
			Assert.IsTrue(StringHelper.ValidateMobilePhone("+77015634321"));
			Assert.IsTrue(StringHelper.ValidateMobilePhone("87075967543"));

			Assert.IsFalse(StringHelper.ValidateMobilePhone(null));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("+7 701 56 34 321"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("8 (701) 56 34 321"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("+7015634321"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("7072245456"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("2739956"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("273 99 56"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("+7777345656"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("54042256707"));
			Assert.IsFalse(StringHelper.ValidateMobilePhone("+666567890345"));
		}

		[Test]
		public void IsIndistinctMatchingMatchCorrectly()
		{
			Assert.AreEqual(75, StringHelper.IndistinctMatching("asdfghjkl", "fghasdjkl"));
			Assert.AreEqual(100, StringHelper.IndistinctMatching("asdfghjkl", "asdfghjkl"));
			Assert.AreEqual(0, StringHelper.IndistinctMatching("qwerty", "asdfgh"));
			Assert.AreEqual(40, StringHelper.IndistinctMatching("qwerty", "ytrewq"));
			Assert.AreEqual(72.22, Math.Round(StringHelper.IndistinctMatching("qwe asd", "asd qwe"), 2));
			Assert.AreEqual(75, StringHelper.IndistinctMatching("qweasdzxc", "qwezxcasd"));
			Assert.AreEqual(89.58, Math.Round(StringHelper.IndistinctMatching("qweasdzxc", "qweasdzxx"), 2));
			Assert.AreEqual(0, StringHelper.IndistinctMatching(null, "fghasdjkl"));
			Assert.AreEqual(0, StringHelper.IndistinctMatching("a", "fghasdjkl", 0));
		}

		[Test]
		public void IsStripHtmlTagsCorrectly()
		{
			Assert.AreEqual("text", StringHelper.StripHtmlTags("<a href=\"link\">text</a><input name=\"login\" />"));
		}
	}
}
