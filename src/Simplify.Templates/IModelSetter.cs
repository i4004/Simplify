using System;
using System.Linq.Expressions;

namespace Simplify.Templates
{
	/// <summary>
	/// Represent model setter to template, sets all objects properties to template
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	public interface IModelSetter<T>
		where T : class 
	{
		/// <summary>
		/// Customizes specified property data set to template, for example, you can set custom expression to convert DateTime values
		/// </summary>
		/// <typeparam name="TData">The type of the data.</typeparam>
		/// <param name="memberExpression">The member expression.</param>
		/// <param name="dataExpression">The data expression.</param>
		/// <returns></returns>
		IModelSetter<T> With<TData>(Expression<Func<T, TData>> memberExpression, Func<TData, object> dataExpression);

		/// <summary>
		/// Sets model data to template.
		/// </summary>
		void SetModel();
	}
}