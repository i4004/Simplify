using System;
using System.IO;
using NUnit.Framework;

namespace Simplify.IO.Tests
{
	[TestFixture]
	public class FileHelperTester
	{
		//[Test]
		//public void IsGetLastLineOfFileGettingCorrectly()
		//{
		//	Assert.AreEqual("</div>", FileHelper.GetLastLineOfFile("TestData/Local/MasterTemplate.tpl"));
		//	Assert.AreEqual(null, FileHelper.GetLastLineOfFile("TestData/EmptyFile.txt"));
		//	Assert.Catch<ArgumentNullException>(() => FileHelper.GetLastLineOfFile(null));
		//	Assert.Catch<FileNotFoundException>(() => FileHelper.GetLastLineOfFile("NotFound"));
		//}

		[Test]
		public void IsFileLockedForRead()
		{
			Assert.Catch<ArgumentNullException>(() => FileHelper.IsFileLockedForRead(null));
			Assert.Catch<FileNotFoundException>(() => FileHelper.IsFileLockedForRead("NotFound"));
		}

		[Test]
		public void IsFileNameMadeValidCorrectly()
		{
			Assert.AreEqual("thisIsValid.txt", FileHelper.MakeValidFileName(@"thisIsValid.txt"));
			Assert.AreEqual("testfile_ do_.txt", FileHelper.MakeValidFileName(@"testfile: do?.txt"));
		}
	}
}
