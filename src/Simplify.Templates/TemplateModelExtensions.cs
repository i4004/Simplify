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
		/// <param name="modelPrefix">The model prefix (for example 'Model', it will be used like 'Model.YourVariableName').</param>
		/// <returns></returns>
		public static IModelSetter<T> Model<T>(this ITemplate template, T model, string modelPrefix = null)
			where T : class
		{
			return new ModelSetter<T>(template, model, modelPrefix);
		}
	}
}