namespace Simplify.Pipelines.Processing
{
	public interface IPipelineStage<in T>
	{
		bool Execute(T item);
	}
}