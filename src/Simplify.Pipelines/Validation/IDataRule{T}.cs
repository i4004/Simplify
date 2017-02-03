namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Represent validation rule which can retrieve and hold data used by other rules
	/// </summary>
	/// <typeparam name="TData">The type of the data.</typeparam>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Simplify.Pipelines.Validation.IRule{T, TResult}" />
	public interface IDataRule<out TData, in T, out TResult> : IRule<T, TResult>
	{
		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		TData Data { get; }
	}
}