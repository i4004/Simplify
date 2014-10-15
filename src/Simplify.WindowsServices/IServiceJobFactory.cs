namespace Simplify.WindowsServices
{
	/// <summary>
	/// Represent service job factory
	/// </summary>
	public interface IServiceJobFactory
	{
		/// <summary>
		/// Creates the service job.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configurationSectionName">Name of the configuration section.</param>
		/// <param name="invokeMethodName">Name of the invoke method.</param>
		/// <returns></returns>
		IServiceJob CreateServiceJob<T>(string configurationSectionName = null, string invokeMethodName = "Run");
	}
}