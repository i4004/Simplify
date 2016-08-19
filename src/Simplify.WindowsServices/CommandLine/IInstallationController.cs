namespace Simplify.WindowsServices.CommandLine
{
	public interface IInstallationController
	{
		bool InstallService();

		bool UninstallService();
	}
}