using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Simplify.Templates
{
	/// <summary>
	/// Provides model setter to template, sets all objects properties to template
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	public class ModelSetter<T> : ModelSetterBase, IModelSetter<T>
		where T : class
	{
		private readonly T _model;
		private readonly Type _modelType;

		private readonly IList<string> _skipProperties = new List<string>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelSetter{T}" /> class.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="model">The model.</param>
		/// <param name="modelPrefix">The model prefix.</param>
		public ModelSetter(ITemplate template, T model, string modelPrefix = null)
			: base(template, modelPrefix)
		{
			_model = model;

			_modelType = typeof(T);
		}

		/// <summary>
		/// Customizes specified property data set to template, for example, you can set custom expression to convert DateTime values
		/// </summary>
		/// <typeparam name="TData">The type of the data.</typeparam>
		/// <param name="memberExpression">The member expression.</param>
		/// <param name="dataExpression">The data expression.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">memberExpression type is not a MemberExpression</exception>
		public IModelSetter<T> With<TData>(Expression<Func<T, TData>> memberExpression, Func<TData, object> dataExpression)
		{
			var expression = memberExpression.Body as MemberExpression;

			if (expression == null)
				throw new ArgumentException("memberExpression type is not a MemberExpression");

			if (_model == null) return this;

			_skipProperties.Add(expression.Member.Name);

			var propInfo = _modelType.GetProperty(expression.Member.Name);

			if (propInfo != null)
				Template.Set(FormatModelVariableName(propInfo.Name), dataExpression.Invoke((TData)propInfo.GetValue(_model)));

			return this;
		}

		/// <summary>
		/// Sets the specified object (model) properties into template (replace variables names like Model.MyPropertyName with respective model properties values).
		/// </summary>
		public ITemplate Set()
		{
			var type = typeof(T);

			foreach (var propInfo in type.GetProperties())
			{
				if (_skipProperties.Contains(propInfo.Name)) continue;

				var value = _model == null ? null : propInfo.GetValue(_model);
				Template.Set(FormatModelVariableName(propInfo.Name), value);
			}

			return Template;
		}

		/// <summary>
		/// Adds specified object (model) properties into template (adds to variables names like Model.MyPropertyName with respective object (model) properties values), values will be replaced on template Get or GetAndRoll methods call.
		/// </summary>
		public ITemplate Add()
		{
			var type = typeof(T);

			foreach (var propInfo in type.GetProperties())
			{
				if (_skipProperties.Contains(propInfo.Name)) continue;

				var value = _model == null ? null : propInfo.GetValue(_model);
				Template.Add(FormatModelVariableName(propInfo.Name), value);
			}

			return Template;
		}
	}
}