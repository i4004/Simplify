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
		public void GetAssociatedValue_ExistingString_Found()
		{
			// Act
			var testString = _uow.GetAssociatedValue(TestType.Value1);

			// Assert
			Assert.AreEqual("Enum value 1", testString);
		}

		[Test]
		public void GetAssociatedValue_NoExistingString_Null()
		{
			// Act
			var testString = _uow.GetAssociatedValue(TestType.Value2);

			// Assert
			Assert.IsNull(testString);
		}

		[Test]
		public void GetKeyValuePairList_NoExistingString_Null()
		{
			// Act
			var valuesList = _uow.GetKeyValuePairList<TestType>();

			// Assert

			Assert.AreEqual(2, valuesList.Count);
			Assert.AreEqual(TestType.Value1, valuesList[0].Key);
			Assert.AreEqual("Enum value 1", valuesList[0].Value);
			Assert.AreEqual(TestType.Value2, valuesList[1].Key);
			Assert.IsNull(valuesList[1].Value);
		}
	}
}