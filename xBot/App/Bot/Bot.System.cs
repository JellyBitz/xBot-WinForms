using xBot.Game;
using xBot.Network;
using xBot.Game.Objects;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace xBot.App
{
	public partial class Bot
	{
		private AutoResetEvent MonitorEntitySelected = new AutoResetEvent(false);
		private AutoResetEvent MonitorSkillCast = new AutoResetEvent(false);
		private AutoResetEvent MonitorMobSpawnDespawnOrBuffChanged = new AutoResetEvent(false);
		private AutoResetEvent MonitorWeaponChanged = new AutoResetEvent(false);

		private System.Timers.Timer tJoinedLoop;
		#region (Logical System Controller)
		public void _OnConnected()
		{
			Window w = Window.Get;
			w.LogProcess("Logged successfully!");
			w.EnableControl(w.Login_btnStart, false);

			this.OnConnected();
		}
		public void _OnCharacterListing(List<SRObject> CharacterList)
		{
			Window w = Window.Get;
			// Add to GUI
			for (int j = 0; j< CharacterList.Count;j++)
			{
				ListViewItem Item = new ListViewItem();
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
			this.OnCharacterListing(CharacterList);
		}
		public void _OnDisconnected()
		{
			inGame = false;
			// Try stop bot
			Stop();
			// Try stop trace
			StopTrace();

			// Set states
			inTeleport = false;
			hasStall = false;
			inParty = false;

			// Stop timers
			tUsingHP.Enabled = tUsingMP.Enabled = tUsingVigor.Enabled =
				tUsingUniversal.Enabled = tUsingPurification.Enabled =
				tUsingRecoveryKit.Enabled = tUsingAbnormalPill.Enabled =
				tUsingHGP.Enabled =
				tCycleAutoParty.Enabled =
			tJoinedLoop.Enabled = false;

			Window w = Window.Get;
			Info i = Info.Get;
			// Reset data
			i.SpawnList.Clear();
			i.MyPets.Clear();
			i.Players.Clear();
			i.Mobs.Clear();
			i.PartyMembers.Clear();
			i.BuffList.Clear();
			EntitySelected = 0;

			// Clear GUI
			w.Party_Clear();
			w.Skills_Clear();
			w.Buffs_Clear();
			w.Minimap_Objects_Clear();

			// Hacks
			LoggedFromBot = false;

			this.OnDisconnected();
		}
		public void _OnNicknameChecked(bool available)
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
		public void _OnTeleporting()
		{
			// Set states
			inTeleport = true;
			inParty = false;

			Window w = Window.Get;
			Info i = Info.Get;
			// Reset data
			EntitySelected = 0;
			i.SpawnList.Clear();
			i.MyPets.Clear();
			i.Players.Clear();
			i.Mobs.Clear();
			i.PartyMembers.Clear();
			i.BuffList.Clear();

			// Clear GUI
			w.Buffs_Clear();
			w.Party_Clear();
			w.Skills_Clear();
			w.TrainingAreas_Clear();
			w.Minimap_Objects_Clear();
			
			this.OnTeleporting();
		}
		public void _OnCharacterSpawn()
		{
			Info i = Info.Get;
			SRObject character = i.Character;
			// Initialize basic
			character[SRProperty.ExpMax] = i.GetExpMax((byte)character[SRProperty.Level]);
			character[SRProperty.JobExpMax] = i.GetJobExpMax((byte)character[SRProperty.JobLevel], (Types.Job)character[SRProperty.JobType]);
			// to avoid wrong values hp = hpmax, will be updated at stats packet anyways 
			character[SRProperty.HP] = character[SRProperty.HPMax];
			character[SRProperty.MP] = character[SRProperty.MPMax];
			character[SRProperty.BadStatusFlags] = Types.BadStatus.None;

			// Updating GUI
			Window w = Window.Get;
			#region (Character Tab)
			WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
				w.Character_pgbHP.ValueMaximum = (uint)character[SRProperty.HPMax];
				w.Character_pgbHP.Value = w.Character_pgbHP.ValueMaximum;
			});
			WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
				w.Character_pgbMP.ValueMaximum = (uint)character[SRProperty.MPMax];
				w.Character_pgbMP.Value = w.Character_pgbMP.ValueMaximum;
			});
			WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
				w.Character_lblLevel.Text = "Lv. " + character[SRProperty.Level];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
				w.Character_pgbExp.ValueMaximum = (ulong)character[SRProperty.ExpMax];
				w.Character_pgbExp.Value = (ulong)character[SRProperty.Exp];
			});
			WinAPI.InvokeIfRequired(w.Character_lblJobLevel, () => {
				w.Character_lblJobLevel.Text = "Job Lv. " + character[SRProperty.JobLevel];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbJobExp, () => {
				w.Character_pgbJobExp.ValueMaximum = (uint)character[SRProperty.JobExpMax];
				w.Character_pgbJobExp.Value = (uint)character[SRProperty.JobExp];
			});
			w.Character_SetGold((ulong)character[SRProperty.Gold]);
			WinAPI.InvokeIfRequired(w.Character_lblLocation, () => {
				w.Character_lblLocation.Text = i.GetRegion(character.GetPosition().Region);
			});
			WinAPI.InvokeIfRequired(w.Character_lblSP, () => {
				w.Character_lblSP.Text = character[SRProperty.SP].ToString();
			});
			WinAPI.InvokeIfRequired(w.Character_lblCoordX, () => {
				w.Character_lblCoordX.Text = "X : " + character.GetPosition().PosX;
			});
			WinAPI.InvokeIfRequired(w.Character_lblCoordY, () => {
				w.Character_lblCoordY.Text = "Y : " + character.GetPosition().PosY;
			});

			SRObjectDictionary<uint> Buffs = (SRObjectDictionary<uint>)character[SRProperty.Buffs];
			for (byte j = 0; j < Buffs.Count; j++)
			{
				SRObject buff = Buffs.ElementAt(j);
				// Global tracking
				i.BuffList[(uint)buff[SRProperty.UniqueID]] = buff;
				// Add to GUI
				w.AddBuff(buff);
			}

			WinAPI.InvokeIfRequired(w.Character_gbxStatPoints, () => {
				w.Character_lblStatPoints.Text = character[SRProperty.StatPoints].ToString();
				if ((ushort)character[SRProperty.StatPoints] > 0)
				{
					w.Character_btnAddSTR.Enabled = w.Character_btnAddINT.Enabled = true;
				}
			});
			#endregion

			#region (Skills Tab)
			SRObjectDictionary<uint> Skills = (SRObjectDictionary<uint>)character[SRProperty.Skills];
			// Add basic attack
			SRObject commonAttack = new SRObject(1u, SRType.Skill);
			commonAttack.Name = "Common Attack";
			commonAttack[SRProperty.MP] = 1u; // Trigger skill as attack
			commonAttack[SRProperty.Icon] = "action\\icon_cha_auto_attack.ddj";
			Skills[commonAttack.ID] = commonAttack;
			
			WinAPI.InvokeIfRequired(w.Skills_lstvSkills, () => {
				w.Skills_lstvSkills.BeginUpdate();
			});
			for (int j = 0; j < Skills.Count; j++)
				w.AddSkill(Skills.ElementAt(j));
			WinAPI.InvokeIfRequired(w.Skills_lstvSkills, () => {
				w.Skills_lstvSkills.EndUpdate();
			});
			#endregion

			#region (Minimap Tab)
			SRCoord myPos = character.GetPosition();
			WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
				w.Minimap_tbxX.Text = myPos.X.ToString();
				w.Minimap_tbxY.Text = myPos.Y.ToString();
				w.Minimap_tbxZ.Text = myPos.Z.ToString();
				w.Minimap_tbxRegion.Text = myPos.Region.ToString();
			});
			w.Minimap_Character_View(myPos, character.GetDegreeAngle());
			#endregion

			if (!inGame)
			{
				WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
					Settings.LoadCharacterSettings();
				});
				// Set window title
				w.SetTitle(i.Server, i.Charname, Proxy.SRO_Client);
			}
		}
		public void _OnCharacterStatsUpdated()
		{
			Info i = Info.Get;
			// Update GUI & game logic
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
				w.Character_pgbHP.ValueMaximum = (uint)i.Character[SRProperty.HPMax];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
				w.Character_pgbMP.ValueMaximum = (uint)i.Character[SRProperty.MPMax];
			});
			if ((uint)i.Character[SRProperty.HP] > (uint)i.Character[SRProperty.HPMax])
			{
				i.Character[SRProperty.HP] = (uint)i.Character[SRProperty.HPMax];
				WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
					w.Character_pgbHP.Value = (uint)i.Character[SRProperty.HP];
				});
			}
			if ((uint)i.Character[SRProperty.MP] > (uint)i.Character[SRProperty.MPMax])
			{
				i.Character[SRProperty.MP] = (uint)i.Character[SRProperty.MPMax];
				WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
					w.Character_pgbMP.Value = (uint)i.Character[SRProperty.MP];
				});
			}
			WinAPI.InvokeIfRequired(w.Character_lblSTR, () => {
				w.Character_lblSTR.Text = i.Character[SRProperty.STR].ToString();
			});
			WinAPI.InvokeIfRequired(w.Character_lblINT, () => {
				w.Character_lblINT.Text = i.Character[SRProperty.INT].ToString();
			});
			if ((Types.LifeState)i.Character[SRProperty.LifeState] == Types.LifeState.None) { 
				i.Character[SRProperty.LifeState] = Types.LifeState.Alive;
			}
		}
		public void _OnTeleported()
		{
			inTeleport = false;
			if (!inGame)
			{
				inGame = true;
				// Let disconnection anytime
				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Login_btnStart, () => {
					w.Login_btnStart.Text = "STOP";
				});
				w.EnableControl(w.Login_btnStart, true);

				OnGameJoined();

				tJoinedLoop = new System.Timers.Timer(200);
				tJoinedLoop.Elapsed += new System.Timers.ElapsedEventHandler(this._OnLoop);
				JoinedLoopCounter = 0;
				tJoinedLoop.Start();
      }
			this.OnTeleported();
		}
		private uint JoinedLoopCounter = 0;
		private void _OnLoop(object timer, System.Timers.ElapsedEventArgs e)
		{
			Window w = Window.Get;
			Info i = Info.Get;
			// Update character realtime position
			SRCoord p = i.Character.GetPosition();
			// Set values
			w.Character_SetPosition(p);
			// Set map view every second
			if (JoinedLoopCounter % 5 == 0)
				w.Minimap_Character_View(p, i.Character.GetDegreeAngle());
			JoinedLoopCounter++;
    }
		public void _OnSpawn(ref SRObject entity)
		{
			Info i = Info.Get;

			uint uniqueID = (uint)entity[SRProperty.UniqueID];
			i.SpawnList[uniqueID] = entity;

			if (entity.isPlayer())
			{
				i.Players[entity.Name.ToUpper()] = entity;
			}
			else if (entity.isMob())
			{
				i.Mobs[uniqueID] = entity;
			}
			else if (entity.isTeleport())
			{
				i.Teleports[uniqueID] = entity;
			}
			// Check for buffs for a global tracking
			object test = entity[SRProperty.Buffs];
			if (test != null)
			{
				SRObjectDictionary<uint> Buffs = (SRObjectDictionary<uint>)test;
				for (byte j = 0; j < Buffs.Count; j++)
				{
					SRObject buff = Buffs.ElementAt(j);
					// Global tracking
					i.BuffList[(uint)buff[SRProperty.UniqueID]] = buff;
				}
			}
			this.OnSpawn(ref entity);

			Window.Get.Minimap_Object_Add(uniqueID, entity);
		}
		public void _OnDespawn(uint uniqueID)
		{
			Info i = Info.Get;
			SRObject entity = i.SpawnList[uniqueID];

			if (entity.isPlayer())
			{
				i.Players.RemoveKey(entity.Name.ToUpper());
			}
			else if (entity.isMob())
			{
				i.Mobs.RemoveKey(uniqueID);
				MonitorMobSpawnDespawnOrBuffChanged.Set();
			}
			else if (entity.isTeleport())
			{
				i.Teleports.RemoveKey(uniqueID);
			}
			i.SpawnList.RemoveKey(uniqueID);

			Window.Get.Minimap_Object_Remove(uniqueID);
		}
		public void _OnEntityDead(uint uniqueID){
			Info i = Info.Get;
			// Entity target has been killed
			SRObject entity = i.GetEntity(uniqueID);
			// Check if the entity has been removed before 
			if(entity != null)
			{
				// Update dead state (faster detection)
				entity[SRProperty.LifeState] = Types.LifeState.Dead;
        if (entity.isMob())
				{
					i.Mobs.RemoveKey(uniqueID);
					MonitorMobSpawnDespawnOrBuffChanged.Set();

					Window.Get.Minimap_Object_Remove(uniqueID);
				}
      }
		}
		public void _OnEntityMovement(ref SRObject entity)
		{
			Info i = Info.Get;
			if (i.Character == entity)
			{
				_OnCharacterMovement(ref entity);
				return;
			}
			if (entity.isPlayer())
			{
				OnPlayerMovement(ref entity);
			}
			else if (entity.isPet())
			{
				if (entity.ID4 == 1)
				{
					// Vehicle
					uint vehicleUniqueID = (uint)entity[SRProperty.UniqueID];
					SRObject player = i.Players.Find(p => (bool)p[SRProperty.isRiding] && (uint)p[SRProperty.RidingUniqueID] == vehicleUniqueID);
					if (player != null)
					{
						player.GetPosition();
						player[SRProperty.MovementPosition] = entity[SRProperty.MovementPosition];
						_OnEntityMovement(ref player);
					}
				}
				else if (entity.ID4 == 2)
				{
					SRObject player = i.GetEntity((uint)entity[SRProperty.OwnerUniqueID]);
					if (player != null && (bool)player[SRProperty.isRiding])
					{
						player.GetPosition();
						player[SRProperty.MovementPosition] = entity[SRProperty.MovementPosition];
						OnPlayerMovement(ref player);
					}
				}
			}
		}
		public void _OnCharacterMovement(ref SRObject character)
		{
			
		}
		public void _OnEntityStatusUpdated(ref SRObject entity, Types.EntityStateUpdate type)
		{
			Info i = Info.Get;
			// Generating character event
			if (i.Character == entity)
			{
				_OnStatusUpdated(type);
			}
			else if (entity.ID1 == 1 && entity.ID2 == 2 && entity.ID3 == 3)
			{
				if (entity.ID4 != 1) 
				{
					// Check if it's my pet
					if((uint)entity[SRProperty.OwnerUniqueID] == (uint)i.Character[SRProperty.UniqueID])
						_OnPetStatusUpdated(type);
				}
				else
				{
					// Check if it's my vehicle
					if ((bool)i.Character[SRProperty.isRiding] 
						&& (uint)i.Character[SRProperty.RidingUniqueID] == (uint)entity[SRProperty.UniqueID])
					{
						_OnPetStatusUpdated(type);
					}
				}
			}
		}
		private void _OnStatusUpdated(Types.EntityStateUpdate type)
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
			this.OnStatusUpdated(type);
		}
		public void _OnExpReceived(long ExpReceived, long Exp, long ExpMax, byte Level)
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
					OnLevelUp((byte)i.Character[SRProperty.Level]);
				}
				// Continue recursivity
				_OnExpReceived((Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)i.Character[SRProperty.ExpMax]), (byte)(Level + 1));
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
				_OnExpReceived(Exp + ExpReceived, (long)((ulong)i.Character[SRProperty.ExpMax]), (long)(ulong)i.Character[SRProperty.ExpMax], (byte)(Level - 1));
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
		public void _OnItemPickedUp(SRObject item,ushort quantity){

			// generate statistics

			OnItemPickedUp(item,quantity);
		}
		public void _OnPetSummoned(uint uniqueID)
		{
			Info i = Info.Get;
			i.MyPets[uniqueID] = i.SpawnList[uniqueID];

			this.OnPetSummoned(uniqueID);
		}
		public void _OnPetUnsummoned(uint uniqueID)
		{
			OnPetUnsummoned(uniqueID);
			Info.Get.MyPets.RemoveKey(uniqueID);
		}
		public void _OnPetStatusUpdated(Types.EntityStateUpdate type)
		{
			OnPetStatusUpdated(type);
		}
		public void _OnPetExpReceived(ref SRObject pet, long ExpReceived, long Exp, long ExpMax, byte level)
		{
			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				pet[SRProperty.Level] = (byte)(level + 1);
				// Update new ExpMax
				pet[SRProperty.ExpMax] = Info.Get.GetPetExpMax((byte)pet[SRProperty.Level]);
				// Continue recursivity
				_OnPetExpReceived(ref pet, (Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)pet[SRProperty.ExpMax]), (byte)(level + 1));
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				pet[SRProperty.Level] = (byte)(level - 1);
				// Update new ExpMax
				pet[SRProperty.ExpMax] = Info.Get.GetPetExpMax((byte)pet[SRProperty.Level]);
				_OnPetExpReceived(ref pet, Exp + ExpReceived, (long)((ulong)pet[SRProperty.ExpMax]), (long)((ulong)pet[SRProperty.ExpMax]), (byte)(level - 1));
			}
			else
			{
				// Increase/Decrease Exp
				pet[SRProperty.Exp] = (ulong)(Exp + ExpReceived);
			}
		}
		public void _OnChat(Types.Chat type, string player, string message)
		{
			Window w = Window.Get;
			switch (type)
			{
				case Types.Chat.All:
					{
						Info i = Info.Get;
						if (player == Info.Get.Charname && message == "PING" && i.Ping != null)
						{
							i.Ping.Stop();
							player = "xBot";
							message = "Your current ping: "+i.Ping.ElapsedMilliseconds+"(ms)";
							PacketBuilder.Client.SendNotice(message);
							i.Ping = null;
						}
						w.LogChatMessage(w.Chat_rtbxAll, player, message);
						w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					}
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
			OnChat(type, player, message);
		}
		public void _OnEntitySelected(uint uniqueID)
		{
			EntitySelected = uniqueID;
			MonitorEntitySelected.Set();

			if(Proxy.ClientlessMode)
			{
				// SRObject entity = Info.Get.GetEntity(uniqueID);
			}
		}
		public void _OnPartyInvitation(uint uniqueID, Types.PartySetup PartySetup)
		{
			OnPartyInvitation(Info.Get.SpawnList[uniqueID].Name, PartySetup);
		}
		public void _OnPartyJoined(Types.PartySetup PartySetupFlags,Types.PartyPurpose PartyPurposeType)
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

			this.OnPartyJoined();
		}
		public void _OnMemberLeaved(uint joinID)
		{
			Info i = Info.Get;
			
			if (i.PartyMembers[joinID].Name == i.Charname)
			{
				_OnPartyLeaved();
			}
			else
			{
				i.PartyMembers.RemoveKey(joinID);

				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
					w.Party_lstvPartyMembers.Items[joinID.ToString()].Remove();
				});
				OnMemberLeaved();
			}
		}
		public void _OnPartyLeaved()
		{
			inParty = false;
			Info.Get.PartyMembers.Clear();
			Window.Get.Party_Clear();

			this.OnPartyLeaved();
		}
		public void _OnPartyMatchListing(byte pageIndex, byte pageCount, Dictionary<uint, SRPartyMatch> PartyMatches)
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
				ListViewItem Item = new ListViewItem();
				Item.Text = Item.Name = Match.Number.ToString();
				Item.SubItems.Add((Match.isJobMode ? "*" : "") + Match.Owner);
				Item.SubItems.Add(Match.Title);
				Item.SubItems.Add(Match.LevelMin + "~" + Match.LevelMax);
				Item.SubItems.Add(Match.MemberCount + "/" + Match.MemberMax);
				Item.SubItems.Add(Match.Purpose.ToString());
				Item.ToolTipText = "Exp. " + (Match.Setup.HasFlag(Types.PartySetup.ExpShared) ? "Shared" : "Free - For - All") + "\nItem " + (Match.Setup.HasFlag(Types.PartySetup.ItemShared) ? "Shared" : "Free-For-All") + "\n" + (Match.Setup.HasFlag(Types.PartySetup.AnyoneCanInvite) ? "Anyone" : "Only Master") + " Can Invite";
				if ((inParty && Match.Owner == i.PartyMembers.ElementAt(0).Name) || Match.Owner == i.Charname){
					myMatch = Match;
					Item.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
				}
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch, () => {
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

			OnPartyMatchListing(myMatch);
		}
		public void _OnStallOpened(bool isMine)
		{
			inStall = true;
			if (isMine)
				hasStall = true;
		}
		public void _OnStallClosed()
		{
			inStall = hasStall = false;
		}
		public void _OnEntityBuffAdded(uint uniqueID,SRObject buff){
			uint buffUniqueID = (uint)buff[SRProperty.UniqueID];
			// Global tracking
			Info i = Info.Get;
			i.BuffList[buffUniqueID] = buff;
			// (Easy) Entity tracking
			SRObject entity = i.GetEntity(uniqueID);
			SRObjectDictionary<uint> Buffs = (SRObjectDictionary<uint>)entity[SRProperty.Buffs];
			if (entity == i.Character)
			{
				MonitorMobSpawnDespawnOrBuffChanged.Set();

				Window w = Window.Get;
				// Check overlap to remove from GUI
				SRObject oldBuff = Buffs[buff.ID];
				if (oldBuff != null)
					w.RemoveBuff((uint)oldBuff[SRProperty.UniqueID]);
				w.AddBuff(buff);
			}
			Buffs[buff.ID] = buff;
		}
		public void _OnEntityBuffRemoved(uint buffUniqueID)
		{
			Info i = Info.Get;
			// Global tracking
			SRObject buff = i.BuffList[buffUniqueID];
			if (buff != null)
			{
				i.BuffList.RemoveKey(buffUniqueID);

				SRObject entity = i.GetEntity((uint)buff[SRProperty.OwnerUniqueID]);
				SRObjectDictionary<uint> Buffs = (SRObjectDictionary<uint>)entity[SRProperty.Buffs];

				Buffs.RemoveKey(buff.ID);
				if (i.Character == entity)
				{
					MonitorMobSpawnDespawnOrBuffChanged.Set();
					Window.Get.RemoveBuff(buffUniqueID);
				}
			}
		}
		public void _OnEntitySkillCast(byte skillType, uint skillID, uint sourceUniqueID, uint targetUniqueID)
		{
			Info i = Info.Get;
			SRObject sourceEntity = i.GetEntity(sourceUniqueID);
			sourceEntity.GetPosition(); // Force update the position

			if (sourceUniqueID == (uint)i.Character[SRProperty.UniqueID])
			{
				// Put the skill at cooldown
				SRObjectDictionary<uint> Skills = (SRObjectDictionary<uint>)i.Character[SRProperty.Skills];
				SRObject Skill = Skills[skillID];
				// Avoid tracking basic attacks
				if (Skill != null) {
					Skill[SRProperty.LastUpdateTime] = Stopwatch.StartNew();
					Skill[SRProperty.isEnabled] = false;
				}
				MonitorSkillCast.Set();
			}
		}
		public void _OnWeaponChanged()
		{
			MonitorWeaponChanged.Set();
		}
		#endregion
	}
}
