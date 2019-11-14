using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using xBot.App;
using xBot.Game.Objects;
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
			Info i = Info.Get;
			i.ServerID = "";
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
				WinAPI.InvokeIfRequired(w.Login_lstvServers, () => {
					//i.Group = w.Login_lstvServers.Groups[serverID_farmID.ToString()];
					w.Login_lstvServers.Items.Add(server);
				});
				if (isAvailable)
				{
					WinAPI.InvokeIfRequired(w.Login_cmbxServer, () => {
						w.Login_cmbxServer.Items.Add(serverName);
						// Select Server if is AutoLogin
						if (Bot.Get.hasAutoLoginMode
						&& w.Login_cmbxServer.Tag != null
						&& serverName.Equals((string)w.Login_cmbxServer.Tag, StringComparison.OrdinalIgnoreCase))
						{
							w.Login_cmbxServer.Text = serverName;
							i.ServerID = serverID.ToString();
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
				if (i.ServerID != "") {
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
		public static void CharacterSelectionActionResponse(Packet packet)
		{
			byte action = packet.ReadByte();
			byte result = packet.ReadByte();

			switch ((Types.CharacterSelectionAction)action)
			{
				case Types.CharacterSelectionAction.Create:
					if (result == 1)
						Window.Get.Log("Character created successfully");
					else
						Window.Get.Log("Character creation failed!");
					if (Bot.Get.Proxy.ClientlessMode)
						PacketBuilder.RequestCharacterList();
					break;
				case Types.CharacterSelectionAction.CheckName:
					Bot.Get._OnNicknameChecked(result == 1);
					break;
				case Types.CharacterSelectionAction.Delete:
					// Not necessary at the moment..
					break;
				case Types.CharacterSelectionAction.List:
					if (result == 1)
					{
						Window w = Window.Get;
						Bot b = Bot.Get;
						Info i = Info.Get;
						// Reset values
						WinAPI.InvokeIfRequired(w.Login_lstvCharacters, () =>
						{
							w.Login_lstvCharacters.Items.Clear();
						});
						WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () =>
						{
							w.Login_cmbxCharacter.Items.Clear();
						});
						// Get character selection
						List<SRObject> CharacterList = new List<SRObject>(packet.ReadByte());
						for (byte n = 0; n < CharacterList.Capacity; n++)
						{
							SRObject character = new SRObject(packet.ReadUInt(),SRType.Model);
							character.Name = packet.ReadAscii();
							character[SRProperty.Scale] = packet.ReadByte();
							character[SRProperty.Level] = packet.ReadByte();
							character[SRProperty.Exp] = packet.ReadULong();
							character[SRProperty.ExpMax] = i.GetExpMax((byte)character[SRProperty.Level]);
							character[SRProperty.STR] = packet.ReadUShort();
							character[SRProperty.INT] = packet.ReadUShort();
							character[SRProperty.StatPoints] = packet.ReadUShort();
							character[SRProperty.HP] = packet.ReadUInt();
							character[SRProperty.MP] = packet.ReadUInt();
							character[SRProperty.isDeleting] = packet.ReadByte() == 1;
							if ((bool)character[SRProperty.isDeleting])
							{
								character[SRProperty.DeletingDate] = DateTime.Now.AddMinutes(packet.ReadUInt());
							}
							character[SRProperty.GuildMemberType] = (Types.GuildMember)packet.ReadByte();
							character[SRProperty.isGuildRenameRequired] = packet.ReadByte() == 1;
							if ((bool)character[SRProperty.isGuildRenameRequired])
							{
								character[SRProperty.GuildName] = packet.ReadAscii();
							}
							character[SRProperty.AcademyMemberType] = (Types.AcademyMember)packet.ReadByte();

							SRObjectCollection Inventory = new SRObjectCollection(packet.ReadByte());
							for (byte j = 0; j < Inventory.Capacity; j++)
							{
								SRObject item = new SRObject(packet.ReadUInt(),SRType.Item);
								item[SRProperty.Plus] = packet.ReadByte();

								Inventory[j] = item;
							}
							character[SRProperty.Inventory] = Inventory;

							SRObjectCollection InventoryAvatar = new SRObjectCollection(packet.ReadByte());
							for (byte j = 0; j < InventoryAvatar.Capacity; j++)
							{
								SRObject item = new SRObject(packet.ReadUInt(), SRType.Item);
								item[SRProperty.Plus] = packet.ReadByte();

								InventoryAvatar[j] = item;
							}
							character[SRProperty.InventoryAvatar] = InventoryAvatar;

							// Adding character
							CharacterList.Add(character);
						}
						// End of Packet

						Bot.Get._OnCharacterListing(CharacterList);
					}
					else if (result == 2)
					{
						ushort error = packet.ReadUShort();
						Window.Get.Log("Error [" + error + "]");
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
				w.Log("Character selected [" + Info.Get.Charname + "]");
			}
			else
			{
				ushort error = packet.ReadUShort();
				w.Log("Error: " + error);
				w.LogProcess("Error", Window.ProcessState.Error);
			}
		}
		private static Packet characterDataPacket;
		public static void CharacterDataBegin(Packet packet)
		{
			characterDataPacket = new Packet(Agent.Opcode.SERVER_CHARACTER_DATA);
			Bot.Get._OnTeleporting();
		}
		public static void CharacterData(Packet packet)
		{
			characterDataPacket.WriteByteArray(packet.GetBytes());
		}
		public static void CharacterDataEnd(Packet packet)
		{
			Packet p = characterDataPacket;
			p.Lock();

			Info i = Info.Get;
			i.ServerTime = p.ReadUInt();

			// Set the current character or update the previously saved
			if (i.Character == null)
				i.Character = new SRObject(p.ReadUInt(),SRType.Model);
			else
				i.Character.LoadDefaultProperties(p.ReadUInt(),SRType.Model);
			SRObject character = i.Character;

			character[SRProperty.Scale] = p.ReadByte();
			character[SRProperty.Level] = p.ReadByte();
			character[SRProperty.LevelMax] = p.ReadByte();
			character[SRProperty.Exp] = p.ReadULong();
			character[SRProperty.SPExp] = p.ReadUInt();
			character[SRProperty.Gold] = p.ReadULong();
			character[SRProperty.SP] = p.ReadUInt();
			character[SRProperty.StatPoints] = p.ReadUShort();
			character[SRProperty.BerserkPoints] = p.ReadByte();
			character[SRProperty.GatheredExpPoint] = p.ReadUInt();
			character[SRProperty.HPMax] = p.ReadUInt();
			character[SRProperty.MPMax] = p.ReadUInt();
			character[SRProperty.ExpIconType] = (Types.ExpIcon)p.ReadByte();
			character[SRProperty.PKDaily] = p.ReadByte();
			character[SRProperty.PKTotal] = p.ReadUShort();
			character[SRProperty.PKPenalty] = p.ReadUInt();
			character[SRProperty.BerserkLevel] = p.ReadByte();
			character[SRProperty.PVPCapeType] = (Types.PVPCape)p.ReadByte();
			#region (Inventory)
			SRObjectCollection Inventory = new SRObjectCollection(p.ReadByte());
			byte itemsCount = p.ReadByte();
			for (byte j = 0; j < itemsCount; j++)
			{
				byte slot = p.ReadByte();
				Inventory[slot] = ItemParsing(p);
			}
			character[SRProperty.Inventory] = Inventory;
			#endregion
			#region (Inventory Avatar)
			SRObjectCollection InventoryAvatar = new SRObjectCollection(p.ReadByte());
			itemsCount = p.ReadByte();
			for (byte j = 0; j < itemsCount; j++)
			{
				byte slot = p.ReadByte();
				InventoryAvatar[slot] = ItemParsing(p);
			}
			character[SRProperty.InventoryAvatar] = InventoryAvatar;
			#endregion
			character[SRProperty.unkByte01] = p.ReadByte();
			#region (Masteries)
			SRObjectCollection Masteries = new SRObjectCollection();
			while (p.ReadByte() == 1)
			{
				SRObject mastery = new SRObject(p.ReadUInt(),SRType.Mastery);
				mastery[SRProperty.Level] = p.ReadByte();

				Masteries.Add(mastery);
			}
			character[SRProperty.Masteries] = Masteries;
			#endregion
			character[SRProperty.unkByte02] = p.ReadByte();
			#region (Skills)
			SRObjectDictionary<uint> Skills = new SRObjectDictionary<uint>();
			while (p.ReadByte() == 1)
			{
				SRObject skill = new SRObject(p.ReadUInt(),SRType.Skill);
				skill[SRProperty.isEnabled] = p.ReadByte() == 1;

				Skills[skill.ID] = skill;
			}
			character[SRProperty.Skills] = Skills;
			#endregion
			#region (Quest)
			SRObjectCollection QuestsCompleted = new SRObjectCollection();
			ushort questCount = p.ReadUShort();
			for (ushort j = 0; j < questCount; j++)
			{
				SRObject quest = new SRObject(p.ReadUInt(), SRType.Quest);

				QuestsCompleted.Add(quest);
			}
			character[SRProperty.QuestsCompleted] = QuestsCompleted;

			SRObjectCollection Quests = new SRObjectCollection();
			questCount = p.ReadByte();
			for (ushort j = 0; j < questCount; j++)
			{
				SRObject quest = new SRObject(p.ReadUInt(),SRType.Quest);

				quest[SRProperty.Achievements] = p.ReadByte();
				quest[SRProperty.isAutoShareRequired] = p.ReadByte() == 1;
				quest[SRProperty.QuestType] = p.ReadByte();
				if ((byte)quest[SRProperty.QuestType] == 28)
				{
					quest[SRProperty.TimeRemain] = p.ReadUInt();
				}
				quest[SRProperty.isEnabled] = p.ReadByte() == 1;
				if ((byte)quest[SRProperty.QuestType] != 8)
				{
					SRObjectCollection Objectives = new SRObjectCollection();
					byte objectiveCount = p.ReadByte();
					for (byte k = 0; k < objectiveCount; k++)
					{
						SRObject objective = new SRObject(p.ReadByte(),SRType.Objective);
						objective[SRProperty.isEnabled] = p.ReadByte() == 1;
						objective.Name = p.ReadAscii();
						objective[SRProperty.TaskID] = p.ReadUIntArray(p.ReadByte());

						Objectives.Add(objective);
					}
					quest[SRProperty.Objectives] = Objectives;
				}
				if ((byte)quest[SRProperty.QuestType] == 88)
				{
					quest[SRProperty.NPCModelID] = p.ReadUIntArray(p.ReadByte());
				}
				Quests.Add(quest);
			}
			character[SRProperty.Quests] = Quests;
			#endregion
			character[SRProperty.unkByte03] = p.ReadByte();
			#region (Collection Books)
			SRObjectCollection CollectionBooks = new SRObjectCollection();
			uint bookCount = p.ReadUInt();
			for (int j = 0; j < bookCount; j++)
			{
				SRObject book = new SRObject(p.ReadUInt(),SRType.Book);
				book[SRProperty.StartedDatetime] = p.ReadUInt();
				book[SRProperty.Pages] = p.ReadUInt();

				CollectionBooks.Add(book);
			}
			character[SRProperty.CollectionBooks] = CollectionBooks;
			#endregion
			#region (Game Position)
			character[SRProperty.UniqueID] = p.ReadUInt();
			character[SRProperty.Position] = new SRCoord(p.ReadUShort(), (int)p.ReadFloat(), (int)p.ReadFloat(), (int)p.ReadFloat());
			character[SRProperty.Angle] = p.ReadUShort();
			bool hasMovement = p.ReadByte() == 1;
			character[SRProperty.MovementSpeedType] =(Types.MovementSpeed)p.ReadByte();
			if (hasMovement)
			{
				if (character.inDungeon())
					character[SRProperty.MovementPosition] = new SRCoord(p.ReadUShort(), p.ReadInt(), p.ReadInt(), p.ReadInt());
				else
					character[SRProperty.MovementPosition] = new SRCoord(p.ReadUShort(),(int)p.ReadUShort(), (int)p.ReadUShort(), (int)p.ReadUShort());
			}
			else
			{
				character[SRProperty.MovementActionType] = (Types.MovementAction)p.ReadByte();
				character[SRProperty.Angle] = p.ReadUShort();
				// Update movement position
				character[SRProperty.MovementPosition] = character[SRProperty.Position];
			}
			character[SRProperty.LastUpdateTime] = Stopwatch.StartNew();
			#endregion
			character[SRProperty.LifeState] = (Types.LifeState)p.ReadByte();
			character[SRProperty.unkByte04] = p.ReadByte();
			character[SRProperty.MotionStateType] = (Types.MotionState)p.ReadByte();
			character[SRProperty.PlayerStateType] = (Types.PlayerState)p.ReadByte();
			character[SRProperty.SpeedWalking] = p.ReadFloat();
			character[SRProperty.SpeedRunning] = p.ReadFloat();
			character[SRProperty.SpeedBerserk] = p.ReadFloat();
			#region (Buffs)
			SRObjectDictionary<uint> Buffs = new SRObjectDictionary<uint>();
			byte buffCount = p.ReadByte();
			for (byte j = 0; j < buffCount; j++)
			{
				SRObject buff = new SRObject(p.ReadUInt(),SRType.Skill);
				uint buffUniqueID = p.ReadUInt();
				buff[SRProperty.UniqueID] = buffUniqueID;
				if (buff.hasAutoTransferEffect())
				{
					buff[SRProperty.isOwner] = p.ReadByte() == 1;
				}
				// Easy tracking
				buff[SRProperty.OwnerUniqueID] = character[SRProperty.UniqueID];
				Buffs[buffUniqueID] = buff;
			}
			character[SRProperty.Buffs] = Buffs;
			#endregion
			character.Name = p.ReadAscii();
			#region (Job & PVP)
			character[SRProperty.JobName] = p.ReadAscii();
			character[SRProperty.JobType] = (Types.Job)p.ReadByte();
			character[SRProperty.JobLevel] = p.ReadByte();
			character[SRProperty.JobExp] = p.ReadUInt();
			character[SRProperty.JobContribution] = p.ReadUInt();
			character[SRProperty.JobReward] = p.ReadUInt();
			character[SRProperty.PVPStateType] = (Types.PVPState)p.ReadByte();
			character[SRProperty.isRiding] = p.ReadByte() == 1;
			character[SRProperty.inCombat] = p.ReadByte() == 1;
			if ((bool)character[SRProperty.isRiding])
			{
				character[SRProperty.RidingUniqueID] = p.ReadUInt();
			}
			character[SRProperty.CaptureTheFlagType] = (Types.CaptureTheFlag)p.ReadByte();
			character[SRProperty.GuideFlag] = p.ReadULong();
			character[SRProperty.JoinID] = p.ReadUInt();
			character[SRProperty.isGameMaster] = p.ReadByte() == 1;
			#endregion

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

			Bot.Get._OnCharacterSpawn();
		}
		private static SRObject ItemParsing(Packet p)
		{
			SRObject item = new SRObject();
			item[SRProperty.RentType] = p.ReadUInt();
			if ((uint)item[SRProperty.RentType] == 1)
			{
				item[SRProperty.RentCanDelete] = p.ReadUShort();
				item[SRProperty.RentPeriodBeginTime] = p.ReadUInt();
				item[SRProperty.RentPeriodEndTime] = p.ReadUInt();
			}
			else if ((uint)item[SRProperty.RentType] == 2)
			{
				item[SRProperty.RentCanDelete] = p.ReadUShort();
				item[SRProperty.RentCanRecharge] = p.ReadUShort();
				item[SRProperty.RentMeterRateTime] = p.ReadUInt();
			}
			else if ((uint)item[SRProperty.RentType] == 3)
			{
				item[SRProperty.RentCanDelete] = p.ReadUShort();
				item[SRProperty.RentCanRecharge] = p.ReadUShort();
				item[SRProperty.RentPeriodBeginTime] = p.ReadUInt();
				item[SRProperty.RentPeriodEndTime] = p.ReadUInt();
				item[SRProperty.RentPackingTime] = p.ReadUInt();
			}
			item.LoadDefaultProperties(p.ReadUInt(), SRType.Item);
			if (item.ID1 == 3)
			{
				// ITEM_
				if (item.ID2 == 1)
				{
					// ITEM_CH_
					// ITEM_EU_
					// ITEM_AVATAR_
					item[SRProperty.Plus] = p.ReadByte();
					item[SRProperty.Variance] = p.ReadULong();
					item[SRProperty.Durability] = p.ReadUInt();

					SRObjectCollection MagicParams = new SRObjectCollection();
					byte paramCount = p.ReadByte();
					for (byte j = 0; j < paramCount; j++)
					{
						SRObject param = new SRObject(p.ReadUInt(),SRType.Param);
						param[SRProperty.Value] = p.ReadUInt();

						MagicParams.Add(param);
					}
					item[SRProperty.MagicParams] = MagicParams;

					// 1 = Socket
					p.ReadByte();
					SRObjectCollection SocketParams = new SRObjectCollection();
					paramCount = p.ReadByte();
					for (byte j = 0; j < paramCount; j++)
					{
						byte slot = p.ReadByte();
						SRObject param = new SRObject(p.ReadUInt(), SRType.Param);
						param[SRProperty.Value] = p.ReadUInt();
						param[SRProperty.Slot] = slot;

						SocketParams.Add(param);
					}
					item[SRProperty.SocketParams] = SocketParams;

					// 2 = Advanced elixir
					p.ReadByte();
					SRObjectCollection AdvanceElixirParams = new SRObjectCollection();
					paramCount = p.ReadByte();
					for (byte j = 0; j < paramCount; j++)
					{
						byte slot = p.ReadByte();
						SRObject param = new SRObject(p.ReadUInt(), SRType.Param);
						param[SRProperty.Value] = p.ReadUInt();
						param[SRProperty.Slot] = slot;

						AdvanceElixirParams.Add(param);
					}
					item[SRProperty.AdvanceElixirParams] = AdvanceElixirParams;
				}
				else if (item.ID2 == 2)
				{
					// ITEM_COS
					if (item.ID3 == 1)
					{
						// ITEM_COS_P
						item[SRProperty.PetState] = (Types.PetState)p.ReadByte();
						switch ((Types.PetState)item[SRProperty.PetState])
						{
							case Types.PetState.Summoned:
							case Types.PetState.Unsummoned:
							case Types.PetState.Dead:
								item[SRProperty.PetModelID] = p.ReadUInt();
								item[SRProperty.PetName] = p.ReadAscii();
								if (item.ID4 == 2)
								{
									// ITEM_COS_P (Ability)
									item[SRProperty.RentPeriodEndTime] = p.ReadUInt();
								}
								item[SRProperty.unkByte01] = p.ReadByte();
								break;
						}
					}
					else if (item.ID3 == 2)
					{
						// ITEM_ETC_TRANS_MONSTER
						item[SRProperty.PetModelID] = p.ReadUInt();
					}
					else if (item.ID3 == 3)
					{
						// MAGIC_CUBE
						item[SRProperty.Amount] = p.ReadUInt();
					}
				}
				else if (item.ID2 == 3)
				{
					// ITEM_ETC
					item[SRProperty.Quantity] = p.ReadUShort();
					if (item.ID3 == 11)
					{
						if (item.ID4 == 1 || item.ID4 == 2)
						{
							// MAGIC/ATRIBUTTE STONE
							item[SRProperty.AssimilationProbability] = p.ReadByte();
						}
					}
					else if (item.ID3 == 14 && item.ID4 == 2)
					{
						// ITEM_MALL_GACHA_CARD_WIN
						// ITEM_MALL_GACHA_CARD_LOSE
						SRObjectCollection MagicParams = new SRObjectCollection();
						byte paramCount = p.ReadByte();
						for (byte j = 0; j < paramCount; j++)
						{
							SRObject param = new SRObject(p.ReadUInt(), SRType.Param);
							param[SRProperty.Value] = p.ReadUInt();

							MagicParams.Add(param);
						}
						item[SRProperty.MagicParams] = MagicParams;
					}
				}
			}
			return item;
		}
		public static void CharacterStatsUpdate(Packet packet)
		{
			Info i = Info.Get;

			i.Character[SRProperty.PhyAtkMin] = packet.ReadUInt();
			i.Character[SRProperty.PhyAtkMax] = packet.ReadUInt();
			i.Character[SRProperty.MagAtkMin] = packet.ReadUInt();
			i.Character[SRProperty.MagAtkMax] = packet.ReadUInt();
			i.Character[SRProperty.PhyDefense] = packet.ReadUShort();
			i.Character[SRProperty.MagDefense] = packet.ReadUShort();
			i.Character[SRProperty.HitRate] = packet.ReadUShort();
			i.Character[SRProperty.ParryRatio] = packet.ReadUShort();
			i.Character[SRProperty.HPMax] = packet.ReadUInt();
			i.Character[SRProperty.MPMax] = packet.ReadUInt();
			i.Character[SRProperty.STR] = packet.ReadUShort();
			i.Character[SRProperty.INT] = packet.ReadUShort();
			// End of Packet
			Bot.Get._OnCharacterStatsUpdated();
    }
		public static void CharacterExperienceUpdate(Packet packet)
		{
			Info i = Info.Get;
			Window w = Window.Get;

			uint sourceUniqueID = packet.ReadUInt(); // used to display exp. graphics
			long ExpReceived = packet.ReadLong();
			long SPExpReceived = packet.ReadLong(); // Long SP EXP? hmmm..
			//byte unkByte01 = packet.ReadByte();
			// End of Packet

			if (w.Character_cbxMessageExp.Checked)
			{
				if (ExpReceived > 0)
				{
					w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_STATE_GAIN_EXP", ExpReceived));
				}
				else if (ExpReceived < 0)
				{
					w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_STATE_LOSE_EXP", ExpReceived));
				}
				if (SPExpReceived > 0)
				{
					w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_STATE_GET_SKILL_EXP", SPExpReceived));
				}
			}
			Bot.Get._OnExpReceived(ExpReceived, (long)((ulong)i.Character[SRProperty.Exp]), (long)((ulong)i.Character[SRProperty.ExpMax]),(byte)i.Character[SRProperty.Level]);
		}
		public static void CharacterInfoUpdate(Packet packet)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			byte updateType = packet.ReadByte();
			switch (updateType)
			{
				case 1: // Gold
					i.Character[SRProperty.Gold] = packet.ReadULong();
					w.Character_SetGold((ulong)i.Character[SRProperty.Gold]);
					break;
				case 2: // SP
					i.Character[SRProperty.SP] = packet.ReadUInt();
					WinAPI.InvokeIfRequired(w.Character_lblSP, () => {
						w.Character_lblSP.Text = i.Character[SRProperty.SP].ToString();
					});
					break;
				case 4: // Berserk
					i.Character[SRProperty.BerserkPoints] = packet.ReadByte();
					break;
			}
		}
		public static void CharacterDied(Packet packet)
		{
			// byte unkByte01 = packet.ReadByte();
			// Probably dead cause? 4 = Dead by mob?
		}
		private static byte groupSpawnType;
		private static ushort groupSpawnCount;
		private static Packet groupSpawnPacket;
		public static void EntityGroupSpawnBegin(Packet packet)
		{
			groupSpawnType = packet.ReadByte();
			groupSpawnCount = packet.ReadUShort();
			groupSpawnPacket = new Packet(Agent.Opcode.SERVER_ENTITY_GROUPSPAWN_DATA);
		}
		public static void EntityGroupSpawnData(Packet packet)
		{
			groupSpawnPacket.WriteByteArray(packet.GetBytes());
		}
		public static void EntityGroupSpawnEnd(Packet packet)
		{
			groupSpawnPacket.Lock();
			for (int i = 0; i < groupSpawnCount; i++)
			{
				if (groupSpawnType == 1)
				{
					EntitySpawn(groupSpawnPacket);
				}
				else
				{
					EntityDespawn(groupSpawnPacket);
				}
			}
		}
		public static void EntitySpawn(Packet packet)
		{
			try
			{
				SRObject entity = new SRObject(packet.ReadUInt(), SRType.Entity);
				if (entity.ID1 == 1)
				{
					// BIONIC:
					// - CHARACTER
					// - NPC
					//   - NPC_FORTRESS_STRUCT
					//   - NPC_MOB
					//   - NPC_COS
					//   - NPC_FORTRESS_COS
					if (entity.ID2 == 1)
					{
						// CHARACTER
						entity[SRProperty.Scale] = packet.ReadByte();
						entity[SRProperty.BerserkLevel] = packet.ReadByte();
						entity[SRProperty.PVPCapeType] = (Types.PVPCape)packet.ReadByte();
						entity[SRProperty.ExpIconType] = (Types.ExpIcon)packet.ReadByte();
						// Inventory
						packet.ReadByte(); // max capacity. seems useless at the moment..
						SRObjectCollection inventory = new SRObjectCollection();
						byte inventoryCount = packet.ReadByte();
						for (byte i = 0; i < inventoryCount; i++)
						{
							inventory[i] = new SRObject(packet.ReadUInt(), SRType.Item);
							if (inventory[i].ID1 == 3 && inventory[i].ID2 == 1)
							{
								inventory[i][SRProperty.Plus] = packet.ReadByte();
							}
						}
						entity[SRProperty.Inventory] = inventory;
						// AvatarInventory
						SRObjectCollection inventoryAvatar = new SRObjectCollection(packet.ReadByte());
						byte inventoryAvatarCount = packet.ReadByte();
						for (byte i = 0; i < inventoryAvatarCount; i++)
						{
							inventoryAvatar[i] = new SRObject(packet.ReadUInt(), SRType.Item);
							if (inventoryAvatar[i].ID1 == 3 && inventoryAvatar[i].ID2 == 1)
							{
								inventoryAvatar[i][SRProperty.Plus] = packet.ReadByte();
							}
						}
						entity[SRProperty.InventoryAvatar] = inventoryAvatar;
						// Mask
						entity[SRProperty.hasMask] = packet.ReadByte() == 1;
						if ((bool)entity[SRProperty.hasMask])
						{
							SRObject mask = new SRObject(packet.ReadUInt(), SRType.Model);
							if (mask.ID1 == entity.ID1 && mask.ID2 == entity.ID2)
							{
								// Clone
								mask[SRProperty.Scale] = packet.ReadByte();
								SRObjectCollection maskItems = new SRObjectCollection(packet.ReadByte());
								for (int i = 0; i < maskItems.Capacity; i++)
								{
									maskItems[i] = new SRObject(packet.ReadUInt(), SRType.Item);
								}
								mask[SRProperty.MaskItems] = maskItems;
							}
							entity[SRProperty.Mask] = mask;
						}
					}
					else if (entity.ID2 == 2 && entity.ID3 == 5)
					{
						// NPC_FORTRESS_STRUCT
						entity[SRProperty.HP] = packet.ReadUInt();
						entity[SRProperty.refEventStructID] = packet.ReadUInt();
						entity[SRProperty.LifeState] = (Types.LifeState)packet.ReadUShort();
					}
					// Position
					entity[SRProperty.UniqueID] = packet.ReadUInt();
					entity[SRProperty.Position] = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
					entity[SRProperty.Angle] = packet.ReadUShort();
					// Movement
					bool hasMovement = packet.ReadByte() == 1;
					entity[SRProperty.MovementSpeedType] = (Types.MovementSpeed)packet.ReadByte();
					if (hasMovement)
					{
						if (entity.inDungeon())
							entity[SRProperty.MovementPosition] = new SRCoord(packet.ReadUShort(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt());
						else
							entity[SRProperty.MovementPosition] = new SRCoord(packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort());
					}
					else
					{
						entity[SRProperty.MovementActionType] = (Types.MovementAction)packet.ReadByte();
						entity[SRProperty.Angle] = packet.ReadUShort();
					}
					entity[SRProperty.LastUpdateTime] = Stopwatch.StartNew();
					// States
					entity[SRProperty.LifeState] = (Types.LifeState)packet.ReadByte();
					entity[SRProperty.unkByte01] = packet.ReadByte(); // Possibly bad status flag
					entity[SRProperty.MotionStateType] = (Types.MotionState)packet.ReadByte();
					entity[SRProperty.PlayerStateType] = (Types.PlayerState)packet.ReadByte();
					// Speed
					entity[SRProperty.SpeedWalking] = packet.ReadFloat();
					entity[SRProperty.SpeedRunning] = packet.ReadFloat();
					entity[SRProperty.SpeedBerserk] = packet.ReadFloat();
					// Buffs
					SRObjectDictionary<uint> Buffs = new SRObjectDictionary<uint>();
					byte buffCount = packet.ReadByte();
					for (byte j = 0; j < buffCount; j++)
					{
						SRObject buff = new SRObject(packet.ReadUInt(), SRType.Skill);
						uint buffUniqueID = packet.ReadUInt();
						buff[SRProperty.UniqueID] = buffUniqueID;
						if (buff.hasAutoTransferEffect()){
							buff[SRProperty.isOwner] = packet.ReadByte() == 1;
						}
						// Easy tracking
						buff[SRProperty.OwnerUniqueID] = entity[SRProperty.UniqueID];
						Buffs[buffUniqueID] = buff;
					}
					entity[SRProperty.Buffs] = Buffs;

					if (entity.ID3 == 1)
					{
						// MOB
						entity[SRProperty.unkByte02] = packet.ReadByte();
						entity[SRProperty.unkByte03] = packet.ReadByte();
						entity[SRProperty.unkByte04] = packet.ReadByte();
						entity[SRProperty.MobType] = (Types.Mob)packet.ReadByte();
					}
					else if (entity.ID2 == 1)
					{
						// CHARACTER
						entity.Name = packet.ReadAscii();
						entity[SRProperty.JobType] = (Types.Job)packet.ReadByte();
						entity[SRProperty.JobLevel] = packet.ReadByte();
						entity[SRProperty.PVPStateType] = (Types.PVPState)packet.ReadByte();
						entity[SRProperty.isRiding] = packet.ReadByte() == 1;
						entity[SRProperty.inCombat] = packet.ReadByte() == 1;
						if ((bool)entity[SRProperty.isRiding])
						{
							entity[SRProperty.RidingUniqueID] = packet.ReadUInt();
						}
						entity[SRProperty.ScrollMode] = (Types.ScrollMode)packet.ReadByte();
						entity[SRProperty.InteractMode] = (Types.PlayerMode)packet.ReadByte();
						entity[SRProperty.unkByte02] = packet.ReadByte();
						// Guild
						entity[SRProperty.GuildName] = packet.ReadAscii();
						if (!entity.hasJobMode())
						{
							entity[SRProperty.GuildID] = packet.ReadUInt();
							entity[SRProperty.GuildMemberName] = packet.ReadAscii();
							entity[SRProperty.GuildLastCrestRev] = packet.ReadUInt();
							entity[SRProperty.UnionID] = packet.ReadUInt();
							entity[SRProperty.UnionLastCrestRev] = packet.ReadUInt();
							entity[SRProperty.GuildisFriendly] = packet.ReadByte();
							entity[SRProperty.GuildMemberAuthorityType] = packet.ReadByte();
						}
						if ((Types.PlayerMode)entity[SRProperty.InteractMode] == Types.PlayerMode.OnStall)
						{
							entity[SRProperty.StallTitle] = packet.ReadAscii();
							entity[SRProperty.StallDecorationType] = packet.ReadUInt();
						}
						entity[SRProperty.EquipmentCooldown] = packet.ReadByte();
						entity[SRProperty.CaptureTheFlagType] = (Types.CaptureTheFlag)packet.ReadByte();
					}
					else if (entity.ID2 == 2)
					{
						// NPC
						entity[SRProperty.hasTalk] = packet.ReadByte() != 0; // Talking stack options or none (0)
						if ((bool)entity[SRProperty.hasTalk])
						{
							entity[SRProperty.TalkOptions] = packet.ReadByteArray(packet.ReadByte());
						}
						if (entity.ID3 == 1)
						{
							// NPC_MOB
							entity[SRProperty.Rarity] = packet.ReadByte();
							if (entity.ID4 == 2 || entity.ID4 == 4)
							{
								// has multiple appearances (Selected by server)
								entity[SRProperty.Appearance] = packet.ReadByte();
							}
						}
						if (entity.ID3 == 3)
						{
							// NPC_COS
							if (entity.ID4 == 3 || entity.ID4 == 4)
							{
								//NPC_COS_P (Growth / Ability)
								entity[SRProperty.PetName] = packet.ReadAscii();
							}
							if (entity.ID4 == 5)
							{
								// NPC_COS_GUILD
								entity[SRProperty.GuildName] = packet.ReadAscii();
							}
							else if (entity.ID4 != 1)
							{
								entity[SRProperty.OwnerName] = packet.ReadAscii();
							}
							if (entity.ID4 == 2 || entity.ID4 == 3 || entity.ID4 == 4 || entity.ID4 == 5)
							{
								// NPC_COS_T
								// NPC_COS_P (Growth / Ability)
								// NPC_COS_GUILD
								entity[SRProperty.JobType] = (Types.Job)packet.ReadByte();
								if (entity.ID4 != 4)
								{
									// Not pet pick (Ability)
									entity[SRProperty.PVPStateType] = (Types.PVPState)packet.ReadByte();
								}
								if (entity.ID4 == 5)
								{
									// NPC_COS_GUILD
									entity[SRProperty.OwnerRefObjID] = packet.ReadUInt();
								}
							}
							if (entity.ID4 != 1)
							{
								entity[SRProperty.OwnerUniqueID] = packet.ReadUInt();
							}
						}
						else if (entity.ID3 == 4)
						{
							// NPC_FORTRESS_COS
							entity[SRProperty.GuildID] = packet.ReadUInt();
							entity[SRProperty.GuildName] = packet.ReadAscii();
						}
					}
				}
				else if (entity.ID1 == 3)
				{
					// ITEM_
					// - ITEM_EQUIP
					// - ITEM_ETC
					//   - ITEM_ETC_MONEY_GOLD
					//   - ITEM_ETC_TRADE
					//   - ITEM_ETC_QUEST   
					if (entity.ID2 == 1)
					{
						// ITEM_EQUIP
						entity[SRProperty.Plus] = packet.ReadByte();
					}
					else if (entity.ID2 == 3)
					{
						// ITEM_ETC
						if (entity.ID3 == 5 && entity.ID4 == 0)
						{
							// ITEM_ETC_MONEY_GOLD
							entity[SRProperty.Gold] = packet.ReadUInt();
						}
						else if (entity.ID3 == 8 || entity.ID3 == 9)
						{
							// ITEM_ETC_TRADE
							// ITEM_ETC_QUEST
							entity[SRProperty.OwnerName] = packet.ReadAscii();
						}
					}
					entity[SRProperty.UniqueID] = packet.ReadUInt();
					entity[SRProperty.Position] = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
					entity[SRProperty.Angle] = packet.ReadUShort();
					entity[SRProperty.hasOwner] = packet.ReadByte() == 1;
					if ((bool)entity[SRProperty.hasOwner])
					{
						entity[SRProperty.OwnerJoinID] = packet.ReadUInt();
					}
					entity[SRProperty.Rarity] = packet.ReadByte();
				}
				else if (entity.ID1 == 4)
				{
					// PORTALS
					// - STORE
					// - INS_TELEPORTER
					entity[SRProperty.UniqueID] = packet.ReadUInt();
					entity[SRProperty.Position] = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
					entity[SRProperty.Angle] = packet.ReadUShort();
					entity[SRProperty.unkByte01] = packet.ReadByte();
					entity[SRProperty.unkByte02] = packet.ReadByte();
					entity[SRProperty.unkByte03] = packet.ReadByte();
					entity[SRProperty.unkByte04] = packet.ReadByte();
					if ((byte)entity[SRProperty.unkByte04] == 1)
					{
						// Regular
						entity[SRProperty.unkUInt01] = packet.ReadUInt();
						entity[SRProperty.unkUInt02] = packet.ReadUInt();
					}
					else if ((byte)entity[SRProperty.unkByte04] == 6)
					{
						// Dimensional Hole
						entity[SRProperty.OwnerName] = packet.ReadAscii();
						entity[SRProperty.OwnerUniqueID] = packet.ReadUInt();
					}
					if ((byte)entity[SRProperty.unkByte02] == 1)
					{
						// STORE_OnONE_DEFAULT
						entity[SRProperty.unkUInt03] = packet.ReadUInt();
						entity[SRProperty.unkByte05] = packet.ReadByte();
					}
				}
				else if (entity.ID == uint.MaxValue)
				{
					// EVENT_ZONE (Traps, Buffzones, ...)
					entity[SRProperty.unkUShort01] = packet.ReadUShort();
					entity[SRProperty.SkillID] = packet.ReadUInt();
					entity[SRProperty.UniqueID] = packet.ReadUInt();
					entity[SRProperty.Position] = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
					entity[SRProperty.Angle] = packet.ReadUShort();

					SRObject skill = new SRObject((uint)entity[SRProperty.SkillID], SRType.Skill);
					entity.Name = skill.Name;
					entity[SRProperty.Level] = skill[SRProperty.Level];
				}
				if (packet.Opcode == Agent.Opcode.SERVER_ENTITY_SPAWN)
				{
					if (entity.ID1 == 1 || entity.ID1 == 4)
					{
						// BIONIC or STORE
						entity[SRProperty.unkByte06] = packet.ReadByte();
					}
					else if (entity.ID1 == 3)
					{
						// DROP
						entity[SRProperty.DropSource] = packet.ReadByte();
						entity[SRProperty.DropUniqueID] = packet.ReadUInt();
					}
				}
				// End of Packet

				// Keep the track of the entity
				Bot.Get._OnSpawn(ref entity);

			}
			catch (Exception ex)
			{
				Bot.Get.LogError("[Parsing Spawn Error][" + ex.Message + "]["+ex.StackTrace+"]", packet);
				throw ex;
			}
		}
		public static void EntityDespawn(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			// End of Packet

			// Keep the track of the entity
			Bot.Get._OnDespawn(uniqueID);
		}
		public static void EntityMovement(Packet packet)
		{
			SRObject entity = Info.Get.GetEntity(packet.ReadUInt());
			SRCoord currentPosition = entity.GetPosition(); // Force update the position
			bool hasMovement = packet.ReadByte() == 1;
			if (hasMovement)
			{
				SRCoord newPosition;
				if (entity.inDungeon())
					newPosition = new SRCoord(packet.ReadUShort(), packet.ReadInt(), packet.ReadInt(), packet.ReadInt());
				else
					newPosition = new SRCoord(packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort());
				entity[SRProperty.MovementPosition] = newPosition;
				// Create an angle pointing to the movement position
				if (entity == Info.Get.Character)
				{
					double xTranslate = newPosition.PosX - currentPosition.PosX;
					double yTranslate = newPosition.PosY - currentPosition.PosY;
					if (xTranslate == 0)
					{
						// 90° or 270° to SRO Angle
						entity[SRProperty.Angle] = ushort.MaxValue / 4 * (yTranslate > 0?1:3);
					}
					else
					{
						if (yTranslate == 0)
						{
							// 0° or 180° to SRO Angle
							entity[SRProperty.Angle] = (xTranslate > 0 ? 0 : ushort.MaxValue / 2);
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
							entity[SRProperty.Angle] = (ushort)Math.Round(angleRadians * ushort.MaxValue / (Math.PI * 2.0));
						}
					}
				}
			}
			bool flag = packet.ReadByte() == 1;
			if (flag)
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
					entity[SRProperty.Angle] = packet.ReadUShort();
					// short unkShort01 = packet.ReadShort();
					// short unkShort02 = packet.ReadShort();
					// short unkShort03 = packet.ReadShort();
					// short unkShort04 = packet.ReadShort();

					// Create a long sky walking movement to calculate realtime coords
					double angle = entity.GetRadianAngle();
					double MovementPosX = Math.Cos(angle) * ushort.MaxValue + currentPosition.PosX;
					double MovementPosY = Math.Sin(angle) * ushort.MaxValue + currentPosition.PosY;
					if (entity.inDungeon())
						entity[SRProperty.MovementPosition] = new SRCoord(MovementPosX, MovementPosY, currentPosition.Region, currentPosition.Z);
					else
						entity[SRProperty.MovementPosition] = new SRCoord(MovementPosX, MovementPosY);
				}
			}
			// End of Packet
			Bot.Get._OnEntityMovement(ref entity);
		}
		public static void EntityMovementStuck(Packet packet)
		{
			SRObject entity = Info.Get.GetEntity(packet.ReadUInt());
			entity[SRProperty.Position] = new SRCoord(packet.ReadUShort(), (int)packet.ReadFloat(), (int)packet.ReadFloat(), (int)packet.ReadFloat());
			entity[SRProperty.Angle] = packet.ReadUShort();
			// End of Packet
			entity[SRProperty.MovementPosition] = entity[SRProperty.Position];
			Stopwatch LastUpdateTime = (Stopwatch)entity[SRProperty.LastUpdateTime];
			LastUpdateTime.Restart();
		}
		public static void EntityMovementAngle(Packet packet)
		{
			SRObject entity = Info.Get.GetEntity(packet.ReadUInt());
			entity[SRProperty.Angle] = packet.ReadUShort();
		}
		public static void EnviromentCelestialPosition(Packet packet)
		{
			Info.Get.Character[SRProperty.UniqueID] = packet.ReadUInt();
			//ushort moonphase = packet.ReadUShort();
			//byte hour = packet.ReadByte();
			//byte minute = packet.ReadByte();
			// End of Packet
		}
		public static void ChatUpdate(Packet packet)
		{
			Types.Chat type = (Types.Chat)packet.ReadByte();
			string player = "";
			switch (type)
			{
				case Types.Chat.All:
				case Types.Chat.GM:
				case Types.Chat.NPC:
					uint uniqueID = packet.ReadUInt();
					SRObject p = Info.Get.GetEntity(uniqueID);
					if (p != null)
						player = p.Name;
					else
						player = "[UID:" + uniqueID + "]"; // Just in case
					break;
				case Types.Chat.Private:
				case Types.Chat.Party:
				case Types.Chat.Guild:
				case Types.Chat.Global:
				case Types.Chat.Stall:
				case Types.Chat.Union:
				case Types.Chat.Academy:
					player = packet.ReadAscii();
					break;
				case Types.Chat.Notice:
				default:
					player = "";
					break;
			}
			string message = packet.ReadAscii();
			// End of Packet
			Bot.Get._OnChat(type, player, message);
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
			SRObject entity = Info.Get.GetEntity(packet.ReadUInt());
			// Check if the entity has been despawned already
			if (entity == null)
				return;

			byte unkByte01 = packet.ReadByte();
			byte unkByte02 = packet.ReadByte();
			
			Types.EntityStateUpdate updateType = (Types.EntityStateUpdate)packet.ReadByte();
			switch (updateType)
			{
				case Types.EntityStateUpdate.HP:
					entity[SRProperty.HP] = packet.ReadUInt();
					break;
				case Types.EntityStateUpdate.MP:
					entity[SRProperty.MP] = packet.ReadUInt();
					break;
				case Types.EntityStateUpdate.HPMP:
				case Types.EntityStateUpdate.EntityHPMP:
					entity[SRProperty.HP] = packet.ReadUInt();
					entity[SRProperty.MP] = packet.ReadUInt();
					break;
				case Types.EntityStateUpdate.BadStatus:
					entity[SRProperty.BadStatusFlags] = (Types.BadStatus)packet.ReadUInt();
					break;
			}
			// End of Packet
			Bot.Get._OnEntityStatusUpdated(ref entity, updateType);
		}
		public static void EnviromentWheaterUpdate(Packet packet)
		{
			//byte wheaterType = packet.ReadByte();
			//byte wheaterIntensity = packet.ReadByte();
		}
		public static void NoticeUniqueUpdate(Packet packet)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			byte type = packet.ReadByte();
			switch (type)
			{
				case 5: 
					if (w.Character_cbxMessageUniques.Checked)
					{
						byte unkByte01 = packet.ReadByte();
						uint modelID = packet.ReadUInt();

						string unique = i.GetModel(modelID)["name"];
						w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_APPEAR_UNIC", unique));
					}
					break;
				case 6:
					if (w.Character_cbxMessageUniques.Checked)
					{
						byte unkByte01 = packet.ReadByte();
						uint modelID = packet.ReadUInt();
						string player = packet.ReadAscii();

						string unique = i.GetModel(modelID)["name"];
						w.LogMessageFilter(i.GetUIFormat("UIIT_MSG_ANYONE_DEAD_UNIC", player, unique));
					}
					break;
			}
		}
		public static void PlayerPetitionRequest(Packet packet)
		{
			byte type = packet.ReadByte();
			uint uniqueID = packet.ReadUInt();
			switch ((Types.PlayerPetition)type)
			{
				case Types.PlayerPetition.PartyCreation:
				case Types.PlayerPetition.PartyInvitation:
					Bot.Get._OnPartyInvitation(uniqueID, (Types.PartySetup)packet.ReadByte());
					break;
				case Types.PlayerPetition.Resurrection:
					Bot.Get.OnResurrection(uniqueID);
					break;
				case Types.PlayerPetition.GuildInvitation:

					break;
				case Types.PlayerPetition.UnionInvitation:

					break;
				case Types.PlayerPetition.AcademyInvitation:

					break;
			}
		}
		public static void PartyData(Packet packet)
		{
			uint unkUint01 = packet.ReadUInt();
			uint unkUint02 = packet.ReadUInt();
			byte partyPurposeType = packet.ReadByte();
			byte partySetupFlags = packet.ReadByte();
			byte playerCount = packet.ReadByte();
			for (int j = 0; j < playerCount; j++)
				PartyAddPlayer(packet);

			// Event hook
			Bot.Get._OnPartyJoined((Types.PartySetup)partySetupFlags, (Types.PartyPurpose)partyPurposeType);
		}
		private static void PartyAddPlayer(Packet packet)
		{
			SRObject player = new SRObject();
			player[SRProperty.unkByte01] = packet.ReadByte();
			player[SRProperty.JoinID] = packet.ReadUInt();
			string name = packet.ReadAscii();
			player.LoadDefaultProperties(packet.ReadUInt(), SRType.Model);
			player.Name = name;
			player[SRProperty.Level] = packet.ReadByte();
			player[SRProperty.HPMP] = packet.ReadByte();
			ushort region = packet.ReadUShort();
			if (SRCoord.inDungeon(region))
				player[SRProperty.Position] = new SRCoord(region, packet.ReadInt(), packet.ReadInt(), packet.ReadInt());
			else
				player[SRProperty.Position] = new SRCoord(region, (int)packet.ReadUShort(), (int)packet.ReadUShort(), (int)packet.ReadUShort());
			player[SRProperty.unkByte02] = packet.ReadByte(); // 2 = unkByte08.
			player[SRProperty.unkByte03] = packet.ReadByte();
			player[SRProperty.unkByte04] = packet.ReadByte();
			player[SRProperty.unkByte05] = packet.ReadByte();
			player[SRProperty.GuildName] = packet.ReadAscii();
			player[SRProperty.unkByte06] = packet.ReadByte();
			if (packet.Opcode == Agent.Opcode.SERVER_PARTY_UPDATE && (byte)player[SRProperty.unkByte02] == 2)
			{
				player[SRProperty.unkByte07] = packet.ReadByte();
			}
			uint masteryID_primary = packet.ReadUInt();
			uint masteryID_secondary = packet.ReadUInt();

			// Keep on track players for updates
			Info i = Info.Get;
			i.PartyMembers[(uint)player[SRProperty.JoinID]] = player;

			// Add player to GUI
			ListViewItem item = new ListViewItem(name);
			item.Name = player[SRProperty.JoinID].ToString();
			item.SubItems.Add((string)player[SRProperty.GuildName]);
			item.SubItems.Add(player[SRProperty.Level].ToString());
			if (i.Charname.Equals(name))
			{
				item.SubItems.Add("- - -");
			}
			else
			{
				byte HPMP = (byte)player[SRProperty.HPMP];
				item.SubItems.Add(string.Format("{0}% / {1}%", (HPMP & 15) * 10, (HPMP >> 4) * 10));
			}
			item.SubItems.Add(i.GetRegion(region));

			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
				w.Party_lstvPartyMembers.Items.Add(item);
			});
		}
		public static void PartyUpdate(Packet packet)
		{
			byte updateType = packet.ReadByte();

			Window w = Window.Get;
			Info i = Info.Get;

			uint joinID;
			switch (updateType)
			{
				case 1: // Dismissed
					ushort unkUShort = packet.ReadUShort();
					// Event hook
					Bot.Get._OnPartyLeaved();
					break;
				case 2: // Member joined
					PartyAddPlayer(packet);
					break;
				case 3: // Member leaved
					joinID = packet.ReadUInt();
					byte reason = packet.ReadByte();

					Bot.Get._OnMemberLeaved(joinID);
					break;
				case 6: // Member update
					joinID = packet.ReadUInt();
					SRObject player = i.PartyMembers[joinID];

					updateType = packet.ReadByte();
					switch (updateType)
					{
						case 2:
							player[SRProperty.Level] = packet.ReadByte();
							WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, ()=>{
								w.Party_lstvPartyMembers.Items[joinID.ToString()].SubItems[2].Text = player[SRProperty.Level].ToString();
							});
							break;
						case 4: // HP & MP
							byte HPMP = packet.ReadByte();
							byte hp = (byte)(HPMP & 15);
							byte mp = (byte)(HPMP >> 4);
							if (hp == 0) // Check if is dead
								mp = 0;
							else if(hp == 0xB)
								hp = (byte)(hp - 1);
							player[SRProperty.HPMP] = (byte)(hp & mp);

							string PercentHPMP = string.Format("{0}% / {1}%", hp * 10, mp * 10);
							// Weird : sometimes hp is wrong (by +1), giving as result 110% or 10% in dead state
							WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
								w.Party_lstvPartyMembers.Items[joinID.ToString()].SubItems[3].Text = PercentHPMP;
							});
							break;
						case 0x20: // Map position
							string region = i.GetRegion(packet.ReadUShort());
							// X,Y,Z .. not important atm, only for minimap
							WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
								w.Party_lstvPartyMembers.Items[joinID.ToString()].SubItems[4].Text = region;
							});
							break;
					}
					break;
			}
		}
		public static void PartyMatchListResponse(Packet packet) {
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch, () => {
				w.Party_lstvPartyMatch.Items.Clear();
			});

			byte success = packet.ReadByte();
			if (success == 1)
			{
				Bot b = Bot.Get;
				bool hasParty = b.inParty;

				byte pageCount = packet.ReadByte();
				byte pageIndex = packet.ReadByte();
				byte partyCount = packet.ReadByte();

				Dictionary<uint, SRPartyMatch> PartyMatches = new Dictionary<uint, SRPartyMatch>();
				for (int j = 0; j < partyCount; j++)
				{
					SRPartyMatch Party = new SRPartyMatch(packet.ReadUInt());
					Party.OwnerJoinID = packet.ReadUInt();
					Party.Owner = packet.ReadAscii();
					packet.ReadByte();
					Party.MemberCount = packet.ReadByte();
					Party.Setup = (Types.PartySetup)packet.ReadByte();
					Party.Purpose = (Types.PartyPurpose)packet.ReadByte();
					Party.LevelMin = packet.ReadByte();
					Party.LevelMax = packet.ReadByte();
					Party.Title = packet.ReadAscii();

					PartyMatches[Party.Number] = Party;
				}
				Bot.Get._OnPartyMatchListing(pageIndex, pageCount, PartyMatches);
			}
		}
		public static void PartyMatchCreationResponse(Packet packet)
		{

		}
		public static void PartyMatchDeleteResponse(Packet packet)
		{
			// Generate events to remake party match
			// PacketBuilder.RequestPartyMatch();
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
		public static void EntitySelection(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				Bot.Get._OnEntitySelected(packet.ReadUInt());
			}
		}
		public static void CharacterAddStatPointResponse(Packet packet)
		{
			Window w = Window.Get;

			bool success = packet.ReadByte() == 1;
			if (success)
			{
				Info i = Info.Get;

				ushort StatPoints = (ushort)i.Character[SRProperty.StatPoints];
				if (StatPoints > 0)
				{
					i.Character[SRProperty.StatPoints] = (ushort)(StatPoints - 1);
					WinAPI.InvokeIfRequired(w.Character_lblStatPoints, () => {
						w.Character_lblStatPoints.Text = i.Character[SRProperty.StatPoints].ToString();
					});
					if ((ushort)i.Character[SRProperty.StatPoints] == 0)
					{
						// lock buttons
						WinAPI.InvokeIfRequired(w.Character_gbxStatPoints, () => {
							w.Character_btnAddINT.Enabled = w.Character_btnAddSTR.Enabled = false;
						});
					}
				}
			}
			else
			{
				WinAPI.InvokeIfRequired(w.Character_gbxStatPoints, () => {
					w.Character_btnAddINT.Enabled = w.Character_btnAddSTR.Enabled = false;
				});
			}
		}
		public static bool InventoryItemMovement(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				byte type = packet.ReadByte();
				switch ((Types.InventoryItemMovement)type)
				{
					case Types.InventoryItemMovement.InventoryToInventory:
						InventoryItemMovement_InventoryToInventory(packet);
						break;
					case Types.InventoryItemMovement.StorageToStorage:
						InventoryItemMovement_StorageToStorage(packet);
						break;
					case Types.InventoryItemMovement.InventoryToStorage:
						InventoryItemMovement_InventoryToStorage(packet);
						break;
					case Types.InventoryItemMovement.StorageToInventory:
						InventoryItemMovement_StorageToInventory(packet);
						break;
					case Types.InventoryItemMovement.GroundToInventory:
						InventoryItemMovement_GroundToInventory(packet);
						break;
					case Types.InventoryItemMovement.InventoryToGround:
						InventoryItemMovement_InventoryToGround(packet);
						break;
					case Types.InventoryItemMovement.ShopToInventory:
						InventoryItemMovement_ShopToInventory(packet);
						// Client ignore packet, the bot will handle it as a pick up injection (always)
						return true;
					case Types.InventoryItemMovement.InventoryToShop:
						InventoryItemMovement_InventoryToShop(packet);
						break;
					case Types.InventoryItemMovement.PetToPet:
						InventoryItemMovement_PetToPet(packet);
						break;
					case Types.InventoryItemMovement.PetToInventory:
						InventoryItemMovement_PetToInventory(packet);
						break;
					case Types.InventoryItemMovement.InventoryToPet:
						InventoryItemMovement_InventoryToPet(packet);
						break;
					case Types.InventoryItemMovement.QuestToInventory:
						InventoryItemMovement_QuestToInventory(packet);
						break;
					case Types.InventoryItemMovement.InventoryToQuest:
						InventoryItemMovement_InventoryToQuest(packet);
						break;
					case Types.InventoryItemMovement.TransportToTransport:
						InventoryItemMovement_TransportToTransport(packet);
						break;
					case Types.InventoryItemMovement.GroundToTransport:
						InventoryItemMovement_GroundToTransport(packet);
						break;
					case Types.InventoryItemMovement.ShopToTransport:
						InventoryItemMovement_ShopToTransport(packet);
						// Client ignore packet, the bot will handle it as a pick up injection (always)
						return true;
					case Types.InventoryItemMovement.TransportToShop:
						InventoryItemMovement_TransportToShop(packet);
						break;
					case Types.InventoryItemMovement.ShopBuyBack:
						InventoryItemMovement_ShopBuyBack(packet);
						break;
					case Types.InventoryItemMovement.AvatarToInventory:
						InventoryItemMovement_AvatarToInventory(packet);
						break;
					case Types.InventoryItemMovement.InventoryToAvatar:
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

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if ((ushort)inventory[slotInitial][SRProperty.QuantityMax] == 1
					|| (ushort)inventory[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					// switch (empty)
					SRObject temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					ushort q = (ushort)inventory[slotInitial][SRProperty.Quantity];
					inventory[slotFinal][SRProperty.Quantity] = quantityMoved;
					inventory[slotInitial][SRProperty.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| quantityMoved == (ushort)inventory[slotFinal][SRProperty.QuantityMax]
				|| (ushort)inventory[slotFinal][SRProperty.Quantity] == (ushort)inventory[slotFinal][SRProperty.QuantityMax])
			{
				// switch
				SRObject temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)inventory[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					inventory[slotFinal][SRProperty.Quantity] = (ushort)((ushort)inventory[slotFinal][SRProperty.Quantity] + quantityMoved);
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal][SRProperty.Quantity] = (ushort)((ushort)inventory[slotFinal][SRProperty.Quantity] + quantityMoved);
					inventory[slotInitial][SRProperty.Quantity] = (ushort)((ushort)inventory[slotInitial][SRProperty.Quantity] - quantityMoved);
				}
			}

			if (slotInitial == 6 || slotFinal == 6)
			{
				Bot.Get._OnWeaponChanged();
			}

			bool isDoubleMovement = p.ReadByte() == 1;
			if (isDoubleMovement)
				InventoryItemMovement_InventoryToInventory(p);
		}
		private static void InventoryItemMovement_StorageToStorage(Packet p)
		{
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRObjectCollection storage = (SRObjectCollection)Info.Get.Character[SRProperty.Storage];

			// Check if is stack or just switch.. and update it.
			if (storage[slotFinal] == null)
			{
				if ((ushort)storage[slotInitial][SRProperty.QuantityMax] == 1
					|| (ushort)storage[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					// switch (empty)
					SRObject temp = storage[slotFinal];
					storage[slotFinal] = storage[slotInitial];
					storage[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					storage[slotFinal] = storage[slotInitial].Clone();
					ushort q = (ushort)storage[slotInitial][SRProperty.Quantity];
					storage[slotFinal][SRProperty.Quantity] = quantityMoved;
					storage[slotInitial][SRProperty.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (storage[slotFinal].ID != storage[slotInitial].ID
				|| quantityMoved == (ushort)storage[slotFinal][SRProperty.QuantityMax])
			{
				// switch
				SRObject temp = storage[slotFinal];
				storage[slotFinal] = storage[slotInitial];
				storage[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)storage[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					storage[slotFinal][SRProperty.Quantity] = (ushort)((ushort)storage[slotFinal][SRProperty.Quantity] + quantityMoved);
					storage[slotInitial] = null;
				}
				else
				{
					// fixing
					storage[slotFinal][SRProperty.Quantity] = (ushort)((ushort)storage[slotFinal][SRProperty.Quantity] + quantityMoved);
					storage[slotInitial][SRProperty.Quantity] = (ushort)((ushort)storage[slotInitial][SRProperty.Quantity] - quantityMoved);
				}
			}
		}
		private static void InventoryItemMovement_InventoryToStorage(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotStorage = p.ReadByte();
			// End of Packet
			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			SRObjectCollection storage = (SRObjectCollection)i.Character[SRProperty.Storage];

			// Just move it leaving an empty space at inventory
			storage[slotStorage] = inventory[slotInventory];
			inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_StorageToInventory(Packet p)
		{
			byte slotStorage = p.ReadByte();
			byte slotInventory = p.ReadByte();
			// End of Packet
			Info i = Info.Get;
			SRObjectCollection storage = (SRObjectCollection)i.Character[SRProperty.Storage];
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];

			// Just move it leaving an empty space at storage
			inventory[slotInventory] = storage[slotStorage];
			storage[slotStorage] = null;
		}
		private static void InventoryItemMovement_GroundToInventory(Packet p)
		{
			byte slotInventory = p.ReadByte();
			if (slotInventory == 254)
			{
				// (?) Not explored yet.
			}
			else
			{
				SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
				
				// Check quantity picked
				ushort quantityPickedUp = 1;
				if(inventory[slotInventory] != null){
					quantityPickedUp = (ushort)inventory[slotInventory][SRProperty.Quantity];
					inventory[slotInventory] = ItemParsing(p);
					quantityPickedUp = (ushort)((ushort)inventory[slotInventory][SRProperty.Quantity] - quantityPickedUp);
				}
				else
				{
					inventory[slotInventory] = ItemParsing(p);
				}

				Bot.Get._OnItemPickedUp(inventory[slotInventory],quantityPickedUp);
			}
		}
		private static void InventoryItemMovement_InventoryToGround(Packet p)
		{
			byte slotInventory = p.ReadByte();
			// End of Packet

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
			inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_ShopToInventory(Packet p)
		{
			byte tabNumber = p.ReadByte();
			byte tabSlot = p.ReadByte();
			byte packageCount = p.ReadByte();

			// Select the item from the shop specified
			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			SRObject NPCEntity = i.GetEntity(Bot.Get.GetEntitySelected());
			SRObject item = i.GetItemFromShop(NPCEntity.ServerName, tabNumber, tabSlot);

			if (packageCount == 1)
			{
				byte slotInventory = p.ReadByte();
				item[SRProperty.Quantity] = p.ReadUShort();
				item[SRProperty.unkUInt01] = p.ReadUInt();
				inventory[slotInventory] = item;

				PacketBuilder.Client.CreatePickUpPacket(inventory[slotInventory], slotInventory);
			}
			else
			{
				//// Not confirmed when will happen this behaviour
				//for (byte j = 0; j < packageCount; j++)
				//{
				//	byte slotInventory = p.ReadByte();
				//	item[SRAttribute.Quantity] = (ushort)(1);
				//	inventory[slotInventory] = item;

				//	PacketBuilder.Client.CreatePickUpPacket(inventory[slotInventory], slotInventory);
				//}
			}
		}
		private static void InventoryItemMovement_InventoryToShop(Packet p)
		{
			byte slotInventory = p.ReadByte();
			ushort quantitySold = p.ReadUShort();
			uint NPCModel = p.ReadUInt();
			byte slotBuyBack = p.ReadByte();

			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];

			if (!i.Character.Contains(SRProperty.ShopBuyBack))
				i.Character[SRProperty.ShopBuyBack] = new SRObjectCollection();
			SRObjectCollection buyBack = (SRObjectCollection)i.Character[SRProperty.ShopBuyBack];

			// Sync max. quantity to buy back
			if (slotBuyBack == 5 && slotBuyBack == buyBack.Count)
				buyBack.RemoveAt(0);

			if ((ushort)inventory[slotInventory][SRProperty.Quantity] == quantitySold)
			{
				buyBack[slotBuyBack - 1] = inventory[slotInventory];
				inventory[slotInventory] = null;
			}
			else
			{
				buyBack[slotBuyBack - 1] = inventory[slotInventory].Clone();
				buyBack[slotBuyBack - 1][SRProperty.Quantity] = quantitySold;
				inventory[slotInventory][SRProperty.Quantity] = (ushort)((ushort)inventory[slotInventory][SRProperty.Quantity] - quantitySold);
			}
		}
		private static void InventoryItemMovement_PetToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRProperty.Inventory];

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if ((ushort)inventory[slotInitial][SRProperty.QuantityMax] == 1
					|| (ushort)inventory[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					// switch (empty)
					SRObject temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					ushort q = (ushort)inventory[slotInitial][SRProperty.Quantity];
					inventory[slotFinal][SRProperty.Quantity] = quantityMoved;
					inventory[slotInitial][SRProperty.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| quantityMoved == (ushort)inventory[slotFinal][SRProperty.QuantityMax]
				|| (ushort)inventory[slotFinal][SRProperty.Quantity] == (ushort)inventory[slotFinal][SRProperty.QuantityMax])
			{
				// switch
				SRObject temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)inventory[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					inventory[slotFinal][SRProperty.Quantity] = (ushort)((ushort)inventory[slotFinal][SRProperty.Quantity] + quantityMoved);
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal][SRProperty.Quantity] = (ushort)((ushort)inventory[slotFinal][SRProperty.Quantity] + quantityMoved);
					inventory[slotInitial][SRProperty.Quantity] = (ushort)((ushort)inventory[slotInitial][SRProperty.Quantity] - quantityMoved);
				}
			}
		}
		private static void InventoryItemMovement_PetToInventory(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotPetInventory = p.ReadByte();
			byte slotMyInventory = p.ReadByte();

			Info i = Info.Get;
			SRObjectCollection myInventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection petInventory = (SRObjectCollection)pet[SRProperty.Inventory];

			myInventory[slotMyInventory] = petInventory[slotPetInventory];
			petInventory[slotPetInventory] = null;
		}
		private static void InventoryItemMovement_InventoryToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotMyInventory = p.ReadByte();
			byte slotPetInventory = p.ReadByte();

			Info i = Info.Get;
			SRObjectCollection myInventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			SRObject pet = i.GetEntity(uniqueID);
			SRObjectCollection petInventory = (SRObjectCollection)pet[SRProperty.Inventory];

			petInventory[slotPetInventory] = myInventory[slotMyInventory];
			myInventory[slotMyInventory] = null;
		}
		private static void InventoryItemMovement_QuestToInventory(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte unkByte01 = p.ReadByte();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
			inventory[slotInventory] = ItemParsing(p);
		}
		private static void InventoryItemMovement_InventoryToQuest(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte unkByte01 = p.ReadByte();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
			inventory[slotInventory] = null;
		}

		private static void InventoryItemMovement_TransportToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRProperty.Inventory];

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if ((ushort)inventory[slotInitial][SRProperty.QuantityMax] == 1
					|| (ushort)inventory[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					// switch (empty)
					SRObject temp = inventory[slotFinal];
					inventory[slotFinal] = inventory[slotInitial];
					inventory[slotInitial] = temp;
				}
				else
				{
					// stack (partition)
					inventory[slotFinal] = inventory[slotInitial].Clone();
					ushort q = (ushort)inventory[slotInitial][SRProperty.Quantity];
					inventory[slotFinal][SRProperty.Quantity] = quantityMoved;
					inventory[slotInitial][SRProperty.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| quantityMoved == (ushort)inventory[slotFinal][SRProperty.QuantityMax]
				|| (ushort)inventory[slotFinal][SRProperty.Quantity] == (ushort)inventory[slotFinal][SRProperty.QuantityMax])
			{
				// switch
				SRObject temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)inventory[slotInitial][SRProperty.Quantity] == quantityMoved)
				{
					inventory[slotFinal][SRProperty.Quantity] = (ushort)((ushort)inventory[slotFinal][SRProperty.Quantity] + quantityMoved);
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal][SRProperty.Quantity] = (ushort)((ushort)inventory[slotFinal][SRProperty.Quantity] + quantityMoved);
					inventory[slotInitial][SRProperty.Quantity] = (ushort)((ushort)inventory[slotInitial][SRProperty.Quantity] - quantityMoved);
				}
			}
		}
		private static void InventoryItemMovement_GroundToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInventory = p.ReadByte();

			SRObject item = new SRObject();
			item[SRProperty.unkUInt01] = p.ReadUInt();
			item.LoadDefaultProperties(p.ReadUInt(), SRType.Item);
			item[SRProperty.Quantity] = p.ReadUShort();
			item[SRProperty.OwnerName] = p.ReadAscii();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRProperty.Inventory];
			inventory[slotInventory] = item;
		}
		private static void InventoryItemMovement_ShopToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte tabNumber = p.ReadByte();
			byte tabSlot = p.ReadByte();
			byte packageCount = p.ReadByte();

			// Select the item from the shop specified
			Info i = Info.Get;
			SRObject pet = i.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRProperty.Inventory];
			SRObject NPCEntity = i.GetEntity(Bot.Get.GetEntitySelected());
			SRObject item = i.GetItemFromShop(NPCEntity.ServerName, tabNumber, tabSlot);
			item[SRProperty.OwnerName] = i.Charname;

			if (packageCount == 1)
			{
				byte slotInventory = p.ReadByte();
				item[SRProperty.Quantity] = p.ReadUShort();
				item[SRProperty.unkUInt01] = p.ReadUInt();
				inventory[slotInventory] = item;

				PacketBuilder.Client.CreatePickUpSpecialtyGoodsPacket(inventory[slotInventory], slotInventory, uniqueID);
			}
			else
			{
				//// Not confirmed when will happen this behaviour
				//for (byte j = 0; j < packageCount; j++)
				//{
				//	byte slotInventory = p.ReadByte();
				//	item[SRAttribute.Quantity] = (ushort)(1);
				//	inventory[slotInventory] = item;

				//	PacketBuilder.Client.CreatePickUpSpecialtyGoodsPacket(inventory[slotInventory], slotInventory, uniqueID);
				//}
			}
		}
		private static void InventoryItemMovement_TransportToShop(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInventory = p.ReadByte();
			ushort quantitySold = p.ReadUShort();
			//uint npcUniqueID = p.ReadUInt();
			//uint unkByte01 = p.ReadByte();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRProperty.Inventory];

			if ((ushort)inventory[slotInventory][SRProperty.Quantity] == quantitySold)
				inventory[slotInventory] = null;
			else
				inventory[slotInventory][SRProperty.Quantity] = (ushort)((ushort)inventory[slotInventory][SRProperty.Quantity] - quantitySold);
		}
		private static void InventoryItemMovement_ShopBuyBack(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotBuyBack = p.ReadByte();
			ushort quantitySold = p.ReadUShort();

			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			SRObjectCollection buyBack = (SRObjectCollection)i.Character[SRProperty.ShopBuyBack];

			inventory[slotInventory] = buyBack[slotBuyBack];
			buyBack.RemoveAt(slotBuyBack);
		}
		private static void InventoryItemMovement_AvatarToInventory(Packet p)
		{               
			byte slotInventoryAvatar = p.ReadByte();
			byte slotInventory = p.ReadByte();
			
			Info i = Info.Get;
			SRObjectCollection inventoryAvatar = (SRObjectCollection)i.Character[SRProperty.InventoryAvatar];
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			// Switch
			SRObject item = inventory[slotInventory];
			inventory[slotInventory] = inventoryAvatar[slotInventoryAvatar];
			inventoryAvatar[slotInventoryAvatar] = item;
    }
		private static void InventoryItemMovement_InventoryToAvatar(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotInventoryAvatar = p.ReadByte();

			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRProperty.Inventory];
			SRObjectCollection inventoryAvatar = (SRObjectCollection)i.Character[SRProperty.InventoryAvatar];
			// Switch
			SRObject item = inventory[slotInventory];
			inventory[slotInventory] = inventoryAvatar[slotInventoryAvatar];
			inventoryAvatar[slotInventoryAvatar] = item;
		}
		public static void InventoryItemUse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				byte slot = packet.ReadByte();
				ushort quantityUpdate = packet.ReadUShort();
				//ushort usageType = packet.ReadUShort();

				SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
				if (quantityUpdate == 0)
					inventory[slot] = null; // Item consumed
				else
					inventory[slot][SRProperty.Quantity] = quantityUpdate;
			}
		}
		public static void InventoryItemDurabilityUpdate(Packet packet)
		{
			byte slotInventory = packet.ReadByte();
			uint durability = packet.ReadUInt();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
			inventory[slotInventory][SRProperty.Durability] = durability;
		}
		public static void InventoryItemStateUpdate(Packet packet)
		{
			byte slotInventory = packet.ReadByte();
			byte updateType = packet.ReadByte();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];
			switch (updateType)
			{
				case 0x40: // Pet State
					inventory[slotInventory][SRProperty.PetState] = (Types.PetState)packet.ReadByte();
					break;
			}
		}
		private static Packet storageDataPacket;
		public static void StorageDataBegin(Packet packet)
		{
			Info.Get.Character[SRProperty.StorageGold] = packet.ReadULong();
			storageDataPacket = new Packet(Agent.Opcode.SERVER_STORAGE_DATA);
		}
		public static void StorageData(Packet packet)
		{
			storageDataPacket.WriteByteArray(packet.GetBytes());
		}
		public static void StorageDataEnd(Packet packet)
		{
			storageDataPacket.Lock();
			Packet p = storageDataPacket;

			SRObjectCollection storage = new SRObjectCollection(p.ReadByte());
			byte itemsCount = p.ReadByte();
			for (int j = 0; j < itemsCount; j++)
			{
				byte slot = p.ReadByte();
				storage[slot] = ItemParsing(p);
			}
			Info.Get.Character[SRProperty.Storage] = storage;
		}
		public static void ConsigmentRegisterResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];

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
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRProperty.Inventory];

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

			Info i = Info.Get;
			SRObject pet = i.GetEntity(uniqueID);
			if (pet.ID1 == 1)
			{
				// BIONIC
				if(pet.ID2 == 2 && pet.ID3 == 3)
				{
					// COS
					if (pet.ID4 == 1 || pet.ID4 == 2 )
					{
						// VEHICLE / TRANSPORT
						pet[SRProperty.HP] = packet.ReadUInt();
						pet[SRProperty.HPMax] = packet.ReadUInt();
						SRObjectCollection Inventory = new SRObjectCollection(packet.ReadByte());
						if (Inventory.Capacity > 0)
						{
							// TRANSPORT
							byte itemsCount = packet.ReadByte();
							for(byte j = 0; j < itemsCount; j++)
							{
								byte slot = packet.ReadByte();

								SRObject item = new SRObject();
								item[SRProperty.unkUInt01] = packet.ReadUInt();
								item.LoadDefaultProperties(packet.ReadUInt(),SRType.Item);
								item[SRProperty.Quantity] = packet.ReadUShort();
								item[SRProperty.OwnerName] = packet.ReadAscii();

								Inventory[slot] = item;
							}
							pet[SRProperty.Inventory] = Inventory;
							//uint ownerUniqueID = packet.ReadUInt();
						}
					}
					else if (pet.ID4 == 3)
					{
						// ATTACK PET
						pet[SRProperty.HP] = packet.ReadUInt();
						pet[SRProperty.unkUInt01] = packet.ReadUInt();
						pet[SRProperty.Exp] = packet.ReadULong();
						pet[SRProperty.Level] = packet.ReadByte();
						pet[SRProperty.ExpMax] = i.GetPetExpMax((byte)pet[SRProperty.Level]);
						pet[SRProperty.HGP] = packet.ReadUShort();
						pet[SRProperty.AttackSettingsFlags] = (Types.PetAttackSettings)packet.ReadUInt();
						string PetName = packet.ReadAscii();
						pet[SRProperty.unkByte07] = packet.ReadByte();
						uint ownerUniqueID = packet.ReadUInt();
						pet[SRProperty.unkByte08] = packet.ReadByte();
					}
					else if (pet.ID4 == 4)
					{
						// GRAB PET
						pet[SRProperty.unkUInt01] = packet.ReadUInt();
						pet[SRProperty.unkUInt02] = packet.ReadUInt();
						pet[SRProperty.PickSettingFlags] = (Types.PetPickSettings)packet.ReadUInt();
						string PetName = packet.ReadAscii();

						SRObjectCollection inventory = new SRObjectCollection(packet.ReadByte());
						byte itemsCount = packet.ReadByte();
						for (byte j = 0; j < itemsCount; j++)
						{
							byte slot = packet.ReadByte();
							inventory[slot] = ItemParsing(packet);
						}
						pet[SRProperty.Inventory] = inventory;
						//uint ownerUniqueID = packet.ReadUInt();
					}
				}
			}
			Bot.Get._OnPetSummoned(uniqueID);
		}
		public static void PetUpdate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			byte updateType = packet.ReadByte();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			switch (updateType)
			{
				case 1: // Unsummoned
					Bot.Get._OnPetUnsummoned(uniqueID);
					break;
				case 3: // Exp
					// long ExpReceived = packet.ReadLong();
					// uint sourceUniqueID = packet.ReadUInt();
					// Possible bug here, also it's not important to track the %exp on pet yet.
					// Bot.Get._OnPetExpReceived(ref pet, ExpReceived, (long)((ulong)pet[SRProperty.Exp]), (long)((ulong)pet[SRProperty.ExpMax]), (byte)pet[SRProperty.Level]);
					break;
				case 4: // Hungry
					pet[SRProperty.HGP] = packet.ReadUShort();
					break;
				case 7: // Model changed
					pet.LoadDefaultProperties(packet.ReadUInt(), SRType.Model);
				break;
			}
		}
		public static void PetSettingsChangeResponse(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			byte settingsType = packet.ReadByte();
			
			SRObject pet = Info.Get.GetEntity(uniqueID);
			switch (settingsType)
			{
				case 1: // Pet Attack settings
					pet[SRProperty.AttackSettingsFlags] = (Types.PetAttackSettings)packet.ReadUInt();
					break;
			}
		}
		public static void PetPlayerMounted(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if(success){
				SRObject player = Info.Get.GetEntity(packet.ReadUInt());
				
				player[SRProperty.isRiding] = packet.ReadByte() == 1;
				if ((bool)player[SRProperty.isRiding]){
					// Avoid reading when it's not necessary
					player[SRProperty.RidingUniqueID] = packet.ReadUInt();
				}
			}
		}
		public static void StallCreateResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
				Bot.Get._OnStallOpened(true);
		}
		public static void StallDestroyResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
				Bot.Get._OnStallClosed();
		}
		public static void StallTalkResponse(Packet packet)
		{
			Bot.Get._OnStallOpened(false);
		}
		public static void StallLeaveResponse(Packet packet)
		{
			Bot.Get._OnStallClosed();
		}
		public static void EntityStallCreate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();

			SRObject entity = Info.Get.GetEntity(uniqueID);
			entity[SRProperty.StallTitle] = packet.ReadAscii();
			entity[SRProperty.StallDecorationType] = packet.ReadUInt();
			entity[SRProperty.InteractMode] = Types.PlayerMode.OnStall;
		}
		public static void EntityStallDestroy(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			//ushort unkUshort01 = packet.ReadUShort();

			SRObject entity = Info.Get.GetEntity(uniqueID);
			entity[SRProperty.InteractMode] = Types.PlayerMode.None;
		}
		public static void EntitySkillUse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				byte skillType = packet.ReadByte();
				byte unkByte01 = packet.ReadByte();
				uint skillID = packet.ReadUInt();
				uint sourceUniqueID = packet.ReadUInt();
				uint skillUniqueID = packet.ReadUInt();
				uint targetUniqueID = packet.ReadUInt();
				//byte unkByte02 = packet.ReadByte();
				// End of Packet
				Bot.Get._OnEntitySkillCast(skillType, skillID, sourceUniqueID, targetUniqueID);
			}
		}
		public static void EntitySkillData(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				uint skillUniqueID = packet.ReadUInt();
				uint targetUniqueID = packet.ReadUInt();
				if(targetUniqueID != 0)
				{
					byte unkByte01 = packet.ReadByte();
					if (unkByte01 == 1)
					{
						byte unkByte02 = packet.ReadByte();
						byte unkByte03 = packet.ReadByte();
						targetUniqueID = packet.ReadUInt();
						byte unkByte04 = packet.ReadByte();
						if (unkByte04 == 128)
						{
							Bot.Get._OnEntityDead(targetUniqueID);
						}
						//	byte unkByte05 = packet.ReadByte();
						//	uint damage = packet.ReadUInt();
						// byte unkByte06 = packet.ReadByte();
						//	byte unkByte07 = packet.ReadByte();
						//	byte unkByte08 = packet.ReadByte();
						//	if(unkByte02 == 2)
						//	{
						//		unkByte04 = packet.ReadByte();
						//		unkByte05 = packet.ReadByte();
						//		damage = packet.ReadUInt();
						//		unkByte06 = packet.ReadByte();
						//		unkByte07 = packet.ReadByte();
						//		unkByte08 = packet.ReadByte();
						//	}
					}
				}
			}
		}
		public static void EntitySkillBuffAdded(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			SRObject buff = new SRObject(packet.ReadUInt(), SRType.Skill);
			buff[SRProperty.UniqueID] = packet.ReadUInt();
			// End of Packet

			// Ignore flashy buffs 
			if((uint)buff[SRProperty.DurationMax] > 0){
				// Easy tracking
				buff[SRProperty.OwnerUniqueID] = uniqueID;
				Bot.Get._OnEntityBuffAdded(uniqueID,buff);
			}
		}
		public static void EntitySkillBuffRemoved(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				uint buffUniqueID = packet.ReadUInt();
				Bot.Get._OnEntityBuffRemoved(buffUniqueID);
			}
		}
		public static void MasterySkillLevelUpResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				SRObject newSkill = new SRObject(packet.ReadUInt(), SRType.Skill);

				// Update skills
				Info i = Info.Get;
				SRObjectDictionary<uint> Skills = (SRObjectDictionary<uint>)i.Character[SRProperty.Skills];

				// Look for the skill with the last groupname
				uint lastSkillID = i.GetLastSkillID(newSkill);

				Window w = Window.Get;
				if (lastSkillID == 0)
				{
					// Add new skill
					Skills[newSkill.ID] = newSkill;
					w.AddSkill(newSkill);
				}
				else
				{
					// Update/override if the skill is sharing the same groupname
					Skills.SetKey(lastSkillID, newSkill.ID);

					SRObject skill = Skills[newSkill.ID];
					skill.CopyFrom(newSkill);
					// Update the skill name/key from every list
					w.UpdateSkillNames(lastSkillID, newSkill.ID);
				}
			}
		}
		public static void MasterySkillLevelDownResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				SRObject newSkill = new SRObject(packet.ReadUInt(), SRType.Skill);

				// Update skills
				Info i = Info.Get;
				SRObjectDictionary<uint> Skills = (SRObjectDictionary<uint>)i.Character[SRProperty.Skills];

				// Look for the skill with the next groupname
				uint nextSkillID = i.GetNextSkillID(newSkill);

				Window w = Window.Get;
				if (nextSkillID == 0) // Just in case
				{
					// Add new skill
					Skills[newSkill.ID] = newSkill;
					w.AddSkill(newSkill);
				}
				else
				{
					SRObject skill = Skills[nextSkillID];
					if (skill == null)
					{
						// Remove skill from mastery
						w.RemoveSkill(newSkill.ID);
						Skills.RemoveKey(newSkill.ID);
          }
					else
					{
						// Update/override if the skill is sharing the same groupname
						Skills.SetKey(nextSkillID, newSkill.ID);

						skill.CopyFrom(newSkill);
						// Update the skill name/key from every list
						w.UpdateSkillNames(nextSkillID, newSkill.ID);
					}
				}
			}
		}
		public static void MasteryLevelUpResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				uint masteryID = packet.ReadUInt();

				SRObjectCollection masteries = (SRObjectCollection)Info.Get.Character[SRProperty.Masteries];
				SRObject mastery = masteries.Find(m => m.ID == masteryID);
				mastery[SRProperty.Level] = packet.ReadByte();
			}
		}
		public static void MasteryLevelDownResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				uint masteryID = packet.ReadUInt();

				SRObjectCollection masteries = (SRObjectCollection)Info.Get.Character[SRProperty.Masteries];
				SRObject mastery = masteries.Find(m => m.ID == masteryID);
				mastery[SRProperty.Level] = packet.ReadByte();
			}
		}
		public static void EntitySpeedUpdate(Packet packet)
		{
			SRObject entity = Info.Get.GetEntity(packet.ReadUInt());
			entity.GetPosition(); // Force update the current position to recalculate the finish
			entity[SRProperty.SpeedWalking] = packet.ReadFloat();
			entity[SRProperty.SpeedRunning] = packet.ReadFloat();
		}
		public static void EntityStateUpdate(Packet packet)
		{
			SRObject entity = Info.Get.GetEntity(packet.ReadUInt());
			byte updateType = packet.ReadByte();
			byte updateState = packet.ReadByte();
			switch (updateType)
			{
				case 0: // LifeState
					entity[SRProperty.LifeState] = (Types.LifeState)updateState;
					break;
				case 1: // MotionState
					entity.GetPosition(); // Force update the position before changing
					entity[SRProperty.MotionStateType] = (Types.MotionState)updateState;
					switch ((Types.MotionState)updateState)
					{
						case Types.MotionState.Running:
							entity[SRProperty.MovementSpeedType] = Types.MovementSpeed.Running;
							break;
						case Types.MotionState.Walking:
							entity[SRProperty.MovementSpeedType] = Types.MovementSpeed.Walking;
							break;
					}
					break;
				case 4:
					entity[SRProperty.PlayerStateType] = (Types.PlayerState)updateState;
					break;
				case 7:
					entity[SRProperty.PVPStateType] = (Types.PVPState)updateState;
					break;
				case 8:
					entity[SRProperty.inCombat] = updateState == 1;
					break;
				case 11:
					entity[SRProperty.ScrollMode] = (Types.ScrollMode)updateState;
					break;
			}
		}
	}
}