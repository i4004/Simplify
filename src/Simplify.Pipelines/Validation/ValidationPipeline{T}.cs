using System.Collections.Generic;
using System.Linq;

namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Provides default validation pipeline
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Validation.IValidationPipeline{T, TResult}" />
	public class ValidationPipeline<T, TResult> : IValidationPipeline<T, TResult>
	{
		private readonly IList<IRule<T, TResult>> _rules;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationPipeline{T, TResult}"/> class.
		/// </summary>
		/// <param name="rules">The rules.</param>
		public ValidationPipeline(IList<IRule<T, TResult>> rules)
		{
			_rules = rules;
		}

		/// <summary>
		/// Validation the specified item through pipeline rules.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public virtual IList<TResult> Check(T item)
		{
			return _rules.Where(x => !x.Check(item)).Select(x => x.InvalidValidationResult).ToList();
		}
	}
}