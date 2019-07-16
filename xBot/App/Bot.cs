using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using xBot.Game;
using System.Threading;

namespace xBot
{
	/// <summary>
	/// Handle everything about Bot logic.
	/// </summary>
	public class Bot
	{
		private static Bot _this = null;
		/// <summary>
		/// Keep reference from commandLine arguments.
		/// </summary>
		public bool isAutoLogin { get; set; }
		/// <summary>
		/// Get or set the proxy actually running.
		/// </summary>
		public Proxy Proxy { get; set; }
		public string ClientPath { get; set; }
		public bool LoginFromBot { get; set; }
		/// <summary>
		/// Gets the context type to save the HWID (Can be Gateway, Agent, or Both)
		/// </summary>
		public string HWIDSaveFrom { get { return _HWIDSaveFrom; } }
		private string _HWIDSaveFrom;
		/// <summary>
		/// Gets the context type to send the HWID (Can be Gateway, Agent, or Both)
		/// </summary>
		public string HWIDSendTo { get { return _HWIDSendTo; } }
		private string _HWIDSendTo;
		private bool _HWIDSent;
		private bool _HWIDSendOnlyOnce;
		/// <summary>
		/// Check if the character is on creation process.
		/// </summary>
		public bool isCreatingCharacter { get { return CreatingCharacterName != ""; } }
		private string CreatingCharacterName;
		private bool CreatingCharacterMale;
		/// <summary>
		/// Check if the character is in game.
		/// </summary>
		public bool inGame { get { return _triggerJoinedToGame; } }
		private bool _triggerJoinedToGame;
		/// <summary>
		/// Check if the character is in party.
		/// </summary>
		public bool inParty { get { return PartySetupType != -1; } }
		/// <summary>
		/// Keep the current party type or (-1) if is not in party. 
		/// </summary>
		private sbyte PartySetupType = -1;
		private byte PartyPurposeType;
		/// <summary>
		/// Get or set the last uniqueID selected.
		/// </summary>
		public uint SelectedUID { get; set; }
		private Bot()
		{
		}
		/// <summary>
		/// GetInstance. Secures an unique class creation for being used anywhere at the project.
		/// </summary>
		public static Bot Get
		{
			get
			{
				if (_this == null)
					_this = new Bot();
				return _this;
			}
		}
		public void LogError(string error, Packet p = null)
		{
			string msg = WinAPI.getDate() + error + Environment.NewLine;
			if (p != null)
				msg += "[" + p.Opcode.ToString("X4") + "][" + WinAPI.BytesToHexString(p.GetBytes()) + "]" + Environment.NewLine;
			File.AppendAllText("erros.log", msg);
		}
		/// <summary>
		/// Set the HWID setup.
		/// </summary>
		/// <param name="cOp">Client opcode used to save the HWID packet</param>
		/// <param name="SaveFrom">Save from Gateway/Server/Both</param>
		/// <param name="sOp">Server opcode used to send the HWID packet</param>
		/// <param name="SendTo">Send to from Gateway/Server/Both</param>
		/// <param name="SendOnlyOnce">Send HWID packet only once</param>
		/// <param name="Data">Packet string format</param>
		public void SetHWID(ushort cOp, string SaveFrom, ushort sOp, string SendTo, bool SendOnlyOnce)
		{
			Agent.Opcode.CLIENT_HWID_RESPONSE = Gateway.Opcode.CLIENT_HWID_RESPONSE = cOp;
			Agent.Opcode.SERVER_HWID_REQUEST = Gateway.Opcode.SERVER_HWID_REQUEST = sOp;
			_HWIDSaveFrom = SaveFrom;
			_HWIDSendTo = SendTo;
			_HWIDSendOnlyOnce = SendOnlyOnce;
			_HWIDSent = false;
		}

		#region (Extended logical methods)
		public void SaveHWID(byte[] data)
		{
			Window.Get.LogProcess("HWID Detected : " + WinAPI.BytesToHexString(data));
			File.WriteAllBytes("Data\\" + Info.Get.Silkroad + ".hwid", data);
		}
		public byte[] LoadHWID()
		{
			if(_HWIDSendOnlyOnce && _HWIDSent)
				return null;
			if (File.Exists("Data\\" + Info.Get.Silkroad + ".hwid"))
			{
				_HWIDSent = true;
				return File.ReadAllBytes("Data\\" + Info.Get.Silkroad + ".hwid");
			}
			return null;
		}
		public void CreateNickname()
		{
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Settings_tbxCustomName, () => {
				CreatingCharacterName = w.Settings_tbxCustomName.Text;
			});

