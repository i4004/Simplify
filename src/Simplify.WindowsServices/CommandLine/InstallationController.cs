using System.Configuration.Install;
using System.Reflection;

namespace Simplify.WindowsServices.CommandLine
{
	public class InstallationController : IInstallationController
	{
		public virtual void InstallService()
		{
			ManagedInstallerClass.InstallHelper(new[] { "/LogFile=", "/LogToConsole=true", GetEntryAssemblyLocation() });
		}

		public virtual void UninstallService()
		{
			ManagedInstallerClass.InstallHelper(new[] { "/u", "/LogFile=", "/LogToConsole=true", GetEntryAssemblyLocation() });
		}

		protected string GetEntryAssemblyLocation()
		{
			return Assembly.GetEntryAssembly().Location;
		}
	}
}