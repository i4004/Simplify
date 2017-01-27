using System.Collections.Generic;

namespace Simplify.Pipelines
{
	public interface IDataPreparer<out T>
	{
		IEnumerable<T> GetData();
	}
}