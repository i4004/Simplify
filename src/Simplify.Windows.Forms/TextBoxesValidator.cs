using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Simplify.Resources;

namespace Simplify.Windows.Forms
{
	/// <summary>
	/// Validates List of <see cref="TextBox"/> items for filled value
	/// </summary>
	public class TextBoxesValidator
	{
		private static IResourcesStringTable LocalStringTableInstance;

		private readonly TextBox[] _checkItems;
		private readonly Control _resultStatusControl;
		private readonly bool _colorHighlight;

		private bool _validationEnabled;

		/// <summary>
		/// ApplicationHelper string table
		/// </summary>
		internal static IResourcesStringTable LocalStringTable
		{
			get { return LocalStringTableInstance ?? (LocalStringTableInstance = new ResourcesStringTable(true, "LocalResources")); }
		}
		
		/// <summary>
		/// Text-box numeric value validation event
		/// </summary>
		public static void OnTextBoxNumberValidated<T>(object sender, CancelEventArgs e)
		{
			var textBox = sender as TextBox;

			if (textBox == null || textBox.Text == "") return;

			var type = typeof(T);

			if (type == typeof(int))
			{
				int result;

				if (!int.TryParse(textBox.Text, out result))
				{
					MessageBox.ShowMessageBox(LocalStringTable["ErrorTextBoxIntegerValidation"]);
					e.Cancel = true;
				}

			}

			if (type == typeof(long))
			{
				long result;

				if (!long.TryParse(textBox.Text, out result))
				{
					MessageBox.ShowMessageBox(LocalStringTable["ErrorTextBoxLongValidation"]);
					e.Cancel = true;
				}
			}

			if (type == typeof(double))
			{
				double result;

				if (!double.TryParse(textBox.Text, out result))
				{
					MessageBox.ShowMessageBox(LocalStringTable["ErrorTextBoxDoubleValidation"]);
					e.Cancel = true;
				}
			}
		}

		/// <summary>
		/// Initialize controls validator
		/// </summary>
		/// <param name="resultStatusControl">Control which will be disabled or enabled after validation</param>
		/// <param name="checkItems">Items to validate</param>
		public TextBoxesValidator(Control resultStatusControl, params TextBox[] checkItems)
		{
			_resultStatusControl = resultStatusControl;
			_checkItems = checkItems;
		}

		/// <summary>
		/// Initialize controls validator
		/// </summary>
		/// <param name="enableColorHightlight">Enabled text boxes color highlight in case of invalid control</param>
		/// <param name="resultStatusControl">Control which will be disabled or enabled after validation</param>
		/// <param name="checkItems">Items to validate</param>
		public TextBoxesValidator(bool enableColorHightlight, Control resultStatusControl, params TextBox[] checkItems)
		{
			_colorHighlight = enableColorHightlight;
			_resultStatusControl = resultStatusControl;
			_checkItems = checkItems;
		}

		private void ValidateItems()
		{
			var nonValidControls = false;

			foreach(var item in _checkItems)
			{
				if(item.Text.Length == 0)
				{
					if(_colorHighlight)
						item.BackColor = Color.FromArgb(255, 210, 210);

					nonValidControls = true;
				}
				else if(_colorHighlight && item.BackColor != SystemColors.Window)
					item.BackColor = SystemColors.Window;
			}

			_resultStatusControl.Enabled = !nonValidControls;
		}

		private void OnCheckItemTextChanged(object sender, EventArgs e)
		{
			if(_validationEnabled)
				ValidateItems();
		}

		/// <summary>
		/// Elable items validation
		/// </summary>
		public void EnableValidation()
		{
			_validationEnabled = true;

			foreach(var item in _checkItems)
				item.TextChanged += OnCheckItemTextChanged;

			ValidateItems();
		}

		/// <summary>
		/// Disable items validation
		/// </summary>
		public void DisableValidation()
		{
			_validationEnabled = false;
		}
	}
}
