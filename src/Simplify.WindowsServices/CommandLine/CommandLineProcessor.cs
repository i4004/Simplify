namespace Simplify.WindowsServices.CommandLine
{
	public class CommandLineProcessor : ICommandLineProcessor
	{
		public ProcessCommandLineResult ProcessCommandLineArguments(string[] args)
		{
			if (args == null || args.Length == 0)
				return ProcessCommandLineResult.NoArguments;

			return ProcessCommandLineResult.NoArguments;
		}
	}
}