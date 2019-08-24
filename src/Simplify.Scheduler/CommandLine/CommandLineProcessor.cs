using System;

namespace Simplify.Scheduler.CommandLine
{
	/// <summary>
	/// Provides scheduler command line processor
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
				case CommandLineAction.SkipScheduler:
					return ProcessCommandLineResult.SkipSchedulerStart;
			}

			Console.WriteLine($"Undefined scheduler parameters: '{string.Concat(args)}'");

			return ProcessCommandLineResult.UndefinedParameters;
		}

		/// <summary>
		/// Parses the command line arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public virtual CommandLineAction ParseCommandLineArguments(string[] args)
		{
			if (args[0] == "skip")
				return CommandLineAction.SkipScheduler;

			return CommandLineAction.UndefinedAction;
		}
	}
}