using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using xBot.Game;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;
using System.Threading;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Item;

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
		/// Check if the character is in trace mode.
		/// </summary>
		public bool inTrace { get; private set; }
		private string TracePlayerName;
		/// <summary>
		/// Check if is sorting any inventory.
		/// </summary>
		public bool isSorting { get { return tSorting != null; } }
		private Thread tSorting;
		/// <summary>
		/// Ping check by command
		/// </summary>
		private System.Diagnostics.Stopwatch m_Ping;
		/// <summary>
		/// Check if bot is recording a walking script.
		/// </summary>
		public bool isRecording { get; private set; }

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
		public void LogError(string message,Exception ex,Packet packet = null)
		{
			string msg = DateTime.Now.ToString("[dd/MM/yyyy|HH:mm:ss]") +"[" + message + "][" + ex.Message + "][" + ex.StackTrace + "]" + Environment.NewLine;
			if (packet != null)
				msg += packet.ToString() + Environment.NewLine;
			File.AppendAllText("erros.log", msg);
		}

		#region (Extended Protocol Setup)
		public void SetExtendedProtocol()
		{

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
		/// Returns the max. member count from the current party.
		/// </summary>
		public SRTypes.Weapon GetWeaponUsed()
		{
			SRItem weapon = InfoManager.Character.Inventory[6];
			if(weapon == null)
				return SRTypes.Weapon.None;
			return (SRTypes.Weapon)weapon.ID4;
		}
		/// <summary>
		/// Search for specific type ID's item in the inventory. Return success.
		/// </summary>
		/// <param name="ID2">type id #2</param>
		/// <param name="ID3">type id #3</param>
		/// <param name="ID4">type id #4</param>
		/// <param name="slot">Inventory slot found</param>
		/// <param name="servername">Rule the search to contains string specified</param>
		public bool FindItem(byte ID2, byte ID3, byte ID4, ref byte slot, string servername = "")
		{
			int index = InfoManager.Character.Inventory.FindIndex(i => i != null  && i.isType(ID2, ID3, ID4) && i.ServerName.Contains(servername),13);
			if(index == -1)
				return false;
			else
			{
				slot = (byte)index;
				return true;
			}
		}
		/// <summary>
		/// Try to change to clientless mode.
		/// </summary>
		public void GoClientless()
		{
			if (!Proxy.ClientlessMode)
			{
				Window w = Window.Get;
				System.Timers.Timer CloseClient = new System.Timers.Timer(1000);

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
			xList<SRItem> inventory = InfoManager.Character.Inventory;
			for (byte j = 13; j < inventory.Capacity; j++)
			{
				if (inventory[j] != null && inventory[j].isType(3, 3, 1))
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
			if (InfoManager.inGame)
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
			if (inTrace)
			{
				// Check if player is around and move it
				SRPlayer player = InfoManager.Players[TracePlayerName];
				if (player != null){
					MoveTo(player.GetRealtimePosition());
				}
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
			if (InfoManager.Character.isRiding)
				PacketBuilder.MoveTo(position,InfoManager.Character.RidingUniqueID);
			else
				PacketBuilder.MoveTo(position);
		}
		/// <summary>
		/// Try to use and item at the slot specified. Return success.
		/// </summary>
		public bool UseItem(byte slotInventory)
		{
			xList<SRItem> inventory = InfoManager.Character.Inventory;
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
										case 1: // All kind of scrolls, return scrolls, even customized ones (it can cause disconnect)
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
				}
			}
			return false;
		}
		/// <summary>
		/// Try to equip or unequip an item from the inventory. Return success.
		/// </summary>
		public bool EquipItem(byte slotInventory,bool useInventoryAvatar = false)
		{
			SRItem item = InfoManager.Character.Inventory[slotInventory];
			if(item != null && item.isEquipable())
			{
				if (slotInventory < 13)
				{
					// UnEquip

					// Find an empty slot
					int newSlot = InfoManager.Character.Inventory.FindIndex(i => i == null,13);
					if (newSlot != -1){
						PacketBuilder.MoveItem(slotInventory, (byte)newSlot, useInventoryAvatar ? SRTypes.InventoryItemMovement.AvatarToInventory : SRTypes.InventoryItemMovement.InventoryToInventory);
						return true;
					}
				}
				else
				{
					// Equip
					switch ((SRTypes.Equipable)item.ID3)
					{
						case SRTypes.Equipable.Garment: // GARMENT
						case SRTypes.Equipable.Protector: // PROTECTOR
						case SRTypes.Equipable.Armor: // ARMOR
						case SRTypes.Equipable.Robe: // ROBE
						case SRTypes.Equipable.LightArmor: // LIGHT ARMOR
						case SRTypes.Equipable.HeavyArmor: // HEAVY ARMOR
							switch ((SRTypes.SetPart)item.ID4)
							{
								case SRTypes.SetPart.Head: // HEAD
									PacketBuilder.MoveItem(slotInventory, 0, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.SetPart.Shoulders: // SHOULDERS
									PacketBuilder.MoveItem(slotInventory, 2, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.SetPart.Chest: // CHEST
									PacketBuilder.MoveItem(slotInventory, 1, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.SetPart.Pants: // PANTS
									PacketBuilder.MoveItem(slotInventory, 4, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.SetPart.Gloves: // GLOVES
									PacketBuilder.MoveItem(slotInventory, 3, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.SetPart.Boots: // BOOTS
									PacketBuilder.MoveItem(slotInventory, 5, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
							}
							break;
						case SRTypes.Equipable.Shield:
							PacketBuilder.MoveItem(slotInventory, 7, SRTypes.InventoryItemMovement.InventoryToInventory);
							return true;
						case SRTypes.Equipable.AccesoriesCH: // ACCESSORIES (CH)
						case SRTypes.Equipable.AccesoriesEU: // ACCESSORIES (EU)
							switch ((SRTypes.AccesoriesPart)item.ID4)
							{
								case SRTypes.AccesoriesPart.Earring: // Earring
									PacketBuilder.MoveItem(slotInventory, 9, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.AccesoriesPart.Necklace: // Necklace
									PacketBuilder.MoveItem(slotInventory, 10, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
								case SRTypes.AccesoriesPart.Ring: // Ring
									if(InfoManager.Character.Inventory[12] == null)
										PacketBuilder.MoveItem(slotInventory, 12, SRTypes.InventoryItemMovement.InventoryToInventory);
									else
										PacketBuilder.MoveItem(slotInventory, 11, SRTypes.InventoryItemMovement.InventoryToInventory);
									return true;
							}
							break;
						case SRTypes.Equipable.Weapon: // WEAPONS (CH & EU)
							PacketBuilder.MoveItem(slotInventory, 6, SRTypes.InventoryItemMovement.InventoryToInventory);
							return true;
						case SRTypes.Equipable.Job: // JOB SUIT
							PacketBuilder.MoveItem(slotInventory, 8, SRTypes.InventoryItemMovement.InventoryToInventory);
							return true;
						case SRTypes.Equipable.Avatar: // Avatar
							switch ((SRTypes.AvatarPart)item.ID4)
							{
								case SRTypes.AvatarPart.Hat: // Hat
									PacketBuilder.MoveItem(slotInventory, 0, SRTypes.InventoryItemMovement.InventoryToAvatar);
									return true;
								case SRTypes.AvatarPart.Dress: // Dress
									PacketBuilder.MoveItem(slotInventory, 1, SRTypes.InventoryItemMovement.InventoryToAvatar);
									return true;
								case SRTypes.AvatarPart.Accessory: // Accessory
									PacketBuilder.MoveItem(slotInventory, 2, SRTypes.InventoryItemMovement.InventoryToAvatar);
									return true;
								case SRTypes.AvatarPart.Flag: // Flag
									PacketBuilder.MoveItem(slotInventory, 3, SRTypes.InventoryItemMovement.InventoryToAvatar);
									return true;
							}
							break;
						case SRTypes.Equipable.DevilSpirit: // Devil Spirit
							PacketBuilder.MoveItem(slotInventory, 4, SRTypes.InventoryItemMovement.InventoryToAvatar);
							return true;
					}
				}
			}
			return false;
		}
		public void UseTeleportAsync(SRTeleport teleport, uint destinationID)
		{
			if (Proxy.ClientlessMode)
			{
				if (InfoManager.isEntityNear(teleport.UniqueID))
					PacketBuilder.UseTeleport(teleport.UniqueID, destinationID);
				else
					Window.Get.LogProcess(teleport.Name + " cannot be selected!");
			}
			else
			{
			  (new Thread( ()=> {
					if (WaitSelectEntity(teleport.UniqueID, 8, 250, "Selecting teleport " + teleport.TeleportName + "..."))
						PacketBuilder.UseTeleport(teleport.UniqueID, destinationID);
					else
						Window.Get.LogProcess(teleport.Name + " cannot be selected!");
				})).Start();
			}
		}

		public bool StartInventorySort()
		{
			if (InfoManager.inGame && !isSorting)
			{
				tSorting = new Thread(InventorySort);
				tSorting.Priority = ThreadPriority.AboveNormal;
				tSorting.Start();

				Window w = Window.Get;
				w.Inventory_btnItemsSort.InvokeIfRequired(() => {
					w.Inventory_btnItemsSort.Tag = w.Inventory_btnItemsSort.ForeColor;
					w.Inventory_btnItemsSort.ForeColor = System.Drawing.Color.FromArgb(0, 180, 255);
				});
				return true;
			}
			return false;
		}
		public void InventorySort()
		{
			Window w = Window.Get;

			bool sort = true;
			while (InfoManager.inGame && sort)
			{
				sort = false;
				xList<SRItem> inventory =InfoManager.Character.Inventory;
				for (byte j = (byte)(inventory.Capacity - 1); j >= 13; j--)
				{
					if (inventory[j] != null && inventory[j].QuantityMax != 1)
					{
						ushort quantityInitial = inventory[j].Quantity;
						ushort quantityMax = inventory[j].QuantityMax;
						if (quantityInitial < quantityMax)
						{
							for (byte k = 13; k < j; k++)
							{
								// Just in case
								if (inventory[j] == null)
									break;

								if (inventory[k] != null)
								{
									if (inventory[k].ID == inventory[j].ID)
									{
										ushort quantityFinal = inventory[k].Quantity;
										if (quantityFinal < quantityMax)
										{
											w.LogProcess("Sorting (" + j + ") -> (" + k + ") ...");

											ushort quantityMaxMoved = (ushort)(quantityMax - quantityFinal);
											if (quantityInitial <= quantityMaxMoved)
												PacketBuilder.MoveItem(j, k, SRTypes.InventoryItemMovement.InventoryToInventory, quantityInitial);
											else
												PacketBuilder.MoveItem(j++, k, SRTypes.InventoryItemMovement.InventoryToInventory, quantityMaxMoved);
											sort = true;
											InfoManager.MonitorInventoryMovement.WaitOne(1000);
											break;
										}
									}
								}
							}
						}
					}
				}
			}

			w.Inventory_btnItemsSort.InvokeIfRequired(() => {
				w.Inventory_btnItemsSort.ForeColor = (System.Drawing.Color)w.Inventory_btnItemsSort.Tag;
				w.Inventory_btnItemsSort.Tag = null;
			});

			w.LogProcess("Sorting completed");
			tSorting = null;
		}
		public bool StopInventorySort()
		{
			if (isSorting)
			{
				tSorting.Abort();
				tSorting = null;

				Window w = Window.Get;
				w.LogProcess("Sorting stopped");
				w.Inventory_btnItemsSort.InvokeIfRequired(() => {
					w.Inventory_btnItemsSort.ForeColor = (System.Drawing.Color)w.Inventory_btnItemsSort.Tag;
					w.Inventory_btnItemsSort.Tag = null;
				});
				return true;
			}
			return false;
		}
		#endregion
	}
}