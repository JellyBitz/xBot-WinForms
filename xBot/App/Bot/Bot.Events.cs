using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

			if (!Proxy.ClientlessMode)
			{
				// Avoid client getting freezed generating issues
				// 10s is enough to leave the client totally loaded
				System.Timers.Timer check = new System.Timers.Timer(10000);
				check.AutoReset = false;
				check.Elapsed += this.CheckLoginOptions;
				check.Start();
			}
			else
			{
				CheckLoginOptions(null,null);
			}
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
		private void OnItemPickedUp(SRObject item,ushort quantity)
		{
			Window w = Window.Get;
			if (w.Character_cbxMessagePicks.Checked)
			{
				// Expendable
				if(item.ID2 == 3)
					w.LogMessageFilter(Info.Get.GetUIFormat("UIIT_MSG_STATE_GET_ITEM_EXPENDABLE", item.Name,quantity));
				else
					w.LogMessageFilter(Info.Get.GetUIFormat("UIIT_MSG_STATE_GET_ITEM_NONEXPENDABLE", item.Name));
			}
		}
		/// <summary>
		/// Called when the Health, Mana, or BadStatus from the character has changed.
		/// </summary>
		private void OnStatusUpdated(Types.EntityStateUpdate type)
		{
			Info i = Info.Get;
			if(i.Character == null){
				// Avoid app crash on disconnect
				return;
			}
			// Check if character is alive
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
		}
		private void OnCharacterDead()
		{
			Window w = Window.Get;
			if (w.Character_cbxAcceptRess.Checked)
			{
				if ((byte)Info.Get.Character[SRProperty.Level] <= 10)
				{
					PacketBuilder.ResurrectAtPresentPoint();
				}
			}
		}
		/// <summary>
		/// Called when the Health, or BadStatus from the pet has changed.
		/// </summary>
		private void OnPetStatusUpdated(Types.EntityStateUpdate type)
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
			if (Info.Get.MyPets[uniqueID].ID4 == 3){
				tUsingHGP.Stop();
			}
		}
		/// <summary>
		/// Called when a message is received.
		/// </summary>
		private void OnChatReceived(Types.Chat type, string playerName, string message)
		{
			Window w = Window.Get;
			if (w.Party_cbxActivateLeaderCommands.Checked && playerName != "")
			{
				bool isLeader = false;
				w.Party_lstvLeaderList.InvokeIfRequired(() => {
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
								PacketBuilder.SendChatPrivate(playerName,"Packet has been injected");
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
					else if (message.StartsWith("TELEPORT "))
					{
						message = message.Substring(9);
						// Check params correctly
						string[] data;
						if(message.Contains(","))
							data = message.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
						else
							data = message.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
						// IF there is at least 2 params
						if (data.Length > 1){
							Info i = Info.Get;
							// Check if the teleport link is from teleport model IDs
							uint sourceTeleportID, destinationTeleportID;
							if (uint.TryParse(data[0], out sourceTeleportID) && uint.TryParse(data[1], out destinationTeleportID))
							{
								uint destinationID = i.GetTeleportLinkDestinationID(sourceTeleportID, destinationTeleportID);
								if (destinationID != 0)
								{
									SRObject teleport = i.Teleports.Find(npc => npc.ID == sourceTeleportID);
									if (teleport != null)
									{
										UseTeleportAsync(teleport, destinationID);
									}
									else
									{
										PacketBuilder.SendChatPrivate(playerName,"Teleport link is not near");
									}
								}
								else
								{
									PacketBuilder.SendChatPrivate(playerName,"Teleport link not found. Please, verify the teleports ID correctly");
								}
								return;
							}
							// Check if the teleport link name exists
							NameValueCollection teleportLinkData = i.GetTeleportLink(data[0], data[1]);
							if(teleportLinkData != null)
							{
								sourceTeleportID = uint.Parse(teleportLinkData["id"]);
								// Check if the teleport source is near
								SRObject teleport = i.Teleports.Find(npc => npc.ID == sourceTeleportID);
                if (teleport != null)
								{
									// Try to select teleport 
									UseTeleportAsync(teleport, uint.Parse(teleportLinkData["destinationid"]));
								}
								else
								{
									PacketBuilder.SendChatPrivate(playerName,"Teleport link is not near");
								}
							}
							else
							{
								PacketBuilder.SendChatPrivate(playerName,"Teleport link not found. Please, verify the teleports location correctly");
							}
						}
					}
					else if (message.StartsWith("RECALL "))
					{
						message = message.Substring(7);
						if(message != "")
						{
							Info i = Info.Get;
							NameValueCollection teleportLinkData = i.GetTeleportLink(message);
							if(teleportLinkData != null)
							{
								uint modelID = uint.Parse(teleportLinkData["id"]);
								if(modelID != 0)
								{
									// Check if the teleport is near
									SRObject teleport = i.Teleports.Find(npc => npc.ID == modelID);
									if (teleport != null)
										PacketBuilder.DesignateRecall((uint)teleport[SRProperty.UniqueID]);
									else
										PacketBuilder.SendChatPrivate(playerName,"Teleport is not near");
								}
							}
							else
							{
								PacketBuilder.SendChatPrivate(playerName,"Wrong teleport name");
							}
						}
					}
					else if (message == "NOTRACE")
					{
						StopTrace();
					}
					else if (message == "RETURN")
					{
						if(!UseReturnScroll()){
							PacketBuilder.SendChatPrivate(playerName,"Return scroll not found");
						}
					}
				}
			}
		}
		/// <summary>
		/// Called everytime a exchange invitation is detected.
		/// </summary>
		public void OnExchangeRequest(uint uniqueID)
		{
			Window w = Window.Get;
			if (w.Character_cbxRefuseExchange.Checked)
			{
				PacketBuilder.PlayerPetitionResponse(false, Types.PlayerPetition.ExchangeRequest);
			}
			else	if (w.Character_cbxAcceptExchange.Checked)
			{
				SRObject player = Info.Get.GetEntity(uniqueID);
				if (w.Character_cbxAcceptExchangeLeaderOnly.Checked)
				{
					bool found = false;
					w.Party_lstvLeaderList.InvokeIfRequired(()=> {
						found = w.Party_lstvLeaderList.Items.ContainsKey(player.Name.ToUpper());
          });
					if (found)
						PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.ExchangeRequest);
				}
				else
				{
					PacketBuilder.PlayerPetitionResponse(true, Types.PlayerPetition.ExchangeRequest);
				}
			}
		}
		private void OnExchangePlayerConfirmed()
		{
			Window w = Window.Get;
			if (w.Character_cbxAcceptExchange.Checked && w.Character_cbxConfirmExchange.Checked)
			{
				PacketBuilder.ConfirmExchange();
			}
		}
		private void OnExchangeConfirmed()
		{
			Window w = Window.Get;
			if (w.Character_cbxAcceptExchange.Checked && w.Character_cbxApproveExchange.Checked)
			{
				PacketBuilder.ApproveExchange();
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
					found = w.Party_lstvPartyList.Items.ContainsKey(playerName.ToUpper());
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
					w.Party_lstvLeaderList.InvokeIfRequired(() => {
						found = w.Party_lstvLeaderList.Items.ContainsKey(playerName.ToUpper());
					});
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
					WinAPI.InvokeIfRequired(w.Party_lstvPartyList, () => {
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
					WinAPI.InvokeIfRequired(w.Party_lstvLeaderList, () => {
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
						|| i.PartyMembers.ElementAt(0).Name == i.Charname)
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
					&& i.PartyMembers.ElementAt(0).Name == player.Name
					&& i.Players[TracePlayerName] == null)
				{
					byte distance = 0;
					if (w.Training_cbxTraceDistance.Checked)
					{
						WinAPI.InvokeIfRequired(w.Training_tbxTraceDistance, () => {
							distance = byte.Parse(w.Training_tbxTraceDistance.Text);
						});
					}
					
					SRCoord Q = (SRCoord)player[SRProperty.MovementPosition];
					if (distance > 0)
					{
						SRCoord P = i.Character.GetPosition();

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
						if (i.PartyMembers.Find( p => p.Name == player.Name) != null)
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
