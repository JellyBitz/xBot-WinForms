using System;
using System.Windows.Forms;
using CefSharp;
using System.Net;

namespace xBot
{
	public partial class Ads : Form
	{
		/// <summary>
		/// Excel data positions.
		/// </summary>
		public const byte
			DATE = 0,
			TITLE = 1,
	    URL_WEBSITE = 2,
			URL_BANNER = 3,
			TIME_MIN = 4,
			TIME_MAX = 5,
			URL_MINIBANNER = 6;
		private Timer AutoClosingTimer;
		private int AutoClosingTimeMax;
		private int AutoClosingTimeMin; // Keep it as smooth ads
		public Ads(Form w)
		{
			InitializeComponent();
			InitializeFonts(this);
			Icon = w.Icon;
			Text = w.ProductName + " - Advertising";
			Tag = w.ProductName + " | Advertising ";
			lblHeader.Text = Tag + "...";
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
		/// <summary>
		/// Try to load the advertisement and returns the result. Returns null if cannot be loaded.
		/// </summary>
		public string[] LoadAds()
		{
			string[] data = GetAdvertisementData();
			if (data != null)
			{
				AutoClosingTimer = new Timer();
				AutoClosingTimer.Interval = 1000;
				AutoClosingTimer.Tag = 0;
				AutoClosingTimer.Tick += new EventHandler(this.AutoClosingTimer_Tick);
				AutoClosingTimeMin = int.Parse(data[TIME_MIN]);
				AutoClosingTimeMax = int.Parse(data[TIME_MAX]);
        if (AutoClosingTimeMax < AutoClosingTimeMin)
					AutoClosingTimeMin = AutoClosingTimeMax;
				btnWinExit.Enabled = false;
				this.lblHeader.Text = this.lblHeader.Text.Substring(0, this.lblHeader.Text.Length - 3) + AutoClosingTimeMin + " ...";
				lblServerName.Text = data[TITLE];
				pbxBanner.Load(data[URL_BANNER]);
				pbxBanner.Tag = data[URL_WEBSITE];
			}
			return data;
		}
		private string[] GetAdvertisementData()
		{
			try
			{
				string txtFile = (new WebClient()).DownloadString("https://docs.google.com/spreadsheets/d/1BBl5L3tLTOV-rn3GxSASXvp_HbVMr5vySeeMXSgMFvI/export?format=csv");
				// Trying to read CSV format
				string[] rows = txtFile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
				string[] colums = rows[0].Split(new string[] { "," }, StringSplitOptions.None);
				// 0 = Date limit; 1 = Server name
				string[] str_today = colums[DATE].Split('[', ']')[1].Split('/');
				DateTime today = new DateTime(int.Parse(str_today[2]), int.Parse(str_today[1]), int.Parse(str_today[0]));
				for (int j = 1; j < rows.Length; j++)
				{
					colums = rows[j].Split(new string[] { "," }, StringSplitOptions.None);
					str_today = colums[DATE].Split('/');
					if (today <= (new DateTime(int.Parse(str_today[2]), int.Parse(str_today[1]), int.Parse(str_today[0]))))
					{
						return colums;
					}
				}
			}
			catch {
				// Format error or website not loaded.
			}
			return null;
		}
		private void Ads_Closing(object sender, FormClosingEventArgs e)
		{
			if (AutoClosingTimer != null)
			{
				int time = (int)AutoClosingTimer.Tag;
				if (time < AutoClosingTimeMin)
				{
					e.Cancel = true;
				}
				else
				{
					AutoClosingTimer.Stop();
				}
      }
		}
		private void AutoClosingTimer_Tick(object sender, EventArgs e)
		{
			int time = (int)AutoClosingTimer.Tag + 1;
			AutoClosingTimer.Tag = time;

			if (time < AutoClosingTimeMin)
			{
				this.lblHeader.Text = this.Tag.ToString() + (AutoClosingTimeMin - time) + " ...";
			}
			else if (time == AutoClosingTimeMin)
			{
				this.lblHeader.Text = this.Tag.ToString() + "...";
				this.btnWinExit.Enabled = true;
			}
			// Auto close
			if (time >= AutoClosingTimeMax)
			{
				AutoClosingTimer.Stop();
				this.Close();
			}
		}
		private void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "btnWinExit":
					this.Close();
					break;
				case "pbxBanner":
					System.Diagnostics.Process.Start(pbxBanner.Tag.ToString());
					break;
			}
		}

		private void Ads_Load(object sender, EventArgs e)
		{
			if (AutoClosingTimer != null)
			{
				AutoClosingTimer.Start();
			}
			WinAPI.SetForegroundWindow(Handle);
		}
	}
}
