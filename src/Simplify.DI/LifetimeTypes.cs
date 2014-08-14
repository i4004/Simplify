namespace Simplify.DI
{
	/// <summary>
	/// Represents life time types of the registered service.
	/// </summary>
	public enum LifetimeType
	{
		/// <summary>
		/// The same object will be resolved for the same scope.
		/// </summary>
		PerLifetimeScope,

		/// <summary>
		/// This object will be created only once and the same object will be returned each time it is resolved.
		/// </summary>
		Singleton,

		/// <summary>
		/// The object will be created every time it is resolved.
		/// </summary>
		Transient
	}
}