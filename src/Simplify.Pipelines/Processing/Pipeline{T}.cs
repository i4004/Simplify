using System.Collections.Generic;

namespace Simplify.Pipelines.Processing
{
	public class Pipeline<T> : IPipeline<T>
	{
		private readonly IList<IPipelineStage<T>> _stages;

		public Pipeline(IList<IPipelineStage<T>> stages)
		{
			_stages = stages;
		}

		public virtual void Execute(T item)
		{
			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var stage in _stages)
				if (!stage.Execute(item))
					return;
		}
	}
}