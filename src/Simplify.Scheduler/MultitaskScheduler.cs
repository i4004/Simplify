using Simplify.Scheduler.CommandLine;
using System;
using System.Threading;

namespace Simplify.Scheduler
{
	/// <summary>
	/// Provides class which periodically creates a class instances specified in added jobs and launches them in separated thread, optimized to work as a console application
	/// </summary>
	public class MultitaskScheduler : SchedulerJobsHandler
	{
		private readonly AutoResetEvent _closing = new AutoResetEvent(false);

		private ICommandLineProcessor _commandLineProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="MultitaskScheduler" /> class.
		/// </summary>
		public MultitaskScheduler()
		{
			Console.CancelKeyPress += StopJobs;
		}

		/// <summary>
		/// Gets or sets the current command line processor.
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		public ICommandLineProcessor CommandLineProcessor
		{
			get => _commandLineProcessor ?? (_commandLineProcessor = new CommandLineProcessor());
			set => _commandLineProcessor = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Starts the scheduler
		/// </summary>
		/// <param name="args">The arguments.</param>
		public bool Start(string[] args = null)
		{
			var commandLineProcessResult = CommandLineProcessor.ProcessCommandLineArguments(args);

			switch (commandLineProcessResult)
			{
				case ProcessCommandLineResult.SkipSchedulerStart:
					return false;

				case ProcessCommandLineResult.NoArguments:
					StartAndWait();
					break;
			}

			return true;
		}

		/// <summary>
		/// Called when scheduler is about to stop, main stopping point
		/// </summary>
		protected void StopJobs(object sender, ConsoleCancelEventArgs args)
		{
			StopJobs();

			args.Cancel = true;
			_closing.Set();
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		// ReSharper disable once FlagArgument
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			_closing.Dispose();
		}

		private void StartAndWait()
		{
			StartJobs();

			Console.WriteLine("Scheduler started. Press Ctrl + C to shut down.");

			_closing.WaitOne();
		}
	}
}