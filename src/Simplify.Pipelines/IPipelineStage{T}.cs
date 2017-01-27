namespace Simplify.Pipelines
{
	public interface IPipelineStage<in T>
	{
		bool Execute(T item);
	}
}