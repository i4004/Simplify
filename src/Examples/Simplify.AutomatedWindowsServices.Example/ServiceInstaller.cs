using System.ComponentModel;
using System.Reflection;

namespace Simplify.AutomatedWindowsServices.Example
{
	[RunInstaller(true)]
	public class ServiceInstaller : ServiceInstallerBase
	{
		public ServiceInstaller(): base(Assembly.GetExecutingAssembly())
		{			
		}
	}
}