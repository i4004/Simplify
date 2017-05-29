using System.Collections.Generic;

namespace Simplify.Pipelines.Processing
{
	/// <summary>
	///Provides default resulting pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Simplify.Pipelines.Processing.IResultingPipeline{T, TResult}" />
	public class ResultingPipeline<T, TResult> : IResultingPipeline<T, TResult>
	{
		private readonly IList<IResultingPipelineStage<T, TResult>> _stages;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultingPipeline{T, TResult}"/> class.
		/// </summary>
		/// <param name="stages">The stages.</param>
		public ResultingPipeline(IList<IResultingPipelineStage<T, TResult>> stages)
		{
			_stages = stages;
		}

		/// <summary>
		/// Process pipeline stages.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public bool Execute(T item)
		{
			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var stage in _stages)
				if (!stage.Execute(item))
				{
					ErrorResult = stage.ErrorResult;
					return false;
				}

			return true;
		}

		/// <summary>
		/// Gets the error result.
		/// </summary>
		/// <value>
		/// The error result.
		/// </value>
		public TResult ErrorResult { get; private set; }
	}
}