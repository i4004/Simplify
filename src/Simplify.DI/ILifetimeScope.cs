using System;

namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container provider lifetime scope
	/// </summary>
	public interface ILifetimeScope : IDisposable
	{
		/// <summary>
		/// Gets the DI container resolver (should be used to resolve types when using scoping).
		/// </summary>
		/// <value>
		/// The DI container resolver (should be used to resolve types when using scoping).
		/// </value>
		IDIResolver Resolver { get; }
	}
}