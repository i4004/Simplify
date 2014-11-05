namespace Simplify.WindowsServices
{
	/// <summary>
	/// Provides service job factory
	/// </summary>
	public class ServiceJobFactory : IServiceJobFactory
	{
		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		public IServiceJob CreateServiceJob<T>(string configurationSectionName = null, string invokeMethodName = "Run")
		{
			if (configurationSectionName == null)
			{
				var type = typeof(T);
				configurationSectionName = type.Name + "Settings";
			}

			return new ServiceJob<T>(new ServiceJobSettings(configurationSectionName), new CrontabProcessorFactory(), invokeMethodName);
		}
	}
}