using System;
using System.Linq.Expressions;

namespace Simplify.Templates
{
	public class ModelSetter<T> : ModelSetterBase, IModelSetter<T>
	{
		private readonly ITemplate _template;
		private readonly T _model;

		public ModelSetter(ITemplate template, T model)
		{
			_template = template;
			_model = model;
		}

		public IModelSetter<T> Set(Expression<Func<T, object>> memberExpression, Expression<Func<T, object>> dataExpression)
		{
			return this;
		}

		public void Export()
		{
			var type = typeof(T);

			foreach (var propInfo in type.GetProperties())
			{
				_template.Set(ModelPrefix + propInfo.Name, propInfo.GetValue(_model));
			}
		}
	}
}