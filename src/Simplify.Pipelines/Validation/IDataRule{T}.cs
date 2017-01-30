namespace Simplify.Pipelines.Validation
{
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