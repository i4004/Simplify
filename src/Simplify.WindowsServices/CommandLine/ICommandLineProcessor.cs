namespace Simplify.WindowsServices.CommandLine
{
	public interface ICommandLineProcessor
	{
		ProcessCommandLineResult ProcessCommandLineArguments(string[] args);
	}
}