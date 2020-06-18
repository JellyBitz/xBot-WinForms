using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using xBot.App;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Guild;
using xBot.Game.Objects.Item;
using xBot.Game.Objects.Party;
using xBot.Network;

namespace xBot.Game
{
	// Class to handle almost everything about parsing packets
	// and filling the GUI with the necessary
	public static class PacketParser
	{
		public static void ShardListResponse(Packet packet)
		{
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Login_lstvServers, () => {
				w.Login_lstvServers.Items.Clear();
				//Window.get.Login_lstvServers.Groups.Clear();
			});
			WinAPI.InvokeIfRequired(w.Login_cmbxServer, () => {
				w.Login_cmbxServer.Items.Clear();
			});
			while (packet.ReadByte() == 1)
			{
				byte farmID = packet.ReadByte();
				string farmName = packet.ReadAscii();
				//WinAPI.InvokeIfRequired(w.Login_lstvServers, () =>{
				//	Window.get.Login_lstvServers.Groups.Add(farmID.ToString(), farmName);
				//});
			}
			InfoManager.ServerID = "";
			while (packet.ReadByte() == 1)
			{
				ushort serverID = packet.ReadUShort();
				string serverName = packet.ReadAscii();
				ushort players = packet.ReadUShort();
				ushort maxPlayers = packet.ReadUShort();
				bool isAvailable = packet.ReadByte() == 1;
				byte farm_ID = packet.ReadByte();

				// Generate server list
				ListViewItem server = new ListViewItem(serverName);
				server.Name = serverID.ToString();
				server.SubItems.Add(players + " / " + maxPlayers + " ("+Math.Round(players*100d/maxPlayers,2)+"%)");
				server.SubItems.Add((isAvailable ? "Online" : "Offline"));
				w.Login_lstvServers.InvokeIfRequired(() => {
					//i.Group = w.Login_lstvServers.Groups[serverID_farmID.ToString()];
					w.Login_lstvServers.Items.Add(server);
				});
				if (isAvailable)
				{
					w.Login_cmbxServer.InvokeIfRequired(() => {
						w.Login_cmbxServer.Items.Add(serverName);
						// Select Server if is AutoLogin
						if (Bot.Get.hasAutoLoginMode
						&& w.Login_cmbxServer.Tag != null
						&& serverName.Equals((string)w.Login_cmbxServer.Tag, StringComparison.OrdinalIgnoreCase))
						{
							w.Login_cmbxServer.SelectedItem = serverName;
						}
					});
				}
			}
			// Unlock button
			if (w.Login_btnStart.Text == "STOP" && Bot.Get.Proxy.ClientlessMode
				|| Bot.Get.hasAutoLoginMode)
			{
				WinAPI.InvokeIfRequired(w.Login_btnStart, () => {
					w.Login_btnStart.Text = "LOGIN";
				});
			}
			w.LogProcess("Server selection");

