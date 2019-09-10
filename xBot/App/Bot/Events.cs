using System;
using System.Collections.Generic;
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
		/// Called when the current agent connection is lost.
		/// </summary>
		private void Event_Disconnected()
		{
			
		}
		/// <summary>
		/// Called when on character selection but only if the AutoLogin fails.
		/// </summary>
		private void Event_CharacterListing(List<SRObject> CharacterList)
		{
			Window w = Window.Get;

			// Reset value
			CreatingCharacterName = "";
			// Delete characters that are not being deleted
			if (w.Settings_cbxDeleteChar40to50.Checked)
			{
				foreach (SRObject character in CharacterList)
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
				foreach (SRObject character in CharacterList)
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
			if (w.Settings_cbxCreateChar.Checked && CharacterList.Count == 0)
			{
				w.Log("Empty character list, creating character...");
				CreateNickname();
			}
			else if (w.Settings_cbxCreateCharBelow40.Checked)
			{
				if (CharacterList.Count < 4)
				{
					bool notFound = true;
					foreach (SRObject character in CharacterList)
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
        byte s = 5;
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
				CloseClient.Start();
			}

			CheckAutoParty(null, null);
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
		private void Event_StateUpdated(Types.EntityStateUpdate type)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRAttribute.LifeState] == Types.LifeState.Alive)
			{
				switch (type)
				{
					case Types.EntityStateUpdate.HP:
						CheckUsingHP();
						CheckUsingVigor();
						break;
					case Types.EntityStateUpdate.MP:
						CheckUsingMP();
						CheckUsingVigor();
						break;
					case Types.EntityStateUpdate.HPMP:
						CheckUsingHP();
						CheckUsingMP();
						CheckUsingVigor();
						break;
					case Types.EntityStateUpdate.BadStatus:
						CheckUsingUniversal();
						CheckUsingPurification();
						break;
				}
			}
			else
			{
				// Character dead.
				if((byte)i.Character[SRAttribute.Level] <= 10){
					PacketBuilder.ResurrectAtPresentPoint();
				}
			}
		}
		/// <summary>
		/// Called when the Health, or BadStatus from the pet has changed.
		/// </summary>
		public void Event_PetStateUpdated(Types.EntityStateUpdate type)
		{
			switch (type){
				case Types.EntityStateUpdate.EntityHPMP:
					CheckUsingRecoveryKit();
					break;
				case Types.EntityStateUpdate.BadStatus:
					CheckUsingAbnormalPill();
					break;
			}
		}
		/// <summary>
		/// Called when a pet has been summoned
		/// </summary>
		/// <param name="uniqueID"></param>
		private void Event_PetSummoned(uint uniqueID)
		{
			CheckUsingHGP();
		}
		/// <summary>
		/// Called when a pet has been unsummoned
		/// </summary>
		/// <param name="uniqueID"></param>
		private void Event_PetUnsummoned(uint uniqueID)
		{
			if (Info.Get.Pets[uniqueID].ID4 == 3){
				tUsingHGP.Stop();
			}
		}
		/// <summary>
		/// Called everytime a party invitation is detected
		/// </summary>
		/// <param name="uniqueID">How send the invitation</param>
		public void Event_PartyInvitation(uint uniqueID,Types.PartySetup PartySetup)
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
						PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
					else if (w.Party_cbxRefusePartys.Checked)
						PacketBuilder.PlayerPetitionResponse(false, Types.PlayerPetition.PartyInvitation);
				}
				else
				{
					PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.PartyInvitation);
				}
				// Highest priority, has no sense continue checking ..
				return;
			}
			// Check party list
			if (w.Party_cbxAcceptPartyList.Checked)
			{
				bool found = false;
				string name = (string)player[SRAttribute.Name];
				WinAPI.InvokeIfRequired(w.Party_lstvPartyList, () => {
					for (int j = 0; j < w.Party_lstvPartyList.Items.Count; j++)
					{
						if (w.Party_lstvPartyList.Items[j].Text.Equals(name, StringComparison.OrdinalIgnoreCase))
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
				WinAPI.InvokeIfRequired(w.Party_lstvLeaderList, () => {
					for (int j = 0; j < w.Party_lstvLeaderList.Items.Count; j++)
					{
						if (w.Party_lstvLeaderList.Items[j].Text.Equals(name, StringComparison.OrdinalIgnoreCase))
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
				PacketBuilder.PlayerPetitionResponse(false, Types.PlayerPetition.PartyInvitation);
		}
		/// <summary>
		/// Called when has been joined to the party and all data is loaded.
		/// </summary>
		public void Event_PartyJoined()
		{
			CheckPartyLeaving();
		}
		/// <summary>
		/// Called when the character has left the party group.
		/// </summary>
		private void Event_PartyLeaved()
		{
			CheckAutoParty(null, null);

			// Create again pt match, etc..
		}
		/// <summary>
		/// Called when a member has left the party.
		/// </summary>
		private void Event_MemberLeaved()
		{
			if (!CheckPartyLeaving())
				CheckAutoParty(null, null);
		}
		/// <summary>
		/// Called when a (new) entity appears.
		/// </summary>
		private void Event_Spawn(SRObject entity)
		{

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
