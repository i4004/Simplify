namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Provides validation rule which can retrieve and hold data used by other rules
	/// </summary>
	/// <typeparam name="TData">The type of the data.</typeparam>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Validation.Rule{T, TResult}" />
	/// <seealso cref="Validation.IDataRule{TData, T, TResult}" />
	public abstract class DataRule<TData, T, TResult> : Rule<T, TResult>, IDataRule<TData, T, TResult>
	{
		/// <summary>
		/// Gets the rule data.
		/// </summary>
		/// <value>
		/// The rule data.
		/// </value>
		public abstract TData Data { get; }
	}
}