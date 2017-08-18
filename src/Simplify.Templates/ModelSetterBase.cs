namespace Simplify.Templates
{
	/// <summary>
	/// Provides ModelSetter base class
	/// </summary>
	public abstract class ModelSetterBase
	{
		/// <summary>
		/// The model prefix separator
		/// </summary>
		public static string ModelPrefixSeparator = ".";

		/// <summary>
		/// The model prefix
		/// </summary>
		protected string ModelPrefix { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelSetterBase" /> class.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="modelPrefix">The model prefix.</param>
		protected ModelSetterBase(ITemplate template, string modelPrefix = null)
		{
			ModelPrefix = modelPrefix;
			Template = template;
		}

		/// <summary>
		/// Gets the template.
		/// </summary>
		/// <value>
		/// The template.
		/// </value>
		public ITemplate Template { get; }

		protected string FormatModelVariableName(string variableName)
		{
			return ModelPrefix != null ? ModelPrefix + ModelPrefixSeparator + variableName : variableName;
		}
	}
}