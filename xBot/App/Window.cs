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
		public enum BotState
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
		public static void InvokeIfRequired(Control control, MethodInvoker action)
		{
			if (control.InvokeRequired)
				control.Invoke(action);
			else
				action();
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
			rtbxLogs.Text = WindowsAPI.getDate() + " Welcome to " + ProductName + " v" + ProductVersion + " | Made by Engels \"JellyBitz\" Quintero\n";
			rtbxLogs.Text += WindowsAPI.getDate() + " Discord : JellyBitz#7643";
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
		private void Window_Drag(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WindowsAPI.ReleaseCapture();
				// WM_NCLBUTTONDOWN, HT_CAPTION
				WindowsAPI.SendMessage(Handle, 0xA1, 0x2, 0);
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
		public void setState(string text = "Ready", BotState state = BotState.Default)
		{
			InvokeIfRequired(lblBotState, () => {
				lblBotState.Text = text;
				switch (state)
				{
					case BotState.Warning:
						setStateColor(Color.FromArgb(202, 81, 0));
						break;
					case BotState.Disconnected:
						setStateColor(Color.FromArgb(104, 33, 122));
						break;
					case BotState.Error:
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
			InvokeIfRequired(this, () => {
				BackColor = newColor;
			});
		}
		public void Log(string text)
		{
			try
			{
				InvokeIfRequired(rtbxLogs, () => {
					if (rtbxLogs.Lines.Length > 256)
						rtbxLogs.Text = rtbxLogs.Text.Substring(rtbxLogs.Text.IndexOf('\n'));
					rtbxLogs.Text += "\n" + WindowsAPI.getDate() + " " + text;
				});
			}
			catch { }
		}
		public void LogPacket(string packet)
		{
			try
			{
				InvokeIfRequired(General_rtbxPackets, () => {
					if (General_rtbxPackets.Lines.Length > 1024)
						General_rtbxPackets.Text = General_rtbxPackets.Text.Substring(General_rtbxPackets.Text.IndexOf('\n'));
					General_rtbxPackets.Text += packet;
				});
			}
			catch { }
		}
		public void LogChatMessage(RichTextBox chat, string player, string message)
		{
			try
			{
				InvokeIfRequired(chat, () => {
					if (chat.Lines.Length > 1024)
						chat.Text = chat.Text.Substring(chat.Text.IndexOf('\n'));
					if (player != "")
						player += ":";
					chat.Text += player + message + "\n";
				});
			}
			catch { }
		}
		private void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Font.Strikeout)
			{
				// Control is disabled
				return;
			}
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
							c.Text = "STOP";
							// Lock Silkroad selection
							Login_cmbxSilkroad.Enabled = false;
							General_btnAddSilkroad.Font = new Font(General_btnAddSilkroad.Font, FontStyle.Strikeout);

							TreeNode silkroad = General_lstrSilkroads.Nodes[Login_cmbxSilkroad.Text];
							// Clientless check
							if (Login_rbnClientless.Checked)
							{
								Data.Get.Locale = byte.Parse(silkroad.Nodes["Locale"].Tag.ToString());
								Data.Get.Version = uint.Parse(silkroad.Nodes["Version"].Tag.ToString());
							}
							// Add possibles Gateways/Ports
							List<string> hosts = new List<string>();
							foreach (TreeNode host in silkroad.Nodes["Hosts"].Nodes)
								hosts.Add(host.Text);
							List<ushort> ports = new List<ushort>();
							foreach (TreeNode port in silkroad.Nodes["Ports"].Nodes)
								ports.Add((ushort)port.Tag);
							if (hosts.Count == 0 || ports.Count == 0)
								return;
							ushort clientOp = (ushort)silkroad.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag;
							ushort serverOp = (ushort)silkroad.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag;
							string saveFrom = (string)silkroad.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
							string sendTo = (string)silkroad.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
							bool sendOnlyOnce = (bool)silkroad.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
							Bot.Get.setHWID(clientOp, saveFrom, serverOp, sendTo, sendOnlyOnce, (string)silkroad.Nodes["HWID"].Nodes["Data"].Tag);
							// Creating Proxy
							Bot.Get.Proxy = new Proxy(Login_rbnClientless.Checked, hosts, ports);
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
							{
								return;
							}
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
					Process sro_client = WindowsAPI.getSROCientProcess();
					if (sro_client != null)
					{
						IntPtr[] clientWindows = WindowsAPI.GetProcessWindows(sro_client.Id);
						// DodgerBlue = Visible, RoyalBlue = Hiden
						if (btnShowHideClient.ForeColor == Color.DodgerBlue)
						{
							foreach (IntPtr p in clientWindows)
							{
								WindowsAPI.ShowWindow(p, WindowsAPI.SW_HIDE);
								WindowsAPI.SetForegroundWindow(p);
							}
							btnShowHideClient.ForeColor = Color.RoyalBlue;
						}
						else
						{
							foreach (IntPtr p in clientWindows)
							{
								WindowsAPI.ShowWindow(p, WindowsAPI.SW_SHOW);
								WindowsAPI.SetForegroundWindow(p);
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
					using (OpenFileDialog openFileDialog = new OpenFileDialog())
					{
						openFileDialog.Multiselect = false;
						openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
						openFileDialog.ValidateNames = true;
						openFileDialog.Title = "Select your media .pk2 file";
						openFileDialog.FileName = "Media.pk2 file";
						openFileDialog.Filter = "pk2 file (*.pk2)|*.pk2|All files (*.*)|*.*";
						openFileDialog.FilterIndex = 0;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
						{
							General_btnPK2Path.Tag = openFileDialog.FileName;
							
              Log(openFileDialog.FileName);


						}
					}
					break;
				case "General_btnAddHost":
					if (General_tbxHost.Text != "")
					{
						General_lstvHost.Items.Add(General_tbxHost.Text.ToLower());
						General_tbxHost.Text = "";
					}
					break;
				case "General_btnAddPort":
					if (General_tbxPort.Text != "")
					{
						ushort port;
						if (ushort.TryParse(General_tbxPort.Text, out port))
						{
							ListViewItem p = new ListViewItem(General_tbxPort.Text);
							p.Tag = port;
							General_lstvPort.Items.Add(p);

							General_tbxPort.Text = "";
						}
					}
					break;
				case "General_btnAddSilkroad":
					if (General_tbxSilkroadName.Text != "")
					{
						string serverKey = General_tbxSilkroadName.Text.Trim().ToUpper();
						if (!Regex.IsMatch(serverKey, @"[\w ._-]+[\w]+"))
							return;
						byte locale;
						if (General_tbxLocale.Text == "" || !byte.TryParse(General_tbxLocale.Text, out locale))
							return;
						uint version;
						if (General_tbxVersion.Text == "" || !uint.TryParse(General_tbxVersion.Text, out version))
							return;
						if (General_lstvHost.Items.Count == 0 || General_lstvPort.Items.Count == 0)
							return;
						ushort hwidClientOp = 0;
						if (General_tbxHWIDClientOp.Text != "" && !ushort.TryParse(General_tbxHWIDClientOp.Text, System.Globalization.NumberStyles.HexNumber, null, out hwidClientOp))
							return;
						ushort hwidServerOp = 0;
						if (General_tbxHWIDServerOp.Text != "" && !ushort.TryParse(General_tbxHWIDServerOp.Text, System.Globalization.NumberStyles.HexNumber, null, out hwidServerOp))
							return;
						// Genearting the whole server node
						TreeNode server = new TreeNode(serverKey);
						server.Name = serverKey;

						TreeNode node = new TreeNode("Hosts");
						node.Name = "Hosts";
						foreach (ListViewItem host in General_lstvHost.Items)
							node.Nodes.Add(host.Text);
						server.Nodes.Add(node);
						node = new TreeNode("Ports");
						node.Name = "Ports";
						foreach (ListViewItem port in General_lstvPort.Items)
						{
							TreeNode p = new TreeNode(port.Text);
							p.Tag = port.Tag;
							node.Nodes.Add(p);
						}
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

						node = new TreeNode("Data : " + (General_rtbxHWIDdata.Text == "" ? "None" : General_rtbxHWIDdata.Text));
						node.Name = "Data";
						node.Tag = General_rtbxHWIDdata.Text;
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
										data = WindowsAPI.HexStringToBytes(General_tbxInjectData.Text);
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
								LogPacket("[DATA] " + WindowsAPI.BytesToHexString(data));
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
						{
							General_lstvHost.Items.Add(host.Text);
						}
						General_lstvPort.Items.Clear();
						foreach (TreeNode port in server.Nodes["Ports"].Nodes)
						{
							ListViewItem p = new ListViewItem(port.Text);
							p.Tag = port.Tag;
							General_lstvPort.Items.Add(p);
						}
						General_tbxLocale.Text = server.Nodes["Locale"].Tag.ToString();
						General_tbxVersion.Text = server.Nodes["Version"].Tag.ToString();
						General_tbxHWIDClientOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag).ToString("X4");
						General_cmbxHWIDClientSaveFrom.Text = (string)server.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;
						General_tbxHWIDServerOp.Text = ((ushort)server.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag).ToString("X4");
						General_cmbxHWIDClientSaveFrom.Text = (string)server.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;
						General_rtbxHWIDdata.Text = (string)server.Nodes["HWID"].Nodes["Data"].Tag;
						General_cbxHWIDSendOnlyOnce.Checked = (bool)server.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
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
		private void RichTextBox_TextChanged_AutoScroll(object sender, EventArgs e)
		{
			RichTextBox r = (RichTextBox)sender;
			r.SelectionStart = r.Text.Length;
			r.ScrollToCaret();
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
					if (General_lstrSilkroads.SelectedNode != null)
					{
						TreeNode server = General_lstrSilkroads.SelectedNode;
						while (server.Parent != null)
							server = server.Parent;
						this.Login_cmbxSilkroad.Items.Remove(server.Name);
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
				case "Menu_lstvPort_remove":
					if (General_lstvPort.SelectedItems.Count == 1)
					{
						General_lstvPort.SelectedItems[0].Remove();
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
		private void Window_Closing(object sender, FormClosingEventArgs e)
		{
			Cef.Shutdown();
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
		public void TEST_ADD_SPAWN(Game.SRObject entity)
		{
			/////////// TEST /////////
			TreeNode node = new TreeNode("" + (uint)entity[Game.SRAttribute.UniqueID]);
			node.Name = node.Text;
			foreach (string str in entity.ToArray())
			{
				node.Nodes.Add(str);
			}
			InvokeIfRequired(treeSpawnsTEST, () => {
				treeSpawnsTEST.Nodes.Add(node);
			});
			///////////////////////////
		}
		public void TEST_REM_SPAWN(uint uniqueID)
		{
			/////////// TEST /////////
			Window.InvokeIfRequired(Window.Get.treeSpawnsTEST, () => {
				Window.Get.treeSpawnsTEST.Nodes[uniqueID.ToString()].Remove();
			});
			//////////////////////////
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			switch (t.Name)
			{
				case "General_tbxSilkroadName":
					if (General_tbxSilkroadName.Text == "")
						General_btnPK2Path.Font = new Font(General_btnPK2Path.Font,FontStyle.Strikeout);
					else
						General_btnPK2Path.Font = new Font(General_btnPK2Path.Font, FontStyle.Regular);
					break;
			}
		}

		internal void TEST_EDIT_SPAWN(Game.SRObject entity, SRAttribute att)
		{
			InvokeIfRequired(treeSpawnsTEST, () => {
				TreeNode n = treeSpawnsTEST.Nodes["" + (uint)entity[SRAttribute.UniqueID]];
				if (n.Nodes.ContainsKey(att.ToString()))
				{
					n.Nodes[att.ToString()].Text = "\"" + att + "\":" + entity[att];
				}
				else
				{
					n.Nodes.Add(att.ToString(), "\"" + att + "\":" + entity[att]);
				}
			});
		}
	}
}