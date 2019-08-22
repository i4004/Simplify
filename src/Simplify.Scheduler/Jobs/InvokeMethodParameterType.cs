namespace Simplify.WindowsServices.Jobs
{
	/// <summary>
	/// Provides a job invoke method parameter type
	/// </summary>
	public enum InvokeMethodParameterType
	{
		/// <summary>
		/// The parameterless invoke method
		/// </summary>
		Parameterless,

		/// <summary>
		/// The invoke method with service name parameter
		/// </summary>
		ServiceName,

		/// <summary>
		/// The invoke method with job args parameter
		/// </summary>
		Args
	}
}