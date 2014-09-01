using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class IntegrationTemplateTests
	{
		private static void TestTemplate(ITemplate tpl, ITemplate masterTemplate, ITemplate testTemplate, ITemplate masterTestTemplate)
		{
			masterTemplate.Set("Title", "Hello world!!!");

			tpl.Set("ItemTitle", "Item1");

			tpl.Add("var1", 15.5m);
			tpl.Add("var1", (long)16);
			tpl.Add("var1", 17);
			tpl.Add("var1", 18.16);
			tpl.Add("var1", new object());

			tpl.Set("var2", 15.5m)
				.Set("var3", (long)16)
				.Set("var4", 17)
				.Set("var5", 18.16)
				.Set("var6", new object());

			Assert.AreEqual(testTemplate.Get(), tpl.Get());

			masterTemplate.Add("Items", tpl.GetAndRoll());

			tpl.Set("ItemTitle", "Item2");

			tpl.Add("var1", 16.5m)
				.Add("var1", (long)255)
				.Add("var1", 300)
				.Add("var1", 26.15)
				.Add("var1", new object());

			tpl.Set("var2", 10.5m);
			tpl.Set("var3", (long)1);
			tpl.Set("var4", 3);
			tpl.Set("var5", 4.1233);
			tpl.Set("var6", new object());

			tpl.Set("NotFoundItem", (ITemplate)null);

			masterTemplate.Add("Items", tpl);

			Assert.AreEqual(masterTestTemplate.Get(), masterTemplate.Get());
		}

		[Test]
		public void LocalTemplate_LoadAndUse_LoadedCorectly()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");
			TestTemplate(new Template("TestData/Local/TemplateTest.tpl"), new Template(File.ReadAllText("TestData/Local/MasterTemplate.tpl"), false),
				new Template("TestData/Local/TemplateTestResult.tpl"), new Template("TestData/Local/MasterTemplateResult.tpl"));
		}

		[Test]
		public void EmbeddedTemplate_LoadAndUse_LoadedCorrectly()
		{
			TestTemplate(new Template(Assembly.GetExecutingAssembly(), "TestData.Embedded.TemplateTest.tpl", "ru"), Template.FromManifest("TestData.Embedded.MasterTemplate.tpl"),
				new Template(Assembly.GetAssembly(typeof(IntegrationTemplateTests)), "TestData.Embedded.TemplateTestResult.tpl"), Template.FromManifest("TestData.Embedded.MasterTemplateResult.tpl"));
		}

		[Test]
		public void LoadWithLineEndingsFix_FixedCorrectly()
		{
			Assert.AreEqual("data<br />", new Template("data" + Environment.NewLine, true).Get());
		}

		[Test]
		public void LoadTemplate_ExceptionsThrownCorrectly()
		{
			Assert.Throws<ArgumentNullException>(() => new Template(null, false));
			Assert.Throws<ArgumentNullException>(() => new Template(null));
			Assert.Throws<TemplateException>(() => new Template("NotFound"));
			Assert.Throws<ArgumentNullException>(() => new Template(Assembly.GetCallingAssembly(), null));
			Assert.Throws<ArgumentNullException>(() => new Template((Assembly)null, null));
			Assert.Throws<TemplateException>(() => new Template(Assembly.GetCallingAssembly(), ("NotFound")));
		}
	}
}
