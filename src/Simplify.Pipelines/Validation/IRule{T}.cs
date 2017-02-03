namespace Simplify.Pipelines.Validation
{
	/// <summary>
	/// Represent validation rule
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	public interface IRule<in T, out TResult>
	{
		/// <summary>
		/// Gets the result representing invalid result of rule validation.
		/// </summary>
		/// <value>
		/// The invalid validation result.
		/// </value>
		TResult InvalidValidationResult { get; }

		/// <summary>
		/// Validates the specified item through this rule.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		bool Check(T item);

		/// <summary>
		/// Perfrom some action in case of invalid rule status.
		/// </summary>
		void GenerateInvalidAction();
	}
}