namespace Simplify.Scheduler.CommandLine
{
	/// <summary>
	/// Represent scheduler command line processor
	/// </summary>
	public interface ICommandLineProcessor
	{
		/// <summary>
		/// Processes the command line arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		ProcessCommandLineResult ProcessCommandLineArguments(string[] args);
	}
}