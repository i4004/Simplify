using System.ComponentModel;
using System.Reflection;

namespace Simplify.WindowsServices.SingleTaskExample
{
	[RunInstaller(true)]
	public class ServiceInstaller : ServiceInstallerBase
	{
		public ServiceInstaller(): base(Assembly.GetExecutingAssembly())
		{			
		}
	}
}