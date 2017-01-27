namespace Simplify.Pipelines
{
	public interface IPipeline<in T>
	{
		void Execute(T item);
	}
}