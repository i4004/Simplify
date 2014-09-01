using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simplify.Templates
{
	public class ModelSetter<T> : ModelSetterBase, IModelSetter<T>
		where T : class
	{
		private readonly ITemplate _template;
		private readonly T _model;
		private readonly Type _modelType;

		private readonly IList<string> _skipProperties = new List<string>();

		public ModelSetter(ITemplate template, T model)
		{
			_template = template;
			_model = model;

			_modelType = typeof(T);
		}

		public IModelSetter<T> With<TData>(Expression<Func<T, TData>> memberExpression, Func<TData, object> dataExpression)
		{
			var expression = memberExpression.Body as MemberExpression;

			if (expression == null)
				throw new ArgumentException("memberExpression type is not a MemberExpression");

			_skipProperties.Add(expression.Member.Name);

			var propInfo = _modelType.GetProperty(expression.Member.Name);
			_template.Set(ModelPrefix + propInfo.Name, dataExpression.Invoke((TData)propInfo.GetValue(_model)));

			return this;
		}

		public void Export()
		{
			var type = typeof(T);

			foreach (var propInfo in type.GetProperties())
			{
				if (_skipProperties.Contains(propInfo.Name)) continue;

				var value = _model == null ? null : propInfo.GetValue(_model);
				_template.Set(ModelPrefix + propInfo.Name, value);
			}
		}
	}
}