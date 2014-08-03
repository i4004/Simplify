using System.Windows.Forms;
using Simplify.Resources;

namespace Simplify.Windows.Forms
{
	/// <summary>
	/// Provides easy message box showing for windows desktop applications
	/// </summary>
	public class MessageBox
	{	
		/// <summary>
		/// Shows the message box
		/// </summary>
		/// <param name="text">Text of the message</param>
		/// <param name="icon">Icon in message box</param>
		/// <param name="buttons">Buttons in message box</param>
		public static DialogResult ShowMessageBox(string text, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			return System.Windows.Forms.MessageBox.Show(text, Application.ProductName, buttons, icon);
		}

		/// <summary>
		///Shows the message box with text from string table and selected icon
		/// </summary>
		/// <param name="stringTableRecordName">Text of the message</param>
		/// <param name="icon">Icon in message box</param>
		/// <param name="buttons">Buttons in message box</param>
		public static DialogResult ShowStMessageBox(string stringTableRecordName, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons buttons = MessageBoxButtons.OK)
		{
			return ShowMessageBox(StringTable.Entry[stringTableRecordName], icon, buttons);
		}		 
	}
}
