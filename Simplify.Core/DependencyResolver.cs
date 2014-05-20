using System;

namespace Simplify.Core
{
	/// <summary>
	/// Dependency resolver
	/// </summary>
	public class DependencyResolver
	{
		private static Lazy<IDependecyResolver> _dependencyResolver = new Lazy<IDependecyResolver>(() => new DefaultDependencyResolver());

		/// <summary>
		/// Gets or sets the dependency resolver fro container factory.
		/// </summary>
		/// <value>
		/// The dependency resolver.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IDependecyResolver Current
		{
			get { return _dependencyResolver.Value; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_dependencyResolver = new Lazy<IDependecyResolver>(() => value);
			}
		}		 
	}
}