﻿using System;
using SecurityAPI;
using System.Windows.Forms;
using System.Drawing;
using xBot.Network;
using System.Collections.Specialized;
using System.IO;

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
			while (packet.ReadByte() == 1)
			{
				ushort serverID = packet.ReadUShort();
				string serverName = packet.ReadAscii();
				ushort onlineCount = packet.ReadUShort();
				ushort maxCapacity = packet.ReadUShort();
				bool isAvailable = packet.ReadByte() == 1;
				byte serverID_farmID = packet.ReadByte();
				
				// Generate server list
				ListViewItem i = new ListViewItem(serverName);
				i.Name = serverID.ToString();
				i.SubItems.Add(onlineCount + " / " + maxCapacity);
				i.SubItems.Add((isAvailable ? "Available" : "Not available"));
				WinAPI.InvokeIfRequired(w.Login_lstvServers, () => {
					//i.Group = Window.get().Login_lstvServers.Groups[serverID_farmID.ToString()];
					w.Login_lstvServers.Items.Add(i);
				});
				if (isAvailable)
				{
					if (Bot.Get.isAutoLogin)
					{
						if (w.Login_cmbxServer.Tag != null
							&& ((string)w.Login_cmbxServer.Tag).ToLower() == serverName.ToLower())
						{
							w.Login_cmbxServer.Tag = serverID;
            }
					}
					else
					{
						WinAPI.InvokeIfRequired(w.Login_cmbxServer, () => {
							w.Login_cmbxServer.Items.Add(serverName);
						});
					}
				}
			}
			// AutoLogin
			if (Bot.Get.isAutoLogin)
			{
				WinAPI.InvokeIfRequired(w.Login_gbxAccount, () => {
					PacketBuilder.Login(w.Login_tbxUsername.Text, w.Login_tbxPassword.Text, (ushort)w.Login_cmbxServer.Tag);
				});
			}
			else
			{
				// Check first one as default
				WinAPI.InvokeIfRequired(w.Login_cmbxServer, () => {
					if (w.Login_cmbxServer.SelectedText == "" && w.Login_cmbxServer.Items.Count > 0)
					{
						w.Login_cmbxServer.SelectedIndex = 0;
					}
				});
			}
			// Unblock button
			if (w.Login_btnStart.Text == "STOP" && Bot.Get.Proxy.ClientlessMode)
			{
				WinAPI.InvokeIfRequired(w.Login_btnStart, () => {
					w.Login_btnStart.Text = "LOGIN";
					if (!Bot.Get.isAutoLogin)
						w.EnableControl(w.Login_btnStart, true);
				});
			}
			w.LogProcess("Server selection");
		}
		public static void CharacterSelectionActionResponse(Packet packet)
		{
			byte action = packet.ReadByte();
			byte result = packet.ReadByte();
			switch (action)
			{
				case Types.CharacterSelectionAction.Create:
					if (result == 1)
					{
						Window.Get.Log("Character created successfully");
					}
					else
					{
						Window.Get.Log("Character creation failed!");
					}
					break;
				case Types.CharacterSelectionAction.CheckName:
					if (result == 1)
					{
						Window.Get.Log("Nickname available..");
					}
					else
					{
						Window.Get.Log("Nickname has been already taken!");
					}
					break;
				case Types.CharacterSelectionAction.Delete:

					break;
				case Types.CharacterSelectionAction.List:
					if (result == 1)
					{
						Window w = Window.Get;
						// Reset values
						WinAPI.InvokeIfRequired(w.Login_lstvCharacters, () =>
						{
							w.Login_lstvCharacters.Items.Clear();
						});
						WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () =>
						{
							w.Login_cmbxCharacter.Items.Clear();
						});
						Info.Get.CharacterList.Clear();
						// Start packet reading 
						byte nChars = packet.ReadByte();
						for (int i = 0; i < nChars; i++)
						{
							SRObject character = new SRObject(packet.ReadUInt(), SRObject.Type.Model);
							character[SRAttribute.Name] = packet.ReadAscii();
							character[SRAttribute.Scale] = packet.ReadByte();
							character[SRAttribute.Level] = packet.ReadByte();
							character[SRAttribute.Exp] = packet.ReadULong();
							character[SRAttribute.ExpMax] = Info.Get.getMaxExp((byte)character[SRAttribute.Level]);
							character[SRAttribute.STR] = packet.ReadUShort();
							character[SRAttribute.INT] = packet.ReadUShort();
							character[SRAttribute.StatPoints] = packet.ReadUShort();
							character[SRAttribute.HP] = packet.ReadUInt();
							character[SRAttribute.MP] = packet.ReadUInt();
							character[SRAttribute.isDeleting] = packet.ReadByte() == 1;
							if ((bool)character[SRAttribute.isDeleting])
							{
								character[SRAttribute.DeletingTime] = packet.ReadUInt();
							}
							character[SRAttribute.GuildMemberType] = packet.ReadByte();
							character[SRAttribute.isGuildRenameRequired] = packet.ReadByte() == 1;
							if ((bool)character[SRAttribute.isGuildRenameRequired])
							{
								character[SRAttribute.GuildName] = packet.ReadAscii();
							}
							character[SRAttribute.AcademyMemberType] = packet.ReadByte();
							SRObjectCollection inventory = new SRObjectCollection(packet.ReadByte());
							for (int j = 0; j < inventory.Capacity; j++)
							{
								inventory[j] = new SRObject(packet.ReadUInt(), SRObject.Type.Item);
								inventory[j][SRAttribute.Plus] = packet.ReadByte();
							}
							character[SRAttribute.Inventory] = inventory;
							SRObjectCollection inventoryAvatar = new SRObjectCollection(packet.ReadByte());
							for (int j = 0; j < inventoryAvatar.Capacity; j++)
							{
								inventoryAvatar[j] = new SRObject(packet.ReadUInt(), SRObject.Type.Item);
								inventoryAvatar[j][SRAttribute.Plus] = packet.ReadByte();
							}
							character[SRAttribute.InventoryAvatar] = inventory;
							// End of Packet

							// Adding to game info
							Info.Get.CharacterList.Add(character);

							// Filling data to GUI
							ListViewItem l = new ListViewItem();
							l.Text = character[SRAttribute.Name] + ((bool)character[SRAttribute.isDeleting] ? " (*)" : "");
							l.Name = (string)character[SRAttribute.Name];
							l.SubItems.Add(character[SRAttribute.Level].ToString());
							l.SubItems.Add(Math.Round((double)character.getExpPercent(), 2) + " %");
							l.SubItems.Add(character.ID.ToString());
							WinAPI.InvokeIfRequired(w.Login_lstvCharacters, () =>
							{
								w.Login_lstvCharacters.Items.Add(l);
							});
							WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () =>
							{
								w.Login_cmbxCharacter.Items.Add(l.Name);
							});
						}
						// Auto select first character as default
						if (w.Login_cmbxCharacter.Items.Count > 0)
						{
							WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () =>
							{
								w.Login_cmbxCharacter.SelectedIndex = 0;
							});
						}
						// Switch Listview's [Servers to Characters] selection 
						WinAPI.InvokeIfRequired(w.Login_gbxServers, () =>
						{
							w.Login_gbxServers.Visible = false;
						});
						WinAPI.InvokeIfRequired(w.Login_gbxCharacters, () =>
						{
							w.Login_gbxCharacters.Visible = true;
						});
						// Switch and restaure [Login to Select] button
						WinAPI.InvokeIfRequired(w.Login_btnStart, () =>
						{
							w.Login_btnStart.Text = "SELECT";
							w.EnableControl(w.Login_btnStart, true);
						});
						w.LogProcess("Character selection");
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
		private static Packet characterDataPacket;
		public static void CharacterDataBegin(Packet packet)
		{
			characterDataPacket = new Packet(Agent.Opcode.SERVER_CHARACTER_DATA);
		}
		public static void CharacterData(Packet packet)
		{
			characterDataPacket.WriteUInt8Array(packet.GetBytes());
		}
		public static void CharacterDataEnd(Packet packet)
		{
			Packet p = characterDataPacket;
			p.Lock();

			p.ReadUInt(); // ServerTime (SROTimeStamp)
			SRObject character = new SRObject(p.ReadUInt(), SRObject.Type.Model);
			character[SRAttribute.Scale] = p.ReadByte();
			character[SRAttribute.Level] = p.ReadByte();
			character[SRAttribute.LevelMax] = p.ReadByte();
			character[SRAttribute.Exp] = p.ReadULong();
			character[SRAttribute.SPExp] = p.ReadUInt();
			character[SRAttribute.Gold] = p.ReadULong();
			character[SRAttribute.SP] = p.ReadUInt();
			character[SRAttribute.StatPoints] = p.ReadUShort();
			character[SRAttribute.BerserkPoints] = p.ReadByte();
			p.ReadUInt(); // GatheredExpPoint.. wut is this ?
			character[SRAttribute.HPMax] = p.ReadUInt();
			character[SRAttribute.MPMax] = p.ReadUInt();
			character[SRAttribute.ExpType] = p.ReadByte();
			character[SRAttribute.PKDaily] = p.ReadByte();
			character[SRAttribute.PKTotal] = p.ReadUShort();
			character[SRAttribute.PKPenalty] = p.ReadUInt();
			character[SRAttribute.BerserkLevel] = p.ReadByte();
			character[SRAttribute.PVPCapeType] = p.ReadByte();
			#region (Inventory)
			SRObjectCollection Inventory = new SRObjectCollection(p.ReadByte());
			character[SRAttribute.Inventory] = Inventory;

			byte itemsCount = p.ReadByte();
			for (byte i = 0; i < itemsCount; i++)
			{
				byte slot = p.ReadByte();
				SRObject item = new SRObject();
				Inventory[slot] = item;

				item[SRAttribute.RentType] = p.ReadUInt();
				if ((uint)item[SRAttribute.RentType] == 1)
				{
					item[SRAttribute.RentCanDelete] = p.ReadUShort();
					item[SRAttribute.RentPeriodBeginTime] = p.ReadUInt();
					item[SRAttribute.RentPeriodEndTime] = p.ReadUInt();
				}
				else if ((uint)item[SRAttribute.RentType] == 2)
				{
					item[SRAttribute.RentCanDelete] = p.ReadUShort();
					item[SRAttribute.RentCanRecharge] = p.ReadUShort();
					item[SRAttribute.RentMeterRateTime] = p.ReadUInt();
				}
				else if ((uint)item[SRAttribute.RentType] == 3)
				{
					item[SRAttribute.RentCanDelete] = p.ReadUShort();
					item[SRAttribute.RentCanRecharge] = p.ReadUShort();
					item[SRAttribute.RentPeriodBeginTime] = p.ReadUInt();
					item[SRAttribute.RentPeriodEndTime] = p.ReadUInt();
					item[SRAttribute.RentPackingTime] = p.ReadUInt();
				}
				item.Load(p.ReadUInt(), SRObject.Type.Item);
				if (item.ID1 == 3)
				{
					// ITEM_
					if (item.ID2 == 1)
					{
						// ITEM_CH_
						// ITEM_EU_
						// ITEM_AVATAR_
						item[SRAttribute.Plus] = p.ReadByte();
						item[SRAttribute.Variance] = p.ReadULong();
						item[SRAttribute.Durability] = p.ReadUInt();

						SRObjectCollection MagicParams = new SRObjectCollection(p.ReadByte());
						for (int j = 0; j < MagicParams.Capacity; j++)
						{
							MagicParams[j] = new SRObject();
							MagicParams[j][SRAttribute.Type] = p.ReadUInt();
							MagicParams[j][SRAttribute.Value] = p.ReadUInt();
						}
						item[SRAttribute.MagicParams] = MagicParams;
						// 1 = Socket
						p.ReadByte();
						SRObjectCollection SocketParams = new SRObjectCollection(p.ReadByte());
						for (int j = 0; j < SocketParams.Capacity; j++)
						{
							SocketParams[j] = new SRObject();
							SocketParams[j][SRAttribute.Slot] = p.ReadByte();
							SocketParams[j][SRAttribute.ID] = p.ReadUInt();
							SocketParams[j][SRAttribute.Value] = p.ReadUInt();
						}
						item[SRAttribute.SocketParams] = SocketParams;
						// 2 = Advanced elixir
						p.ReadByte();
						SRObjectCollection AdvanceElixirParams = new SRObjectCollection(p.ReadByte());
						for (int j = 0; j < AdvanceElixirParams.Capacity; j++)
						{
							AdvanceElixirParams[j] = new SRObject();
							AdvanceElixirParams[j][SRAttribute.Slot] = p.ReadByte();
							AdvanceElixirParams[j][SRAttribute.ID] = p.ReadUInt();
							AdvanceElixirParams[j][SRAttribute.Plus] = p.ReadUInt();
						}
						item[SRAttribute.AdvanceElixirParams] = AdvanceElixirParams;
					}
					else if (item.ID2 == 2)
					{
						// ITEM_COS
						if (item.ID3 == 1)
						{
							// ITEM_COS_P
							item[SRAttribute.PetState] = p.ReadByte();
							switch ((byte)item[SRAttribute.PetState])
							{
								case Types.PetState.Summoned:
								case Types.PetState.Alive:
								case Types.PetState.Dead:
									item[SRAttribute.Model] = new SRObject(p.ReadUInt(), SRObject.Type.Model);
									item[SRAttribute.Name] = p.ReadAscii();
									if (item.ID4 == 2)
									{
										// ITEM_COS_P (Ability)
										item[SRAttribute.RentPeriodEndTime] = p.ReadUInt();
									}
									item[SRAttribute.unkByte01] = p.ReadByte();
									break;
							}
						}
						else if (item.ID3 == 2)
						{
							// ITEM_ETC_TRANS_MONSTER
							item[SRAttribute.Model] = new SRObject(p.ReadUInt(), SRObject.Type.Model);
						}
						else if (item.ID3 == 3)
						{
							// MAGIC_CUBE
							item[SRAttribute.Amount] = p.ReadUInt();
						}
					}
					else if (item.ID2 == 3)
					{
						// ITEM_ETC
						item[SRAttribute.Quantity] = p.ReadUShort();
						if (item.ID3 == 11)
						{
							if (item.ID4 == 1 || item.ID4 == 2)
							{
								// MAGIC/ATRIBUTTE STONE
								item[SRAttribute.AssimilationProbability] = p.ReadByte();
							}
						}
						else if (item.ID3 == 14 && item.ID4 == 2)
						{
							// ITEM_MALL_GACHA_CARD_WIN
							// ITEM_MALL_GACHA_CARD_LOSE
							SRObjectCollection MagicParams = new SRObjectCollection(p.ReadByte());
							for (int j = 0; j < MagicParams.Capacity; j++)
							{
								MagicParams[j] = new SRObject();
								MagicParams[j][SRAttribute.Type] = p.ReadUInt();
								MagicParams[j][SRAttribute.Value] = p.ReadUInt();
							}
							item[SRAttribute.MagicParams] = MagicParams;
						}
					}
				}
			}
			#endregion
			#region (Inventory Avatar)
			SRObjectCollection InventoryAvatar = new SRObjectCollection(p.ReadByte());
			character[SRAttribute.InventoryAvatar] = InventoryAvatar;

			itemsCount = p.ReadByte();
			for (byte i = 0; i < itemsCount; i++)
			{
				byte slot = p.ReadByte();
				SRObject item = new SRObject();
				InventoryAvatar[slot] = item;

				item[SRAttribute.RentType] = p.ReadUInt();
				if ((byte)item[SRAttribute.RentType] == 1)
				{
					item[SRAttribute.RentCanDelete] = p.ReadUShort();
					item[SRAttribute.RentPeriodBeginTime] = p.ReadUInt();
					item[SRAttribute.RentPeriodEndTime] = p.ReadUInt();
				}
				else if ((byte)item[SRAttribute.RentType] == 2)
				{
					item[SRAttribute.RentCanDelete] = p.ReadUShort();
					item[SRAttribute.RentCanRecharge] = p.ReadUShort();
					item[SRAttribute.RentMeterRateTime] = p.ReadUInt();
				}
				else if ((byte)item[SRAttribute.RentType] == 3)
				{
					item[SRAttribute.RentCanDelete] = p.ReadUShort();
					item[SRAttribute.RentCanRecharge] = p.ReadUShort();
					item[SRAttribute.RentPeriodBeginTime] = p.ReadUInt();
					item[SRAttribute.RentPeriodEndTime] = p.ReadUInt();
					item[SRAttribute.RentPackingTime] = p.ReadUInt();
				}
				item.Load(p.ReadUInt(), SRObject.Type.Item);
				if (item.ID1 == 3 && item.ID2 == 1)
				{
					// ITEM_CH_
					// ITEM_EU_
					// ITEM_AVATAR_
					item[SRAttribute.Plus] = p.ReadByte();
					item[SRAttribute.Variance] = p.ReadULong();
					item[SRAttribute.Durability] = p.ReadUInt();
					SRObjectCollection MagicParams = new SRObjectCollection(p.ReadByte());
					for (int j = 0; j < MagicParams.Capacity; j++)
					{
						MagicParams[j] = new SRObject();
						MagicParams[j][SRAttribute.Type] = p.ReadUInt();
						MagicParams[j][SRAttribute.Value] = p.ReadUInt();
					}
					item[SRAttribute.MagicParams] = MagicParams;
					// 1 = Socket
					p.ReadByte();
					SRObjectCollection SocketParams = new SRObjectCollection(p.ReadByte());
					for (int j = 0; j < SocketParams.Capacity; j++)
					{
						SocketParams[j] = new SRObject();
						SocketParams[j][SRAttribute.Slot] = p.ReadByte();
						SocketParams[j][SRAttribute.ID] = p.ReadUInt();
						SocketParams[j][SRAttribute.Value] = p.ReadUInt();
					}
					item[SRAttribute.SocketParams] = SocketParams;
					// 2 = Advanced elixir
					p.ReadByte();
					SRObjectCollection AdvanceElixirParams = new SRObjectCollection(p.ReadByte());
					for (int j = 0; j < AdvanceElixirParams.Capacity; j++)
					{
						AdvanceElixirParams[j] = new SRObject();
						AdvanceElixirParams[j][SRAttribute.Slot] = p.ReadByte();
						AdvanceElixirParams[j][SRAttribute.ID] = p.ReadUInt();
						AdvanceElixirParams[j][SRAttribute.Plus] = p.ReadUInt();
					}
					item[SRAttribute.AdvanceElixirParams] = AdvanceElixirParams;
				}
			}
			#endregion
			character[SRAttribute.unkByte02] = p.ReadByte();
			#region (Masteries)
			SRObjectCollection Masteries = new SRObjectCollection();
			character[SRAttribute.Masteries] = Masteries;

			while (p.ReadByte() == 1)
			{
				SRObject mastery = new SRObject(p.ReadUInt(), SRObject.Type.Mastery);
				Masteries[Masteries.Count] = mastery;

				mastery[SRAttribute.Level] = p.ReadByte();
			}
			#endregion
			character[SRAttribute.unkByte03] = p.ReadByte();
			#region (Skills)
			SRObjectCollection Skills = new SRObjectCollection();
			character[SRAttribute.Skills] = Skills;

			while (p.ReadByte() == 1)
			{
				SRObject skill = new SRObject(p.ReadUInt(), SRObject.Type.Skill);
				Skills[Skills.Count] = skill;

				skill[SRAttribute.Enabled] = p.ReadByte();
			}
			#endregion
			#region (Quest)
			character[SRAttribute.QuestsCompletedID] = p.ReadUInt32Array(p.ReadUShort());

			SRObjectCollection Quests = new SRObjectCollection(p.ReadByte());
			character[SRAttribute.Quests] = Quests;
			for (byte i = 0; i < Quests.Capacity; i++)
			{
				SRObject quest = new SRObject(p.ReadUInt(), SRObject.Type.Quest);
				Quests[i] = quest;

				quest[SRAttribute.Achievements] = p.ReadByte();
				quest[SRAttribute.isAutoShareRequired] = p.ReadByte() == 1;
				quest[SRAttribute.QuestType] = p.ReadByte();
				if ((byte)quest[SRAttribute.QuestType] == 28)
				{
					quest[SRAttribute.TimeRemain] = p.ReadUInt();
				}
				quest[SRAttribute.isActive] = p.ReadByte() == 1;
				if ((byte)quest[SRAttribute.QuestType] != 8)
				{
					SRObjectCollection Objectives = new SRObjectCollection(p.ReadByte());
					quest[SRAttribute.Objectives] = Objectives;

					for (int j = 0; j < Objectives.Capacity; j++)
					{
						Objectives[j] = new SRObject();
						Objectives[j][SRAttribute.ObjectiveID] = p.ReadByte();
						Objectives[j][SRAttribute.isActive] = p.ReadByte() == 1;
						Objectives[j][SRAttribute.Name] = p.ReadAscii();
						Objectives[j][SRAttribute.TaskValues] = p.ReadUInt32Array(p.ReadByte());
					}
				}
				if ((byte)quest[SRAttribute.QuestType] == 88)
				{
					quest[SRAttribute.NPCIDs] = p.ReadUInt32Array(p.ReadByte());
				}
			}
			#endregion
			character[SRAttribute.unkByte04] = p.ReadByte();
			#region (Collection Books)

			SRObjectCollection CollectionBooks = new SRObjectCollection(p.ReadUInt());
			character[SRAttribute.CollectionBooks] = CollectionBooks;
			for (int j = 0; j < CollectionBooks.Capacity; j++)
			{
				SRObject book = new SRObject();
				CollectionBooks[j] = book;

				book[SRAttribute.Number] = p.ReadUInt();
				book[SRAttribute.StartedDatetime] = p.ReadUInt();
				book[SRAttribute.Pages] = p.ReadUInt();
			}
			#endregion
			#region (Game Position)
			character[SRAttribute.UniqueID] = p.ReadUInt();
			character[SRAttribute.Region] = p.ReadUShort();
			character[SRAttribute.X] = p.ReadSingle();
			character[SRAttribute.Z] = p.ReadSingle();
			character[SRAttribute.Y] = p.ReadSingle();
			character[SRAttribute.Angle] = p.ReadUShort();

			character[SRAttribute.hasMovement] = p.ReadByte() == 1;
			character[SRAttribute.MovementType] = p.ReadByte();
			if ((bool)character[SRAttribute.hasMovement])
			{
				character[SRAttribute.MovementRegion] = p.ReadUShort();
				if (character.inDungeon())
				{
					// Dungeon offsets
					character[SRAttribute.MovementOffsetX] = p.ReadUInt();
					character[SRAttribute.MovementOffsetZ] = p.ReadUInt();
					character[SRAttribute.MovementOffsetY] = p.ReadUInt();
				}
				else
				{
					// World
					character[SRAttribute.MovementOffsetX] = p.ReadUShort();
					character[SRAttribute.MovementOffsetZ] = p.ReadUShort();
					character[SRAttribute.MovementOffsetY] = p.ReadUShort();
				}
			}
			else
			{
				character[SRAttribute.MovementSource] = p.ReadByte();
				character[SRAttribute.MovementAngle] = p.ReadUShort();
			}
			#endregion
			character[SRAttribute.LifeState] = p.ReadByte();
			character[SRAttribute.unkByte05] = p.ReadByte();
			character[SRAttribute.MotionState] = p.ReadByte();
			character[SRAttribute.PlayerStatus] = p.ReadByte();
			character[SRAttribute.SpeedWalking] = p.ReadSingle();
			character[SRAttribute.SpeedRunning] = p.ReadSingle();
			character[SRAttribute.SpeedBerserk] = p.ReadSingle();
			#region (Buffs)
			SRObjectCollection Buffs = new SRObjectCollection(p.ReadByte());
			character[SRAttribute.Buffs] = Buffs;

			for (int i = 0; i < Buffs.Capacity; i++)
			{
				SRObject buff = new SRObject(p.ReadUInt(), SRObject.Type.Skill);
				Buffs[i] = buff;

				buff[SRAttribute.Duration] = p.ReadUInt();
				if (buff.isAutoTransferEffect())
				{
					buff[SRAttribute.isCreator] = p.ReadByte() == 1;
				}
			}
			#endregion
			character[SRAttribute.Name] = p.ReadAscii();
			#region (Job & PVP)
			character[SRAttribute.JobName] = p.ReadAscii();
			character[SRAttribute.JobType] = p.ReadByte();
			character[SRAttribute.JobLevel] = p.ReadByte();
			character[SRAttribute.JobExp] = p.ReadUInt();
			character[SRAttribute.JobContribution] = p.ReadUInt();
			character[SRAttribute.JobReward] = p.ReadUInt();
			character[SRAttribute.PVPState] = p.ReadByte();
			character[SRAttribute.hasTransport] = p.ReadByte() == 1;
			character[SRAttribute.isFighting] = p.ReadByte() == 1;
			if ((bool)character[SRAttribute.hasTransport])
			{
				character[SRAttribute.TransportUniqueID] = p.ReadUInt();
			}
			character[SRAttribute.PVPCaptureTheFlagType] = p.ReadByte();
			character[SRAttribute.GuideFlag] = p.ReadULong();
			character[SRAttribute.JID] = p.ReadUInt();
			character[SRAttribute.GMFlag] = p.ReadByte();
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

			// Set the current character selected
			Info.Get.Character = character;
			foreach (SRObject charList in Info.Get.CharacterList)
			{
				// Copy all previous data saved on character selection
				if ((string)character[SRAttribute.Name] == (string)charList[SRAttribute.Name])
				{
					character.CopyAttributes(charList);
				}
			}

			// Updating GUI
			Window w = Window.Get;
			w.TESTING_AddSpawn(character);

			#region (Character)
			WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
				w.Character_lblLevel.Text = "Lv. " + (byte)character[SRAttribute.Level];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
				w.Character_pgbHP.Maximum = (int)((uint)character[SRAttribute.HPMax]);
				w.Character_pgbHP.Value = w.Character_pgbHP.Maximum;
			});
			WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
				w.Character_pgbMP.Maximum = (int)((uint)character[SRAttribute.MPMax]);
				w.Character_pgbMP.Value = w.Character_pgbMP.Maximum;
			});
			WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
				w.Character_pgbExp.Maximum = (int)((ulong)character[SRAttribute.ExpMax]);
				w.Character_pgbExp.Value = (int)((ulong)character[SRAttribute.Exp]);
			});
			#endregion

			WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
				w.Minimap_tbxX.Text = ((int)((float)character[SRAttribute.X])).ToString();
				w.Minimap_tbxY.Text = ((int)((float)character[SRAttribute.Y])).ToString();
				w.Minimap_tbxZ.Text = ((int)((float)character[SRAttribute.Z])).ToString();
				w.Minimap_tbxRegion.Text = ((ushort)character[SRAttribute.Region]).ToString();
			});
			Point coord = character.getPosition();
			WinAPI.InvokeIfRequired(w.Minimap_panelGameCoords, () => {
				w.Minimap_tbxGameX.Text = coord.X.ToString();
				w.Minimap_tbxGameY.Text = coord.Y.ToString();
			});
			w.Minimap_CharacterPointer_Move(coord.X, coord.Y, (float)character[SRAttribute.Z], (ushort)character[SRAttribute.Region]);
		}
		public static void CharacterInfoUpdate(Packet packet)
		{
			Info i = Info.Get;

			packet.ReadUInt();
			packet.ReadUInt();
			packet.ReadUInt();
			packet.ReadUInt();
			packet.ReadUShort();
			packet.ReadUShort();
			packet.ReadUShort();
			packet.ReadUShort();
			i.Character[SRAttribute.HPMax] = packet.ReadUInt();
			i.Character[SRAttribute.MPMax] = packet.ReadUInt();
			i.Character[SRAttribute.STR] = packet.ReadUShort();
			i.Character[SRAttribute.INT] = packet.ReadUShort();
			// End of Packet

			// Update GUI & game logic
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
				w.Character_pgbHP.Maximum = (int)((uint)i.Character[SRAttribute.HPMax]);
			});
			WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
				w.Character_pgbMP.Maximum = (int)((uint)i.Character[SRAttribute.MPMax]);
			});
			if ((uint)i.Character[SRAttribute.HP] > (uint)i.Character[SRAttribute.HPMax])
			{
				i.Character[SRAttribute.HP] = i.Character[SRAttribute.HPMax];
				WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
					w.Character_pgbHP.Value = (int)((uint)i.Character[SRAttribute.HP]);
				});
			}
			if ((uint)i.Character[SRAttribute.MP] > (uint)i.Character[SRAttribute.MPMax])
			{
				i.Character[SRAttribute.MP] = i.Character[SRAttribute.MPMax];
				WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
					w.Character_pgbMP.Value = (int)((uint)i.Character[SRAttribute.MP]);
				});
			}
		}
		public static void CharacterExperienceUpdate(Packet packet)
		{
			Info i = Info.Get;
			Window w = Window.Get;

			packet.ReadUInt(); // UniqueID from source exp.
			long ExpReceived = packet.ReadLong();
			long SPExpReceived = packet.ReadLong(); // Long SP EXP ?'?
			byte f = packet.ReadByte();
			// End of Packet

			if (w.Character_cbxMessageExp.Checked)
			{
				if(ExpReceived != 0)
					w.LogMessageFilter("["+ ExpReceived + "] experience points gained.");
				//if(SPExpReceived !=0 )
				//	w.LogMessageFilter("[" + SPExpReceived + "] skill experience points gained.");
			}
			CalculateExp(ExpReceived, (long)((ulong)i.Character[SRAttribute.Exp]), (long)((ulong)i.Character[SRAttribute.ExpMax]), (byte)i.Character[SRAttribute.Level]);
		}
		private static void CalculateExp(long ExpReceived,long Exp,long ExpMax,byte level)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				i.Character[SRAttribute.Level] = (byte)(level + 1);
				if ((byte)i.Character[SRAttribute.Level] > (byte)i.Character[SRAttribute.LevelMax])
				{
					i.Character[SRAttribute.LevelMax] = i.Character[SRAttribute.Level];
					Bot.Get.Event_LevelUp();
				}
				// Update new ExpMax
				i.Character[SRAttribute.ExpMax] = i.getMaxExp((byte)i.Character[SRAttribute.Level]);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.Maximum = (int)((ulong)i.Character[SRAttribute.ExpMax]);
				});
				CalculateExp((Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)i.Character[SRAttribute.ExpMax]), (byte)(level + 1));
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				i.Character[SRAttribute.Level] = (byte)(level - 1);
				// Update new ExpMax
				i.Character[SRAttribute.ExpMax] = i.getMaxExp((byte)i.Character[SRAttribute.Level]);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.Maximum = (int)((ulong)i.Character[SRAttribute.ExpMax]);
				});
				CalculateExp(Exp + ExpReceived, (long)((ulong)i.Character[SRAttribute.ExpMax]), (long)((ulong)i.Character[SRAttribute.ExpMax]), (byte)(level - 1));
			}
			else
			{
				// Increase/Decrease Exp
				i.Character[SRAttribute.Exp] = (ulong)(Exp + ExpReceived);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.Value = (int)((ulong)i.Character[SRAttribute.Exp]);
				});
			}
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
			groupSpawnPacket.WriteUInt8Array(packet.GetBytes());
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
			SRObject entity = new SRObject(packet.ReadUInt(), SRObject.Type.Entity);
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
					entity[SRAttribute.Scale] = packet.ReadByte();
					entity[SRAttribute.BerserkLevel] = packet.ReadByte();
					entity[SRAttribute.PVPCape] = packet.ReadByte();
					entity[SRAttribute.ExpType] = packet.ReadByte();
					// Inventory
					SRObjectCollection inventory = new SRObjectCollection(packet.ReadByte());
					byte inventoryCount = packet.ReadByte();
					for (byte i = 0; i < inventoryCount; i++)
					{
						inventory[i] = new SRObject(packet.ReadUInt(), SRObject.Type.Item);
						if (inventory[i].ID1 == 3 && inventory[i].ID2 == 1)
						{
							inventory[i][SRAttribute.ItemOptLevel] = packet.ReadByte();
						}
					}
					entity[SRAttribute.Inventory] = inventory;
					// AvatarInventory
					SRObjectCollection inventoryAvatar = new SRObjectCollection(packet.ReadByte());
					byte inventoryAvatarCount = packet.ReadByte();
					for (byte i = 0; i < inventoryAvatarCount; i++)
					{
						inventoryAvatar[i] = new SRObject(packet.ReadUInt(), SRObject.Type.Item);
						if (inventoryAvatar[i].ID1 == 3 && inventoryAvatar[i].ID2 == 1)
						{
							inventoryAvatar[i][SRAttribute.ItemOptLevel] = packet.ReadByte();
						}
					}
					entity[SRAttribute.InventoryAvatar] = inventoryAvatar;
					// Mask
					byte hasMask = packet.ReadByte();
					entity[SRAttribute.hasMask] = hasMask;
					if (hasMask == 1)
					{
						SRObject mask = new SRObject(packet.ReadUInt(), SRObject.Type.Model);
						if (mask.ID1 == entity.ID1 && mask.ID2 == entity.ID2)
						{
							// Clone
							mask[SRAttribute.Scale] = packet.ReadByte();
							SRObjectCollection maskItems = new SRObjectCollection(packet.ReadByte());
							for (int i = 0; i < maskItems.Capacity; i++)
							{
								maskItems[i] = new SRObject(packet.ReadUInt(), SRObject.Type.Item);
							}
							mask[SRAttribute.MaskItems] = maskItems;
						}
						entity[SRAttribute.Mask] = mask;
					}

				}
				else if (entity.ID2 == 2 && entity.ID3 == 5)
				{
					// NPC_FORTRESS_STRUCT
					entity[SRAttribute.HP] = packet.ReadUInt();
					entity[SRAttribute.refEventStructID] = packet.ReadUInt();
					entity[SRAttribute.LifeState] = packet.ReadUShort();
				}
				// Position
				entity[SRAttribute.UniqueID] = packet.ReadUInt();
				entity[SRAttribute.Region] = packet.ReadUShort();
				entity[SRAttribute.X] = packet.ReadSingle();
				entity[SRAttribute.Z] = packet.ReadSingle();
				entity[SRAttribute.Y] = packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();
				// Movement
				byte hasDestination = packet.ReadByte();
				entity[SRAttribute.hasMovement] = hasDestination;
				entity[SRAttribute.MovementType] = packet.ReadByte();
				if (hasDestination == 1)
				{
					// Mouse movement
					entity[SRAttribute.MovementRegion] = packet.ReadUShort();
					if ((ushort)entity[SRAttribute.Region] < short.MaxValue)
					{
						// World
						entity[SRAttribute.MovementOffsetX] = packet.ReadUShort();
						entity[SRAttribute.MovementOffsetZ] = packet.ReadUShort();
						entity[SRAttribute.MovementOffsetY] = packet.ReadUShort();
					}
					else
					{
						// Dungeon
						entity[SRAttribute.MovementOffsetX] = packet.ReadUInt();
						entity[SRAttribute.MovementOffsetZ] = packet.ReadUInt();
						entity[SRAttribute.MovementOffsetY] = packet.ReadUInt();
					}
				}
				else
				{
					entity[SRAttribute.MovementSource] = packet.ReadByte();
					// Represents the new angle, character is looking at
					entity[SRAttribute.MovementAngle] = packet.ReadUShort();
				}
				// State
				entity[SRAttribute.LifeState] = packet.ReadByte();
				entity[SRAttribute.unkByte3] = packet.ReadByte();
				entity[SRAttribute.MotionState] = packet.ReadByte();
				entity[SRAttribute.PlayerStatus] = packet.ReadByte();
				// Speed
				entity[SRAttribute.WalkSpeed] = packet.ReadSingle();
				entity[SRAttribute.RunSpeed] = packet.ReadSingle();
				entity[SRAttribute.BerserkSpeed] = packet.ReadSingle();
				// Buffs
				SRObjectCollection buffs = new SRObjectCollection(packet.ReadByte());
				for (int i = 0; i < buffs.Capacity; i++)
				{
					uint id = packet.ReadUInt();
					buffs[i] = new SRObject(id, SRObject.Type.Skill);
					buffs[i][SRAttribute.Duration] = packet.ReadUInt();
					if (buffs[i].isAutoTransferEffect())
					{
						buffs[i][SRAttribute.isCreator] = packet.ReadByte();
					}
				}
				entity[SRAttribute.Buffs] = buffs;
				if (entity.ID3 == 1)
				{
					// MOB
					packet.ReadByte();
					packet.ReadByte();
					packet.ReadByte();
					entity[SRAttribute.MobType] = packet.ReadByte();
				}
				else if (entity.ID2 == 1)
				{
					// CHARACTER
					entity[SRAttribute.Name] = packet.ReadAscii();
					entity[SRAttribute.JobType] = packet.ReadByte();
					entity[SRAttribute.JobLevel] = packet.ReadByte();
					entity[SRAttribute.PVPState] = packet.ReadByte();
					bool hasTransport = packet.ReadByte() == 1;
					entity[SRAttribute.hasTransport] = hasTransport;
					entity[SRAttribute.isFighting] = packet.ReadByte();
					if (hasTransport)
					{
						entity[SRAttribute.TransportUniqueID] = packet.ReadUInt();
					}
					entity[SRAttribute.ScrollMode] = packet.ReadByte();
					entity[SRAttribute.InteractMode] = packet.ReadByte();
					entity[SRAttribute.unkByte4] = packet.ReadByte();
					// Guild
					entity[SRAttribute.GuildName] = packet.ReadAscii();
					if (!((SRObjectCollection)entity[SRAttribute.Inventory]).ContainsJobEquipment())
					{
						entity[SRAttribute.GuildID] = packet.ReadUInt();
						entity[SRAttribute.GuildMemberName] = packet.ReadAscii();
						entity[SRAttribute.GuildLastCrestRev] = packet.ReadUInt();
						entity[SRAttribute.UnionID] = packet.ReadUInt();
						entity[SRAttribute.UnionLastCrestRev] = packet.ReadUInt();
						entity[SRAttribute.GuildisFriendly] = packet.ReadByte();
						entity[SRAttribute.GuildMemberAuthorityType] = packet.ReadByte();
					}
					if ((byte)entity[SRAttribute.InteractMode] == Types.InteractMode.P2N_TALK)
					{
						entity[SRAttribute.StallName] = packet.ReadAscii();
						entity[SRAttribute.DecorationItemID] = packet.ReadUInt();
					}
					entity[SRAttribute.EquipmentCooldown] = packet.ReadByte();
					entity[SRAttribute.PVPCaptureTheFlagType] = packet.ReadByte();
				}
				else if (entity.ID2 == 2)
				{
					// NPC
					entity[SRAttribute.hasTalk] = packet.ReadByte() != 0;
					if ((bool)entity[SRAttribute.hasTalk])
					{
						entity[SRAttribute.TalkOptions] = packet.ReadUInt8Array(packet.ReadByte());
					}
					if (entity.ID3 == 1)
					{
						// NPC_MOB
						entity[SRAttribute.Rarity] = packet.ReadByte();
						if (entity.ID4 == 2 || entity.ID4 == 4)
						{
							// has multiple appearances (Selected by server)
							entity[SRAttribute.Appearance] = packet.ReadByte();
						}
					}
					if (entity.ID3 == 3)
					{
						// NPC_COS
						if (entity.ID4 == 3 || entity.ID4 == 4)
						{
							//NPC_COS_P (Growth / Ability)
							entity[SRAttribute.Name] = packet.ReadAscii();
						}
						if (entity.ID4 == 5)
						{
							// NPC_COS_GUILD
							entity[SRAttribute.GuildName] = packet.ReadAscii();
						}
						else if (entity.ID4 != 1)
						{
							entity[SRAttribute.OwnerName] = packet.ReadAscii();
						}
						if (entity.ID4 == 2 || entity.ID4 == 3 || entity.ID4 == 4 || entity.ID4 == 5)
						{
							// NPC_COS_T
							// NPC_COS_P (Growth / Ability)
							// NPC_COS_GUILD
							entity[SRAttribute.JobType] = packet.ReadByte();
							if (entity.ID4 != 4)
							{
								// Not pet pick (Ability)
								entity[SRAttribute.PVPState] = packet.ReadByte();
							}
							if (entity.ID4 == 5)
							{
								// NPC_COS_GUILD
								entity[SRAttribute.OwnerRefObjID] = packet.ReadUInt();
							}
						}
						if (entity.ID4 != 1)
						{
							entity[SRAttribute.OwnerUniqueID] = packet.ReadUInt();
						}
					}
					else if (entity.ID3 == 4)
					{
						// NPC_FORTRESS_COS
						entity[SRAttribute.GuildID] = packet.ReadUInt();
						entity[SRAttribute.GuildName] = packet.ReadAscii();
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
					entity[SRAttribute.ItemOptLevel] = packet.ReadByte();
				}
				else if (entity.ID2 == 3)
				{
					// ITEM_ETC
					if (entity.ID3 == 5 && entity.ID4 == 0)
					{
						// ITEM_ETC_MONEY_GOLD
						entity[SRAttribute.Gold] = packet.ReadUInt();
					}
					else if (entity.ID3 == 8 || entity.ID3 == 9)
					{
						// ITEM_ETC_TRADE
						// ITEM_ETC_QUEST
						entity[SRAttribute.OwnerName] = packet.ReadAscii();
					}
				}
				entity[SRAttribute.UniqueID] = packet.ReadUInt();
				entity[SRAttribute.Region] = packet.ReadUShort();
				entity[SRAttribute.X] = packet.ReadSingle();
				entity[SRAttribute.Z] = packet.ReadSingle();
				entity[SRAttribute.Y] = packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();
				entity[SRAttribute.hasOwner] = packet.ReadByte() == 1;
				if ((bool)entity[SRAttribute.hasOwner])
				{
					entity[SRAttribute.OwnerJID] = packet.ReadUInt();
				}
				entity[SRAttribute.Rarity] = packet.ReadByte();
			}
			else if (entity.ID1 == 4)
			{
				// PORTALS
				// - STORE
				// - INS_TELEPORTER
				entity[SRAttribute.UniqueID] = packet.ReadUInt();
				entity[SRAttribute.Region] = packet.ReadUShort();
				entity[SRAttribute.X] = packet.ReadSingle();
				entity[SRAttribute.Y] = packet.ReadSingle();
				entity[SRAttribute.Z] = packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();

				entity[SRAttribute.unkByte0] = packet.ReadByte();
				entity[SRAttribute.unkByte1] = packet.ReadByte();
				entity[SRAttribute.unkByte2] = packet.ReadByte();
				entity[SRAttribute.unkByte3] = packet.ReadByte();
				if ((byte)entity[SRAttribute.unkByte3] == 1)
				{
					// Regular
					entity[SRAttribute.unkUInt0] = packet.ReadUInt();
					entity[SRAttribute.unkUInt1] = packet.ReadUInt();
				}
				else if ((byte)entity[SRAttribute.unkByte3] == 3)
				{
					// Dimensional Hole
					entity[SRAttribute.OwnerName] = packet.ReadAscii();
					entity[SRAttribute.OwnerUniqueID] = packet.ReadUInt();
				}
				if ((byte)entity[SRAttribute.unkByte1] == 1)
				{
					// STORE_EVENTZONE_DEFAULT
					entity[SRAttribute.unkUInt2] = packet.ReadUInt();
					entity[SRAttribute.unkByte5] = packet.ReadByte();
				}
			}
			else if (entity.ID == uint.MaxValue)
			{
				// EVENT_ZONE (Traps, Buffzones, ...)
				entity[SRAttribute.unkUShort0] = packet.ReadUShort();
				entity[SRAttribute.refSkillID] = packet.ReadUInt();
				entity[SRAttribute.UniqueID] = packet.ReadUInt();
				entity[SRAttribute.Region] = packet.ReadUShort();
				entity[SRAttribute.X] = packet.ReadSingle();
				entity[SRAttribute.Z] = packet.ReadSingle();
				entity[SRAttribute.Y] = packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();

				NameValueCollection data = Info.Get.getSkill((uint)entity[SRAttribute.refSkillID]);
				entity[SRAttribute.Servername] = data["servername"];
        entity[SRAttribute.Name] = data["name"];
      }
			if (packet.Opcode == Agent.Opcode.SERVER_ENTITY_SPAWN)
			{
				if (entity.ID1 == 1 || entity.ID1 == 4)
				{
					// BIONIC or STORE
					entity[SRAttribute.unkByte6] = packet.ReadByte();
				}
				else if (entity.ID1 == 3)
				{
					// DROP
					entity[SRAttribute.DropSource] = packet.ReadByte();
					entity[SRAttribute.DropUniqueID] = packet.ReadUInt();
				}
			}
			// End of Packet

			// Keep the track of the entity
			Info.Get.EntityList.Add(entity);

			// Update GUI
			Window w = Window.Get;
			w.TESTING_AddSpawn(entity);
			w.Minimap_ObjectPointer_Add((uint)entity[SRAttribute.UniqueID], (string)entity[SRAttribute.Servername], (string)entity[SRAttribute.Name], (float)entity[SRAttribute.X], (float)entity[SRAttribute.Y], (float)entity[SRAttribute.Z], (ushort)entity[SRAttribute.Region]);
		}
		public static void EntityDespawn(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			// End of Packet

			// Keep the track of the entity
			SRObject entity = Info.Get.getEntity(uniqueID);
			Info.Get.EntityList.Remove(entity);

			// Update GUI
			Window w = Window.Get;
			w.TESTING_RemoveSpawn(uniqueID);
			w.Minimap_ObjectPointer_Remove(uniqueID);
		}
		public static void EntityMovement(Packet packet)
		{
			SRObject entity = new SRObject();
			entity[SRAttribute.UniqueID] = packet.ReadUInt();
			entity[SRAttribute.hasMovement] = packet.ReadByte() == 1;
			if ((bool)entity[SRAttribute.hasMovement])
			{
				entity[SRAttribute.MovementRegion] = packet.ReadUShort();
				if (entity.inDungeon((ushort)entity[SRAttribute.MovementRegion]))
				{
					entity[SRAttribute.MovementOffsetX] = packet.ReadInt32();
					entity[SRAttribute.MovementOffsetZ] = packet.ReadInt32();
					entity[SRAttribute.MovementOffsetY] = packet.ReadInt32();
				}
				else
				{
					entity[SRAttribute.MovementOffsetX] = packet.ReadInt16();
					entity[SRAttribute.MovementOffsetZ] = packet.ReadInt16();
					entity[SRAttribute.MovementOffsetY] = packet.ReadInt16();
				}
			}
			bool flag = packet.ReadByte() == 1;
			if (flag)
			{
				packet.ReadUShort(); // Region
				packet.ReadUShort(); // ???
				packet.ReadUShort(); // ???
				packet.ReadUShort(); // ???
				packet.ReadUShort(); // ???
			}
			// End of Packet

			// Keep the track of the entity
			SRObject e = Info.Get.getEntity((uint)entity[SRAttribute.UniqueID]);
			e.CopyAttributes(entity,true);

			// Update Minimap Movement
			if ((bool)entity[SRAttribute.hasMovement])
			{
				Window w = Window.Get;
				Point p = e.getMovementPosition();
				if (e == Info.Get.Character)
				{
					WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
						w.Minimap_tbxX.Text = ((short)entity[SRAttribute.MovementOffsetX]).ToString();
						w.Minimap_tbxY.Text = ((short)entity[SRAttribute.MovementOffsetY]).ToString();
						w.Minimap_tbxZ.Text = ((short)entity[SRAttribute.MovementOffsetZ]).ToString();
						w.Minimap_tbxRegion.Text = ((ushort)entity[SRAttribute.MovementRegion]).ToString();
					});
					WinAPI.InvokeIfRequired(w.Minimap_panelGameCoords, () => {
						w.Minimap_tbxGameX.Text = p.X.ToString();
						w.Minimap_tbxGameY.Text = p.Y.ToString();
					});
					w.Minimap_CharacterPointer_Move(p.X, p.Y, (short)entity[SRAttribute.MovementOffsetZ], (ushort)entity[SRAttribute.MovementRegion]);
				}
				else
				{
					w.Minimap_ObjectPointer_Move((uint)entity[SRAttribute.UniqueID], p.X, p.Y, (short)entity[SRAttribute.MovementOffsetZ], (ushort)entity[SRAttribute.MovementRegion]);
				}
			}
		}
		public static void EnviromentCelestialPosition(Packet packet)
		{
			Info i = Info.Get;
			i.Character[SRAttribute.UniqueID] = packet.ReadUInt();
			i.Moonphase = packet.ReadUShort();
			i.Hour = packet.ReadByte();
			i.Minute = packet.ReadByte();
			// End of Packet
		}
		public static void ChatUpdate(Packet packet)
		{
			byte type = packet.ReadByte();
			string player = "";
			switch (type)
			{
				case Types.Chat.All:
				case Types.Chat.GM:
				case Types.Chat.NPC:
					uint playerID = packet.ReadUInt();
					SRObject p = Info.Get.getEntity(playerID);
					if (p != null && p.Contains(SRAttribute.Name))
						player = (string)p[SRAttribute.Name];
					else
						player = "[UID:" + playerID + "]";
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

			Window w = Window.Get;
			// Chat GUI filter
			switch (type)
			{
				case Types.Chat.All:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					break;
				case Types.Chat.GM:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					break;
				case Types.Chat.NPC:
					w.LogChatMessage(w.Chat_rtbxAll, player, message);
					break;
				case Types.Chat.Private:
					w.LogChatMessage(w.Chat_rtbxPrivate, player, message);
					break;
				case Types.Chat.Party:
					w.LogChatMessage(w.Chat_rtbxParty, player, message);
					break;
				case Types.Chat.Guild:
					w.LogChatMessage(w.Chat_rtbxGuild, player, message);
					break;
				case Types.Chat.Union:
					w.LogChatMessage(w.Chat_rtbxUnion, player, message);
					break;
				case Types.Chat.Academy:
					w.LogChatMessage(w.Chat_rtbxAcademy, player, message);
					break;
				case Types.Chat.Global:
					w.LogChatMessage(w.Chat_rtbxGlobal, player, message);
					break;
				case Types.Chat.Stall:
					w.LogChatMessage(w.Chat_rtbxStall, player, message);
					break;
				case Types.Chat.Notice:
					w.LogChatMessage(w.Chat_rtbxAll,"(Notice)", message);
					break;
			}
		}
		public static void EnviromentCelestialUpdate(Packet packet)
		{
			Info i = Info.Get;
			i.Moonphase = packet.ReadUShort();
			i.Hour = packet.ReadByte();
			i.Minute = packet.ReadByte();
			// End of Packet
		}
		public static void EntityLevelUp(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();

			Info i = Info.Get;
			if (uniqueID == (uint)i.Character[SRAttribute.UniqueID])
			{
				// Unneccesary packet since everything
				// is calculated on SERVER_CHARACTER_EXPERIENCE_UPDATE
				Window w =	Window.Get;
				if (w.Character_cbxMessageExp.Checked)
					w.LogMessageFilter("Level Up!");
			}
		}
		public static void EntityBarUpdate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			packet.ReadByte();
			packet.ReadByte();

			Info i = Info.Get;
			SRObject entity = i.getEntity(uniqueID);
			if (entity == null)
				return;

			Window w = Window.Get;
			byte barType = packet.ReadByte();
			switch (barType)
			{
				case Types.BarUpdate.HP:
					entity[SRAttribute.HP] = packet.ReadUInt();
					break;
				case Types.BarUpdate.MP:
					entity[SRAttribute.MP] = packet.ReadUInt();
					break;
				case Types.BarUpdate.HPMP:
				case Types.BarUpdate.EntityHPMP:
					entity[SRAttribute.HP] = packet.ReadUInt();
					entity[SRAttribute.MP] = packet.ReadUInt();
					break;
				case Types.BarUpdate.BadStatus:
					entity[SRAttribute.hasBadStatus] = packet.ReadByte() == 1;
					break;
			}
			// End of Packet
			
			if (entity == i.Character)
			{
				if (barType == Types.BarUpdate.HP || barType == Types.BarUpdate.HPMP)
				{
					WinAPI.InvokeIfRequired(w.Character_pgbHP, () =>
					{
						w.Character_pgbHP.Value = (int)((uint)entity[SRAttribute.HP]);
					});
					Bot.Get.Event_BarUpdated();
				}
				if (barType == Types.BarUpdate.MP || barType == Types.BarUpdate.HPMP)
				{
					WinAPI.InvokeIfRequired(w.Character_pgbMP, () =>
					{
						w.Character_pgbMP.Value = (int)((uint)entity[SRAttribute.MP]);
					});
					Bot.Get.Event_BarUpdated();
				}
			}
			else
			{
				w.TESTING_EditSpawn(entity, SRAttribute.HP);
				w.TESTING_EditSpawn(entity, SRAttribute.MP);
			}
		}
	}
}