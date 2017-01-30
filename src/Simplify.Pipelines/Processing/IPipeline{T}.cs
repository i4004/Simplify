namespace Simplify.Pipelines.Processing
{
	public interface IPipeline<in T>
	{
		void Execute(T item);
	}
}