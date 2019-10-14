using System;
using System.Windows.Forms;
using CefSharp;
using System.Net;

namespace xBot.App
{
	public partial class Ads : Form
	{
		/// <summary>
		/// Excel data positions.
		/// </summary>
		public enum EXCEL : byte
		{
			DATE,
			TITLE,
			URL_WEBSITE,
			URL_BANNER,
			TIME_MIN,
			TIME_MAX,
			URL_MINIBANNER
		}
		private string[] ExcelData;
		private Timer AutoClosingTimer;
		private int AutoClosingTimeMax;
		private int AutoClosingTimeMin;

		public Ads(Form w)
		{
			InitializeComponent();
			InitializeFonts(this);
			base.Icon = w.Icon;
			Text = base.ProductName + " - Advertising";
			base.Tag = base.ProductName + " | Advertising ";
			lblHeader.Text = base.Tag + "...";
		}
		private void InitializeFonts(Control c)
		{
			Fonts f = Fonts.Get;
			// Using fontName as TAG to be selected from WinForms
			c.Font = f.Load(c.Font, (string)c.Tag);
			c.Tag = null;
			for (int j = 0; j < c.Controls.Count; j++)
        InitializeFonts(c.Controls[j]);
		}
		/// <summary>
		/// Try to load the advertisement and returns the result. Returns null if cannot be loaded.
		/// </summary>
		public bool TryLoad()
		{
			ExcelData = null;
			try
			{
				string txtFile = new WebClient().DownloadString("https://docs.google.com/spreadsheets/d/1BBl5L3tLTOV-rn3GxSASXvp_HbVMr5vySeeMXSgMFvI/export?format=csv");
				string[] rows = txtFile.Split(new string[]{"\r\n"}, StringSplitOptions.None);
				string[] colums = rows[0].Split(new string[]{ "," }, StringSplitOptions.None);
				string[] str_today2 = colums[0].Split('(', ')')[1].Split('/', ' ', ':');
				DateTime today = new DateTime(int.Parse(str_today2[2]), int.Parse(str_today2[1]), int.Parse(str_today2[0]), int.Parse(str_today2[3]), int.Parse(str_today2[4]), 0);

				for (int i = 1; i < rows.Length; i++)
				{
					colums = rows[i].Split(new string[] { "," }, StringSplitOptions.None);
					str_today2 = colums[0].Split('/', ' ', ':');
					if (today <= new DateTime(int.Parse(str_today2[2]), int.Parse(str_today2[1]), int.Parse(str_today2[0]), int.Parse(str_today2[3]), int.Parse(str_today2[4]), 0)){
						ExcelData = colums;
						break;
					}
				}
				LoadMediaData();
			}
			catch
			{
				ExcelData = null;
			}
			return ExcelData != null;
		}
		private void LoadMediaData()
		{
			lblServerName.Text = GetData(EXCEL.TITLE);
			pbxBanner.Load(GetData(EXCEL.URL_BANNER));
			pbxBanner.Tag = GetData(EXCEL.URL_WEBSITE);
			ToolTips.SetToolTip(pbxBanner, GetData(EXCEL.URL_WEBSITE));
		}
		public bool isLoaded()
		{
			return ExcelData != null;
		}
		public string GetData(EXCEL type)
		{
			return ExcelData[(uint)type];
		}
		private void Ads_Load(object sender, EventArgs e)
		{
			if (isLoaded())
			{
				AutoClosingTimeMin = int.Parse(GetData(EXCEL.TIME_MIN));
				AutoClosingTimeMax = int.Parse(GetData(EXCEL.TIME_MAX));
				if (AutoClosingTimeMax < AutoClosingTimeMin || AutoClosingTimer != null)
				{
					AutoClosingTimeMin = AutoClosingTimeMax;
				}
				lblHeader.Text = base.Tag.ToString() + AutoClosingTimeMin + " ...";
				AutoClosingTimer = new Timer();
				AutoClosingTimer.Interval = 1000;
				AutoClosingTimer.Tag = 0;
				AutoClosingTimer.Tick += AutoClosingTimer_Tick;
				AutoClosingTimer.Start();
			}
			Focus();
			BringToFront();
		}
		private void Ads_Closing(object sender, FormClosingEventArgs e)
		{
			if (AutoClosingTimer != null)
			{
				AutoClosingTimer.Stop();
			}
		}
		private void AutoClosingTimer_Tick(object sender, EventArgs e)
		{
			int time = (int)AutoClosingTimer.Tag + 1;
			AutoClosingTimer.Tag = time;
			if (time < AutoClosingTimeMin)
			{
				lblHeader.Text = base.Tag.ToString() + (AutoClosingTimeMin - time) + " ...";
			}
			else if (time == AutoClosingTimeMin)
			{
				lblHeader.Text = base.Tag.ToString() + "...";
				btnWinExit.Enabled = true;
			}
			// Auto close it
			if (time >= AutoClosingTimeMax)
			{
				AutoClosingTimer.Stop();
				Close();
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
	}
}
