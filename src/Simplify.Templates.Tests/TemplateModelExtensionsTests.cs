using System;
using NUnit.Framework;
using Simplify.Templates.Tests.Models;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateModelExtensionsTests
	{

		[Test]
		public void Set_NullModel()
		{
			// Assign

			var tpl = Template.FromString("{Model.Name} {Model.EMail} {Model.CreationTime}");
			//var model = new TestModel { CreationTime = new DateTime(2014, 10, 5), Name = "Foo", EMail = "Foo@example.com" };

			TestModel a = null;

			// Act
			//tpl.Set(a);
		}

		[Test]
		public void Set_Model_SetCorrectly()
		{
			// Assign

			var tpl = Template.FromString("{Model.Name} {Model.EMail} {Model.CreationTime}");
			var model = new TestModel { CreationTime = new DateTime(2014, 10, 5), Name = "Foo", EMail = "Foo@example.com" };

			// Act
			tpl.Set(model).Set(x => x.CreationTime, x => x.CreationTime.ToString("dd.MM.yyyy")).Export();
			//tpl.Set(model).Export();

			// Assert
			Assert.AreEqual("Foo Foo@example.com 05.10.2014", tpl.Get());
		} 
	}
}