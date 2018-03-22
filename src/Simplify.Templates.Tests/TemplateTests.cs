using System.Collections.Generic;
using System.IO;
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
			var tpl = Template.FromString("test");

			Assert.AreEqual("test", tpl.Get());
			Assert.AreEqual("en", tpl.Language);
		}

		[Test]
		public void Load_FromFileSystem_LoadedCorrectly()
		{
			var file = Path.Combine("Templates", "Foo.en.tpl");

			var files = new Dictionary<string, MockFileData> { { file, "test" } };

			var fs = new MockFileSystem(files);

			Template.FileSystem = fs;

			Assert.AreEqual("test", new Template(file).Get());
		}
	}
}