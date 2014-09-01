using System;
using NUnit.Framework;
using Simplify.Templates.Tests.Models;

namespace Simplify.Templates.Tests
{
	[TestFixture]
	public class TemplateModelExtensionsTests
	{
		private ITemplate _template;

		[SetUp]
		public void Initialize()
		{
			_template = Template.FromString("{Model.ID} {Model.Name} {Model.EMail} {Model.CreationTime}");			
		}

		[Test]
		public void Set_NullModel_ReplacesWithNothing()
		{
			// Assign
			TestModel model = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			_template.Set(model).SetModel();

			// Assert
			Assert.AreEqual("   ", _template.Get());
		}

		[Test]
		public void Set_Model_SetCorrectly()
		{
			// Assign
			var model = new TestModel { CreationTime = new DateTime(2014, 10, 5), Name = "Foo", EMail = "Foo@example.com", ID = 5};

			// Act
			_template.Set(model).With(x => x.CreationTime, x => x.ToString("dd.MM.yyyy")).SetModel();

			// Assert
			Assert.AreEqual("5 Foo Foo@example.com 05.10.2014", _template.Get());
		}

		[Test]
		public void Set_EverythingIsNull_SetCorrectly()
		{
			// Assign
			_template = Template.FromString("{Model.ID} {Model.Name} {Model.EMail}");
			var model = new TestModel();

			// Act
			_template.Set(model).SetModel();

			// Assert
			Assert.AreEqual("  ", _template.Get());
		}
	}
}