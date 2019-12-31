using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;
using xBot.Game;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;

namespace xBot.App
{
	public static class Settings{
		/// <summary>
		/// Avoid loading overload
		/// </summary>
		private static bool LoadingBotSettings;
		private static readonly object BotSettingsLock = new object();
		/// <summary>
		/// Avoid saving settings while is loading
		/// </summary>
		private static bool LoadingCharacterSettings;
		private static readonly object CharacterSettingsLock = new object();
		public static void SaveBotSettings()
		{
			if (!LoadingBotSettings)
			{
				lock (BotSettingsLock)
				{
					JObject root = new JObject();
					// Generating/editing nodes manually
					Window w = Window.Get;

					#region (Silkroad Tab)
					JObject Silkroads = new JObject();
					root["Silkroads"] = Silkroads;

					foreach (ListViewItem item in w.Settings_lstvSilkroads.Items)
					{
						JObject server = new JObject();
						Silkroads[item.Name] = server;

						server["Locale"] = (byte)item.SubItems[1].Tag;
						server["Version"] = (uint)item.SubItems[2].Tag;
						server["Port"] = (ushort)item.SubItems[3].Tag;

						JArray gateways = new JArray();
						foreach (string gw in (System.Collections.Generic.List<string>)item.SubItems[4].Tag)
							gateways.Add(gw);
						server["Gateways"] = gateways;

						server["RandomGateway"] = (bool)item.SubItems[5].Tag;
						server["LauncherPath"] = (string)item.SubItems[6].Tag;
						server["ClientPath"] = (string)item.SubItems[7].Tag;
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
					foreach (ListViewItem opcode in w.Settings_lstvOpcodes.Items)
						opcodes.Add(opcode.Text);

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
					LoadingBotSettings = true;
					LoadBotSettings(path);
					LoadingBotSettings = false;
				}
				catch
				{
					LoadingBotSettings = false;
					Window.Get.Log("Error loading bot settings.. using the settings partially loaded");
					File.Move(path, path+".bkp");
					SaveBotSettings();
				}
			}
			else
			{
				SaveBotSettings();
			}
		}
		/// <summary>
		/// Load bot settings if exists.
		/// </summary>
		private static void LoadBotSettings(string path)
		{
			lock (BotSettingsLock)
			{
				Window w = Window.Get;
				JObject root = JObject.Parse(File.ReadAllText(path));

				#region (Silkroad Tab)
				if (root.ContainsKey("Silkroads"))
				{
					JObject Silkroads = (JObject)root["Silkroads"];
					foreach (JProperty key in Silkroads.Properties())
					{
						JObject server = (JObject)Silkroads[key.Name];

						ListViewItem item = new ListViewItem(key.Name);
						item.Name = item.Text;

						if (!server.ContainsKey("Locale"))
							break;
						ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = (byte)server["Locale"];
						item.SubItems.Add(subitem);

						if (!server.ContainsKey("Version"))
							break;
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = (uint)server["Version"];
						item.SubItems.Add(subitem);

						if (!server.ContainsKey("Port"))
							break;
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = (ushort)server["Port"];
						item.SubItems.Add(subitem);

						if (!server.ContainsKey("Gateways"))
							break;
						subitem = new ListViewItem.ListViewSubItem();
						System.Collections.Generic.List<string> gws = new System.Collections.Generic.List<string>();
						foreach (JToken gw in (JArray)server["Gateways"])
							gws.Add((string)gw);
						subitem.Tag = gws;
						item.SubItems.Add(subitem);

						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = server.ContainsKey("RandomGateway") ? (bool)server["RandomGateway"] : false;
						item.SubItems.Add(subitem);

						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = server.ContainsKey("LauncherPath") ? (string)server["LauncherPath"]:"";
						item.SubItems.Add(subitem);

						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = server.ContainsKey("ClientPath") ? (string)server["ClientPath"] : "";
						item.SubItems.Add(subitem);

						w.Settings_lstvSilkroads.Items.Add(item);
						w.Login_cmbxSilkroad.Items.Add(item.Name);
					}
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

				foreach (JToken token in (JArray)packetAnalyzer["Filter"])
				{
					ushort opcode;
					if (ushort.TryParse((string)token, System.Globalization.NumberStyles.HexNumber,null,out opcode))
					{
						ListViewItem item = new ListViewItem((string)token);
						item.Name = opcode.ToString();
						w.Settings_lstvOpcodes.Items.Add(item);
					}
				}
				w.Settings_rbnPacketOnlyShow.Checked = (bool)packetAnalyzer["FilterOnlyShow"];
				#endregion
			}
		}
		/// <summary>
		/// Save the character settings if it's in game.
		/// </summary>
		public static void SaveCharacterSettings()
		{
			if (!LoadingCharacterSettings && InfoManager.inGame)
			{
				lock (CharacterSettingsLock)
				{
					Window w = Window.Get;

					// Generating nodes manually
					JObject root = new JObject();

					#region (Character Tab)
					JObject Character = new JObject();
					root["Character"] = Character;
					{
						JObject Inf = new JObject();
						Character["Info"] = Inf;
						Inf["ShowExp"] = w.Character_cbxMessageExp.Checked;
						Inf["ShowUniques"] = w.Character_cbxMessageUniques.Checked;
						Inf["ShowEvents"] = w.Character_cbxMessageEvents.Checked;
						Inf["ShowPicks"] = w.Character_cbxMessagePicks.Checked;

						JObject Potions = new JObject();
						Character["Potions"] = Potions;
						Potions["UseHP"] = w.Character_cbxUseHP.Checked;
						Potions["UseHPPercent"] = w.Character_tbxUseHP.Text;
						Potions["UseHPGrain"] = w.Character_cbxUseHPGrain.Checked;
						Potions["UseHPVigor"] = w.Character_cbxUseHPVigor.Checked;
						Potions["UseMP"] = w.Character_cbxUseMP.Checked;
						Potions["UseMPPercent"] = w.Character_tbxUseMP.Text;
						Potions["UseMPGrain"] = w.Character_cbxUseMPGrain.Checked;
						Potions["UseMPVigor"] = w.Character_cbxUseMPVigor.Checked;
						Potions["UseUniversalPills"] = w.Character_cbxUsePillUniversal.Checked;
						Potions["UsePurificationPills"] = w.Character_cbxUsePillPurification.Checked;
						Potions["UsePetHP"] = w.Character_cbxUsePetHP.Checked;
						Potions["UsePetHPPercent"] = w.Character_tbxUsePetHP.Text;
						Potions["UseTransportHP"] = w.Character_cbxUseTransportHP.Checked;
						Potions["UseTransportHPPercent"] = w.Character_tbxUseTransportHP.Text;
						Potions["UsePetsPill"] = w.Character_cbxUsePetsPill.Checked;
						Potions["UsePetHGP"] = w.Character_cbxUsePetHGP.Checked;
						Potions["UsePetHGPPercent"] = w.Character_tbxUsePetHGP.Text;

						JObject Misc = new JObject();
						Character["Misc"] = Misc;
						Misc["AcceptRess"] = w.Character_cbxAcceptRess.Checked;
						Misc["AcceptRessPartyOnly"] = w.Character_cbxAcceptRessPartyOnly.Checked;
						Misc["RefuseExchange"] = w.Character_cbxRefuseExchange.Checked;
						Misc["AcceptExchange"] = w.Character_cbxAcceptExchange.Checked;
						Misc["AcceptExchangePartyOnly"] = w.Character_cbxAcceptExchangeLeaderOnly.Checked;
						Misc["ConfirmExchange"] = w.Character_cbxConfirmExchange.Checked;
						Misc["ApproveExchange"] = w.Character_cbxApproveExchange.Checked;
					}
					#endregion

					#region (Party Tab)
					JObject Party = new JObject();
					root["Party"] = Party;
					{
						JObject Options = new JObject();
						Party["Options"] = Options;
						Options["ExpFree"] = w.Party_rbnSetupExpFree.Checked;
						Options["ItemFree"] = w.Party_rbnSetupItemFree.Checked;
						Options["OnlyMasterInvite"] = w.Party_cbxSetupMasterInvite.Checked;
						Options["AcceptOnlyPartySetup"] = w.Party_cbxAcceptOnlyPartySetup.Checked;
						Options["AcceptAll"] = w.Party_cbxAcceptAll.Checked;
						Options["AcceptPartyList"] = w.Party_cbxAcceptPartyList.Checked;
						Options["AcceptLeaderList"] = w.Party_cbxAcceptLeaderList.Checked;
						Options["LeavePartyLeaderNotFound"] = w.Party_cbxLeavePartyNoneLeader.Checked;
						Options["RefuseInvitations"] = w.Party_cbxRefuseInvitations.Checked;
						Options["ActivateLeaderCommands"] = w.Party_cbxActivateLeaderCommands.Checked;
						Options["InviteOnlyPartySetup"] = w.Party_cbxInviteOnlyPartySetup.Checked;
						Options["InviteAll"] = w.Party_cbxInviteAll.Checked;
						Options["InvitePartyList"] = w.Party_cbxInvitePartyList.Checked;
						JArray players = new JArray();
						foreach (ListViewItem item in w.Party_lstvPartyList.Items)
							players.Add(item.Text);
						Options["PartyList"] = players;
						JArray leaders = new JArray();
						foreach (ListViewItem item in w.Party_lstvLeaderList.Items)
							leaders.Add(item.Text);
						Options["LeaderList"] = leaders;

						JObject Match = new JObject();
						Party["Match"] = Match;
						Match["Title"] = w.Party_tbxMatchTitle.Text;
						Match["From"] = w.Party_tbxMatchFrom.Text;
						Match["To"] = w.Party_tbxMatchTo.Text;
						Match["AutoReform"] = w.Party_cbxMatchAutoReform.Checked;
						Match["AcceptAll"] = w.Party_cbxMatchAcceptAll.Checked;
						Match["AcceptPartyList"] = w.Party_cbxMatchAcceptPartyList.Checked;
						Match["AcceptLeaderList"] = w.Party_cbxMatchAcceptLeaderList.Checked;
						Match["Refuse"] = w.Party_cbxMatchRefuse.Checked;
					}
					#endregion

					#region (Skills Tab)
					JObject Skills = new JObject();
					root["Skills"] = Skills;
					{
						JObject Attack = new JObject();
						Skills["Attack"] = Attack;

						JArray skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_General.Items)
							skills.Add(item.Text);
						Attack["General"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_Champion.Items)
							skills.Add(item.Text);
						Attack["Champion"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_Giant.Items)
							skills.Add(item.Text);
						Attack["Giant"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_PartyGeneral.Items)
							skills.Add(item.Text);
						Attack["PartyGeneral"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_PartyChampion.Items)
							skills.Add(item.Text);
						Attack["PartyChampion"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_PartyGiant.Items)
							skills.Add(item.Text);
						Attack["PartyGiant"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_Unique.Items)
							skills.Add(item.Text);
						Attack["Unique"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_Elite.Items)
							skills.Add(item.Text);
						Attack["Elite"] = skills;
						skills = new JArray();
						foreach (ListViewItem item in w.Skills_lstvAttackMobType_Event.Items)
							skills.Add(item.Text);
						Attack["Event"] = skills;
						Attack["WalkToCenter"] = w.Training_cbxWalkToCenter.Checked;
					}
					#endregion

					#region (Training Tab)
					JObject Training = new JObject();
					root["Training"] = Training;
					{
						JObject Area = new JObject();
						Training["Area"] = Area;
						foreach (ListViewItem item in w.Training_lstvAreas.Items)
						{
							JObject area = new JObject();
							area["Region"] = (ushort)item.SubItems[1].Tag;
							area["X"] = (int)item.SubItems[2].Tag;
							area["Y"] = (int)item.SubItems[3].Tag;
							area["Z"] = (int)item.SubItems[4].Tag;
							area["Radius"] = (int)item.SubItems[5].Tag;
							area["Path"] = item.SubItems[6].Text;
							Area[item.Name] = area;
						}
						Training["AreaActivated"] = w.Training_lstvAreas.Tag != null ? ((ListViewItem)w.Training_lstvAreas.Tag).Name : "";

						JObject Trace = new JObject();
						Training["Trace"] = Trace;
						Trace["TracePartyMaster"] = w.Training_cbxTraceMaster.Checked;
						Trace["UseTraceDistance"] = w.Training_cbxTraceDistance.Checked;
						Trace["TraceDistance"] = w.Training_tbxTraceDistance.Text;
					}
					#endregion

					#region (Stall Tab)
					JObject Stall = new JObject();
					root["Stall"] = Stall;
					{
						JObject Options = new JObject();
						Stall["Options"] = Options;
						Options["Title"] = w.Stall_tbxStallTitle.Text;
						Options["Note"] = w.Stall_tbxStallNote.Text;
					}
					#endregion

					// Saving
					File.WriteAllText("Config\\" + DataManager.SilkroadName + "_" + InfoManager.ServerName + "_" + InfoManager.CharName + ".json", root.ToString());
				}
			}
		}
		/// <summary>
		/// Load character settings if exists or try to load default if it's checked.
		/// </summary>
		public static void LoadCharacterSettings()
		{
			// Check folder
			if (!Directory.Exists("Config"))
				Directory.CreateDirectory("Config");

			Window w = Window.Get;
			// Check config path
			string cfgPath = "Config\\"+DataManager.SilkroadName + "_" + InfoManager.ServerName + "_" + InfoManager.CharName + ".json";
			try
			{
				if (File.Exists(cfgPath))
				{
					LoadCharacterSettings(cfgPath);
					w.Log("Configs loaded successfully");
				}
				else if (w.Settings_cbxLoadDefaultConfigs.Checked && File.Exists("Config\\Default.json"))
				{
					LoadCharacterSettings("Config\\Default.json");
					w.Log("Configs by default loaded successfully");
				}
				else
				{
					LoadCharacterSettings("");
					w.Log("Configs created successfully");
				}
			}
			catch
			{
				LoadingCharacterSettings = false;
				File.Move(cfgPath, cfgPath + ".bkp");
				w.Log("Error loading character configs... Using the configs partially loaded");
				SaveCharacterSettings();
			}
		}
		private static void LoadCharacterSettings(string path)
		{
			lock (CharacterSettingsLock)
			{
				LoadingCharacterSettings = true;
				JObject root;

				// Load or create config
				root = path == "" ? new JObject() : JObject.Parse(File.ReadAllText(path));

				Window w = Window.Get;

				#region (Character Tab)
				JObject Character = root.ContainsKey("Character") ? (JObject)root["Character"] : new JObject();
				{
					JObject Inf = Character.ContainsKey("Info") ? (JObject)Character["Info"] : new JObject();
					w.Character_cbxMessageExp.Checked = Inf.ContainsKey("ShowExp") ? (bool)Inf["ShowExp"] : false;
					w.Character_cbxMessageUniques.Checked = Inf.ContainsKey("ShowUniques") ? (bool)Inf["ShowUniques"] : false;
					w.Character_cbxMessageEvents.Checked = Inf.ContainsKey("ShowEvents") ? (bool)Inf["ShowEvents"] : false;
					w.Character_cbxMessagePicks.Checked = Inf.ContainsKey("ShowPicks") ? (bool)Inf["ShowPicks"] : false;

					JObject Potions = Character.ContainsKey("Potions") ? (JObject)Character["Potions"] : new JObject();
					w.Character_cbxUseHP.Checked = Potions.ContainsKey("UseHP") ? (bool)Potions["UseHP"] : false;
					w.Character_tbxUseHP.Text = Potions.ContainsKey("UseHPPercent") ? (string)Potions["UseHPPercent"] : "50";
					w.Character_cbxUseHPGrain.Checked = Potions.ContainsKey("UseHPGrain") ? (bool)Potions["UseHPGrain"] : false;
					w.Character_cbxUseHPVigor.Checked = Potions.ContainsKey("UseHPVigor") ? (bool)Potions["UseHPVigor"] : false;
					w.Character_cbxUseMP.Checked = Potions.ContainsKey("UseMP") ? (bool)Potions["UseMP"] : false;
					w.Character_tbxUseMP.Text = Potions.ContainsKey("UseMPPercent") ? (string)Potions["UseMPPercent"] : "50";
					w.Character_cbxUseMPGrain.Checked = Potions.ContainsKey("UseMPGrain") ? (bool)Potions["UseMPGrain"] : false;
					w.Character_cbxUseMPVigor.Checked = Potions.ContainsKey("UseMPVigor") ? (bool)Potions["UseMPVigor"] : false;
					w.Character_cbxUsePillUniversal.Checked = Potions.ContainsKey("UseUniversalPills") ? (bool)Potions["UseUniversalPills"] : false;
					w.Character_cbxUsePillPurification.Checked = Potions.ContainsKey("UsePurificationPills") ? (bool)Potions["UsePurificationPills"] : false;
					w.Character_cbxUsePetHP.Checked = Potions.ContainsKey("UsePetHP") ? (bool)Potions["UsePetHP"] : false;
					w.Character_tbxUsePetHP.Text = Potions.ContainsKey("UsePetHPPercent") ? (string)Potions["UsePetHPPercent"] : "50";
					w.Character_cbxUseTransportHP.Checked = Potions.ContainsKey("UseTransportHP") ? (bool)Potions["UseTransportHP"] : false;
					w.Character_tbxUseTransportHP.Text = Potions.ContainsKey("UseTransportHPPercent") ? (string)Potions["UseTransportHPPercent"] : "50";
					w.Character_cbxUsePetsPill.Checked = Potions.ContainsKey("UsePetsPill") ? (bool)Potions["UsePetsPill"] : false;
					w.Character_cbxUsePetHGP.Checked = Potions.ContainsKey("UsePetHGP") ? (bool)Potions["UsePetHGP"] : false;
					w.Character_tbxUsePetHGP.Text = Potions.ContainsKey("UsePetHGPPercent") ? (string)Potions["UsePetHGPPercent"] : "50";

					JObject Misc = Character.ContainsKey("Misc") ? (JObject)Character["Misc"] : new JObject();
					w.Character_cbxAcceptRess.Checked = Misc.ContainsKey("AcceptRess") ? (bool)Misc["AcceptRess"] : false;
					w.Character_cbxAcceptRessPartyOnly.Checked = Misc.ContainsKey("AcceptRessPartyOnly") ? (bool)Misc["AcceptRessPartyOnly"] : false;
					w.Character_cbxRefuseExchange.Checked = Misc.ContainsKey("RefuseExchange") ? (bool)Misc["RefuseExchange"] : false;
					w.Character_cbxAcceptExchange.Checked = Misc.ContainsKey("AcceptExchange") ? (bool)Misc["AcceptExchange"] : false;
					w.Character_cbxAcceptExchangeLeaderOnly.Checked = Misc.ContainsKey("AcceptExchangePartyOnly") ? (bool)Misc["AcceptExchangePartyOnly"] : false;
					w.Character_cbxConfirmExchange.Checked = Misc.ContainsKey("ConfirmExchange") ? (bool)Misc["ConfirmExchange"] : false;
					w.Character_cbxApproveExchange.Checked = Misc.ContainsKey("ApproveExchange") ? (bool)Misc["ApproveExchange"] : false;

				}
				#endregion

				#region (Party Tab)
				JObject Party = root.ContainsKey("Party") ? (JObject)root["Party"] : new JObject();
				{
					JObject Options = Party.ContainsKey("Options") ? (JObject)Party["Options"] : new JObject();
					w.Party_rbnSetupExpFree.Checked = Options.ContainsKey("ExpFree") ? (bool)Options["ExpFree"] : true;
					w.Party_rbnSetupExpShared.Checked = !w.Party_rbnSetupExpFree.Checked;
					w.Party_rbnSetupItemFree.Checked = Options.ContainsKey("ItemFree") ? (bool)Options["ItemFree"] : true;
					w.Party_rbnSetupItemShared.Checked = !w.Party_rbnSetupItemFree.Checked;
					w.Party_cbxSetupMasterInvite.Checked = Options.ContainsKey("OnlyMasterInvite") ? (bool)Options["OnlyMasterInvite"] : false;
					w.Party_cbxAcceptOnlyPartySetup.Checked = Options.ContainsKey("AcceptOnlyPartySetup") ? (bool)Options["AcceptOnlyPartySetup"] : false;
					w.Party_cbxAcceptAll.Checked = Options.ContainsKey("AcceptAll") ? (bool)Options["AcceptAll"] : false;
					w.Party_cbxAcceptPartyList.Checked = Options.ContainsKey("AcceptPartyList") ? (bool)Options["AcceptPartyList"] : false;
					w.Party_cbxAcceptLeaderList.Checked = Options.ContainsKey("AcceptLeaderList") ? (bool)Options["AcceptLeaderList"] : false;
					w.Party_cbxLeavePartyNoneLeader.Checked = Options.ContainsKey("LeavePartyLeaderNotFound") ? (bool)Options["LeavePartyLeaderNotFound"] : false;
					w.Party_cbxRefuseInvitations.Checked = Options.ContainsKey("RefuseInvitations") ? (bool)Options["RefuseInvitations"] : false;
					w.Party_cbxActivateLeaderCommands.Checked = Options.ContainsKey("ActivateLeaderCommands") ? (bool)Options["ActivateLeaderCommands"] : false;
					w.Party_cbxInviteOnlyPartySetup.Checked = Options.ContainsKey("InviteOnlyPartySetup") ? (bool)Options["InviteOnlyPartySetup"] : false;
					w.Party_cbxInviteAll.Checked = Options.ContainsKey("InviteAll") ? (bool)Options["InviteAll"] : false;
					w.Party_cbxInvitePartyList.Checked = Options.ContainsKey("InvitePartyList") ? (bool)Options["InvitePartyList"] : false;
					w.Party_lstvPartyList.Items.Clear();
					if (Options.ContainsKey("PartyList"))
					{
						foreach (JToken player in (JArray)Options["PartyList"])
						{
							ListViewItem item = new ListViewItem((string)player);
							item.Name = item.Text.ToUpper();
							w.Party_lstvPartyList.Items.Add(item);
						}
					}
					w.Party_lstvLeaderList.Items.Clear();
					if (Options.ContainsKey("LeaderList"))
					{
						foreach (JToken leader in (JArray)Options["LeaderList"])
						{
							ListViewItem item = new ListViewItem((string)leader);
							item.Name = item.Text.ToUpper();
							w.Party_lstvLeaderList.Items.Add(item);
						}
					}

					JObject Match = Party.ContainsKey("Match") ? (JObject)Party["Match"] : new JObject();
					w.Party_tbxMatchTitle.Text = Match.ContainsKey("Title") ? (string)Match["Title"] : "[xBot] When you play Silkroad you win or you die..";
					w.Party_tbxMatchFrom.Text = Match.ContainsKey("From") ? (string)Match["From"] : "0";
					w.Party_tbxMatchTo.Text = Match.ContainsKey("To") ? (string)Match["To"] : "255";
					w.Party_cbxMatchAutoReform.Checked = Match.ContainsKey("AutoReform") ? (bool)Match["AutoReform"] : false;
					w.Party_cbxMatchAcceptAll.Checked = Match.ContainsKey("AcceptAll") ? (bool)Match["AcceptAll"] : true;
					w.Party_cbxMatchAcceptPartyList.Checked = Match.ContainsKey("AcceptPartyList") ? (bool)Match["AcceptPartyList"] : false;
					w.Party_cbxMatchAcceptLeaderList.Checked = Match.ContainsKey("AcceptLeaderList") ? (bool)Match["AcceptLeaderList"] : false;
					w.Party_cbxMatchRefuse.Checked = Match.ContainsKey("Refuse") ? (bool)Match["Refuse"] : false;
				}
				#endregion

				#region (Skills Tab)
				JObject Skills = root.ContainsKey("Skills") ? (JObject)root["Skills"] : new JObject();
				{
					xDictionary<uint, SRSkill> mySkills = InfoManager.Character.Skills;

					JObject Attack = Skills.ContainsKey("Attack") ? (JObject)Skills["Attack"] : new JObject();
					w.Skills_lstvAttackMobType_General.Items.Clear();
					if (Attack.ContainsKey("General"))
					{
						foreach (JToken token in (JArray)Attack["General"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_General.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_Champion.Items.Clear();
					if (Attack.ContainsKey("Champion"))
					{
						foreach (JToken token in (JArray)Attack["Champion"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_Champion.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_Giant.Items.Clear();
					if (Attack.ContainsKey("Giant"))
					{
						foreach (JToken token in (JArray)Attack["Giant"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_Giant.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_PartyGeneral.Items.Clear();
					if (Attack.ContainsKey("PartyGeneral"))
					{
						foreach (JToken token in (JArray)Attack["PartyGeneral"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_PartyGeneral.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_PartyChampion.Items.Clear();
					if (Attack.ContainsKey("PartyChampion"))
					{
						foreach (JToken token in (JArray)Attack["PartyChampion"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_PartyChampion.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_PartyGiant.Items.Clear();
					if (Attack.ContainsKey("PartyGiant"))
					{
						foreach (JToken token in (JArray)Attack["PartyGiant"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_PartyGiant.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_Unique.Items.Clear();
					if (Attack.ContainsKey("Unique"))
					{
						foreach (JToken token in (JArray)Attack["Unique"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_Unique.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_Elite.Items.Clear();
					if (Attack.ContainsKey("Elite"))
					{
						foreach (JToken token in (JArray)Attack["Elite"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_Elite.Items.Add(item);
							}
						}
					}
					w.Skills_lstvAttackMobType_Event.Items.Clear();
					if (Attack.ContainsKey("Event"))
					{
						foreach (JToken token in (JArray)Attack["Event"])
						{
							string skillName = (string)token;
							SRSkill skill = mySkills.Find(s => s.Name == skillName);
							if (skill != null)
							{
								ListViewItem item = new ListViewItem(skillName);
								item.Name = skill.ID.ToString();
								item.Tag = skill;
								w.Skills_lstvAttackMobType_Event.Items.Add(item);
							}
						}
					}
					w.Training_cbxWalkToCenter.Checked = Attack.ContainsKey("WalkToCenter") ? (bool)Attack["WalkToCenter"] : false;
				}
				#endregion

				#region (Training Tab)
				JObject Training = root.ContainsKey("Training") ? (JObject)root["Training"] : new JObject();
				{
					JObject Area = Training.ContainsKey("Area") ? (JObject)Training["Area"] : new JObject();
					string AreaActivated = Training.ContainsKey("AreaActivated") ? (string)Training["AreaActivated"] : "";
					foreach (JProperty key in Area.Properties())
					{
						JObject area = (JObject)Area[key.Name];

						ListViewItem item = new ListViewItem(key.Name);
						item.Name = key.Name;

						ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = area.ContainsKey("Region") ? (ushort)area["Region"] : (ushort)0;
						item.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = area.ContainsKey("X") ? (int)area["X"] : 0;
						item.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = area.ContainsKey("Y") ? (int)area["Y"] : 0;
						item.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = area.ContainsKey("Z") ? (int)area["Z"] : 0;
						item.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = area.ContainsKey("Radius") ? (int)area["Radius"] : 0;
						item.SubItems.Add(subitem);
						item.SubItems.Add(area.ContainsKey("Path") ? (string)area["Path"] : "");
						// Check if this area is activated
						if (AreaActivated != "" && AreaActivated == item.Name)
						{
							item.ForeColor = System.Drawing.Color.FromArgb(0, 180, 255);
							w.Training_lstvAreas.Tag = item;
							AreaActivated = "";
						}
						w.Training_lstvAreas.Items.Add(item);
					}

					JObject Trace = Training.ContainsKey("Trace") ? (JObject)Training["Trace"] : new JObject();
					w.Training_cbxTraceMaster.Checked = Trace.ContainsKey("TracePartyMaster") ? (bool)Trace["TracePartyMaster"] : false;
					w.Training_cbxTraceDistance.Checked = Trace.ContainsKey("UseTraceDistance") ? (bool)Trace["UseTraceDistance"] : false;
					w.Training_tbxTraceDistance.Text = Trace.ContainsKey("TraceDistance") ? (string)Trace["TraceDistance"] : "5";
				}
				#endregion

				#region (Stall Tab)
				JObject Stall = root.ContainsKey("Stall") ? (JObject)root["Stall"] : new JObject();
				{
					JObject Options = Stall.ContainsKey("Options") ? (JObject)Stall["Options"] : new JObject();
					w.Stall_tbxStallTitle.Text = Options.ContainsKey("Title") ? (string)Options["Title"] : "[xBot] The things I do for love..";
					w.Stall_tbxStallNote.Text = Options.ContainsKey("Note") ? (string)Options["Note"] : "[xBot] Fear cuts deeper than swords..";
				}
				#endregion

				LoadingCharacterSettings = false;
			}
		}
	}
}