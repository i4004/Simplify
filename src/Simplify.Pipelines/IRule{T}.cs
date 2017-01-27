namespace Simplify.Pipelines
{
	public interface IRule<in T, out TResult>
	{
		TResult InvalidValidationResult { get; }

		bool IsValid(T item);
	}
}