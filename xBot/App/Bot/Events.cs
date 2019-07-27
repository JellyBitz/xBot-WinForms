using System;
using System.Timers;
using xBot.Game;

namespace xBot
{
	public partial class Bot
	{
		#region (Game Events & Bot Logic)
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
						System.Threading.Thread.Sleep(500);
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
			w.LogChatMessage(w.Chat_rtbxAll, "(Welcome)", i.GetUIFormat("UIIT_STT_STARTING_MSG").Replace("\\n", "\n"));
			if (!Proxy.ClientlessMode && w.Login_cbxGoClientless.Checked)
			{
				Timer CloseClient = new Timer();
				CloseClient.Interval = 1000;
				byte s = 10;
				CloseClient.Elapsed += (sender, e) => {
					try
					{
						if (s > 0)
						{
							w.LogProcess("Closing client in " + s + " seconds...");
							s--;
						}
						else
						{
							w.LogProcess("Closing client...");
							Proxy.CloseClient();
							CloseClient.Enabled = false;
						}
					}
					catch { /* Bot closed */ }
				};
				CloseClient.AutoReset = true;
				CloseClient.Enabled = true;
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

			w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_STRGERR_LEVEL", level));
			// Up skills, etc..
		}
		/// <summary>
		/// Called when the Health, Mana, or BadStatus from the character has changed.
		/// </summary>
		private void Event_StateUpdated()
		{

		}
		/// <summary>
		/// Called everytime a party invitation is detected
		/// </summary>
		/// <param name="uniqueID">How send the invitation</param>
		public void Event_PartyInvitation(uint uniqueID, byte PartySetup)
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
					if (GetPartySetup() == PartySetup)
					{
						PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
					}
					else if (w.Party_cbxRefusePartys.Checked)
					{
						PacketBuilder.PlayerPetitionResponse(false, Types.PlayerPetition.PartyInvitation);
					}
				}
				else
				{
					PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
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
							PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
							return;
						}
					}
					else
					{
						PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
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
							PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
							return;
						}
					}
					else
					{
						PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
						return;

					}
				}
			}
			if (w.Party_cbxRefusePartys.Checked)
			{
				PacketBuilder.PlayerPetitionResponse(false, Types.PlayerPetition.PartyInvitation);
			}
		}
		/// <summary>
		/// Called when has been joined to the party and all data is loaded.
		/// </summary>
		public void Event_PartyJoined(byte PartySetupType, byte PartyPurposeType)
		{
			Window w = Window.Get;

			this.PartySetupType = (sbyte)PartySetupType;
			this.PartyPurposeType = (sbyte)PartyPurposeType;

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
		private void Event_PartyLeaved()
		{
			// Create again pt match, etc..
		}
		/// <summary>
		/// Called when the current agent connection is lost.
		/// </summary>
		private void Event_Disconnected()
		{
			// Start relogin, etc
		}
		/// <summary>
		/// Called when a (new) entity appears.
		/// </summary>
		public void Event_Spawn(SRObject entity)
		{
			// invite to party, etc...
		}
		/// <summary>
		/// Called when a player is giving you resurrection request
		/// </summary>
		public void Event_Resurrection(uint uniqueID)
		{
			Window w = Window.Get;

			if (w.Character_cbxAcceptRess.Checked)
			{
				if (!w.Character_cbxAcceptRessPartyOnly.Checked)
				{
					PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.Resurrection);
				}
				else
				{
					Info i = Info.Get;
					SRObject player = i.GetEntity(uniqueID);
					if(player != null)
					{
						if (i.GetPartyMember((string)player[SRAttribute.Name]) != null)
						{
							PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.Resurrection);
						}
					}
				}
			}
		}
		#endregion
	}
}