			if (CreatingCharacterName == "")
			{
				WinAPI.InvokeIfRequired(w.Settings_cmbxCreateCharGenre, () => {
					CreatingCharacterName = getRandomNickname(w.Settings_cmbxCreateCharGenre.Text);
				});
			}
			else
			{
				string Number = "0";
				// Extract and update number sequence
				WinAPI.InvokeIfRequired(w.Settings_tbxCustomSequence, () => {
					int length = w.Settings_tbxCustomSequence.Text.Length;
					if (length == 0)
					{
						// Start default
						w.Settings_tbxCustomSequence.Text = Number;
					}
					else
					{
						// Increase or reset
						Number = w.Settings_tbxCustomSequence.Text;
						string NextNumber = (int.Parse(Number) + 1).ToString();
						if (NextNumber.Length > w.Settings_tbxCustomSequence.MaxLength) {
							// Reset
							w.Settings_tbxCustomSequence.Text = "0";
						}
						else
						{
							w.Settings_tbxCustomSequence.Text = NextNumber;
						}
					}
				});
				WinAPI.InvokeIfRequired(w, () => {
					Settings.SaveBotSettings();
				});
				// Join name and number
				if ((CreatingCharacterName + Number).Length > 12)
				{
					// Check Silkroad restriction & fix it
					CreatingCharacterName = CreatingCharacterName.Substring(0, 12 - Number.Length);
				}
				else
				{
					CreatingCharacterName = CreatingCharacterName + Number;
				}
			}

			w.Log("Checking nickname [" + CreatingCharacterName + "]");
			PacketBuilder.CheckCharacterName(CreatingCharacterName);
		}
		/// <summary>
		/// Generates a random Game of Thrones nickname with Discord style.
		/// </summary>
		public string getRandomNickname(string nameGenre)
		{
			Random rand = new Random();
			// List with names as maximum 8 letters!
			List<string> nicknames = new List<string>();
			// Choosing name genre
			CreatingCharacterMale = (nameGenre == "Random" ? rand.Next(100) % 2 == 0 : (nameGenre == "Male"));
			if (CreatingCharacterMale)
			{
				// Male
				nicknames.AddRange(new string[] {
					"Aegon","Aerys","Aemon","Aeron","Alliser","Areo","Artos","Alyn","Alester",
					"Bran","Bronn","Benjen","Brynden","Beric","Balon","Bowen","Brandon","Barthogan","Beron",
					"Craster","Cregan","Cregard",
					"Davos","Daario","Doran","Darrik","Dyron","Duncan",
					"Eddard","Edric","Euron","Edmure",
					"Gendry","Gilly","Gregor","Garth","Gwayne",
					"Hoster","Hardwin","Harlen",
					"Illyrio",
					"Jon","Jaime","Jorah","Joffrey","Jeor","Jaqen","Jojen","Janos","Jonnel","Jory",
					"Kevan","Karlon",
					"Lancel","Loras","Luceon","Lothar","Lyonel",
					"Maekar","Mace","Mance","Meribald","Martyn",
					"Nestor",
					"Oberyn","Osric",
					"Petyr","Podrick","Perwyn",
					"Quentyn","Qyburn",
					"Robert","Robb","Ramsay","Roose","Rickon","Rickard","Rhaegar","Renly","Rodrik","Randyll",
					"Samwell","Sandor","Stannis","Stefon","Syrio","Symond",
					"Tywin","Tyrion","Theon","Tormund","Trystane","Tommen","Thoros","Tycho","Tomard",
					"Val","Varys","Viserys","Victarion","Vimar",
					"Walder","Wyman","Walys",
					"Yoren","Yohn","Yezzan",
					"Zane",
				});
			}
			else
			{
				// Female
				nicknames.AddRange(new string[] {
					"Arya","Alys","Arianne","Asha","Alaysha","Alissa","Arby","Avelley","Anya","Amerei","Alla",
					"Brienne","Bryna",
					"Catelyn","Cersei","Carlys","Chayle",
					"Daenerys","Dorea","Dyana","Daerya","Daenya","Daella",
					"Elia","Ellaria","Evelyne","Emilee","Elaenys",
					"Haenys","Hemys",
					"Graycie","Gabielle","Genna",
					"Jeyne","Jaeneth","Jocey","Jaennis",
					"Khailey","Kathryn","Khelsie","Kiara","Kristyne",
					"Lyanna","Lysa","Loreza","Laurane",
					"Margaery","Meera","Myrcella","Maella","Mordane","Megga",
					"Nymeria","Naemys","Naesys",
					"Obara","Obella","Olenna",
					"Rina","Rhaerya","Roslin",
					"Shae","Sansa","Selyse","Shireen","Sarella","Serena","Sara",
					"Tyene",
					"Unella",
					"Valeris","Vaehra","Vhaenyra","Vaella",
					"Walda",
					"Ygritte",
				});
			}
			// Adding +10000 possibilities to every nick
			return nicknames[rand.Next(nicknames.Count)] + rand.Next(10000).ToString().PadLeft(4, '0');
		}
		public void CreateCharacter()
		{
			Window w = Window.Get;
			string CreatingCharacterRace = "CH";
			WinAPI.InvokeIfRequired(w.Settings_cmbxCreateCharRace, () => {
				CreatingCharacterRace = w.Settings_cmbxCreateCharRace.Text;
			});
			bool sucess = PacketBuilder.CreateCharacter(CreatingCharacterName, CreatingCharacterMale, CreatingCharacterRace);
			CreatingCharacterName = "";
			if (sucess)
			{
				Window.Get.LogProcess("Creating character...");
			}
		}
		/// <summary>
		/// Returns the current party setup used by the GUI.
		/// </summary>
		public byte GetPartySetup()
		{
			Window w =	Window.Get;
			return (byte)
				((w.Party_rbnSetupExpShared.Checked ? Types.PartySetup.ExpShared : 0)
				& (w.Party_rbnSetupItemShared.Checked ? Types.PartySetup.ItemShared : 0)
				& (w.Party_cbxSetupMasterInvite.Checked ? 0: Types.PartySetup.AnyoneCanInvite ));
		}
		#endregion

