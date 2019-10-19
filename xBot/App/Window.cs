using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using CefSharp;
using CefSharp.WinForms;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using xBot.Game;
using xBot.App.PK2Extractor;
using xBot.Game.Objects;
using SecurityAPI;
using xBot.Network;
using xGraphics;

namespace xBot.App
{
	/// <summary>
	/// Main bot window GUI that handle all bot events.
	/// </summary>
	public partial class Window : Form
	{
		/// <summary>
		/// Unique instance of this class.
		/// </summary>
		private static Window _this = null;
		/// <summary>
		/// Used to show interactive extern minimap.
		/// </summary>
		private ChromiumWebBrowser Minimap_wbrChromeMap = null;
		/// <summary>
		/// Advertising window.
		/// </summary>
		private Ads adsWindow;
		private Thread tAdsWindow;
		private Window()
		{
			InitializeComponent();
			InitializeFonts(this);
			InitializeValues();
		}
		/// <summary>
		/// GetInstance. Secures an unique class creation for being used anywhere at the project.
		/// </summary>
		public static Window Get
		{
			get
			{
				if (_this == null)
					_this = new Window();
				return _this;
			}
		}
		/// <summary>
		/// Initialize fonts natives or not to the controls specified.
		/// </summary>
		private void InitializeFonts(Control c)
		{
			// Using fontName as TAG to be selected from WinForms
			c.Font = Fonts.GetFont(c.Font, (string)c.Tag);
			c.Tag = null;
			for (int j = 0; j < c.Controls.Count; j++)
				InitializeFonts(c.Controls[j]);
		}
		/// <summary>
		/// Initialize all default values required by the GUI.
		/// </summary>
		private void InitializeValues()
		{
			// Window Texts
			SetTitle();
			this.lblHeaderText01.Text = this.ProductName + " -";
			this.lblHeaderText02.Location = new Point(this.lblHeaderText01.Location.X + this.lblHeaderText01.Size.Width, this.lblHeaderText01.Location.Y);
			this.lblHeaderText02.Text = "v" + this.ProductVersion;
			adsWindow = new Ads(this);

			// Vertical tabs
			// Login
			TabPageV_Option_Click(this.TabPageV_Control01_Option01, null);
			// Horizontal tabs
			TabPageH_Option_Click(this.TabPageH_Character_Option01, null);

			TabPageH_Option_Click(this.TabPageH_Inventory_Option01, null);

			TabPageH_Option_Click(this.TabPageH_Party_Option01, null);

			TabPageH_Option_Click(this.TabPageH_Skills_Option01, null);
			Skills_cmbxAttackMobType.SelectedIndex = 0;

			TabPageH_Option_Click(this.TabPageH_Training_Option01, null);

			TabPageH_Option_Click(this.TabPageH_Chat_Option01, null);
			Chat_cmbxMsgType.SelectedIndex = 0;

			TabPageH_Option_Click(this.TabPageH_Settings_Option01, null);
			Settings_cmbxHWIDClientSaveFrom.SelectedIndex =
			Settings_cmbxHWIDServerSendTo.SelectedIndex =
			Settings_cmbxCreateCharRace.SelectedIndex =
			Settings_cmbxCreateCharGenre.SelectedIndex =
			Settings_cmbxInjectTo.SelectedIndex = 0;
		}
		/// <summary>
		/// Load command arguments to the App.
		/// </summary>
		private void LoadCommandLine()
		{
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				string cmd = args[i].ToLower();
				// Check data
				if (cmd.StartsWith("-silkroad="))
				{
					Login_cmbxSilkroad.Text = args[i].Substring(10);
				}
				else if (cmd.StartsWith("-username="))
				{
					Login_tbxUsername.Text = args[i].Substring(10);
				}
				else if (cmd.StartsWith("-password="))
				{
					Login_tbxPassword.Text = args[i].Substring(10);
				}
				else if (cmd.StartsWith("-captcha="))
				{
					Login_tbxCaptcha.Text = args[i].Substring(9);
				}
				else if (cmd.StartsWith("-server="))
				{
					// Saving to Tag because cannot be set as text yet
					Login_cmbxServer.Tag = args[i].Substring(8);
				}
				else if (cmd.StartsWith("-character="))
				{
					// Saving to Tag because cannot be set as text yet
					Login_cmbxCharacter.Tag = args[i].Substring(11);
				}
				else if (cmd.Equals("--clientless"))
				{
					Login_rbnClientless.Checked = true;
				}
				else if (cmd.Equals("--goclientless"))
				{
					Login_cbxGoClientless.Checked = true;
				}
				else if (cmd.Equals("--relogin"))
				{
					Login_cbxRelogin.Checked = true;
				}
				else if (cmd.Equals("--usereturn"))
				{
					Login_cbxUseReturnScroll.Checked = true;
				}
			}
			// Check if minimum neccesary is correct to start auto login
			if (Login_cmbxSilkroad.Text != ""
				&& Login_tbxUsername.Text != ""
				&& Login_tbxPassword.Text != "" && Login_cmbxServer.Tag != null)
			{
				Bot.Get.hasAutoLoginMode = true;
				Control_Click(Login_btnStart, null);
			}
		}
		private void ShowAds()
		{
			tAdsWindow = new Thread((ThreadStart)delegate{
				try
				{
					if (!adsWindow.isLoaded() && adsWindow.TryLoad())
					{
						// Load the banner in background
						WinAPI.InvokeIfRequired(Login_pbxAds, () => {
							Login_pbxAds.LoadAsync(adsWindow.GetData(Ads.EXCEL.URL_MINIBANNER));
							ToolTips.SetToolTip(Login_pbxAds, adsWindow.GetData(Ads.EXCEL.TITLE));
						});
					}
					if (adsWindow.isLoaded())
					{
						// Show Advertising
						WinAPI.InvokeIfRequired(this, () => {
							adsWindow.ShowDialog(this);
						});
					}
				}
				catch { /*Window closed or something else..*/ }
			});
			tAdsWindow.Start();
		}
		public void LogProcess(string text = "Ready", ProcessState state = ProcessState.Default)
		{
			WinAPI.InvokeIfRequired(lblBotState, () => {
				lblBotState.Text = text;
				switch (state)
				{
					case ProcessState.Warning:
						SetProcessColor(Color.FromArgb(202, 81, 0));
						break;
					case ProcessState.Disconnected:
						SetProcessColor(Color.FromArgb(104, 33, 122));
						break;
					case ProcessState.Error:
						SetProcessColor(Color.Red);
						break;
					default:
						SetProcessColor(Color.FromArgb(0, 122, 204));
						break;
				}
			});
		}
		private void SetProcessColor(Color newColor)
		{
			if (newColor == BackColor)
				return;
			lblBotState.BackColor = newColor;
			WinAPI.InvokeIfRequired(this, () => {
				BackColor = newColor;
			});
		}
		public void Log(string text)
		{
			try
			{
				WinAPI.InvokeIfRequired(rtbxLogs, () => {
					rtbxLogs.AppendText(Environment.NewLine + WinAPI.GetDate() + " " + text);
				});
			}
			catch { }
		}
		public void SetTitle()
		{
			WinAPI.InvokeIfRequired(this, () =>
			{
				this.Text = this.ProductName;
				this.lblHeaderText02.Text = "v" + this.ProductVersion;
				this.NotifyIcon.Text = this.ProductName + " v" + this.ProductVersion + "\nMade by JellyBitz";
			});
		}
		public void SetTitle(string server, string charname,Process client = null)
		{
			string wTitle = this.ProductName + " - [" + server + "] " + charname;
			WinAPI.InvokeIfRequired(this, () => {
				this.Text = wTitle;
				this.lblHeaderText02.Text = charname;
				this.NotifyIcon.Text = this.ProductName+" v"+this.ProductVersion+"\n[" + server + "] " + charname;
			});
			if (client != null)
				WinAPI.SetWindowText(client.MainWindowHandle, wTitle);
		}
		public void LogMessageFilter(string message)
		{
			try
			{
				WinAPI.InvokeIfRequired(Character_rtbxMessageFilter, () => {
					Character_rtbxMessageFilter.AppendText(WinAPI.GetDate() + " " + message + Environment.NewLine);
				});
			}
			catch { }
		}
		public void LogPacket(string text)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				if (Menu_rtbxPackets_AddTimestamp.Checked)
					sb.Append(WinAPI.GetDate());
				sb.AppendLine(text);

