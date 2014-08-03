using System;
using System.Windows.Forms;

namespace Simplify.Windows.Forms
{
	/// <summary>
	/// Validates List of controls items for filled value or existing value
	/// </summary>
	public class ControlsValidator
	{
		private readonly Control[] _checkItems;
		private readonly Control _resultStatusControl;

		private bool _validationEnabled;

		/// <summary>
		/// Initialize controls validator
		/// </summary>
		/// <param name="resultStatusControl">Control which will be disabled or enabled after validation</param>
		/// <param name="checkItems">Items to validate</param>
		public ControlsValidator(Control resultStatusControl, params Control[] checkItems)
		{
			_resultStatusControl = resultStatusControl;
			_checkItems = checkItems;
		}

		private void ValidateItems()
		{
			foreach(var item in _checkItems)
			{
				var castItemComboBox = item as ComboBox;

				if(castItemComboBox != null)
				{
					if(castItemComboBox.DropDownStyle == ComboBoxStyle.DropDownList && castItemComboBox.SelectedIndex == -1)
					{
						_resultStatusControl.Enabled = false;
						return;
					}
				}
				else if(item.Text.Length == 0)
				{
					_resultStatusControl.Enabled = false;
					return;
				}
			}

			_resultStatusControl.Enabled = true;
		}

		private void OnItemCheckEvent(object sender, EventArgs e)
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
			{
				var castItemComboBox = item as ComboBox;

				// Special validation for ComboBox controls
				if(castItemComboBox != null)
				{
					if(castItemComboBox.Items.Count == 0 && castItemComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
						castItemComboBox.Enabled = false;
					else
						castItemComboBox.SelectedIndexChanged += OnItemCheckEvent;
				}
				else
					item.TextChanged += OnItemCheckEvent;
			}

			ValidateItems();
		}
	}
}
