namespace Simplify.Pipelines
{
	public class PipelineProcessor<T> : IPipelineProcessor
	{
		private readonly IPipeline<T> _pipeline;
		private readonly IDataPreparer<T> _dataPreparer;

		public PipelineProcessor(IPipeline<T> pipeline, IDataPreparer<T> dataPreparer)
		{
			_pipeline = pipeline;
			_dataPreparer = dataPreparer;
		}

		public virtual void Execute()
		{
			var data = _dataPreparer.GetData();

			foreach (var item in data)
				_pipeline.Execute(item);
		}
	}
}