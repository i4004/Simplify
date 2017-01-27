using System.Collections.Generic;

namespace Simplify.Pipelines
{
	public interface IValidationPipeline<in T, TResult>
	{
		IList<TResult> Check(T item);
	}
}