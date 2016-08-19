namespace Simplify.WindowsServices.CommandLine
{
	/// <summary>
	/// List of possible action from command line
	/// </summary>
	public enum CommandLineAction
	{
		/// <summary>
		/// The undefined action in command line
		/// </summary>
		UndefinedAction,

		/// <summary>
		/// The install service action from command line
		/// </summary>
		InstallService,

		/// <summary>
		/// The uninstall service action from command line
		/// </summary>
		UninstallService
	}
}