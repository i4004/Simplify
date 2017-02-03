using System.Collections.Generic;
using System.Linq;

namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Provides default validation pipeline processor
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Validation.IValidationPipelineProcessor{T, TResult}" />
	public class ValidationPipelineProcessor<T, TResult> : IValidationPipelineProcessor<T, TResult>
	{
		private readonly IValidationPipeline<T, TResult> _pipeline;
		private readonly IDataPreparer<T> _dataPreparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationPipelineProcessor{T, TResult}"/> class.
		/// </summary>
		/// <param name="pipeline">The pipeline.</param>
		/// <param name="dataPreparer">The data preparer.</param>
		public ValidationPipelineProcessor(IValidationPipeline<T, TResult> pipeline, IDataPreparer<T> dataPreparer)
		{
			_pipeline = pipeline;
			_dataPreparer = dataPreparer;
		}

		/// <summary>
		/// Executes the validation pipeline.
		/// </summary>
		/// <returns></returns>
		public virtual IDictionary<T, IList<TResult>> Check()
		{
			var data = _dataPreparer.GetData();

			return data.ToDictionary(item => item, item => _pipeline.Check(item));
		}
	}
}