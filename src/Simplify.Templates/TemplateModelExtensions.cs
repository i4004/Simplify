namespace Simplify.Templates
{
	/// <summary>
	/// Template model extensions
	/// </summary>
	public static class TemplateModelExtensions
	{
		/// <summary>
		/// Selects the object (model) to get a properties values from and set them to template.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="template">The template.</param>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static IModelSetter<T> Model<T>(this ITemplate template, T model)
			where T : class
		{
			return new ModelSetter<T>(template, model);
		}
	}
}