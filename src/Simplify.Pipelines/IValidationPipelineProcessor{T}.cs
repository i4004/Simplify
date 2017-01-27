using System.Collections.Generic;

namespace Simplify.Pipelines
{
	public interface IValidationPipelineProcessor<T, TResult>
	{
		IDictionary<T, IList<TResult>> Check();
	}
}