			// AutoLogin
			if (Bot.Get.hasAutoLoginMode) {
				// Server found
				if (InfoManager.ServerID != "") {
					WinAPI.InvokeIfRequired(w, () => {
						w.Control_Click(w.Login_btnStart, null);
					});
				}
			}
			else
			{
				// Select first one (Just for UX)
				if (w.Login_cmbxServer.Items.Count > 0)
				{
					WinAPI.InvokeIfRequired(w.Login_cmbxServer, () => {
						w.Login_cmbxServer.SelectedIndex = 0;
					});
				}
			}
		}
		public static void CaptchaData(Packet packet)
		{
			
		}
		public static void CharacterSelectionActionResponse(Packet packet)
		{
			byte action = packet.ReadByte();
			byte result = packet.ReadByte();

			switch ((SRTypes.CharacterSelectionAction)action)
			{
				case SRTypes.CharacterSelectionAction.Create:
					if (result == 1)
						Window.Get.Log("Character created successfully");
					else
						Window.Get.Log("Character creation failed!");
					if (Bot.Get.Proxy.ClientlessMode)
						PacketBuilder.RequestCharacterList();
					break;
				case SRTypes.CharacterSelectionAction.CheckName:
					Bot.Get.OnNicknameChecked(result == 1);
					break;
				case SRTypes.CharacterSelectionAction.Delete:
					// Not necessary at the moment..
					break;
				case SRTypes.CharacterSelectionAction.List:
					if (result == 1)
					{
						Window w = Window.Get;
						// Reset values
						w.Login_lstvCharacters.InvokeIfRequired(() =>{
							w.Login_lstvCharacters.Items.Clear();
						});
						w.Login_cmbxCharacter.InvokeIfRequired(() =>{
							w.Login_cmbxCharacter.Items.Clear();
						});
						// Get character selection
						List<SRCharSelection> CharacterList = new List<SRCharSelection>(packet.ReadByte());
						for (byte n = 0; n < CharacterList.Capacity; n++)
						{
							SRCharSelection character = new SRCharSelection();
							character.ModelID = packet.ReadUInt();
							character.Name = packet.ReadAscii();
							character.Scale = packet.ReadByte();
							character.Level = packet.ReadByte();
							character.Exp = packet.ReadULong();
							character.STR = packet.ReadUShort();
							character.INT = packet.ReadUShort();
							character.StatPoints = packet.ReadUShort();
							character.HP = packet.ReadUInt();
							character.MP = packet.ReadUInt();
							bool isDeleting = packet.ReadBool();
							if (isDeleting)
								character.DeletingDate = DateTime.Now.AddMinutes(packet.ReadUInt());
							character.GuildMemberType = (SRCharSelection.GuildMember)packet.ReadByte();
							bool isGuildRenameRequired = packet.ReadBool();
							if (isGuildRenameRequired)
								character.GuildName = packet.ReadAscii();
							character.AcademyMemberType = (SRCharSelection.AcademyMember)packet.ReadByte();
							// inventory
							xList<SRItem> inventory = new xList<SRItem>(packet.ReadByte());
							for (byte j = 0; j < inventory.Capacity; j++)
							{
								inventory[j] = SRItem.Create(packet.ReadUInt(),null);
								byte plus = packet.ReadByte();
								if (inventory[j].isEquipable())
									((SREquipable)inventory[j]).Plus = plus;
							}
							character.Inventory = inventory;
							// inventory avatar
							xList<SREquipable> inventoryAvatar = new xList<SREquipable>(packet.ReadByte());
							for (byte j = 0; j < inventoryAvatar.Capacity; j++)
							{
								inventoryAvatar[j] = (SREquipable)SRItem.Create(packet.ReadUInt(), null);
								inventoryAvatar[j].Plus = packet.ReadByte();
							}
							character.InventoryAvatar = inventoryAvatar;

							// Adding character
							CharacterList.Add(character);
						}
						// End of Packet
						InfoManager.OnCharacterListing(CharacterList);
					}
					else if (result == 2)
					{
						ushort errCode = packet.ReadUShort();
						Window.Get.Log("Error [" + errCode + "]");
						Bot.Get.Proxy.Stop();
					}
					break;
			}
		}
		public static void CharacterSelectionJoinResponse(Packet packet)
		{
			byte success = packet.ReadByte();

			Window w = Window.Get;
			if (success == 1)
			{
				w.Log("Character selected [" + InfoManager.CharName + "]");
			}
			else
			{
				ushort errCode = packet.ReadUShort();
				w.Log("Error: " + errCode);
			}
		}
		private static Packet characterDataPacket;
		public static void CharacterDataBegin(Packet packet)
		{
			characterDataPacket = new Packet(Agent.Opcode.SERVER_CHARACTER_DATA);
			//InfoManager.OnTeleporting();
		}
		public static void CharacterData(Packet packet)
		{
			characterDataPacket.WriteByteArray(packet.GetBytes());
		}
		public static void CharacterDataEnd()
		{
			Packet p = characterDataPacket;
			p.Lock();

			InfoManager.SetServerTime(new SRTimeStamp(p.ReadUInt()));
			SRCharacter character = new SRCharacter(p.ReadUInt());
			character.Scale = p.ReadByte();
			character.Level = p.ReadByte();
			character.LevelMax = p.ReadByte();
			character.Exp = p.ReadULong();
			character.SPExp = p.ReadUInt();
			character.Gold = p.ReadULong();
			character.SP = p.ReadUInt();
			character.StatPoints = p.ReadUShort();
			character.BerserkPoints = p.ReadByte();
			character.GatheredExpPoint = p.ReadUInt();
			character.HPMax = p.ReadUInt();
			character.MPMax = p.ReadUInt();
			character.ExpIconType = (SRPlayer.ExpIcon)p.ReadByte();
			character.PKDaily = p.ReadByte();
			character.PKTotal = p.ReadUShort();
			character.PKPenalty = p.ReadUInt();
			character.BerserkLevel = p.ReadByte();
			character.PVPCapeType = (SRPlayer.PVPCape)p.ReadByte();
			// Inventory
			xList<SRItem> inventory = new xList<SRItem>(p.ReadByte());
			byte itemCount = p.ReadByte();
			for (byte j = 0; j < itemCount; j++)
			{
				byte slot = p.ReadByte();
				inventory[slot] = ItemParsing(p);
			}
			character.Inventory = inventory;
			// Inventory Avatar
			inventory = new xList<SRItem>(p.ReadByte());
			itemCount = p.ReadByte();
			for (byte j = 0; j < itemCount; j++)
			{
				byte slot = p.ReadByte();
				inventory[slot] = ItemParsing(p);
			}
			character.InventoryAvatar = inventory;
			// Masteries
			character.unkByte01 = p.ReadByte();
			xDictionary<uint,SRMastery> masteries = new xDictionary<uint,SRMastery>();
			while (p.ReadBool())
			{
				SRMastery mastery = new SRMastery(p.ReadUInt());
				mastery.Level = p.ReadByte();
				masteries[mastery.ID] = mastery;
			}
			character.Masteries = masteries;
			// Skills
			character.unkByte02 = p.ReadByte();
			xDictionary<uint, SRSkill> skills = new xDictionary<uint, SRSkill>();
			while (p.ReadBool())
			{
				SRSkill skill = new SRSkill(p.ReadUInt());
				skill.Enabled = p.ReadBool();
				skills[skill.ID] = skill;
			}
			character.Skills = skills;
			// Quests
			xDictionary<uint, SRQuest> quests = new xDictionary<uint, SRQuest>();
			ushort questsCompletedCount = p.ReadUShort();
			for (ushort j = 0; j < questsCompletedCount; j++)
			{
				SRQuest quest = new SRQuest(p.ReadUInt());
				quests[quest.ID] = quest;
			}
			character.QuestsCompleted = quests;

			quests = new xDictionary<uint, SRQuest>();
			byte questCount = p.ReadByte();
			for (byte j = 0; j < questCount; j++)
			{
				SRQuest quest = new SRQuest(p.ReadUInt());
				quest.Achievements = p.ReadByte();
				quest.isAutoShareRequired = p.ReadBool();
				quest.QuestType = p.ReadByte();
				if (quest.QuestType == 28)
					quest.TimeRemain = p.ReadUInt();
				quest.State = p.ReadByte();
				if (quest.QuestType != 8)
				{
					xList<SRQuestObjective> objectives = new xList<SRQuestObjective>(p.ReadByte());
					for (byte k = 0; k < objectives.Capacity; k++)
					{
						SRQuestObjective objective = new SRQuestObjective(p.ReadByte());
						objective.isEnabled = p.ReadBool();
						objective.Name = p.ReadAscii();
						objective.TasksID = p.ReadUIntArray(p.ReadByte());
						objectives[k] = objective;
					}
					quest.Objectives = objectives;
				}
				if (quest.QuestType == 88)
					quest.NpcsID = p.ReadUIntArray(p.ReadByte());
				quests[quest.ID] = quest;
			}
			character.Quests = quests;
			// Collection Books
			character.unkByte03 = p.ReadByte();
			xDictionary<uint, SRCollectionBook> collectionBooks = new xDictionary<uint, SRCollectionBook>();
			uint bookCount = p.ReadUInt();
			for (uint j = 0; j < bookCount; j++)
			{
				SRCollectionBook book = new SRCollectionBook(p.ReadUInt());
				book.StartedDatetime = new SRTimeStamp(p.ReadUInt());
				book.Pages = p.ReadUInt();
				collectionBooks[book.ID] = book;
			}
			character.CollectionBooks = collectionBooks;
			// Position
			character.UniqueID = p.ReadUInt();
			character.Position = new SRCoord(p.ReadUShort(), (int)p.ReadFloat(), (int)p.ReadFloat(), (int)p.ReadFloat());
			character.Angle = p.ReadUShort();
			bool hasMovement = p.ReadBool();
			character.MovementSpeedType = (SRModel.MovementSpeed)p.ReadByte();
			if (hasMovement)
			{
				if (character.Position.inDungeon())
					character.MovementPosition = new SRCoord(p.ReadUShort(), p.ReadInt(), p.ReadInt(), p.ReadInt());
				else
					character.MovementPosition = new SRCoord(p.ReadUShort(),(int)p.ReadUShort(), (int)p.ReadUShort(), (int)p.ReadUShort());
			}
			else
			{
				character.MovementActionType = (SRModel.MovementAction)p.ReadByte();
				character.Angle = p.ReadUShort();
			}
			character.GetRealtimePosition();
			// States
			character.LifeStateType = (SRModel.LifeState)p.ReadByte();
			character.unkByte04 = p.ReadByte();
			character.MotionStateType = (SRModel.MotionState)p.ReadByte();
			character.GameStateType = (SRModel.GameState)p.ReadByte();
			character.SpeedWalking = p.ReadFloat();
			character.SpeedRunning = p.ReadFloat();
			character.SpeedBerserk = p.ReadFloat();
			// Buffs
			xDictionary<uint, SRBuff> buffs = new xDictionary<uint, SRBuff>();
			byte buffCount = p.ReadByte();
			for (byte j = 0; j < buffCount; j++)
			{
				SRBuff buff = new SRBuff(p.ReadUInt());
				buff.UniqueID = p.ReadUInt();
				if (buff.hasAutoTransferEffect())
				{
					bool isCaster = p.ReadBool();
					if (isCaster)
						buff.CasterUniqueID = character.UniqueID;
				}
				buff.TargetUniqueID = character.UniqueID;
				// Easy track using GroupID to avoid overlap issues
				buffs[buff.GroupID] = buff;
			}
			character.Buffs = buffs;
			// Identification
			character.Name = p.ReadAscii();
			character.JobName = p.ReadAscii();
			character.JobType = (SRPlayer.Job)p.ReadByte();
			character.JobLevel = p.ReadByte();
			character.JobExp = p.ReadUInt();
			character.JobContribution = p.ReadUInt();
			character.JobReward = p.ReadUInt();
			character.PVPStateType = (SRPlayer.PVPState)p.ReadByte();
			bool isRiding = p.ReadBool();
			character.inCombat = p.ReadBool();
			if (isRiding)
				character.RidingUniqueID = p.ReadUInt();
			character.CaptureTheFlagType = (SRPlayer.CaptureTheFlag)p.ReadByte();
			character.GuideFlag =  p.ReadULong();
			character.JoinID = p.ReadUInt();
			character.isGameMaster = p.ReadBool();

			#region (Game Settings from GUI)
			//byte activationFlag = packet.ReadByte(); // ConfigType:0 --> (0 = Not activated, 7 = activated)
			//byte hotkeysCount = packet.ReadByte(); // ConfigType:1
			//for (int i = 0; i < hotkeysCount; i++)
			//{
			//	byte HotkeySlotSeq = packet.ReadByte();
			//	byte HotkeySlotContentType = packet.ReadByte();
			//	uint HotkeySlotData = packet.ReadUInt();
			//}
			//ushort AutoHPConfig = packet.ReadUShort(); // ConfigType:11
			//ushort AutoMPConfig = packet.ReadUShort(); // ConfigType:12
			//ushort AutoUniversalConfig = packet.ReadUShort(); // ConfigType:13
			//ushort AutoPotionDelay = packet.ReadByte(); // ConfigType:14

			//byte BlockedWhisperCount = packet.ReadByte();
			//for (int i = 0; i < BlockedWhisperCount; i++)
			//{
			//	string Target = packet.ReadAscii();
			//}
			//uint unkUShort0 = packet.ReadUInt(); // Structure changes!!!
			//byte unkByte4 = packet.ReadByte(); // Structure changes!!!
			#endregion
			// End of Packet
			InfoManager.OnCharacterInfo(character);
		}
		private static SRItem ItemParsing(Packet p)
		{
			SRRentable rentable = new SRRentable(p.ReadUInt());
			if(rentable.RentableType != SRRentable.Type.None)
			{
				if (rentable.RentableType == SRRentable.Type.LimitedTime)
				{
					rentable.CanDelete = p.ReadUShort();
					rentable.PeriodBeginTime = p.ReadUInt();
					rentable.PeriodEndTime = p.ReadUInt();
				}
				else if (rentable.RentableType == SRRentable.Type.LimitedDistance)
				{
					rentable.CanDelete = p.ReadUShort();
					rentable.CanRecharge = p.ReadUShort();
					rentable.MeterRateTime = p.ReadUInt();
				}
				else if (rentable.RentableType == SRRentable.Type.Package)
				{
					rentable.CanDelete = p.ReadUShort();
					rentable.CanRecharge = p.ReadUShort();
					rentable.PeriodBeginTime = p.ReadUInt();
					rentable.PeriodEndTime = p.ReadUInt();
					rentable.PackingTime = p.ReadUInt();
				}
			}
			SRItem item = SRItem.Create(p.ReadUInt(), rentable);
			if (item.isEquipable())
			{
				SREquipable equipable = (SREquipable)item;
				equipable.Plus = p.ReadByte();
				equipable.Variance = p.ReadULong();
				equipable.Durability = p.ReadUInt();
				// Magic options
				xList<SRMagicOption> magicOptions = new xList<SRMagicOption>(p.ReadByte());
				for (byte j = 0; j < magicOptions.Capacity; j++)
				{
					magicOptions[j] = new SRMagicOption(p.ReadUInt());
					magicOptions[j].Value = p.ReadUInt();
				}
				equipable.MagicOptions = magicOptions;
				// 1 = Socket
				p.ReadByte();
				xList<SRSocket> sockets = new xList<SRSocket>(p.ReadByte());
				for (byte j = 0; j < sockets.Capacity; j++)
				{
					sockets[j] = new SRSocket(p.ReadByte(),p.ReadUInt());
					sockets[j].Value = p.ReadUInt();
				}
				equipable.Sockets = sockets;
				// 2 = Advanced elixir
				p.ReadByte();
				xList<SRAdvancedElixir> advancedElixirs = new xList<SRAdvancedElixir>(p.ReadByte());
				for (byte j = 0; j < advancedElixirs.Capacity; j++)
				{
					advancedElixirs[j] = new SRAdvancedElixir(p.ReadByte(), p.ReadUInt());
					advancedElixirs[j].Value = p.ReadUInt();
				}
				equipable.AdvancedElixirs = advancedElixirs;
			}
			else if (item.isCoS())
			{
				SRCoS cos = (SRCoS)item;
				if (cos.isPet())
				{
					cos.StateType = (SRCoS.State)p.ReadByte();
					if (cos.StateType != SRCoS.State.NeverSummoned)
					{
						cos.ModelID = p.ReadUInt();
						cos.ModelName = p.ReadAscii();
						if (cos.ID4 == 2)
							cos.Rentable.PeriodEndTime = p.ReadUInt();
						cos.unkByte01 = p.ReadByte();
					}
				}
				else if (cos.isTransform())
				{
					cos.ModelID = p.ReadUInt();
				}
				else if (cos.isCube())
				{
					cos.Quantity = (ushort)p.ReadUInt();
				}
			}
			else if (item.isEtc())
			{
				SREtc etc = (SREtc)item;
				etc.Quantity = p.ReadUShort();
				if (etc.isAlchemy())
				{
					if (item.ID4 == 1 || item.ID4 == 2)
					{
						// MAGIC/ATRIBUTTE STONE
						etc.AssimilationProbability = p.ReadByte();
					}
				}
				else if (item.ID3 == 14 && item.ID4 == 2)
				{
					// ITEM_MALL_GACHA_CARD_WIN
					// ITEM_MALL_GACHA_CARD_LOSE
					byte paramCount = p.ReadByte();
					for (byte j = 0; j < paramCount; j++)
					{
						uint magicParamID = p.ReadUInt();
						uint value = p.ReadUInt();
					}
				}
			}
			return item;
		}
		public static void CharacterStatsUpdate(Packet packet)
		{
			SRCharacter character = InfoManager.Character;
			character.PhyAtkMin = packet.ReadUInt();
			character.PhyAtkMax = packet.ReadUInt();
			character.MagAtkMin = packet.ReadUInt();
			character.MagAtkMax = packet.ReadUInt();
			character.PhyDefense = packet.ReadUShort();
			character.MagDefense = packet.ReadUShort();
			character.HitRate = packet.ReadUShort();
			character.ParryRatio = packet.ReadUShort();
			character.HPMax = packet.ReadUInt();
			character.MPMax = packet.ReadUInt();
			character.STR = packet.ReadUShort();
			character.INT = packet.ReadUShort();
			// End of Packet
			InfoManager.OnCharacterStatsUpdated();
		}
		public static void CharacterExperienceUpdate(Packet packet)
		{
			Window w = Window.Get;

			uint sourceUniqueID = packet.ReadUInt(); // used to display exp. graphics
			long ExpReceived = packet.ReadLong();
			long SPExpReceived = packet.ReadLong(); // Long SP EXP? hmmm..
			// byte unkByte01 = packet.ReadByte();
			// End of Packet

			if (w.Character_cbxMessageExp.Checked)
			{
				if (ExpReceived > 0)
					w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_STATE_GAIN_EXP", ExpReceived));
				else if (ExpReceived < 0)
					w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_STATE_LOSE_EXP", ExpReceived));
				if (SPExpReceived > 0)
					w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_STATE_GET_SKILL_EXP", SPExpReceived));
			}

			InfoManager.OnExpReceived(ExpReceived,(long)InfoManager.Character.Exp, (long)InfoManager.Character.ExpMax, InfoManager.Character.Level);
		}
		public static void CharacterInfoUpdate(Packet packet)
		{
			Window w = Window.Get;
			byte updateType = packet.ReadByte();
			switch (updateType)
			{
				case 1: // Gold
					InfoManager.Character.Gold = packet.ReadULong();
					w.Character_SetGold(InfoManager.Character.Gold);
					break;
				case 2: // SP
					InfoManager.Character.SP = packet.ReadUInt();
					w.Character_lblSP.InvokeIfRequired(() => {
						w.Character_lblSP.Text = InfoManager.Character.SP.ToString();
					});
					break;
				case 4: // Berserk
					InfoManager.Character.BerserkPoints = packet.ReadByte();
					break;
			}
		}
		public static void CharacterDied(Packet packet)
		{
			// Probably dead cause? 4 = Dead by mob?
			byte unkByte01 = packet.ReadByte();
			InfoManager.OnCharacterDead(unkByte01);
		}
		private static byte GroupSpawnType;
		private static ushort GroupSpawnCount;
		private static Packet GroupSpawnPacket;
		public static void EntityGroupSpawnBegin(Packet packet)
		{
			GroupSpawnType = packet.ReadByte();
			GroupSpawnCount = packet.ReadUShort();
			GroupSpawnPacket = new Packet(Agent.Opcode.SERVER_ENTITY_GROUPSPAWN_DATA);
		}
		public static void EntityGroupSpawnData(Packet packet)
		{
			GroupSpawnPacket.WriteByteArray(packet.GetBytes());
		}
		public static void EntityGroupSpawnEnd(Packet packet)
		{
			GroupSpawnPacket.Lock();
			for (int i = 0; i < GroupSpawnCount; i++)
			{
				if (GroupSpawnType == 1)
				{
					EntitySpawn(GroupSpawnPacket);
				}
				else
				{
					EntityDespawn(GroupSpawnPacket);
				}
			}
		}
		public static void EntitySpawn(Packet packet)
		{
			try
			{
				SREntity entity = SREntity.Create(packet.ReadUInt());
				if (!entity.isSkillZone())
				{
					if (entity.isModel())
					{
						SRModel model = (SRModel)entity;
						if (model.isPlayer())
						{
							SRPlayer player = (SRPlayer)entity;

							player.Scale = packet.ReadByte();
							player.BerserkLevel = packet.ReadByte();
							player.PVPCapeType = (SRPlayer.PVPCape)packet.ReadByte();
							player.ExpIconType = (SRPlayer.ExpIcon)packet.ReadByte();
							// Inventory
							packet.ReadByte(); // max capacity. seems useless at the moment..
							xList<SRItem> inventory = new xList<SRItem>(packet.ReadByte());
							for (byte j = 0; j < inventory.Capacity; j++)
							{
								inventory[j] = SRItem.Create(packet.ReadUInt(),null);
								if (inventory[j].isEquipable())
									((SREquipable)inventory[j]).Plus = packet.ReadByte();
							}
							player.Inventory = inventory;
							// AvatarInventory
							packet.ReadByte(); // max capacity. seems useless at the moment..
							inventory = new xList<SRItem>(packet.ReadByte());
							for (byte j = 0; j < inventory.Capacity; j++)
							{
								inventory[j] = SRItem.Create(packet.ReadUInt(), null);
								if (inventory[j].isEquipable())
									((SREquipable)inventory[j]).Plus = packet.ReadByte();
							}
							player.InventoryAvatar = inventory;
							// Mask
							bool hasMask = packet.ReadBool();
							if (hasMask)
							{
								SRMask mask = new SRMask(packet.ReadUInt());
								if (mask.ID1 == player.ID1 && mask.ID2 == player.ID2)
								{
									// Clone
									mask.Scale = packet.ReadByte();
									mask.Inventory = packet.ReadUIntArray(packet.ReadByte());
									//inventory = new xList<SRItem>(packet.ReadByte());
									//for (byte i = 0; i < inventory.Capacity; i++)
									//	inventory[i] = new SRItem(packet.ReadUInt());
									//mask.Inventory = inventory;
								}
								player.Mask = mask;
							}
						}
						else if (model.isNPC() && ((SRNpc)model).isFortressStruct())
						{
							SRFortressStruct fStruct = (SRFortressStruct)entity;
							fStruct.HP = packet.ReadUInt();
							fStruct.refEventStructID = packet.ReadUInt();
							fStruct.State = packet.ReadUShort();
						}
						// Position
						model.UniqueID = packet.ReadUInt();
						model.Position = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
						model.Angle = packet.ReadUShort();
						// Movement
						bool hasMovement = packet.ReadBool();
						model.MovementSpeedType = (SRModel.MovementSpeed)packet.ReadByte();
						if (hasMovement)
						{
							if (model.Position.inDungeon())
								model.MovementPosition = new SRCoord(packet.ReadUShort(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt());
							else
								model.MovementPosition = new SRCoord(packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort());
						}
						else
						{
							model.MovementActionType = (SRModel.MovementAction)packet.ReadByte();
							model.Angle = packet.ReadUShort();
						}
						// States
						model.LifeStateType = (SRModel.LifeState)packet.ReadByte();
						model.unkByte01 = packet.ReadByte();
						model.MotionStateType = (SRModel.MotionState)packet.ReadByte();
						model.GameStateType = (SRModel.GameState)packet.ReadByte();
						// Speed
						model.SpeedWalking = packet.ReadFloat();
						model.SpeedRunning = packet.ReadFloat();
						model.SpeedBerserk = packet.ReadFloat();
						// Buffs
						xDictionary<uint, SRBuff> buffs = new xDictionary<uint, SRBuff>();
						byte buffCount = packet.ReadByte();
						for (byte j = 0; j < buffCount; j++)
						{
							SRBuff buff = new SRBuff(packet.ReadUInt());
							buff.UniqueID = packet.ReadUInt();
							if (buff.hasAutoTransferEffect())
							{
								bool isCaster = packet.ReadBool();
								if (isCaster)
									buff.CasterUniqueID = model.UniqueID;
							}
							buff.TargetUniqueID = model.UniqueID;
							// Easy track using GroupID to avoid overlap issues
							buffs[buff.GroupID] = buff;
						}
						model.Buffs = buffs;
						if (model.isPlayer())
						{
							SRPlayer player = (SRPlayer)model;
							// Actions
							player.Name = packet.ReadAscii();
							player.JobType = (SRPlayer.Job)packet.ReadByte();
							player.JobLevel = packet.ReadByte();
							player.PVPStateType = (SRPlayer.PVPState)packet.ReadByte();
							bool isRiding = packet.ReadBool();
							player.inCombat = packet.ReadBool();
							if (isRiding)
								player.RidingUniqueID = packet.ReadUInt();
							player.ScrollingType = (SRPlayer.Scrolling)packet.ReadByte();
							player.InteractionType = (SRPlayer.Interaction)packet.ReadByte();
							player.unkByte03 = packet.ReadByte();
							// Guild
							player.GuildName = packet.ReadAscii();
							if (player.hasJobMode())
							{
								player.Name = "*" + player.Name;
							}
							else
							{
								SRPlayer.SRPlayerGuildInfo gInfo = new SRPlayer.SRPlayerGuildInfo();
								gInfo.GuildID = packet.ReadUInt();
								gInfo.GuildMemberName = packet.ReadAscii();
								gInfo.GuildLastCrestRev = packet.ReadUInt();
								gInfo.UnionID = packet.ReadUInt();
								gInfo.UnionLastCrestRev = packet.ReadUInt();
								gInfo.isFriendly = packet.ReadBool();
								gInfo.GuildMemberAuthorityType = (SRPlayer.SRPlayerGuildInfo.GuildMemberAuthority)packet.ReadByte();
								player.GuildInfo = gInfo;
							}
							if (player.InteractionType == SRPlayer.Interaction.OnStall)
							{
								SRStall stall = new SRStall();
								stall.Title = packet.ReadAscii();
								stall.DecorationID = packet.ReadUInt();
								player.Stall = stall;
							}
							player.EquipmentCooldown = packet.ReadByte();
							player.CaptureTheFlagType = (SRPlayer.CaptureTheFlag)packet.ReadByte();
						}
						else if (model.isNPC())
						{
							SRNpc npc = (SRNpc)model;
							bool hasTalk = packet.ReadByte() != 0;
							if (hasTalk)
								npc.TalkOptions = packet.ReadByteArray(packet.ReadByte());
							if (npc.isMob())
							{
								SRMob mob = (SRMob)npc;
								mob.MobType = (SRMob.Mob)packet.ReadByte();
								if (mob.ID4 == 2 || mob.ID4 == 3)
									mob.Appearence = packet.ReadByte();
							}
							else if (npc.isCOS())
							{
								SRCoService cos = (SRCoService)npc;
								if (!cos.isHorse())
								{
									if (cos.isAttackPet() || cos.isPickPet())
										cos.Name = packet.ReadAscii();
									cos.OwnerName = packet.ReadAscii();
									cos.JobType = (SRPlayer.Job)packet.ReadByte();
									if (!cos.isPickPet())
									{
										cos.PVPStateType = (SRPlayer.PVPState)packet.ReadByte();
									}
									if (cos.isGuildGuard())
										cos.OwnerObjectID = packet.ReadUInt();
									cos.OwnerUniqueID = packet.ReadUInt();
								}
							}
							else if (npc.isFortressCos())
							{
								SRFortressCos fCos = (SRFortressCos)npc;
								fCos.GuildID = packet.ReadUInt();
								fCos.GuildName = packet.ReadAscii();
							}
						}
						if (packet.Opcode == Agent.Opcode.SERVER_ENTITY_SPAWN)
							model.unkByte02 = packet.ReadByte();
					}
					else if (entity.isDrop())
					{
						SRDrop drop = (SRDrop)entity;

						if (drop.isEquipable())
						{
							drop.Plus = packet.ReadByte();
						}
						else if (drop.isEtc())
						{
							if (drop.isGold())
								drop.Gold = packet.ReadUInt();
							else if (drop.isQuest() || drop.isTradeGoods())
								drop.OwnerName = packet.ReadAscii();
						}
						// Position
						drop.UniqueID = packet.ReadUInt();
						drop.Position = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
						drop.Angle = packet.ReadUShort();
						// States
						bool hasOwner = packet.ReadBool();
						if (hasOwner)
							drop.OwnerJoinID = packet.ReadUInt();
						drop.Rarity = packet.ReadByte();

						if (packet.Opcode == Agent.Opcode.SERVER_ENTITY_SPAWN)
						{
							drop.DropSourceType = packet.ReadByte();
							drop.DropUniqueID = packet.ReadUInt();
						}
					}
					else if (entity.isTeleport())
					{
						SRTeleport portal = (SRTeleport)entity;
						// Position
						portal.UniqueID = packet.ReadUInt();
						portal.Position = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
						portal.Angle = packet.ReadUShort();
						// Types
						portal.unkByte01 = packet.ReadByte();
						portal.unkByte02 = packet.ReadByte();
						portal.unkByte03 = packet.ReadByte();
						portal.PortalType = (SRTeleport.Portal)packet.ReadByte();
						if (portal.PortalType == SRTeleport.Portal.Regular)
						{
							portal.unkUInt01 = packet.ReadUInt();
							portal.unkUInt02 = packet.ReadUInt();
						}
						else if (portal.PortalType == SRTeleport.Portal.Dimensional)
						{
							portal.OwnerName = packet.ReadAscii();
							portal.OwnerUniqueID = packet.ReadUInt();
						}
						if (portal.unkByte02 == 1)
						{
							// STORE_OnONE_DEFAULT
							portal.unkUInt03 = packet.ReadUInt();
							portal.unkByte04 = packet.ReadByte();
						}
					}
				}
				else
				{
					SRSkillZone skillZone = (SRSkillZone)entity;
					skillZone.unkUShort01 = packet.ReadUShort();
					skillZone.SkillID = packet.ReadUInt();
					// Position
					skillZone.UniqueID = packet.ReadUInt();
					skillZone.Position = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
					skillZone.Angle = packet.ReadUShort();
				}
				// Tracking spawns
				InfoManager.OnSpawn(entity);
			}
			catch (Exception ex)
			{
				Bot.Get.LogError("Parsing Spawn Error", ex, packet);
				throw ex;
			}
		}
		public static void EntityDespawn(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			// End of Packet
			InfoManager.OnDespawn(uniqueID);
		}
		public static void EntitySelection(Packet packet)
		{
			// Success
			if (packet.ReadBool())
				InfoManager.OnEntitySelected(packet.ReadUInt());
			else
				InfoManager.OnEntitySelected(0);
		}
		public static void EntityMovement(Packet packet)
		{
			SRModel entity = (SRModel)InfoManager.GetEntity(packet.ReadUInt());
			SRCoord currentPosition = entity.GetRealtimePosition();
			bool hasMovement = packet.ReadBool();
			if (hasMovement)
			{
				SRCoord newPosition;
				if (currentPosition.inDungeon())
					newPosition = new SRCoord(packet.ReadUShort(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt());
				else
					newPosition = new SRCoord(packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort());
				entity.MovementPosition = newPosition;
				// Create an angle pointing to the movement position
				if (entity == InfoManager.Character)
				{
					double xTranslate = newPosition.PosX - currentPosition.PosX;
					double yTranslate = newPosition.PosY - currentPosition.PosY;
					if (xTranslate == 0)
					{
						// 90° or 270° to SRO Angle
						entity.Angle = (ushort)(ushort.MaxValue / 4 * (yTranslate > 0?1:3));
					}
					else
					{
						if (yTranslate == 0)
						{
							// 0° or 180° to SRO Angle
							entity.Angle = (ushort)(xTranslate > 0 ? 0 : ushort.MaxValue / 2);
						}
						else
						{
							double angleRadians = Math.Atan(yTranslate / xTranslate);
							// Fix direction manually
							if(yTranslate < 0 || xTranslate < 0)
							{
								angleRadians += Math.PI;
								if(xTranslate > 0)
									angleRadians += Math.PI;
							}
							// Radians to SRO Angle
							entity.Angle = (ushort)Math.Round(angleRadians * ushort.MaxValue / (Math.PI * 2.0));
						}
					}
				}
			}
			if (packet.ReadBool())
			{
				if (hasMovement)
				{
					// ushort oldRegion = packet.ReadUShort();
					// ushort unkUShort01
					// ushort unkUShort02
					// ushort unkUShort03
					// ushort unkUShort04
				}
				else
				{
					// SKY WALKING
					entity.Angle = packet.ReadUShort();
					// short unkShort01 = packet.ReadShort();
					// short unkShort02 = packet.ReadShort();
					// short unkShort03 = packet.ReadShort();
					// short unkShort04 = packet.ReadShort();

					// Create a long sky walking movement to calculate realtime coords
					double angle = entity.GetRadianAngle();
					double MovementPosX = Math.Cos(angle) * ushort.MaxValue + currentPosition.PosX;
					double MovementPosY = Math.Sin(angle) * ushort.MaxValue + currentPosition.PosY;
					if (currentPosition.inDungeon())
						entity.MovementPosition = new SRCoord(MovementPosX, MovementPosY, currentPosition.Region, currentPosition.Z);
					else
						entity.MovementPosition = new SRCoord(MovementPosX, MovementPosY);
				}
			}
			// End of Packet
			InfoManager.OnEntityMovement(ref entity);
		}
		public static void EntityMovementStuck(Packet packet)
		{
			SREntity entity = InfoManager.GetEntity(packet.ReadUInt());
			entity.Position = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
			entity.Angle = packet.ReadUShort();
			// End of Packet
			if (entity.isModel())
			{
				SRModel model = ((SRModel)entity);
				model.MovementPosition = entity.Position;
				model.PositionUpdateTimer.Restart();
			}
		}
		public static void EntityMovementAngle(Packet packet)
		{
			SREntity entity = InfoManager.GetEntity(packet.ReadUInt());
			entity.Angle = packet.ReadUShort();
		}
		public static void EnviromentCelestialPosition(Packet packet)
		{
			InfoManager.Character.UniqueID = packet.ReadUInt();
			//ushort moonphase = packet.ReadUShort();
			//byte hour = packet.ReadByte();
			//byte minute = packet.ReadByte();
			// End of Packet
		}
		public static void ChatUpdate(Packet packet)
		{
			SRTypes.Chat updateType = (SRTypes.Chat)packet.ReadByte();
			string player = "";
			switch (updateType)
			{
				case SRTypes.Chat.All:
				case SRTypes.Chat.GM:
				case SRTypes.Chat.NPC:
					uint uniqueID = packet.ReadUInt();
					SREntity p = InfoManager.GetEntity(uniqueID);
					if (p == null)
						player = "[UID:" + uniqueID + "]"; // Just in case
					else
						player = p.Name;
					break;
				case SRTypes.Chat.Private:
				case SRTypes.Chat.Party:
				case SRTypes.Chat.Guild:
				case SRTypes.Chat.Global:
				case SRTypes.Chat.Stall:
				case SRTypes.Chat.Union:
				case SRTypes.Chat.Academy:
					player = packet.ReadAscii();
					break;
				case SRTypes.Chat.Notice:
				default:
					player = "";
					break;
			}
			string message = packet.ReadAscii();
			// End of Packet
			InfoManager.OnChatReceived(updateType, player, message);
		}
		public static void EnviromentCelestialUpdate(Packet packet)
		{
			//byte moonphase = packet.ReadUShort();
			//byte hour = packet.ReadByte();
			//byte minute = packet.ReadByte();
			// End of Packet
		}
		public static void EntityLevelUp(Packet packet)
		{
			// uint uniqueID = packet.ReadUInt();
			// End of Packet
		}
		public static void EntityStatusUpdate(Packet packet)
		{
			SREntity e = InfoManager.GetEntity(packet.ReadUInt());
			// Check if the entity has been despawned already
			if (e == null)
				return;
			SRModel entity = (SRModel)e;
			
			byte unkByte01 = packet.ReadByte();
			byte unkByte02 = packet.ReadByte();
			
			SRTypes.EntityStateUpdate updateType = (SRTypes.EntityStateUpdate)packet.ReadByte();
			switch (updateType)
			{
				case SRTypes.EntityStateUpdate.HP:
					entity.HP = packet.ReadUInt();
					break;
				case SRTypes.EntityStateUpdate.MP:
					entity.MP = packet.ReadUInt();
					break;
				case SRTypes.EntityStateUpdate.HPMP:
				case SRTypes.EntityStateUpdate.EntityHPMP:
					entity.HP = packet.ReadUInt();
					entity.MP = packet.ReadUInt();
					break;
				case SRTypes.EntityStateUpdate.BadStatus:
					entity.BadStatusFlags = (SRModel.BadStatus)packet.ReadUInt();
					break;
			}
			// End of Packet
			InfoManager.OnEntityStatusUpdated(updateType, entity);
		}
		public static void EnviromentWheaterUpdate(Packet packet)
		{
			//byte wheaterType = packet.ReadByte();
			//byte wheaterIntensity = packet.ReadByte();
		}
		public static void NoticeUniqueUpdate(Packet packet)
		{
			Window w = Window.Get;

			byte type = packet.ReadByte();
			switch (type)
			{
				case 5: 
					if (w.Character_cbxMessageUniques.Checked)
					{
						byte unkByte01 = packet.ReadByte();
						uint modelID = packet.ReadUInt();

						string unique = DataManager.GetModelData(modelID)["name"];
						w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_APPEAR_UNIC", unique));
					}
					break;
				case 6:
					if (w.Character_cbxMessageUniques.Checked)
					{
						byte unkByte01 = packet.ReadByte();
						uint modelID = packet.ReadUInt();
						string player = packet.ReadAscii();

						string unique = DataManager.GetModelData(modelID)["name"];
						w.LogMessageFilter(DataManager.GetUIFormat("UIIT_MSG_ANYONE_DEAD_UNIC", player, unique));
					}
					break;
			}
			// 3100 
			// 01
			// ASCII UIIT_MSG_..
		}
		public static void PlayerPetitionRequest(Packet packet)
		{
			SRTypes.PlayerPetition type = (SRTypes.PlayerPetition)packet.ReadByte();
			uint uniqueID = packet.ReadUInt();
			switch (type)
			{
				case SRTypes.PlayerPetition.ExchangeRequest:
					Bot.Get.OnExchangeRequest(uniqueID);
					break;
				case SRTypes.PlayerPetition.PartyCreation:
				case SRTypes.PlayerPetition.PartyInvitation:
					{
						SRParty.Setup setup = (SRParty.Setup)packet.ReadByte();
						Bot.Get.OnPartyInvitation(uniqueID, setup);
					}
					break;
				case SRTypes.PlayerPetition.Resurrection:
					Bot.Get.OnResurrection(uniqueID);
					break;
				case SRTypes.PlayerPetition.GuildInvitation:
					//Bot.Get.OnGuildInvitation(uniqueID);
					break;
				case SRTypes.PlayerPetition.UnionInvitation:
					//Bot.Get.OnUnionInvitation(uniqueID);
					break;
				case SRTypes.PlayerPetition.AcademyInvitation:
					//Bot.Get.OnAcademyInvitation(uniqueID);
					break;
			}
		}
		public static void ExchangeStarted(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			// End of Packet
			InfoManager.OnExchangeStart(uniqueID);
		}
		public static void ExchangePlayerConfirmed(Packet packet)
		{
			InfoManager.OnExchangePlayerConfirmed();
		}
		public static void ExchangeCompleted(Packet packet)
		{
			InfoManager.OnExchangeCompleted();
		}
		public static void ExchangeCanceled(Packet packet)
		{
			// ushort reasonID = packet.ReadUShort();
			// End of Packet
			InfoManager.OnExchangeCanceled();
		}
		public static void ExchangeItemsUpdate(Packet packet)
		{
			SRPlayer player = (SRPlayer)InfoManager.GetEntity(packet.ReadUInt());
			if (InfoManager.Character == player)
				// it's already handled trought inventory movements
				return;

			// Updating items
			xList<SRItemExchange> inventoryExchange = new xList<SRItemExchange>(packet.ReadByte());
			for (byte j = 0; j < inventoryExchange.Capacity; j++)
			{
				SRItemExchange item = new SRItemExchange();
				byte slotInventory = packet.ReadByte();
				if (player == InfoManager.Character)
				{
					byte slotExchange = packet.ReadByte();
				}
				inventoryExchange[j] = new SRItemExchange();
				inventoryExchange[j].Item = ItemParsing(packet);
			}
			// End of Packet
			InfoManager.OnExchangeItemsUpdate(player, inventoryExchange);
		}
		public static void ExchangeGoldUpdate(Packet packet)
		{
			byte unkByte01 = packet.ReadByte();
			ulong gold = packet.ReadULong();

			InfoManager.OnExchangeGoldUpdate(gold,false);
		}
		public static void ExchangeInvitationResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint uniqueID = packet.ReadUInt();
				InfoManager.OnExchangeStart(uniqueID);
			}
		}
		public static void ExchangeConfirmResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
				InfoManager.OnExchangeConfirmed();
		}
		public static void ExchangeApproveResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
				InfoManager.OnExchangeApproved();
		}
		public static void ExchangeExitResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
				InfoManager.OnExchangeCanceled();
		}
		public static void PartyData(Packet packet)
		{
			SRParty Party = new SRParty();
			uint unkUint01 = packet.ReadUInt();
			uint unkUint02 = packet.ReadUInt();
			Party.PurposeType = (SRParty.Purpose)packet.ReadByte();
			Party.SetupFlags = (SRParty.Setup)packet.ReadByte();
			byte playerCount = packet.ReadByte();
			for (int j = 0; j < playerCount; j++)
			{
				SRPartyMember member = PartyDataMemberParsing(packet);
				Party.Members[member.ID] = member;
			}
			// End of Packet
			InfoManager.OnPartyInfo(Party);
		}
		private static SRPartyMember PartyDataMemberParsing(Packet p)
		{
			SRPartyMember PartyMember = new SRPartyMember();
			byte unkByte01 = p.ReadByte();
			PartyMember.ID = p.ReadUInt();
			PartyMember.Name = p.ReadAscii();
			PartyMember.ModelID = p.ReadUInt();
			PartyMember.Level = p.ReadByte();
			PartyMember.HPMP = p.ReadByte();
			ushort region = p.ReadUShort();
			if (SRCoord.inDungeon(region))
				PartyMember.Position = new SRCoord(region, p.ReadInt(), p.ReadInt(), p.ReadInt());
			else
				PartyMember.Position = new SRCoord(region, (int)p.ReadUShort(), (int)p.ReadUShort(), (int)p.ReadUShort());
			byte unkByte02 = p.ReadByte(); // 2 = unkByte07.
			byte unkByte03 = p.ReadByte();
			byte unkByte04 = p.ReadByte();
			byte unkByte05 = p.ReadByte();
			PartyMember.GuildName = p.ReadAscii();
			byte unkByte06 = p.ReadByte();
			if (p.Opcode == Agent.Opcode.SERVER_PARTY_UPDATE && unkByte02 == 2)
			{
				byte unkByte07 = p.ReadByte();
			}
			PartyMember.MasteryPrimaryID = p.ReadUInt();
			PartyMember.MasterySecondaryID = p.ReadUInt();
			return PartyMember;
		}
		public static void PartyUpdate(Packet packet)
		{
			byte updateType = packet.ReadByte();
			switch (updateType)
			{
				case 1: // Dismissed
					{
						// ushort errCode = packet.ReadUShort();
						InfoManager.OnPartyLeft();
					}
					break;
				case 2: // Member joined
					{
						SRPartyMember newMember = PartyDataMemberParsing(packet);
						InfoManager.OnPartyMemberJoined(newMember);
					}
					break;
				case 3: // Member left
					{
						uint ID = packet.ReadUInt();
						// byte unkByte01 = packet.ReadByte();
						InfoManager.OnPartyMemberLeft(ID);
					}
					break;
				case 6: // Member update
					{
						uint ID = packet.ReadUInt();
						SRPartyMember member = InfoManager.Party.Members[ID];

						Window w = Window.Get;
						byte memberUpdateType = packet.ReadByte();
						switch (memberUpdateType)
						{
							case 2:
								member.Level = packet.ReadByte();

								w.Party_lstvPartyMembers.InvokeIfRequired(() => {
									w.Party_lstvPartyMembers.Items[ID.ToString()].SubItems[2].Text = member.Level.ToString();
								});
								break;
							case 4: // HP & MP
								member.HPMP = packet.ReadByte();
								// Weird : sometimes hp is wrong (by 10%), giving as result 110% or 10% in dead state
								byte fixedHP = member.HPPercent;

								w.Party_lstvPartyMembers.InvokeIfRequired(() => {
									w.Party_lstvPartyMembers.Items[ID.ToString()].SubItems[3].Text = string.Format("{0}% / {1}%", fixedHP > 100 ? 100 : fixedHP, member.MPPercent);
								});
								break;
							case 0x20: // Map position
								{
									ushort region = packet.ReadUShort();
									if (SRCoord.inDungeon(region))
										member.Position = new SRCoord(region, packet.ReadInt(), packet.ReadInt(), packet.ReadInt());
									else
										member.Position = new SRCoord(region, (int)packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort());
									string regionName = DataManager.GetRegion(region);

									w.Party_lstvPartyMembers.InvokeIfRequired(() => {
										w.Party_lstvPartyMembers.Items[ID.ToString()].SubItems[4].Text = regionName;
									});
								}
								break;
						}
					}
					break;
			}
		}
		public static void PartyMatchListResponse(Packet packet) {

			xDictionary<uint, SRPartyMatch> PartyMatches = new xDictionary<uint, SRPartyMatch>();
			if (packet.ReadBool())
			{
				byte pageCount = packet.ReadByte();
				byte pageIndex = packet.ReadByte();
				byte partyCount = packet.ReadByte();
				for (byte j = 0; j < partyCount; j++)
				{
					SRPartyMatch Party = new SRPartyMatch();
					Party.Number = packet.ReadUInt();
					Party.MasterJoinID = packet.ReadUInt();
					Party.MasterName = packet.ReadAscii();
					byte RaceType = packet.ReadByte();
					Party.MemberCount = packet.ReadByte();
					Party.Setup = (SRParty.Setup)packet.ReadByte();
					Party.Purpose = (SRParty.Purpose)packet.ReadByte();
					Party.LevelMin = packet.ReadByte();
					Party.LevelMax = packet.ReadByte();
					Party.Title = packet.ReadAscii();
					PartyMatches[Party.Number] = Party;
				}
				InfoManager.OnPartyMatchListing(pageIndex,pageCount, PartyMatches);
			}
			else
			{
				InfoManager.OnPartyMatchListing(0, 0, PartyMatches);
			}
		}
		public static void PartyMatchDeleteResponse(Packet packet)
		{
			// success
			if (packet.ReadBool()){
				// Generate events to remake party match
				Bot.Get.OnPartyMatchDeleted(packet.ReadUInt());
			}
		}
		public static void PartyMatchJoinRequest(Packet packet)
		{
			uint requestID = packet.ReadUInt();
			uint joinID = packet.ReadUInt();
			uint matchNumber = packet.ReadUInt();
			uint masteryID_primary = packet.ReadUInt();
			uint masteryID_secondary = packet.ReadUInt();
			byte unkByte00 = packet.ReadByte();
			byte unkByte01 = packet.ReadByte();
			uint joinID_x2 = packet.ReadUInt();
			string name = packet.ReadAscii();
			// ...
			// ...

			// End of packet
			Bot.Get.OnPartyMatchJoinRequest(requestID, joinID, name);
		}

		private static Packet StorageDataPacket;
		public static void StorageDataBegin(Packet packet)
		{
			InfoManager.Character.StorageGold = packet.ReadULong();
			StorageDataPacket = new Packet(Agent.Opcode.SERVER_STORAGE_DATA);
		}
		public static void StorageData(Packet packet)
		{
			StorageDataPacket.WriteByteArray(packet.GetBytes());
		}
		public static void StorageDataEnd(Packet packet)
		{
			Packet p = StorageDataPacket;
			p.Lock();

			xList<SRItem> storage = new xList<SRItem>(p.ReadByte());
			byte itemsCount = p.ReadByte();
			for (int j = 0; j < itemsCount; j++)
			{
				byte slot = p.ReadByte();
				storage[slot] = ItemParsing(p);
			}
			InfoManager.OnStorageInfo(storage);
		}
		private static Packet GuildDataPacket;
		public static void GuildCreatedData(Packet packet)
		{
			// success
			if (packet.ReadBool())
				GuildDataParsing(packet);
		}
		public static void GuildDataBegin()
		{
			GuildDataPacket = new Packet(Agent.Opcode.SERVER_GUILD_DATA);
		}
		public static void GuildData(Packet packet)
		{
			GuildDataPacket.WriteByteArray(packet.GetBytes());
		}
		public static void GuildDataEnd()
		{
			GuildDataPacket.Lock();
			GuildDataParsing(GuildDataPacket);
		}
		private static void GuildDataParsing(Packet p)
		{
			SRGuild Guild = new SRGuild();
			Guild.ID = p.ReadUInt();
			Guild.Name = p.ReadAscii();
			Guild.Level = p.ReadByte();
			Guild.GPoints = p.ReadUInt();
			Guild.Notice = p.ReadAscii();
			Guild.Message = p.ReadAscii();
			uint unkUInt00 = p.ReadUInt();
			byte unkByte00 = p.ReadByte();
			byte memberCount = p.ReadByte();
			for (byte j = 0; j < memberCount; j++)
			{
				SRGuildMember Member = new SRGuildMember();
				Member.ID = p.ReadUInt();
				Member.Name = p.ReadAscii();
				Member.unkByte01 = p.ReadByte();
				Member.Level = p.ReadByte();
				Member.GPoints = p.ReadUInt();
				Member.PermissionsFlags = (SRGuildMember.Permissions)p.ReadUInt();
				if (Member.PermissionsFlags == SRGuildMember.Permissions.Master)
					Guild.Master = Member;
				Member.unkUInt01 = p.ReadUInt();
				Member.unkUInt02 = p.ReadUInt();
				Member.unkUInt03 = p.ReadUInt();
				Member.Nickname = p.ReadAscii();
				Member.ModelID = p.ReadUInt();
				bool isMaster = p.ReadBool();
				Member.isOffline = p.ReadBool();
				Guild.Members[Member.ID] = Member;
			}
			// End of Packet
			InfoManager.OnGuildInfo(Guild);
		}
		public static void GuildUpdate(Packet packet)
		{
			 byte updateType = packet.ReadByte();
			 switch(updateType)
			 {
			 	case 5: // NOTICE

			 	break;
			 	case 6: // Permissions

			 	break;
			 	case 15:

			 	break;
			 }
		}
		private static Packet GuildStorageDataPacket;
		public static void GuildStorageDataBegin(Packet packet)
		{
			ulong gold = packet.ReadULong();
			// End of Packet
			InfoManager.Guild.StorageGold = gold;
			GuildStorageDataPacket = new Packet(Agent.Opcode.SERVER_GUILD_STORAGE_DATA);
		}
		public static void GuildStorageData(Packet packet)
		{
			GuildStorageDataPacket.WriteByteArray(packet.GetBytes());
		}
		public static void GuildStorageDataEnd(Packet packet)
		{
			Packet p = GuildStorageDataPacket;
			p.Lock();

			xList<SRItem> storage = new xList<SRItem>(p.ReadByte());
			byte itemsCount = p.ReadByte();
			for (int j = 0; j < itemsCount; j++)
			{
				byte slot = p.ReadByte();
				storage[slot] = ItemParsing(p);
			}
			InfoManager.Guild.Storage = storage;
		}
		public static void AcademyData(Packet packet)
		{
			InfoManager.OnAcademyInfo();
		}
		public static void CharacterAddStatPointResponse(Packet packet)
		{
			InfoManager.OnCharacterStatPointAdded(packet.ReadBool());
		}
		public static bool InventoryItemMovement(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				SRTypes.InventoryItemMovement type = (SRTypes.InventoryItemMovement)packet.ReadByte();
				switch (type)
				{
					case SRTypes.InventoryItemMovement.InventoryToInventory:
						InventoryItemMovement_InventoryToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.StorageToStorage:
						InventoryItemMovement_StorageToStorage(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToStorage:
						InventoryItemMovement_InventoryToStorage(packet);
						break;
					case SRTypes.InventoryItemMovement.StorageToInventory:
						InventoryItemMovement_StorageToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToExchange:
						InventoryItemMovement_InventoryToExchange(packet);
						break;
					case SRTypes.InventoryItemMovement.ExchangeToInventory:
						InventoryItemMovement_ExchangeToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.GroundToInventory:
						InventoryItemMovement_GroundToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToGround:
						InventoryItemMovement_InventoryToGround(packet);
						break;
					case SRTypes.InventoryItemMovement.ShopToInventory:
						InventoryItemMovement_ShopToInventory(packet);
						// Client ignore packet, the bot will handle it as a pick up injection (always)
						return true;
					case SRTypes.InventoryItemMovement.InventoryToShop:
						InventoryItemMovement_InventoryToShop(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryGoldToGround:
						InventoryItemMovement_InventoryGoldToGround(packet);
						break;
					case SRTypes.InventoryItemMovement.StorageGoldToInventory:
						InventoryItemMovement_StorageGoldToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryGoldToStorage:
						InventoryItemMovement_InventoryGoldToStorage(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryGoldToExchange:
						InventoryItemMovement_InventoryGoldToExchange(packet);
						break;
					case SRTypes.InventoryItemMovement.QuestToInventory:
						InventoryItemMovement_QuestToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToQuest:
						InventoryItemMovement_InventoryToQuest(packet);
						break;
					case SRTypes.InventoryItemMovement.TransportToTransport:
						InventoryItemMovement_TransportToTransport(packet);
						break;
					case SRTypes.InventoryItemMovement.GroundToPet:
						InventoryItemMovement_GroundToPet(packet);
						break;
					case SRTypes.InventoryItemMovement.ShopToTransport:
						InventoryItemMovement_ShopToTransport(packet);
						// Client ignore packet, the bot will handle it as a pick up injection (always)
						return true;
					case SRTypes.InventoryItemMovement.TransportToShop:
						InventoryItemMovement_TransportToShop(packet);
						break;
					case SRTypes.InventoryItemMovement.PetToPet:
						InventoryItemMovement_PetToPet(packet);
						break;
					case SRTypes.InventoryItemMovement.PetToInventory:
						InventoryItemMovement_PetToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToPet:
						InventoryItemMovement_InventoryToPet(packet);
						break;
					case SRTypes.InventoryItemMovement.GroundToPetToInventory:
						InventoryItemMovement_GroundToPetToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.GuildToGuild:
						InventoryItemMovement_GuildToGuild(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToGuild:
						InventoryItemMovement_InventoryToGuild(packet);
						break;
					case SRTypes.InventoryItemMovement.GuildToInventory:
						InventoryItemMovement_GuildToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryGoldToGuild:
						InventoryItemMovement_InventoryGoldToGuild(packet);
						break;
					case SRTypes.InventoryItemMovement.GuildGoldToInventory:
						InventoryItemMovement_GuildGoldToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.ShopBuyBack:
						InventoryItemMovement_ShopBuyBack(packet);
						break;
					case SRTypes.InventoryItemMovement.AvatarToInventory:
						InventoryItemMovement_AvatarToInventory(packet);
						break;
					case SRTypes.InventoryItemMovement.InventoryToAvatar:
						InventoryItemMovement_InventoryToAvatar(packet);
						break;
				}
			}
			return false;
		}
		private static void InventoryItemMovement_InventoryToInventory(Packet p)
		{
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();
			bool isDoubleMovement = p.ReadBool();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if (inventory[slotInitial].QuantityMax == 1
					|| inventory[slotInitial].Quantity == quantityMoved)
				{
					// switch (empty)
					SRItem temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					inventory[slotFinal].Quantity = quantityMoved;
					inventory[slotInitial].Quantity = (ushort)(inventory[slotInitial].Quantity - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| inventory[slotFinal].Quantity == inventory[slotFinal].QuantityMax
				|| quantityMoved == inventory[slotFinal].QuantityMax)
			{
				// switch
				SRItem temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if (inventory[slotInitial].Quantity == quantityMoved)
				{
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial].Quantity -= quantityMoved;
				}
			}

			InfoManager.OnInventoryMovement(slotInitial, slotFinal);

			if (isDoubleMovement)
				InventoryItemMovement_InventoryToInventory(p);
		}
		private static void InventoryItemMovement_StorageToStorage(Packet p)
		{
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Storage;

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if (inventory[slotInitial].QuantityMax == 1
					|| inventory[slotInitial].Quantity == quantityMoved)
				{
					// switch (empty)
					SRItem temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					inventory[slotFinal].Quantity = quantityMoved;
					inventory[slotInitial].Quantity = (ushort)(inventory[slotInitial].Quantity - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| inventory[slotFinal].Quantity == inventory[slotFinal].QuantityMax
				|| quantityMoved == inventory[slotFinal].QuantityMax)
			{
				// switch
				SRItem temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if (inventory[slotInitial].Quantity == quantityMoved)
				{
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial].Quantity -= quantityMoved;
				}
			}
		}
		private static void InventoryItemMovement_InventoryToStorage(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotStorage = p.ReadByte();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;
			xList<SRItem> storage = InfoManager.Character.Storage;

			// Just move it leaving an empty space at inventory
			storage[slotStorage] = inventory[slotInventory];
			inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_StorageToInventory(Packet p)
		{
			byte slotStorage = p.ReadByte();
			byte slotInventory = p.ReadByte();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;
			xList<SRItem> storage = InfoManager.Character.Storage;

			// Just move it leaving an empty space at storage
			inventory[slotInventory] = storage[slotStorage];
			storage[slotStorage] = null;
		}
		private static void InventoryItemMovement_InventoryToExchange(Packet p)
		{
			byte slotInventory = p.ReadByte();
			// byte unkByte01 = p.ReadByte();
			// End of Packet
			InfoManager.OnInventoryToExchange(slotInventory);
		}
		private static void InventoryItemMovement_ExchangeToInventory(Packet p)
		{
			byte slotInventoryExchange = p.ReadByte();
			// byte unkByte01 = p.ReadByte();
			// End of Packet
			InfoManager.OnExchangeToInventory(slotInventoryExchange);
		}
		private static void InventoryItemMovement_GroundToInventory(Packet p)
		{
			byte slotInventory = p.ReadByte();
			SRItem item = ItemParsing(p);
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;

			// Check quantity picked up
			ushort quantity = 1;
			if (inventory[slotInventory] != null)
				quantity = (ushort)(item.Quantity - inventory[slotInventory].Quantity);

			inventory[slotInventory] = item;

			Bot.Get.OnItemPickedUp(item, quantity);
		}
		private static void InventoryItemMovement_InventoryToGround(Packet p)
		{
			byte slotInventory = p.ReadByte();
			// End of Packet
			
			InfoManager.Character.Inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_ShopToInventory(Packet p)
		{
			byte tabNumber = p.ReadByte();
			byte tabSlot = p.ReadByte();
			byte packageCount = p.ReadByte();

			xList<SRItem> inventory = InfoManager.Character.Inventory;

			// Select the item from the shop specified
			SREntity NPCEntity = InfoManager.GetEntity(InfoManager.SelectedEntityUniqueID);
			SRItem item = DataManager.GetItemFromShop(NPCEntity.ServerName, tabNumber, tabSlot);

			if (packageCount == 1)
			{
				byte slotInventory = p.ReadByte();
				item.Quantity = p.ReadUShort();
				uint unkUInt01 = p.ReadUInt();
				inventory[slotInventory] = item;

				PacketBuilder.Client.CreatePickUpPacket(item, slotInventory);
			}
			else
			{
				/// Not confirmed when will happen this behaviour
				//for (byte j = 0; j < packageCount; j++)
				//{
				//	byte slotInventory = p.ReadByte();
				//	item.Quantity = 1;
				//	inventory[slotInventory] = item;

				//	PacketBuilder.Client.CreatePickUpPacket(item, slotInventory);
				//}
			}
		}
		private static void InventoryItemMovement_InventoryToShop(Packet p)
		{
			byte slotInventory = p.ReadByte();
			ushort quantitySold = p.ReadUShort();
			uint NPCModel = p.ReadUInt();
			byte slotBuyBack = p.ReadByte();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;

			if (InfoManager.Character.ShopBuyBack == null)
				InfoManager.Character.ShopBuyBack = new xList<SRItem>();
			xList<SRItem> buyBack = InfoManager.Character.ShopBuyBack;

			// Sync max. quantity to buy back
			if (slotBuyBack == 5 && slotBuyBack == buyBack.Count)
				buyBack.RemoveAt(0);

			if (inventory[slotInventory].Quantity == quantitySold)
			{
				// Check if action can be revert as buy back
				if (slotBuyBack != byte.MaxValue)
					buyBack[slotBuyBack - 1] = inventory[slotInventory];
				inventory[slotInventory] = null;
			}
			else
			{
				buyBack[slotBuyBack - 1] = inventory[slotInventory].Clone();
				buyBack[slotBuyBack - 1].Quantity = quantitySold;
				inventory[slotInventory].Quantity -= quantitySold;
			}
		}
		private static void InventoryItemMovement_InventoryGoldToGround(Packet p)
		{
			// ulong gold =  p.ReadULong();
		}
		private static void InventoryItemMovement_StorageGoldToInventory(Packet p)
		{
			ulong gold =  p.ReadULong();
			// End of Packet
			InfoManager.Character.StorageGold -= gold;
		}
		private static void InventoryItemMovement_InventoryGoldToStorage(Packet p)
		{
			ulong gold =  p.ReadULong();
			// End of Packet
			InfoManager.Character.StorageGold += gold;
		}
		private static void InventoryItemMovement_InventoryGoldToExchange(Packet p)
		{
			ulong gold = p.ReadULong();
			//byte unkByte01 = p.ReadByte();
			// End of Packet
			InfoManager.OnExchangeGoldUpdate(gold, true);
		}
		private static void InventoryItemMovement_QuestToInventory(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte unkByte01 = p.ReadByte();
			SRItem item = ItemParsing(p);
			// End of Packet
			InfoManager.Character.Inventory[slotInventory] = item;
		}
		private static void InventoryItemMovement_InventoryToQuest(Packet p)
		{
			byte slotInventory = p.ReadByte();
			//byte unkByte01 = p.ReadByte();
			// End of Packet
			InfoManager.Character.Inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_TransportToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRCoService pet = InfoManager.MyPets[uniqueID];
			xList<SRItem> inventory = pet.Inventory;

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if (inventory[slotInitial].QuantityMax == 1
					|| inventory[slotInitial].Quantity == quantityMoved)
				{
					// switch (empty)
					SRItem temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					inventory[slotFinal].Quantity = quantityMoved;
					inventory[slotInitial].Quantity = (ushort)(inventory[slotInitial].Quantity - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| inventory[slotFinal].Quantity == inventory[slotFinal].QuantityMax
				|| quantityMoved == inventory[slotFinal].QuantityMax)
			{
				// switch
				SRItem temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if (inventory[slotInitial].Quantity == quantityMoved)
				{
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial].Quantity -= quantityMoved;
				}
			}
		}
		private static void InventoryItemMovement_GroundToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInventory = p.ReadByte();
			SRItem item = ItemParsing(p);
			//string OwnerName = p.ReadAscii(); ??
			// End of Packet

			SRCoService pet = InfoManager.MyPets[uniqueID];
			// Check quantity picked up
			ushort quantity = 1;
			if (pet.Inventory[slotInventory] != null)
				quantity = (ushort)(item.Quantity - pet.Inventory[slotInventory].Quantity);

			pet.Inventory[slotInventory] = item;

			Bot.Get.OnItemPickedUp(item, quantity);
		}
		private static void InventoryItemMovement_ShopToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte tabNumber = p.ReadByte();
			byte tabSlot = p.ReadByte();
			byte packageCount = p.ReadByte();
			
			SRCoService pet = InfoManager.MyPets[uniqueID];
			xList<SRItem> inventory = pet.Inventory;
			// Select the item from the shop specified
			SREntity NPCEntity = InfoManager.GetEntity(InfoManager.SelectedEntityUniqueID);
			SRItem item = DataManager.GetItemFromShop(NPCEntity.ServerName, tabNumber, tabSlot);
			
			if (packageCount == 1)
			{
				byte slotInventory = p.ReadByte();
				item.Quantity = p.ReadUShort();
				uint unkUInt01 = p.ReadUInt();
				inventory[slotInventory] = item;

				PacketBuilder.Client.CreatePickUpSpecialtyGoodsPacket(inventory[slotInventory], slotInventory, uniqueID, InfoManager.CharName,unkUInt01);
			}
		}
		private static void InventoryItemMovement_TransportToShop(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInventory = p.ReadByte();
			ushort quantitySold = p.ReadUShort();
			//uint npcUniqueID = p.ReadUInt();
			//uint unkByte01 = p.ReadByte();

			SRCoService pet = InfoManager.MyPets[uniqueID];
			xList<SRItem> inventory = pet.Inventory;
			if (inventory[slotInventory].Quantity == quantitySold)
				inventory[slotInventory] = null;
			else
				inventory[slotInventory].Quantity -= quantitySold;
		}
		private static void InventoryItemMovement_PetToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();
			// End of Packet

			SRCoService pet = InfoManager.MyPets[uniqueID];
			xList<SRItem> inventory = pet.Inventory;

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if (inventory[slotInitial].QuantityMax == 1
					|| inventory[slotInitial].Quantity == quantityMoved)
				{
					// switch (empty)
					SRItem temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					inventory[slotFinal].Quantity = quantityMoved;
					inventory[slotInitial].Quantity = (ushort)(inventory[slotInitial].Quantity - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| inventory[slotFinal].Quantity == inventory[slotFinal].QuantityMax
				|| quantityMoved == inventory[slotFinal].QuantityMax)
			{
				// switch
				SRItem temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if (inventory[slotInitial].Quantity == quantityMoved)
				{
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal].Quantity += quantityMoved;
					inventory[slotInitial].Quantity -= quantityMoved;
				}
			}
		}
		private static void InventoryItemMovement_PetToInventory(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotPetInventory = p.ReadByte();
			byte slotMyInventory = p.ReadByte();
			// End of Packet

			xList<SRItem> myInventory = InfoManager.Character.Inventory;
			SRCoService pet = InfoManager.MyPets[uniqueID];
			xList<SRItem> petInventory = pet.Inventory;

			myInventory[slotMyInventory] = petInventory[slotPetInventory];
			petInventory[slotPetInventory] = null;
		}
		private static void InventoryItemMovement_InventoryToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotMyInventory = p.ReadByte();
			byte slotPetInventory = p.ReadByte();
			// End of Packet

			xList<SRItem> myInventory = InfoManager.Character.Inventory;
			SRCoService pet = InfoManager.MyPets[uniqueID];
			xList<SRItem> petInventory = pet.Inventory;

			petInventory[slotPetInventory] = myInventory[slotMyInventory];
			myInventory[slotMyInventory] = null;
		}
		private static void InventoryItemMovement_GroundToPetToInventory(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInventory = p.ReadByte();
			
			if(slotInventory != 254)
			{
				SRItem item = ItemParsing(p);
				// End of Packet

				xList<SRItem> inventory = InfoManager.Character.Inventory;

				// Check quantity picked up
				ushort quantity = 1;
				if (inventory[slotInventory] != null)
					quantity = (ushort)(item.Quantity - inventory[slotInventory].Quantity);

				inventory[slotInventory] = item;

				Bot.Get.OnItemPickedUp(item, quantity);
			}
		}
		private static void InventoryItemMovement_GuildToGuild(Packet p)
		{
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();
			// End of Packet

			xList<SRItem> storage = InfoManager.Guild.Storage;

			// Check if is stack or just switch.. and update it.
			if (storage[slotFinal] == null)
			{
				if (storage[slotInitial].QuantityMax == 1
					|| storage[slotInitial].Quantity == quantityMoved)
				{
					// switch (empty)
					SRItem temp = storage[slotFinal];
					storage[slotFinal] = storage[slotInitial];
					storage[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					storage[slotFinal] = storage[slotInitial].Clone();
					storage[slotFinal].Quantity = quantityMoved;
					storage[slotInitial].Quantity = (ushort)(storage[slotInitial].Quantity - quantityMoved);
				}
			}
			else if (storage[slotFinal].ID != storage[slotInitial].ID
				|| storage[slotFinal].Quantity == storage[slotFinal].QuantityMax
				|| quantityMoved == storage[slotFinal].QuantityMax)
			{
				// switch
				SRItem temp = storage[slotFinal];
				storage[slotFinal] = storage[slotInitial];
				storage[slotInitial] = temp;
			}
			else
			{
				// stacking
				if (storage[slotInitial].Quantity == quantityMoved)
				{
					storage[slotFinal].Quantity += quantityMoved;
					storage[slotInitial] = null;
				}
				else
				{
					// fixing
					storage[slotFinal].Quantity += quantityMoved;
					storage[slotInitial].Quantity -= quantityMoved;
				}
			}
		}
		private static void InventoryItemMovement_InventoryToGuild(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotStorage = p.ReadByte();
			// End of Packet
			
			xList<SRItem> inventory = InfoManager.Character.Inventory;
			xList<SRItem> storage = InfoManager.Guild.Storage;

			// Just move it leaving an empty space at inventory
			storage[slotStorage] = inventory[slotInventory];
			inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_GuildToInventory(Packet p)
		{
			byte slotStorage = p.ReadByte();
			byte slotInventory = p.ReadByte();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;
			xList<SRItem> storage = InfoManager.Guild.Storage;

			// Just move it leaving an empty space at storage
			inventory[slotInventory] = storage[slotStorage];
			storage[slotStorage] = null;
		}
		private static void InventoryItemMovement_InventoryGoldToGuild(Packet p)
		{
			ulong gold = p.ReadULong();
			// End of Packet
			InfoManager.Guild.StorageGold += gold;
		}
		private static void InventoryItemMovement_GuildGoldToInventory(Packet p)
		{
			ulong gold = p.ReadULong();
			// End of Packet
			InfoManager.Guild.StorageGold -= gold;
		}
		private static void InventoryItemMovement_ShopBuyBack(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotBuyBack = p.ReadByte();
			ushort quantitySold = p.ReadUShort();
			// End of Packet

			xList<SRItem> inventory = InfoManager.Character.Inventory;
			xList<SRItem> buyBack = InfoManager.Character.ShopBuyBack;

			inventory[slotInventory] = buyBack[slotBuyBack];
			buyBack.RemoveAt(slotBuyBack);
		}
		private static void InventoryItemMovement_AvatarToInventory(Packet p)
		{               
			byte slotInventoryAvatar = p.ReadByte();
			byte slotInventory = p.ReadByte();
			// End of Packet

			xList<SRItem> inventoryAvatar = InfoManager.Character.InventoryAvatar;
			xList<SRItem> inventory = InfoManager.Character.Inventory;
			
			// Switch
			SRItem item = inventory[slotInventory];
			inventory[slotInventory] = inventoryAvatar[slotInventoryAvatar];
			inventoryAvatar[slotInventoryAvatar] = item;
		}
		private static void InventoryItemMovement_InventoryToAvatar(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotInventoryAvatar = p.ReadByte();
			// End of Packet

			xList<SRItem> inventoryAvatar = InfoManager.Character.InventoryAvatar;
			xList<SRItem> inventory = InfoManager.Character.Inventory;

			// Switch
			SRItem item = inventory[slotInventory];
			inventory[slotInventory] = inventoryAvatar[slotInventoryAvatar];
			inventoryAvatar[slotInventoryAvatar] = item;
		}
		public static void InventoryItemUse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				byte slotInventory = packet.ReadByte();
				ushort quantityUpdate = packet.ReadUShort();
				//ushort usageType = packet.ReadUShort();
				// End of Packet

				xList<SRItem> inventory = InfoManager.Character.Inventory;

				if (quantityUpdate == 0)
					inventory[slotInventory] = null; // Item consumed
				else
					inventory[slotInventory].Quantity = quantityUpdate;
			}
		}
		public static void InventoryItemDurabilityUpdate(Packet packet)
		{
			byte slotInventory = packet.ReadByte();
			uint durability = packet.ReadUInt();
			// End of Packet

			SREquipable item = (SREquipable)InfoManager.Character.Inventory[slotInventory];
			item.Durability = durability;
		}
		public static void InventoryItemUpdate(Packet packet)
		{
			byte slotInventory = packet.ReadByte();
			byte updateType = packet.ReadByte();
			
			xList<SRItem> inventory = InfoManager.Character.Inventory;
			switch (updateType)
			{
				case 8: // Quantity
					{
						ushort quantity = packet.ReadUShort();
						if (quantity == 0)
							inventory[slotInventory] = null; // Item consumed
						else
							inventory[slotInventory].Quantity = quantity;
					}
					break;
				case 0x40: // Pet State
					{
						SRCoS cos = (SRCoS)inventory[slotInventory];
						cos.StateType = (SRCoS.State)packet.ReadByte();
					}
					break;
			}
		}
		public static void InventoryCapacityUpdate(Packet packet)
		{
			// success
			if(packet.ReadBool()){
				byte newCapacity = packet.ReadByte();
				InfoManager.Character.Inventory.Resize(newCapacity);
			}
		}
		public static void ConsigmentRegisterResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				xList<SRItem> inventory = InfoManager.Character.Inventory;

				byte itemCount = packet.ReadByte();
				for (byte j = 0; j < itemCount; j++)
				{
					byte slotInventory = packet.ReadByte();
					byte saleStatus = packet.ReadByte();
					uint slotConsigment = packet.ReadUInt();
					uint itemID = packet.ReadUInt();
					ulong goldDeposited = packet.ReadULong();
					ulong goldSellingFee = packet.ReadULong();
					uint EndDate = packet.ReadUInt();

					// remove it from inventory
					inventory[slotInventory] = null;
				}
			}
		}
		public static void ConsigmentUnregisterResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				xList<SRItem> inventory = InfoManager.Character.Inventory;

				byte itemCount = packet.ReadByte();
				for (byte j = 0; j < itemCount; j++)
				{
					uint slotConsigment = packet.ReadUInt();
					byte slotInventory = packet.ReadByte();
					// Add to inventory
					inventory[slotInventory] = ItemParsing(packet);
				}
			}
		}
		public static void PetData(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			uint modelID = packet.ReadUInt();

			SRCoService cos = (SRCoService)InfoManager.GetEntity(uniqueID);
			if (cos.isHorse() || cos.isTransport())
			{
				cos.HP = packet.ReadUInt();
				cos.HPMax = packet.ReadUInt();
				// Inventory
				xList<SRItem> inventory = new xList<SRItem>(packet.ReadByte());
				if (inventory.Capacity > 0)
				{
					// TRANSPORT
					byte itemsCount = packet.ReadByte();
					for (byte j = 0; j < itemsCount; j++)
					{
						byte slotInventory = packet.ReadByte();

						SRRentable rentable = new SRRentable(packet.ReadUInt());
						SRItem item = SRItem.Create(packet.ReadUInt(), rentable);
						item.Quantity = packet.ReadUShort();
						string OwnerName = packet.ReadAscii();
					
						inventory[slotInventory] = item;
					}
					cos.Inventory = inventory;
					//uint ownerUniqueID = packet.ReadUInt();
				}
			}
			else if (cos.isAttackPet())
			{
				SRAttackPet pet = (SRAttackPet)cos;
				pet.HP = packet.ReadUInt();
				pet.unkUInt01 = packet.ReadUInt();
				pet.Exp = packet.ReadULong();
				pet.Level = packet.ReadByte();
				pet.HGP = packet.ReadUShort();
				pet.SettingsFlags = (SRAttackPet.Settings)packet.ReadUInt();
				pet.Name = packet.ReadAscii();
				pet.unkByte03 = packet.ReadByte();
				pet.OwnerUniqueID = packet.ReadUInt();
				pet.unkByte04 = packet.ReadByte();
			}
			else if (cos.isPickPet())
			{
				SRPickPet pet = (SRPickPet)cos;
				pet.unkUInt01 = packet.ReadUInt();
				pet.unkUInt02 = packet.ReadUInt();
				pet.SettingsFlags = (SRPickPet.Settings)packet.ReadUInt();
				pet.Name = packet.ReadAscii();
				// Inventory
				xList<SRItem> inventory = new xList<SRItem>(packet.ReadByte());
				byte itemsCount = packet.ReadByte();
				for (byte j = 0; j < itemsCount; j++)
				{
					byte slot = packet.ReadByte();
					inventory[slot] = ItemParsing(packet);
				}
				pet.Inventory = inventory;
				//uint ownerUniqueID = packet.ReadUInt();
			}
			InfoManager.OnPetSummoned(cos);
		}
		public static void PetUpdate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			byte updateType = packet.ReadByte();

			SRCoService cos = (SRCoService)InfoManager.GetEntity(uniqueID);
			switch (updateType)
			{
				case 1: // Unsummoned
					{
						InfoManager.OnPetUnsummoned(cos);
					}
					break;
				case 3: // Exp
					// long ExpReceived = packet.ReadLong();
					// uint sourceUniqueID = packet.ReadUInt();
					// Possible parsing error here, also it's not important to track the %exp on pet yet.
					// Bot.Get._OnPetExpReceived(ref pet, ExpReceived, (long)((ulong)pet[SRProperty.Exp]), (long)((ulong)pet[SRProperty.ExpMax]), (byte)pet[SRProperty.Level]);
					break;
				case 4: // Hungry
					{
						((SRAttackPet)cos).HGP = packet.ReadUShort();
					}
					break;
				case 7: // Model changed on level up
					{
						SRModel newModel = new SRModel(packet.ReadUInt());
						
						SRAttackPet pet = (SRAttackPet)cos;
						pet.ID = newModel.ID;
						pet.HP = pet.HPMax = newModel.HPMax;
					}
				break;
			}
		}
		public static void PetSettingsChangeResponse(Packet packet)
		{
			// Success
			if(packet.ReadBool())
			{
				uint uniqueID = packet.ReadUInt();
				byte settingsType = packet.ReadByte();
				
				SREntity cos = InfoManager.GetEntity(uniqueID);
				switch (settingsType)
				{
					case 1: // Pet Attack settings
						((SRAttackPet)cos).SettingsFlags = (SRAttackPet.Settings)packet.ReadUInt();
						break;
				}
			}
		}
		public static void PetPlayerMounted(Packet packet)
		{
			// success
			if(packet.ReadBool())
			{
				SRPlayer player = (SRPlayer)InfoManager.GetEntity(packet.ReadUInt());
				bool isMounting = packet.ReadBool();
				if (isMounting)
					player.RidingUniqueID = packet.ReadUInt();
				else
					player.RidingUniqueID = 0;
			}
		}
		public static void StallCreateResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
				InfoManager.OnStallOpened();
		}
		public static void StallDestroyResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
				InfoManager.OnStallClosed();
		}
		public static void StallTalkResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				// identification
				SRPlayer player = (SRPlayer)InfoManager.GetEntity(packet.ReadUInt());
				string note = packet.ReadAscii();
				bool isOpen = packet.ReadBool();
				byte mode = packet.ReadByte();
				// items
				xList<SRItemStall> inventoryStall = new xList<SRItemStall>(10);
				byte slotStall;
				while( (slotStall = packet.ReadByte()) != byte.MaxValue){
					SRItemStall item = new SRItemStall();
					item.Item = ItemParsing(packet);
					item.SlotInventory = packet.ReadByte();
					item.Item.Quantity = packet.ReadUShort();
					item.Price = packet.ReadULong();
					inventoryStall[slotStall] = item;
				}
				player.Stall.Inventory = inventoryStall;
				// player.Stall.StallViewersID = packet.ReadUIntArray(packet.ReadByte());
				// End of Packet


				InfoManager.OnStallOpened(player);
				if (isOpen)
					InfoManager.OnStallStateUpdate(isOpen);
				InfoManager.OnStallNoteUpdate(note);
			}
		}
		public static void StallBuyResponse(Packet packet)
		{
			//// success
			//if (packet.ReadBool())
			//{
			//	byte slotStall = packet.ReadByte();
			//	// End of Packet
			//}
		}
		public static void StallLeaveResponse(Packet packet)
		{
			InfoManager.OnStallClosed();
		}
		public static void StalleEntityAction(Packet packet)
		{
			byte stallAction = packet.ReadByte();
			switch (stallAction)
			{
				case 1: // exit
					{
						// uint uniqueID = packet.ReadUInt();
						InfoManager.OnStallViewer(false);
					}
					break;
				case 2: // enter
					{
						// uint uniqueID = packet.ReadUInt();
						InfoManager.OnStallViewer(true);
					}
					break;
				case 3: // buy
					{
						byte slotItemBought = packet.ReadByte();
						string name = packet.ReadAscii();
						// items
						xList<SRItemStall> inventoryStall = new xList<SRItemStall>(10);
						byte slotStall;
						while ((slotStall = packet.ReadByte()) != byte.MaxValue)
						{
							SRItemStall item = new SRItemStall();
							item.Item = ItemParsing(packet);
							item.SlotInventory = packet.ReadByte();
							item.Item.Quantity = packet.ReadUShort();
							item.Price = packet.ReadULong();
							inventoryStall[slotStall] = item;
						}
						InfoManager.OnStallBuy(slotItemBought, name);
						InfoManager.OnStallUpdate(inventoryStall);
					}
					break;
			}
		}
		public static void StallUpdateResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				SRTypes.StallUpdate type = (SRTypes.StallUpdate)packet.ReadByte();
				switch (type)
				{
					case SRTypes.StallUpdate.ItemUpdate:
						{
							byte slotStall = packet.ReadByte();
							ushort quantity = packet.ReadUShort();
							ulong price = packet.ReadULong();
							// ushort errCode = packet.ReadUShort();
							InfoManager.OnStallItemUpdate(slotStall, quantity, price);
						}
						break;
					case SRTypes.StallUpdate.ItemRemoved:
					case SRTypes.StallUpdate.ItemAdded:
						{
							ushort errorCode = packet.ReadUShort();
							// items
							xList<SRItemStall> inventoryStall = new xList<SRItemStall>(10);
							byte slotStall;
							while ((slotStall = packet.ReadByte()) != byte.MaxValue)
							{
								SRItemStall item = new SRItemStall();
								item.Item = ItemParsing(packet);
								item.SlotInventory = packet.ReadByte();
								item.Item.Quantity = packet.ReadUShort();
								item.Price = packet.ReadULong();
								inventoryStall[slotStall] = item;
							}
							InfoManager.OnStallUpdate(inventoryStall);
						}
						break;
					case SRTypes.StallUpdate.State:
						InfoManager.OnStallStateUpdate(packet.ReadBool());
						break;
					case SRTypes.StallUpdate.Note:
						InfoManager.OnStallNoteUpdate(packet.ReadAscii());
						break;
					case SRTypes.StallUpdate.Title:
						InfoManager.OnStallNoteUpdate();
						break;
				}
			}
		}
		public static void EntityStallCreate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			SRStall stall = new SRStall();
			stall.Title = packet.ReadAscii();
			stall.DecorationID = packet.ReadUInt();
			// End of Packet

			SRPlayer player = (SRPlayer)InfoManager.GetEntity(uniqueID);
			player.Stall = stall;
			player.InteractionType = SRPlayer.Interaction.OnStall;
		}
		public static void EntityStallDestroy(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			//ushort unkUshort01 = packet.ReadUShort();

			SRPlayer player = (SRPlayer)InfoManager.GetEntity(uniqueID);
			player.InteractionType = SRPlayer.Interaction.None;

			if (InfoManager.inStall && InfoManager.StallerPlayer == player)
				InfoManager.OnStallClosed();
		}
		public static void EntityStallTitleUpdate(Packet packet)
		{
			SRPlayer player = (SRPlayer)InfoManager.GetEntity(packet.ReadUInt());
			player.Stall.Title = packet.ReadAscii();
		}
		public static void EntitySkillStart(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				SRTypes.SkillCast type = (SRTypes.SkillCast)packet.ReadByte();
				byte unkByte01 = packet.ReadByte();
				uint skillID = packet.ReadUInt();
				uint sourceUniqueID = packet.ReadUInt();
				uint skillUniqueID = packet.ReadUInt();
				uint targetUniqueID = packet.ReadUInt();
				if(type == SRTypes.SkillCast.Attack)
					SkillDamageParsing(packet);
				// End of Packet
				InfoManager.OnEntitySkillCast(type, skillID, sourceUniqueID, targetUniqueID);
			}
		}
		public static void EntitySkillEnd(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint skillUniqueID = packet.ReadUInt();
				uint targetUniqueID = packet.ReadUInt();
				SkillDamageParsing(packet);
			}
		}
		private static void SkillDamageParsing(Packet p){
			bool hasDamage = p.ReadBool();
			if (hasDamage)
			{
				byte hitCount = p.ReadByte();
				byte targetCount = p.ReadByte();
				// AOE
				for (byte j = 0; j < targetCount; j++)
				{
					uint targetUniqueID = p.ReadUInt();
					byte dmgEffect = p.ReadByte();
					// Since there it's not enough flags to check, then this way have to be used
					if (dmgEffect == 128)
						InfoManager.OnEntityDead(targetUniqueID);
					else if (dmgEffect.HasFlags((byte)(SRTypes.DamageEffect.Block | SRTypes.DamageEffect.Cancel)))
						continue;
					SRTypes.Damage dmgState = (SRTypes.Damage)p.ReadByte();
					uint dmgValue = p.ReadUInt();
					byte unkByte01 = p.ReadByte();
					byte unkByte02 = p.ReadByte();
					byte unkByte03 = p.ReadByte();
				}
			}
		}
		public static void EntitySkillBuffAdded(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			SRBuff buff = new SRBuff(packet.ReadUInt());
			buff.UniqueID = packet.ReadUInt();
			// End of Packet

			InfoManager.OnEntityBuffAdded(uniqueID, buff);
		}
		public static void EntitySkillBuffRemoved(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint buffUniqueID = packet.ReadUInt();
				// End of Packet
				InfoManager.OnEntityBuffRemoved(buffUniqueID);
			}
		}
		public static void MasterySkillLevelUpResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint newSkillID = packet.ReadUInt();
				// End of Packet
				InfoManager.OnSkillLevelUp(newSkillID);
			}
		}
		public static void MasterySkillLevelDownResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint newSkillID = packet.ReadUInt();
				InfoManager.OnSkillLevelDown(newSkillID);
			}
		}
		public static void MasteryLevelUpResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint masteryID = packet.ReadUInt();

				SRMastery mastery = InfoManager.Character.Masteries[masteryID];
				mastery.Level = packet.ReadByte();
			}
		}
		public static void MasteryLevelDownResponse(Packet packet)
		{
			// success
			if (packet.ReadBool())
			{
				uint masteryID = packet.ReadUInt();

				SRMastery mastery = InfoManager.Character.Masteries[masteryID];
				mastery.Level = packet.ReadByte();
			}
		}
		public static void EntitySpeedUpdate(Packet packet)
		{
			SRModel entity = (SRModel)InfoManager.GetEntity(packet.ReadUInt());
			entity.GetRealtimePosition(); // Force update the current position
			entity.SpeedWalking = packet.ReadFloat();
			entity.SpeedRunning = packet.ReadFloat();
		}
		public static void EntityStateUpdate(Packet packet)
		{
			SRModel entity = (SRModel)InfoManager.GetEntity(packet.ReadUInt());
			byte updateType = packet.ReadByte();
			byte updateState = packet.ReadByte();
			switch (updateType)
			{
				case 0: // LifeState
					entity.LifeStateType = (SRModel.LifeState)updateState;
					break;
				case 1: // MotionState
					entity.GetRealtimePosition(); // Force update the position
					entity.MotionStateType = (SRModel.MotionState)updateState;
					switch (entity.MotionStateType)
					{
						case SRModel.MotionState.Running:
							entity.MovementSpeedType = SRModel.MovementSpeed.Running;
							break;
						case SRModel.MotionState.Walking:
							entity.MovementSpeedType = SRModel.MovementSpeed.Walking;
							break;
					}
					break;
				case 4:
					entity.GameStateType = (SRModel.GameState)updateState;
					break;
				case 7:
					((SRPlayer)entity).PVPStateType = (SRPlayer.PVPState)updateState;
					break;
				case 8:
					((SRPlayer)entity).inCombat = updateState == 1;
					break;
				case 11:
					((SRPlayer)entity).ScrollingType = (SRPlayer.Scrolling)updateState;
					break;
			}
		}
		public static void EntityTalkResponse(Packet packet)
		{
			// success
			if(packet.ReadBool())
			{
				byte talkID = packet.ReadByte();
				InfoManager.OnTalkNpc(talkID);
			}
		}
		public static void NpcCloseResponse(Packet packet)
		{
			// success
			if(packet.ReadBool())
			{
				InfoManager.OnTalkClose();
			}
		}
	}
}