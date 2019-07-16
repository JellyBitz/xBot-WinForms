using System;
using System.Windows.Forms;

namespace xBot
{
	public partial class About : Form
	{
		public About(Form w)
		{
			InitializeComponent();
			InitializeFonts(this);
			Icon = w.Icon;
			lblHeader.Text = w.ProductName + " v" + w.ProductVersion +@" | Easy & Flexible! Design perfection.";
    }
		private void InitializeFonts(Control c)
		{
			for (int i = 0; i < c.Controls.Count; i++)
			{
				// Using fontName as TAG to be selected from WinForms
				c.Controls[i].Font = Fonts.Get.Load(c.Controls[i].Font, (string)c.Controls[i].Tag);
				InitializeFonts(c.Controls[i]);
				c.Controls[i].Tag = null;
			}
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
					// Nothing atm.
					System.Diagnostics.Process.Start("http://www.google.com");
					break;
			}
		}
	}
}
