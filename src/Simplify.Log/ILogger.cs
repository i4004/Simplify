using System;

namespace Simplify.Log
{
	/// <summary>
	/// Logger class interface
	/// </summary>
	public interface ILogger : IHideObjectMembers
	{
		/// <summary>
		/// Write text message to log file
		/// </summary>
		/// <param name="message">Text message</param>
		/// <returns>Text written to log file (contain time information etc.)</returns>
		string Write(string message);

		/// <summary>
		/// Write data of an exception to log file
		/// </summary>
		/// <param name="e">Exception to get data from</param>
		/// <returns>Text written to log file (contain time information etc.)</returns>
		string Write(Exception e);

		/// <summary>
		/// Write data of an exception to log file and returs written data formatted with HTML line breaks
		/// </summary>
		/// <param name="e">Exception to get data from</param>
		/// <returns>Text written to log file (contain time information etc.) formatted with HTML line breaks</returns>
		string WriteWeb(Exception e);
	}
}