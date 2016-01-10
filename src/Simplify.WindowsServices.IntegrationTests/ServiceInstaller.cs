using System.ComponentModel;
using System.Reflection;

namespace Simplify.WindowsServices.IntegrationTests
{
	[RunInstaller(true)]
	public class ServiceInstaller : ServiceInstallerBase
	{
		public ServiceInstaller(): base(Assembly.GetExecutingAssembly())
		{			
		}
	}
}