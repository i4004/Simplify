using System;

namespace Simplify.WindowsServices.CommandLine
{
	/// <summary>
	/// Providles windows-service command line processor
	/// </summary>
	/// <seealso cref="Simplify.WindowsServices.CommandLine.ICommandLineProcessor" />
	public class CommandLineProcessor : ICommandLineProcessor
	{
		private IInstallationController _installationController;

		/// <summary>
		/// Gets or sets the current installation controller.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public IInstallationController InstallationController
		{
			get
			{
				return _installationController ?? (_installationController = new InstallationController());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_installationController = value;
			}
		}

		/// <summary>
		/// Processes the command line arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public virtual ProcessCommandLineResult ProcessCommandLineArguments(string[] args)
		{
			if (args == null || args.Length == 0)
				return ProcessCommandLineResult.NoArguments;

			var action = ParseCommandLineArguments(args);

			switch (action)
			{
				case CommandLineAction.InstallService:
					InstallationController.InstallService();
					return ProcessCommandLineResult.CommandLineActionExecuted;

				case CommandLineAction.UninstallService:
					InstallationController.UninstallService();
					return ProcessCommandLineResult.CommandLineActionExecuted;
			}

			Console.WriteLine($"Undefined service parameters: '{string.Concat(args)}'");
			Console.WriteLine("To install service use 'install' command");
			Console.WriteLine("To unistall service use 'uninstall' command");

			return ProcessCommandLineResult.UndefinedParameters;
		}

		/// <summary>
		/// Parses the command line arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public virtual CommandLineAction ParseCommandLineArguments(string[] args)
		{
			if (args[0] == "install")
				return CommandLineAction.InstallService;

			if (args[0] == "uninstall")
				return CommandLineAction.UninstallService;

			return CommandLineAction.UndefinedAction;
		}
	}
}