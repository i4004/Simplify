namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Represent pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPipeline<in T>
	{
		/// <summary>
		/// Process item through pipeline.
		/// </summary>
		/// <param name="item">The item.</param>
		void Execute(T item);
	}
}