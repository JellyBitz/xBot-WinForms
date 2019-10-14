using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using xBot.Game;
using xBot.Game.Objects;

namespace xBot.App
{
	public partial class Bot
	{
		#region (Game Events & Bot Logic)
		/// <summary>
		/// Called when the account has been logged succesfully and the Agent has been connected.
		/// </summary>
		private void OnConnected()
		{

		}
		/// <summary>
		/// Called when the current agent connection is lost.
		/// </summary>
		private void OnDisconnected()
		{
			
		}
		/// <summary>
		/// Called when on character selection but only if the AutoLogin fails.
		/// </summary>
		private void OnCharacterListing(List<SRObject> CharacterList)
		{
			Window w = Window.Get;
			// Select character
			if (w.Login_cmbxCharacter.Items.Count > 0)
			{
				// Try Autologin
				if (hasAutoLoginMode)
				{
					WinAPI.InvokeIfRequired(w, () => {
						if (w.Login_cmbxCharacter.Text != ""){
							w.Control_Click(w.Login_btnStart, null);
							return;
						}
					});
				}
				else
				{
					// Select first one (Just for UX)
					WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () => {
						w.Login_cmbxCharacter.SelectedIndex = 0;
					});
				}
			}
			
			// Reset value
			CreatingCharacterName = "";
			// Delete characters that are not being deleted
			if (w.Settings_cbxDeleteChar40to50.Checked)
			{
				foreach (SRObject character in CharacterList)
				{
					if (!(bool)character[SRProperty.isDeleting]
						&& (byte)character[SRProperty.Level] >= 40 && (byte)character[SRProperty.Level] <= 50)
					{
						w.Log("Deleting character [" + character.Name + "]");
						w.LogProcess("Deleting...");
						PacketBuilder.DeleteCharacter(character.Name);
						System.Threading.Thread.Sleep(500);
					}
				}
			}
			// Select the first character available
			if (w.Settings_cbxSelectFirstChar.Checked)
			{
				SRObject character = CharacterList.Find(c => !(bool)c[SRProperty.isDeleting]);
				if (character != null)
				{
					w.LogProcess("Selecting...");
					WinAPI.InvokeIfRequired(w, () => {
						w.Login_cmbxCharacter.Text = character.Name;
						w.Control_Click(w.Login_btnStart, null);
					});
					return;
				}
				else
				{
					w.Log("No character available to select!");
				}
			}
			// No characters selected, then create it?
			if (w.Settings_cbxCreateChar.Checked 
				&& CharacterList.Count == 0)
			{
				w.Log("Empty character list, creating character...");
				CreateNickname();
			}
			else if (w.Settings_cbxCreateCharBelow40.Checked)
			{
				if (CharacterList.Count < 4)
				{
					if( !CharacterList.Exists( c => !(bool)c[SRProperty.isDeleting] && (byte)c[SRProperty.Level] < 40 ) )
					{
						w.Log("No characters below Lv.40, creating character...");
						CreateNickname();
					}
				}
				else
				{
					w.Log("Character list full, you cannot create more characters!");
				}
			}
		}
		/// <summary>
		/// Called when the character start loading from any teleport.
		/// </summary>
		private void OnTeleporting()
		{
			if (inGame)
			{
				Window.Get.LogProcess("Teleporting...");
			}
			else
			{
				Window.Get.LogProcess("Loading...");
			}
		}
		/// <summary>
		/// Just before <see cref="OnTeleported"/> is called. Generated only once per character login.
		/// </summary>
		private void OnGameJoined()
		{
			Window w = Window.Get;
			Info i = Info.Get;
			w.Log("You has been joined to the game");
			w.LogChatMessage(w.Chat_rtbxAll, "(Welcome)", i.GetUIFormat("UIIT_STT_STARTING_MSG").Replace("\\n", "\n"));

			// Check login options
			if (w.Login_cbxGoClientless.Checked)
				GoClientless();

			if (w.Login_cbxUseReturnScroll.Checked)
				UseReturnScroll();

			CheckAutoParty();
			CheckPartyMatchAutoReform();
		}
		/// <summary>
		/// Called right before all character data is saved & spawn packet is detected from client.
		/// </summary>
		private void OnTeleported()
		{
			Window.Get.LogProcess("Teleported");
		}
		/// <summary>
		/// Called only when the maximum level has been increased.
		/// </summary>
		private void OnLevelUp(byte level)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_STRGERR_LEVEL", level));
			// Up skills, etc..
		}
		private void OnItemPickedUp(SRObject item)
		{
			Window w = Window.Get;
			if (w.Character_cbxMessagePicks.Checked)
			{
				w.LogMessageFilter(Info.Get.GetUIFormat("UIIT_MSG_STRGERR_LEVEL", item.Name));
			}
		}
		/// <summary>
		/// Called when the Health, Mana, or BadStatus from the character has changed.
		/// </summary>
		private void OnStateUpdated(Types.EntityStateUpdate type)
		{
			Info i = Info.Get;
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.Alive)
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
				if((byte)i.Character[SRProperty.Level] <= 10){
					PacketBuilder.ResurrectAtPresentPoint();
				}
			}
		}
		/// <summary>
		/// Called when the Health, or BadStatus from the pet has changed.
		/// </summary>
		private void OnPetStateUpdated(Types.EntityStateUpdate type)
		{
			switch (type)
			{
				case Types.EntityStateUpdate.HP:
				case Types.EntityStateUpdate.HPMP:
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
		private void OnPetSummoned(uint uniqueID)
		{
			CheckUsingHGP();
		}
		/// <summary>
		/// Called when a pet has been unsummoned
		/// </summary>
		/// <param name="uniqueID"></param>
		private void OnPetUnsummoned(uint uniqueID)
		{
			if (Info.Get.Pets[uniqueID].ID4 == 3){
				tUsingHGP.Stop();
			}
		}
		/// <summary>
		/// Called when a message is received.
		/// </summary>
		private void OnChat(Types.Chat type, string playerName, string message)
		{
			Window w = Window.Get;
			if (w.Party_cbxActivateLeaderCommands.Checked && playerName != "")
			{
				bool isLeader = false;
				WinAPI.InvokeIfRequired(w.Party_lstvLeaderList,() => {
					isLeader = w.Party_lstvLeaderList.Items.ContainsKey(playerName.ToUpper());
				});
				if (isLeader)
				{
					if (message.StartsWith("INJECT ")){
						string[] data = message.Substring(7).ToUpper().Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
						if (data.Length >= 1)
						{
							ushort opcode;
							if (ushort.TryParse(data[0], NumberStyles.HexNumber, null, out opcode))
							{
								bool encrypted = false;
								int dataIndex = 1;
								if (data.Length > 1 && (data[dataIndex] == "FALSE" || data[dataIndex] == "TRUE"))
								{
									encrypted = bool.Parse(data[dataIndex++]);
								}
								List<byte> bytes = new List<byte>();
								for (int j = dataIndex; j < data.Length; j++)
								{
									byte temp;
									if (byte.TryParse(data[j], NumberStyles.HexNumber, null, out temp)){
										bytes.Add(temp);
									}
								}
								Proxy.Agent.InjectToServer(new Packet(opcode, encrypted,false, bytes.ToArray()));
							}
						}
					}
					else if (message.StartsWith("TRACE"))
					{
						if (message == "TRACE"){
							StartTrace(playerName);
							return;
						}
						else
						{
							string[] data = message.Substring(5).Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
							if (data.Length == 1){
								StartTrace(data[0]);
							}
						}
						
					}
					else if (message == "NOTRACE")
					{
						StopTrace();
					}
					else if (message == "RETURN")
					{
						UseReturnScroll();
					}
				}
			}
		}
		/// <summary>
		/// Called everytime a party invitation is detected.
		/// </summary>
		private void OnPartyInvitation(string playerName, Types.PartySetup PartySetup)
		{
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
					else if (w.Party_cbxRefuseInvitations.Checked)
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
				WinAPI.InvokeIfRequired(w.Party_lstvPartyList, () => {
					for (int j = 0; j < w.Party_lstvPartyList.Items.Count; j++)
					{
						if (w.Party_lstvPartyList.Items[j].Text.Equals(playerName, StringComparison.OrdinalIgnoreCase))
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
				WinAPI.InvokeIfRequired(w.Party_lstvLeaderList, () => {
					for (int j = 0; j < w.Party_lstvLeaderList.Items.Count; j++)
					{
						if (w.Party_lstvLeaderList.Items[j].Text.Equals(playerName, StringComparison.OrdinalIgnoreCase))
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
			if (w.Party_cbxRefuseInvitations.Checked)
				PacketBuilder.PlayerPetitionResponse(false, Types.PlayerPetition.PartyInvitation);
		}
		/// <summary>
		/// Called when has been joined to the party and all data is loaded.
		/// </summary>
		public void OnPartyJoined()
		{
			CheckPartyLeaving();
		}
		/// <summary>
		/// Called when the character has left the party group.
		/// </summary>
		private void OnPartyLeaved()
		{
			CheckPartyMatchAutoReform();
			CheckAutoParty();
		}
		/// <summary>
		/// Called when a member has left the party.
		/// </summary>
		private void OnMemberLeaved()
		{
			if (!CheckPartyLeaving()){
				CheckPartyMatchAutoReform();
			}
		}
		/// <summary>
		/// Called when a player wants to join to our party match.
		/// </summary>
		public void OnPartyMatchJoinRequest(uint requestID, uint playerJoinID, string playerName)
		{
			Window w = Window.Get;
			if (w.Party_cbxMatchAutoReform.Checked)
			{
				if (w.Party_cbxMatchAcceptAll.Checked)
				{
					PacketBuilder.PartyMatchJoinResponse(requestID, playerJoinID, accept: true);
					return;
				}
				if (w.Party_cbxMatchAcceptPartyList.Checked)
				{
					bool found = false;
					WinAPI.InvokeIfRequired(w.Party_lstvPartyList, delegate
					{
						found = w.Party_lstvPartyList.Items.ContainsKey(playerName.ToUpper());
					});
					if (found)
					{
						PacketBuilder.PartyMatchJoinResponse(requestID, playerJoinID, accept: true);
						return;
					}
				}
				if (w.Party_cbxMatchAcceptLeaderList.Checked)
				{
					bool found = false;
					WinAPI.InvokeIfRequired(w.Party_lstvLeaderList, delegate
					{
						found = w.Party_lstvLeaderList.Items.ContainsKey(playerName.ToUpper());
					});
					if (found)
					{
						PacketBuilder.PartyMatchJoinResponse(requestID, playerJoinID, accept: true);
						return;
					}
				}
				if (w.Party_cbxMatchRefuse.Checked)
				{
					PacketBuilder.PartyMatchJoinResponse(requestID, playerJoinID, accept: false);
				}
			}
		}
		/// <summary>
		/// Called when the party match has been updated.
		/// </summary>
		public void OnPartyMatchListing(SRPartyMatch myMatch)
		{
			Window w = Window.Get;
			if (myMatch == null)
			{
				if (w.Party_cbxMatchAutoReform.Checked)
				{
					Info i = Info.Get;
					if (!inParty
						|| i.PartyList[0].Name == i.Charname)
					{
						PacketBuilder.CreatePartyMatch(GetPartyMatchSetup());
					}
				}
			}
			else if (!w.Party_cbxMatchAutoReform.Checked)
			{
				Info i = Info.Get;
				SRPartyMatch GUIMatch = GetPartyMatchSetup();
				// Check if the party match is barely identical GUI
				if (myMatch.Owner == i.Charname 
					&& myMatch.Title == GUIMatch.Title 
					&& myMatch.LevelMin == GUIMatch.LevelMin 
					&& myMatch.LevelMax == GUIMatch.LevelMax)
				{
					PacketBuilder.RemovePartyMatch(myMatch.Number);
				}
			}
		}
		/// <summary>
		/// Called when a (new) entity appears.
		/// </summary>
		private void OnSpawn(ref SRObject entity)
		{

		}
		/// <summary>
		/// Called right after a player makes a destination movement.
		/// </summary>
		/// <param name="player"></param>
		private void OnPlayerMovement(ref SRObject player)
		{
			if (inTrace)
			{
				Info i = Info.Get;
				Window w = Window.Get;

				if ( TracePlayerName.Equals(player.Name, StringComparison.OrdinalIgnoreCase)
					|| 
					inParty
					&& w.Training_cbxTraceMaster.Checked
					&& i.PartyList[0].Name == player.Name
					&& !i.PlayersNear.ContainsKey(TracePlayerName))
				{
					byte distance = 0;
					if (w.Training_cbxTraceDistance.Checked)
					{
						WinAPI.InvokeIfRequired(w.Training_tbxTraceDistance, () => {
							distance = byte.Parse(w.Training_tbxTraceDistance.Text);
						});
					}
					
					SRCoord P = i.Character.GetPosition();
					SRCoord Q = player.GetPosition();
					if (distance > 0)
					{
						double PQMod = P.DistanceTo(Q);
						if (distance < PQMod)
						{
							SRCoord PQUnit = new SRCoord((Q.PosX - P.PosX) / PQMod, (Q.PosY - P.PosY) / PQMod);

							SRCoord NewPositon;
							if (P.inDungeon())
							{
								NewPositon = new SRCoord((PQMod - distance) * PQUnit.PosX + P.PosX, (PQMod - distance) * PQUnit.PosY + P.PosY, P.Region, P.Z);
							}
							else
							{
								NewPositon = new SRCoord((PQMod - distance) * PQUnit.PosX + P.PosX, (PQMod - distance) * PQUnit.PosY + P.PosY);
							}
							 
							MoveTo(NewPositon);
							w.LogProcess("Tracing to [" + player.Name + "] ...");
						}
					}
					else
					{
						MoveTo(Q);
						w.LogProcess("Tracing to [" + player.Name + "] ...");
					}
				}
			}
		}
		/// <summary>
		/// Called when a player is giving you resurrection request
		/// </summary>
		public void OnResurrection(uint uniqueID)
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
						if (i.GetPartyMember(player.Name) != null)
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
