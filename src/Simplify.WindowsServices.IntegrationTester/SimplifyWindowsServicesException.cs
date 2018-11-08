using System;

namespace Simplify.WindowsServices.IntegrationTests
{
	public class SimplifyWindowsServicesException : Exception
	{
		public SimplifyWindowsServicesException(string message) : base(message)
		{
		}
	}
}