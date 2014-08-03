using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;

using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateTests
	{
		[Test]
		public void Load_FromString_LoadedCorrectly()
		{
			var tpl = new Template("test", false);

			Assert.AreEqual("test", tpl.Get());
		}

		[Test]
		public void Load_FromFileSystem_LoadedCorrectly()
		{
			var files = new Dictionary<string, MockFileData>();
			files.Add("Templates/Foo.en.tpl", "test");

			var fs = new MockFileSystem(files);

			Template.FileSystem = fs;

			Assert.AreEqual("test", new Template("Templates/Foo.en.tpl").Get());
		}
	}
}