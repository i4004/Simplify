using Microsoft.Extensions.Configuration;
using Simplify.Scheduler.Jobs.Crontab;
using Simplify.Scheduler.Jobs.Settings.Impl;
using System;

namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Provides service jobs factory
	/// </summary>
	public class ServiceJobFactory : IServiceJobFactory
	{
		private readonly string _serviceName;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceJobFactory"/> class.
		/// </summary>
		/// <param name="serviceName">Name of the service.</param>
		public ServiceJobFactory(string serviceName)
		{
			if (string.IsNullOrEmpty(serviceName)) throw new ArgumentException("Value cannot be null or empty.", nameof(serviceName));

			_serviceName = serviceName;
		}

		/// <summary>
		/// Creates the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		public IServiceJob CreateServiceJob<T>(string invokeMethodName,
			object startupArgs)
		{
			return new ServiceJob<T>(invokeMethodName, CreateJobArgs(startupArgs));
		}

		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		public ICrontabServiceJob CreateCrontabServiceJob<T>(string configurationSectionName,
			string invokeMethodName,
			object startupArgs)
		{
			return new CrontabServiceJob<T>(
				new ConfigurationManagerBasedServiceJobSettings(FormatConfigurationSectionName<T>(configurationSectionName)),
				new CrontabProcessorFactory(),
				invokeMethodName,
				CreateJobArgs(startupArgs));
		}

		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <param name="startupArgs">The startup arguments.</param>
		/// <returns></returns>
		public ICrontabServiceJob CreateCrontabServiceJob<T>(IConfiguration configuration,
			string configurationSectionName,
			string invokeMethodName,
			object startupArgs)
		{
			return new CrontabServiceJob<T>(
				new ConfigurationBasedServiceJobSetting(configuration, FormatConfigurationSectionName<T>(configurationSectionName)),
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
			return new JobArgs(_serviceName, startupArgs);
		}
	}
}