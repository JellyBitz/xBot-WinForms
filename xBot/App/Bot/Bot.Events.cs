using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Timers;
using xBot.Game;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Item;
using xBot.Game.Objects.Party;
using xBot.Network;

namespace xBot.App
{
	public partial class Bot
	{
		#region (Game Events & Bot Logic)
		/// <summary>
		/// Called when the account has been logged succesfully and the Agent has been connected.
		/// </summary>
		public void OnConnected()
		{

		}
		/// <summary>
		/// Called when the current agent connection is lost.
		/// </summary>
		public void OnDisconnected()
		{
			// Stop recording
			isRecording = false;

			// Login hack
			LoggedFromBot = false;

			// Stop timers
			tUsingHP.Enabled = tUsingMP.Enabled = tUsingVigor.Enabled =
				tUsingUniversal.Enabled = tUsingPurification.Enabled =
				tUsingRecoveryKit.Enabled = tUsingAbnormalPill.Enabled =
				tUsingHGP.Enabled =
				tCycleAutoParty.Enabled =
			tJoinedLoop.Enabled = false;
		}
		/// <summary>
		/// Called when on character selection but only if the AutoLogin fails.
		/// </summary>
		public void OnCharacterListing(List<SRCharSelection> CharacterList)
		{
			Window w = Window.Get;
			// Select character
			if (w.Login_cmbxCharacter.Items.Count > 0)
			{
				// Try Autologin
				if (hasAutoLoginMode)
				{
					w.InvokeIfRequired(() => {
						if (w.Login_cmbxCharacter.Text != ""){
							w.Control_Click(w.Login_btnStart, null);
							return;
						}
					});
				}
				else
				{
					// Select first one (UX)
					w.Login_cmbxCharacter.InvokeIfRequired(() => {
						w.Login_cmbxCharacter.SelectedIndex = 0;
					});
				}
			}
			
			// Reset value
			CreatingCharacterName = "";
			// Delete characters that are not being deleted
			if (w.Settings_cbxDeleteChar40to50.Checked)
			{

				for (byte j = 0; j < CharacterList.Count; j++)
				{
					if (!CharacterList[j].isDeleting
						&& CharacterList[j].Level >= 40 && CharacterList[j].Level <= 50)
					{
						w.Log("Deleting character [" + CharacterList[j].Name + "]");
						w.LogProcess("Deleting...");
						PacketBuilder.DeleteCharacter(CharacterList[j].Name);
						System.Threading.Thread.Sleep(500);
					}
				}
			}
			// Select the first character available
			if (w.Settings_cbxSelectFirstChar.Checked)
			{
				SRCharSelection character = CharacterList.Find(c => !c.isDeleting);
				if (character != null)
				{
					w.LogProcess("Selecting...");
					w.InvokeIfRequired(() => {
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
					if( !CharacterList.Exists( c => !c.isDeleting && c.Level < 40 ) )
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
		/// Called when the nick is checked on character creation.
		/// </summary>
		public void OnNicknameChecked(bool available)
		{
			if (isCreatingCharacter)
			{
				if (available)
				{
					Window.Get.Log("Nickname available!");
					CreateCharacter();
				}
				else
				{
					Window.Get.Log("Nickname has been already taken!");
					CreateNickname();
				}
			}
		}
		/// <summary>
		/// Called when the character start loading from any teleport.
		/// </summary>
		public void OnTeleporting()
		{
			
		}
		/// <summary>
		/// Just before <see cref="OnTeleported"/> is called. Generated only once per character login.
		/// </summary>
		public void OnGameJoined()
		{
			Window w = Window.Get;
			w.Log("You has been joined to the game");
			w.LogChatMessage(w.Chat_rtbxAll, "(Welcome)", DataManager.GetUIFormat("UIIT_STT_STARTING_MSG").Replace("\\n", "\n"));

			if (!Proxy.ClientlessMode)
			{
				// Avoid client getting freezed generating issues
				// 10s is enough to leave the client totally loaded
				Timer check = new Timer(10000);
				check.AutoReset = false;
				check.Elapsed += this.CheckLoginOptions;
				check.Start();
			}
			else
			{
				CheckLoginOptions(null, null);
			}

			// Start loop event
			tJoinedLoop = new Timer(200);
			tJoinedLoop.Elapsed += new ElapsedEventHandler(this.OnLoop);
			JoinedLoopCounter = 0;
			tJoinedLoop.Start();
		}
		/// <summary>
		/// Event loop.
		/// </summary>
		private void OnLoop(object timer,ElapsedEventArgs e)
		{
			Window w = Window.Get;
			// Update character realtime position
			SRCoord p = InfoManager.Character.GetRealtimePosition();
			// Set values
			w.Character_SetPosition(p);
			// Update map view every seconds
			if (JoinedLoopCounter % 5 == 0)
				w.Minimap_Character_View(p, InfoManager.Character.GetDegreeAngle());
			JoinedLoopCounter++;
		}
		private Timer tJoinedLoop;
		private uint JoinedLoopCounter = 0;
		/// <summary>
		/// Called right before all character data is loaded.
		/// </summary>
		public void OnTeleported()
		{

		}
		/// <summary>
		/// Called when a near entity spawn.
		/// </summary>
		public void OnSpawn(SREntity entity)
		{

		}
		/// <summary>
		/// Called when a near entity despawn.
		/// </summary>
		public void OnDespawn(uint uniqueID)
		{

		}
		/// <summary>
		/// Called only when the maximum level has been increased.
		/// </summary>
		public void OnLevelUp(byte level)
		{
			// Up skills, etc..
		}
		/// <summary>
		/// Called if any item or quantity is picked up.
		/// </summary>
		public void OnItemPickedUp(SRItem item,ushort quantity)
		{
			Window w = Window.Get;
			if (w.Character_cbxMessagePicks.Checked)
			{
				if(!item.isEquipable())
					w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_STATE_GET_ITEM_NONEXPENDABLE", item.Name));
				else
					w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_STATE_GET_ITEM_EXPENDABLE", item.Name, quantity));
			}
		}
		/// <summary>
		/// Called when the Health, Mana, or BadStatus from the character has changed.
		/// </summary>
		public void OnStatusUpdated(SRTypes.EntityStateUpdate type)
		{
			switch (type)
			{
				case SRTypes.EntityStateUpdate.HP:
					CheckUsingHP();
					CheckUsingVigor();
					break;
				case SRTypes.EntityStateUpdate.MP:
					CheckUsingMP();
					CheckUsingVigor();
					break;
				case SRTypes.EntityStateUpdate.HPMP:
					CheckUsingHP();
					CheckUsingMP();
					CheckUsingVigor();
					break;
				case SRTypes.EntityStateUpdate.BadStatus:
					CheckUsingUniversal();
					CheckUsingPurification();
					break;
			}
		}
		public void OnCharacterDead()
		{
			Window w = Window.Get;
			if (w.Character_cbxAcceptRess.Checked)
			{
				if (InfoManager.Character.Level <= 10 || InfoManager.Character.PVPCapeType != SRPlayer.PVPCape.None)
				{
					PacketBuilder.ResurrectAtPresentPoint();
				}
			}
		}
		/// <summary>
		/// Called when the Health, or BadStatus from the pet has changed.
		/// </summary>
		public void OnPetStatusUpdated(SRTypes.EntityStateUpdate type)
		{
			switch (type)
			{
				case SRTypes.EntityStateUpdate.HP:
				case SRTypes.EntityStateUpdate.HPMP:
				case SRTypes.EntityStateUpdate.EntityHPMP:
					CheckUsingRecoveryKit();
					break;
				case SRTypes.EntityStateUpdate.BadStatus:
					CheckUsingAbnormalPill();
					break;
			}
		}
		/// <summary>
		/// Called when a pet has been summoned
		/// </summary>
		/// <param name="uniqueID"></param>
		public void OnPetSummoned(SRCoService CoS)
		{
			if (CoS.isAttackPet())
				CheckUsingHGP();
		}
		/// <summary>
		/// Called when a pet has been unsummoned
		/// </summary>
		/// <param name="uniqueID"></param>
		public void OnPetUnsummoned(SRCoService CoS)
		{
			if(CoS.isAttackPet())
				tUsingHGP.Stop();
		}
		/// <summary>
		/// Called when "All" message is being sent. Returns the cancel effect.
		/// </summary>
		public bool OnChatSending(string message)
		{
			switch (message)
			{
				case "PING":
					m_Ping = new System.Diagnostics.Stopwatch();
					m_Ping.Start();
					break;
				case "TIME":
					message = "[xBot] Server time : " + InfoManager.GetServerTime().ToString("HH:mm:ss | dd/MM/yyyy");
					PacketBuilder.Client.SendNotice(message);
					if (Proxy.ClientlessMode)
					  Window.Get.LogChatMessage(Window.Get.Chat_rtbxAll, "xBot", message);
					// cancel it
					return true;
				case "ISEEDEADPEOPLE":
					if (!Proxy.ClientlessMode)
					{
						for (int i = 0; i < InfoManager.Players.Count; i++)
						{
							SRPlayer player = InfoManager.Players.GetAt(i);
							if(player.GameStateType != SRModel.GameState.None)
							{
								Packet p = new Packet(Agent.Opcode.SERVER_ENTITY_STATE_UPDATE);
								p.WriteUInt(player.UniqueID);
								p.WriteUShort(4);
								Proxy.InjectToClient(p);
							}
						}
						message = "[xBot] The void has been revealed!";
						PacketBuilder.Client.SendNotice(message);
					}
					return true;
			}
			return false;
		}
		/// <summary>
		/// Called when a message is received.
		/// </summary>
		public void OnChatReceived(SRTypes.Chat type, string playerName, string message)
		{
			Window w = Window.Get;
			// Check Bot commands
			if (type == SRTypes.Chat.All)
			{
				if (playerName == InfoManager.CharName)
				{
					switch (message)
					{
						case "PING":
							if (m_Ping != null)
							{
								m_Ping.Stop();
								message = "[xBot] Your current ping : " + m_Ping.ElapsedMilliseconds + "(ms)";
								PacketBuilder.Client.SendNotice(message);
								m_Ping = null;
								if (Proxy.ClientlessMode)
									w.LogChatMessage(w.Chat_rtbxAll, "xBot", message);
							}
							return;
					}
				}
			}
			// Check Leader Commands
			if (w.Party_cbxActivateLeaderCommands.Checked && playerName != "")
			{
				bool isLeader = false;
				w.Party_lstvLeaderList.InvokeIfRequired(() => {
					isLeader = w.Party_lstvLeaderList.Items.ContainsKey(playerName.ToUpper());
				});
				
				if (isLeader)
				{
					if (message.StartsWith("INJECT ")) {
						string[] data = message.Substring(7).ToUpper().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
									if (byte.TryParse(data[j], NumberStyles.HexNumber, null, out temp)) {
										bytes.Add(temp);
									}
								}
								Proxy.Agent.InjectToServer(new Packet(opcode, encrypted, false, bytes.ToArray()));
								PacketBuilder.SendChatPrivate(playerName, "Packet has been injected");
							}
						}
					}
					else if (message.StartsWith("TRACE"))
					{
						if (message == "TRACE") {
							StartTrace(playerName);
							return;
						}
						else
						{
							string[] data = message.Substring(5).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
							if (data.Length == 1) {
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
						if (data.Length > 1)
						{
							// Check if the teleport link is from teleport model IDs
							uint sourceTeleportID, destinationTeleportID;
							if (uint.TryParse(data[0], out sourceTeleportID) && uint.TryParse(data[1], out destinationTeleportID))
							{
								uint destinationID = DataManager.GetTeleportLinkDestinationID(sourceTeleportID, destinationTeleportID);
								if (destinationID != 0)
								{
									SRTeleport teleport = InfoManager.TeleportAndBuildings.Find(t => t.ID == sourceTeleportID);
									if (teleport != null)
										UseTeleportAsync(teleport, destinationID);
									else
										PacketBuilder.SendChatPrivate(playerName,"Teleport link is not near");
								}
								else
								{
									PacketBuilder.SendChatPrivate(playerName,"Teleport link not found. Please, verify the teleports ID correctly");
								}
								return;
							}
							// Check if the teleport link name exists
							NameValueCollection teleportLinkData = DataManager.GetTeleportLink(data[0], data[1]);
							if(teleportLinkData != null)
							{
								sourceTeleportID = uint.Parse(teleportLinkData["id"]);
								// Check if the teleport source is near
								SRTeleport teleport = InfoManager.TeleportAndBuildings.Find(t => t.ID == sourceTeleportID);
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
							NameValueCollection teleportLinkData = DataManager.GetTeleportLink(message);
							if(teleportLinkData != null)
							{
								uint modelID = uint.Parse(teleportLinkData["id"]);
								if(modelID != 0)
								{
									// Check if the teleport is near
									SRTeleport teleport = InfoManager.TeleportAndBuildings.Find(t => t.ID == modelID);
									if (teleport != null)
										PacketBuilder.DesignateRecall(teleport.UniqueID);
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
				PacketBuilder.PlayerPetitionResponse(false, SRTypes.PlayerPetition.ExchangeRequest);
			}
			else if (w.Character_cbxAcceptExchange.Checked)
			{
				SREntity entity = InfoManager.GetEntity(uniqueID);
				if (w.Character_cbxAcceptExchangeLeaderOnly.Checked)
				{
					bool found = false;
					w.Party_lstvLeaderList.InvokeIfRequired(()=> {
						found = w.Party_lstvLeaderList.Items.ContainsKey(entity.Name.ToUpper());
          });
					if (found)
						PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.ExchangeRequest);
				}
				else
				{
					PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.ExchangeRequest);
				}
			}
		}
		public void OnExchangePlayerConfirmed()
		{
			Window w = Window.Get;
			if (w.Character_cbxAcceptExchange.Checked && w.Character_cbxConfirmExchange.Checked)
				PacketBuilder.ConfirmExchange();
		}
		public void OnExchangeConfirmed()
		{
			Window w = Window.Get;
			if (w.Character_cbxAcceptExchange.Checked && w.Character_cbxApproveExchange.Checked)
				PacketBuilder.ApproveExchange();
		}
		/// <summary>
		/// Called everytime a party invitation is detected.
		/// </summary>
		public void OnPartyInvitation(uint uniqueID, SRParty.Setup Setup)
		{
			// Check GUI
			Window w = Window.Get;

			// All
			if (w.Party_cbxAcceptAll.Checked)
			{
				if (w.Party_cbxAcceptOnlyPartySetup.Checked)
				{
					// Exactly same party setup?
					if (w.GetPartySetup() == Setup)
						PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.PartyInvitation);
					else if (w.Party_cbxRefuseInvitations.Checked)
						PacketBuilder.PlayerPetitionResponse(false, SRTypes.PlayerPetition.PartyInvitation);
				}
				else
				{
					PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.PartyInvitation);
				}
				// Highest priority, has no sense continue checking ..
				return;
			}
			// Check party list
			if (w.Party_cbxAcceptPartyList.Checked)
			{
				string playerName = InfoManager.GetEntity(uniqueID).Name.ToUpper();

				bool found = false;
				w.Party_lstvPartyList.InvokeIfRequired(() => {
					found = w.Party_lstvPartyList.Items.ContainsKey(playerName);
				});
				if (found)
				{
					if (w.Party_cbxAcceptOnlyPartySetup.Checked)
					{
						if (w.GetPartySetup() == Setup)
						{
							PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.PartyInvitation);
							return;
						}
					}
					else
					{
						PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.PartyInvitation);
						return;
					}
				}
			}
			// Check leader list
			if (w.Party_cbxAcceptLeaderList.Checked)
			{
				string playerName = InfoManager.GetEntity(uniqueID).Name.ToUpper();

				bool found = false;
				w.Party_lstvLeaderList.InvokeIfRequired(() => {
					w.Party_lstvLeaderList.InvokeIfRequired(() => {
						found = w.Party_lstvLeaderList.Items.ContainsKey(playerName);
					});
				});
				if (found)
				{
					if (w.Party_cbxAcceptOnlyPartySetup.Checked)
					{
						if (w.GetPartySetup() == Setup)
						{
							PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.PartyInvitation);
							return;
						}
					}
					else
					{
						PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.PartyInvitation);
						return;
					}
				}
			}
			if (w.Party_cbxRefuseInvitations.Checked)
				PacketBuilder.PlayerPetitionResponse(false, SRTypes.PlayerPetition.PartyInvitation);
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
		public void OnPartyLeft()
		{
			CheckPartyMatchAutoReform();
			CheckAutoParty();
		}
		/// <summary>
		/// Called when a member has left the party.
		/// </summary>
		public void OnMemberLeaved()
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
					w.Party_lstvPartyList.InvokeIfRequired(() => {
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
					w.Party_lstvLeaderList.InvokeIfRequired(() => {
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
					if (!InfoManager.inParty || InfoManager.Party.Master.Name == InfoManager.CharName)
					{
						PacketBuilder.CreatePartyMatch(w.GetPartyMatchSetup());
					}
				}
			}
			else if (!w.Party_cbxMatchAutoReform.Checked)
			{
				// Check if I'm master from party match
				if (myMatch.MasterName == InfoManager.CharName)
					PacketBuilder.RemovePartyMatch(myMatch.Number);
			}
		}
		/// <summary>
		/// Called when the party match has been deleted.
		/// </summary>
		public void OnPartyMatchDeleted(uint number)
		{
			Window w = Window.Get;
			if (w.Party_cbxMatchAutoReform.Checked)
			{
				w.Party_lstvPartyMatch.InvokeIfRequired(() => {
					w.Party_lstvPartyMatch.Items.RemoveByKey(number.ToString());
				});
				PacketBuilder.RequestPartyMatch();
			}
		}
		/// <summary>
		/// Called right after a player makes a destination movement.
		/// </summary>
		public void OnPlayerMovement(SRPlayer player)
		{
			if (inTrace)
			{
				Window w = Window.Get;
				if ( TracePlayerName.Equals(player.Name, StringComparison.OrdinalIgnoreCase)
					|| 
					InfoManager.inParty
					&& w.Training_cbxTraceMaster.Checked
					&& InfoManager.Party.Master.Name == player.Name
					&& InfoManager.isEntityNear(player.UniqueID))
				{
					byte distance = 0;
					if (w.Training_cbxTraceDistance.Checked)
					{
						w.Training_tbxTraceDistance.InvokeIfRequired(() => {
							distance = byte.Parse(w.Training_tbxTraceDistance.Text);
						});
					}
					// Follow blind movement
					SRCoord Q = player.MovementPosition;
					if (distance > 0)
					{
						SRCoord P = InfoManager.Character.GetRealtimePosition();

						double PQMod = P.DistanceTo(Q);
						if (distance < PQMod)
						{
							SRCoord PQUnit = new SRCoord((Q.PosX - P.PosX) / PQMod, (Q.PosY - P.PosY) / PQMod);

							SRCoord NewPositon;
							if (P.inDungeon())
								NewPositon = new SRCoord((PQMod - distance) * PQUnit.PosX + P.PosX, (PQMod - distance) * PQUnit.PosY + P.PosY, P.Region, P.Z);
							else
								NewPositon = new SRCoord((PQMod - distance) * PQUnit.PosX + P.PosX, (PQMod - distance) * PQUnit.PosY + P.PosY);

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
		public void OnResurrection(uint UniqueID)
		{
			Window w = Window.Get;

			if (w.Character_cbxAcceptRess.Checked)
			{
				if (!w.Character_cbxAcceptRessPartyOnly.Checked)
				{
					PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.Resurrection);
				}
				else
				{
					SREntity player = InfoManager.GetEntity(UniqueID);
					if(player != null)
					{
						if (InfoManager.Party.Members.Find(p => p.Name == player.Name) != null)
							PacketBuilder.PlayerPetitionResponse(true, SRTypes.PlayerPetition.Resurrection);
					}
				}
			}
		}
		#endregion
	}
}
