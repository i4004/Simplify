namespace Simplify.Pipelines.Validation
{
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