using System.ComponentModel;
using System.Reflection;
using Simplify.WindowsServices;

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