		#region (Game & Bot Events)
		/// <summary>
		/// Called when the account has been logged succesfully and the Agent has been connected.
		/// </summary>
		private void Event_Connected()
		{

		}
		/// <summary>
		/// Called when on character selection but only if the AutoLogin fails.
		/// </summary>
		public void Event_CharacterListing()
		{
			Window w = Window.Get;
			Info i = Info.Get;

			// Reset value
			CreatingCharacterName = "";
			// Delete characters that are not being deleted
			if (w.Settings_cbxDeleteChar40to50.Checked)
			{
				foreach (SRObject character in i.CharacterList)
				{
					if (!(bool)character[SRAttribute.isDeleting]
						&& (byte)character[SRAttribute.Level] >= 40
						&& (byte)character[SRAttribute.Level] <= 50)
					{
						w.Log("Deleting character [" + (string)character[SRAttribute.Name] + "]");
						w.LogProcess("Deleting...");
						PacketBuilder.DeleteCharacter((string)character[SRAttribute.Name]);
						Thread.Sleep(1000);
					}
				}
			}
			// Select the first character available
			if (w.Settings_cbxSelectFirstChar.Checked)
			{
				foreach (SRObject character in i.CharacterList)
				{
					if (!(bool)character[SRAttribute.isDeleting])
					{
						w.LogProcess("Selecting...");
						WinAPI.InvokeIfRequired(w, () => {
							w.Login_cmbxCharacter.Text = (string)character[SRAttribute.Name];
							w.Control_Click(w.Login_btnStart, null);
						});
						return;
					}
				}
				w.Log("No characters availables to select!");
			}
			// No characters selected, then create it?
			if (w.Settings_cbxCreateChar.Checked && i.CharacterList.Count == 0)
			{
				w.Log("Empty character list, creating character...");
				CreateNickname();
			}
			else if (w.Settings_cbxCreateCharBelow40.Checked)
			{
				if (i.CharacterList.Count < 4)
				{
					bool notFound = true;
					foreach (SRObject character in i.CharacterList)
					{
						if (!(bool)character[SRAttribute.isDeleting]
							&& (byte)character[SRAttribute.Level] < 40)
						{
							notFound = false;
						}
					}
					if (notFound)
					{
						w.Log("No characters below Lv.40,..");
						CreateNickname();
					}
				}
				else
				{
					w.Log("Character creation is full, cannot create more characters!");
				}
			}
		}
		/// <summary>
		/// Called when the character start loading from any teleport.
		/// </summary>
		private void Event_Teleporting()
		{
			if (inGame)
			{
				Window.Get.LogProcess("Teleporting...");
			}
			else
			{
				Window.Get.LogProcess("Loading...");
				Settings.LoadCharacterSettings();
      }
		}
		/// <summary>
		/// Just before <see cref="Event_Teleported"/> is called. Generated only once per character login.
		/// </summary>
		private void Event_GameJoined()
		{
			Window w = Window.Get;
			Info i = Info.Get;

			w.Log("Joined successfully to the game");
			w.LogChatMessage(w.Chat_rtbxAll, "(Welcome)",i.GetUIFormat("UIIT_STT_STARTING_MSG").Replace("\\n","\n"));
			if (!Proxy.ClientlessMode && w.Login_cbxGoClientless.Checked)
			{
				(new Thread((ThreadStart)delegate{
					for (byte sec = 10; sec > 0; sec--)
					{
						w.LogProcess("Closing client in "+ sec + " seconds...");
						Thread.Sleep(1000);
					}
					w.LogProcess("Closing client...");
					Proxy.CloseClient();
				})).Start();
      }
		}
		/// <summary>
		/// Called right before all character data is saved & spawn packet is detected from client.
		/// </summary>
		private void Event_Teleported()
		{
			Window.Get.LogProcess("Teleported");
			// Recommended to wait 5 seconds to do some action

		}
		/// <summary>
		/// Called only when the maximum level has been increased.
		/// </summary>
		public void Event_LevelUp(byte level)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if(w.Character_cbxMessageExp.Checked)
				w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_STRGERR_LEVEL", level));

