namespace Simplify.Pipelines
{
	/// <summary>
	/// Provides chain of responsibility handler base class
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ChainHandler<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChainHandler{T}"/> class.
		/// </summary>
		/// <param name="successor">The successor handler.</param>
		protected ChainHandler(ChainHandler<T> successor = null)
		{
			Successor = successor;
		}

		/// <summary>
		/// Gets the successor handler.
		/// </summary>
		/// <value>
		/// The successor.
		/// </value>
		public ChainHandler<T> Successor { get; }

		/// <summary>
		/// Executes the handler.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public virtual void Execute(T args)
		{
			Successor?.Execute(args);
		}
	}
}