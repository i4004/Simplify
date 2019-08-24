using Microsoft.Extensions.Configuration;
using Simplify.Scheduler.Jobs.Crontab;
using Simplify.Scheduler.Jobs.Settings.Impl;
using System;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Provides scheduler jobs factory
	/// </summary>
	public class SchedulerJobFactory : ISchedulerJobFactory
	{
		private readonly string _appName;

		/// <summary>
		/// Initializes a new instance of the <see cref="SchedulerJobFactory"/> class.
		/// </summary>
		/// <param name="appName">Name of the application.</param>
		public SchedulerJobFactory(string appName)
		{
			if (string.IsNullOrEmpty(appName)) throw new ArgumentException("Value cannot be null or empty.", nameof(appName));

			_appName = appName;
		}

		/// <summary>
		/// Creates the basic scheduler job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		public ISchedulerJob CreateJob<T>(string invokeMethodName,
			object startupArgs)
		{
			return new SchedulerJob<T>(invokeMethodName, CreateJobArgs(startupArgs));
		}

		/// <summary>
		/// Creates the crontab-based scheduler job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		public ICrontabSchedulerJob CreateCrontabJob<T>(IConfiguration configuration,
			string configurationSectionName,
			string invokeMethodName,
			object startupArgs)
		{
			return new CrontabSchedulerJob<T>(
				new ConfigurationBasedSchedulerJobSetting(configuration, FormatConfigurationSectionName<T>(configurationSectionName)),
				new CrontabProcessorFactory(),
				invokeMethodName,
				CreateJobArgs(startupArgs));
		}

		private static string FormatConfigurationSectionName<T>(string configurationSectionName)
		{
			if (configurationSectionName != null)
				return configurationSectionName;

			var type = typeof(T);
			return type.Name + "Settings";
		}

		private IJobArgs CreateJobArgs(object startupArgs)
		{
			return new JobArgs(_appName, startupArgs);
		}
	}
}