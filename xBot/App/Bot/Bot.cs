using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using xBot.Game;
using System.Timers;

namespace xBot
{
	/// <summary>
	/// Handle everything about bot logic.
	/// </summary>
	public partial class Bot
	{
		private static Bot _this = null;
		/// <summary>
		/// Check if the bot is using auto login mode from command line.
		/// </summary>
		public bool isAutoLoginMode { get; set; }
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
		public bool isCreatingCharacter { get { return CreatingCharacterName != ""; } }
		private string CreatingCharacterName;
		private bool CreatingCharacterMale;
		/// <summary>
		/// Check if the character is in game.
		/// </summary>
		public bool inGame { get { return _inGame; } }
		private bool _inGame;
		/// <summary>
		/// Check if the character is in party.
		/// </summary>
		public bool inParty { get { return PartySetupType != -1; } }
		/// <summary>
		/// Keep the current party setup type or (-1) if is not in party. 
		/// </summary>
		private sbyte PartySetupType = -1;
		/// <summary>
		/// Keep the current party purpose type or (-1) if is not in party. 
		/// </summary>
		private sbyte PartyPurposeType = -1;
		/// <summary>
		/// Get or set the last uniqueID selected.
		/// </summary>
		public uint EntitySelected { get { return _EntitySelected; } }
		private uint _EntitySelected;
		/// <summary>
		/// Cooldown timer.
		/// </summary>
		Timer tUsingHP, tUsingMP, tUsingVigor, tUsingUniversal, tUsingPurification;
		private Bot()
		{
			// Preparing all neccesary timers
			tUsingHP = new Timer();
			tUsingMP = new Timer();
			tUsingVigor = new Timer();
			tUsingUniversal = new Timer();
			tUsingPurification = new Timer();
			// A second is enought for any potion cooldown
			tUsingHP.Interval = tUsingMP.Interval = tUsingVigor.Interval =
				tUsingUniversal.Interval = tUsingPurification.Interval = 1000;
			// Callbacks
			tUsingHP.Elapsed += (sender, e) => {
				CheckUsingHP();
			};
			tUsingMP.Elapsed += (sender, e) => {
				CheckUsingMP();
			};
			tUsingVigor.Elapsed += (sender, e) => {
				CheckUsingVigor();
			};
			tUsingUniversal.Elapsed += (sender, e) => {
				CheckUsingUniversal();
			};
			tUsingPurification.Elapsed += (sender, e) => {
				CheckUsingPurification();
			};
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
				msg += "[" + packet.Opcode.ToString("X4") + "][" + WinAPI.BytesToHexString(packet.GetBytes()) + "]" + Environment.NewLine;
			File.AppendAllText("erros.log", msg);
		}

