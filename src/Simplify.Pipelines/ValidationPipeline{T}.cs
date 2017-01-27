using System.Collections.Generic;
using System.Linq;

namespace Simplify.Pipelines
{
	public class ValidationPipeline<T, TResult> : IValidationPipeline<T, TResult>
	{
		private readonly IList<IRule<T, TResult>> _rules;

		public ValidationPipeline(IList<IRule<T, TResult>> rules)
		{
			_rules = rules;
		}

		public virtual IList<TResult> Check(T item)
		{
			return _rules.Where(x => !x.IsValid(item)).Select(x => x.InvalidValidationResult).ToList();
		}
	}
}