namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Provides base validation rule
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <seealso cref="Validation.IRule{T, TResult}" />
	public abstract class Rule<T, TResult> : IRule<T, TResult>
	{
		/// <summary>
		/// Gets the validation result in case of rule is invalid.
		/// </summary>
		/// <value>
		/// The invalid validation result.
		/// </value>
		public abstract TResult InvalidValidationResult { get; }

		/// <summary>
		/// Validates the specified item through this rule.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public abstract bool Check(T item);

		/// <summary>
		/// Generates the action for invalid rule.
		/// </summary>
		public virtual void GenerateInvalidAction()
		{
		}
	}
}