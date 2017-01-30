namespace Simplify.Pipelines.Validation
{
	public interface IRule<in T, out TResult>
	{
		TResult InvalidValidationResult { get; }

		bool Check(T item);

		/// <summary>
		/// Generates the action for invalid rule.
		/// </summary>
		void GenerateInvalidAction();
	}
}