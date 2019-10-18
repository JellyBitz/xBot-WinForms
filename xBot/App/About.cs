using System;
using System.Windows.Forms;

namespace xBot.App
{
	public partial class About : Form
	{
		public About(Form w)
		{
			InitializeComponent();
			InitializeFonts(this);
			Icon = w.Icon;
			Text = w.ProductName + " - About";
			lblHeader.Text = w.ProductName + " v" + w.ProductVersion +@" | Easy & Flexible! Design perfection.";
    }
		private void InitializeFonts(Control c)
		{
			// Using fontName as TAG to be selected from WinForms
			c.Font = Fonts.GetFont(c.Font, (string)c.Tag);
			c.Tag = null;
			for (int j = 0; j < c.Controls.Count; j++)
        InitializeFonts(c.Controls[j]);
		}
		#region (GUI Design)
		private void Window_Drag_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WinAPI.ReleaseCapture();
				WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
			}
		}
		#endregion

		private void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Font.Strikeout){
				// Control is disabled
				return;
			}
			switch (c.Name)
			{
				case "btnWinExit":
					this.Close();
					break;
				case "btnSupport":
					System.Diagnostics.Process.Start("https://www.buymeacoffee.com/JellyBitz");
					break;
			}
		}
	}
}
