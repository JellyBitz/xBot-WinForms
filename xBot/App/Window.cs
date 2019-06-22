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
	public partial class Window : Form
	{
		public enum ProcessState
		{
			Default,
			Warning,
			Disconnected,
			Error
		}
		// Will exist only one reference and can be called from any class
		private static Window _this = null;
		// Used to show interactive minimap
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
		private void InitializeFonts(Control c)
		{
			for (int i = 0; i < c.Controls.Count; i++)
			{
				if (c.Controls[i].GetType().Name == "xProgressBar")
				{
					xProgressBar pgb = (xProgressBar)c.Controls[i];
					pgb.TextFont = Fonts.Get.Load(pgb.TextFont, (string)pgb.Tag);
        }
				else
				{
					// Using fontName as TAG to be selected from WinForms
					c.Controls[i].Font = Fonts.Get.Load(c.Controls[i].Font, (string)c.Controls[i].Tag);
					InitializeFonts(c.Controls[i]);
				}
				c.Controls[i].Tag = null;
      }
		}
		private void InitializeIndexAndTabs()
		{
			// Vertical tabs
			// Login
			this.TabPageV_Option_Click(this.TabPageV_Control01_Option01, null);
			// Horizontal tabs
			// Character
			this.TabPageH_Option_Click(this.TabPageH_Character_Option01, null);
			// Party
			this.TabPageH_Option_Click(this.TabPageH_Party_Option01, null);
			// Chat
			this.TabPageH_Option_Click(this.TabPageH_Chat_Option01, null);
			Chat_cmbxMsgType.SelectedIndex = 0;
			// General
			this.TabPageH_Option_Click(this.TabPageH_General_Option01, null);
			this.General_cmbxHWIDClientSaveFrom.SelectedIndex = 0;
			this.General_cmbxHWIDServerSendTo.SelectedIndex = 0;
			this.General_cmbxInjectTo.SelectedIndex = 0;
		}
		/// <summary>
		/// Load WebBrowser diplaying the Silkroad world map.
		/// </summary>
		public void InitializeChromium()
		{
			string path = Directory.GetCurrentDirectory() + "\\Minimap\\index.html";
			if (Minimap_wbrChromeMap == null && File.Exists(path))
			{
				// Initialize cef with the provided settings
				CefSettings settings = new CefSettings();
				Cef.Initialize(settings);
				Minimap_wbrChromeMap = new ChromiumWebBrowser(path);
        Minimap_wbrChromeMap.Dock = DockStyle.Fill;
				Minimap_panelMap.Controls.Add(Minimap_wbrChromeMap);
			}
		}
		private void Window_Load(object sender, EventArgs e)
		{
			// Welcome
			rtbxLogs.Text = WinAPI.getDate() + " Welcome to " + ProductName + " v" + ProductVersion + " | Made by Engels \"JellyBitz\" Quintero\n";
			rtbxLogs.Text += WinAPI.getDate() + " Discord : JellyBitz#7643";
			LogProcess();
			// Load basic
			Settings.LoadBotSettings();
			LoadCommandLine();
			// Force visible (because no title)
			this.BringToFront();
		}
		private void LoadCommandLine()
		{
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
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
					Login_cmbxServer.Text = args[i].Substring(8);
				}
				else if (args[i].StartsWith("-charname="))
				{
					Login_cmbxCharacter.Text = args[i].Substring(11);
				}
				else if (args[i].Equals("--client"))
				{
					Login_rbnClient.Checked = true;
				}
			}
			// Check if everything is correct to start auto login
			if (Login_tbxUsername.Text != ""
				&& Login_tbxPassword.Text != ""
				&& Login_cmbxServer.Text != ""
				&& Login_cmbxCharacter.Text != "")
			{
				Login_gbxConnection.Enabled = false;
				Login_gbxAccount.Enabled = false;
				Bot.Get.isAutoLogin = true;
				Control_Click(this.Login_btnStart, null);
			}
		}
		#region GUI Design
		// Header Draggable
		private void Window_Drag_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WinAPI.ReleaseCapture();
				WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
			}
		}
		// TabPage Vertical Control
		private Color TabPageV_ColorHover = Color.FromArgb(62, 62, 64), TabPageV_ColorSelected = Color.FromArgb(0, 122, 204);
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
		// TabPage Horizontal Control
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
		public void EnableControl(Control c, bool active)
		{
			WinAPI.InvokeIfRequired(c, () =>{
				c.Font = new Font(c.Font, (active ? FontStyle.Regular : FontStyle.Strikeout));
			});
		}
		// Control Focus
		private void Control_FocusEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = Color.FromArgb(30, 150, 220);
					break;
				}
			}
		}
		private void Control_FocusLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = c.Parent.BackColor;
					break;
				}
			}
		}
		private void WindowState_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
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
			}
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
						rtbxLogs.Text = rtbxLogs.Text.Substring(rtbxLogs.Text.IndexOf("\n"));
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
					if (Character_rtbxMessageFilter.Lines.Length > 256)
						Character_rtbxMessageFilter.Text = Character_rtbxMessageFilter.Text.Substring(Character_rtbxMessageFilter.Text.IndexOf("\n"));
					Character_rtbxMessageFilter.Text += WinAPI.getDate() + " " + message + Environment.NewLine;
				});
			}
			catch { }
		}
		public void LogPacket(string packet)
		{
			try
			{
				WinAPI.InvokeIfRequired(General_rtbxPackets, () => {
					// Keep 1024 lines as max capacity
					if (General_rtbxPackets.Lines.Length > 1024)
						General_rtbxPackets.Text = General_rtbxPackets.Text.Substring(General_rtbxPackets.Text.IndexOf("\n"));
					General_rtbxPackets.Text += packet + Environment.NewLine;
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
						chat.Text = chat.Text.Substring(chat.Text.IndexOf('\n'));
					chat.Text += player +": "+ message + Environment.NewLine;
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
				case "btnBotStart":
					if (c.ForeColor == Color.Red)
					{
						// Button stop
						c.ForeColor = Color.Lime;
					}
					else
					{
						c.ForeColor = Color.Red;
					}
					break;
				case "Login_btnAddSilkroad":
					TabPageV_Option_Click(TabPageV_Control01_Option14, null);
					TabPageH_Option_Click(TabPageH_General_Option01, null);
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
								MessageBox.Show(this, "The database \"" + General_tbxSilkroadName.Text + "\" needs to be created.", "xBot", MessageBoxButtons.OK);
								TabPageV_Option_Click(TabPageV_Control01_Option14, null);
								TabPageH_Option_Click(TabPageH_General_Option01, null);
								return;
							}
							TreeNode silkroad = General_lstrSilkroads.Nodes[i.Silkroad];

							// SR_Client Path check
							if (!Login_rbnClientless.Checked)
							{
								if (!silkroad.Nodes.ContainsKey("ClientPath"))
								{
									MessageBox.Show(this, "You need to select the SRO_Client path first.", "xBot", MessageBoxButtons.OK);
									TabPageV_Option_Click(TabPageV_Control01_Option14, null);
									TabPageH_Option_Click(TabPageH_General_Option01, null);
									return;
								}
								Bot.Get.ClientPath = (string)silkroad.Nodes["ClientPath"].Tag;
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
							EnableControl(General_btnAddSilkroad, false);
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
							Bot.Get.LoginFromBot = true;
							PacketBuilder.Login(Login_tbxUsername.Text, Login_tbxPassword.Text, (ushort)Login_cmbxServer.Tag);
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
						TreeNode silkroad = General_lstrSilkroads.Nodes[Login_cmbxSilkroad.Text];
						if (!silkroad.Nodes.ContainsKey("LauncherPath"))
						{
							MessageBox.Show(this, "You need to select the Launcher path first.", "xBot", MessageBoxButtons.OK);
							TabPageV_Option_Click(TabPageV_Control01_Option14, null);
							TabPageH_Option_Click(TabPageH_General_Option01, null);
						}
					}
					break;
				case "btnShowHideClient":
					Process sro_client = WinAPI.getSROCientProcess();
					if (sro_client != null)
					{
						IntPtr[] clientWindows = WinAPI.GetProcessWindows(sro_client.Id);
						// DodgerBlue = Visible, RoyalBlue = Hiden
						if (btnShowHideClient.ForeColor == Color.DodgerBlue)
						{
							foreach (IntPtr p in clientWindows)
							{
								WinAPI.ShowWindow(p, WinAPI.SW_HIDE);
								WinAPI.SetForegroundWindow(p);
								WinAPI.EmptyWorkingSet(sro_client.Handle);
							}
							btnShowHideClient.ForeColor = Color.RoyalBlue;
						}
						else
						{
							foreach (IntPtr p in clientWindows)
							{
								WinAPI.ShowWindow(p, WinAPI.SW_SHOW);
								WinAPI.SetForegroundWindow(p);
							}
							btnShowHideClient.ForeColor = Color.DodgerBlue;
						}
					}
					break;
				case "btnAnalizer":
					TabPageV_Option_Click(TabPageV_Control01_Option14, null);
					TabPageH_Option_Click(TabPageH_General_Option04, null);
					break;
				case "General_btnAddOpcode":
					if (General_tbxFilterOpcode.Text != "")
					{
						int hexNumber;
						if (int.TryParse(General_tbxFilterOpcode.Text.ToLower().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out hexNumber))
						{
							string opcode = "0x" + hexNumber.ToString("X4");
							// Check if exists
							if (General_lstvOpcodes.Items.Find(opcode, false).Length == 0)
							{
								ListViewItem item = new ListViewItem(opcode);
								item.Name = opcode;
								item.Tag = opcode;
								General_lstvOpcodes.Items.Add(item);
								General_tbxFilterOpcode.Text = "";
								Settings.SaveBotSettings();
							}
						}
					}
					break;
				case "Minimap_btnLoadMap":
					// Try to load WebBrowser map
					InitializeChromium();
					if (Minimap_wbrChromeMap != null)
					{
						// Map loaded
						Minimap_btnLoadMap.Parent.Controls.Remove(Minimap_btnLoadMap);
					}
					break;
				case "General_btnPK2Path":
					if (!Directory.Exists("Data"))
						Directory.CreateDirectory("Data");
					if (!Database.Exists(General_tbxSilkroadName.Text) || Database.Exists(General_tbxSilkroadName.Text) && MessageBox.Show(this, "The database \"" + General_tbxSilkroadName.Text + "\" already exists, Do you want to update it?", "xBot", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						OpenFileDialog fileDialog = new OpenFileDialog();
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
							using (PK2Extractor pk2 = new PK2Extractor(fileDialog.FileName, General_tbxSilkroadName.Text))
							{
								pk2.ShowDialog(this);
							}
						}
						WinAPI.SetForegroundWindow(this.Handle);
					}
					break;
				case "General_btnLauncherPath":
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
							General_btnLauncherPath.Tag = fileDialog.FileName;
						}
						else
						{
							General_btnLauncherPath.Tag = null;
						}
					}
					break;
				case "General_btnClientPath":
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
							General_btnClientPath.Tag = fileDialog.FileName;
						}
						else
						{
							General_btnClientPath.Tag = null;
						}
					}
					break;
				case "General_btnAddSilkroad":
					if (General_tbxSilkroadName.Text != "")
					{
						string silkroadkey = General_tbxSilkroadName.Text;
						if (!CleanSilkroadName(ref silkroadkey))
							return;
						if (!Database.Exists(silkroadkey))
							return;
						byte locale;
						if (General_tbxLocale.Text == "" || !byte.TryParse(General_tbxLocale.Text, out locale))
							return;
						uint version;
						if (General_tbxVersion.Text == "" || !uint.TryParse(General_tbxVersion.Text, out version))
							return;
						if (General_lstvHost.Items.Count == 0)
							return;
						ushort port;
						if (General_tbxPort.Text == "" || !ushort.TryParse(General_tbxPort.Text, out port))
							return;
						ushort hwidClientOp = 0;
						if (General_tbxHWIDClientOp.Text != "" && !ushort.TryParse(General_tbxHWIDClientOp.Text, System.Globalization.NumberStyles.HexNumber, null, out hwidClientOp))
							return;
						ushort hwidServerOp = 0;
						if (General_tbxHWIDServerOp.Text != "" && !ushort.TryParse(General_tbxHWIDServerOp.Text, System.Globalization.NumberStyles.HexNumber, null, out hwidServerOp))
							return;
						// Genearting the whole server node
						TreeNode server = new TreeNode(silkroadkey);
						server.Name = silkroadkey;

						TreeNode node = new TreeNode("Hosts");
						node.Name = "Hosts";
						foreach (ListViewItem host in General_lstvHost.Items)
							node.Nodes.Add(host.Text);
						server.Nodes.Add(node);
						node = new TreeNode("Use random host : " + (General_cbxRandomHost.Checked ? "Yes" : "No"));
						node.Name = "RandomHost";
						node.Tag = General_cbxRandomHost.Checked;
						server.Nodes.Add(node);
						node = new TreeNode("Port : " + port);
						node.Name = "Port";
						node.Tag = port;
						server.Nodes.Add(node);
						if (General_btnLauncherPath.Tag != null)
						{
							node = new TreeNode("Launcher Path : " + (string)General_btnLauncherPath.Tag);
							node.Name = "LauncherPath";
							node.Tag = General_btnLauncherPath.Tag;
							server.Nodes.Add(node);
						}
						if (General_btnClientPath.Tag != null)
						{
							node = new TreeNode("Client Path : " + (string)General_btnClientPath.Tag);
							node.Name = "ClientPath";
							node.Tag = General_btnClientPath.Tag;
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
						node = new TreeNode("Save from : " + General_cmbxHWIDClientSaveFrom.Text);
						node.Name = "SaveFrom";
						node.Tag = General_cmbxHWIDClientSaveFrom.Text;
						clientNode.Nodes.Add(node);
						hwid.Nodes.Add(clientNode);

						TreeNode serverNode = new TreeNode("Server");
						serverNode.Name = "Server";
						hwidOpHex = (hwidServerOp == 0 ? "None" : "0x" + hwidServerOp.ToString("X4"));
						node = new TreeNode("Opcode: " + hwidOpHex);
						node.Name = "Opcode";
						node.Tag = hwidServerOp;
						serverNode.Nodes.Add(node);
						node = new TreeNode("Send to : " + General_cmbxHWIDServerSendTo.Text);
						node.Name = "SendTo";
						node.Tag = General_cmbxHWIDServerSendTo.Text;
						serverNode.Nodes.Add(node);
						hwid.Nodes.Add(serverNode);

						string hwidData = "";
						if (File.Exists("Data\\" + silkroadkey + ".hwid"))
							hwidData = WinAPI.BytesToHexString(File.ReadAllBytes("Data\\" + silkroadkey + ".hwid"));

						node = new TreeNode("Data : " + (hwidData == "" ? "None" : hwidData));
						node.Name = "Data";
						node.Tag = hwidData;
						hwid.Nodes.Add(node);

						node = new TreeNode("Send Data only once : " + (General_cbxHWIDSendOnlyOnce.Checked ? "Yes" : "No"));
						node.Name = "SendOnlyOnce";
						node.Tag = General_cbxHWIDSendOnlyOnce.Checked;
						hwid.Nodes.Add(node);

						// Check if the key exists
						if (General_lstrSilkroads.Nodes.ContainsKey(server.Name))
							General_lstrSilkroads.Nodes[server.Name].Remove();
						else
							Login_cmbxSilkroad.Items.Add(server.Name);

						General_lstrSilkroads.Nodes.Add(server);
						General_lstrSilkroads.SelectedNode = server;
						Settings.SaveBotSettings();
					}
					break;
				case "General_btnInjectPacket":
					if (General_tbxInjectOpcode.Text != "" && Bot.Get.Proxy.isRunning)
					{
						int hexNumber;
						if (int.TryParse(General_tbxInjectOpcode.Text.ToLower().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out hexNumber))
						{
							ushort opcode;
							if (ushort.TryParse(hexNumber.ToString(), out opcode))
							{
								byte[] data = new byte[0];
								if (General_tbxInjectData.Text != "")
								{
									try
									{
										data = WinAPI.HexStringToBytes(General_tbxInjectData.Text.Replace(" ",""));
									}
									catch
									{
										MessageBox.Show(this, "Error: The data is not a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
								}
								Packet p = new Packet(opcode, General_cbxInjectEncrypted.Checked, General_cbxInjectMassive.Checked, data);
								if (General_cmbxInjectTo.SelectedIndex == 0)
									Bot.Get.Proxy.InjectToServer(p);
								else
									Bot.Get.Proxy.InjectToClient(p);
								LogPacket("Packet injected 0x" + opcode.ToString("X4"));
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
				case "General_lstrSilkroads":
					if (General_lstrSilkroads.SelectedNode != null
						&& General_lstrSilkroads.SelectedNode.Parent == null)
					{
						// Fill data if the root node is selected
						TreeNode server = General_lstrSilkroads.SelectedNode;

						General_tbxSilkroadName.Text = server.Text;
						General_lstvHost.Items.Clear();
						foreach (TreeNode host in server.Nodes["Hosts"].Nodes)
							General_lstvHost.Items.Add(host.Text);
						General_cbxRandomHost.Checked = (bool)server.Nodes["RandomHost"].Tag;
						General_tbxPort.Text = server.Nodes["Port"].Tag.ToString();
						General_btnLauncherPath.Tag = (server.Nodes.ContainsKey("LauncherPath") ? server.Nodes["LauncherPath"].Tag : null);
						General_btnClientPath.Tag = (server.Nodes.ContainsKey("ClientPath") ? server.Nodes["ClientPath"].Tag : null);
						General_tbxVersion.Text = server.Nodes["Version"].Tag.ToString();
						General_tbxLocale.Text = server.Nodes["Locale"].Tag.ToString();
						General_tbxHWIDClientOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag).ToString("X4");
						General_cmbxHWIDClientSaveFrom.Text = (string)server.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
						General_tbxHWIDServerOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag).ToString("X4");
						General_cmbxHWIDServerSendTo.Text = (string)server.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
						General_rtbxHWIDdata.Text = (string)server.Nodes["HWID"].Nodes["Data"].Tag;
						General_cbxHWIDSendOnlyOnce.Checked = (bool)server.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
						if (Login_cmbxSilkroad.Enabled)
							this.EnableControl(General_btnAddSilkroad, true);
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
					{
						Login_cmbxServer.Tag = ushort.Parse(l.SelectedItems[0].Name);
						Login_cmbxServer.Text = l.SelectedItems[0].Text;
					}
					break;
				case "Login_lstvCharacters":
					if (l.SelectedItems.Count == 1)
					{
						Login_cmbxCharacter.Text = l.SelectedItems[0].Name;
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
					Login_cmbxServer.Tag = ushort.Parse(Login_lstvServers.Items[c.SelectedIndex].Name);
					Login_cmbxServer.Text = Login_lstvServers.Items[c.SelectedIndex].Text;
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
						Login_btnStart.Location = new Point(110, 14);
					}
					else
					{
						Login_btnLauncher.Visible = false;
						Login_btnStart.Location = new Point(110, 32);
					}
					break;
				case "General_cbxCreateChar":
				case "General_cbxCreateCharBelow40":
				case "General_cbxDeleteChar40to50":
				case "General_cbxLoadDefault":
				case "General_rbnPacketShow":
				case "General_rbnPacketNotShow":
					Settings.SaveBotSettings();
					break;
				case "General_cbxInjectEncrypted":
					if (General_cbxInjectEncrypted.Checked && General_cbxInjectMassive.Checked)
					{
						General_cbxInjectMassive.Checked = false;
					}
					break;
				case "General_cbxInjectMassive":
					if (General_cbxInjectMassive.Checked && General_cbxInjectEncrypted.Checked)
					{
						General_cbxInjectEncrypted.Checked = false;
					}
					break;
			}
		}
		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			switch (t.Name)
			{
				case "General_tbxSilkroadName":
					string silkroadkey = General_tbxSilkroadName.Text;
					if (CleanSilkroadName(ref silkroadkey))
					{
						EnableControl(General_btnPK2Path, true);
						General_btnPK2Path.Tag = silkroadkey;
						EnableControl(General_btnLauncherPath, true);
						EnableControl(General_btnClientPath, true);
					}
					else
					{
						EnableControl(General_btnPK2Path, false);
						General_btnPK2Path.Tag = null;
						EnableControl(General_btnLauncherPath, false);
						EnableControl(General_btnClientPath, false);
					}
					break;
			}
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
				case "Menu_lstvOpcodes_remove":
					if (General_lstvOpcodes.SelectedItems.Count == 1)
					{
						General_lstvOpcodes.SelectedItems[0].Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvOpcodes_removeAll":
					if (General_lstvOpcodes.Items.Count > 0)
					{
						General_lstvOpcodes.Items.Clear();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_rtbxPackets_scroll":
					if (t.Checked)
						General_rtbxPackets.TextChanged += new EventHandler(RichTextBox_TextChanged_AutoScroll);
					else
						General_rtbxPackets.TextChanged -= new EventHandler(RichTextBox_TextChanged_AutoScroll);
					break;
				case "Menu_rtbxPackets_clear":
					General_rtbxPackets.Clear();
					break;
				case "Menu_lstrSilkroads_remove":
					if (General_lstrSilkroads.SelectedNode != null && General_lstrSilkroads.SelectedNode.Parent == null)
					{
						if (!Login_cmbxSilkroad.Enabled && Login_cmbxSilkroad.Text == General_lstrSilkroads.SelectedNode.Name)
							return; // Is actually being used
						Login_cmbxSilkroad.Items.Remove(General_lstrSilkroads.SelectedNode.Name);
						General_lstrSilkroads.SelectedNode.Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvHost_remove":
					if (General_lstvHost.SelectedItems.Count == 1)
					{
						General_lstvHost.SelectedItems[0].Remove();
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
							if (Chat_cmbxMsgType.Text == "Private" && Chat_tbxMsgPlayer.Text != "")
								return;
							switch (Chat_cmbxMsgType.Text)
							{
								case "All":
									PacketBuilder.SendChatAll(Chat_tbxMsg.Text);
									break;
								case "Private":
									PacketBuilder.SendChatPrivate(Chat_tbxMsgPlayer.Text, Chat_tbxMsg.Text);
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
		public void Minimap_CharacterPointer_Move(float x, float y, float z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.MovePointer(" + ((int)x) + "," + ((int)y) + "," + ((int)z) + "," + region + ");", true);
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
		private void Window_Closing(object sender, FormClosingEventArgs e)
		{
			Cef.Shutdown();
		}
		public void TESTING_AddSpawn(SRObject entity)
		{
			string text = (string)entity[SRAttribute.Name];
			TreeNode node = new TreeNode();
			if(text != "")
				node.Text = text;
			else
				node.Text = (string)entity[SRAttribute.Servername];
			node.Name = ((uint)entity[SRAttribute.UniqueID]).ToString();
			foreach (string str in entity.ToStringArray())
				node.Nodes.Add(str);
			WinAPI.InvokeIfRequired(lstrTESTING, () => {
				lstrTESTING.Nodes.Add(node);
			});
		}
		public void TESTING_Clear()
		{
      WinAPI.InvokeIfRequired(lstrTESTING, () => {
				lstrTESTING.Nodes.Clear();
			});
		}
		public void TESTING_RemoveSpawn(uint uniqueID)
		{
			WinAPI.InvokeIfRequired(lstrTESTING, () => {
				lstrTESTING.Nodes[uniqueID.ToString()].Remove();
			});
		}
		private void TESTINGbtn_Click(object sender, EventArgs e)
		{
			Info.Get.SelectDatabase("JSRO.NET");
			Packet p = new Packet(0x3019, false, false, WinAPI.HexStringToBytes("73 07 00 00 22 00 00 00 2D 05 3B 0E 00 00 00 3C 0E 00 00 00 3D 0E 00 00 00 34 0E 00 00 00 47 0E 00 00 05 00 00 7E 5D 23 01 99 65 00 80 68 44 6F 14 CF C2 00 60 D7 44 39 B7 01 01 99 65 A2 03 99 FF BB 06 01 00 00 00 00 00 80 41 00 00 48 42 00 00 C8 42 01 8A 0F 00 00 82 25 03 00 07 00 4A 6F 63 6B 65 72 6F 00 01 00 01 01 5B 6B 23 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 8B 07 00 00 44 00 00 00 37 07 35 06 00 00 00 7D 06 00 00 00 59 06 00 00 00 C6 06 00 00 00 A1 06 00 00 00 E9 06 00 00 00 BA 00 00 00 00 05 00 00 B3 B9 23 01 99 65 45 42 6D 44 7F 18 CA C2 81 00 D9 44 09 91 01 01 99 65 B5 03 9B FF C8 06 01 00 00 00 9A 99 99 41 01 00 70 42 00 00 C8 42 01 8A 0F 00 00 6C 2A 07 00 0C 00 69 44 6F 6E 54 43 61 72 65 5F 78 44 00 01 00 01 01 21 BA 23 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 84 07 00 00 40 00 00 01 37 05 41 0E 00 00 00 42 0E 00 00 00 43 0E 00 00 00 30 0E 00 00 00 86 2A 00 00 00 05 00 00 98 F9 23 01 99 65 00 00 6D 44 0B 81 CE C2 00 A0 DD 44 1C 77 01 01 99 65 B4 03 99 FF ED 06 01 00 00 00 00 00 80 41 00 00 48 42 00 00 C8 42 01 8A 0F 00 00 08 DF 02 00 0A 00 53 75 6D 6D 65 72 54 69 6D 65 00 01 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 3C 5D 00 00 5B 6B 23 01 99 65 00 80 68 44 6F 14 CF C2 00 60 D7 44 39 B7 01 01 99 65 A2 03 99 FF BB 06 01 00 00 00 00 00 34 42 00 00 16 43 00 00 C8 42 00 00 8F 08 00 00 21 BA 23 01 99 65 45 42 6D 44 7F 18 CA C2 81 00 D9 44 09 91 01 01 99 65 B5 03 9B FF C8 06 01 00 00 00 00 00 34 42 00 00 B4 42 00 00 C8 42 00 00 FF FF FF FF 54 00 8A 0F 00 00 38 3E 24 01 99 65 00 C0 6E 44 0A B5 C1 C2 00 20 D1 44 00 00 FF FF FF FF 54 00 8A 0F 00 00 85 40 24 01 99 65 00 80 65 44 66 4E D3 C2 00 40 DD 44 00 00"));
			p.Lock();
			PacketParser.EntitySpawn(p);
			PacketParser.EntitySpawn(p);
			PacketParser.EntitySpawn(p);
			PacketParser.EntitySpawn(p);
			PacketParser.EntitySpawn(p);
			PacketParser.EntitySpawn(p);
			PacketParser.EntitySpawn(p);
		}
		public void TESTING_EditSpawn(SRObject entity,SRAttribute att)
		{
			WinAPI.InvokeIfRequired(lstrTESTING, ()=> {
				TreeNode n = lstrTESTING.Nodes[entity[SRAttribute.UniqueID].ToString()];
				if (n.Nodes.ContainsKey(att.ToString()))
					n.Nodes[att.ToString()].Text = "\"" + att + "\":" + entity[att];
				else
					n.Nodes.Add(att.ToString(), "\"" + att + "\":" + entity[att]);
			});
		}
		public void Minimap_ObjectPointer_Add(uint UniqueID, string servername, string htmlPopup, float x, float y, float z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.AddExtraPointer('" + UniqueID + "','" + servername + "','" + htmlPopup + "'," + ((int)x) + "," + ((int)y) + "," + ((int)z) + "," + region + ");", true);
			}
		}
		public void Minimap_ObjectPointer_Move(uint UniqueID, float x, float y, float z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.MoveExtraPointer('" + UniqueID + "'," + ((int)x) + "," + ((int)y) + "," + ((int)z) + "," + region + ");", true);
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