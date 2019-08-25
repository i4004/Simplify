namespace Simplify.Scheduler.Jobs
{
	/// <summary>
	/// Provides job invoke method parameter type
	/// </summary>
	public enum InvokeMethodParameterType
	{
		/// <summary>
		/// The parameterless invoke method
		/// </summary>
		Parameterless,

		/// <summary>
		/// The invoke method with application name parameter
		/// </summary>
		AppName,

		/// <summary>
		/// The invoke method with job args parameter
		/// </summary>
		Args
	}
}