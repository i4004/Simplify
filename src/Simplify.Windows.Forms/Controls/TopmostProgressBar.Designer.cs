namespace Simplify.Windows.Forms.Controls
{
	partial class TopmostProgressBar
	{
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Progress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// Progress
			// 
			this.Progress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Progress.Location = new System.Drawing.Point(0, 0);
			this.Progress.Margin = new System.Windows.Forms.Padding(0);
			this.Progress.Name = "Progress";
			this.Progress.Size = new System.Drawing.Size(381, 19);
			this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.Progress.TabIndex = 0;
			// 
			// ProgressBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 19);
			this.ControlBox = false;
			this.Controls.Add(this.Progress);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(384, 21);
			this.MinimizeBox = false;
			this.Name = "ProgressBar";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " ";
			this.TopMost = true;
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// The ProgressBar control of the topmost progress bar
		/// </summary>
		public System.Windows.Forms.ProgressBar Progress;
	}
}
