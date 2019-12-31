using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using xBot.App;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Guild;
using xBot.Game.Objects.Item;
using xBot.Game.Objects.Party;

namespace xBot.Game
{
	/// <summary>
	/// Provide all the information stored into the client used to control the GUI and bot events.
	/// </summary>
	public static class InfoManager
	{
		#region (Information handled)
		private static xDictionary<uint, SREntity> m_Entities = new xDictionary<uint, SREntity>();
		private static xDictionary<string, SRPlayer> m_Players = new xDictionary<string, SRPlayer>();
		private static xDictionary<uint, SRCoService> m_PetsOwned = new xDictionary<uint, SRCoService>();
		private static xDictionary<uint, SRTeleport> m_TeleportAndBuildings = new xDictionary<uint, SRTeleport>();
		private static xDictionary<uint, SRMob> m_Mobs = new xDictionary<uint, SRMob>();
		private static xDictionary<uint, SRNpc> m_Npcs = new xDictionary<uint, SRNpc>();
		private static xDictionary<uint, SRBuff> m_Buffs = new xDictionary<uint, SRBuff>();
		private static SRTimeStamp m_SRTimeStamp;
		private static DateTime m_SRTimeStampDate;
		private static AutoResetEvent m_MonitorEntitySelected = new AutoResetEvent(false);
		private static AutoResetEvent m_MonitorInventoryMovement = new AutoResetEvent(false);
		private static AutoResetEvent m_MonitorWeaponChanged = new AutoResetEvent(false);
		private static AutoResetEvent m_MonitorBuffRemoved = new AutoResetEvent(false);
		private static AutoResetEvent m_MonitorSkillCast = new AutoResetEvent(false);
		private static AutoResetEvent m_MonitorMobSpawnChanged = new AutoResetEvent(false);
		private static int m_stallViewsCount;
		private static ulong m_stallEarnings;
		#endregion

		#region (Public Information)
		/// <summary>
		/// Server name.
		/// </summary>
		public static string ServerName { get; set; }
		/// <summary>
		/// Server ID. Returns an empty string if is not selected yet.
		/// </summary>
		public static string ServerID { get; set; }
		/// <summary>
		/// Gets the character name, available right before the character is selected.
		/// </summary>
		public static string CharName { get; private set; }
		/// <summary>
		/// Gets the character name, available right before the character is selected.
		/// </summary>
		public static SRCharacter Character { get; private set; }
		/// <summary>
		/// Check if the character is in teleport.
		/// </summary>
		public static bool inTeleport { get; private set; }
		/// <summary>
		/// Check if the character is in game.
		/// </summary>
		public static bool inGame { get; private set; }
		/// <summary>
		/// Get the last entity unique ID selected.
		/// </summary>
		public static uint SelectedEntityUniqueID { get; private set; }
		/// <summary>
		/// Gets all entities around.
		/// </summary>
		public static xDictionary<uint, SREntity> Entities { get { return m_Entities; } }
		/// <summary>
		/// Gets all players around.
		/// </summary>
		public static xDictionary<string,SRPlayer> Players { get { return m_Players; } }
		/// <summary>
		/// Gets all pets owned.
		/// </summary>
		public static xDictionary<uint, SRCoService> MyPets { get { return m_PetsOwned; } }
		/// <summary>
		/// Gets all pets owned.
		/// </summary>
		public static xDictionary<uint, SRNpc> Npcs { get { return m_Npcs; } }
		/// <summary>
		/// Gets all teleports and buildings around.
		/// </summary>
		public static xDictionary<uint, SRTeleport> TeleportAndBuildings { get { return m_TeleportAndBuildings; } }
		/// <summary>
		/// Gets all mobs around.
		/// </summary>
		public static xDictionary<uint, SRMob> Mobs { get { return m_Mobs; } }
		/// <summary>
		/// Gets the party information.
		/// </summary>
		public static SRParty Party { get; private set; }
		/// <summary>
		/// Check if the character is in Party.
		/// </summary>
		public static bool inParty { get { return Party != null; } }
		/// <summary>
		/// Gets the guild information.
		/// </summary>
		public static SRGuild Guild { get; private set; }
		/// <summary>
		/// Check if the character is in guild.
		/// </summary>
		public static bool inGuild { get { return Guild != null; } }
		/// <summary>
		/// Check if the character is in academy.
		/// </summary>
		public static bool inAcademy { get; private set; }
		/// <summary>
		/// Check if the character is inside of stall, including his own.
		/// </summary>
		public static bool inStall { get; private set; }
		/// <summary>
		/// Get the owner from stall or null.
		/// </summary>
		public static SRPlayer StallerPlayer { get; private set; }
		/// <summary>
		/// Check if the character is exchanging with another player.
		/// </summary>
		public static bool inExchange { get { return ExchangerPlayer != null; } }
		/// <summary>
		/// Get the entity from exchange or null.
		/// </summary>
		public static SRPlayer ExchangerPlayer { get; private set; }
		/// <summary>
		/// Check if the exchanger has confirmed.
		/// </summary>
		public static bool isExchangerConfirmed { get; private set; }
		/// <summary>
		/// Check if the storage has been requested at least once after teleport.
		/// </summary>
		public static bool isStorageLoaded { get; private set; }
		/// <summary>
		/// Check if the character is with storage open.
		/// </summary>
		public static bool inStorage { get; private set; }
		#endregion

		#region (Monitors)
		public static AutoResetEvent MonitorEntitySelected { get { return m_MonitorEntitySelected; } }
		public static AutoResetEvent MonitorWeaponChanged { get { return m_MonitorWeaponChanged; } }
		public static AutoResetEvent MonitorInventoryMovement { get { return m_MonitorInventoryMovement; } }
		public static AutoResetEvent MonitorBuffRemoved { get { return m_MonitorBuffRemoved; } }
		public static AutoResetEvent MonitorSkillCast { get { return m_MonitorSkillCast; } }
		public static AutoResetEvent MonitorMobSpawnChanged { get { return m_MonitorMobSpawnChanged; } }
		#endregion

		#region (Methods)
		public static void SetCharacter(string CharName)
		{
			InfoManager.CharName = CharName;

			Window w = Window.Get;
			w.Login_btnStart.InvokeIfRequired(() => {
				w.Login_btnStart.Enabled = false;
			});
			w.LogProcess("Selecting ["+ CharName + "] ...");
			PacketBuilder.SelectCharacter(CharName);
		}
		public static void SetCredentials(string Username,string Password,string ServerName)
		{
			InfoManager.ServerName = ServerName;

			Window w = Window.Get;
			w.LogProcess("Login...");
			Bot.Get.LoggedFromBot = true;
			PacketBuilder.Login(Username,Password,ushort.Parse(ServerID));
		}
		public static void SetServerTime(SRTimeStamp SRTimeStamp)
		{
			m_SRTimeStampDate = DateTime.UtcNow;
			m_SRTimeStamp = SRTimeStamp;
		}
		/// <summary>
		/// Server time synchronized with the last SRTimeStamp set.
		/// </summary>
		public static DateTime GetServerTime()
		{
			if (m_SRTimeStampDate != null)
				return m_SRTimeStamp.DateTime.Add(DateTime.UtcNow.Subtract(m_SRTimeStampDate));
			return DateTime.UtcNow;
		}
		public static SREntity GetEntity(uint uniqueID)
		{
			if (uniqueID == Character.UniqueID)
				return Character;
			SREntity entity = m_Entities[uniqueID];
			if (entity != null)
				return entity;
			return m_PetsOwned[uniqueID];
		}
		public static bool isEntityNear(uint uniqueID)
		{
			return m_Entities[uniqueID] != null;
		}
		#endregion

