using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Simplify.Windows.Forms.Controls
{
	/// <summary>
	/// Nullable System.Windows.Forms.DateTimePicker control
	/// </summary>
	public class NullableDateTimePicker : DateTimePicker
	{
		private bool _isNull;

		private string _nullValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="NullableDateTimePicker"/> class.
		/// </summary>
		public NullableDateTimePicker()
		{
			Format = DateTimePickerFormat.Custom;

			CustomFormat = string.Format(@" ");

			_isNull = true;
		}

		/// <summary>
		/// Gets or sets the string value that is showing in the control as <see langword="null"/> value. 
		/// </summary>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("The string used to display null values in the control")]
		[DefaultValue(" ")]
		public string NullValue
		{
			get { return _nullValue; }
			set { _nullValue = value; }
		}

		/// <summary>
		/// Nullable <see cref="DateTime"/> value of the control
		/// </summary>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("The current date/time value for this control")]
		public new DateTime? Value
		{
			get
			{
				if(_isNull)
					return null;

				return base.Value;
			}
			set
			{
				if(value == null)
					SetToNullValue();
				else
				{
					SetToDateTimeValue();
					base.Value = (DateTime)value;
				}
			}
		}

		/// <summary>
		/// Sets the <see cref="DateTimePicker"/> to the value of the <see cref="NullValue"/> property.
		/// </summary>
		private void SetToNullValue()
		{
			Format = DateTimePickerFormat.Custom;
			CustomFormat = string.IsNullOrEmpty(_nullValue) ? " " : _nullValue;
			_isNull = true;
		}

		/// <summary>
		/// Sets the <see cref="DateTimePicker"/> to a non <see langword="null"/> value.
		/// </summary>
		private void SetToDateTimeValue()
		{
			if (!_isNull) return;

			Format = DateTimePickerFormat.Long;
			_isNull = false;
		}

		/// <summary>
		/// Raises the CloseUp event.
		/// </summary>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		protected override void OnCloseUp(EventArgs e)
		{
			if(MouseButtons == MouseButtons.None && _isNull)
				SetToDateTimeValue();

			base.OnCloseUp(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
				Value = null;

			base.OnKeyUp(e);
		}
	}
}
