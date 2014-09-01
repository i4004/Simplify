using System;
using System.Linq.Expressions;

namespace Simplify.Templates
{
	public interface IModelSetter<T>
		where T : class 
	{
		IModelSetter<T> With<TData>(Expression<Func<T, TData>> memberExpression, Func<TData, object> dataExpression);

		void Export();
	}
}