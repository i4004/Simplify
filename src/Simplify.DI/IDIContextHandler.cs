namespace Simplify.DI
{
	/// <summary>
	/// Represents DI container scoped context handler
	/// </summary>
	public interface IDIContextHandler
	{
		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		ILifetimeScope BeginLifetimeScope();
	}
}