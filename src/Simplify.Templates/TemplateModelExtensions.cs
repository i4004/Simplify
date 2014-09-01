namespace Simplify.Templates
{
	public static class TemplateModelExtensions
	{
		/// <summary>
		/// Sets the specified model into template (replace variables names like Model.MyPropertyName with respective model values).
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="template">The template.</param>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static IModelSetter<T> Set<T>(this ITemplate template, T model)
			where T : class
		{
			return new ModelSetter<T>(template, model);
		}
	}
}