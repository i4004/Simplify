using System.Collections.Generic;
using System.Linq;

namespace Simplify.Pipelines.Validation
{
	public class ValidationPipelineProcessor<T, TResult> : IValidationPipelineProcessor<T, TResult>
	{
		private readonly IValidationPipeline<T, TResult> _pipeline;
		private readonly IDataPreparer<T> _dataPreparer;

		public ValidationPipelineProcessor(IValidationPipeline<T, TResult> pipeline, IDataPreparer<T> dataPreparer)
		{
			_pipeline = pipeline;
			_dataPreparer = dataPreparer;
		}

		public virtual IDictionary<T, IList<TResult>> Check()
		{
			var data = _dataPreparer.GetData();

			return data.ToDictionary(item => item, item => _pipeline.Check(item));
		}
	}
}