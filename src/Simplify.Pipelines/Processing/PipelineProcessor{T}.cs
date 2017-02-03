namespace Simplify.Pipelines.Processing
{
	/// <summary>
	///Provides default pipeline processor
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="IPipelineProcessor" />
	public class PipelineProcessor<T> : IPipelineProcessor
	{
		private readonly IPipeline<T> _pipeline;
		private readonly IDataPreparer<T> _dataPreparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="PipelineProcessor{T}"/> class.
		/// </summary>
		/// <param name="pipeline">The pipeline.</param>
		/// <param name="dataPreparer">The data preparer.</param>
		public PipelineProcessor(IPipeline<T> pipeline, IDataPreparer<T> dataPreparer)
		{
			_pipeline = pipeline;
			_dataPreparer = dataPreparer;
		}

		/// <summary>
		/// Retrieve data from preparer and process it through pipeline.
		/// </summary>
		public virtual void Execute()
		{
			var data = _dataPreparer.GetData();

			foreach (var item in data)
				_pipeline.Execute(item);
		}
	}
}