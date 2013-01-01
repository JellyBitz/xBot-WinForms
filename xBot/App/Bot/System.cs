using System.IO;
using xBot.Game;
using xBot.Network;
using System.Timers;
using xBot.Game.Objects;
using System.Collections.Generic;
using System;

namespace xBot
{
	public partial class Bot
	{
		#region (Logical System Controller)
		/// <summary>
		/// Cooldown timer.
		/// </summary>
		Timer tUsingHP, tUsingMP, tUsingVigor,
			tUsingUniversal, tUsingPurification,
			tUsingRecoveryKit, tUsingAbnormalPill,
			tUsingHGP,
			tCycleAutoParty;

		private void InitializeTimers()
		{
			// Preparing all neccesary timers
			tUsingHP = new Timer();
			tUsingMP = new Timer();
			tUsingVigor = new Timer();
			tUsingUniversal = new Timer();
			tUsingPurification = new Timer();
			tUsingRecoveryKit = new Timer();
			tUsingAbnormalPill = new Timer();
			tUsingHGP = new Timer();
			tCycleAutoParty = new Timer();

			// A second is enought for any potion cooldown
			tUsingHP.Interval = tUsingMP.Interval = tUsingVigor.Interval =
			tUsingUniversal.Interval = tUsingPurification.Interval = 
			tUsingRecoveryKit.Interval = tUsingAbnormalPill.Interval;

			tCycleAutoParty.Interval = 5000;
			
			// Callbacks
			tUsingHP.Elapsed += CheckUsingHP;
			tUsingMP.Elapsed += CheckUsingMP;
			tUsingVigor.Elapsed += CheckUsingVigor;
			tUsingUniversal.Elapsed += CheckUsingUniversal;
			tUsingPurification.Elapsed += CheckUsingPurification;
			tCycleAutoParty.Elapsed += CheckAutoParty;
			tUsingRecoveryKit.Elapsed += CheckUsingRecoveryKit;
			tUsingAbnormalPill.Elapsed += CheckUsingAbnormalPill;
			tUsingHGP.Elapsed += CheckUsingHGP;
		}

		public void _Event_Connected()
		{
			this.Event_Connected();
		}
		public void _Event_CharacterListing(List<SRObject> CharacterList)
		{
			Window w = Window.Get;
			// Add to GUI
			for (int j = 0; j< CharacterList.Count;j++)
			{
				System.Windows.Forms.ListViewItem Item = new System.Windows.Forms.ListViewItem();
				Item.Text = CharacterList[j].Name + ((bool)CharacterList[j][SRProperty.isDeleting] ? " (*)" : "");
				Item.Name = CharacterList[j].Name;
				Item.SubItems.Add(CharacterList[j][SRProperty.Level].ToString());
				Item.SubItems.Add(CharacterList[j].GetExpPercent() + " %");
				if ((bool)CharacterList[j][SRProperty.isDeleting]){
					Item.SubItems.Add(((DateTime)CharacterList[j][SRProperty.DeletingDate]).ToString("dd/mm/yyyy hh:mm"));
				}
				// Add to view
				WinAPI.InvokeIfRequired(w.Login_lstvCharacters, () => {
					w.Login_lstvCharacters.Items.Add(Item);
				});

				// Auto select if has autologin
				if (!(bool)CharacterList[j][SRProperty.isDeleting])
				{
					WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () => {
						w.Login_cmbxCharacter.Items.Add(Item.Name);
						if (hasAutoLoginMode 
						&& w.Login_cmbxCharacter.Tag != null 
						&& ((string)w.Login_cmbxCharacter.Tag).ToUpper() == CharacterList[j].Name.ToUpper()){
							w.Login_cmbxCharacter.Text = CharacterList[j].Name;
						}
					});
				}
			}
			// Switch Listview's [Servers to Characters] selection 
			WinAPI.InvokeIfRequired(w.Login_gbxServers, () => {
				w.Login_gbxServers.Visible = false;
			});
			WinAPI.InvokeIfRequired(w.Login_gbxCharacters, () => {
				w.Login_gbxCharacters.Visible = true;
			});
			// Switch and restaure [LOGIN to SELECT] button
			WinAPI.InvokeIfRequired(w.Login_btnStart, () => {
				w.Login_btnStart.Text = "SELECT";
				w.EnableControl(w.Login_btnStart, true);
			});
			
