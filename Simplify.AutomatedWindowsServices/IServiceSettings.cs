using System;
using System.Collections.Generic;

namespace Simplify.AutomatedWindowsServices
{
	/// <summary>
	/// Represent windows-service processing settings
	/// </summary>
	public interface IServiceSettings
	{
		/// <summary>
		/// Gets the service processing interval (sec).
		/// </summary>
		/// <value>
		/// The service processing interval (sec).
		/// </value>
		int ProcessingInterval { get; }

		/// <summary>
		/// Gets the service working points.
		/// </summary>
		/// <value>
		/// The service working points.
		/// </value>
		IList<DateTime> WorkingPoints { get; }
	}
}