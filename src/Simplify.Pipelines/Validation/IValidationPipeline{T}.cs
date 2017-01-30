using System.Collections.Generic;

namespace Simplify.Pipelines.Validation
{
	public interface IValidationPipeline<in T, TResult>
	{
		IList<TResult> Check(T item);
	}
}