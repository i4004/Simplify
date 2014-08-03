using System.Reflection;
using NUnit.Framework;

namespace Simplify.Core.Tests
{
	[TestFixture]
	public class AssemblyInfoTests
	{
		[Test]
		public void AssemblyInfo_GetCurrentAssemblyInfo_InformationsIsCorrect()
		{
			var assemblyInfo = new AssemblyInfo(Assembly.GetAssembly((typeof(AssemblyInfoTests))));

			Assert.AreEqual("Alexander Krylkov", assemblyInfo.CompanyName);
			Assert.AreEqual("Licensed under LGPL", assemblyInfo.Copyright);
			Assert.AreEqual("Simplify.Core unit tests", assemblyInfo.Description);
			Assert.AreEqual("Simplify", assemblyInfo.ProductName);
			Assert.AreEqual("Simplify.Core.Tests", assemblyInfo.Title);
			Assert.AreEqual("1.0.0.0", assemblyInfo.Version.ToString());
		}
	}
}