			// Up stats points, skills, etc..
		}
		/// <summary>
		/// Called when the Health or Mana from the character has changed.
		/// </summary>
		public void Event_StateUpdated()
		{
			// Check for pots, skills, etc..
		}
		/// <summary>
		/// Called everytime a party invitation is detected
		/// </summary>
		/// <param name="uniqueID">How send the invitation</param>
		public void Event_PartyInvitation(uint uniqueID,byte PartySetup)
		{
			// Get entity
			Info i = Info.Get;
			SRObject player = i.GetEntity(uniqueID);
			
			// Check GUI
			Window w = Window.Get;

			// All
			if (w.Party_cbxAcceptAll.Checked)
			{
				if (w.Party_cbxAcceptOnlyPartySetup.Checked)
				{
					// Exactly same party setup?
					if (GetPartySetup() == PartySetup){
						PacketBuilder.PlayerInvitationResponse(true);
					}
					else if (w.Party_cbxRefusePartys.Checked)
					{
						PacketBuilder.PlayerInvitationResponse(false);
					}
				}
				else
				{
					PacketBuilder.PlayerInvitationResponse(true);
				}
				// Highest priority, has no sense continue checking ..
				return;
			}
			// Check party list
			if (w.Party_cbxAcceptPlayerList.Checked)
			{
				bool found = false;
				string name = (string)player[SRAttribute.Name];
				WinAPI.InvokeIfRequired(w.Party_lstvPlayers, () => {
					for (int j = 0; j < w.Party_lstvPlayers.Items.Count; j++)
					{
						if (w.Party_lstvPlayers.Items[j].Text.Equals(name, StringComparison.OrdinalIgnoreCase))
						{
							found = true;
							break;
						}
					}
				});
				if (found)
				{
					if (w.Party_cbxAcceptOnlyPartySetup.Checked)
					{
						if (GetPartySetup() == PartySetup)
						{
							PacketBuilder.PlayerInvitationResponse(true);
							return;
						}
					}
					else
					{
						PacketBuilder.PlayerInvitationResponse(true);
						return;
					}
				}
			}
			// Check leader list
			if (w.Party_cbxAcceptLeaderList.Checked)
			{
				bool found = false;
				string name = (string)player[SRAttribute.Name];
				WinAPI.InvokeIfRequired(w.Party_lstvLeaders, () => {
					for (int j = 0; j < w.Party_lstvLeaders.Items.Count; j++)
					{
						if (w.Party_lstvLeaders.Items[j].Text.Equals(name, StringComparison.OrdinalIgnoreCase))
						{
							found = true;
							break;
						}
					}
				});
				if (found)
				{
					if (w.Party_cbxAcceptOnlyPartySetup.Checked)
					{
						if (GetPartySetup() == PartySetup)
						{
							PacketBuilder.PlayerInvitationResponse(true);
							return;
						}
					}
					else
					{
						PacketBuilder.PlayerInvitationResponse(true);
						return;

					}
				}
			}
			if (w.Party_cbxRefusePartys.Checked)
			{
				PacketBuilder.PlayerInvitationResponse(false);
			}
		}
		/// <summary>
		/// Called when has been joined to the party and all data is loaded.
		/// </summary>
		public void Event_PartyJoined(byte PartySetupType,byte PartyPurposeType)
		{
			Window w = Window.Get;

			this.PartySetupType = (sbyte)PartySetupType;
			this.PartyPurposeType = PartyPurposeType;

			// Leave party if none leader is found
			if (w.Party_cbxLeavePartyNoneLeader.Checked)
			{
				Info i = Info.Get;
				
				bool NotFound = true;
				SRObject[] players = i.PartyList.ToArray();

				WinAPI.InvokeIfRequired(w.Party_lstvLeaders, () => {
					foreach (SRObject member in players)
					{
						if (w.Party_lstvLeaders.Items.ContainsKey(((string)member[SRAttribute.Name]).ToLower()))
						{
							NotFound = false;
							break;
						}
					}
				});
				if (NotFound)
				{
					PacketBuilder.LeaveParty();
				}
			}
		}
		/// <summary>
		/// Called when the character has left the party group.
		/// </summary>
		public void Event_PartyLeaved()
		{
			// Create again pt match, etc..
			PartySetupType = -1;
		}
		/// <summary>
		/// Called when the current agent connection is lost.
		/// </summary>
		private void Event_Disconnected()
		{
			PartySetupType = -1;
    }
		/// <summary>
		/// Called when a (new) entity appears.
		/// </summary>
		public void Event_Spawn(SRObject entity)
		{
			// invite to party, ETC...
		}
		#endregion

		#region (Event hooks & game system logic)
		public void _Event_Connected()
		{
			Event_Connected();
		}
		public void _Event_Disconnected()
		{
			_HWIDSent = false;
			_triggerJoinedToGame = false;
			Event_Disconnected();
		}
		public void _Event_Teleported()
		{
			if (!_triggerJoinedToGame)
			{
				Window w = Window.Get;
        WinAPI.InvokeIfRequired(w, () => {
					w.Text = "xBot - [" + Info.Get.Server + "] " + Info.Get.Charname;
					w.NotifyIcon.Text = w.Text;
        });
				_triggerJoinedToGame = true;
				Event_GameJoined();
			}
			Event_Teleported();
		}
		public void _Event_Teleporting()
		{
			Window w = Window.Get;
			Info i = Info.Get;

			// Clar entity data
			i.EntityList.Clear();
			w.GameInfo_Clear();
			w.Minimap_ObjectPointer_Clear();

			// Clear party data
			w.Party_Clear();
			i.PartyList.Clear();
			
			Event_Teleporting();
		}
		public void _Event_Spawn(SRObject entity)
		{
			Info.Get.EntityList.Add(entity);

			Window w = Window.Get;
			w.GameInfo_AddSpawn(entity);
			if(!entity.Contains(SRAttribute.Region)
				|| !entity.Contains(SRAttribute.X)
				|| !entity.Contains(SRAttribute.Y)
				|| !entity.Contains(SRAttribute.Z)
				|| !entity.Contains(SRAttribute.UniqueID)
				|| !entity.Contains(SRAttribute.Servername)
				|| !entity.Contains(SRAttribute.Name))
			{
				int wtf = 0;
				wtf++;
			}

			w.Minimap_ObjectPointer_Add((uint)entity[SRAttribute.UniqueID], (string)entity[SRAttribute.Servername], (string)entity[SRAttribute.Name], (int)entity[SRAttribute.X], (int)entity[SRAttribute.Y], (int)entity[SRAttribute.Z], (ushort)entity[SRAttribute.Region]);

			Event_Spawn(entity);
		}
		public void _Event_Despawn(uint uniqueID)
		{
			Info i = Info.Get;
			i.EntityList.Remove(i.GetEntity(uniqueID));

			Window w = Window.Get;
      w.GameInfo_RemoveSpawn(uniqueID);
			w.Minimap_ObjectPointer_Remove(uniqueID);
		}
		public void _Event_PartyLeaved()
		{
			Info.Get.PartyList.Clear();

			Event_PartyLeaved();
    }
		#endregion
	}
}