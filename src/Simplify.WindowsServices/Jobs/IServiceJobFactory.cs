using Simplify.WindowsServices.Jobs.Crontab;

namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Represent service jobs factory
	/// </summary>
	public interface IServiceJobFactory
	{
		/// <summary>
		/// Creates the basic service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		IServiceJob CreateServiceJob<T>(string invokeMethodName = "Run");

		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		ICrontabServiceJob CreateCrontabServiceJob<T>(string configurationSectionName = null, string invokeMethodName = "Run");
	}
}