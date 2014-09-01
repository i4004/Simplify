using System;
using System.Linq.Expressions;

namespace Simplify.Templates
{
	public interface IModelSetter<T>
	{
		IModelSetter<T> Set(Expression<Func<T, object>> memberExpression, Expression<Func<T, object>> dataExpression);

		void Export();
	}
}