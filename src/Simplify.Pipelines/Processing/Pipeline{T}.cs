using System.Collections.Generic;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	/// Provides default pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="Processing.IPipeline{T}" />
	public class Pipeline<T> : IPipeline<T>
	{
		private readonly IList<IPipelineStage<T>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="Pipeline{T}"/> class.
		/// </summary>
		/// <param name="stages">The pipeline stages.</param>
		public Pipeline(IList<IPipelineStage<T>> stages)
		{
			_stages = stages;
		}

		/// <summary>
		/// Process item through pipeline.
		/// </summary>
		/// <param name="item">The item.</param>
		public virtual void Execute(T item)
		{
			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var stage in _stages)
				if (!stage.Execute(item))
					return;
		}
	}
}