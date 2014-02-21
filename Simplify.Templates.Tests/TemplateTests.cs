//using System.Collections.Generic;

//namespace Simplify.Templates.Tests
//{
//	[TestFixture]
//	public class TemplateTests
//	{
//		[Test]
//		public void TestTemplateMock()
//		{
//			var files = new Dictionary<string, MockFileData>();
//			files.Add("Templates/Foo.en.tpl", "Hello world!!!");
			
//			var fs = new MockFileSystem(files);

//			Template.FileSystem = fs;

//			Assert.AreEqual("Hello world!!!", new Template("Templates/Foo.en.tpl").Get());
//		}
//	}
//}