		#region (Event System)
		internal static void OnConnected()
		{
			Window w = Window.Get;
			w.LogProcess("Logged successfully!");
			w.Login_btnStart.InvokeIfRequired(() => {
				w.Login_btnStart.Enabled = false;
			});

			Bot.Get.OnConnected();
		}
		internal static void OnCharacterListing(List<SRCharSelection> CharacterList)
		{
			// Add to GUI & control autologin if has one
			Window w = Window.Get;
			Bot b = Bot.Get;

			for (int j = 0; j < CharacterList.Count; j++)
			{
				ListViewItem Item = new ListViewItem();
				Item.Text = CharacterList[j].Name + (CharacterList[j].isDeleting ? " (*)" : "");
				Item.Name = CharacterList[j].Name;
				Item.SubItems.Add(CharacterList[j].Level.ToString());
				Item.SubItems.Add(CharacterList[j].GetExpPercent() + " %");
				if (CharacterList[j].isDeleting)
					Item.SubItems.Add(CharacterList[j].DeletingDate.ToString("dd/mm/yyyy hh:mm"));

				// Add to view
				w.Login_lstvCharacters.InvokeIfRequired(() => {
					w.Login_lstvCharacters.Items.Add(Item);
				});

				// Auto select if has autologin activated
				if (!CharacterList[j].isDeleting)
				{
					w.Login_cmbxCharacter.InvokeIfRequired(() => {
						w.Login_cmbxCharacter.Items.Add(Item.Name);
						if (b.hasAutoLoginMode
						&& w.Login_cmbxCharacter.Tag != null
						&& CharacterList[j].Name.Equals((string)w.Login_cmbxCharacter.Tag, StringComparison.OrdinalIgnoreCase))
						{
							w.Login_cmbxCharacter.Text = CharacterList[j].Name;
						}
					});
				}
			}
			// Switch Listview's [Servers to Characters] selection 
			w.Login_gbxServers.InvokeIfRequired(() => {
				w.Login_gbxServers.Visible = false;
			});
			w.Login_gbxCharacters.InvokeIfRequired(() => {
				w.Login_gbxCharacters.Visible = true;
			});
			// Switch and restaure [LOGIN to SELECT] button
			w.Login_btnStart.InvokeIfRequired(() => {
				w.Login_btnStart.Text = "SELECT";
				w.Login_btnStart.Enabled = true;
			});
			w.LogProcess("Character selection");

			b.OnCharacterListing(CharacterList);
		}
		internal static void OnDisconnected()
		{
			inGame = false;
			inTeleport = false;

			// Try stop bot process
			Bot b = Bot.Get;
			b.Stop();
			b.StopTrace();
			b.StopInventorySort();
			// Reset data
			SelectedEntityUniqueID = 0;
			m_Entities.Clear();
			m_Players.Clear();
			m_Mobs.Clear();
			m_PetsOwned.Clear();
			m_Buffs.Clear();
			m_TeleportAndBuildings.Clear();
			OnTalkClose();

			// GUI
			Window w = Window.Get;
			w.SetIngameButtons(false);
			w.Character_Buffs_Clear();
			if (inExchange)
			{
				ExchangerPlayer = null;
				w.Players_ClearExchange();
			}
			if (inParty)
			{
				Party = null;
				w.Party_Clear();
			}
			if (inGuild)
			{
				Guild = null;
				w.Guild_Clear();
			}
			inAcademy = false;
			w.Skills_Clear();
			if (inStall)
			{
				inStall = false;
				StallerPlayer = null;
				w.Stall_Clear();
			}
			w.Minimap_Objects_Clear();
			
			b.OnDisconnected();
		}
		internal static void OnTeleporting()
		{
			inTeleport = true;

			// Try stop bot process
			Bot b = Bot.Get;
			b.StopInventorySort();

			// Reset data
			SelectedEntityUniqueID = 0;
			m_Entities.Clear();
			m_Players.Clear();
			m_Mobs.Clear();
			m_Buffs.Clear();
			m_PetsOwned.Clear();
			m_TeleportAndBuildings.Clear();
			isStorageLoaded = false;
			OnTalkClose();

			// GUI
			Window w = Window.Get;
			w.SetIngameButtons(false);
			w.Character_Buffs_Clear();
			if (inExchange)
			{
				ExchangerPlayer = null;
				w.Players_ClearExchange();
			}
			if (inParty)
			{
				Party = null;
				w.Party_Clear();
			}
			if (inGuild)
			{
				Guild = null;
				w.Guild_Clear();
			}
			inAcademy = false;
			w.Skills_Clear();
			if (inStall)
			{
				inStall = false;
				StallerPlayer = null;
				w.Stall_Clear();
			}
			w.TrainingAreas_Clear();
			w.Minimap_Objects_Clear();

			if (inGame)
				w.LogProcess("Teleporting...");
			else
				w.LogProcess("Loading...");

			b.OnTeleporting();
		}
		internal static void OnCharacterInfo(SRCharacter Character)
		{
			// Keep some info if has been loaded before
			if(InfoManager.Character != null)
			{
				Character.Storage = InfoManager.Character.Storage;
				Character.StorageGold = InfoManager.Character.StorageGold;
			}
			InfoManager.Character = Character;

			// GUI
			Window w = Window.Get;
			#region (Character Tab)
			w.Character_pgbHP.InvokeIfRequired(() => {
				w.Character_pgbHP.ValueMaximum = Character.HPMax;
				w.Character_pgbHP.Value = w.Character_pgbHP.ValueMaximum;
			});
			w.Character_pgbMP.InvokeIfRequired(() => {
				w.Character_pgbMP.ValueMaximum = Character.MPMax;
				w.Character_pgbMP.Value = w.Character_pgbMP.ValueMaximum;
			});
			w.Character_lblLevel.InvokeIfRequired(() => {
				w.Character_lblLevel.Text = "Lv. " + Character.Level;
			});
			w.Character_pgbExp.InvokeIfRequired(() => {
				w.Character_pgbExp.ValueMaximum = Character.ExpMax;
				w.Character_pgbExp.Value = Character.Exp;
			});
			w.Character_lblJobLevel.InvokeIfRequired(() => {
				w.Character_lblJobLevel.Text = "Job Lv. " + Character.JobLevel;
			});
			w.Character_pgbJobExp.InvokeIfRequired(() => {
				w.Character_pgbJobExp.ValueMaximum = Character.JobExpMax;
				w.Character_pgbJobExp.Value = Character.JobExp;
			});
			w.Character_lblSP.InvokeIfRequired(() => {
				w.Character_lblSP.Text = Character.SP.ToString();
			});
			w.Character_SetGold(Character.Gold);
			w.Character_SetPosition(Character.Position);

			w.Character_gbxStatPoints.InvokeIfRequired(() => {
				w.Character_lblStatPoints.Text = Character.StatPoints.ToString();
				if (Character.StatPoints > 0)
					w.Character_btnAddSTR.Enabled = w.Character_btnAddINT.Enabled = true;
			});
			for (byte j = 0; j < Character.Buffs.Count; j++)
			{
				SRBuff buff = Character.Buffs.GetAt(j);
				w.AddBuff(buff);
				// Add buff to world tracking
				m_Buffs[buff.UniqueID] = buff;
			}
			#endregion

			#region (Skills Tab)
			// Add basic attack
			SRSkill commonAttack = new SRSkill(1u);
			commonAttack.Name = "Common Attack";
			commonAttack.MPUsage = 1u; // Trigger skill as attack
			commonAttack.Icon = "action\\icon_cha_auto_attack.ddj";
			Character.Skills[commonAttack.ID] = commonAttack;

			w.Skills_lstvSkills.InvokeIfRequired(() => {
				w.Skills_lstvSkills.BeginUpdate();
			});
			for (int j = 0; j < Character.Skills.Count; j++)
				w.AddSkill(Character.Skills.GetAt(j));
			w.Skills_lstvSkills.InvokeIfRequired(() => {
				w.Skills_lstvSkills.EndUpdate();
			});
			#endregion

			#region (Minimap Tab)
			w.Minimap_Character_View(Character.Position,Character.GetDegreeAngle());
			#endregion

			if (!inGame)
			{
				w.Minimap_panelCoords.InvokeIfRequired(() => {
					Settings.LoadCharacterSettings();
				});
				// Set window title
				w.SetTitle(ServerName,CharName, Bot.Get.Proxy.SRO_Client);
			}
		}
		internal static void OnTeleported()
		{
			inTeleport = false;

			// GUI
			Window w = Window.Get;
			w.SetIngameButtons(true);

			if (!inGame)
			{
				inGame = true;
				// Enable button
				w.Login_btnStart.InvokeIfRequired(() => {
					w.Login_btnStart.Text = "STOP";
					w.Login_btnStart.Enabled = true;
				});
				Bot.Get.OnGameJoined();
			}
			w.LogProcess("Teleported");

			Bot.Get.OnTeleported();
		}
		internal static void OnCharacterStatsUpdated()
		{
			if (Character.LifeStateType == SRModel.LifeState.Unknown)
				Character.LifeStateType = SRModel.LifeState.Alive;

			// Update GUI
			Window w = Window.Get;
			w.Character_pgbHP.InvokeIfRequired(() => {
				w.Character_pgbHP.ValueMaximum = Character.HPMax;
			});
			w.Character_pgbMP.InvokeIfRequired(() => {
				w.Character_pgbMP.ValueMaximum = Character.MPMax;
			});
			if(Character.HP > Character.HPMax)
			{
				Character.HP = Character.HPMax;
				w.Character_pgbHP.InvokeIfRequired(() => {
					w.Character_pgbHP.Value = Character.HP;
				});
			}
			if (Character.MP > Character.MPMax)
			{
				Character.MP = Character.MPMax;
				w.Character_pgbMP.InvokeIfRequired(() => {
					w.Character_pgbMP.Value = Character.MP;
				});
			}
			w.Character_lblSTR.InvokeIfRequired(() => {
				w.Character_lblSTR.Text = Character.STR.ToString();
			});
			w.Character_lblINT.InvokeIfRequired(() => {
				w.Character_lblINT.Text = Character.INT.ToString();
			});
		}
		internal static void OnExpReceived(long ExpReceived, long Exp, long ExpMax, byte Level)
		{
			Window w = Window.Get;
			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				Character.Level += 1;
				w.Character_lblLevel.InvokeIfRequired(() => {
					w.Character_lblLevel.Text = "Lv. " + Character.Level;
				});
				w.Character_pgbExp.InvokeIfRequired(() => {
					w.Character_pgbExp.ValueMaximum = Character.ExpMax;
				});
				if (Character.Level > Character.LevelMax)
				{
					Character.LevelMax = Character.Level;
					Character.StatPoints += 3;
					w.Character_gbxStatPoints.InvokeIfRequired(() => {
						w.Character_lblStatPoints.Text = Character.StatPoints.ToString();
						w.Character_btnAddSTR.Enabled = w.Character_btnAddINT.Enabled = true;
					});
					w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_STRGERR_LEVEL", Character.Level));
				}
				Bot.Get.OnLevelUp(Character.Level);
				// Continue recursivity
				OnExpReceived((Exp + ExpReceived) - ExpMax, 0L, (long)Character.Exp, Character.Level);
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				Character.Level -= 1;
				w.Character_lblLevel.InvokeIfRequired(() => {
					w.Character_lblLevel.Text = Character.Level.ToString();
				});
				w.Character_pgbExp.InvokeIfRequired(() => {
					w.Character_pgbExp.ValueMaximum = Character.ExpMax;
				});
				// Continue recursivity
				OnExpReceived(Exp + ExpReceived, (long)Character.ExpMax, (long)Character.ExpMax, Character.Level);
			}
			else
			{
				// Increase/Decrease Exp
				Character.Exp = (ulong)(Exp + ExpReceived);
				w.Character_pgbExp.InvokeIfRequired(() => {
					w.Character_pgbExp.Value = Character.Exp;
				});
			}
		}
		internal static void OnCharacterDead(byte ReasonType)
		{
			Character.LifeStateType = SRModel.LifeState.Dead;
			Character.Buffs.Clear();
			if (inStall)
				OnStallClosed();

			// GUI
			Window.Get.Character_Buffs_Clear();
			
			Bot.Get.OnCharacterDead();
		}
		internal static void OnSpawn(SREntity entity)
		{
			m_Entities[entity.UniqueID] = entity;

			if (entity.isModel())
			{
				SRModel model = (SRModel)entity;
				if (model.isPlayer())
				{
					m_Players[entity.Name.ToUpper()] = (SRPlayer)model;
				}
				else if (model.isNPC())
				{
					SRNpc npc = (SRNpc)model;
					if (npc.isMob())
					{
						m_Mobs[entity.UniqueID] = (SRMob)npc;
					}
					else if (npc.isGuide())
					{
						m_Npcs[entity.UniqueID] = npc;
          }
				}
				// Add all buffs to global tracking
				for(byte j = 0; j < model.Buffs.Count; j++)
				{
					SRBuff buff = model.Buffs.GetAt(j);
					m_Buffs[buff.UniqueID] = buff;
				}
			}
			else if (entity.isTeleport())
			{
				m_TeleportAndBuildings[entity.UniqueID] = (SRTeleport)entity;
			}

			Window.Get.Minimap_Object_Add(entity.UniqueID, entity);

			Bot.Get.OnSpawn(entity);
		}
		internal static void OnDespawn(uint UniqueID)
		{
			SREntity entity = m_Entities[UniqueID];
			m_Entities.RemoveKey(UniqueID);
			
			if (entity.isModel())
			{
				SRModel model = (SRModel)entity;
				if (model.isPlayer())
				{
					m_Players.RemoveKey(entity.Name.ToUpper());
				}
				else if (model.isNPC())
				{
					SRNpc npc = (SRNpc)model;
					if (npc.isMob())
					{
						m_Mobs.RemoveKey(UniqueID);
					}
					else if (npc.isGuide())
					{
						m_Npcs.RemoveKey(UniqueID);
					}
				}
			}
			else if (entity.isTeleport())
			{
				m_TeleportAndBuildings.RemoveKey(UniqueID);
			}
			// GUI
			Window.Get.Minimap_Object_Remove(UniqueID);

			Bot.Get.OnDespawn(UniqueID);
		}
		internal static void OnPartyInfo(SRParty Party)
		{
			InfoManager.Party = Party;

			// Visuals
			Window w = Window.Get;
			// Party info setup
			bool ExpShared = Party.SetupFlags.HasFlag(SRParty.Setup.ExpShared);
			bool ItemShared = Party.SetupFlags.HasFlag(SRParty.Setup.ItemShared);
			bool AnyoneCanInvite = Party.SetupFlags.HasFlag(SRParty.Setup.AnyoneCanInvite);

			string partySetup = string.Format("• Exp. {0} • Item {1} • {2} Can Invite", ExpShared ? "Shared" : "Free-For-All", ItemShared ? "Shared" : "Free-For-All", AnyoneCanInvite ? "Anyone" : "Only Master");
			w.Party_lblCurrentSetup.InvokeIfRequired(() => {
				w.Party_lblCurrentSetup.Text = partySetup;
				w.ToolTips.SetToolTip(w.Party_lblCurrentSetup, Party.PurposeType.ToString());
			});

			// Create Party view
			w.Party_lstvPartyMembers.InvokeIfRequired(() => {
				w.Party_lstvPartyMembers.BeginUpdate();
			});
			for (byte j = 0; j < Party.Members.Count; j++)
			{
				SRPartyMember member = Party.Members.GetAt(j);
				w.Party_AddMember(member, CharName.Equals(member.Name));
			}
			w.Party_lstvPartyMembers.InvokeIfRequired(() => {
				w.Party_lstvPartyMembers.EndUpdate();
			});

			// Generate bot logical event
			Bot.Get.OnPartyJoined();
		}

