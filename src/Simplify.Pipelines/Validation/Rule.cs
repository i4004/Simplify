namespace Simplify.Pipelines.Validation
{
	public abstract class Rule<T, TResult> : IRule<T, TResult>
	{
		/// <summary>
		/// Gets the validation result in case of rule is invalid.
		/// </summary>
		/// <value>
		/// The invalid validation result.
		/// </value>
		public abstract TResult InvalidValidationResult { get; }

		public abstract bool Check(T item);

		/// <summary>
		/// Generates the action for invalid rule.
		/// </summary>
		public virtual void GenerateInvalidAction()
		{
		}
	}
}