namespace Simplify.WindowsServices.CommandLine
{
	/// <summary>
	/// Represent windows-service command line processor
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