		internal static void OnPartyMemberJoined(SRPartyMember NewMember)
		{
			Party.Members[NewMember.ID] = NewMember;
			Window.Get.Party_AddMember(NewMember, false);
		}
		internal static void OnPartyLeft()
		{
			Party = null;

			Window.Get.Party_Clear();

			Bot.Get.OnPartyLeft();
		}
		internal static void OnPartyMemberLeft(uint ID)
		{
			if (Party.Members[ID].Name == CharName)
			{
				OnPartyLeft();
			}
			else
			{
				Party.Members.RemoveKey(ID);

				Window w = Window.Get;
				w.Party_lstvPartyMembers.InvokeIfRequired(() => {
					w.Party_lstvPartyMembers.Items[ID.ToString()].Remove();
				});

				Bot.Get.OnMemberLeaved();
			}
		}
		internal static void OnGuildInfo(SRGuild Guild)
		{
			// Keep some info if has been loaded before
			if(InfoManager.Guild != null)
				Guild.Storage = InfoManager.Guild.Storage;

			InfoManager.Guild = Guild;
		}
		internal static void OnAcademyInfo()
		{


		}
		internal static void OnEntitySelected(uint UniqueID)
		{
			SelectedEntityUniqueID = UniqueID;
			m_MonitorEntitySelected.Set();
		}
		internal static void OnEntityMovement(ref SRModel entity)
		{
			if (Character == entity)
			{
				return;
			}
			else if (entity.isPlayer())
			{
				Bot.Get.OnPlayerMovement((SRPlayer)entity);
			}
			else if (entity.isNPC())
			{
				SRNpc npc = (SRNpc)entity;
				if (npc.isCOS())
				{
					SRCoService CoS = (SRCoService)npc;
					if (CoS.isHorse())
					{
						// Update owner position
						SRPlayer owner;
						if(Character.isRiding && Character.RidingUniqueID == CoS.UniqueID)
							owner = Character;
						else
							owner = m_Players.Find(o => o.isRiding && o.RidingUniqueID == CoS.UniqueID);
						// Just in case
						if (owner != null)
						{
							owner.Position = CoS.Position;
							owner.MovementPosition = CoS.MovementPosition;
							owner.PositionUpdateTimer.Restart();
						}
					}
					else if (CoS.isTransport())
					{
						SREntity owner = GetEntity(CoS.OwnerUniqueID);
						if (owner != null)
						{
							SRPlayer player = ((SRPlayer)owner);
							if (player.isRiding)
							{
								player.Position = CoS.Position;
								player.MovementPosition = CoS.MovementPosition;
								player.PositionUpdateTimer.Restart();
								Bot.Get.OnPlayerMovement(player);
							}
						}
					}
				}
			}
		}
		internal static void OnChatReceived(SRTypes.Chat updateType, string player, string message)
		{
			Window w = Window.Get;
			switch (updateType)
			{
				case SRTypes.Chat.All:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				case SRTypes.Chat.GM:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				case SRTypes.Chat.NPC:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				case SRTypes.Chat.Private:
					w.LogChatMessage(w.Chat_rtbxPrivate, player + "(From)", message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option02);
					break;
				case SRTypes.Chat.Party:
					w.LogChatMessage(w.Chat_rtbxParty, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option03);
					break;
				case SRTypes.Chat.Guild:
					w.LogChatMessage(w.Chat_rtbxGuild, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option04);
					break;
				case SRTypes.Chat.Union:
					w.LogChatMessage(w.Chat_rtbxUnion, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option05);
					break;
				case SRTypes.Chat.Academy:
					w.LogChatMessage(w.Chat_rtbxAcademy, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option06);
					break;
				case SRTypes.Chat.Global:
					w.LogChatMessage(w.Chat_rtbxGlobal, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option08);
					break;
				case SRTypes.Chat.Stall:
					w.LogChatMessage(w.Chat_rtbxStall, player, message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option07);
					break;
				case SRTypes.Chat.Notice:
					w.LogChatMessage(w.Chat_rtbxAll, "(Notice)", message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
				default:
					w.LogChatMessage(w.Chat_rtbxAll, "(...)", message);
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option01);
					break;
			}

			Bot.Get.OnChatReceived(updateType, player, message);
		}

