using System;

namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container provider
	/// </summary>
	public interface IDIContainerProvider : IDIRegistrator, IDIResolver, IDIContextHandler, IDisposable
	{
	}
}