			w.LogProcess("Character selection");
			this.Event_CharacterListing(CharacterList);
		}
		public void _Event_Disconnected()
		{
			inGame = false;
			inTeleport = false;
			hasStall = false;

			Window w = Window.Get;
			Info i = Info.Get;

			// Reset data
			i.Character = null;
			
			EntitySelected = 0;
			i.Pets.Clear();
			i.PlayersNear.Clear();
			i.EntityList.Clear();
			w.Minimap_ObjectPointer_Clear();

			inParty = false;
			i.PartyList.Clear();
			w.Party_Clear();
			StopTrace();

			// Stop timers
			tUsingHP.Enabled = tUsingMP.Enabled = tUsingVigor.Enabled =
				tUsingUniversal.Enabled = tUsingPurification.Enabled =
				tUsingRecoveryKit.Enabled = tUsingAbnormalPill.Enabled = 
				tUsingHGP.Enabled =
				tCycleAutoParty.Enabled = false;

			LoggedFromBot = false;
			_HWIDSent = false;

			this.Event_Disconnected();
		}
		public void _Event_NicknameChecked(bool available)
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
		public void _Event_Teleporting()
		{
			inTeleport = true;

			Window w = Window.Get;
			Info i = Info.Get;

			// Reset entity data
			EntitySelected = 0;
			i.Pets.Clear();
			i.PlayersNear.Clear();
			i.EntityList.Clear();
			w.Minimap_ObjectPointer_Clear();

			// Reset party data
			inParty = false;
			i.PartyList.Clear();
			w.Party_Clear();

			this.Event_Teleporting();
		}
		public void _Event_Teleported()
		{
			inTeleport = false;

			Info i = Info.Get;
			if (!inGame)
			{
				inGame = true;
				Window.Get.SetTitle(i.Server, i.Charname, Proxy.SRO_Client);
				Event_GameJoined();
			}

			this.Event_Teleported();
		}
		public void _Event_Spawn(ref SRObject entity)
		{
			Info i = Info.Get;
			uint uniqueID = (uint)entity[SRProperty.UniqueID];
			if (entity.isPlayer())
			{
				i.PlayersNear[entity.Name.ToUpper()] = entity;
			}
			i.EntityList[uniqueID] = entity;
			Window.Get.Minimap_ObjectPointer_Add((uint)entity[SRProperty.UniqueID], entity.ServerName, entity.Name,(SRCoord)entity[SRProperty.Position]);
			Event_Spawn(ref entity);
		}
		public void _Event_Despawn(uint uniqueID)
		{
			Info i = Info.Get;
			SRObject entity = i.EntityList[uniqueID];
			if (entity.isPlayer())
			{
				i.PlayersNear.Remove(entity.Name.ToUpper());
			}
			i.EntityList.Remove(uniqueID);
			Window.Get.Minimap_ObjectPointer_Remove(uniqueID);
		}
		public void _Event_EntityMovement(ref SRObject entity)
		{
			Info i = Info.Get;
			if ((bool)entity[SRProperty.hasMovement])
			{
				Window w = Window.Get;
				if ((uint)entity[SRProperty.UniqueID] == (uint)i.Character[SRProperty.UniqueID])
				{
					SRCoord p = (SRCoord)entity[SRProperty.MovementPosition];

					WinAPI.InvokeIfRequired(w.Character_lblLocation, () => {
						if(p.Region.ToString() != w.Character_lblLocation.Text)
							w.Character_lblLocation.Text = i.GetRegion(p.Region);
					});
					WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
						w.Minimap_tbxX.Text = p.X.ToString();
						w.Minimap_tbxY.Text = p.Y.ToString();
						w.Minimap_tbxZ.Text = p.Z.ToString();
						w.Minimap_tbxRegion.Text = p.Region.ToString();
					});
					WinAPI.InvokeIfRequired(w.Character_lblCoordX, () => {
						w.Character_lblCoordX.Text = "X : " + Math.Round(p.PosX);
					});
					WinAPI.InvokeIfRequired(w.Character_lblCoordY, () => {
						w.Character_lblCoordY.Text = "Y : " + Math.Round(p.PosY);
					});
					w.Minimap_CharacterPointer_Move(p);
					return;
				}
				if (entity.isPlayer())
				{
					Event_PlayerMovement(ref entity);
				}
				else if (entity.ID1 == 1 && entity.ID2 == 2 && entity.ID3 == 3)
				{
					if (entity.ID4 == 1)
					{
						// Vehicle
						uint vehicleUniqueID = (uint)entity[SRProperty.UniqueID];
						SRObject player = i.GetPlayers().Find(p => (bool)p[SRProperty.isRiding] && (uint)p[SRProperty.RidingUniqueID] == vehicleUniqueID);
						if (player != null)
						{
							player[SRProperty.MovementPosition] = entity[SRProperty.MovementPosition];
							player[SRProperty.MovementDate] = entity[SRProperty.MovementDate];
							_Event_EntityMovement(ref player);
						}
					}
					else if (entity.ID4 == 2)
					{
						SRObject player = i.GetEntity((uint)entity[SRProperty.OwnerUniqueID]);
						if (player != null && (bool)player[SRProperty.isRiding])
						{
							player[SRProperty.MovementPosition] = entity[SRProperty.MovementPosition];
							player[SRProperty.MovementDate] = entity[SRProperty.MovementDate];
							Event_PlayerMovement(ref player);
						}
					}
				}
				w.Minimap_ObjectPointer_Move((uint)entity[SRProperty.UniqueID], (SRCoord)entity[SRProperty.MovementPosition]);
			}
		}
		public void _Event_EntityStateUpdated(ref SRObject entity, Types.EntityStateUpdate type)
		{
			// Update dead/alive state
			switch (type)
			{
				case Types.EntityStateUpdate.HP:
				case Types.EntityStateUpdate.HPMP:
				case Types.EntityStateUpdate.EntityHPMP:
					if ((uint)entity[SRProperty.HP] == 0)
					{
						entity[SRProperty.LifeState] = Types.LifeState.Dead;
					}
					else if ((Types.LifeState)entity[SRProperty.LifeState] != Types.LifeState.Alive)
					{
						entity[SRProperty.LifeState] = Types.LifeState.Alive;
					}
					break;
			}
			Info i = Info.Get;
			// Generating character event
			if ((uint)entity[SRProperty.UniqueID] == (uint)i.Character[SRProperty.UniqueID])
			{
				_Event_StateUpdated(type);
			}
			else if (entity.ID1 == 1 && entity.ID2 == 2 && entity.ID3 == 3)
			{
				// Check entity is a pet
				if (entity.ID4 == 1 && (string)entity[SRProperty.OwnerName] == i.Charname // vehicle
					|| (entity.ID4 != 1 && (uint)entity[SRProperty.OwnerUniqueID] == (uint)i.Character[SRProperty.UniqueID]))
				{
					// Check if it's my pet
					_Event_PetStateUpdated(type);
				}
			}
		}
		private void _Event_StateUpdated(Types.EntityStateUpdate type)
		{
			// Update GUI bars
			Window w = Window.Get;
			Info i = Info.Get;

			switch (type){
				case Types.EntityStateUpdate.HP:
					WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
						w.Character_pgbHP.Value = (uint)i.Character[SRProperty.HP];
					});
					break;
				case Types.EntityStateUpdate.MP:
					WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
						w.Character_pgbMP.Value = (uint)i.Character[SRProperty.MP];
					});
					break;
				case Types.EntityStateUpdate.HPMP:
					WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
						w.Character_pgbHP.Value = (uint)i.Character[SRProperty.HP];
					});
					WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
						w.Character_pgbMP.Value = (uint)i.Character[SRProperty.MP];
					});
					break;
			}
			this.Event_StateUpdated(type);
		}
		public void _Event_ExpReceived(long ExpReceived, long Exp, long ExpMax, byte Level)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				i.Character[SRProperty.Level] = (byte)(Level + 1);
				WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
					w.Character_lblLevel.Text = "Lv. " + i.Character[SRProperty.Level];
				});
				// Update new ExpMax
				i.Character[SRProperty.ExpMax] = i.GetExpMax((byte)(Level + 1));
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.ValueMaximum = (ulong)i.Character[SRProperty.ExpMax];
				});
				// Update max. level reached
				if ((byte)i.Character[SRProperty.Level] > (byte)i.Character[SRProperty.LevelMax])
				{
					i.Character[SRProperty.LevelMax]= (byte)i.Character[SRProperty.Level];
					i.Character[SRProperty.StatPoints] = (ushort)((ushort)i.Character[SRProperty.StatPoints] + 3);
					WinAPI.InvokeIfRequired(w.Character_gbxStatPoints, () => {
						w.Character_lblStatPoints.Text = i.Character[SRProperty.StatPoints].ToString();
						w.Character_btnAddSTR.Enabled = w.Character_btnAddINT.Enabled = true;
					});
					// Generate bot event
					Event_LevelUp((byte)i.Character[SRProperty.Level]);
				}
				// Continue recursivity
				_Event_ExpReceived((Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)i.Character[SRProperty.ExpMax]), (byte)(Level + 1));
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				i.Character[SRProperty.Level] = (byte)(Level - 1);
				WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
					w.Character_lblLevel.Text = i.Character[SRProperty.Level].ToString();
				});
				// Update new ExpMax
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.ValueMaximum = (ulong)i.Character[SRProperty.ExpMax];
				});
				_Event_ExpReceived(Exp + ExpReceived, (long)((ulong)i.Character[SRProperty.ExpMax]), (long)(ulong)i.Character[SRProperty.ExpMax], (byte)(Level - 1));
			}
			else
			{
				// Increase/Decrease Exp
				i.Character[SRProperty.Exp] = (ulong)(Exp + ExpReceived);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.Value = (ulong)i.Character[SRProperty.Exp];
				});
			}
		}
		public void _Event_PetSummoned(uint uniqueID)
		{
			Info i = Info.Get;
			i.Pets[uniqueID] = i.EntityList[uniqueID];

			this.Event_PetSummoned(uniqueID);
		}
		public void _Event_PetUnsummoned(uint uniqueID)
		{
			Event_PetUnsummoned(uniqueID);
			Info.Get.Pets.Remove(uniqueID);
		}
		public void _Event_PetStateUpdated(Types.EntityStateUpdate type)
		{
			Event_PetStateUpdated(type);
		}
		public void _Event_PetExpReceived(ref SRObject pet, long ExpReceived, long Exp, long ExpMax, byte level)
		{
			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				pet[SRProperty.Level] = (byte)(level + 1);
				// Update new ExpMax
				pet[SRProperty.ExpMax] = Info.Get.GetPetExpMax((byte)pet[SRProperty.Level]);
				// Continue recursivity
				_Event_PetExpReceived(ref pet, (Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)pet[SRProperty.ExpMax]), (byte)(level + 1));
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				pet[SRProperty.Level] = (byte)(level - 1);
				// Update new ExpMax
				pet[SRProperty.ExpMax] = Info.Get.GetPetExpMax((byte)pet[SRProperty.Level]);
				_Event_PetExpReceived(ref pet, Exp + ExpReceived, (long)((ulong)pet[SRProperty.ExpMax]), (long)((ulong)pet[SRProperty.ExpMax]), (byte)(level - 1));
			}
			else
			{
				// Increase/Decrease Exp
				pet[SRProperty.Exp] = (ulong)(Exp + ExpReceived);
			}
		}
		public void _Event_Chat(Types.Chat type, string player, string message)
		{
			Window w = Window.Get;
			switch (type)
			{
				case Types.Chat.All:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				case Types.Chat.GM:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				case Types.Chat.NPC:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				case Types.Chat.Private:
					w.LogChatMessage(w.Chat_rtbxPrivate, player + "(From)", message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option02);
					break;
				case Types.Chat.Party:
					w.LogChatMessage(w.Chat_rtbxParty, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option03);
					break;
				case Types.Chat.Guild:
					w.LogChatMessage(w.Chat_rtbxGuild, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option04);
					break;
				case Types.Chat.Union:
					w.LogChatMessage(w.Chat_rtbxUnion, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option05);
					break;
				case Types.Chat.Academy:
					w.LogChatMessage(w.Chat_rtbxAcademy, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option06);
					break;
				case Types.Chat.Global:
					w.LogChatMessage(w.Chat_rtbxGlobal, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option08);
					break;
				case Types.Chat.Stall:
					w.LogChatMessage(w.Chat_rtbxStall, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option07);
					break;
				case Types.Chat.Notice:
					w.LogChatMessage(w.Chat_rtbxAll, "(Notice)", message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				default:
					w.LogChatMessage(w.Chat_rtbxAll, "(...)", message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
			}
			Event_Chat(type, player, message);
		}
		public void _Event_EntitySelected(uint uniqueID)
		{
			EntitySelected = uniqueID;
		}
		public void _Event_PartyInvitation(uint uniqueID, Types.PartySetup PartySetup)
		{
			Event_PartyInvitation(Info.Get.EntityList[uniqueID].Name, PartySetup);
		}
		public void _Event_PartyJoined(Types.PartySetup PartySetupFlags,Types.PartyPurpose PartyPurposeType)
		{
			this.inParty = true;
			this.PartySetupFlags = PartySetupFlags;
			this.PartyPurposeType = PartyPurposeType;

			// Party Setup to boolean
			bool ExpShared = PartySetupFlags.HasFlag(Types.PartySetup.ExpShared);
			bool ItemShared = PartySetupFlags.HasFlag(Types.PartySetup.ItemShared);
			bool AnyoneCanInvite = PartySetupFlags.HasFlag(Types.PartySetup.AnyoneCanInvite);

			// Update GUI with current party setup
			Window w = Window.Get;

			string partySetup = "• Exp. " + (ExpShared ? "Shared" : "Free-For-All") + " • Item " + (ItemShared ? "Shared" : "Free-For-All") + " • " + (AnyoneCanInvite ? "Anyone" : "Only Master") + " Can Invite";
			WinAPI.InvokeIfRequired(w.Party_lblCurrentSetup, () => {
				w.Party_lblCurrentSetup.Text = partySetup;
			});
			WinAPI.InvokeIfRequired(w.Party_lblCurrentSetup, () => {
				w.ToolTips.SetToolTip(w.Party_lblCurrentSetup, PartyPurposeType.ToString());
			});

			this.Event_PartyJoined();
    }
		public void _Event_MemberLeaved(uint joinID)
		{
			Info i = Info.Get;

			SRObject player = i.GetPartyMember(joinID);
			if (player.Name == i.Charname)
			{
				_Event_PartyLeaved();
			}
			else
			{
				i.PartyList.Remove(player);

				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
					w.Party_lstvPartyMembers.Items[joinID.ToString()].Remove();
				});
				Event_MemberLeaved();
			}
		}
		public void _Event_PartyLeaved()
		{
			inParty = false;
			Info.Get.PartyList.Clear();
			Window.Get.Party_Clear();

			this.Event_PartyLeaved();
		}
		public void _Event_PartyMatchListing(byte pageIndex, byte pageCount, Dictionary<uint, SRPartyMatch> PartyMatches)
		{
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch, () =>	{
				w.Party_lstvPartyMatch.Items.Clear();
			});

			// Set page changer
			WinAPI.InvokeIfRequired(w.Party_lblPageNumber, () => {
				w.Party_lblPageNumber.Text = (pageIndex + 1).ToString();
			});
			WinAPI.InvokeIfRequired(w.Party_btnLastPage, () => {
				w.Party_btnLastPage.Enabled = (pageIndex != 0);
			});
			WinAPI.InvokeIfRequired(w.Party_btnNextPage, () => {
				w.Party_btnNextPage.Enabled = (pageCount != pageIndex + 1);
			});

			// Stop drawing if is necessary
			if (PartyMatches.Count > 0){
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch, () =>{
					w.Party_lstvPartyMatch.BeginUpdate();
				});
			}

			Info i = Info.Get;
			SRPartyMatch myMatch = null;

			foreach (SRPartyMatch Match in PartyMatches.Values)
			{
				System.Windows.Forms.ListViewItem Item = new System.Windows.Forms.ListViewItem();
				Item.Text = Item.Name = Match.Number.ToString();
				Item.SubItems.Add((Match.isJobMode ? "*" : "") + Match.Owner);
				Item.SubItems.Add(Match.Title);
				Item.SubItems.Add(Match.LevelMin + "~" + Match.LevelMax);
				Item.SubItems.Add(Match.MemberCount + "/" + Match.MemberMax);
				Item.SubItems.Add(Match.Purpose.ToString());
				Item.ToolTipText = "Exp. " + (Match.Setup.HasFlag(Types.PartySetup.ExpShared) ? "Shared" : "Free - For - All") + "\nItem " + (Match.Setup.HasFlag(Types.PartySetup.ItemShared) ? "Shared" : "Free-For-All") + "\n" + (Match.Setup.HasFlag(Types.PartySetup.AnyoneCanInvite) ? "Anyone" : "Only Master") + " Can Invite";
				if ((inParty && Match.Owner == i.PartyList[0].Name) || Match.Owner == i.Charname){
					myMatch = Match;
					Item.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
				}
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch,()=>{
					w.Party_lstvPartyMatch.Items.Add(Item);
				});
			}
			// GUI loaded, continue drawing
			if (PartyMatches.Count > 0)
			{
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch, () => {
					w.Party_lstvPartyMatch.EndUpdate();
				});
			}

			Event_PartyMatchListing(myMatch);
		}
		public void _Event_StallOpened(bool isMine)
		{
			inStall = true;
			if (isMine)
				hasStall = true;
		}
		public void _Event_StallClosed()
		{
			inStall = hasStall = false;
		}
		#endregion

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
	}
}
