using System.Collections.Generic;

namespace Simplify.Pipelines.Validation
{
	public interface IValidationPipelineProcessor<T, TResult>
	{
		IDictionary<T, IList<TResult>> Check();
	}
}