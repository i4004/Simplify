using System.ComponentModel;
using System.Reflection;

namespace Simplify.WindowsServices.MultitaskExample
{
	[RunInstaller(true)]
	public class ServiceInstaller : ServiceInstallerBase
	{
		public ServiceInstaller(): base(Assembly.GetExecutingAssembly())
		{			
		}
	}
}