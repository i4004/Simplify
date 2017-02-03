using System.Collections.Generic;

namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Represent validation pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	public interface IValidationPipeline<in T, TResult>
	{
		/// <summary>
		/// Validation the specified item through pipeline rules.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		IList<TResult> Check(T item);
	}
}