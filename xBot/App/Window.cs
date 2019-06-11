using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using xBot.Network;
using xBot.Network.Packets;
using CefSharp;
using CefSharp.WinForms;
using xBot.Game;
using SecurityAPI;
using System.Text.RegularExpressions;
using System.Text;

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
			InitializeChromium();
		}
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
				// Using fontName as TAG to be selected from WinForms
				c.Controls[i].Font = Fonts.Get.Load(c.Controls[i].Font, (string)c.Controls[i].Tag);
				InitializeFonts(c.Controls[i]);
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
		public void InitializeChromium()
		{
			string path = Directory.GetCurrentDirectory() + "\\Minimap\\index.html";
			if (File.Exists(path))
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
			setState();
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
				if (i + 1 < args.Length)
				{
					// Fill fields
					switch (args[i])
					{
						case "-username":
							Login_tbxUsername.Text = args[i];
							break;
						case "-password":
							Login_tbxPassword.Text = args[i];
							break;
						case "-server":
							Login_cmbxServer.Text = args[i];
							break;
						case "-charname":
							Login_cmbxCharacter.Text = args[i];
							break;
					}
				}
				else
				{
					// Checks
					switch (args[i])
					{
						case "--client":
							Login_rbnClient.Checked = true;
							break;
					}
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
		public void setState(string text = "Ready", ProcessState state = ProcessState.Default)
		{
			WinAPI.InvokeIfRequired(lblBotState, () => {
				lblBotState.Text = text;
				switch (state)
				{
					case ProcessState.Warning:
						setStateColor(Color.FromArgb(202, 81, 0));
						break;
					case ProcessState.Disconnected:
						setStateColor(Color.FromArgb(104, 33, 122));
						break;
					case ProcessState.Error:
						setStateColor(Color.Red);
						break;
					default:
						setStateColor(Color.FromArgb(0, 122, 204));
						break;
				}
			});
		}
		private void setStateColor(Color newColor)
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
					if (rtbxLogs.Lines.Length > 256)
						rtbxLogs.Text = rtbxLogs.Text.Substring(rtbxLogs.Text.IndexOf('\n'));
					rtbxLogs.Text += "\n" + WinAPI.getDate() + " " + text;
				});
			}
			catch { }
		}
		public void LogPacket(string packet)
		{
			try
			{
				WinAPI.InvokeIfRequired(General_rtbxPackets, () => {
					if (General_rtbxPackets.Lines.Length > 1024)
						General_rtbxPackets.Text = General_rtbxPackets.Text.Substring(General_rtbxPackets.Text.IndexOf('\n'));
					General_rtbxPackets.AppendText(packet);
				});
			}
			catch { }
		}
		public void LogChatMessage(RichTextBox chat, string player, string message)
		{
			try
			{
				WinAPI.InvokeIfRequired(chat, () => {
					if (chat.Lines.Length > 1024)
						chat.Text = chat.Text.Substring(chat.Text.IndexOf('\n'));
					if (player != "")
						player += ":";
					chat.Text += player + message + "\n";
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
							// Check if database has been generated previously
							if (!Info.Get.SelectDatabase(Login_cmbxSilkroad.Text))
							{
								MessageBox.Show(this, "The database \"" + General_tbxSilkroadName.Text + "\" needs to be created.", "xBot", MessageBoxButtons.OK);
								Control_Click(General_btnPK2Path, null);
								return;
							}

							c.Text = "STOP";
							// Lock Silkroad selection
							Login_cmbxSilkroad.Enabled = false;
							General_btnAddSilkroad.Font = new Font(General_btnAddSilkroad.Font, FontStyle.Strikeout);

							TreeNode silkroad = General_lstrSilkroads.Nodes[Login_cmbxSilkroad.Text];
							// Clientless check
							if (Login_rbnClientless.Checked)
							{
								Info.Get.Locale = byte.Parse(silkroad.Nodes["Locale"].Tag.ToString());
								Info.Get.Version = uint.Parse(silkroad.Nodes["Version"].Tag.ToString());
							}

							// Add possibles Gateways/Ports
							List<string> hosts = new List<string>();
							foreach (TreeNode host in silkroad.Nodes["Hosts"].Nodes)
								hosts.Add(host.Text);
							List<ushort> ports = new List<ushort>();
							ports.Add((ushort)silkroad.Nodes["Port"].Tag);
							if (hosts.Count == 0 || ports.Count == 0)
								return;// Just in case

							// HWID Setup
							ushort clientOp = (ushort)silkroad.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag;
							ushort serverOp = (ushort)silkroad.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag;
							string saveFrom = (string)silkroad.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
							string sendTo = (string)silkroad.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
							bool sendOnlyOnce = (bool)silkroad.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
							Bot.Get.SetHWID(clientOp, saveFrom, serverOp, sendTo, sendOnlyOnce);

							// Creating Proxy
							Bot.Get.Proxy = new Proxy(Login_rbnClientless.Checked, hosts, ports);
							Bot.Get.Proxy.RandomHost = (bool)silkroad.Nodes["RandomHost"].Tag;
							Bot.Get.Proxy.Start();
							break;
						case "STOP":
							// Lock Silkroad selection
							Login_cmbxSilkroad.Enabled = true;
							General_btnAddSilkroad.Font = new Font(General_btnAddSilkroad.Font, FontStyle.Regular);
							c.Text = "START";
							Bot.Get.Proxy.Stop();
							break;
						case "LOGIN":
							if (Login_tbxUsername.Text == "" || Login_tbxPassword.Text == "" || Login_cmbxServer.Text == "")
							{
								return;
							}
							c.Font = new Font(c.Font, FontStyle.Strikeout);
							setState("Login...");
							PacketBuilder.Login(Login_tbxUsername.Text, Login_tbxPassword.Text, (int)Login_cmbxServer.Tag);
							break;
						case "SELECT":
							if (Login_cmbxCharacter.Text == "")
								return;
							c.Font = new Font(c.Font, FontStyle.Strikeout);
							PacketBuilder.SelectCharacter(Login_cmbxCharacter.Text);
							break;
					}
					break;
				case "Login_btnLoader":
					if (File.Exists("edxSilkroadLoader6.exe"))
					{
						Process.Start("edxSilkroadLoader6.exe");
					}
					else
					{
						MessageBox.Show(this, "edxSilkroadLoader6 cannot be found at bot folder");
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
										data = WinAPI.HexStringToBytes(General_tbxInjectData.Text);
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
								LogPacket("Packet injected > [Opcode] 0x" + opcode.ToString("X4") + " [Encrypted] " + General_cbxInjectEncrypted.Checked + " [Massive] ");
								LogPacket("[DATA] " + WinAPI.BytesToHexString(data));
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
						General_tbxLocale.Text = server.Nodes["Locale"].Tag.ToString();
						General_tbxVersion.Text = server.Nodes["Version"].Tag.ToString();
						General_tbxHWIDClientOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag).ToString("X4");
						General_cmbxHWIDClientSaveFrom.Text = (string)server.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
						General_tbxHWIDServerOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag).ToString("X4");
						General_cmbxHWIDServerSendTo.Text = (string)server.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
						General_rtbxHWIDdata.Text = (string)server.Nodes["HWID"].Nodes["Data"].Tag;
						General_cbxHWIDSendOnlyOnce.Checked = (bool)server.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
						General_btnAddSilkroad.Font = new Font(General_btnAddSilkroad.Font, FontStyle.Regular);
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
						Login_cmbxServer.Tag = int.Parse(l.SelectedItems[0].SubItems[3].Text);
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
					Login_cmbxServer.Tag = int.Parse(Login_lstvServers.Items[c.SelectedIndex].SubItems[3].Text);
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
						Login_btnLoader.Visible = true;
						Login_btnStart.Location = new Point(110, 14);
					}
					else
					{
						Login_btnLoader.Visible = false;
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
						General_btnPK2Path.Font = new Font(General_btnPK2Path.Font, FontStyle.Regular);
						General_btnPK2Path.Tag = silkroadkey;
					}
					else
					{
						General_btnPK2Path.Font = new Font(General_btnPK2Path.Font, FontStyle.Strikeout);
						General_btnPK2Path.Tag = null;
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
						TreeNode server = General_lstrSilkroads.SelectedNode;
						while (server.Parent != null)
							server = server.Parent;
						Login_cmbxSilkroad.Items.Remove(server.Name);
						server.Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvHost_remove":
					if (General_lstvHost.SelectedItems.Count == 1)
					{
						General_lstvHost.SelectedItems[0].Remove();
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
		public void Minimap_ObjectPointer_Add(uint UniqueID, string type, string htmlPopup, float x, float y, float z, ushort region)
		{
			if (Minimap_wbrChromeMap != null)
			{
				Minimap_wbrChromeMap.ExecuteScriptAsyncWhenPageLoaded("SilkroadMap.AddExtraPointer('" + UniqueID + "','" + type + "','" + htmlPopup + "'," + ((int)x) + "," + ((int)y) + "," + ((int)z) + "," + region + ");", true);
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
			TreeNode node = new TreeNode((string)entity[SRAttribute.Name]);
			node.Name = entity[SRAttribute.UniqueID].ToString();
			foreach (string str in entity.ToArray())
				node.Nodes.Add(str);
			WinAPI.InvokeIfRequired(lstrTESTING, () => {
				lstrTESTING.Nodes.Add(node);
			});
		}
		public void TESTING_RemoveSpawn(uint uniqueID)
		{
			WinAPI.InvokeIfRequired(lstrTESTING, () => {
				lstrTESTING.Nodes[uniqueID.ToString()].Remove();
			});
		}
		int ii = 1;
		private void button2_Click(object sender, EventArgs e)
		{
			if (button2.Tag == null)
			{
				button2.Tag = "  ";
				Info.Get.SelectDatabase("ELECTUS");
			}
			lstrTESTING.Nodes.Clear();
			Packet p = new Packet(0x3019);
			p.WriteUInt8Array(WinAPI.HexStringToBytes("73 07 00 00 22 00 00 01 6D 08 1F 01 00 00 00 8B 01 00 00 00 67 01 00 00 00 D3 01 00 00 00 AF 01 00 00 00 F7 01 00 00 00 47 00 00 00 00 86 2A 00 00 00 05 00 00 28 11 00 00 A8 61 00 80 65 44 7F 6F 02 C2 00 00 82 44 C8 7D 00 01 00 C8 7D 01 00 00 04 00 00 80 41 00 00 48 42 00 00 C8 42 00 09 00 5B 42 4F 54 5D 46 61 69 6C 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 88 07 00 00 22 02 00 00 6D 07 94 14 00 00 07 00 15 00 00 07 DC 14 00 00 07 48 15 00 00 07 24 15 00 00 07 6C 15 00 00 07 C0 23 01 00 09 05 05 CD 5E 00 00 00 C7 5E 00 00 00 2A 5F 00 00 00 9F 24 00 00 00 3F CE 00 00 02 00 77 D9 6A 00 A8 61 C3 7D 6E 44 7E 6F 02 C2 5A 07 85 44 27 C6 01 01 A8 61 B9 03 E0 FF 28 04 01 00 00 00 CD CC 8C 41 00 00 5C 42 00 00 C8 42 03 51 0D 00 00 0D 33 05 00 24 15 00 00 D4 E5 05 00 2C 1F 00 00 67 24 05 00 07 00 52 49 56 41 4C 53 53 01 01 00 00 00 00 00 00 0B 00 46 65 61 52 5F 4F 72 5F 44 69 65 82 02 00 00 07 00 5F 5F 42 52 4E 5F 5F 01 00 00 00 72 00 00 00 01 00 00 00 01 00 00 FF 76 07 00 00 33 00 00 00 6D 08 97 11 00 00 03 03 12 00 00 03 E2 11 00 00 03 4E 12 00 00 03 27 12 00 00 03 72 12 00 00 03 C6 0F 00 00 03 7A 10 00 00 03 05 00 00 1B A3 7E 00 A8 61 00 00 69 44 7E 6F 02 C2 00 00 9E 44 EA A6 01 01 A8 61 A4 03 E0 FF F0 04 01 00 00 00 CD CC 0C 42 00 00 DC 42 00 00 C8 42 01 49 85 00 00 FD 1D 07 00 09 00 5A 65 65 76 52 65 76 61 68 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 23 3A 00 00 00 22 00 00 6D 07 7D 32 00 00 07 D0 F8 00 00 07 CF F8 00 00 07 D2 F8 00 00 07 D1 F8 00 00 07 55 33 00 00 07 B0 13 01 00 08 05 05 CB 64 00 00 00 D5 64 00 00 00 9E 97 00 00 00 78 25 01 00 00 55 5F 00 00 03 00 2B D2 7F 00 A8 61 AC 9F 66 44 7E 6F 02 C2 2B B2 87 44 BD 60 01 01 A8 61 0D 03 E0 FF C6 04 01 00 03 00 55 E3 3D 42 9A 59 14 43 00 00 C8 42 05 24 15 00 00 E7 49 02 00 66 84 00 00 B8 46 02 00 61 84 00 00 BA D8 00 00 62 84 00 00 7C 28 00 00 63 84 00 00 54 F5 01 00 06 00 4B 6F 62 61 69 6E 02 05 00 00 00 00 00 00 05 00 4F 6D 65 67 61 8C 00 00 00 0C 00 47 65 74 54 72 69 67 67 65 72 65 64 00 00 00 00 0B 00 00 00 01 00 00 00 01 02 00 FF".Replace(" ","")));
			p.Lock();
			for (int i=0;i< ii; i++)
			{
				PacketParser.EntitySpawn(p);
			}
			ii++;
    }
		public void TESTING_EditSpawn(SRObject entity, SRAttribute att)
		{
			WinAPI.InvokeIfRequired(lstrTESTING, () => {
				TreeNode n = lstrTESTING.Nodes["" + (uint)entity[SRAttribute.UniqueID]];
				if (n.Nodes.ContainsKey(att.ToString()))
					n.Nodes[att.ToString()].Text = "\"" + att + "\":" + entity[att];
				else
					n.Nodes.Add(att.ToString(), "\"" + att + "\":" + entity[att]);
			});
		}
	}
}