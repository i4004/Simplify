namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Represent pipeline with processing error result informatioo
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	public interface IResultingPipeline<in T, out TResult>
	{
		/// <summary>
		/// Process args through pipeline.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		bool Execute(T args);

		/// <summary>
		/// Gets the error result.
		/// </summary>
		/// <value>
		/// The error result.
		/// </value>
		TResult ErrorResult { get; }
	}
}