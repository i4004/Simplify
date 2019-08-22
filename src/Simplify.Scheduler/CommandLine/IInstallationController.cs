namespace Simplify.Scheduler.CommandLine
{
	/// <summary>
	/// Represent windows-service installation controller
	/// </summary>
	public interface IInstallationController
	{
		/// <summary>
		/// Installs the service.
		/// </summary>
		void InstallService();

		/// <summary>
		/// Uninstalls the service.
		/// </summary>
		void UninstallService();
	}
}