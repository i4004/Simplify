using NUnit.Framework;

namespace Simplify.Resources.Tests
{
	[TestFixture]
	public class StringTableTests
	{
		private IResourcesStringTable _uow;

		[SetUp]
		public void Initialize()
		{
			_uow = new ResourcesStringTable(true, "ProgramResources");
		}

		[Test]
		public void ResourcesStringTableIndexer_ExistingString_Found()
		{
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