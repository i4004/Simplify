namespace Simplify.Templates
{
	/// <summary>
	/// Provides ModelSetter base class
	/// </summary>
	public abstract class ModelSetterBase
	{
		/// <summary>
		/// The model prefix
		/// </summary>
		public static string ModelPrefix = "Model.";

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelSetterBase"/> class.
		/// </summary>
		/// <param name="template">The template.</param>
		protected ModelSetterBase(ITemplate template)
		{
			Template = template;
		}

		/// <summary>
		/// Gets the template.
		/// </summary>
		/// <value>
		/// The template.
		/// </value>
		public ITemplate Template { get; private set; }
	}
}