namespace Simplify.Templates
{
	/// <summary>
	/// Text templates interface
	/// </summary>
	public interface ITemplate : IHideObjectMembers
	{
		/// <summary>
		/// Gets the file path of the template file.
		/// </summary>
		/// <value>
		/// The file path of the template file.
		/// </value>
		string FilePath { get; }

		/// <summary>
		/// Template current language
		/// </summary>
		string Language { get; }

		/// <summary>
		/// Template default language
		/// </summary>
		string DefaultLanguage { get; }
		
		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Set(string variableName, string value);

		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Set(string variableName, int value);

		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Set(string variableName, object value);

		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Set(string variableName, double value);

		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Set(string variableName, decimal value);

		/// <summary>
		/// Set template variable value (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Set(string variableName, long value);

		/// <summary>
		/// Set template variable value with text from template (all occurrences will be replaced)
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="template">Value to set</param>
		ITemplate Set(string variableName, ITemplate template);

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Add(string variableName, string value);

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Add(string variableName, int value);

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Add(string variableName, object value);

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Add(string variableName, double value);

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Add(string variableName, decimal value);

		/// <summary>
		/// Add value to set template variable value (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Value to set</param>
		ITemplate Add(string variableName, long value);

		/// <summary>
		/// Add value to set template variable value with text from template (all occurrences will be replaced on Get method execute) allows setting multiple values to template variable
		/// </summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="template">Value to set</param>
		ITemplate Add(string variableName, ITemplate template);

		/// <summary>
		/// Get text of the template
		/// </summary>
		string Get();

		/// <summary>
		/// Return loaded template to it's initial state
		/// </summary>
		void RollBack();

		/// <summary>
		/// Gets the text of the template and returns loaded template to it's initial state
		/// </summary>
		/// <returns>Text of the template</returns>
		string GetAndRoll();
	}
}