using System;
using Simplify.DI.Provider.DryIoc;

namespace Simplify.DI
{
	/// <summary>
	/// IOC ambient context container to register/resolve objects for DI
	/// </summary>
	public class DIContainer
	{
		private static IDIContainerProvider _current;

		/// <summary>
		/// The IOC container
		/// </summary>
		public static IDIContainerProvider Current
		{
			get
			{
				return _current ?? (_current = new DryIocDIProvider());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_current = value;
			}
		}
	}
}