namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Represent pipeline stage with processing error result information
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Simplify.Pipelines.Processing.IPipelineStage{T}" />
	public interface IResultingPipelineStage<in T, out TResult> : IPipelineStage<T>
	{
		/// <summary>
		/// Gets the error result.
		/// </summary>
		/// <value>
		/// The error result.
		/// </value>
		TResult ErrorResult { get; }
	}
}