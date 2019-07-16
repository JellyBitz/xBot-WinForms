using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;
using xBot.Game;

namespace xBot
{
	public static class Settings{
		private static readonly object BotSettingsLock = new object();
		private static bool isLoading = true; // Avoid loading overload
		private static readonly object CharacterSettingsLock = new object();
		public static void SaveBotSettings()
		{
			if (!isLoading)
			{
				lock (BotSettingsLock)
				{
					JObject root = new JObject();
					// Generating/editing nodes manually
					Window w = Window.Get;

					#region (Silkroad Tab)
					JObject silkroadList = new JObject();
					root["Silkroad"] = silkroadList;

					foreach (TreeNode node in w.Settings_lstrSilkroads.Nodes)
					{
						JObject server = new JObject();
						silkroadList[node.Name] = server;

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

					autoMagic["SelectFirstCharacter"] = w.Settings_cbxSelectFirstChar.Checked;
					autoMagic["CreateCharacter"] = w.Settings_cbxCreateChar.Checked;
					autoMagic["CreateCharacterBelow40"] = w.Settings_cbxCreateCharBelow40.Checked;
					autoMagic["CreateCustomName"] = w.Settings_tbxCustomName.Text;
					autoMagic["CreateCustomSequence"] = w.Settings_tbxCustomSequence.Text;
					autoMagic["CreateCustomRace"] = w.Settings_cmbxCreateCharRace.Text;
					autoMagic["CreateCustomGenre"] = w.Settings_cmbxCreateCharGenre.Text;
					autoMagic["DeleteCharacterFrom40To50"] = w.Settings_cbxDeleteChar40to50.Checked;
					autoMagic["LoadDefaultConfigs"] = w.Settings_cbxLoadDefaultConfigs.Checked;
					#endregion

					#region (Packet Analyzer Tab)
					JObject packetAnalyzer = new JObject();
					root["PacketAnalyzer"] = packetAnalyzer;

					JArray opcodes = new JArray();
					WinAPI.InvokeIfRequired(w.Settings_lstvOpcodes, () => {
						foreach (ListViewItem opcode in w.Settings_lstvOpcodes.Items)
							opcodes.Add(opcode.Text);
					});

					packetAnalyzer["Filter"] = opcodes;
					packetAnalyzer["FilterOnlyShow"] = w.Settings_rbnPacketOnlyShow.Checked;
					#endregion

					// File info
					root["_xBot"] = "ProjexNET | Easy & Flexible. Design perfection!";
					root["_Version"] = w.ProductVersion;
					root["_ContactMe"] = "Engels [JellyBitz] Quintero | Discord: JellyBitz#7643";
					// Saving
					File.WriteAllText("Settings.json", root.ToString());
				}
			}
		}
		/// <summary>
		/// Load bot settings or creates a new one.
		/// </summary>
		/// <returns></returns>
		public static void LoadBotSettings()
		{
			string path = "Settings.json";
			if (File.Exists(path))
			{
				try
				{
					LoadBotSettings(path);
					isLoading = false;
				}
				catch
				{
					isLoading = false;
					Window.Get.Log("Error loading bot settings. A new one has been generated!");
					File.Move(path, path+".bkp");
					SaveBotSettings();
				}
			}
			else
			{
				SaveBotSettings();
			}
		}
		private static void LoadBotSettings(string path)
		{
			lock (BotSettingsLock)
			{
				Window w = Window.Get;
				JObject root = JObject.Parse(File.ReadAllText(path));

				#region (Silkroad Tab)
				JObject silkroadList = (JObject)root["Silkroad"];
				foreach (JProperty key in silkroadList.Properties())
				{
					JObject silkroad = (JObject)silkroadList[key.Name];

					TreeNode s = new TreeNode(key.Name);
					s.Name = s.Text;

					TreeNode node = new TreeNode("Hosts");
					node.Name = "Hosts";
					foreach (JToken host in (JArray)silkroad["Hosts"])
					{
						node.Nodes.Add((string)host);
					}
					s.Nodes.Add(node);
					node = new TreeNode("Use random host : " + ((bool)silkroad["Port"] ? "Yes" : "No"));
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

					node = new TreeNode("Locale : " + silkroad["Locale"]);
					node.Name = "Locale";
					node.Tag = (byte)silkroad["Locale"];
					s.Nodes.Add(node);

					ushort hwidClientOp = ushort.Parse((string)silkroad["HWID"]["Client"]["Opcode"], System.Globalization.NumberStyles.HexNumber);
					ushort hwidServerOp = ushort.Parse((string)silkroad["HWID"]["Server"]["Opcode"], System.Globalization.NumberStyles.HexNumber);
					TreeNode hwid = new TreeNode("HWID Setup (" + (hwidClientOp == 0 && hwidServerOp == 0 ? "Off" : "On") + ")");
					hwid.Name = "HWID";

					TreeNode hwidclient = new TreeNode("Client");
					hwidclient.Name = "Client";
					hwid.Nodes.Add(hwidclient);
					node = new TreeNode("Opcode : " + (hwidClientOp == 0 ? "None" : "0x" + hwidClientOp.ToString("X4")));
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
					node = new TreeNode("Opcode : " + (hwidServerOp == 0 ? "None" : "0x" + hwidServerOp.ToString("X4")));
					node.Name = "Opcode";
					node.Tag = hwidServerOp;
					hwidserver.Nodes.Add(node);
					node = new TreeNode("Send to : " + silkroad["HWID"]["Server"]["SendTo"].ToString());
					node.Name = "SendTo";
					node.Tag = (string)silkroad["HWID"]["Server"]["SendTo"];
					hwidserver.Nodes.Add(node);

					string hwidData = "";
					if (File.Exists("Data\\" + key.Name + ".hwid"))
						hwidData = WinAPI.BytesToHexString(File.ReadAllBytes("Data\\" + key.Name + ".hwid"));
					node = new TreeNode("Data : " + (hwidData == "" ? "None" : hwidData));
					node.Name = "Data";
					node.Tag = hwidData;
					hwid.Nodes.Add(node);
					node = new TreeNode("Send Data only once : " + (bool.Parse(silkroad["HWID"]["SendOnlyOnce"].ToString()) ? "Yes" : "No"));
					node.Name = "SendOnlyOnce";
					node.Tag = (bool)silkroad["HWID"]["SendOnlyOnce"];
					hwid.Nodes.Add(node);

					s.Nodes.Add(hwid);

					Window.Get.Settings_lstrSilkroads.Nodes.Add(s);
					Window.Get.Login_cmbxSilkroad.Items.Add(s.Name);
				}
				#endregion

				#region (Auto Magically Tab)
				JObject autoMagic = (JObject)root["AutoMagically"];
				
				w.Settings_cbxSelectFirstChar.Checked = (bool)autoMagic["SelectFirstCharacter"];
				w.Settings_cbxCreateChar.Checked = (bool)autoMagic["CreateCharacter"];
				w.Settings_cbxCreateCharBelow40.Checked = (bool)autoMagic["CreateCharacterBelow40"];
				w.Settings_tbxCustomName.Text = (string)autoMagic["CreateCustomName"];
				w.Settings_tbxCustomSequence.Text = (string)autoMagic["CreateCustomSequence"];
				w.Settings_cmbxCreateCharRace.Text = (string)autoMagic["CreateCustomRace"];
				w.Settings_cmbxCreateCharGenre.Text = (string)autoMagic["CreateCustomGenre"];
				w.Settings_cbxDeleteChar40to50.Checked = (bool)autoMagic["DeleteCharacterFrom40To50"];
				w.Settings_cbxLoadDefaultConfigs.Checked = (bool)autoMagic["LoadDefaultConfigs"];
				#endregion

				#region (Packet Analyzer Tab)
				JObject packetAnalyzer = (JObject)root["PacketAnalyzer"];

				foreach (JToken opcode in (JArray)packetAnalyzer["Filter"])
				{
					ListViewItem item = new ListViewItem((string)opcode);
					item.Name = item.Text;
					w.Settings_lstvOpcodes.Items.Add(item);
				}
				
				w.Settings_rbnPacketOnlyShow.Checked = (bool)packetAnalyzer["FilterOnlyShow"];
				#endregion
			}
		}
		public static void SaveCharacterSettings()
		{
			lock (CharacterSettingsLock)
			{
				Window w = Window.Get;

				// Generating nodes manually
				JObject root = new JObject();

				#region (Character Tab)
				JObject Character = new JObject();
				root["Character"] = Character;

				JObject Basic = new JObject();
				Character["Basic"] = Basic;

				Basic["ShowExp"] = w.Character_cbxMessageExp.Checked;
				Basic["ShowPicks"] = w.Character_cbxMessagePick.Checked;
				Basic["ShowUniques"] = w.Character_cbxMessageUnique.Checked;
				#endregion

				#region (Party Tab)
				JObject Party = new JObject();
				root["Party"] = Party;

				JObject Options = new JObject();
				Party["Options"] = Options;

				Options["ExpFree"] = w.Party_rbnSetupExpFree.Checked;
				Options["ItemFree"] = w.Party_rbnSetupItemFree.Checked;
				Options["OnlyMasterInvite"] = w.Party_cbxSetupMasterInvite.Checked;
				Options["AcceptOnlyPartySetup"] = w.Party_cbxAcceptOnlyPartySetup.Checked;
				Options["AcceptAll"] = w.Party_cbxAcceptAll.Checked;
				Options["AcceptPlayerList"] = w.Party_cbxAcceptPlayerList.Checked;
				Options["AcceptLeaderList"] = w.Party_cbxAcceptLeaderList.Checked;
				Options["LeavePartyLeaderNotFound"] = w.Party_cbxLeavePartyNoneLeader.Checked;
				Options["RefusePartys"] = w.Party_cbxRefusePartys.Checked;

				JArray players = new JArray();
				foreach (ListViewItem item in w.Party_lstvPlayers.Items)
          players.Add(item.Text);
				Options["PlayerList"] = players;
        JArray leaders = new JArray();
				foreach (ListViewItem item in w.Party_lstvLeaders.Items)
					leaders.Add(item.Text);
				Options["LeaderList"] = leaders;

				#endregion

				// Saving
				Info i = Info.Get;
				File.WriteAllText("Config\\" + i.Silkroad + "_" + i.Server + "_" + i.Charname + ".json", root.ToString());
			}
		}
		public static void LoadCharacterSettings()
		{
			// Check folder
			if (!Directory.Exists("Config"))
				Directory.CreateDirectory("Config");

			Window w = Window.Get;
			Info i = Info.Get;
			// Check config path
			string cfgPath = "Config\\"+i.Silkroad + "_" + i.Server + "_" + i.Charname + ".json";
			WinAPI.InvokeIfRequired(w,()=> {
				if (File.Exists(cfgPath))
				{
					LoadCharacterSettings(cfgPath);
				}
				else if (w.Settings_cbxLoadDefaultConfigs.Checked && File.Exists("Config\\default.json"))
				{
					LoadCharacterSettings("Config\\default.json");
				}
				else
				{
					// TO DO :
					// RESET GUI TO SAVE CLEAN CHARACTER SETTINGS
					SaveCharacterSettings();
				}
			});
		}
		private static void LoadCharacterSettings(string path)
		{
			lock (CharacterSettingsLock)
			{
				JObject root = JObject.Parse(File.ReadAllText(path));

				Window w = Window.Get;

				#region (Character Tab)
				JObject Character = (JObject)root["Character"];

				JObject Basic = (JObject)Character["Basic"];
				w.Character_cbxMessageExp.Checked = (bool)Basic["ShowExp"];
				w.Character_cbxMessagePick.Checked = (bool)Basic["ShowPicks"];
				w.Character_cbxMessageUnique.Checked = (bool)Basic["ShowUniques"];
				#endregion

				#region (Party Tab)
				JObject Party = (JObject)root["Party"];
				
				JObject Options = (JObject)Party["Options"];
				w.Party_rbnSetupExpFree.Checked = (bool)Options["ExpFree"];
				w.Party_rbnSetupItemFree.Checked = (bool)Options["ItemFree"];
				w.Party_cbxSetupMasterInvite.Checked = (bool)Options["OnlyMasterInvite"];
				w.Party_cbxAcceptOnlyPartySetup.Checked = (bool)Options["AcceptOnlyPartySetup"];
				w.Party_cbxAcceptAll.Checked = (bool)Options["AcceptAll"];
				w.Party_cbxAcceptPlayerList.Checked = (bool)Options["AcceptPlayerList"];
				w.Party_cbxAcceptLeaderList.Checked = (bool)Options["AcceptLeaderList"];
				if(Options.ContainsKey("LeavePartyLeaderNotFound"))
					w.Party_cbxLeavePartyNoneLeader.Checked = (bool)Options["LeavePartyLeaderNotFound"];
				if (Options.ContainsKey("RefusePartys"))
					w.Party_cbxRefusePartys.Checked = (bool)Options["RefusePartys"];

				foreach (JToken player in (JArray)Options["PlayerList"])
				{
					ListViewItem item = new ListViewItem((string)player);
					item.Name = item.Text.ToLower();
					w.Party_lstvPlayers.Items.Add(item);
				}
				foreach (JToken leader in (JArray)Options["LeaderList"])
				{
					ListViewItem item = new ListViewItem((string)leader);
					item.Name = item.Text.ToLower();
					w.Party_lstvLeaders.Items.Add(item);
				}
				#endregion
			}
		}
	}
}