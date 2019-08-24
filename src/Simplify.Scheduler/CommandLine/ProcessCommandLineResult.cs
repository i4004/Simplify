namespace Simplify.Scheduler.CommandLine
{
	/// <summary>
	/// Result of command line process
	/// </summary>
	public enum ProcessCommandLineResult
	{
		/// <summary>
		/// The command line contains no arguments
		/// </summary>
		NoArguments,

		/// <summary>
		/// The command line contains undefined parameters
		/// </summary>
		UndefinedParameters,

		/// <summary>
		/// The command line action executed
		/// </summary>
		CommandLineActionExecuted,

		/// <summary>
		/// The skip scheduler start
		/// </summary>
		SkipSchedulerStart
	}
}