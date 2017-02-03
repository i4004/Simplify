using System.Collections.Generic;

namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Represent validation pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	public interface IValidationPipelineProcessor<T, TResult>
	{
		/// <summary>
		/// Executes the validation pipeline.
		/// </summary>
		/// <returns></returns>
		IDictionary<T, IList<TResult>> Check();
	}
}