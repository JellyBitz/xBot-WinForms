using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using xBot.Network;
using CefSharp;
using CefSharp.WinForms;
using xBot.Game;
using SecurityAPI;
using System.Text.RegularExpressions;
using System.Text;
using xGraphics;

namespace xBot
{
	/// <summary>
	/// Main bot window GUI that handle all bot events.
	/// </summary>
	public partial class Window : Form
	{
		public enum ProcessState
		{
			Default,
			Warning,
			Disconnected,
			Error
		}
		/// <summary>
		/// Unique instance of this class.
		/// </summary>
		private static Window _this = null;
		/// <summary>
		/// Used to show interactive extern minimap.
		/// </summary>
		public ChromiumWebBrowser Minimap_wbrChromeMap;
		private Window()
		{
			InitializeComponent();
			InitializeFonts(this);
			InitializeIndexAndTabs();
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
			Fonts f = Fonts.Get;
			for (int i = 0; i < c.Controls.Count; i++)
			{
				if (c.Controls[i].GetType().Name == "xProgressBar")
				{
					xProgressBar pgb = (xProgressBar)c.Controls[i];
					pgb.DisplayFont = Fonts.Get.Load(pgb.DisplayFont, (string)pgb.Tag);
				}
				else
				{
					// Using fontName as TAG to be selected from WinForms
					c.Controls[i].Font = f.Load(c.Controls[i].Font, (string)c.Controls[i].Tag);
					InitializeFonts(c.Controls[i]);
				}
				c.Controls[i].Tag = null;
			}
		}
		/// <summary>
		/// Initialize tab pages and options selected on GUI by default.
		/// </summary>
		private void InitializeIndexAndTabs()
		{
			// Vertical tabs
			// Login
			TabPageV_Option_Click(this.TabPageV_Control01_Option01, null);
			// Horizontal tabs
			// Character
			TabPageH_Option_Click(this.TabPageH_Character_Option01, null);
			// Party
			TabPageH_Option_Click(this.TabPageH_Party_Option01, null);
			// Chat
			TabPageH_Option_Click(this.TabPageH_Chat_Option01, null);
			Chat_cmbxMsgType.SelectedIndex = 0;
			// Settings
			TabPageH_Option_Click(this.TabPageH_Settings_Option01, null);
			Settings_cmbxHWIDClientSaveFrom.SelectedIndex = 0;
			Settings_cmbxHWIDServerSendTo.SelectedIndex = 0;
			Settings_cmbxCreateCharRace.SelectedIndex = 0;
			Settings_cmbxCreateCharGenre.SelectedIndex = 0;
			Settings_cmbxInjectTo.SelectedIndex = 0;
		}
		/// <summary>
		/// Load WebBrowser diplaying the Silkroad world map.
		/// </summary>
		public void LoadWebBrowserMap()
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
		/// <summary>
		/// Load all components (not visuals) to the App. Like settings and stuffs.
		/// </summary>
		private void Window_Load(object sender, EventArgs e)
		{
			// Welcome
			rtbxLogs.Text = string.Format("{0} Welcome to {1} v{2} | Made by Engels \"JellyBitz\" Quintero{3}{0} Discord : JellyBitz#7643 | FaceBook : @ImJellyBitz", WinAPI.getDate(), ProductName, ProductVersion,Environment.NewLine);
			LogProcess();
			// Load basic
			Settings.LoadBotSettings();
			LoadCommandLine();
			// Force visible (because no title)
			WinAPI.SetForegroundWindow(Handle);
		}
		private void Window_Closing(object sender, FormClosingEventArgs e)
		{
			Cef.Shutdown();
		}
		/// <summary>
		/// Load command arguments to the App.
		/// </summary>
		private void LoadCommandLine()
		{
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = args[i].ToLower();
				// Check data
				if (args[i].StartsWith("-silkroad="))
				{
					Login_cmbxSilkroad.Text = args[i].Substring(10);
				}
				else if (args[i].StartsWith("-username="))
				{
					Login_tbxUsername.Text = args[i].Substring(10);
				}
				else if (args[i].StartsWith("-password="))
				{
					Login_tbxPassword.Text = args[i].Substring(10);
				}
				else if (args[i].StartsWith("-server="))
				{
					// Saving to Tag because cannot be set as text yet
					Login_cmbxServer.Tag = args[i].Substring(8);
				}
				else if (args[i].StartsWith("-character="))
				{
					// Saving to Tag because cannot be set as text yet
					Login_cmbxCharacter.Tag = args[i].Substring(11);
				}
				else if (args[i].Equals("--clientless"))
				{
					Login_rbnClientless.Checked = true;
				}
				else if (args[i].Equals("--goclientless"))
				{
					Login_cbxGoClientless.Checked = true;
				}
			}
			// Check if minimum neccesary is correct to start auto login
			if (Login_cmbxSilkroad.Text != ""
				&& Login_tbxUsername.Text != ""
				&& Login_tbxPassword.Text != ""
				&& Login_cmbxServer.Tag != null)
			{
				Bot.Get.isAutoLoginMode = true;
				Control_Click(Login_btnStart, null);
			}
		}
		#region (GUI theme design)
		/// <summary>
		/// Set the control to be used as window drag from the current window.
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
		/// Colors used on TabPage Vertical.
		/// </summary>
		private Color TabPageV_ColorHover = Color.FromArgb(74,74,76),
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
		/// <para>Results are not truly as expected because Windows native focus...</para>
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
		/// Color the label associated (by name) to the current control focused.
		/// </summary>
		private void Control_FocusEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv","btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if(c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = Color.FromArgb(30, 150, 220);
					}
					break;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="Control_FocusEnter(object, EventArgs)"/>
		/// </summary>
		private void Control_FocusLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv","btn" };
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
					// Keep 256 lines as max capacity
					if (rtbxLogs.Lines.Length > 256)
						rtbxLogs.Text = rtbxLogs.Text.Substring(rtbxLogs.Text.IndexOf("\n")) + Environment.NewLine + WinAPI.getDate() + " " + text;
					else
						rtbxLogs.Text += Environment.NewLine + WinAPI.getDate() + " " + text;
				});
			}
			catch { }
		}
		public void LogMessageFilter(string message)
		{
			try
			{
				WinAPI.InvokeIfRequired(Character_rtbxMessageFilter, () => {
					if (Character_rtbxMessageFilter.Lines.Length > 2048)
						Character_rtbxMessageFilter.Text = Character_rtbxMessageFilter.Text.Substring(Character_rtbxMessageFilter.Text.IndexOf("\n")) + WinAPI.getDate() + " " + message + Environment.NewLine;
					else
						Character_rtbxMessageFilter.Text += WinAPI.getDate() + " " + message + Environment.NewLine;
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
					sb.Append(WinAPI.getDate());
				sb.AppendLine(text);
        WinAPI.InvokeIfRequired(Settings_rtbxPackets, () => {
					// Keep 2048 lines as max capacity
					if (Settings_rtbxPackets.Lines.Length > 2048)
						sb.Insert(0,Settings_rtbxPackets.Text.Substring(Settings_rtbxPackets.Text.IndexOf("\n")));
					else
						sb.Insert(0,Settings_rtbxPackets.Text);
					Settings_rtbxPackets.Text = sb.ToString();
        });
			}
			catch { }
		}
		public void LogChatMessage(RichTextBox chat, string player, string message)
		{
			try
			{
				WinAPI.InvokeIfRequired(chat, () => {
					if (chat.Lines.Length > 512)
						chat.Text = chat.Text.Substring(chat.Text.IndexOf('\n')) + player + ": " + message + Environment.NewLine;
					else
						chat.Text += player + ": " + message + Environment.NewLine;
				});
			}
			catch { }
		}
		public void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			// Check if control is disabled
			if (c.Font.Strikeout)
				return;

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
					if (Bot.Get.Proxy != null)
						Bot.Get.Proxy.Stop();
					Application.Exit();
					break;
				case "btnBotStart":
					if (c.ForeColor == Color.Red)
					{
						c.ForeColor = Color.Lime;
					}
					else
					{
						c.ForeColor = Color.Red;
					}
					break;
				case "btnAnalyzer":
					TabPageV_Option_Click(TabPageV_Control01_Option14, null);
					TabPageH_Option_Click(TabPageH_Settings_Option04, null);
					break;
				case "btnShowHideClient":
					if (Bot.Get.Proxy != null)
					{
						Process sro_client = Bot.Get.Proxy.SRO_Client;
						if (sro_client != null)
						{
							IntPtr[] clientWindows = WinAPI.GetProcessWindows(sro_client.Id);
							if (btnShowHideClient.ForeColor == Color.DodgerBlue)
							{
								// visible > hide and reduce the memory usage
								foreach (IntPtr p in clientWindows)
								{
									WinAPI.ShowWindow(p, WinAPI.SW_HIDE);
									WinAPI.EmptyWorkingSet(p);
								}
								btnShowHideClient.ForeColor = Color.RoyalBlue;
							}
							else
							{
								// hiden > show and make it front
								foreach (IntPtr p in clientWindows)
								{
									WinAPI.ShowWindow(p, WinAPI.SW_SHOW);
									WinAPI.SetForegroundWindow(p);
								}
								btnShowHideClient.ForeColor = Color.DodgerBlue;
							}
						}
					}
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
							i.Version = uint.Parse(silkroad.Nodes["Version"].Tag.ToString());

							// Lock Silkroad selection
							c.Text = "STOP";
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
							PacketBuilder.Login(Login_tbxUsername.Text, Login_tbxPassword.Text,Convert.ToUInt16(Info.Get.ServerID));
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
				case "Character_btnAddINT":
					if (Bot.Get.inGame)
					{
						if((ushort)Info.Get.Character[SRAttribute.StatPoints] > 0)
						{
							PacketBuilder.AddStatPointINT();
						}
					}
					break;
				case "Character_btnAddSTR":
					if (Bot.Get.inGame)
					{
						if ((ushort)Info.Get.Character[SRAttribute.StatPoints] > 0)
						{
							PacketBuilder.AddStatPointSTR();
						}
					}
					break;
				case "Party_btnAddPlayer":
					if (Bot.Get.inGame)
					{
						// Check if already exists
						if (Party_tbxPlayer.Text != "" && !Party_lstvPlayers.Items.ContainsKey(Party_tbxPlayer.Text.ToLower()))
						{
							ListViewItem player = new ListViewItem(Party_tbxPlayer.Text);
							player.Name = player.Text.ToLower();
							Party_lstvPlayers.Items.Add(player);

							Party_tbxPlayer.Text = "";
							Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Party_btnAddLeader":
					if (Bot.Get.inGame)
					{
						if (Party_tbxLeader.Text != "" && !Party_lstvLeaders.Items.ContainsKey(Party_tbxLeader.Text.ToLower()))
						{
							ListViewItem leader = new ListViewItem(Party_tbxLeader.Text);
							leader.Name = leader.Text.ToLower();
							Party_lstvLeaders.Items.Add(leader);

							Party_tbxLeader.Text = "";
							Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Party_btnRefreshMatch":
					if (Bot.Get.inGame)
					{
						PacketBuilder.RequestPartyMatch(0);
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
						PacketBuilder.RequestPartyMatch(int.Parse(Party_lblPageNumber.Text) - 2);
					}
					break;
				case "Party_btnNextPage":
					Party_btnNextPage.Enabled = false;
					if (Bot.Get.inGame)
					{
						PacketBuilder.RequestPartyMatch(int.Parse(Party_lblPageNumber.Text));
					}
					break;
				case "Minimap_btnLoadMap":
					// Try to load WebBrowser map
					LoadWebBrowserMap();
					if (Minimap_wbrChromeMap != null)
					{
						// Map loaded
						Minimap_btnLoadMap.Parent.Controls.Remove(Minimap_btnLoadMap);
					}
					break;
				case "GameInfo_btnRefresh":
					GameInfo_lstrObjects.Nodes.Clear();
					if (Bot.Get.inGame)
					{
						Info i = Info.Get;
						GameInfo_lstrObjects.Nodes.Add(i.Character.ToNode());
						// Making a copy because the list can be edited while iterating
						SRObject[] objects = i.EntityList.ToArray();
						foreach (SRObject obj in objects)
							GameInfo_lstrObjects.Nodes.Add(obj.ToNode());
						GameInfo_tbxServerTime.Text = i.GetServerTime();
						GameInfo_tbxWheaterTime.Text = i.GetWheaterTime()+" "+ i.GetWheater() + " | " +i.GetMoonphase();
					}
					break;
				case "Settings_btnPK2Path":
					if (!Directory.Exists("Data"))
						Directory.CreateDirectory("Data");
					if (!Database.Exists(Settings_tbxSilkroadName.Text) || Database.Exists(Settings_tbxSilkroadName.Text) && MessageBox.Show(this, "The database \"" + Settings_tbxSilkroadName.Text + "\" already exists, Do you want to update it?", "xBot", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
								using (PK2Extractor pk2 = new PK2Extractor(fileDialog.FileName, Settings_tbxSilkroadName.Text))
								{
									pk2.ShowDialog(this);
								}
							}
							WinAPI.SetForegroundWindow(this.Handle);
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
						string silkroadkey = Settings_tbxSilkroadName.Text;
						if (!CleanSilkroadName(ref silkroadkey))
							return;
						if (!Database.Exists(silkroadkey))
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
						TreeNode server = new TreeNode(silkroadkey);
						server.Name = silkroadkey;

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
						if (File.Exists("Data\\" + silkroadkey + ".hwid"))
							hwidData = WinAPI.BytesToHexString(File.ReadAllBytes("Data\\" + silkroadkey + ".hwid"));

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
							if (Settings_lstvOpcodes.Items.Find(opcode, false).Length == 0)
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
					if (Settings_tbxInjectOpcode.Text != "" && Bot.Get.Proxy.isRunning)
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
										data = WinAPI.HexStringToBytes(Settings_tbxInjectData.Text.Replace(" ",""));
									}
									catch
									{
										MessageBox.Show(this, "Error: The data is not a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
								}
								LogPacket("Packet injected 0x" + opcode.ToString("X4"));

								Packet p = new Packet(opcode, Settings_cbxInjectEncrypted.Checked, Settings_cbxInjectMassive.Checked, data);
								if (Settings_cmbxInjectTo.SelectedIndex == 0) {
									Bot.Get.Proxy.InjectToServer(p);
								}
								else
								{
									LogPacket(Utility.HexDump(p.GetBytes())+Environment.NewLine);
									Bot.Get.Proxy.InjectToClient(p);
								}
							}
							else
							{
								MessageBox.Show(this, "Error: the opcode is not ushort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						if(!Login_cmbxSilkroad.Enabled && Login_cmbxSilkroad.Text == server.Text)
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
			switch (c.Name)
			{
				case "Login_rbnClient":
					if (Login_rbnClient.Checked)
					{
						Login_btnLauncher.Visible = true;
						Login_btnStart.Location = new Point(110, 16);
					}
					else
					{
						Login_btnLauncher.Visible = false;
						Login_btnStart.Location = new Point(110, 34);
					}
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
				case "Character_cbxMessageExp":
				case "Character_cbxMessageUniques":
				case "Character_cbxMessageEvents":
				case "Character_cbxMessagePicks":
				case "Character_cbxUseHP":
				case "Character_cbxUseHPGrain":
				case "Character_cbxUseHPVigor":
				case "Character_cbxUseMP":
				case "Character_cbxUseMPGrain":
				case "Character_cbxUseMPVigor":
				case "Character_cbxUsePillUniversal":
				case "Character_cbxUsePillPurification":
				case "Character_cbxAcceptRess":
        case "Character_cbxAcceptRessPartyOnly":
        case "Party_rbnSetupExpFree":
				case "Party_rbnSetupItemFree":
				case "Party_cbxSetupMasterInvite":
				case "Party_cbxAcceptOnlyPartySetup":
				case "Party_cbxAcceptAll":
				case "Party_cbxAcceptPlayerList":
				case "Party_cbxAcceptLeaderList":
				case "Party_cbxLeavePartyNoneLeader":
				case "Party_cbxRefusePartys":
					Settings.SaveCharacterSettings();
					break;
			}
		}
		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			switch (t.Name)
			{
				case "Character_tbxUseHP":
				case "Character_tbxUseHPVigor":
				case "Character_tbxUseMP":
				case "Character_tbxUseMPVigor":
					// Percentage only
					if (t.Text != "")
					{
						ushort value;
						if (ushort.TryParse(t.Text,out value))
						{
							if (value > 100)
							{
								t.Text = "100"; // Max
							}
						}
						else
						{
							string fixedText = FixTextRestriction(t.Text.Trim(), "[0-9]*");
							if (fixedText != t.Text)
							{
								t.Text = fixedText;
							}
						}
					}
					else
					{
						t.Text = "0"; // Min
					}
					if (Bot.Get.inGame)
					{
						// Force check about potions right there
						Bot.Get._Event_StateUpdated();
					}
					Settings.SaveCharacterSettings();
					break;
				case "Party_tbxJoinToNumber":
					if (t.Text != "")
					{
						// Numeric only
						string fixedText = FixTextRestriction(t.Text.Trim(), "[0-9]*");
						if (fixedText != t.Text)
						{
							t.Text = fixedText;
						}
					}
					break;
				case "Settings_tbxSilkroadName":
					string silkroadkey = Settings_tbxSilkroadName.Text;
					if (CleanSilkroadName(ref silkroadkey))
					{
						EnableControl(Settings_btnPK2Path, true);
						Settings_btnPK2Path.Tag = silkroadkey;
						EnableControl(Settings_btnLauncherPath, true);
						EnableControl(Settings_btnClientPath, true);
					}
					else
					{
						EnableControl(Settings_btnPK2Path, false);
						Settings_btnPK2Path.Tag = null;
						EnableControl(Settings_btnLauncherPath, false);
						EnableControl(Settings_btnClientPath, false);
					}
					break;
				case "Settings_tbxCustomSequence":
					if (t.Text != "")
					{
						// Numeric only
						string fixedText = FixTextRestriction(t.Text.Trim(), "[0-9]*");
						if (fixedText != t.Text)
						{
							t.Text = fixedText;
							Settings.SaveBotSettings();
						}
					}
					break;
				case "Settings_tbxCustomName":
					if (t.Text != "")
					{
						// Silkroad nickname
						string fixedText = FixTextRestriction(t.Text.Trim(), "[a-zA-Z0-9_]*");
						if (fixedText != t.Text)
						{
							t.Text = fixedText;
							Settings.SaveBotSettings();
						}
					}
					break;
			}
		}
		private bool CleanSilkroadName(ref string SilkroadName)
		{
			SilkroadName = SilkroadName.Trim();
			if (SilkroadName != "")
			{
				MatchCollection matches = Regex.Matches(SilkroadName, @"^[\w\-._ ]+$");
				if (matches.Count > 0)
				{
					SilkroadName = "";
					foreach (Match m in matches)
						SilkroadName += m.Value;
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Fix the text using a pattern restriction. Returns empty if the pattern is not found.
		/// </summary>
		private string FixTextRestriction(string Text,string Pattern,bool FirstMatch=false)
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
		private void RichTextBox_TextChanged_AutoScroll(object sender, EventArgs e)
		{
			RichTextBox r = (RichTextBox)sender;
			WinAPI.SendMessage(r.Handle, WinAPI.WM_VSCROLL, WinAPI.SB_PAGEBOTTOM, 0);
			r.SelectionStart = r.Text.Length;
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
					using(About about = new About(this)){
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
					}
					break;
				case "Menu_lstvMembers_AddToPlayerList":
					if (Party_lstvMembers.SelectedItems.Count == 1)
					{
						Party_tbxPlayer.Text = Party_lstvMembers.SelectedItems[0].Text;
						Control_Click(Party_btnAddPlayer, null);
					}
					break;
				case "Menu_lstvMembers_AddToLeaderList":
					if (Party_lstvMembers.SelectedItems.Count == 1)
					{
						Party_tbxLeader.Text = Party_lstvMembers.SelectedItems[0].Text;
						Control_Click(Party_btnAddLeader, null);
					}
					break;
				case "Menu_lstvMembers_LeaveParty":
					if (Bot.Get.inParty)
					{
						PacketBuilder.LeaveParty();
					}
					break;
				case "Menu_lstvOpcodes_remove":
					if (Settings_lstvOpcodes.SelectedItems.Count == 1)
					{
						Settings_lstvOpcodes.SelectedItems[0].Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvOpcodes_removeAll":
					if (Settings_lstvOpcodes.Items.Count > 0)
					{
						Settings_lstvOpcodes.Items.Clear();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_rtbxPackets_AutoScroll":
					if (t.Checked)
						Settings_rtbxPackets.TextChanged += new EventHandler(RichTextBox_TextChanged_AutoScroll);
					else
						Settings_rtbxPackets.TextChanged -= new EventHandler(RichTextBox_TextChanged_AutoScroll);
					break;
				case "Menu_rtbxPackets_Clear":
					Settings_rtbxPackets.Clear();
					break;
				case "Menu_lstrSilkroads_remove":
					if (Settings_lstrSilkroads.SelectedNode != null && Settings_lstrSilkroads.SelectedNode.Parent == null)
					{
						if (!Login_cmbxSilkroad.Enabled && Login_cmbxSilkroad.Text == Settings_lstrSilkroads.SelectedNode.Name)
							return; // Is actually being used
						Login_cmbxSilkroad.Items.Remove(Settings_lstrSilkroads.SelectedNode.Name);
						Database.Delete(Settings_lstrSilkroads.SelectedNode.Name);

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
							if (Chat_cmbxMsgType.Text == "Private" && Chat_tbxMsgPlayer.Text == "")
								return;
							switch (Chat_cmbxMsgType.Text)
							{
								case "All":
									PacketBuilder.SendChatAll(Chat_tbxMsg.Text);
									break;
								case "Private":
									PacketBuilder.SendChatPrivate(Chat_tbxMsgPlayer.Text, Chat_tbxMsg.Text);
									LogChatMessage(Chat_rtbxPrivate,Chat_tbxMsgPlayer.Text + "(To)",Chat_tbxMsg.Text);
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
							}
							Chat_tbxMsg.Text = "";
						}
						break;
				}
			}
		}
		/// <summary>
		/// Set the gold in Silkroad format color.
		/// </summary>
		/// <param name="gold"></param>
		public void SetSROGold(ulong gold)
		{
			// 1000000 to 1.000.000
			string Text = gold.ToString("#,0");
			int GoldDigits = gold.ToString().Length;
			// Visual color
			Color ForeColor = Color.White; // Default
			if (GoldDigits > 4)
			{
				if (GoldDigits < 6)
				{
					ForeColor = Color.FromArgb(255, 250, 133);
				}
				else if (GoldDigits < 7)
				{
					ForeColor = Color.FromArgb(255, 211, 72);
				}
				else if (GoldDigits < 10)
				{
					ForeColor = Color.FromArgb(255, 173, 92);
				}
				else if (GoldDigits < 13)
				{
					
				}
			}
			WinAPI.InvokeIfRequired(Character_lblGold, () => {
				Character_lblGold.ForeColor = ForeColor;
				Character_lblGold.Text = Text;
			});
		}
		public void Party_Clear()
		{
			WinAPI.InvokeIfRequired(Party_lstvMembers, () => {
				Party_lstvMembers.Items.Clear();
			});
			WinAPI.InvokeIfRequired(Party_lblCurrentSetup, () => {
				Party_lblCurrentSetup.Text = "";
				Party_lblCurrentSetup.Tag = null;
			});
			WinAPI.InvokeIfRequired(this, () => {
				ToolTips.SetToolTip(Party_lblCurrentSetup, "");
			});
		}
		public void GameInfo_Clear()
		{
			WinAPI.InvokeIfRequired(this, () => {
				GameInfo_lstrObjects.Nodes.Clear();
			});
		}
		public void GameInfo_AddSpawn(SRObject entity)
		{
			WinAPI.InvokeIfRequired(this, () =>
			{
				GameInfo_lstrObjects.Nodes.Add(entity.ToNode());
			});
		}
		public void GameInfo_RemoveSpawn(uint uniqueID)
		{
			WinAPI.InvokeIfRequired(this, () => {
				if (GameInfo_lstrObjects.Nodes.ContainsKey(uniqueID.ToString()))
				{
					GameInfo_lstrObjects.Nodes[uniqueID.ToString()].Remove();
				}
			});
		}
		public void Minimap_CharacterPointer_Move(int x, int y, int z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.MovePointer(" + x + "," + y + "," + z + "," + region + ");", true);
			}
		}
		public void Minimap_ObjectPointer_Add(uint UniqueID, string servername, string htmlPopup, int x, int y, int z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.AddExtraPointer('" + UniqueID + "','" + servername + "','" + htmlPopup + "'," + ((int)x) + "," + ((int)y) + "," + ((int)z) + "," + region + ");", true);
			}
		}
		public void Minimap_ObjectPointer_Move(uint UniqueID, int x, int y, int z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.MoveExtraPointer('" + UniqueID + "'," + x + "," + y + "," + z + "," + region + ");", true);
			}
		}
		public void Minimap_ObjectPointer_Remove(uint UniqueID)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.RemoveExtraPointer('" + UniqueID + "');", true);
			}
		}

		public void Minimap_ObjectPointer_Clear()
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.RemoveAllExtraPointers();", true);
			}
		}
	}
}