		internal static void OnEntityStatusUpdated(SRTypes.EntityStateUpdate updateType, SRModel entity)
		{
			if(entity == Character)
			{
				// GUI bars
				Window w = Window.Get;
				switch (updateType)
				{
					case SRTypes.EntityStateUpdate.HP:
						w.Character_pgbHP.InvokeIfRequired(() => {
							w.Character_pgbHP.Value = Character.HP;
						});
						break;
					case SRTypes.EntityStateUpdate.MP:
						w.Character_pgbMP.InvokeIfRequired(() => {
							w.Character_pgbMP.Value = Character.MP;
						});
						break;
					case SRTypes.EntityStateUpdate.HPMP:
						w.Character_pgbHP.InvokeIfRequired(() => {
							w.Character_pgbHP.Value = Character.HP;
						});
						w.Character_pgbMP.InvokeIfRequired(() => {
							w.Character_pgbMP.Value = Character.MP;
						});
						break;
				}
				if(Character.LifeStateType == SRModel.LifeState.Alive)
					Bot.Get.OnStatusUpdated(updateType);
			}
			else if (entity.isNPC())
			{
				SRNpc npc = (SRNpc)entity;
				if (npc.isCOS())
				{
					SRCoService cos = (SRCoService)npc;
					if(cos.isHorse() )
					{
						if(cos.UniqueID == Character.RidingUniqueID)
							Bot.Get.OnPetStatusUpdated(updateType);
					}
					else
					{
						if(cos.OwnerUniqueID == Character.UniqueID)
							Bot.Get.OnPetStatusUpdated(updateType);
					}
				}
			}
		}

