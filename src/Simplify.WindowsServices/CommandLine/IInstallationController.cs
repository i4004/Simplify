namespace Simplify.WindowsServices.CommandLine
{
	public interface IInstallationController
	{
		void InstallService();

		void UninstallService();
	}
}