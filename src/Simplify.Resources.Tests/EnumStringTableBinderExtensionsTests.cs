using NUnit.Framework;

namespace Simplify.Resources.Tests
{
	[TestFixture]
	public class EnumStringTableBinderExtensionsTests
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
			var testString = _uow.GetAssociatedValue(TestType.Value1);

			// Assert
			Assert.AreEqual("Enum value 1", testString);
		}

		[Test]
		public void ResourcesStringTableIndexer_NoExistingString_Null()
		{
			// Act
			var testString = _uow.GetAssociatedValue(TestType.Value2);

			// Assert
			Assert.IsNull(testString);
		}
	}
}