		internal static void OnExchangeStart(uint uniqueID)
		{
			ExchangerPlayer = (SRPlayer)GetEntity(uniqueID);

			// GUI
			Window w = Window.Get;
			// Exchanger
			w.Players_lblExchangeStatus.InvokeIfRequired(() => {
				w.Players_lblExchangeStatus.Text = "Waiting confirmation...";
			});
			w.Players_lblExchangerName.InvokeIfRequired(() => {
				w.Players_lblExchangerName.Text = ExchangerPlayer.GetFullName();
			});
			w.Players_tbxExchangerGold.InvokeIfRequired(() => {
				w.Players_tbxExchangerGold.Text = "0";
			});
			// Me
			w.Players_lblExchangerMyName.InvokeIfRequired(() => {
				w.Players_lblExchangerMyName.Text = Character.Name;
			});
			// Create inventory for exchanging
			w.Players_lstvInventoryExchange.InvokeIfRequired(() => {
				w.Players_lstvInventoryExchange.BeginUpdate();
			});
			for (byte j = 13; j < Character.Inventory.Capacity; j++)
			{
				SRItem item = Character.Inventory[j];
				if (item != null)
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Name = j.ToString();
					listViewItem.Text = item.GetFullName() + item.GetQuantityText();
					listViewItem.ToolTipText = item.GetTooltip();
					listViewItem.ImageKey = w.GetImageKeyIcon(item.Icon);
					w.Players_lstvInventoryExchange.InvokeIfRequired(() => {
						w.Players_lstvInventoryExchange.Items.Add(listViewItem);
					});
				}
			}
			w.Players_lstvInventoryExchange.InvokeIfRequired(() => {
				w.Players_lstvInventoryExchange.EndUpdate();
			});
			// turn on controls
			System.Drawing.Color gForeColor = w.GetGoldColor(Character.Gold);
			w.Players_tbxGoldRemain.InvokeIfRequired(() => {
				w.Players_tbxGoldRemain.Text = Character.Gold.ToString("#,0");
				w.Players_tbxGoldRemain.ForeColor = gForeColor;
			});
			w.Players_tbxExchangingGold.InvokeIfRequired(() => {
				w.Players_tbxExchangingGold.Text = "0";
				w.Players_tbxExchangingGold.ReadOnly = false;
			});
			w.Players_btnCancelExchange.InvokeIfRequired(() => {
				w.Players_btnCancelExchange.Enabled = true;
			});
			w.Players_btnExchange.InvokeIfRequired(() => {
				w.Players_btnExchange.Enabled = true;
			});
			w.Players_btnExchangingGoldEdit.InvokeIfRequired(() => {
				w.Players_btnExchangingGoldEdit.Enabled = true;
			});
			w.Log("Exchange [" + ExchangerPlayer.Name + "] started");
		}

		internal static void OnExchangePlayerConfirmed()
		{
			isExchangerConfirmed = true;

			Window w = Window.Get;
			w.Players_lblExchangeStatus.InvokeIfRequired(() => {
				w.Players_lblExchangeStatus.Text = "Waiting approvation...";
			});
			w.Players_btnExchange.InvokeIfRequired(() => {
				w.Players_btnExchange.Enabled = true;
			});

			Bot.Get.OnExchangePlayerConfirmed();
		}

		internal static void OnExchangeCompleted()
		{
			// Exchanger - Add new items to my inventory
			if (ExchangerPlayer.InventoryExchange != null)
			{
				for (byte j = 0; j < ExchangerPlayer.InventoryExchange.Capacity; j++)
				{
					byte emptySlot = (byte)Character.Inventory.FindIndex(item => item == null, 13);
					Character.Inventory[emptySlot] = ExchangerPlayer.InventoryExchange[j].Item;
				}
				ExchangerPlayer.InventoryExchange.Clear();
			}
			// Me - Remove exchanging items from my inventory
			if (Character.InventoryExchange != null)
			{
				for (byte j = 0; j < Character.InventoryExchange.Capacity; j++)
				{
					byte SlotInventory = Character.InventoryExchange[j].SlotInventory;
					Character.Inventory[SlotInventory] = null;
				}
				Character.InventoryExchange.Clear();
			}
			
			// GUI
			Window w = Window.Get;
			w.Players_lblExchangeStatus.InvokeIfRequired(() => {
				w.Players_lblExchangeStatus.Text = "Exchange completed!";
			});
			w.Players_ClearExchange();

			// Exchange finished
			ExchangerPlayer = null;
			isExchangerConfirmed = false;
			w.Log("Exchange completed!");
		}
		internal static void OnExchangeItemsUpdate(SRPlayer player,xList<SRItemExchange> inventoryExchange)
		{
			// GUI
			Window w = Window.Get;
			// Select the ListView
			ListView inventoryView;
			if (player == Character)
			{
				inventoryView = w.Players_lstvExchangingItems;
			}
			else
			{
				inventoryView = w.Players_lstvExchangerItems;
				player.InventoryExchange = inventoryExchange;
			}
			// Update Listview
			inventoryView.InvokeIfRequired(() => {
				inventoryView.BeginUpdate();
				inventoryView.Items.Clear();
			});
			for (byte j = 0; j < player.InventoryExchange.Count; j++)
			{
				SRItem item = player.InventoryExchange[j].Item;
				ListViewItem listViewItem = new ListViewItem(item.GetFullName() + item.GetQuantityText());
				listViewItem.ImageKey = w.GetImageKeyIcon(item.Icon);
				listViewItem.ToolTipText = item.GetTooltip();

				inventoryView.InvokeIfRequired(() => {
					inventoryView.Items.Add(listViewItem);
				});
			}
			inventoryView.InvokeIfRequired(() => {
				inventoryView.EndUpdate();
			});
		}
		internal static void OnExchangeGoldUpdate(ulong Gold, bool isMe)
		{
			Window w = Window.Get;
			// Set visuals
			string gText = Gold.ToString("#,0");
			System.Drawing.Color gForeColor = w.GetGoldColor(Gold);
			// Update it
			if (isMe)
			{
				w.Players_tbxExchangingGold.InvokeIfRequired(() => {
					w.Players_tbxExchangingGold.Text = gText;
					w.Players_tbxExchangingGold.ForeColor = gForeColor;
				});
				// Set gold difference
				Gold = Character.Gold - Gold;
				gText = Gold.ToString("#,0");
				gForeColor = w.GetGoldColor(Gold);
				w.Players_tbxGoldRemain.InvokeIfRequired(() => {
					w.Players_tbxGoldRemain.Text = gText;
					w.Players_tbxGoldRemain.ForeColor = gForeColor;
				});
			}
			else
			{
				w.Players_tbxExchangerGold.InvokeIfRequired(() => {
					w.Players_tbxExchangerGold.Text = gText;
					w.Players_tbxExchangerGold.ForeColor = gForeColor;
				});
			}
		}

		internal static void OnExchangeConfirmed()
		{
			Window w = Window.Get;
			w.Players_btnExchange.InvokeIfRequired(() => {
				w.Players_btnExchange.Text = "Approve";
				if (!isExchangerConfirmed)
					w.Players_btnExchange.Enabled = false;
			});
			w.Players_tbxExchangingGold.InvokeIfRequired(() => {
				w.Players_tbxExchangingGold.ReadOnly = true;
			});

			Bot.Get.OnExchangeConfirmed();
		}

		internal static void OnExchangeApproved()
		{
			Window w = Window.Get;
			w.Players_btnExchange.InvokeIfRequired(() => {
				w.Players_btnExchange.Enabled = false;
			});
		}

		internal static void OnExchangeCanceled()
		{
			if (inExchange)
			{
				// Exchanger
				if (ExchangerPlayer.InventoryExchange != null)
					ExchangerPlayer.InventoryExchange.Clear();
				// Me
				if (Character.InventoryExchange != null)
					Character.InventoryExchange.Clear();
				// Update GUI
				Window w = Window.Get;
				w.Players_lblExchangeStatus.InvokeIfRequired(() => {
					w.Players_lblExchangeStatus.Text = "Exchange canceled!";
				});
				w.Players_ClearExchange();
				// Exchange finished
				ExchangerPlayer = null;
				isExchangerConfirmed = false;
				w.Log("Exchange canceled!");
			}
		}
		internal static void OnPartyMatchListing(byte pageIndex, byte pageCount, xDictionary<uint, SRPartyMatch> partyMatches)
		{
			// GUI
			Window w = Window.Get;
			// Set page changer
			w.Party_lblPageNumber.InvokeIfRequired(() => {
				w.Party_lblPageNumber.Text = (pageIndex + 1).ToString();
			});
			w.Party_btnLastPage.InvokeIfRequired(() => {
				w.Party_btnLastPage.Enabled = (pageIndex != 0);
			});
			w.Party_btnNextPage.InvokeIfRequired(() => {
				w.Party_btnNextPage.Enabled = (pageCount != pageIndex + 1);
			});
			// Update listview
			w.Party_lstvPartyMatch.InvokeIfRequired(() => {
				w.Party_lstvPartyMatch.Items.Clear();
			});

			SRPartyMatch myMatch = null;
			if (partyMatches.Count > 0)
			{
				w.Party_lstvPartyMatch.InvokeIfRequired(() => {
					w.Party_lstvPartyMatch.BeginUpdate();
				});
				for(byte j = 0; j < partyMatches.Count; j++)
				{
					SRPartyMatch match = partyMatches.GetAt(j);

					ListViewItem Item = new ListViewItem();
					Item.Text = Item.Name = match.Number.ToString();
					Item.SubItems.Add((match.isJobMode ? "*" : "") + match.MasterName);
					Item.SubItems.Add(match.Title);
					Item.SubItems.Add(match.LevelMin + "~" + match.LevelMax);
					Item.SubItems.Add(match.MemberCount + "/" + match.MemberMax);
					Item.SubItems.Add(match.Purpose.ToString());
					Item.ToolTipText = match.GetTooltip();
					if ( (inParty && match.MasterName == Party.Master.Name) || match.MasterName == CharName)
					{
						myMatch = match;
						Item.BackColor = w.ColorItemHighlight;
					}
					w.Party_lstvPartyMatch.InvokeIfRequired(() => {
						w.Party_lstvPartyMatch.Items.Add(Item);
					});
				}
				w.Party_lstvPartyMatch.InvokeIfRequired(() => {
					w.Party_lstvPartyMatch.EndUpdate();
				});
			}
			
			Bot.Get.OnPartyMatchListing(myMatch);
		}

		internal static void OnCharacterStatPointAdded(bool success)
		{
			Window w = Window.Get;
			if (success)
			{
				if (Character.StatPoints > 0)
				{
					Character.StatPoints -= 1;
					w.Character_lblStatPoints.InvokeIfRequired(() => {
						w.Character_lblStatPoints.Text = Character.StatPoints.ToString();
					});
					if(Character.StatPoints == 0)
					{
						w.Character_gbxStatPoints.InvokeIfRequired(() => {
							w.Character_btnAddINT.Enabled = w.Character_btnAddSTR.Enabled = false;
						});
					}
				}
			}
			else
			{
				w.Character_gbxStatPoints.InvokeIfRequired(() => {
					w.Character_btnAddINT.Enabled = w.Character_btnAddSTR.Enabled = false;
				});
			}
		}
		internal static void OnInventoryMovement(byte slotInitial, byte slotFinal)
		{
			// Check if is a weapon movement
			if (slotInitial == 6 || slotFinal == 6)
				m_MonitorWeaponChanged.Set();
			m_MonitorInventoryMovement.Set();
		}

		internal static void OnInventoryToExchange(byte slotInventory)
		{
			if (Character.InventoryExchange == null)
				Character.InventoryExchange = new xList<SRItemExchange>();

			SRItemExchange itemEx = new SRItemExchange();
			itemEx.Item = Character.Inventory[slotInventory];
			itemEx.SlotInventory = slotInventory;
			Character.InventoryExchange.Add(itemEx);

			// Lock GUI
			Window w = Window.Get;
			w.Players_lstvInventoryExchange.InvokeIfRequired(() => {
				ListViewItem lstvItem = w.Players_lstvInventoryExchange.Items[slotInventory.ToString()];
				lstvItem.Tag = lstvItem.BackColor;
				lstvItem.BackColor = w.ColorItemHighlight;
			});
			// Create and add item
			SRItem item = itemEx.Item;
			ListViewItem listViewItem = new ListViewItem(item.GetFullName() + item.GetQuantityText());
			listViewItem.Tag = itemEx;
			listViewItem.ImageKey = w.GetImageKeyIcon(item.Icon);
			listViewItem.ToolTipText = item.GetTooltip();
			w.Players_lstvExchangingItems.InvokeIfRequired(() => {
				w.Players_lstvExchangingItems.Items.Add(listViewItem);
			});
		}

		internal static void OnExchangeToInventory(byte slotInventoryExchange)
		{
			SRItemExchange item = Character.InventoryExchange[slotInventoryExchange];
			Character.InventoryExchange.RemoveAt(slotInventoryExchange);

			// Unlock GUI
			Window w = Window.Get;
			w.Players_lstvInventoryExchange.InvokeIfRequired(() => {
				ListViewItem lstvItem = w.Players_lstvInventoryExchange.Items[item.SlotInventory.ToString()];
				lstvItem.BackColor = (System.Drawing.Color)lstvItem.Tag;
				lstvItem.Tag = null;
			});
			// Remove item
			w.Players_lstvExchangingItems.InvokeIfRequired(() => {
				w.Players_lstvExchangingItems.Items.RemoveAt(slotInventoryExchange);
			});
		}
		internal static void OnPetSummoned(SRCoService cos)
		{
			m_PetsOwned[cos.UniqueID] = cos;

			Bot.Get.OnPetSummoned(cos);
		}
		internal static void OnPetUnsummoned(SRCoService cos)
		{
			m_PetsOwned.RemoveKey(cos.UniqueID);

			Bot.Get.OnPetUnsummoned(cos);
		}
		internal static void OnStallOpened(SRPlayer Staller = null)
		{
			inStall = true;
			StallerPlayer = Staller;

			// GUI
			Window w = Window.Get;

			// Keep the inventory to draw on GUI
			xList<SRItemStall> inventoryStall;

			// Check if is my stall
			if (StallerPlayer == null)
			{
				// Create Stall (10 empty slots)
				Character.Stall.Inventory = new xList<SRItemStall>(10);

				// Initialize buttons and stuffs
				m_stallViewsCount = 0;
				m_stallEarnings = 0;

				w.Stall_btnIGCreateModify.InvokeIfRequired(() => {
					w.Stall_btnIGCreateModify.Text = "Open";
				});
				w.Stall_lblState.InvokeIfRequired(() => {
					w.Stall_lblState.Text = "Modifying...";
				});
				w.InvokeIfRequired(() => {
					w.ToolTips.SetToolTip(w.Stall_lblState, "Earnings : 0");
				});
				w.Stall_tbxTitle.InvokeIfRequired(() => {
					w.Stall_tbxTitle.Text = Character.Stall.Title;
					w.Stall_tbxTitle.ReadOnly = false;
				});
				w.Stall_btnTitleEdit.InvokeIfRequired(() => {
					w.Stall_btnTitleEdit.Enabled = true;
				});
				w.Stall_tbxNote.InvokeIfRequired(() => {
					w.Stall_tbxNote.ReadOnly = false;
					w.Stall_tbxStallNote.InvokeIfRequired(() => {
						w.Stall_tbxNote.Text = w.Stall_tbxStallNote.Text;
					});
				});
				w.Stall_btnNoteEdit.InvokeIfRequired(() => {
					w.Stall_btnNoteEdit.Enabled = true;
				});
				w.Stall_tbxPrice.InvokeIfRequired(() => {
					w.Stall_tbxPrice.Text = "";
					w.Stall_tbxPrice.ReadOnly = false;
				});
				w.Stall_btnAddItem.InvokeIfRequired(() => {
					w.Stall_btnAddItem.Enabled = true;
				});
				// Create my inventory for selling
				w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
					w.Stall_lstvInventoryStall.BeginUpdate();
				});
				for (byte j = 13; j < Character.Inventory.Capacity; j++)
				{
					SRItem item = Character.Inventory[j];
					if (item != null)
					{
						ListViewItem listViewItem = new ListViewItem();
						listViewItem.Name = j.ToString();
						listViewItem.Tag = item;
						listViewItem.Text = item.Name + item.GetQuantityText();
						listViewItem.ToolTipText = item.GetTooltip();
						listViewItem.ImageKey = w.GetImageKeyIcon(item.Icon);
						w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
							w.Stall_lstvInventoryStall.Items.Add(listViewItem);
						});
					}
				}
				w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
					w.Stall_lstvInventoryStall.EndUpdate();
				});
				// Add Editing options
				w.Stall_lstvStall.InvokeIfRequired(() => {
					w.Stall_lstvStall.ContextMenuStrip = w.Menu_lstvStall_Selling;
				});
				inventoryStall = Character.Stall.Inventory;
				w.Log("Stall created");
			}
			else
			{
				w.Stall_tbxTitle.InvokeIfRequired(() => {
					w.Stall_tbxTitle.Text = Staller.Stall.Title;
				});
				// Add Buy options
				w.Stall_lstvStall.InvokeIfRequired(() => {
					w.Stall_lstvStall.ContextMenuStrip = w.Menu_lstvStall_Buying;
				});
				inventoryStall = Staller.Stall.Inventory;
			}

			w.Stall_btnClose.InvokeIfRequired(() => {
				w.Stall_btnClose.Enabled = true;
			});
			// Create stall
			w.Stall_Create(inventoryStall);
		}

		internal static void OnStallClosed()
		{
			inStall = false;

			// Clear GUI & stuffs
			Window w = Window.Get;
			if (StallerPlayer == null)
			{
				Character.Stall.Inventory.Clear();
				w.Log("Stall closed");
			}
			else
			{
				StallerPlayer = null;
			}
			w.Stall_Clear();
		}

		internal static void OnStallStateUpdate(bool isOpen)
		{
			// GUI
			Window w = Window.Get;
			if (StallerPlayer == null)
			{
				// My stall
				if (isOpen)
				{
					w.Stall_btnIGCreateModify.InvokeIfRequired(() => {
						w.Stall_btnIGCreateModify.Text = "Modify";
					});
					w.Stall_lblState.InvokeIfRequired(() => {
						w.Stall_lblState.Text = "Player Views : " + m_stallViewsCount;
					});
					w.Log("Stall opened");
				}
				else
				{
					w.Stall_btnIGCreateModify.InvokeIfRequired(() => {
						w.Stall_btnIGCreateModify.Text = "Open";
					});
					w.Log("Stall editing");
				}
				w.Stall_tbxTitle.InvokeIfRequired(() => {
					w.Stall_tbxTitle.ReadOnly = isOpen;
				});
				w.Stall_btnTitleEdit.InvokeIfRequired(() => {
					w.Stall_btnTitleEdit.Enabled = !isOpen;
				});
				w.Stall_btnAddItem.InvokeIfRequired(() => {
					w.Stall_btnAddItem.Enabled = !isOpen;
				});
			}
			else
			{
				// Other stall
				if (isOpen)
				{
					w.Stall_lblState.InvokeIfRequired(() => {
						w.Stall_lblState.Text = "Operating...";
					});
				}
			}
			if (!isOpen)
			{
				w.Stall_lblState.InvokeIfRequired(() => {
					w.Stall_lblState.Text = "Modifying...";
				});
			}
		}
		internal static void OnStallNoteUpdate(string note)
		{
			Window w = Window.Get;
			if (StallerPlayer == null)
			{
				Character.Stall.Note = note;
				w.Log("Stall note updated");
			}
			else
			{
				StallerPlayer.Stall.Note = note;
			}
			w.Stall_tbxNote.InvokeIfRequired(() => {
				w.Stall_tbxNote.Text = note;
			});
		}
		internal static void OnStallNoteUpdate()
		{
			Window w = Window.Get;
			string title = (StallerPlayer == null ? Character : StallerPlayer).Stall.Title;
			w.Stall_tbxTitle.InvokeIfRequired(() => {
				w.Stall_tbxTitle.Text = title;
			});
			if (StallerPlayer == null)
				w.Log("Stall title updated");
		}
		internal static void OnStallViewer(bool enter)
		{
			if (StallerPlayer == null)
			{
				if (enter)
				{
					m_stallViewsCount++;

					Window w = Window.Get;
					if (!w.Stall_btnTitleEdit.Enabled) {
						// Show if it's open
						w.Stall_lblState.InvokeIfRequired(() => {
							w.Stall_lblState.Text = "Player Views : " + m_stallViewsCount;
						});
					}
				}
			}
		}
		internal static void OnStallBuy(byte slotStall, string playerName)
		{
			// Remove item from view
			Window w = Window.Get;
			
			if (StallerPlayer == null)
			{
				SRItemStall item = Character.Stall.Inventory[slotStall];
				// Remove it from inventory
				Character.Inventory[item.SlotInventory] = null;

				m_stallEarnings += item.Price;

				// GUI
				w.Stall_lstvInventoryStall.Items.RemoveByKey(item.SlotInventory.ToString());

				w.InvokeIfRequired(() => {
					w.ToolTips.SetToolTip(w.Stall_lblState, "Earnings : " + m_stallEarnings.ToString("#,0"));
				});

				// Notify selling
				w.LogChatMessage(w.Chat_rtbxStall, "(Notice)", "[" + playerName + "] bought you [" + item.Item.GetFullName() + item.Item.GetQuantityText() + "]");
				w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option07);
			}
			else
			{
				SRItemStall item = StallerPlayer.Stall.Inventory[slotStall];
				if (playerName == CharName)
				{
					// Add to my inventory
					byte emptySlot = (byte)Character.Inventory.FindIndex(i => i == null, 13);
					Character.Inventory[emptySlot] = item.Item;

					// GUI
					// Add to preview inventory 
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Tag = item;
					listViewItem.Text = item.Item.Name + item.Item.GetQuantityText();
					listViewItem.ToolTipText = item.Item.GetTooltip();
					listViewItem.ImageKey = w.GetImageKeyIcon(item.Item.Icon);
					w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
						w.Stall_lstvInventoryStall.Items.Add(listViewItem);
					});

					// Notify my buy
					w.LogChatMessage(w.Chat_rtbxStall, "(Notice)", "You bought [" + item.Item.GetFullName() + item.Item.GetQuantityText() + "]");
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option07);
				}
				else
				{
					// Notify player buy
					w.LogChatMessage(w.Chat_rtbxStall, "(Notice)", "[" + playerName + "] bought [" + item.Item.GetFullName() + item.Item.GetQuantityText() + "]");
					w.TabPageH_ChatOption_Notify(w.TabPageH_Chat_Option07);
				}
			}
		}
		internal static void OnStallUpdate(xList<SRItemStall> inventoryStall)
		{
			// Update stall
			if (StallerPlayer == null)
				Character.Stall.Inventory = inventoryStall;
			else
				StallerPlayer.Stall.Inventory = inventoryStall;

			// GUI
			Window w = Window.Get;
			if (StallerPlayer == null)
			{
				// Update my inventory to sell
				w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
					w.Stall_lstvInventoryStall.BeginUpdate();
				});
				// enable items
				System.Drawing.Color disabledItem = w.ColorItemHighlight;
				for (byte j = 0; j < w.Stall_lstvInventoryStall.Items.Count; j++)
				{
					if (w.Stall_lstvInventoryStall.Items[j].BackColor == disabledItem)
					{
						w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
							w.Stall_lstvInventoryStall.Items[j].BackColor = w.Stall_lstvInventoryStall.BackColor;
						});
					}
				}
				// disable items
				for (byte j = 0; j < inventoryStall.Capacity; j++)
				{
					if (inventoryStall[j] != null)
					{
						w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
							w.Stall_lstvInventoryStall.Items[inventoryStall[j].SlotInventory.ToString()].BackColor = disabledItem;
						});
					}
				}
				w.Stall_lstvInventoryStall.InvokeIfRequired(() => {
					w.Stall_lstvInventoryStall.EndUpdate();
					w.Stall_lstvInventoryStall.Invalidate();
				});
			}
			// stall
			w.Stall_Create(inventoryStall);
		}
		internal static void OnStallItemUpdate(byte slotStall, ushort quantity, ulong price)
		{
			SRItemStall item = (StallerPlayer == null ? Character : StallerPlayer).Stall.Inventory[slotStall];
			item.Item.Quantity = quantity;
			item.Price = price;

			// Visual change
			Window w = Window.Get;
			w.Stall_lstvStall.InvokeIfRequired(() => {
				w.Stall_lstvStall.Items[slotStall].SubItems[1].Text = quantity.ToString();
				w.Stall_lstvStall.Items[slotStall].SubItems[2].Text = price.ToString("#,0");
				w.Stall_lstvStall.Items[slotStall].SubItems[2].ForeColor = w.GetGoldColor(price);
			});
		}
		internal static void OnEntitySkillCast(SRTypes.SkillCast type, uint skillID, uint sourceUniqueID, uint targetUniqueID)
		{
			SRModel entity = (SRModel)GetEntity(sourceUniqueID);
			entity.GetRealtimePosition(); // Force update the position

			// Check if it's me
			if (sourceUniqueID == Character.UniqueID)
			{
				// Put skill at cooldown
				SRSkill skill = Character.Skills[skillID];
				// Avoid basic attacks
				if (skill != null)
					skill.StartCooldown();
				m_MonitorSkillCast.Set();
			}
		}
		internal static void OnEntityBuffAdded(uint uniqueID, SRBuff buff)
		{
			// Ignore flashy buffs
			if (buff.DurationMax > 0)
			{
				buff.TargetUniqueID = uniqueID;
				m_Buffs[buff.UniqueID] = buff;

				// Add and override entity buff
				SRModel entity = (SRModel)GetEntity(uniqueID);

				// Remove last buff
				SRBuff lastBuff = entity.Buffs[buff.GroupID];
				if (lastBuff != null)
					m_Buffs.RemoveKey(lastBuff.UniqueID);
				entity.Buffs[buff.GroupID] = buff;
				// Check my own
				if (entity == Character)
				{
					// GUI
					Window w = Window.Get;
					if (lastBuff != null)
						w.RemoveBuff(lastBuff.UniqueID);
					w.AddBuff(buff);
				}
			}
		}

		internal static void OnEntityBuffRemoved(uint buffUniqueID)
		{
			SRBuff buff = m_Buffs[buffUniqueID];
			// Check just in case
			if (buff != null)
			{
				m_Buffs.RemoveKey(buffUniqueID);

				// remove entity buff
				SRModel entity = (SRModel)GetEntity(buff.TargetUniqueID);
				entity.Buffs.RemoveKey(buff.GroupID);
				// Check my own
				if (Character == entity)
				{
					MonitorBuffRemoved.Set();
					Window.Get.RemoveBuff(buffUniqueID);
				}
			}
		}
		internal static void OnEntityDead(uint uniqueID)
		{
			// Entity target has been killed through skillshot
			SRModel entity = (SRModel)GetEntity(uniqueID);
			// Check if the entity has been removed before
			if (entity != null)
			{
				// Update dead state (fastest detection)
				entity.LifeStateType = SRModel.LifeState.Dead;
				if (entity.isNPC() && ((SRNpc)entity).isMob())
				{
					m_MonitorMobSpawnChanged.Set();

					m_Mobs.RemoveKey(uniqueID);
					// GUI
					Window.Get.Minimap_Object_Remove(uniqueID);
				}
			}
		}
		internal static void OnSkillLevelUp(uint newSkillID)
		{
			SRSkill skill = new SRSkill(newSkillID);
			
			// Look for the skill with the last group ID
			uint lastSkillID = DataManager.GetLastSkillID(skill);
			if (lastSkillID == 0)
			{
				// Add new skill
				Character.Skills[skill.ID] = skill;
				Window.Get.AddSkill(skill);
			}
			else
			{
				// Update if the skill is sharing the same group
				Character.Skills.RemoveKey(lastSkillID);
				Character.Skills[skill.ID] = skill;
				// Update the skill from every list
				Window.Get.UpdateSkill(lastSkillID, skill);
			}
		}

		internal static void OnSkillLevelDown(uint newSkillID)
		{
			SRSkill skill = new SRSkill(newSkillID);

			// Look for the skill with the next groupname
			uint nextSkillID = DataManager.GetNextSkillID(skill);

			Window w = Window.Get;
			if (nextSkillID != 0) // Just in case
			{
				SRSkill nextSkill = Character.Skills[nextSkillID];
				// Check if doesn't exists, then it's the last skill point to remove
				if (skill == null)
				{
					// Remove skill
					Character.Skills.RemoveKey(skill.ID);
					Window.Get.RemoveSkill(skill.ID);
				}
				else
				{
					// Update if the skill is sharing the same group
					Character.Skills.RemoveKey(nextSkillID);
					Character.Skills[skill.ID] = skill;
					// Update the skill from every list
					Window.Get.UpdateSkill(nextSkillID, skill);
				}
			}
		}
		internal static void OnStorageInfo(xList<SRItem> storage)
		{
			Character.Storage = storage;
			isStorageLoaded = true;

			if (Bot.Get.Proxy.ClientlessMode)
			{
				PacketBuilder.TalkNPC(SelectedEntityUniqueID, 3);
			}
		}
		internal static void OnTalkNpc(byte talkID)
		{
			SREntity entity = m_Entities[SelectedEntityUniqueID];
			switch (talkID)
			{
				case 3:
					{
						if (entity.ServerName.Contains("_WAREHOUSE"))
						{
							inStorage = true;

							Window w = Window.Get;
							w.LogProcess("Storage open!");
							w.ToolTips.SetToolTip(w.Inventory_btnOpenCloseStorage, "Close Storage");
						}
					}
					break;
			}
		}
		internal static void OnTalkClose()
		{
			if (inStorage)
			{
				inStorage = false;

				Window w = Window.Get;
				w.LogProcess("Storage closed");
				w.ToolTips.SetToolTip(w.Inventory_btnOpenCloseStorage, "Open Storage");
			}
    }
		#endregion
	}
}