		#region (Extended methods)
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
			Window w = Window.Get;
			return (byte)
				((w.Party_rbnSetupExpShared.Checked ? Types.PartySetup.ExpShared : 0)
				& (w.Party_rbnSetupItemShared.Checked ? Types.PartySetup.ItemShared : 0)
				& (w.Party_cbxSetupMasterInvite.Checked ? 0 : Types.PartySetup.AnyoneCanInvite));
		}
		/// <summary>
		/// Search for specific type ID's item in the inventory. Return success.
		/// </summary>
		/// <param name="tid1">type id #1</param>
		/// <param name="tid2">type id #2</param>
		/// <param name="tid3">type id #3</param>
		/// <param name="tid4">type id #4</param>
		/// <param name="slot">Invenory slot found</param>
		/// <param name="servername">Rule the search to contains string specified</param>
		public bool FindItem(byte tid1, byte tid2, byte tid3, byte tid4, ref byte slot, string servername = "")
		{
			SRObjectCollection inventory = ((SRObjectCollection)Info.Get.Character[SRAttribute.Inventory]).Clone();
			for (byte i = 13; i < inventory.Capacity; i++)
			{
				if (inventory[i] != null)
				{
					if (inventory[i].Equals(tid1, tid2, tid3, tid4) && ((string)inventory[i][SRAttribute.Servername]).Contains(servername))
					{
						slot = i;
						return true;
					}
				}
			}
			return false;
		}
		private void CheckUsingHP()
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseHP.Checked || w.Character_cbxUseHPGrain.Checked)
				{
					byte useHP = 100; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseHP, () => {
						useHP = byte.Parse(w.Character_tbxUseHP.Text);
					});
					if ((int)i.Character.GetHPPercent() < useHP)
					{
						byte slot = 0;
						if (w.Character_cbxUseHPGrain.Checked && FindItem(3, 3, 1, 1, ref slot, "_SPOTION_")
							|| w.Character_cbxUseHP.Checked && FindItem(3, 3, 1, 1, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingHP.Start();
						}
					}
				}
			}
		}
		private void CheckUsingMP()
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseMP.Checked || w.Character_cbxUseMPGrain.Checked)
				{
					byte useMP = 100; // dummy
					WinAPI.InvokeIfRequired(w.Character_tbxUseMP, () =>
					{
						useMP = byte.Parse(w.Character_tbxUseMP.Text);
					});
					if ((int)i.Character.GetMPPercent() < useMP)
					{
						byte slot = 0;
						if (w.Character_cbxUseMPGrain.Checked && FindItem(3, 3, 1, 2, ref slot, "_SPOTION_")
							|| w.Character_cbxUseMP.Checked && FindItem(3, 3, 1, 2, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingMP.Start();
						}
					}
				}
			}
		}
		private void CheckUsingVigor()
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{

				Window w = Window.Get;
				if (w.Character_cbxUseHPVigor.Checked || w.Character_cbxUseMPVigor.Checked)
				{
					byte usePercent = 100;
					WinAPI.InvokeIfRequired(w.Character_tbxUseHPVigor, () => {
						usePercent = byte.Parse(w.Character_tbxUseHPVigor.Text);
					});
					// Check hp %
					if ((int)i.Character.GetHPPercent() < usePercent)
					{
						byte slot = 0;
						if (FindItem(3, 3, 1, 3, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingVigor.Start();
						}
					}
					else
					{
						// Check mp %
						WinAPI.InvokeIfRequired(w.Character_tbxUseMPVigor, () => {
							usePercent = byte.Parse(w.Character_tbxUseMPVigor.Text);
						});
						if ((int)i.Character.GetMPPercent() < usePercent)
						{
							byte slot = 0;
							if (FindItem(3, 3, 1, 3, ref slot))
							{
								PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
								tUsingVigor.Start();
							}
						}
					}
				}
			}
		}
		private void CheckUsingUniversal()
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePillUniversal.Checked)
				{
					Types.BadStatus status = (Types.BadStatus)i.Character[SRAttribute.BadStatusType];
					if (status.HasFlag(Types.BadStatus.Freezing
						| Types.BadStatus.Frostbite
						| Types.BadStatus.ElectricShock
						| Types.BadStatus.Burn
						| Types.BadStatus.Poisoning
						| Types.BadStatus.Fear))
					{
						byte slot = 0;
						if (FindItem(3, 3, 2, 6, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingUniversal.Start();
						}
					}
				}
			}
		}
		private void CheckUsingPurification()
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{
				Window w = Window.Get;
				if (w.Character_cbxUsePillPurification.Checked)
				{
					Types.BadStatus status = (Types.BadStatus)i.Character[SRAttribute.BadStatusType];
					if (status.HasFlag(Types.BadStatus.Bleed))
					{
						byte slot = 0;
						if (FindItem(3, 3, 2, 1, ref slot))
						{
							PacketBuilder.UseItem(((SRObjectCollection)i.Character[SRAttribute.Inventory])[slot], slot);
							tUsingPurification.Start();
						}
					}

				}
			}
		}
		#endregion
	}
}