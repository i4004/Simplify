using System;

namespace Simplify.WindowsServices.CommandLine
{
	public class CommandLineProcessor : ICommandLineProcessor
	{
		private IInstallationController _installationController;

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

			return ProcessCommandLineResult.UndefinedParameters;
		}

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