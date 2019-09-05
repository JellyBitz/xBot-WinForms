using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using xBot.Game;

namespace xBot
{
	/// <summary>
	/// Handle everything about bot logic.
	/// </summary>
	public partial class Bot
	{
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
		public bool hasParty { get { return PartySetupType != -1; } }
		/// <summary>
		/// Keep the current party setup type or (-1) if is not in party. 
		/// </summary>
		private sbyte PartySetupType = -1;
		/// <summary>
		/// Keep the current party purpose type or (-1) if is not in party. 
		/// </summary>
		private sbyte PartyPurposeType = -1;
		/// <summary>
		/// Keep the last entity selected by the character
		/// </summary>
		private uint EntitySelected;
		/// <summary>
		/// Check if the character has his own stall opened.
		/// </summary>
		public bool hasStall { get { return _hasStall; } }
		private bool _hasStall;
		/// <summary>
		/// Check if the character is inside of stall, including his own.
		/// </summary>
		public bool inStall { get { return _inStall; } }
		private bool _inStall;
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
		/// Returns the current party setup used by the GUI.
		/// </summary>
		public byte GetPartySetup()
		{
			Window w = Window.Get;
			return (byte)
				((w.Party_rbnSetupExpShared.Checked ? Types.PartySetup.ExpShared : 0)
				| (w.Party_rbnSetupItemShared.Checked ? Types.PartySetup.ItemShared : 0)
				| (w.Party_cbxSetupMasterInvite.Checked ? 0 : Types.PartySetup.AnyoneCanInvite));
		}
		/// <summary>
		/// Get's the last uniqueID selected.
		/// </summary>
		public uint GetEntitySelected() {
			return EntitySelected;
		}
		/// <summary>
		/// Search for specific type ID's item in the inventory. Return success.
		/// </summary>
		/// <param name="tid2">type id #2</param>
		/// <param name="tid3">type id #3</param>
		/// <param name="tid4">type id #4</param>
		/// <param name="slot">Invenory slot found</param>
		/// <param name="servername">Rule the search to contains string specified</param>
		public bool FindItem(byte tid2, byte tid3, byte tid4, ref byte slot, string servername = "")
		{
			SRObjectCollection inventory = ((SRObjectCollection)Info.Get.Character[SRAttribute.Inventory]).Clone();
			for (byte i = 13; i < inventory.Capacity; i++)
			{
				if (inventory[i] != null)
				{
					// tid1 = item (3)
					if (inventory[i].Equals(3, tid2, tid3, tid4) && ((string)inventory[i][SRAttribute.Servername]).Contains(servername))
					{
						slot = i;
						return true;
					}
				}
			}
			return false;
		}
		public bool FindPet(ref byte slot,uint modelID)
		{
			SRObjectCollection inventory = ((SRObjectCollection)Info.Get.Character[SRAttribute.Inventory]).Clone();
			for (byte i = 13; i < inventory.Capacity; i++)
			{
				if (inventory[i] != null)
				{
					// pet summon scroll
					if (inventory[i].Equals(3,2,1,1)
						&& (uint)inventory[i][SRAttribute.ModelID] == modelID)
					{
						slot = i;
						return true;
					}
				}
			}
			return false;
		}
		#endregion
	}
}