				WinAPI.InvokeIfRequired(Settings_rtbxPackets, ()=>{
					Settings_rtbxPackets.AppendText(sb.ToString());
				});
			}
			catch
			{
			}
		}
		public void LogChatMessage(RichTextBox chat, string player, string message)
		{
			try
			{
				WinAPI.InvokeIfRequired(chat, ()=> {
					chat.AppendText(WinAPI.GetDate() + " " + player + ": " + message + Environment.NewLine);
				});
			}
			catch
			{
			}
		}
		private bool isValidFilename(string FileName)
		{
			try
			{
				Path.GetFileName(FileName);
			}
			catch
			{
				return false;
			}
			return true;
		}
		public void AddBuff(SRObject Buff)
		{
			ListViewItem item = new ListViewItem();
			item.ToolTipText = Buff.Name;
			item.Name = Buff.ID.ToString();
			item.ImageKey = GetImageKeyIcon((string)Buff[SRProperty.Icon]);
			WinAPI.InvokeIfRequired(Character_lstvBuffs, () =>{
				Character_lstvBuffs.Items.Add(item);
			});
		}
    public void RemoveBuff(uint SkillID)
		{
			WinAPI.InvokeIfRequired(Character_lstvBuffs, () => {
				Character_lstvBuffs.Items.RemoveByKey(SkillID.ToString());
			});
		}
		public void ClearBuffs()
		{
			WinAPI.InvokeIfRequired(Character_lstvBuffs, () => {
				Character_lstvBuffs.Items.Clear();
			});
		}
		/// <summary>
		/// Add an skill (learned) to the skill list
		/// </summary>
		public void AddSkill(SRObject Skill)
		{
			ListViewItem item = new ListViewItem(Skill.Name);
			item.Name = Skill.ID.ToString();
			// Keep a whole reference, easier skill checks
			item.Tag = Skill;
      item.ImageKey = GetImageKeyIcon((string)Skill[SRProperty.Icon]);
			WinAPI.InvokeIfRequired(Skills_lstvSkills, () => {
				Skills_lstvSkills.Items.Add(item);
			});
		}
		public void UpdateSkillNames(uint oldSkillID, uint newSkillID)
		{
			ListViewItem temp;
			string key = oldSkillID.ToString();
			string newkey = newSkillID.ToString();
			// Not necessary update the TAG, it's already updated (by reference)
			if ((temp = this.Skills_lstvSkills.Items[key]) != null) temp.Name = newkey;
			// An array of references cannot be possible.. Using the long way "copy & paste" code :(
			if ((temp = this.Skills_lstvAttackMobType_General.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_Champion.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_Giant.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_PartyGeneral.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_PartyChampion.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_PartyGiant.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_Unique.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_Elite.Items[key]) != null) temp.Name = newkey;
			if ((temp = this.Skills_lstvAttackMobType_Event.Items[key]) != null) temp.Name = newkey;
		}
		public void RemoveSkill(uint SkillID)
		{
			WinAPI.InvokeIfRequired(Skills_lstvSkills, () => {
				Skills_lstvSkills.Items.RemoveByKey(SkillID.ToString());
			});
		}
		public void ClearSkills()
		{
			WinAPI.InvokeIfRequired(Skills_lstvSkills, () => {
				Skills_lstvSkills.Items.Clear();
			});
		}
		public string GetImageKeyIcon(string Pk2Path)
		{
			// Check if image is not loaded
			if (!lstimgIcons.Images.ContainsKey(Pk2Path))
			{
				// Check if the file exists
				string FullPath = Pk2Extractor.GetDirectory(Info.Get.Silkroad) + "icon\\" + Pk2Path;
				FullPath = Path.ChangeExtension(FullPath, "png");
				if (File.Exists(FullPath))
				{
					lstimgIcons.Images.Add(Pk2Path, Image.FromFile(FullPath));
				}
				else
				{
					// Try to load the default image
					FullPath = Pk2Extractor.GetDirectory(Info.Get.Silkroad) + "icon\\icon_default.png";
					if (File.Exists(FullPath))
					{
						lstimgIcons.Images.Add(Pk2Path, Image.FromFile(FullPath));
					}
					else
					{
						return "";
					}
				}
			}
			return Pk2Path;
		}
		/// <summary>
		/// Fix the text using a pattern restriction. Returns empty if the pattern is not found.
		/// </summary>
		private string FixTextRestriction(string Text, string Pattern, bool FirstMatch = false)
		{
			MatchCollection matches = Regex.Matches(Text, Pattern);
			if (matches.Count > 0)
			{
				if (FirstMatch)
					return matches[0].Value;
				StringBuilder fixedText = new StringBuilder();
				foreach (Match m in matches)
					fixedText.Append(m.Value);
				return fixedText.ToString();
			}
			return "";
		}
		
		public void BringToTop() {
			if (this.WindowState == FormWindowState.Minimized){
				this.WindowState = FormWindowState.Normal;
			}
			this.Activate();
		}
		
		
		/// <summary>
		/// Set the gold in Silkroad format color.
		/// </summary>
		public void Character_SetGold(ulong gold)
		{
			// 1000000 to 1.000.000
			string Text = gold.ToString("#,0");
			int GoldDigits = gold.ToString().Length;
			// Visual color
			Color ForeColor = Color.White; // Default
			if(GoldDigits <= 4){
				ForeColor = Color.White;
			}
			else if (GoldDigits <= 5){
				ForeColor = Color.FromArgb(255, 250, 133); // Light Yellow
			}
			else if (GoldDigits <= 6)
			{
				ForeColor = Color.FromArgb(255, 211, 72); // Yellow
			}
			else if (GoldDigits <= 7)
			{
				ForeColor = Color.FromArgb(255, 173, 92); // Dark Orange
			}
			else if (GoldDigits <= 8)
			{
				ForeColor = Color.FromArgb(255, 154, 161); // Pink
			}
			else if (GoldDigits <= 9)
			{
				ForeColor = Color.FromArgb(235, 161, 255); // Purple
			}
			WinAPI.InvokeIfRequired(Character_lblGold, () => {
				Character_lblGold.ForeColor = ForeColor;
				Character_lblGold.Text = Text;
			});
		}
		public void Inventory_Refresh()
		{
			WinAPI.InvokeIfRequired(Inventory_lstvItems, () => {
				Inventory_lstvItems.Items.Clear();
				Inventory_lstvItems.BeginUpdate();
			});

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
			
			for (int j = 0; j < inventory.Capacity; j++)
			{
				ListViewItem item = new ListViewItem();
				item.Name = item.Text = j.ToString();
				if (inventory[j] != null)
				{
					item.SubItems.Add(inventory[j].Name + (inventory[j].Contains(SRProperty.Plus) ? " (+" + (byte)inventory[j][SRProperty.Plus] + ")" : ""));
					item.SubItems.Add((ushort)inventory[j][SRProperty.QuantityMax] == 1 ? "1" : inventory[j][SRProperty.Quantity] + "/" + inventory[j][SRProperty.QuantityMax]);
					item.SubItems.Add(inventory[j].ServerName);
					item.ImageKey = GetImageKeyIcon((string)inventory[j][SRProperty.Icon]);
					// TO DO:
					// Add as tooltip the item stats
				}
				else
				{
					item.SubItems.Add("Empty");
				}

				// Add
				WinAPI.InvokeIfRequired(Inventory_lstvItems, () => {
					Inventory_lstvItems.Items.Add(item);
				});
			}
			WinAPI.InvokeIfRequired(Inventory_lblCapacity, () => {
				Inventory_lblCapacity.Text = "Capacity : " + inventory.Count + "/" + inventory.Capacity;
			});
			WinAPI.InvokeIfRequired(Inventory_lstvItems, () => {
				Inventory_lstvItems.EndUpdate();
			});
		}
		public void Party_Clear()
		{
			WinAPI.InvokeIfRequired(Party_lstvPartyMembers, () => {
				Party_lstvPartyMembers.Items.Clear();
			});
			WinAPI.InvokeIfRequired(Party_lblCurrentSetup, () => {
				Party_lblCurrentSetup.Text = "";
				Party_lblCurrentSetup.Tag = null;
			});
			WinAPI.InvokeIfRequired(this, () => {
				ToolTips.SetToolTip(Party_lblCurrentSetup, "");
			});
		}
		/// <summary>
		/// Load WebBrowser diplaying the Silkroad world map.
		/// </summary>
		public void Minimap_Load()
		{
			string path = Directory.GetCurrentDirectory() + "\\Minimap\\index.html";
			if (Minimap_wbrChromeMap == null && File.Exists(path))
			{
				// Initialize cef with the provided settings
				CefSettings settings = new CefSettings();
				Cef.Initialize(settings);
				// Empty URL because DockStyle.Fill can (maybe) slow down the responsive process.
				Minimap_wbrChromeMap = new ChromiumWebBrowser("");
				Minimap_wbrChromeMap.Dock = DockStyle.Fill;
				Minimap_wbrChromeMap.Load(path);
				Minimap_panelMap.Controls.Add(Minimap_wbrChromeMap);
			}
		}
		public void Minimap_ObjectPointer_Clear()
		{
			if (Minimap_wbrChromeMap != null)
			{
				WebBrowserExtensions.ExecuteScriptAsyncWhenPageLoaded(Minimap_wbrChromeMap, "SilkroadMap.RemoveAllExtraPointers();", true);
			}
		}

		public void Minimap_CharacterPointer_Move(SRCoord position)
		{
			if (Minimap_wbrChromeMap != null)
			{
				WebBrowserExtensions.ExecuteScriptAsyncWhenPageLoaded(Minimap_wbrChromeMap, "SilkroadMap.MovePointer(" + position.Region + "," + position.X + "," + position.Y + "," + position.Z + ");", true);
			}
		}

		public void Minimap_ObjectPointer_Add(uint UniqueID, string servername, string htmlPopup, SRCoord position)
		{
			if (Minimap_wbrChromeMap != null)
			{
				WebBrowserExtensions.ExecuteScriptAsyncWhenPageLoaded(Minimap_wbrChromeMap, "SilkroadMap.AddExtraPointer('" + UniqueID + "','" + servername + "','" + htmlPopup + "'," + position.Region + "," + position.X + "," + position.Y + "," + position.Z + ");", true);
			}
		}
		public void Minimap_ObjectPointer_Move(uint UniqueID, SRCoord position)
		{
			if (Minimap_wbrChromeMap != null)
			{
				WebBrowserExtensions.ExecuteScriptAsyncWhenPageLoaded(Minimap_wbrChromeMap, "SilkroadMap.MoveExtraPointer('" + UniqueID + "'," + position.Region + "," + position.X + "," + position.Y + "," + position.Z + ");", true);
			}
		}
		public void Minimap_ObjectPointer_Remove(uint UniqueID)
		{
			if (Minimap_wbrChromeMap != null)
			{
				WebBrowserExtensions.ExecuteScriptAsyncWhenPageLoaded(Minimap_wbrChromeMap, "SilkroadMap.RemoveExtraPointer('" + UniqueID + "');", true);
			}
		}

		#region (GUI theme design behaviour)
		/// <summary>
		/// Set the control to be used as window drag.
		/// </summary>
		private void Window_Drag_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WinAPI.ReleaseCapture();
				WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
			}
		}
		/// <summary>
		/// Color the label associated (by name) to the current control focused.
		/// </summary>
		private void Control_Focus_Enter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv", "btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if (c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = Color.FromArgb(30, 150, 220);
					}
					break;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="Control_Focus_Enter(object, EventArgs)"/>
		/// </summary>
		private void Control_Focus_Leave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv", "btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if (c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = c.Parent.BackColor;
					}
					break;
				}
			}
		}
		/// <summary>
		/// Colors used on TabPage Vertical.
		/// </summary>
		private Color TabPageV_ColorHover = Color.FromArgb(74, 74, 76),
			TabPageV_ColorSelected = Color.FromArgb(0, 122, 204);
		/// <summary>
		///  TabPage Vertical option click.
		/// </summary>
		private void TabPageV_Option_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			List<Control> currentOption;
			if (c.Parent.Tag != null)
			{
				currentOption = (List<Control>)c.Parent.Tag;
				if (currentOption[0].Name == c.Name || currentOption[1].Name == c.Name)
					return;
				currentOption[0].BackColor = c.Parent.BackColor;
				currentOption[1].BackColor = c.Parent.BackColor;
				c.Parent.Parent.Controls[currentOption[0].Name + "_Panel"].Visible = false;
			}
			currentOption = new List<Control>();
			currentOption.Add(c.Parent.Controls[c.Name.Replace("_Icon", "")]);
			currentOption.Add(c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"]);
			c.Parent.Tag = currentOption;
			currentOption[0].BackColor = TabPageV_ColorSelected;
			currentOption[1].BackColor = TabPageV_ColorSelected;
			c.Parent.Parent.Controls[currentOption[1].Name.Replace("Icon", "Panel")].Visible = true;
		}
		/// <summary>
		/// Creates a custom color focus on icon and option used on TabPage Vertical.
		/// <para>Results are not as expected because Windows native focus interference...</para>
		/// </summary>
		private void TabPageV_Option_MouseEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorHover;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"].BackColor = TabPageV_ColorHover;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="TabPageV_Option_MouseEnter(object, EventArgs)"/>
		/// </summary>
		private void TabPageV_Option_MouseLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = c.Parent.BackColor;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"].BackColor = c.Parent.BackColor;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		/// <summary>
		///  TabPage Horizontal option click.
		/// </summary>
		private void TabPageH_Option_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				Control currentOption = (Control)c.Parent.Tag;
				if (currentOption.Name == c.Name)
					return;
				currentOption.BackColor = c.Parent.Parent.BackColor;
				c.Parent.Parent.Controls[currentOption.Name + "_Panel"].Visible = false;
			}
			c.Parent.Tag = c;
			c.BackColor = c.Parent.BackColor;
			c.Parent.Parent.Controls[c.Name + "_Panel"].Visible = true;
		}
		/// <summary>
		/// Modified TabPage Horizontal behavior for better chat UX.
		/// </summary>
		private void TabPageH_ChatOption_Click(object sender, EventArgs e)
		{
			TabPageH_Option_Click(sender, e);
			Control option = (Control)sender;
			if (option.Font.Bold)
				option.Font = new Font(option.Font, FontStyle.Regular);
			Chat_cmbxMsgType.Text = option.Text; // If is not into the options, then will not change.
		}
		/// <summary>
		/// Activate notify on chat.
		/// </summary>
		public void TabPageH_ChatOption_Notify(Control c)
		{
			WinAPI.InvokeIfRequired(c.Parent, () => {
				if (c.Parent.Tag != c && !c.Font.Bold)
					c.Font = new Font(c.Font, FontStyle.Bold);
			});
		}
		/// <summary>
		/// Forces the listview header to keep his width.
		/// </summary>
		private void ListView_ColumnWidthChanging_Cancel(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
		}
		public void EnableControl(Control c, bool active)
		{
			WinAPI.InvokeIfRequired(c, () => {
				c.Font = new Font(c.Font, (active ? FontStyle.Regular : FontStyle.Strikeout));
			});
		}
		#endregion

		#region (GUI events generated)
		/// <summary>
		/// Load all components (not visuals) to the App. Like settings and stuffs.
		/// </summary>
		private void Window_Load(object sender, EventArgs e)
		{
			// Welcome
			rtbxLogs.AppendText(string.Format("{0} Welcome to {1} v{2} | Made by Engels \"JellyBitz\" Quintero{3}{0} Discord : JellyBitz#7643 | FaceBook : @ImJellyBitz", WinAPI.GetDate(), base.ProductName, base.ProductVersion, Environment.NewLine));
			LogProcess();
			// Load basic
			Settings.LoadBotSettings();
			LoadCommandLine();
			// Try to load adverstising
			ShowAds();
			// Force visible
			Activate();
			BringToFront();
		}
		/// <summary>
		/// Close all necessary to not leaving any background process.
		/// </summary>
		private void Window_Closing(object sender, FormClosingEventArgs e)
		{
			if (Bot.Get.Proxy != null)
			{
				Bot.Get.Proxy.Stop();
			}
			if(tAdsWindow.ThreadState == System.Threading.ThreadState.Running)
			{
				tAdsWindow.Abort();
			}
			if (Minimap_wbrChromeMap != null)
			{
				Cef.Shutdown();
			}
		}
		/// <summary>
		/// Control OnClick event.
		/// </summary>
		public void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			// Check if control is disabled
			if (c.Font.Strikeout)
				return;
			// Check his name
			switch (c.Name)
			{
				case "btnWinMinimize":
					this.WindowState = FormWindowState.Minimized;
					break;
				case "btnWinRestore":
					if (this.WindowState != FormWindowState.Normal)
						this.WindowState = FormWindowState.Normal;
					this.WindowState = FormWindowState.Maximized;
					break;
				case "btnWinExit":
					Application.Exit();
					break;
				case "btnBotStart":
					if (c.ForeColor == Color.Red)
					{
						c.ForeColor = Color.Lime;
						ToolTips.SetToolTip(c, "Stop Bot");
					}
					else
					{
						c.ForeColor = Color.Red;
						ToolTips.SetToolTip(c, "Start Bot");
					}
					break;
				case "btnAnalyzer":
					TabPageV_Option_Click(TabPageV_Control01_Option14, null);
					TabPageH_Option_Click(TabPageH_Settings_Option04, null);
					break;
				case "Login_btnAddSilkroad":
					TabPageV_Option_Click(TabPageV_Control01_Option14, null);
					TabPageH_Option_Click(TabPageH_Settings_Option01, null);
					break;
				case "Login_btnStart":
					switch (c.Text)
					{
						case "START":
							if (Login_cmbxSilkroad.Text == "")
								return;
							Info i = Info.Get;
							// Check if database has been generated previously
							if (!i.SelectDatabase(Login_cmbxSilkroad.Text))
							{
								MessageBox.Show(this, "The database \"" + Settings_tbxSilkroadName.Text + "\" needs to be created.", "xBot", MessageBoxButtons.OK);
								TabPageV_Option_Click(TabPageV_Control01_Option14, null);
								TabPageH_Option_Click(TabPageH_Settings_Option01, null);
								return;
							}

							TreeNode silkroad = Settings_lstrSilkroads.Nodes[i.Silkroad];

							// SR_Client Path check
							if (!Login_rbnClientless.Checked)
							{
								if (!silkroad.Nodes.ContainsKey("ClientPath"))
								{
									MessageBox.Show(this, "You need to select the SRO_Client path first.", "xBot", MessageBoxButtons.OK);
									TabPageV_Option_Click(TabPageV_Control01_Option14, null);
									TabPageH_Option_Click(TabPageH_Settings_Option01, null);
									return;
								}
								Info.Get.ClientPath = (string)silkroad.Nodes["ClientPath"].Tag;
							}

							// Add possibles Gateways/Ports
							List<string> hosts = new List<string>();
							foreach (TreeNode host in silkroad.Nodes["Hosts"].Nodes)
								hosts.Add(host.Text);
							List<ushort> ports = new List<ushort>();
							ports.Add((ushort)silkroad.Nodes["Port"].Tag);
							if (hosts.Count == 0 || ports.Count == 0)
								return; // Just in case

							i.Locale = byte.Parse(silkroad.Nodes["Locale"].Tag.ToString());
							i.SR_Client = "SR_Client";
							i.Version = uint.Parse(silkroad.Nodes["Version"].Tag.ToString());

							// Lock Silkroad selection
							EnableControl(Login_btnLauncher, false);
							EnableControl(Settings_btnAddSilkroad, false);
							Login_cmbxSilkroad.Enabled = false;

							// HWID Setup
							Bot b = Bot.Get;
							ushort clientOp = (ushort)silkroad.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag;
							ushort serverOp = (ushort)silkroad.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag;
							string saveFrom = (string)silkroad.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
							string sendTo = (string)silkroad.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
							bool sendOnlyOnce = (bool)silkroad.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
							b.SetHWID(clientOp, saveFrom, serverOp, sendTo, sendOnlyOnce);

							// Creating Proxy
							b.Proxy = new Proxy(Login_rbnClientless.Checked, hosts, ports);
							b.Proxy.RandomHost = (bool)silkroad.Nodes["RandomHost"].Tag;
							b.Proxy.Start();
							break;
						case "STOP":
							Bot.Get.Proxy.Stop();
							break;
						case "LOGIN":
							if (Login_tbxUsername.Text == "" || Login_tbxPassword.Text == "" || Login_cmbxServer.Text == "")
								return;
							Info.Get.Server = Login_cmbxServer.Text;
							EnableControl(c, false);
							LogProcess("Login...");
							Bot.Get.LoggedFromBot = true;
							PacketBuilder.Login(Login_tbxUsername.Text, Login_tbxPassword.Text, Convert.ToUInt16(Info.Get.ServerID));
							break;
						case "SELECT":
							if (Login_cmbxCharacter.Text == "")
								return;
							Info.Get.Charname = Login_cmbxCharacter.Text;
							EnableControl(c, false);
							PacketBuilder.SelectCharacter(Login_cmbxCharacter.Text);
							break;
					}
					break;
				case "Login_btnLauncher":
					if (Login_cmbxSilkroad.Text != "")
					{
						TreeNode silkroad = Settings_lstrSilkroads.Nodes[Login_cmbxSilkroad.Text];
						if (silkroad.Nodes.ContainsKey("LauncherPath"))
						{
							Process.Start((string)silkroad.Nodes["LauncherPath"].Tag);
						}
						else
						{
							MessageBox.Show(this, "You need to select the Launcher path first.", "xBot", MessageBoxButtons.OK);
							TabPageV_Option_Click(TabPageV_Control01_Option14, null);
							TabPageH_Option_Click(TabPageH_Settings_Option01, null);
						}
					}
					break;
				case "Login_pbxAds":
					ShowAds();
					break;
				case "Character_pgbHP":
				case "Character_pgbMP":
					xProgressBar xProgressBar = (xProgressBar)c;
					if (xProgressBar.Display == xProgressBarDisplay.Percentage)
					{
						xProgressBar.Display = xProgressBarDisplay.Values;
					}
					else
					{
						xProgressBar.Display = xProgressBarDisplay.Percentage;
					}
					xProgressBar.Invalidate();
					break;
				case "Character_btnAddINT":
					if (Bot.Get.inGame)
					{
						PacketBuilder.AddStatPointINT();
					}
					break;
				case "Character_btnAddSTR":
					if (Bot.Get.inGame)
					{
						PacketBuilder.AddStatPointSTR();
					}
					break;
				case "Inventory_btnRefresh":
					if (Bot.Get.inGame)
						this.Inventory_Refresh();
					else
						this.Inventory_lstvItems.Items.Clear();
					break;
				case "Party_btnAddPlayer":
					if (Bot.Get.inGame)
					{
						// Check if already exists
						if (Party_tbxPlayer.Text != "" && !Party_lstvPartyList.Items.ContainsKey(Party_tbxPlayer.Text.ToLower()))
						{
							ListViewItem player = new ListViewItem(Party_tbxPlayer.Text);
							player.Name = player.Text.ToLower();
							Party_lstvPartyList.Items.Add(player);

							Party_tbxPlayer.Text = "";
							Settings.SaveCharacterSettings();
							Bot.Get.CheckAutoParty();
						}
					}
					break;
				case "Party_btnAddLeader":
					if (Bot.Get.inGame)
					{
						if (Party_tbxLeader.Text != "" && !Party_lstvLeaderList.Items.ContainsKey(Party_tbxLeader.Text.ToLower()))
						{
							ListViewItem leader = new ListViewItem(Party_tbxLeader.Text);
							leader.Name = leader.Text.ToLower();
							Party_lstvLeaderList.Items.Add(leader);

							Party_tbxLeader.Text = "";
							Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Party_btnRefreshMatch":
					if (Bot.Get.inGame)
					{
						PacketBuilder.RequestPartyMatch();
					}
					break;
				case "Party_btnJoinMatch":
					if (Party_tbxJoinToNumber.Text != "")
					{
						if (Bot.Get.inGame && !Bot.Get.inParty)
						{
							PacketBuilder.JoinToPartyMatch(uint.Parse(Party_tbxJoinToNumber.Text));
						}
					}
					break;
				case "Party_btnLastPage":
					Party_btnLastPage.Enabled = false;
					if (Bot.Get.inGame)
					{
						PacketBuilder.RequestPartyMatch((byte)(byte.Parse(Party_lblPageNumber.Text) - 2));
					}
					break;
				case "Party_btnNextPage":
					Party_btnNextPage.Enabled = false;
					if (Bot.Get.inGame)
					{
						PacketBuilder.RequestPartyMatch(byte.Parse(Party_lblPageNumber.Text));
					}
					break;
				case "Training_btnGetCoordinates":
					if (Bot.Get.inGame && Training_lstvAreas.SelectedItems.Count == 1)
					{
						SRCoord Position = Info.Get.Character.GetPosition();
						Training_tbxRegion.Text = Position.Region.ToString();
						Training_tbxX.Text = Position.X.ToString();
						Training_tbxY.Text = Position.Y.ToString();
						Training_tbxZ.Text = Position.Z.ToString();

						Training_lstvAreas.SelectedItems[0].SubItems[1].Text = Training_tbxRegion.Text;
						Training_lstvAreas.SelectedItems[0].SubItems[2].Text = Training_tbxX.Text;
						Training_lstvAreas.SelectedItems[0].SubItems[3].Text = Training_tbxY.Text;
						Training_lstvAreas.SelectedItems[0].SubItems[4].Text = Training_tbxZ.Text;

						Settings.SaveCharacterSettings();
					}
					break;
				case "Training_btnLoadScriptPath":
					if (Bot.Get.inGame && Training_lstvAreas.SelectedItems.Count == 1)
					{
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your Script file";
							fileDialog.FileName = "Media.pk2";
							fileDialog.Filter = "Script files (*.xcript)|*.xcript|All files (*.*)|*.*";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								Training_tbxScriptPath.Text = fileDialog.FileName;
								Training_lstvAreas.SelectedItems[0].SubItems[6].Text = fileDialog.FileName;
								Settings.SaveCharacterSettings();
							}
						}
					}
					break;
				case "Training_btnTraceStart":
					if (c.Text == "START")
					{
						Bot.Get.StartTrace(Training_cmbxTracePlayer.Text);
					}
					else
					{
						Bot.Get.StopTrace();
					}
					break;
				case "Minimap_btnLoadMap":
					// Try to load WebBrowser map
					Minimap_Load();
					if (Minimap_wbrChromeMap != null)
					{
						// Map loaded
						Minimap_btnLoadMap.Parent.Controls.Remove(Minimap_btnLoadMap);
						if (Bot.Get.inGame)
						{
							Minimap_CharacterPointer_Move(Info.Get.Character.GetPosition());
						}
					}
					break;
				case "GameInfo_btnRefresh":
					GameInfo_lstrObjects.Nodes.Clear();
					if (Bot.Get.inGame)
					{
						Info i = Info.Get;
						// Making a copy because the list could be edited while iterating
						List<SRObject> objects = new List<SRObject>(i.EntityList.Values);
						// Pause drawing, possibly long data
						GameInfo_lstrObjects.BeginUpdate();
						// Add character always
						GameInfo_lstrObjects.Nodes.Add(i.Character.Clone().ToNode());
						// Filter and add entities
						foreach (SRObject obj in objects)
						{
							if (obj.isPlayer())
							{
								if (this.GameInfo_cbxPlayer.Checked)
									GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
							}
							else if (obj.isPet())
							{
								if (this.GameInfo_cbxPet.Checked)
									GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
							}
							else if (obj.isMob())
							{
								if (this.GameInfo_cbxMob.Checked)
									GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
							}
							else if (obj.isNPC())
							{
								if (this.GameInfo_cbxNPC.Checked)
									GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
							}
							else if (obj.isItem())
							{
								if (this.GameInfo_cbxDrop.Checked)
									GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
							}
							else
							{
								if (this.GameInfo_cbxOthers.Checked)
									GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
							}
						}
						GameInfo_lstrObjects.EndUpdate();
						GameInfo_tbxServerTime.Text = i.GetServerTime();
					}
					break;
				case "Settings_btnPK2Path":

					if (!Pk2Extractor.DirectoryExists(Settings_tbxSilkroadName.Text)
						|| MessageBox.Show(this, "The Silkroad \"" + Settings_tbxSilkroadName.Text + "\" exist already, Do you want to update it?", "xBot", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your Media.pk2 file";
							fileDialog.FileName = "Media.pk2";
							fileDialog.Filter = "pk2 files (*.pk2)|*.pk2|All files (*.*)|*.*";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								// Keep memory clean
								using (Pk2Extractor pk2 = new Pk2Extractor(fileDialog.FileName, Settings_tbxSilkroadName.Text))
								{
									pk2.ShowDialog(this);
								}
							}
						}
					}
					break;
				case "Settings_btnLauncherPath":
					using (OpenFileDialog fileDialog = new OpenFileDialog())
					{
						fileDialog.Multiselect = false;
						fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
						fileDialog.ValidateNames = true;
						fileDialog.Title = "Select your Silkroad Launcher";
						fileDialog.FileName = "Silkroad.exe";
						fileDialog.Filter = "Silkroad.exe (*.exe)|silkroad.exe|All executables (*.exe)|*.exe";
						fileDialog.FilterIndex = 0;
						if (fileDialog.ShowDialog() == DialogResult.OK)
						{
							Settings_btnLauncherPath.Tag = fileDialog.FileName;
						}
						else
						{
							Settings_btnLauncherPath.Tag = null;
						}
					}
					break;
				case "Settings_btnClientPath":
					using (OpenFileDialog fileDialog = new OpenFileDialog())
					{
						fileDialog.Multiselect = false;
						fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
						fileDialog.ValidateNames = true;
						fileDialog.Title = "Select your SRO_Client.exe";
						fileDialog.FileName = "SRO_Client.exe";
						fileDialog.Filter = "SRO_Client.exe (sro_client.exe)|sro_client.exe|SRO_Client.exe (*.exe)|*.exe";
						fileDialog.FilterIndex = 0;
						if (fileDialog.ShowDialog() == DialogResult.OK)
						{
							Settings_btnClientPath.Tag = fileDialog.FileName;
						}
						else
						{
							Settings_btnClientPath.Tag = null;
						}
					}
					break;
				case "Settings_btnAddSilkroad":
					if (Settings_tbxSilkroadName.Text != "")
					{
						if (!isValidFilename(Settings_tbxSilkroadName.Text))
							return;
						if (!File.Exists(Pk2Extractor.GetDatabasePath(Settings_tbxSilkroadName.Text)))
							return;
						byte locale;
						if (Settings_tbxLocale.Text == "" || !byte.TryParse(Settings_tbxLocale.Text, out locale))
							return;
						uint version;
						if (Settings_tbxVersion.Text == "" || !uint.TryParse(Settings_tbxVersion.Text, out version))
							return;
						if (Settings_lstvHost.Items.Count == 0)
							return;
						ushort port;
						if (Settings_tbxPort.Text == "" || !ushort.TryParse(Settings_tbxPort.Text, out port))
							return;
						ushort hwidClientOp = 0;
						if (Settings_tbxHWIDClientOp.Text != "" && !ushort.TryParse(Settings_tbxHWIDClientOp.Text, System.Globalization.NumberStyles.HexNumber, null, out hwidClientOp))
							return;
						ushort hwidServerOp = 0;
						if (Settings_tbxHWIDServerOp.Text != "" && !ushort.TryParse(Settings_tbxHWIDServerOp.Text, System.Globalization.NumberStyles.HexNumber, null, out hwidServerOp))
							return;
						// Genearting the whole server node
						TreeNode server = new TreeNode(Settings_tbxSilkroadName.Text);
						server.Name = server.Text;

						TreeNode node = new TreeNode("Hosts");
						node.Name = "Hosts";
						foreach (ListViewItem host in Settings_lstvHost.Items)
							node.Nodes.Add(host.Text);
						server.Nodes.Add(node);
						node = new TreeNode("Use random host : " + (Settings_cbxRandomHost.Checked ? "Yes" : "No"));
						node.Name = "RandomHost";
						node.Tag = Settings_cbxRandomHost.Checked;
						server.Nodes.Add(node);
						node = new TreeNode("Port : " + port);
						node.Name = "Port";
						node.Tag = port;
						server.Nodes.Add(node);
						if (Settings_btnLauncherPath.Tag != null)
						{
							node = new TreeNode("Launcher Path : " + (string)Settings_btnLauncherPath.Tag);
							node.Name = "LauncherPath";
							node.Tag = Settings_btnLauncherPath.Tag;
							server.Nodes.Add(node);
						}
						if (Settings_btnClientPath.Tag != null)
						{
							node = new TreeNode("Client Path : " + (string)Settings_btnClientPath.Tag);
							node.Name = "ClientPath";
							node.Tag = Settings_btnClientPath.Tag;
							server.Nodes.Add(node);
						}
						node = new TreeNode("Version : " + version);
						node.Name = "Version";
						node.Tag = version;
						server.Nodes.Add(node);
						node = new TreeNode("Locale : " + locale);
						node.Name = "Locale";
						node.Tag = locale;
						server.Nodes.Add(node);

						TreeNode hwid = new TreeNode("HWID Setup (" + (hwidClientOp == 0 && hwidServerOp == 0 ? "Off" : "On") + ")");
						hwid.Name = "HWID";
						server.Nodes.Add(hwid);

						TreeNode clientNode = new TreeNode("Client");
						clientNode.Name = "Client";
						string hwidOpHex = (hwidClientOp == 0 ? "None" : "0x" + hwidClientOp.ToString("X4"));
						node = new TreeNode("Opcode: " + hwidOpHex);
						node.Name = "Opcode";
						node.Tag = hwidClientOp;
						clientNode.Nodes.Add(node);
						node = new TreeNode("Save from : " + Settings_cmbxHWIDClientSaveFrom.Text);
						node.Name = "SaveFrom";
						node.Tag = Settings_cmbxHWIDClientSaveFrom.Text;
						clientNode.Nodes.Add(node);
						hwid.Nodes.Add(clientNode);

						TreeNode serverNode = new TreeNode("Server");
						serverNode.Name = "Server";
						hwidOpHex = (hwidServerOp == 0 ? "None" : "0x" + hwidServerOp.ToString("X4"));
						node = new TreeNode("Opcode: " + hwidOpHex);
						node.Name = "Opcode";
						node.Tag = hwidServerOp;
						serverNode.Nodes.Add(node);
						node = new TreeNode("Send to : " + Settings_cmbxHWIDServerSendTo.Text);
						node.Name = "SendTo";
						node.Tag = Settings_cmbxHWIDServerSendTo.Text;
						serverNode.Nodes.Add(node);
						hwid.Nodes.Add(serverNode);

						string hwidData = "";
						if (File.Exists("Data\\" + server.Name + ".hwid"))
							hwidData = WinAPI.ToHexString(File.ReadAllBytes("Data\\" + server.Name + ".hwid"));

						node = new TreeNode("Data : " + (hwidData == "" ? "None" : hwidData));
						node.Name = "Data";
						node.Tag = hwidData;
						hwid.Nodes.Add(node);

						node = new TreeNode("Send Data only once : " + (Settings_cbxHWIDSendOnlyOnce.Checked ? "Yes" : "No"));
						node.Name = "SendOnlyOnce";
						node.Tag = Settings_cbxHWIDSendOnlyOnce.Checked;
						hwid.Nodes.Add(node);

						// Check if the key exists
						if (Settings_lstrSilkroads.Nodes.ContainsKey(server.Name))
							Settings_lstrSilkroads.Nodes[server.Name].Remove();
						else
							Login_cmbxSilkroad.Items.Add(server.Name);

						Settings_lstrSilkroads.Nodes.Add(server);
						Settings_lstrSilkroads.SelectedNode = server;
						Settings.SaveBotSettings();
					}
					break;
				case "Settings_btnAddOpcode":
					if (Settings_tbxFilterOpcode.Text != "")
					{
						int hexNumber;
						if (int.TryParse(Settings_tbxFilterOpcode.Text.ToLower().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out hexNumber))
						{
							string opcode = "0x" + hexNumber.ToString("X4");
							// Check if exists
							if (!Settings_lstvOpcodes.Items.ContainsKey(opcode))
							{
								ListViewItem item = new ListViewItem(opcode);
								item.Name = opcode;
								item.Tag = opcode;
								Settings_lstvOpcodes.Items.Add(item);
								Settings_tbxFilterOpcode.Text = "";
								Settings.SaveBotSettings();
							}
						}
					}
					break;
				case "Settings_btnInjectPacket":
					{
						Bot b = Bot.Get;
						if (Settings_tbxInjectOpcode.Text != "" && b.Proxy != null && b.Proxy.isRunning)
						{
							int hexNumber;
							if (int.TryParse(Settings_tbxInjectOpcode.Text.ToLower().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out hexNumber))
							{
								ushort opcode;
								if (ushort.TryParse(hexNumber.ToString(), out opcode))
								{
									byte[] data = new byte[0];
									if (Settings_tbxInjectData.Text != "")
									{
										try
										{
											data = WinAPI.ToByteArray(Settings_tbxInjectData.Text.Replace(" ", ""));
										}
										catch
										{
											MessageBox.Show(this, "Error: The data is not a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
									}
									LogPacket("Packet injected 0x" + opcode.ToString("X4"));

									Packet p = new Packet(opcode, Settings_cbxInjectEncrypted.Checked, Settings_cbxInjectMassive.Checked, data);
									if (Settings_cmbxInjectTo.SelectedIndex == 0)
									{
										b.Proxy.InjectToServer(p);
									}
									else
									{
										LogPacket(Utility.HexDump(p.GetBytes()) + Environment.NewLine);
										b.Proxy.InjectToClient(p);
									}
								}
								else
								{
									MessageBox.Show(this, "Error: the opcode is not ushort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
						}
					}

					break;
			}
		}
		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView t = (TreeView)sender;
			switch (t.Name)
			{
				case "Settings_lstrSilkroads":
					if (Settings_lstrSilkroads.SelectedNode != null
						&& Settings_lstrSilkroads.SelectedNode.Parent == null)
					{
						// Fill data if the root node is selected
						TreeNode server = Settings_lstrSilkroads.SelectedNode;

						Settings_tbxSilkroadName.Text = server.Text;
						Settings_lstvHost.Items.Clear();
						foreach (TreeNode host in server.Nodes["Hosts"].Nodes)
							Settings_lstvHost.Items.Add(host.Text);
						Settings_cbxRandomHost.Checked = (bool)server.Nodes["RandomHost"].Tag;
						Settings_tbxPort.Text = server.Nodes["Port"].Tag.ToString();
						Settings_btnLauncherPath.Tag = (server.Nodes.ContainsKey("LauncherPath") ? server.Nodes["LauncherPath"].Tag : null);
						Settings_btnClientPath.Tag = (server.Nodes.ContainsKey("ClientPath") ? server.Nodes["ClientPath"].Tag : null);
						Settings_tbxVersion.Text = server.Nodes["Version"].Tag.ToString();
						Settings_tbxLocale.Text = server.Nodes["Locale"].Tag.ToString();
						Settings_tbxHWIDClientOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag).ToString("X4");
						Settings_cmbxHWIDClientSaveFrom.Text = (string)server.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
						Settings_tbxHWIDServerOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag).ToString("X4");
						Settings_cmbxHWIDServerSendTo.Text = (string)server.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
						Settings_rtbxHWIDdata.Text = (string)server.Nodes["HWID"].Nodes["Data"].Tag;
						Settings_cbxHWIDSendOnlyOnce.Checked = (bool)server.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
						// Can be edited if is not being used
						if (!Login_cmbxSilkroad.Enabled && Login_cmbxSilkroad.Text == server.Text)
							this.EnableControl(Settings_btnAddSilkroad, false);
						else
							this.EnableControl(Settings_btnAddSilkroad, true);
					}
					break;
			}
		}
		private void ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView l = (ListView)sender;
			switch (l.Name)
			{
				case "Login_lstvServers":
					if (l.SelectedItems.Count == 1)
						Login_cmbxServer.Text = l.SelectedItems[0].Text;
					break;
				case "Login_lstvCharacters":
					if (l.SelectedItems.Count == 1)
						Login_cmbxCharacter.Text = l.SelectedItems[0].Name;
					break;
				case "Training_lstvAreas":
					if (l.SelectedItems.Count == 1)
					{
						ListViewItem area = l.SelectedItems[0];

            Training_tbxRegion.Text = area.SubItems[1].Text;
						Training_tbxX.Text = area.SubItems[2].Text;
						Training_tbxY.Text = area.SubItems[3].Text;
						Training_tbxZ.Text = area.SubItems[4].Text;
						Training_tbxRadius.Text = area.SubItems[5].Text;
						Training_tbxScriptPath.Text = area.SubItems[6].Text;
					}
					break;
			}
		}
		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox c = (ComboBox)sender;
			switch (c.Name)
			{
				case "Login_cmbxServer":
					foreach (ListViewItem server in Login_lstvServers.Items)
						if (Login_cmbxServer.Text == server.Text)
							Info.Get.ServerID = server.Name;
					break;
				case "Skills_cmbxAttackMobType":
					// Get the control selected and show it
					string lstvName = "Skills_lstvAttackMobType_" + Skills_cmbxAttackMobType.Text;
					Control lstvControl = Skills_cmbxAttackMobType.Parent.Controls[lstvName];
					lstvControl.Visible = true;
					// Check if exists an active control 
					if (Skills_cmbxAttackMobType.Tag != null)
					{
						Control listview = (Control)Skills_cmbxAttackMobType.Tag;
						if (listview.Name == lstvName)
							return;
						listview.Visible = false;
					}
					// Save the new control activated
					Skills_cmbxAttackMobType.Tag = lstvControl;
					break;
				case "Chat_cmbxMsgType":
					if (Chat_cmbxMsgType.Text == "Private")
						Chat_tbxMsgPlayer.Enabled = true;
					else
						Chat_tbxMsgPlayer.Enabled = false;
					break;
			}
		}
		private void Control_CheckedChanged(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			// Save settings
			switch (c.Name)
			{
				case "Login_rbnClient":
					if (Login_rbnClient.Checked)
					{
						Login_btnLauncher.Visible = true;
						Login_btnStart.Location = new Point(110, 15);
					}
					else
					{
						Login_btnLauncher.Visible = false;
						Login_btnStart.Location = new Point(110, 29);
					}
					break;
				case "Character_cbxUseHP":
				case "Character_cbxUseHPGrain":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingHP();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUseMP":
				case "Character_cbxUseMPGrain":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingMP();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUseHPVigor":
				case "Character_cbxUseMPVigor":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingVigor();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePillUniversal":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingUniversal();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePillPurification":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingPurification();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePetHP":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingRecoveryKit();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUseTransportHP":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingRecoveryKit();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePetsPill":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingAbnormalPill();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePetHGP":
					if (Bot.Get.inGame)
						Bot.Get.CheckUsingHGP();
					Settings.SaveCharacterSettings();
					break;
				case "Party_rbnSetupExpFree":
				case "Party_rbnSetupItemFree":
				case "Party_cbxSetupMasterInvite":
				case "Party_cbxInviteAll":
				case "Party_cbxInvitePartyList":
					if (Bot.Get.inGame)
						Bot.Get.CheckAutoParty();
					Settings.SaveCharacterSettings();
					break;
				case "Party_cbxLeavePartyNoneLeader":
					if (Bot.Get.inGame)
						Bot.Get.CheckPartyLeaving();
					Settings.SaveCharacterSettings();
					break;
				case "Party_cbxMatchAutoReform":
					if (Bot.Get.inGame)
						Bot.Get.CheckPartyMatchAutoReform();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxMessageExp":
				case "Character_cbxMessageUniques":
				case "Character_cbxMessageEvents":
				case "Character_cbxMessagePicks":
				case "Character_cbxAcceptRess":
				case "Character_cbxAcceptRessPartyOnly":
				case "Party_cbxAcceptOnlyPartySetup":
				case "Party_cbxInviteOnlyPartySetup":
				case "Party_cbxAcceptAll":
				case "Party_cbxAcceptPartyList":
				case "Party_cbxAcceptLeaderList":
				case "Party_cbxRefusePartys":
				case "Party_cbxMatchAcceptAll":
				case "Party_cbxMatchAcceptPartyList":
				case "Party_cbxMatchAcceptLeaderList":
				case "Party_cbxMatchRefuse":
					Settings.SaveCharacterSettings();
					break;
				case "Settings_cbxSelectFirstChar":
				case "Settings_cbxCreateChar":
				case "Settings_cbxCreateCharBelow40":
				case "Settings_cbxDeleteChar40to50":
				case "Settings_cbxLoadDefaultConfigs":
					Settings.SaveBotSettings();
					break;
				case "Settings_cbxInjectEncrypted":
					if (Settings_cbxInjectEncrypted.Checked && Settings_cbxInjectMassive.Checked)
						Settings_cbxInjectMassive.Checked = false;
					break;
				case "Settings_cbxInjectMassive":
					if (Settings_cbxInjectMassive.Checked && Settings_cbxInjectEncrypted.Checked)
						Settings_cbxInjectEncrypted.Checked = false;
					break;
			}
		}
		private void Control_TextChanged(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "Character_tbxUseHP":
				case "Character_tbxUseHPVigor":
				case "Character_tbxUseMP":
				case "Character_tbxUseMPVigor":
				case "Character_tbxUsePetHP":
				case "Character_tbxUseTransportHP":
				case "Character_tbxUsePetHGP":
				case "Training_tbxTraceDistance":
					{
						if (c.Text != "")
						{
							// check percentage 0 - 100
							ulong value;
							if (ulong.TryParse(c.Text, out value))
							{
								if (value > 100)
								{
									c.Text = "100";
								}
							}
							else
							{
								string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
								if (fixedText != c.Text)
								{
									c.Text = fixedText;
								}
							}
						}
						else
						{
							c.Text = "0";
						}
						Bot b = Bot.Get;
						if (b.inGame)
						{
							switch (c.Name)
							{
								case "Character_tbxUseHP":
									b.CheckUsingHP();
									break;
								case "Character_tbxUseHPVigor":
								case "Character_tbxUseMPVigor":
									b.CheckUsingVigor();
									break;
								case "Character_tbxUseMP":
									b.CheckUsingMP();
									break;
								case "Character_tbxUsePetHP":
								case "Character_tbxUseTransportHP":
									b.CheckUsingRecoveryKit();
									break;
								case "Character_tbxUsePetHGP":
									b.CheckUsingHGP();
									break;
							}
						}
						Settings.SaveCharacterSettings();
					}
					break;
				case "Party_tbxJoinToNumber":
					{
						// check uint
						if (c.Text != "")
						{
							ulong value;
							if (ulong.TryParse(c.Text, out value))
							{
								if (value > uint.MaxValue)
								{
									c.Text = uint.MaxValue.ToString();
								}
							}
							else
							{
								string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
								if (fixedText != c.Text)
								{
									c.Text = fixedText;
								}
							}
						}
						else
						{
							c.Text = "0";
						}
					}
					break;
				case "Party_tbxMatchTo":
				case "Party_tbxMatchFrom":
					if (c.Text != "")
					{
						// check byte
						ulong value;
						if (ulong.TryParse(c.Text, out value))
						{
							if (value > 255)
							{
								c.Text = byte.MaxValue.ToString();
							}
						}
						else
						{
							string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
							if (fixedText != c.Text)
							{
								c.Text = fixedText;
							}
						}
					}
					else
					{
						c.Text = "0";
					}
					if (Bot.Get.inGame)
					{
						Settings.SaveCharacterSettings();
					}
					break;
				case "Training_cmbxTracePlayer":
					Bot.Get.SetTraceName(c.Text);
					break;
				case "Training_tbxRadius":
					if (Bot.Get.inGame && Training_lstvAreas.SelectedItems.Count == 1)
					{
						// Check it's positive number
						ushort dummy;
						if (c.Text == "" || !ushort.TryParse(c.Text,out dummy))
						{
							// Trigger to recursive it
							c.Text = "0";
						}
						else
						{
							Training_lstvAreas.SelectedItems[0].SubItems[5].Text = c.Text;
							Settings.SaveCharacterSettings();
						}
					}
					break;
        case "Settings_tbxSilkroadName":
					{
						if (c.Text == "" || !isValidFilename(c.Text))
						{
							// Disable controls
							EnableControl(Settings_btnPK2Path, active: false);
							Settings_btnPK2Path.Tag = null;
							EnableControl(Settings_btnLauncherPath, active: false);
							EnableControl(Settings_btnClientPath, active: false);
						}
						else
						{
							// Enable
							EnableControl(Settings_btnPK2Path, active: true);
							Settings_btnPK2Path.Tag = c.Text;
							EnableControl(Settings_btnLauncherPath, active: true);
							EnableControl(Settings_btnClientPath, active: true);
						}
					}
					break;
				case "Settings_tbxCustomSequence":
					if (c.Text != "")
					{
						string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
						if (fixedText != c.Text)
						{
							c.Text = fixedText;
							Settings.SaveBotSettings();
						}
					}
					break;
				case "Settings_tbxCustomName":
					if (c.Text != "")
					{
						string fixedText = FixTextRestriction(c.Text.Trim(), "[a-zA-Z0-9_]*");
						if (fixedText != c.Text)
						{
							c.Text = fixedText;
							Settings.SaveBotSettings();
						}
					}
					break;
			}
		}
		private void ComboBox_DropDown(object sender, EventArgs e)
		{
			ComboBox c = (ComboBox)sender;
			switch (c.Name)
			{
				case "Training_cmbxTracePlayer":
					c.Items.Clear();
					if (Bot.Get.inGame)
					{
						List<SRObject> players = Info.Get.GetPlayers();
						for (int j = 0; j < players.Count; j++)
						{
							c.Items.Add(players[j].Name);
						}
					}
					break;
			}
		}
		private void Menu_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem t = (ToolStripMenuItem)sender;
			switch (t.Name)
			{
				case "Menu_NotifyIcon_Exit":
					this.Control_Click(btnWinExit, null);
					break;
				case "Menu_NotifyIcon_About":
					using (About about = new About(this))
					{
						about.ShowDialog(this);
					}
					break;
				case "Menu_NotifyIcon_HideShow":
					if (this.Visible)
					{
						this.Visible = false;
						t.Text = "Show";
					}
					else
					{
						this.Visible = true;
						t.Text = "Hide";
						BringToTop();
					}
					break;
				case "Menu_btnClientOptions_ShowHide":
					if (Bot.Get.inGame)
					{
						Process client = Bot.Get.Proxy.SRO_Client;
						if (client != null)
						{
							Color ClientVisible = Color.FromArgb(0, 128, 255);

							IntPtr[] clientWindows = WinAPI.GetProcessWindows(client.Id);
							if (btnClientOptions.ForeColor == ClientVisible)
							{
								// visible > hide and reduce the memory usage
								foreach (IntPtr p in clientWindows)
								{
									WinAPI.ShowWindow(p, WinAPI.SW_HIDE);
									WinAPI.EmptyWorkingSet(p);
								}
								btnClientOptions.BackColor = Color.FromArgb(0, 64, 191);
							}
							else
							{
								// hiden > show and make it front
								foreach (IntPtr p in clientWindows)
								{
									WinAPI.ShowWindow(p, WinAPI.SW_SHOW);
									WinAPI.SetForegroundWindow(p);
								}
								btnClientOptions.ForeColor = ClientVisible;
							}
						}
					}
					break;
				case "Menu_btnClientOptions_GoClientless":
					if (Bot.Get.inGame)
					{
						Bot.Get.GoClientless();
					}
					break;
				case "Menu_lstvItems_Use":
					if (Bot.Get.inGame && Inventory_lstvItems.SelectedItems.Count == 1)
					{
						if (!Bot.Get.UseItem(byte.Parse(Inventory_lstvItems.SelectedItems[0].Name)))
						{
							Log("This item cannot to be used");
						}
					}
					break;
				case "Menu_lstvItems_Drop":
					if (Bot.Get.inGame && Inventory_lstvItems.SelectedItems.Count == 1)
					{
						byte index = (byte)Inventory_lstvItems.SelectedIndices[0];
						if (index >= 13)
						{
							SRObject item = ((SRObjectCollection)Info.Get.Character[SRProperty.Inventory])[index];
							if (item != null)
							{
								if (MessageBox.Show(this, "Do you want to drop \"" + item.Name + "\"?", "xBot - Inventory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									PacketBuilder.DropItem(index);
								}
							}
						}
					}
					break;
				case "Menu_lstvItems_Equip":

					break;
				case "Menu_lstvPartyMembers_AddToPartyList":
					if (Party_lstvPartyMembers.SelectedItems.Count == 1)
					{
						Party_tbxPlayer.Text = Party_lstvPartyMembers.SelectedItems[0].Text;
						Control_Click(Party_btnAddPlayer, null);
					}
					break;
				case "Menu_lstvPartyMembers_AddToLeaderList":
					if (Party_lstvPartyMembers.SelectedItems.Count == 1)
					{
						Party_tbxLeader.Text = Party_lstvPartyMembers.SelectedItems[0].Text;
						Control_Click(Party_btnAddLeader, null);
					}
					break;
				case "Menu_lstvPartyMembers_KickPlayer":
					if (Party_lstvPartyMembers.SelectedItems.Count == 1)
					{
						if (Bot.Get.inParty)
						{
							PacketBuilder.BanFromParty(uint.Parse(Party_lstvPartyMembers.SelectedItems[0].Name));
						}
					}
					break;
				case "Menu_lstvPartyMembers_LeaveParty":
					if (Bot.Get.inParty)
						PacketBuilder.LeaveParty();
					break;
				case "Menu_lstvPartyList_Remove":
					if (Party_lstvPartyList.SelectedItems.Count == 1)
					{
						Party_lstvPartyList.SelectedItems[0].Remove();
						Settings.SaveCharacterSettings();
					}
					break;
				case "Menu_lstvPartyList_RemoveAll":
					if (Party_lstvPartyList.SelectedItems.Count > 0)
					{
						Party_lstvPartyList.Items.Clear();
						Settings.SaveCharacterSettings();
					}
					break;
				case "Menu_lstvLeaderList_Remove":
					if (Party_lstvLeaderList.SelectedItems.Count == 1)
					{
						Party_lstvLeaderList.SelectedItems[0].Remove();
						Settings.SaveCharacterSettings();
						if (Bot.Get.inGame)
							Bot.Get.CheckPartyLeaving();
					}
					break;
				case "Menu_lstvLeaderList_RemoveAll":
					if (Party_lstvLeaderList.SelectedItems.Count > 0)
					{
						Party_lstvLeaderList.Items.Clear();
						Settings.SaveCharacterSettings();
						if (Bot.Get.inGame)
							Bot.Get.CheckPartyLeaving();
					}
					break;
				case "Menu_lstvPartyMatch_JoinToParty":
					if (Party_lstvPartyMatch.SelectedItems.Count > 0)
					{
						Bot b = Bot.Get;
						if (b.inGame && !b.inParty)
							PacketBuilder.JoinToPartyMatch(uint.Parse(Party_lstvPartyMatch.SelectedItems[0].Name));
					}
					break;
				case "Menu_lstvPartyMatch_PrivateMsg":
					if (Party_lstvPartyMatch.SelectedItems.Count == 1)
					{
						Chat_cmbxMsgType.Text = "Private";
						Chat_tbxMsgPlayer.Text = Party_lstvPartyMatch.SelectedItems[0].SubItems[1].Text;
						TabPageV_Option_Click(TabPageV_Control01_Option11, e); // Go to chat
						TabPageH_Option_Click(TabPageH_Chat_Option02, e); // Go to private
						Chat_tbxMsg.Focus();
					}
					break;
				case "Menu_lstvArea_Add":
					if (Bot.Get.inGame)
					{
						// Create 
						int defaultKey = 1;
						string defaultAreaName;
						while (Training_lstvAreas.Items.ContainsKey(defaultAreaName = "Area #" + defaultKey))
						{
							defaultKey++;
						}

						ListViewItem newArea = new ListViewItem(defaultAreaName);
						newArea.Name = defaultAreaName;

						// Name,Region,X,Y,Z,Radius,Script
						newArea.SubItems.Add("0");
						newArea.SubItems.Add("0");
						newArea.SubItems.Add("0");
						newArea.SubItems.Add("0");
						newArea.SubItems.Add("0");
						newArea.SubItems.Add("");

						Training_lstvAreas.Items.Add(newArea);
						newArea.Selected = true;

						Settings.SaveCharacterSettings();
					}
					break;
				case "Menu_lstvArea_Remove":
					if (Bot.Get.inGame)
					{
						if (Training_lstvAreas.SelectedItems.Count == 1)
						{
							ListViewItem area = Training_lstvAreas.SelectedItems[0];
							// Remove from activated area if it's the same
							if (Training_lstvAreas.Tag != null && area == (ListViewItem)Training_lstvAreas.Tag)
								Training_lstvAreas.Tag = null;
							area.Remove();
              Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Menu_lstvArea_Activate":
					if (Bot.Get.inGame)
					{
						if (Training_lstvAreas.SelectedItems.Count == 1)
						{
							// Deactivate
							if(Training_lstvAreas.Tag != null)
							{
								ListViewItem lastActivated = (ListViewItem)Training_lstvAreas.Tag;
								lastActivated.ForeColor = Training_lstvAreas.ForeColor;
              }
							// Activate it
							Training_lstvAreas.SelectedItems[0].ForeColor = Color.FromArgb(0, 180, 255);
							Training_lstvAreas.Tag = Training_lstvAreas.SelectedItems[0];
              Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Menu_lstvOpcodes_Remove":
					if (Settings_lstvOpcodes.SelectedItems.Count == 1)
					{
						Settings_lstvOpcodes.SelectedItems[0].Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvOpcodes_RemoveAll":
					if (Settings_lstvOpcodes.Items.Count > 0)
					{
						Settings_lstvOpcodes.Items.Clear();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_rtbxPackets_AutoScroll":
					Settings_rtbxPackets.AutoScroll = t.Checked;
					break;
				case "Menu_rtbxPackets_Clear":
					Settings_rtbxPackets.Clear();
					break;
				case "Menu_lstrSilkroads_Remove":
					if (Settings_lstrSilkroads.SelectedNode != null && Settings_lstrSilkroads.SelectedNode.Parent == null)
					{
						if (!Login_cmbxSilkroad.Enabled && Login_cmbxSilkroad.Text == Settings_lstrSilkroads.SelectedNode.Name)
							return; // Is actually being used
						Login_cmbxSilkroad.Items.Remove(Settings_lstrSilkroads.SelectedNode.Name);
						Pk2Extractor.DirectoryDelete(Settings_lstrSilkroads.SelectedNode.Name);

						Settings_lstrSilkroads.SelectedNode.Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvHost_remove":
					if (Settings_lstvHost.SelectedItems.Count == 1)
					{
						Settings_lstvHost.SelectedItems[0].Remove();
						Settings.SaveBotSettings();
					}
					break;
			}
		}
		private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Menu_Click(this.Menu_NotifyIcon_HideShow, e);
		}
		private void Control_KeyDown(object sender, KeyEventArgs e)
		{
			Control c = (Control)sender;
			if (e.KeyCode == Keys.Enter)
			{
				switch (c.Name)
				{
					case "Chat_tbxMsg":
						if (Chat_tbxMsg.Text != "" && Bot.Get.inGame)
						{
							switch (Chat_cmbxMsgType.Text)
							{
								case "All":
									PacketBuilder.SendChatAll(Chat_tbxMsg.Text);
									break;
								case "Private":
									Chat_tbxMsgPlayer.Text = Chat_tbxMsgPlayer.Text.Trim();
									if (Chat_tbxMsgPlayer.Text == "" || Chat_tbxMsgPlayer.Text.StartsWith("*"))
										return;
									PacketBuilder.SendChatPrivate(Chat_tbxMsgPlayer.Text, Chat_tbxMsg.Text);
									LogChatMessage(Chat_rtbxPrivate, Chat_tbxMsgPlayer.Text + "(To)", Chat_tbxMsg.Text);
									break;
								case "Party":
									PacketBuilder.SendChatParty(Chat_tbxMsg.Text);
									break;
								case "Guild":
									PacketBuilder.SendChatGuild(Chat_tbxMsg.Text);
									break;
								case "Union":
									PacketBuilder.SendChatUnion(Chat_tbxMsg.Text);
									break;
								case "Academy":
									PacketBuilder.SendChatAcademy(Chat_tbxMsg.Text);
									break;
								case "Stall":
									if (!Bot.Get.inStall)
										return;
									PacketBuilder.SendChatStall(Chat_tbxMsg.Text);
									break;
							}
							Chat_tbxMsg.Text = "";
						}
						break;
				}
			}
		}
		private void Control_MouseClick(object sender, MouseEventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "btnClientOptions":
					c.ContextMenuStrip.Show(c, new Point(e.X, e.Y));
					break;
				case "Character_lstvBuffs":
					if (Bot.Get.inGame && e.Button == MouseButtons.Right)
					{
						ListViewItem itemClicked = this.Character_lstvBuffs.GetItemAt(e.X, e.Y);
						if (itemClicked != null)
						{
							PacketBuilder.RemoveBuff(uint.Parse(itemClicked.Name));
						}
					}
					break;
			}
		}
		/// <summary>
		/// Package all items selected to be moved.
		/// </summary>
		private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ListView l = (ListView)sender;
			// Create list to all selected items to move
			List<ListViewItem> items = new List<ListViewItem>();
			foreach (ListViewItem item in l.SelectedItems)
				items.Add(item);
			// Add moving animation
			l.DoDragDrop(l.SelectedItems, DragDropEffects.Move);
		}
		private void ListView_DragOver(object sender, DragEventArgs e)
		{
			// Check if the drag is a ListViewItem list
			if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
			{
				ListView listView = (ListView)sender;
				ListView.SelectedListViewItemCollection items = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
				// Disable drag movement in the same listview
				if (items.Count == 0
					|| items[0].ListView == listView)
				{
					return;
				}
				e.Effect = DragDropEffects.Move;
			}
		}
		private void ListView_DragDrop_RemoveFromSource(object sender, DragEventArgs e)
		{
			// Check if the drag is a ListViewItem list
			if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
			{
				ListView l = (ListView)sender;
				ListView.SelectedListViewItemCollection items = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));

				foreach (ListViewItem item in items)
				{
					item.ListView.Items.Remove(item);
					if (!l.Items.ContainsKey(item.Name))
					{
						l.Items.Add(item);
					}
				}
				// Check if at least one item has been changed
				if (items.Count > 0)
					Settings.SaveCharacterSettings();
			}
		}
		private void ListView_DragDrop_CopyFromSource_AttacksOnly(object sender, DragEventArgs e)
		{
			// Check if the drag is a ListViewItem list
			if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
			{
				ListView l = (ListView)sender;
				ListView.SelectedListViewItemCollection items = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));

				bool itemUpdated = false;
				foreach (ListViewItem item in items)
				{
					if (!l.Items.ContainsKey(item.Name))
					{
						SRObject skill = (SRObject)item.Tag;
						// Check if is an attacking skill
            if (skill.isAttackingSkill())
						{
							ListViewItem copy = (ListViewItem)item.Clone();
							copy.Name = item.Name;
							l.Items.Add(copy);
							itemUpdated = true;
						}
					}
				}
				// Check if at least one item has been changed
				if (itemUpdated)
					Settings.SaveCharacterSettings();
			}
		}
		private void ListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListView l = (ListView)sender;
			switch (l.Name)
			{
				case "Training_lstvAreas":
					if (string.IsNullOrEmpty(e.Label))
					{
						e.CancelEdit = true;
						MessageBox.Show(this, "Please, insert a valid name!", "xBot", MessageBoxButtons.OK);
					}
					else
					{
						ListViewItem item = Training_lstvAreas.Items[e.Item];
            if (item.Text != e.Label && Training_lstvAreas.Items.ContainsKey(e.Label))
						{
							e.CancelEdit = true;
							MessageBox.Show(this, "This name has been used before!", "xBot", MessageBoxButtons.OK);
						}
						else
						{
							item.Name = e.Label;
							Settings.SaveCharacterSettings();
            }
					}
					break;
			}
		}
		#endregion

		public enum ProcessState
		{
			Default,
			Warning,
			Disconnected,
			Error
		}
	}
}