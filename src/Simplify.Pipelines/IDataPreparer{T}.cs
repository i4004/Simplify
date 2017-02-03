using System.Collections.Generic;

namespace Simplify.Pipelines
{
	/// <summary>
	/// Represent data preparer (retriever) for pipeline processing
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDataPreparer<out T>
	{
		/// <summary>
		/// Gets the data for pipeline processing.
		/// </summary>
		/// <returns></returns>
		IEnumerable<T> GetData();
	}
}