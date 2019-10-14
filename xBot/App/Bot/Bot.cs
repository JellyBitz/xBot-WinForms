using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using xBot.Game;
using xBot.Game.Objects;

namespace xBot.App
{
	/// <summary>
	/// Handle everything about bot logic.
	/// </summary>
	public partial class Bot
	{
		/// <summary>
		/// Unique instance of this class.
		/// </summary>
		private static Bot _this = null;
		private Random rand = new Random();
		/// <summary>
		/// Check if the bot is using auto login mode from command line.
		/// </summary>
		public bool hasAutoLoginMode { get; set; }
		/// <summary>
		/// Get or set the proxy actually running.
		/// </summary>
		public Proxy Proxy { get; set; }
		/// <summary>
		/// Gets or set if the login has been sent through bot.
		/// </summary>
		public bool LoggedFromBot { get; set; }
		/// <summary>
		/// Check if the character is on creation process.
		/// </summary>
		public bool isCreatingCharacter { get { return !string.IsNullOrEmpty(CreatingCharacterName); } }
		private string CreatingCharacterName;
		private bool CreatingCharacterMale;
		/// <summary>
		/// Check if the character is in game.
		/// </summary>
		public bool inGame { get; private set; }
		/// <summary>
		/// Check if the character is in game.
		/// </summary>
		public bool inTeleport { get;  private set; }
		/// <summary>
		/// Check if the character is in party.
		/// </summary>
		public bool inParty { get; private set; }
		/// <summary>
		/// Keep the current party setup. 
		/// </summary>
		private Types.PartySetup PartySetupFlags;
		/// <summary>
		/// Keep the current party purpose type. 
		/// </summary>
		private Types.PartyPurpose PartyPurposeType;
		/// <summary>
		/// Keep the last entity selected by the character
		/// </summary>
		private uint EntitySelected;
		/// <summary>
		/// Check if the character has his own stall opened.
		/// </summary>
		public bool hasStall { get; private set; }
		/// <summary>
		/// Check if the character is inside of stall, including his own.
		/// </summary>
		public bool inStall { get; private set; }
		/// <summary>
		/// Check if the character is inside of stall, including his own.
		/// </summary>
		public bool inTrace { get; private set; }
		private string TracePlayerName;
		/// <summary>
		/// Constructor.
		/// </summary>
		private Bot()
		{
			InitializeTimers();
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
		/// <summary>
		/// Log to a file the errors with the packet if is needed.
		/// </summary>
		public void LogError(string error, Packet packet = null)
		{
			string msg = DateTime.Now.ToString("[dd/MM/yyyy|HH:mm:ss]") + error + Environment.NewLine;
			if (packet != null)
				msg += "[" + packet.Opcode.ToString("X4") + "][" + WinAPI.ToHexString(packet.GetBytes()) + "]" + Environment.NewLine;
			File.AppendAllText("erros.log", msg);
		}

		#region (HWID setup)
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
		/// <summary>
		/// Saves the hwid data to be used later.
		/// </summary>
		public void SaveHWID(byte[] data)
		{
			Window.Get.LogProcess("HWID Detected : " + WinAPI.ToHexString(data));
			File.WriteAllBytes("Data\\" + Info.Get.Silkroad + ".hwid", data);
		}
		/// <summary>
		/// Loads the HWID previously saved. Returns null if is not found.
		/// </summary>
		public byte[] LoadHWID()
		{
			if (_HWIDSendOnlyOnce && _HWIDSent)
				return null;
			if (File.Exists("Data\\" + Info.Get.Silkroad + ".hwid"))
			{
				_HWIDSent = true;
				return File.ReadAllBytes("Data\\" + Info.Get.Silkroad + ".hwid");
			}
			return null;
		}
		#endregion

		#region (Methods)
		private void CreateNickname()
		{
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Settings_tbxCustomName, () => {
				CreatingCharacterName = w.Settings_tbxCustomName.Text;
			});

			if (CreatingCharacterName == "")
			{
				WinAPI.InvokeIfRequired(w.Settings_cmbxCreateCharGenre, () => {
					CreatingCharacterName = GetRandomNickname(w.Settings_cmbxCreateCharGenre.Text);
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
						if (NextNumber.Length > w.Settings_tbxCustomSequence.MaxLength)
						{
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
		public string GetRandomNickname(string nameGenre)
		{
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
		private void CreateCharacter()
		{
			Window w = Window.Get;
			string CreatingCharacterRace = "CH";
			WinAPI.InvokeIfRequired(w.Settings_cmbxCreateCharRace, () => {
				CreatingCharacterRace = w.Settings_cmbxCreateCharRace.Text;
			});
			bool success = PacketBuilder.CreateCharacter(CreatingCharacterName, CreatingCharacterMale, CreatingCharacterRace);
			CreatingCharacterName = "";
			if (success)
			{
				Window.Get.LogProcess("Creating character...");
			}
		}
		/// <summary>
		/// Get's the last uniqueID selected.
		/// </summary>
		public uint GetEntitySelected() {
			return EntitySelected;
		}
		/// <summary>
		/// Returns the current party setup used by the GUI.
		/// </summary>
		public Types.PartySetup GetPartySetup()
		{
			Window w = Window.Get;
			return ((w.Party_rbnSetupExpShared.Checked ? Types.PartySetup.ExpShared : 0)
				| (w.Party_rbnSetupItemShared.Checked ? Types.PartySetup.ItemShared : 0)
				| (w.Party_cbxSetupMasterInvite.Checked ? 0 : Types.PartySetup.AnyoneCanInvite));
		}
		/// <summary>
		/// Returns the current party match setup used by the GUI.
		/// </summary>
		public SRPartyMatch GetPartyMatchSetup()
		{
			Window w = Window.Get;
			SRPartyMatch match = new SRPartyMatch(0);

			WinAPI.InvokeIfRequired(w.Party_tbxMatchTitle, ()=>{
				match.Title = w.Party_tbxMatchTitle.Text;
			});
			WinAPI.InvokeIfRequired(w.Party_tbxMatchFrom, () => {
				match.LevelMin = byte.Parse(w.Party_tbxMatchFrom.Text);
			});
			WinAPI.InvokeIfRequired(w.Party_tbxMatchTo, () => {
				match.LevelMax = byte.Parse(w.Party_tbxMatchTo.Text);
			});

			if (inParty)
				match.Setup = PartySetupFlags;
			else
				match.Setup = GetPartySetup();

			Info i = Info.Get;
			if (i.Character.hasJobMode())
			{
				if ((Types.Job)i.Character[SRProperty.JobType] == Types.Job.Thief)
					match.Purpose = Types.PartyPurpose.Thief;
				else
					match.Purpose = Types.PartyPurpose.Trader;
			}
			else
			{
				match.Purpose = Types.PartyPurpose.Hunting;
			}
			return match;
		}
		/// <summary>
		/// Search for specific type ID's item in the inventory. Return success.
		/// </summary>
		/// <param name="ID2">type id #2</param>
		/// <param name="ID3">type id #3</param>
		/// <param name="ID4">type id #4</param>
		/// <param name="slot">Invenory slot found</param>
		/// <param name="servername">Rule the search to contains string specified</param>
		public bool FindItem(byte ID2, byte ID3, byte ID4, ref byte slot, string servername = "")
		{
			SRObjectCollection inventory = ((SRObjectCollection)Info.Get.Character[SRProperty.Inventory]).Clone();
			for (byte i = 13; i < inventory.Capacity; i++)
			{
				if (inventory[i] != null)
				{
					// ID1 = Item (3)
					if (inventory[i].isType(3, ID2, ID3, ID4) && inventory[i].ServerName.Contains(servername))
					{
						slot = i;
						return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Try to change to clientless mode.
		/// </summary>
		public void GoClientless()
		{
			if (!Proxy.ClientlessMode)
			{
				Window w = Window.Get;
				System.Timers.Timer CloseClient = new System.Timers.Timer();
				CloseClient.Interval = 1000;

				byte s = 5;
				CloseClient.Elapsed += delegate	{
					try{
						if (s > 0){
							w.LogProcess("Closing client in " + s + " seconds...");
							s = (byte)(s - 1);
						}
						else
						{
							w.LogProcess("Closing client...");
							Proxy.CloseClient();
							CloseClient.Stop();
						}
					}
					catch{ }
				};
				CloseClient.AutoReset = true;
				CloseClient.Start();
			}
		}
		/// <summary>
		/// Try to use a return scroll from inventory.
		/// </summary>
		public bool UseReturnScroll()
		{
			SRObjectCollection inventory = ((SRObjectCollection)Info.Get.Character[SRProperty.Inventory]).Clone();
			for (byte j = 13; j < inventory.Capacity; j++)
			{
				if (inventory[j] != null && inventory[j].isType(3, 3, 3, 1))
				{
					switch (inventory[j].ServerName)
					{
						case "ITEM_ETC_SCROLL_RETURN_01":
						case "ITEM_ETC_SCROLL_RETURN_02":
						case "ITEM_ETC_SCROLL_RETURN_03":
						case "ITEM_ETC_SCROLL_RETURN_NEWBIE_01":
						case "ITEM_ETC_E041225_SANTA_WINGS":
						case "ITEM_MALL_RETURN_SCROLL_HIGH_SPEED":
						case "ITEM_EVENT_RETURN_SCROLL_HIGH_SPEED":
							PacketBuilder.UseItem(inventory[j], j);
							return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Starts tracing a player.
		/// </summary>
		public bool StartTrace(string PlayerName)
		{
			if (inGame)
			{
				inTrace = true;
				SetTraceName(PlayerName);
				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Training_btnTraceStart, () => {
					w.Training_btnTraceStart.Text = "STOP";
				});
				return true;
			}
			return false;
		}
		public void SetTraceName(string PlayerName)
		{
			// Normalize Key
			TracePlayerName = PlayerName.Trim().ToUpper();
			// Check if player is around and move it
			SRObject player;
			if (inTrace && Info.Get.PlayersNear.TryGetValue(TracePlayerName, out player)){
				MoveTo(player.GetPosition());
			}
		}
		/// <summary>
		/// Try to stop the trace.
		/// </summary>
		public bool StopTrace()
		{
			if (inTrace)
			{
				inTrace = false;
				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Training_btnTraceStart, ()=>{
					w.Training_btnTraceStart.Text = "START";
				});
				return true;
			}
			return false;
		}
		/// <summary>
		/// Move the character to the position specified.
		/// </summary>
		public void MoveTo(SRCoord position)
		{
			Info i = Info.Get;
			if ((bool)i.Character[SRProperty.isRiding])
			{
				PacketBuilder.MoveTo(position, (uint)i.Character[SRProperty.RidingUniqueID]);
			}
			else
			{
				PacketBuilder.MoveTo(position);
			}
		}
		/// <summary>
		/// Try to use and item at the slot specified but only if it's possible to use. Return success.
		/// </summary>
		public bool UseItem(byte slotInventory)
		{
			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
      if (slotInventory >= 13 && slotInventory < inventory.Capacity)
			{
				if (inventory[slotInventory] != null)
				{
					switch (inventory[slotInventory].ID2)
					{
						case 2: // Summon scroll
							PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
							return true;
						case 3: // Usable
							switch(inventory[slotInventory].ID3)
							{
								case 1: // Potions
									switch (inventory[slotInventory].ID4)
									{
										case 1: // HP
										case 3: // MP
										case 2: // Vigor
											PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
											return true;
									}
									break;
								case 2: // Pills
									switch (inventory[slotInventory].ID4)
									{
										case 1: // Universal
										case 6: // Purification
											PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
											return true;
									}
									break;
								case 3: // Event, vehicles, etc.
									switch (inventory[slotInventory].ID4)
									{
										case 1: // All kind of scrolls, even customized ones (it can cause disconnect)
										case 2: // Vehicle, Transport
										case 6: // Fortress summon pet
										case 7: // Fortress summon guard
										case 9: // Fortress battle flag
										case 10: // Exp/SP scroll
										case 11: // Fortress summon unique
										case 12: // Skill Points scroll
											PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
											return true;
									}
									break;
								case 13: // Buff scroll
									PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
									return true;
								case 15: // Monster scroll
									PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
									return true;
							}
							break;
					}

					if(inventory[slotInventory].ID2 == 3 )
					{
						if (inventory[slotInventory].ID3 >= 1 && inventory[slotInventory].ID3 <= 3)
						{
							PacketBuilder.UseItem(inventory[slotInventory], slotInventory);
							return true;
						}
					}
				}
			}
			return false;
		}
		#endregion
	}
}