using System;

namespace Simplify.Scheduler.CommandLine
{
	/// <summary>
	/// Provides windows-service command line processor
	/// </summary>
	/// <seealso cref="ICommandLineProcessor" />
	public class CommandLineProcessor : ICommandLineProcessor
	{
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
				case CommandLineAction.RunAsConsole:
					return ProcessCommandLineResult.SkipServiceStart;
			}

			Console.WriteLine($"Undefined service parameters: '{string.Concat(args)}'");
			Console.WriteLine("To install service use 'install' command");
			Console.WriteLine("To uninstall service use 'uninstall' command");

			return ProcessCommandLineResult.UndefinedParameters;
		}

		/// <summary>
		/// Parses the command line arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public virtual CommandLineAction ParseCommandLineArguments(string[] args)
		{
			if (args[0] == "console")
				return CommandLineAction.RunAsConsole;

			return CommandLineAction.UndefinedAction;
		}
	}
}