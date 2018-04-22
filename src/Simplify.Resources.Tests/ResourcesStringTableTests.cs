using System.Reflection;
using NUnit.Framework;

namespace Simplify.Resources.Tests
{
	[TestFixture]
	public class ResourcesStringTableTests
	{
		private IResourcesStringTable _uow;

		[SetUp]
		public void Initialize()
		{
		}

		[Test]
		public void ResourcesStringTableIndexer_ExistingString_Found()
		{
			//Assign

			_uow = new ResourcesStringTable(true, "ProgramResources");

			// Act
			var testString = _uow["TestString"];

			// Assert
			Assert.AreEqual("Hello World!", testString);
		}

		[Test]
		public void ResourcesStringTableIndexer_CustomAssemblyExistingString_Found()
		{
			//Assign

			_uow = new ResourcesStringTable(Assembly.GetAssembly(typeof(ResourcesStringTableTests)), "ProgramResources");

			// Act
			var testString = _uow["TestString"];

			// Assert
			Assert.AreEqual("Hello World!", testString);
		}

		[Test]
		public void ResourcesStringTableIndexer_NoExistingString_Null()
		{
			// Act
			var testString = _uow["TestString2"];

			// Assert
			Assert.IsNull(testString);
		}
	}
}