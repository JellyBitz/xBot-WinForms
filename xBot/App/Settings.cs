using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace xBot
{
	public static class Settings{
		/// <summary>
		/// Used to not overload loading & saving, keeping it safe!
		/// </summary>
		private static bool _usingBotSettings;
		public static void SaveBotSettings()
		{
			if (!_usingBotSettings)
			{
				_usingBotSettings = true;
				JObject root = new JObject();
				// Generating/editing nodes manually
				Window w = Window.Get;
				root["_xBot"] = "ProjexNET | Easy & Flexible. Design perfection!";
				root["_Version"] = w.ProductVersion;
				root["_ContactMe"] = "Engels [JellyBitz] Quintero | Discord: JellyBitz#7643";

				#region (Server Tab)
				JObject serverlist = new JObject();
				root["Silkroad"] = serverlist;

				foreach (TreeNode node in w.General_lstrSilkroads.Nodes)
				{
					JObject server = new JObject();
					serverlist[node.Name] = server;
					
					JArray hosts = new JArray();
					foreach (TreeNode host in node.Nodes["Hosts"].Nodes)
						hosts.Add(host.Text);
					server["Hosts"] = hosts;
					server["RandomHost"] = (bool)node.Nodes["RandomHost"].Tag;
					server["Port"] = (ushort)node.Nodes["Port"].Tag;
					if (node.Nodes.ContainsKey("LauncherPath"))
						server["LauncherPath"] = (string)node.Nodes["LauncherPath"].Tag;
					if (node.Nodes.ContainsKey("ClientPath"))
						server["ClientPath"] = (string)node.Nodes["ClientPath"].Tag;
					server["Version"] = (uint)node.Nodes["Version"].Tag;
					server["Locale"] = (byte)node.Nodes["Locale"].Tag;
					
					JObject hwid = new JObject();
					server["HWID"] = hwid;

					JObject hwidclient = new JObject();
					hwid["Client"] = hwidclient;
					hwidclient["Opcode"] = ((ushort)node.Nodes["HWID"].Nodes["Client"].Nodes["Opcode"].Tag).ToString("X4");
					hwidclient["SaveFrom"] = (string)node.Nodes["HWID"].Nodes["Client"].Nodes["SaveFrom"].Tag;

					JObject hwidserver = new JObject();
					hwid["Server"] = hwidserver;
					hwidserver["Opcode"] = ((ushort)node.Nodes["HWID"].Nodes["Server"].Nodes["Opcode"].Tag).ToString("X4");
					hwidserver["SendTo"] = (string)node.Nodes["HWID"].Nodes["Server"].Nodes["SendTo"].Tag;

					hwid["SendOnlyOnce"] = (bool)node.Nodes["HWID"].Nodes["SendOnlyOnce"].Tag;
				}
				#endregion

				#region (AutoMagically Tab)
				JObject autoMagic = new JObject();
				root["AutoMagically"] = autoMagic;
				autoMagic["CreateCharacter"] = w.General_cbxCreateChar.Checked;
				autoMagic["CreateCharacterBelow40"] = w.General_cbxCreateCharBelow40.Checked;
				autoMagic["DeleteCharacterFrom40To50"] = w.General_cbxDeleteChar40to50.Checked;
				autoMagic["LoadDefaultConfigs"] = w.General_cbxLoadDefault.Checked;
				#endregion

				JObject packetAnalyzer = new JObject();
				root["PacketAnalyzer"] = packetAnalyzer;

				JArray opcodes = new JArray();
				WinAPI.InvokeIfRequired(w.General_lstvOpcodes, ()=> {
					foreach (ListViewItem opcode in w.General_lstvOpcodes.Items)
						opcodes.Add(opcode.Text);
				});
				packetAnalyzer["Filter"] = opcodes;
				packetAnalyzer["FilterOnlyShow"] = w.General_rbnPacketOnlyShow.Checked;

				// Saving
				File.WriteAllText("Settings.json", root.ToString());
				_usingBotSettings = false;
      }
		}
		public static void LoadBotSettings()
		{
			// Load configs or save the current
			if (File.Exists("Settings.json"))
			{
				Window w = Window.Get;
				_usingBotSettings = true;
				JObject root = JObject.Parse(File.ReadAllText("Settings.json"));
				if (root.ContainsKey("Silkroad"))
				{
					JObject silkroadList = (JObject)root["Silkroad"];
					foreach (JProperty key in silkroadList.Properties())
					{
						JObject silkroad = (JObject)silkroadList[key.Name];
						TreeNode s = new TreeNode(key.Name);
						s.Name = key.Name;

						TreeNode node = new TreeNode("Hosts");
						node.Name = "Hosts";
						foreach (JToken host in (JArray)silkroad["Hosts"]) {
							node.Nodes.Add(host.ToString());
            }
						s.Nodes.Add(node);
						node = new TreeNode("Use random host : " + ((bool)silkroad["Port"]?"Yes":"No"));
            node.Name = "RandomHost";
						node.Tag = (bool)silkroad["RandomHost"];
            s.Nodes.Add(node);

						node = new TreeNode("Port : " + silkroad["Port"]);
						node.Name = "Port";
						node.Tag = (ushort)silkroad["Port"];
						s.Nodes.Add(node);

						if (silkroad.ContainsKey("LauncherPath"))
						{
							node = new TreeNode("Launcher Path : " + silkroad["LauncherPath"]);
							node.Name = "LauncherPath";
							node.Tag = (string)silkroad["LauncherPath"];
							s.Nodes.Add(node);
						}
						if (silkroad.ContainsKey("ClientPath"))
						{
							node = new TreeNode("Client Path : " + silkroad["ClientPath"]);
							node.Name = "ClientPath";
							node.Tag = (string)silkroad["ClientPath"];
							s.Nodes.Add(node);
						}

						node = new TreeNode("Version : " + silkroad["Version"]);
						node.Name = "Version";
						node.Tag = (uint)silkroad["Version"];
						s.Nodes.Add(node);

						node = new TreeNode("Locale : "+ silkroad["Locale"]);
						node.Name = "Locale";
						node.Tag = (byte)silkroad["Locale"];
            s.Nodes.Add(node);

						ushort hwidClientOp = ushort.Parse((string)silkroad["HWID"]["Client"]["Opcode"], System.Globalization.NumberStyles.HexNumber);
						ushort hwidServerOp = ushort.Parse((string)silkroad["HWID"]["Server"]["Opcode"],System.Globalization.NumberStyles.HexNumber);
						TreeNode hwid = new TreeNode("HWID Setup (" + (hwidClientOp == 0 && hwidServerOp == 0 ? "Off" : "On") + ")");
						hwid.Name = "HWID";

						TreeNode hwidclient = new TreeNode("Client");
						hwidclient.Name = "Client";
						hwid.Nodes.Add(hwidclient);
						node = new TreeNode("Opcode : " + (hwidClientOp == 0 ? "None" : "0x"+hwidClientOp.ToString("X4")));
						node.Name = "Opcode";
            node.Tag = hwidClientOp;
						hwidclient.Nodes.Add(node);
						node = new TreeNode("Save from : " + silkroad["HWID"]["Client"]["SaveFrom"].ToString());
						node.Name = "SaveFrom";
						node.Tag = (string)silkroad["HWID"]["Client"]["SaveFrom"];
						hwidclient.Nodes.Add(node);

						TreeNode hwidserver = new TreeNode("Server");
						hwidserver.Name = "Server";
						hwid.Nodes.Add(hwidserver);
						node = new TreeNode("Opcode : " + (hwidServerOp == 0 ? "None" : "0x"+hwidServerOp.ToString("X4")));
						node.Name = "Opcode";
						node.Tag = hwidServerOp;
						hwidserver.Nodes.Add(node);
						node = new TreeNode("Send to : " + silkroad["HWID"]["Server"]["SendTo"].ToString());
						node.Name = "SendTo";
						node.Tag = (string)silkroad["HWID"]["Server"]["SendTo"];
						hwidserver.Nodes.Add(node);

						string hwidData = "";
            if (File.Exists("Data\\"+ key.Name + ".hwid"))
							hwidData = WinAPI.BytesToHexString(File.ReadAllBytes("Data\\" + key.Name + ".hwid"));
						node = new TreeNode("Data : " + (hwidData == ""? "None": hwidData));
						node.Name = "Data";
						node.Tag = hwidData;
						hwid.Nodes.Add(node);
						node = new TreeNode("Send Data only once : " + (bool.Parse(silkroad["HWID"]["SendOnlyOnce"].ToString()) ? "Yes" : "No"));
						node.Name = "SendOnlyOnce";
						node.Tag = (bool)silkroad["HWID"]["SendOnlyOnce"];
						hwid.Nodes.Add(node);

						s.Nodes.Add(hwid);

						Window.Get.General_lstrSilkroads.Nodes.Add(s);
						Window.Get.Login_cmbxSilkroad.Items.Add(s.Name);
					}
				}
				if (root.ContainsKey("AutoMagically")) {
					JObject autoMagic = (JObject)root["AutoMagically"];
					try
					{
						w.General_cbxCreateChar.Checked = (bool)autoMagic["CreateCharacter"];
					}
					catch { }
					try
					{
						w.General_cbxCreateCharBelow40.Checked = (bool)autoMagic["CreateCharacterBelow40"];
					}
					catch { }
					try
					{
						w.General_cbxDeleteChar40to50.Checked = (bool)autoMagic["DeleteCharacterFrom40To50"];
					}
					catch { }
					try
					{
						w.General_cbxLoadDefault.Checked = (bool)autoMagic["LoadDefaultConfigs"];
					}
					catch { }
				}
				if (root.ContainsKey("PacketAnalyzer"))
				{
					JToken packetAnalyzer = root["PacketAnalyzer"];
					try
					{
						foreach (JToken opcode in (JArray)packetAnalyzer["Filter"])
						{
							ListViewItem item = new ListViewItem(opcode.ToString());
							item.Name = item.Text;
							w.General_lstvOpcodes.Items.Add(item);
						}
					}
					catch { }
					try
					{
						w.General_rbnPacketOnlyShow.Checked = (bool)packetAnalyzer["FilterOnlyShow"];
					}
					catch { }
				}
				_usingBotSettings = false;
      }
			else
			{
				SaveBotSettings();
			}
		}
		public static void SaveCharacterSettings()
		{

		}
		public static void LoadCharacterSettings(string silkroad,string server,string charname)
		{

		}
	}
}