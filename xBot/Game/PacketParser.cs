using System;
using SecurityAPI;
using System.Windows.Forms;
using System.Drawing;
using xBot.Network;
using System.Collections.Generic;

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
					Bot.Get._Event_NicknameChecked(result == 1);
					break;
				case Types.CharacterSelectionAction.Delete:
					// Not necessary at the moment..
					break;
				case Types.CharacterSelectionAction.List:
					if (result == 1)
					{
						Window w = Window.Get;
						Bot b = Bot.Get;
						// Reset values
						WinAPI.InvokeIfRequired(w.Login_lstvCharacters, () =>
						{
							w.Login_lstvCharacters.Items.Clear();
						});
						WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () =>
						{
							w.Login_cmbxCharacter.Items.Clear();
						});

						List<SRObject> CharacterList = new List<SRObject>();
						// Start packet reading 
						byte nChars = packet.ReadByte();
						for (int n = 0; n < nChars; n++)
						{
							SRObject character = new SRObject(packet.ReadUInt(), SRObject.Type.Model);
							character[SRAttribute.Name] = packet.ReadAscii();
							character[SRAttribute.Scale] = packet.ReadByte();
							character[SRAttribute.Level] = packet.ReadByte();
							character[SRAttribute.Exp] = packet.ReadULong();
							character[SRAttribute.ExpMax] = Info.Get.GetExpMax((byte)character[SRAttribute.Level]);
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
							character[SRAttribute.GuildMemberType] = (Types.GuildMember)packet.ReadByte();
							character[SRAttribute.isGuildRenameRequired] = packet.ReadByte() == 1;
							if ((bool)character[SRAttribute.isGuildRenameRequired])
							{
								character[SRAttribute.GuildName] = packet.ReadAscii();
							}
							character[SRAttribute.AcademyMemberType] = (Types.AcademyMember)packet.ReadByte();
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
							CharacterList.Add(character);

							// Filling data to GUI
							ListViewItem l = new ListViewItem();
							l.Text = character[SRAttribute.Name] + ((bool)character[SRAttribute.isDeleting] ? " (*)" : "");
							l.Name = (string)character[SRAttribute.Name];
							l.SubItems.Add(character[SRAttribute.Level].ToString());
							l.SubItems.Add(Math.Round(character.GetExpPercent(), 2) + " %");
							l.SubItems.Add(character.ID.ToString());
							WinAPI.InvokeIfRequired(w.Login_lstvCharacters, () => {
								w.Login_lstvCharacters.Items.Add(l);
							});
							if (!(bool)character[SRAttribute.isDeleting])
							{
								WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () => {
									w.Login_cmbxCharacter.Items.Add(l.Name);
									// Select Charname if AutoLogin is activated
									if (b.hasAutoLoginMode
									&& w.Login_cmbxCharacter.Tag != null
									&& ((string)w.Login_cmbxCharacter.Tag).ToLower() == l.Name.ToLower())
									{
										w.Login_cmbxCharacter.Text = l.Name;
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

						Bot.Get._Event_CharacterListing(CharacterList);
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
		}
		public static void CharacterData(Packet packet)
		{
			characterDataPacket.WriteUInt8Array(packet.GetBytes());
		}
		public static void CharacterDataEnd(Packet packet)
		{
			Packet p = characterDataPacket;
			p.Lock();

			Info.Get.ServerTime = p.ReadUInt();
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
			character[SRAttribute.GatheredExpPoint] = p.ReadUInt();
			character[SRAttribute.HPMax] = p.ReadUInt();
			character[SRAttribute.MPMax] = p.ReadUInt();
			character[SRAttribute.ExpIconType] = (Types.ExpIcon)p.ReadByte();
			character[SRAttribute.PKDaily] = p.ReadByte();
			character[SRAttribute.PKTotal] = p.ReadUShort();
			character[SRAttribute.PKPenalty] = p.ReadUInt();
			character[SRAttribute.BerserkLevel] = p.ReadByte();
			character[SRAttribute.PVPCapeType] = (Types.PVPCape)p.ReadByte();
			#region (Inventory)
			SRObjectCollection Inventory = new SRObjectCollection(p.ReadByte());
			character[SRAttribute.Inventory] = Inventory;

			byte itemsCount = p.ReadByte();
			for (byte i = 0; i < itemsCount; i++)
			{
				byte slot = p.ReadByte();
				Inventory[slot] = ItemParsing(p);
			}
			#endregion
			#region (Inventory Avatar)
			SRObjectCollection InventoryAvatar = new SRObjectCollection(p.ReadByte());
			character[SRAttribute.InventoryAvatar] = InventoryAvatar;

			itemsCount = p.ReadByte();
			for (byte i = 0; i < itemsCount; i++)
			{
				byte slot = p.ReadByte();
				InventoryAvatar[slot] = ItemParsing(p);
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
			character[SRAttribute.X] = (int)p.ReadSingle();
			character[SRAttribute.Z] = (int)p.ReadSingle();
			character[SRAttribute.Y] = (int)p.ReadSingle();
			character[SRAttribute.Angle] = p.ReadUShort();

			character[SRAttribute.hasMovement] = p.ReadByte() == 1;
			character[SRAttribute.MovementType] = p.ReadByte();
			if ((bool)character[SRAttribute.hasMovement])
			{
				character[SRAttribute.MovementRegion] = p.ReadUShort();
				if (character.inDungeon((ushort)character[SRAttribute.MovementRegion]))
				{
					// Dungeon offsets
					character[SRAttribute.MovementOffsetX] = p.ReadInt();
					character[SRAttribute.MovementOffsetZ] = p.ReadInt();
					character[SRAttribute.MovementOffsetY] = p.ReadInt();
				}
				else
				{
					// World
					character[SRAttribute.MovementOffsetX] = (int)(p.ReadShort());
					character[SRAttribute.MovementOffsetZ] = (int)(p.ReadShort());
					character[SRAttribute.MovementOffsetY] = (int)(p.ReadShort());
				}
			}
			else
			{
				character[SRAttribute.MovementActionType] = (Types.MovementAction)p.ReadByte();
				character[SRAttribute.MovementAngle] = p.ReadUShort();
			}
			#endregion
			character[SRAttribute.LifeState] = (Types.LifeState)p.ReadByte();
			character[SRAttribute.unkByte05] = p.ReadByte(); // (Possibly bad status flag)
			character[SRAttribute.MotionState] = (Types.MotionState)p.ReadByte();
			character[SRAttribute.PlayerState] = (Types.PlayerState)p.ReadByte();
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
			character[SRAttribute.JobType] = (Types.Job)p.ReadByte();
			character[SRAttribute.JobLevel] = p.ReadByte();
			character[SRAttribute.JobExp] = p.ReadUInt();
			character[SRAttribute.JobContribution] = p.ReadUInt();
			character[SRAttribute.JobReward] = p.ReadUInt();
			character[SRAttribute.PVPState] = (Types.PVPState)p.ReadByte();
			character[SRAttribute.isRiding] = p.ReadByte() == 1;
			character[SRAttribute.isFighting] = p.ReadByte() == 1;
			if ((bool)character[SRAttribute.isRiding])
			{
				character[SRAttribute.RidingUniqueID] = p.ReadUInt();
			}
			character[SRAttribute.CaptureTheFlagType] = (Types.CaptureTheFlag)p.ReadByte();
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

			Info info = Info.Get;

			// Initializing basic bars
			character[SRAttribute.ExpMax] = info.GetExpMax((byte)character[SRAttribute.Level]);
			character[SRAttribute.JobExpMax] = info.GetJobExpMax((byte)character[SRAttribute.JobLevel], (Types.Job)character[SRAttribute.JobType]);
			character[SRAttribute.HP] = character[SRAttribute.HPMax];
			character[SRAttribute.MP] = character[SRAttribute.MPMax];

			// Set the current character or update the previously saved
			if (info.Character != null)
				character.CopyAttributes(info.Character);
			info.Character = character;

			// Updating GUI
			Window w = Window.Get;
			//w.GameInfo_AddSpawn(character);

			#region (Character)
			WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
				w.Character_pgbHP.ValueMaximum = (uint)character[SRAttribute.HPMax];
				w.Character_pgbHP.Value = w.Character_pgbHP.ValueMaximum;
			});
			WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
				w.Character_pgbMP.ValueMaximum = (uint)character[SRAttribute.MPMax];
				w.Character_pgbMP.Value = w.Character_pgbMP.ValueMaximum;
			});
			WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
				w.Character_lblLevel.Text = "Lv. " + character[SRAttribute.Level];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
				w.Character_pgbExp.ValueMaximum = (ulong)character[SRAttribute.ExpMax];
				w.Character_pgbExp.Value = (ulong)character[SRAttribute.Exp];
			});
			WinAPI.InvokeIfRequired(w.Character_lblJobLevel, () => {
				w.Character_lblJobLevel.Text = "Job Lv. " + character[SRAttribute.JobLevel];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbJobExp, () => {
				w.Character_pgbJobExp.ValueMaximum = (uint)character[SRAttribute.JobExpMax];
				w.Character_pgbJobExp.Value = (uint)character[SRAttribute.JobExp];
			});
			w.Character_SetGold((ulong)character[SRAttribute.Gold]);
			WinAPI.InvokeIfRequired(w.Character_lblLocation, () => {
				w.Character_lblLocation.Text = Info.Get.GetRegion((ushort)character[SRAttribute.Region]);
			});
			WinAPI.InvokeIfRequired(w.Character_lblSP, () => {
				w.Character_lblSP.Text = character[SRAttribute.SP].ToString();
			});
			WinAPI.InvokeIfRequired(w.Character_gbxStatPoints, () => {
				w.Character_lblStatPoints.Text = character[SRAttribute.StatPoints].ToString();
				if ((ushort)character[SRAttribute.StatPoints] > 0)
					w.Character_btnAddSTR.Enabled = w.Character_btnAddINT.Enabled = true;
			});
			#endregion

			#region (Minimap)
			WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
				w.Minimap_tbxX.Text = ((int)character[SRAttribute.X]).ToString();
				w.Minimap_tbxY.Text = ((int)character[SRAttribute.Y]).ToString();
				w.Minimap_tbxZ.Text = ((int)character[SRAttribute.Z]).ToString();
				w.Minimap_tbxRegion.Text = ((ushort)character[SRAttribute.Region]).ToString();
			});
			Point coord = character.GetPosition((ushort)character[SRAttribute.Region], (int)character[SRAttribute.X], (int)character[SRAttribute.Y], (int)character[SRAttribute.Z]);
			WinAPI.InvokeIfRequired(w.Minimap_panelGameCoords, () => {
				w.Minimap_tbxCoordX.Text = coord.X.ToString();
				w.Minimap_tbxCoordY.Text = coord.Y.ToString();
			});
			w.Minimap_CharacterPointer_Move((int)character[SRAttribute.X], (int)character[SRAttribute.Y], (int)character[SRAttribute.Z], (ushort)character[SRAttribute.Region]);
			#endregion
		}
		private static SRObject ItemParsing(Packet p)
		{
			SRObject item = new SRObject();

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
					for (byte j = 0; j < MagicParams.Capacity; j++)
					{
						MagicParams[j] = new SRObject();
						MagicParams[j][SRAttribute.Type] = p.ReadUInt();
						MagicParams[j][SRAttribute.Value] = p.ReadUInt();
					}
					item[SRAttribute.MagicParams] = MagicParams;
					// 1 = Socket
					p.ReadByte();
					SRObjectCollection SocketParams = new SRObjectCollection(p.ReadByte());
					for (byte j = 0; j < SocketParams.Capacity; j++)
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
					for (byte j = 0; j < AdvanceElixirParams.Capacity; j++)
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
						item[SRAttribute.PetState] = (Types.PetState)p.ReadByte();
						switch ((Types.PetState)item[SRAttribute.PetState])
						{
							case Types.PetState.Summoned:
							case Types.PetState.Unsummoned:
							case Types.PetState.Dead:
								item[SRAttribute.ModelID] = p.ReadUInt();
								item[SRAttribute.PetName] = p.ReadAscii();
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
						item[SRAttribute.ModelID] = p.ReadUInt();
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
						for (byte j = 0; j < MagicParams.Capacity; j++)
						{
							MagicParams[j] = new SRObject();
							MagicParams[j][SRAttribute.Type] = p.ReadUInt();
							MagicParams[j][SRAttribute.Value] = p.ReadUInt();
						}
						item[SRAttribute.MagicParams] = MagicParams;
					}
				}
			}
			return item;
		}
		public static void CharacterStatsUpdate(Packet packet)
		{
			Info i = Info.Get;

			uint PhyAtkMin = packet.ReadUInt();
			uint PhyAtkMax = packet.ReadUInt();
			uint MagAtkMin = packet.ReadUInt();
			uint MagAtkMax = packet.ReadUInt();
			ushort PhyDefense = packet.ReadUShort();
			ushort MagDefense = packet.ReadUShort();
			ushort HitRate = packet.ReadUShort();
			ushort ParryRatio = packet.ReadUShort();
			i.Character[SRAttribute.HPMax] = packet.ReadUInt();
			i.Character[SRAttribute.MPMax] = packet.ReadUInt();
			i.Character[SRAttribute.STR] = packet.ReadUShort();
			i.Character[SRAttribute.INT] = packet.ReadUShort();
			// End of Packet

			// Update GUI & game logic
			Window w = Window.Get;
			WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
				w.Character_pgbHP.ValueMaximum = (uint)i.Character[SRAttribute.HPMax];
			});
			WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
				w.Character_pgbMP.ValueMaximum = (uint)i.Character[SRAttribute.MPMax];
			});
			if ((uint)i.Character[SRAttribute.HP] > (uint)i.Character[SRAttribute.HPMax])
			{
				i.Character[SRAttribute.HP] = i.Character[SRAttribute.HPMax];
				WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
					w.Character_pgbHP.Value = (uint)i.Character[SRAttribute.HP];
				});
			}
			if ((uint)i.Character[SRAttribute.MP] > (uint)i.Character[SRAttribute.MPMax])
			{
				i.Character[SRAttribute.MP] = i.Character[SRAttribute.MPMax];
				WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
					w.Character_pgbMP.Value = (uint)i.Character[SRAttribute.MP];
				});
			}
			WinAPI.InvokeIfRequired(w.Character_lblSTR, () => {
				w.Character_lblSTR.Text = i.Character[SRAttribute.STR].ToString();
			});
			WinAPI.InvokeIfRequired(w.Character_lblINT, () => {
				w.Character_lblINT.Text = i.Character[SRAttribute.INT].ToString();
			});
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
			CalculateExp(ExpReceived, (long)((ulong)i.Character[SRAttribute.Exp]), (long)((ulong)i.Character[SRAttribute.ExpMax]), (byte)i.Character[SRAttribute.Level]);
		}
		private static void CalculateExp(long ExpReceived, long Exp, long ExpMax, byte level)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				i.Character[SRAttribute.Level] = (byte)(level + 1);
				WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
					w.Character_lblLevel.Text = "Lv. " + i.Character[SRAttribute.Level];
				});
				// Update new ExpMax
				i.Character[SRAttribute.ExpMax] = i.GetExpMax((byte)i.Character[SRAttribute.Level]);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.ValueMaximum = (ulong)i.Character[SRAttribute.ExpMax];
				});
				// Update max. level reached
				if ((byte)i.Character[SRAttribute.Level] > (byte)i.Character[SRAttribute.LevelMax])
				{
					i.Character[SRAttribute.LevelMax] = i.Character[SRAttribute.Level];
					i.Character[SRAttribute.StatPoints] = (ushort)((ushort)i.Character[SRAttribute.StatPoints] + 3);
					WinAPI.InvokeIfRequired(w.Character_gbxStatPoints, () => {
						w.Character_lblStatPoints.Text = i.Character[SRAttribute.StatPoints].ToString();
						w.Character_btnAddSTR.Enabled = w.Character_btnAddINT.Enabled = true;
					});
					// Generate bot event
					Bot.Get.Event_LevelUp((byte)(i.Character[SRAttribute.Level]));
				}
				// Continue recursivity
				CalculateExp((Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)i.Character[SRAttribute.ExpMax]), (byte)(level + 1));
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				i.Character[SRAttribute.Level] = (byte)(level - 1);
				WinAPI.InvokeIfRequired(w.Character_lblLevel, () => {
					w.Character_lblLevel.Text = ((byte)i.Character[SRAttribute.Level]).ToString();
				});
				// Update new ExpMax
				i.Character[SRAttribute.ExpMax] = i.GetExpMax((byte)i.Character[SRAttribute.Level]);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.ValueMaximum = (ulong)i.Character[SRAttribute.ExpMax];
				});
				CalculateExp(Exp + ExpReceived, (long)((ulong)i.Character[SRAttribute.ExpMax]), (long)((ulong)i.Character[SRAttribute.ExpMax]), (byte)(level - 1));
			}
			else
			{
				// Increase/Decrease Exp
				i.Character[SRAttribute.Exp] = (ulong)(Exp + ExpReceived);
				WinAPI.InvokeIfRequired(w.Character_pgbExp, () => {
					w.Character_pgbExp.Value = (ulong)i.Character[SRAttribute.Exp];
				});
			}
		}
		public static void CharacterInfoUpdate(Packet packet)
		{
			Window w = Window.Get;
			Info i = Info.Get;

			byte type = packet.ReadByte();
			switch (type)
			{
				case 1: // Gold
					i.Character[SRAttribute.Gold] = packet.ReadULong();
					w.Character_SetGold((ulong)i.Character[SRAttribute.Gold]);
					break;
				case 2: // SP
					i.Character[SRAttribute.SP] = packet.ReadUInt();
					WinAPI.InvokeIfRequired(w.Character_lblSP, () => {
						w.Character_lblSP.Text = i.Character[SRAttribute.SP].ToString();
					});
					break;
				case 4: // Berserk
					i.Character[SRAttribute.BerserkLevel] = packet.ReadByte();
					break;
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
					entity[SRAttribute.PVPCapeType] = (Types.PVPCape)packet.ReadByte();
					entity[SRAttribute.ExpIconType] = (Types.ExpIcon)packet.ReadByte();
					// Inventory
					packet.ReadByte(); // max capacity. seems useless at the moment..
					SRObjectCollection inventory = new SRObjectCollection();
					byte inventoryCount = packet.ReadByte();
					for (byte i = 0; i < inventoryCount; i++)
					{
						inventory[i] = new SRObject(packet.ReadUInt(), SRObject.Type.Item);
						if (inventory[i].ID1 == 3 && inventory[i].ID2 == 1)
						{
							inventory[i][SRAttribute.Plus] = packet.ReadByte();
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
							inventoryAvatar[i][SRAttribute.Plus] = packet.ReadByte();
						}
					}
					entity[SRAttribute.InventoryAvatar] = inventoryAvatar;
					// Mask
					entity[SRAttribute.hasMask] = packet.ReadByte() == 1;
					if ((bool)entity[SRAttribute.hasMask])
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
					entity[SRAttribute.LifeState] = (Types.LifeState)packet.ReadUShort();
				}
				// Position
				entity[SRAttribute.UniqueID] = packet.ReadUInt();
				entity[SRAttribute.Region] = packet.ReadUShort();
				entity[SRAttribute.X] = (int)packet.ReadSingle();
				entity[SRAttribute.Z] = (int)packet.ReadSingle();
				entity[SRAttribute.Y] = (int)packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();
				// Movement
				entity[SRAttribute.hasMovement] = packet.ReadByte() == 1;
				entity[SRAttribute.MovementType] = packet.ReadByte();
				if ((bool)entity[SRAttribute.hasMovement])
				{
					// Mouse movement
					entity[SRAttribute.MovementRegion] = packet.ReadUShort();
					if ((ushort)entity[SRAttribute.Region] < short.MaxValue)
					{
						// World
						entity[SRAttribute.MovementOffsetX] = (int)packet.ReadInt16();
						entity[SRAttribute.MovementOffsetZ] = (int)packet.ReadInt16();
						entity[SRAttribute.MovementOffsetY] = (int)packet.ReadInt16();
					}
					else
					{
						// Dungeon
						entity[SRAttribute.MovementOffsetX] = packet.ReadInt();
						entity[SRAttribute.MovementOffsetZ] = packet.ReadInt();
						entity[SRAttribute.MovementOffsetY] = packet.ReadInt();
					}
					entity[SRAttribute.MovementDate] = DateTime.Now;
				}
				else
				{
					entity[SRAttribute.MovementActionType] = (Types.MovementAction)packet.ReadByte();
					entity[SRAttribute.MovementAngle] = packet.ReadUShort();
				}
				// States
				entity[SRAttribute.LifeState] = (Types.LifeState)packet.ReadByte();
				entity[SRAttribute.unkByte01] = packet.ReadByte(); // Possibly bad status flag
				entity[SRAttribute.MotionState] = (Types.MotionState)packet.ReadByte();
				entity[SRAttribute.PlayerState] = (Types.PlayerState)packet.ReadByte();
				// Speed
				entity[SRAttribute.SpeedWalking] = packet.ReadSingle();
				entity[SRAttribute.SpeedRunning] = packet.ReadSingle();
				entity[SRAttribute.SpeedBerserk] = packet.ReadSingle();
				// Buffs
				SRObjectCollection buffs = new SRObjectCollection(packet.ReadByte());
				for (int i = 0; i < buffs.Capacity; i++)
				{
					buffs[i] = new SRObject(packet.ReadUInt(), SRObject.Type.Skill);
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
					entity[SRAttribute.unkByte02] = packet.ReadByte();
					entity[SRAttribute.unkByte03] = packet.ReadByte();
					entity[SRAttribute.unkByte04] = packet.ReadByte();
					entity[SRAttribute.MobType] = (Types.Mob)packet.ReadByte();
				}
				else if (entity.ID2 == 1)
				{
					// CHARACTER
					entity[SRAttribute.Name] = packet.ReadAscii();
					entity[SRAttribute.JobType] = (Types.Job)packet.ReadByte();
					entity[SRAttribute.JobLevel] = packet.ReadByte();
					entity[SRAttribute.PVPState] = (Types.PVPState)packet.ReadByte();
					entity[SRAttribute.isRiding] = packet.ReadByte() == 1;
					entity[SRAttribute.isFighting] = packet.ReadByte();
					if ((bool)entity[SRAttribute.isRiding])
					{
						entity[SRAttribute.RidingUniqueID] = packet.ReadUInt();
					}
					entity[SRAttribute.ScrollMode] = (Types.ScrollMode)packet.ReadByte();
					entity[SRAttribute.InteractMode] = (Types.PlayerMode)packet.ReadByte();
					entity[SRAttribute.unkByte02] = packet.ReadByte();
					// Guild
					entity[SRAttribute.GuildName] = packet.ReadAscii();
					if (!entity.isJobMode())
					{
						entity[SRAttribute.GuildID] = packet.ReadUInt();
						entity[SRAttribute.GuildMemberName] = packet.ReadAscii();
						entity[SRAttribute.GuildLastCrestRev] = packet.ReadUInt();
						entity[SRAttribute.UnionID] = packet.ReadUInt();
						entity[SRAttribute.UnionLastCrestRev] = packet.ReadUInt();
						entity[SRAttribute.GuildisFriendly] = packet.ReadByte();
						entity[SRAttribute.GuildMemberAuthorityType] = packet.ReadByte();
					}
					if ((Types.PlayerMode)entity[SRAttribute.InteractMode] == Types.PlayerMode.OnStall)
					{
						entity[SRAttribute.StallTitle] = packet.ReadAscii();
						entity[SRAttribute.StallDecorationType] = packet.ReadUInt();
					}
					entity[SRAttribute.EquipmentCooldown] = packet.ReadByte();
					entity[SRAttribute.CaptureTheFlagType] = (Types.CaptureTheFlag)packet.ReadByte();
				}
				else if (entity.ID2 == 2)
				{
					// NPC
					entity[SRAttribute.hasTalk] = packet.ReadByte() != 0; // Talking stack options or none (0)
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
							entity[SRAttribute.PetName] = packet.ReadAscii();
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
							entity[SRAttribute.JobType] = (Types.Job)packet.ReadByte();
							if (entity.ID4 != 4)
							{
								// Not pet pick (Ability)
								entity[SRAttribute.PVPState] = (Types.PVPState)packet.ReadByte();
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
					entity[SRAttribute.Plus] = packet.ReadByte();
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
				entity[SRAttribute.X] = (int)packet.ReadSingle();
				entity[SRAttribute.Z] = (int)packet.ReadSingle();
				entity[SRAttribute.Y] = (int)packet.ReadSingle();
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
				entity[SRAttribute.X] = (int)packet.ReadSingle();
				entity[SRAttribute.Y] = (int)packet.ReadSingle();
				entity[SRAttribute.Z] = (int)packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();

				entity[SRAttribute.unkByte01] = packet.ReadByte();
				entity[SRAttribute.unkByte02] = packet.ReadByte();
				entity[SRAttribute.unkByte03] = packet.ReadByte();
				entity[SRAttribute.unkByte04] = packet.ReadByte();
				if ((byte)entity[SRAttribute.unkByte04] == 1)
				{
					// Regular
					entity[SRAttribute.unkUInt01] = packet.ReadUInt();
					entity[SRAttribute.unkUInt02] = packet.ReadUInt();
				}
				else if ((byte)entity[SRAttribute.unkByte04] == 6)
				{
					// Dimensional Hole
					entity[SRAttribute.OwnerName] = packet.ReadAscii();
					entity[SRAttribute.OwnerUniqueID] = packet.ReadUInt();
				}
				if ((byte)entity[SRAttribute.unkByte02] == 1)
				{
					// STORE_EVENTZONE_DEFAULT
					entity[SRAttribute.unkUInt03] = packet.ReadUInt();
					entity[SRAttribute.unkByte05] = packet.ReadByte();
				}
			}
			else if (entity.ID == uint.MaxValue)
			{
				// EVENT_ZONE (Traps, Buffzones, ...)
				entity[SRAttribute.unkUShort01] = packet.ReadUShort();
				SRObject Skill = new SRObject(packet.ReadUInt(), SRObject.Type.Skill);
				entity[SRAttribute.Skill] = Skill;

				entity[SRAttribute.UniqueID] = packet.ReadUInt();
				entity[SRAttribute.Region] = packet.ReadUShort();
				entity[SRAttribute.X] = (int)packet.ReadSingle();
				entity[SRAttribute.Z] = (int)packet.ReadSingle();
				entity[SRAttribute.Y] = (int)packet.ReadSingle();
				entity[SRAttribute.Angle] = packet.ReadUShort();

				entity[SRAttribute.Name] = Skill[SRAttribute.Name];
			}
			if (packet.Opcode == Agent.Opcode.SERVER_ENTITY_SPAWN)
			{
				if (entity.ID1 == 1 || entity.ID1 == 4)
				{
					// BIONIC or STORE
					entity[SRAttribute.unkByte06] = packet.ReadByte();
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
			Bot.Get._Event_Spawn(entity);
		}
		public static void EntityDespawn(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			// End of Packet

			// Keep the track of the entity
			Bot.Get._Event_Despawn(uniqueID);
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
					entity[SRAttribute.MovementOffsetX] = (int)(packet.ReadInt16());
					entity[SRAttribute.MovementOffsetZ] = (int)(packet.ReadInt16());
					entity[SRAttribute.MovementOffsetY] = (int)(packet.ReadInt16());
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
			entity[SRAttribute.MovementDate] = DateTime.Now;

			// Keep the track of the entity
			SRObject e = Info.Get.GetEntity((uint)entity[SRAttribute.UniqueID]);
			e.CopyAttributes(entity, true);

			// Update Minimap Movement
			if ((bool)entity[SRAttribute.hasMovement])
			{
				Window w = Window.Get;
				if (e == Info.Get.Character)
				{
					WinAPI.InvokeIfRequired(w.Character_lblLocation, () => {
						w.Character_lblLocation.Text = Info.Get.GetRegion((ushort)entity[SRAttribute.MovementRegion]);
					});
					WinAPI.InvokeIfRequired(w.Minimap_panelCoords, () => {
						w.Minimap_tbxX.Text = ((int)entity[SRAttribute.MovementOffsetX]).ToString();
						w.Minimap_tbxY.Text = ((int)entity[SRAttribute.MovementOffsetY]).ToString();
						w.Minimap_tbxZ.Text = ((int)entity[SRAttribute.MovementOffsetZ]).ToString();
						w.Minimap_tbxRegion.Text = ((ushort)entity[SRAttribute.MovementRegion]).ToString();
					});
					Point p = e.GetPosition((ushort)entity[SRAttribute.MovementRegion], (int)entity[SRAttribute.MovementOffsetX], (int)entity[SRAttribute.MovementOffsetY], (int)entity[SRAttribute.MovementOffsetZ]);
					WinAPI.InvokeIfRequired(w.Minimap_panelGameCoords, () => {
						w.Minimap_tbxCoordX.Text = p.X.ToString();
						w.Minimap_tbxCoordY.Text = p.Y.ToString();
					});
					w.Minimap_CharacterPointer_Move((int)entity[SRAttribute.MovementOffsetX], (int)entity[SRAttribute.MovementOffsetY], (int)entity[SRAttribute.MovementOffsetZ], (ushort)entity[SRAttribute.MovementRegion]);
				}
				else
				{
					w.Minimap_ObjectPointer_Move((uint)entity[SRAttribute.UniqueID], (int)entity[SRAttribute.MovementOffsetX], (int)entity[SRAttribute.MovementOffsetY], (int)entity[SRAttribute.MovementOffsetZ], (ushort)entity[SRAttribute.MovementRegion]);
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
			switch ((Types.Chat)type)
			{
				case Types.Chat.All:
				case Types.Chat.GM:
				case Types.Chat.NPC:
					uint playerID = packet.ReadUInt();
					SRObject p = Info.Get.GetEntity(playerID);
					if (p != null)
						player = (string)p[SRAttribute.Name];
					else
						player = "[UID:" + playerID + "]"; // Just in case
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
			switch ((Types.Chat)type)
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
			//uint uniqueID = packet.ReadUInt();
			//// End of Packet

			// "Possibly" used later when is on party for kicking players
		}
		public static void EntityStateUpdate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			byte unkByte01 = packet.ReadByte();
			byte unkByte02 = packet.ReadByte();

			Info i = Info.Get;
			SRObject entity = i.GetEntity(uniqueID);

			// Check if the entity has been despawned (killed) already
			if (entity == null)
				return;

			Types.EntityStateUpdate updateType = (Types.EntityStateUpdate)packet.ReadByte();
			switch (updateType)
			{
				case Types.EntityStateUpdate.HP:
					entity[SRAttribute.HP] = packet.ReadUInt();
					break;
				case Types.EntityStateUpdate.MP:
					entity[SRAttribute.MP] = packet.ReadUInt();
					break;
				case Types.EntityStateUpdate.HPMP:
				case Types.EntityStateUpdate.EntityHPMP:
					entity[SRAttribute.HP] = packet.ReadUInt();
					entity[SRAttribute.MP] = packet.ReadUInt();
					break;
				case Types.EntityStateUpdate.BadStatus:
					entity[SRAttribute.BadStatusFlags] = (Types.BadStatus)packet.ReadUInt();
					// Just for testing
					// w.LogMessageFilter("BadStatus:"+entity[SRAttribute.BadStatusType]+"|"+ entity[SRAttribute.UniqueID]);
					break;
			}
			// End of Packet

			// Update dead/alive state
			if (entity.Contains(SRAttribute.HP)
				&& (uint)entity[SRAttribute.HP] == 0)
			{
				entity[SRAttribute.LifeState] = Types.LifeState.Dead;
			}
			else if ((Types.LifeState)entity[SRAttribute.LifeState] != Types.LifeState.Alive)
			{
				entity[SRAttribute.LifeState] = Types.LifeState.Alive;
			}

			// Genrating event
			if ((uint)entity[SRAttribute.UniqueID] == (uint)i.Character[SRAttribute.UniqueID])
			{
				Bot.Get._Event_StateUpdated(updateType);
			}
			else if(entity.ID1 == 1 && entity.ID2 == 2 && entity.ID3 == 3){
				// Check if is pet
        if(entity.ID4 == 1 && (string)entity[SRAttribute.OwnerName] == i.Charname // vehicle
					|| (entity.ID4 != 1 && (uint)entity[SRAttribute.OwnerUniqueID] == (uint)i.Character[SRAttribute.UniqueID]))
				{
					// Check if it's my pet
					Bot.Get._Event_PetStateUpdated(updateType);
				}
			}
		}
		public static void EnviromentWheaterUpdate(Packet packet)
		{
			Info i = Info.Get;
			i.WheaterType = packet.ReadByte();
			i.WheaterIntensity = packet.ReadByte();
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
			uint uniqueID = packet.ReadUInt32();
			switch ((Types.PlayerPetition)type)
			{
				case Types.PlayerPetition.PartyCreation:
				case Types.PlayerPetition.PartyInvitation:
					Bot.Get.Event_PartyInvitation(uniqueID, (Types.PartySetup)packet.ReadByte());
					break;
				case Types.PlayerPetition.Resurrection:
					Bot.Get.Event_Resurrection(uniqueID);
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
			Bot.Get._Event_PartyJoined((Types.PartySetup)partySetupFlags, (Types.PartyPurpose)partyPurposeType);
		}
		private static void PartyAddPlayer(Packet packet)
		{
			SRObject player = new SRObject();
			player[SRAttribute.unkByte01] = packet.ReadByte();
			player[SRAttribute.ID] = packet.ReadUInt(); // ID from Party
			string name = packet.ReadAscii();
			player.Load(packet.ReadUInt(), SRObject.Type.Model);
			player[SRAttribute.Name] = name;
			player[SRAttribute.Level] = packet.ReadByte();
			player[SRAttribute.HPMP] = packet.ReadByte();
			player[SRAttribute.Region] = packet.ReadUShort();
			if (player.inDungeon((ushort)player[SRAttribute.Region]))
			{
				// On dungeon
				player[SRAttribute.X] = packet.ReadInt();
				player[SRAttribute.Z] = packet.ReadInt();
				player[SRAttribute.Y] = packet.ReadInt();
			}
			else
			{
				player[SRAttribute.X] = (int)packet.ReadShort();
				player[SRAttribute.Z] = (int)packet.ReadShort();
				player[SRAttribute.Y] = (int)packet.ReadShort();
			}
			player[SRAttribute.unkByte02] = packet.ReadByte(); // 2 = unkByte08.
			player[SRAttribute.unkByte03] = packet.ReadByte();
			player[SRAttribute.unkByte04] = packet.ReadByte();
			player[SRAttribute.unkByte05] = packet.ReadByte();
			player[SRAttribute.GuildName] = packet.ReadAscii();
			player[SRAttribute.unkByte06] = packet.ReadByte();
			if (packet.Opcode == Agent.Opcode.SERVER_PARTY_UPDATE && (byte)player[SRAttribute.unkByte02] == 2)
			{
				player[SRAttribute.unkByte07] = packet.ReadByte();
			}
			uint masteryID_primary = packet.ReadUInt();
			uint masteryID_secondary = packet.ReadUInt();

			// Keep on track players for updates
			Info i = Info.Get;
			i.PartyList.Add(player);

			// Add player to GUI
			ListViewItem item = new ListViewItem(name);
			item.Name = player[SRAttribute.ID].ToString();
			item.SubItems.Add((string)player[SRAttribute.GuildName]);
			item.SubItems.Add(player[SRAttribute.Level].ToString());
			if (i.Charname.Equals(name))
			{
				item.SubItems.Add("- - -");
			}
			else
			{
				byte HPMP = (byte)player[SRAttribute.HPMP];
				item.SubItems.Add(string.Format("{0}% / {1}%", (HPMP & 15) * 10, (HPMP >> 4) * 10));
			}
			item.SubItems.Add(i.GetRegion((ushort)player[SRAttribute.Region]));

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

			uint memberID;
			switch (updateType)
			{
				case 1: // Dismissed
					ushort unkUShort = packet.ReadUShort();
					// Event hook
					Bot.Get._Event_PartyLeaved();
					break;
				case 2: // Member joined
					PartyAddPlayer(packet);
					break;
				case 3: // Member leaved
					memberID = packet.ReadUInt();
					byte reason = packet.ReadByte();

					Bot.Get._Event_MemberLeaved(memberID);
					break;
				case 6: // Member update
					memberID = packet.ReadUInt();
					SRObject player = i.GetPartyMember(memberID);

					updateType = packet.ReadByte();
					switch (updateType)
					{
						case 4: // HP & MP
							byte HPMP = packet.ReadByte();
							byte hp = (byte)(HPMP & 15);
							byte mp = (byte)(HPMP >> 4);
							if (hp == 0) // Check if is dead
								mp = 0;
							else
								hp = (byte)(hp - 1);
							player[SRAttribute.HPMP] = (byte)(hp & mp);

							string PercentHPMP = string.Format("{0}% / {1}%", hp * 10, mp * 10);
							// Weird : sometimes hp is wrong (by +1), giving as result 110% or 10% in dead state
							WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
								w.Party_lstvPartyMembers.Items[memberID.ToString()].SubItems[3].Text = PercentHPMP;
							});
							break;
						case 0x20: // Map position
							string region = i.GetRegion(packet.ReadUShort());
							// X,Y,Z .. not important atm, only for minimap
							WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
								w.Party_lstvPartyMembers.Items[memberID.ToString()].SubItems[4].Text = region;
							});
							break;
					}
					break;
			}
		}
		public static void PartyMatchResponse(Packet packet) {
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

				Dictionary<string, ListViewItem> PartyMatches = new Dictionary<string, ListViewItem>();
				for (int j = 0; j < partyCount; j++)
				{
					uint number = packet.ReadUInt();
					uint unkUInt01 = packet.ReadUInt(); // Possibly UniqueID?
					string master = packet.ReadAscii();
					byte countryType = packet.ReadByte();
					byte membersCount = packet.ReadByte();
					byte setupType = packet.ReadByte();
					byte purposeType = packet.ReadByte();
					byte levelMin = packet.ReadByte();
					byte levelMax = packet.ReadByte();
					string title = packet.ReadAscii();

					// Party setup to boolean
					Types.PartySetup setupFlags = (Types.PartySetup)setupType;
					bool ExpShared = setupFlags.HasFlag(Types.PartySetup.ExpShared);
					bool ItemShared = setupFlags.HasFlag(Types.PartySetup.ItemShared);
					bool AnyoneCanInvite = setupFlags.HasFlag(Types.PartySetup.AnyoneCanInvite);

					if (!PartyMatches.ContainsKey(number.ToString()))
					{
						ListViewItem Party = new ListViewItem(number.ToString());
						Party.Name = number.ToString();
						bool isJob = (Types.PartyPurpose)purposeType == Types.PartyPurpose.Thief || (Types.PartyPurpose)purposeType == Types.PartyPurpose.Trader;
						Party.SubItems.Add((isJob ? "*" : "") + master);
						Party.SubItems.Add(title);
						Party.SubItems.Add(levelMin + "~" + levelMax);
						Party.SubItems.Add(membersCount + "/" + (ExpShared ? "8" : "4"));
						Party.SubItems.Add(((Types.PartyPurpose)purposeType).ToString());
						Party.ToolTipText = "Exp. " + (ExpShared ? "Shared" : "Free - For - All") + "\nItem " + (ItemShared ? "Shared" : "Free-For-All") + "\n" + (AnyoneCanInvite ? "Anyone" : "Only Master") + " Can Invite";

						if(hasParty && master == (string)Info.Get.PartyList[0][SRAttribute.Name])
							Party.BackColor = Color.FromArgb(120, 120, 120);

						PartyMatches[Party.Name] = Party;
					}
				}
				if (partyCount > 0)
				{
					ListViewItem[] array = new ListViewItem[PartyMatches.Count];
					PartyMatches.Values.CopyTo(array, 0);
					// Add partys
					WinAPI.InvokeIfRequired(w.Party_lstvPartyMatch, () => {
						w.Party_lstvPartyMatch.Items.AddRange(array);
					});
				}

				// Set page changer
				WinAPI.InvokeIfRequired(w.Party_lblPageNumber, () => {
					w.Party_lblPageNumber.Text = (pageIndex + 1).ToString();
				});
				WinAPI.InvokeIfRequired(w.Party_btnLastPage, () => {
					w.Party_btnLastPage.Enabled = pageIndex != 0;
				});
				WinAPI.InvokeIfRequired(w.Party_btnNextPage, () => {
					w.Party_btnNextPage.Enabled = pageCount != pageIndex + 1;
				});
			}
		}
		public static void EntitySelection(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				uint uniqueID = packet.ReadUInt();
				Bot.Get._Event_EntitySelected(uniqueID);
			}
		}
		public static void CharacterAddStatPointResponse(Packet packet)
		{
			Window w = Window.Get;

			bool success = packet.ReadByte() == 1;
			if (success)
			{
				Info i = Info.Get;

				ushort StatPoints = (ushort)i.Character[SRAttribute.StatPoints];
				if (StatPoints > 0)
				{
					i.Character[SRAttribute.StatPoints] = (ushort)(StatPoints - 1);
					WinAPI.InvokeIfRequired(w.Character_lblStatPoints, () => {
						w.Character_lblStatPoints.Text = i.Character[SRAttribute.StatPoints].ToString();
					});
					if ((ushort)i.Character[SRAttribute.StatPoints] == 0)
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
		public static void InventoryItemUse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				byte slot = packet.ReadByte();
				ushort quantityUpdate = packet.ReadUShort();
				//ushort usageType = packet.ReadUShort();

				SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRAttribute.Inventory];
				if (quantityUpdate == 0)
					inventory[slot] = null; // Item consumed
				else
					inventory[slot][SRAttribute.Quantity] = quantityUpdate;
			}
		}
		public static void InventoryItemDurabilityUpdate(Packet packet)
		{
			byte slotInventory = packet.ReadByte();
			uint durability = packet.ReadUInt();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRAttribute.Inventory];
			inventory[slotInventory][SRAttribute.Durability] = durability;
		}
		public static void InventoryItemStateUpdate(Packet packet)
		{
			byte slotInventory = packet.ReadByte();
			byte updateType = packet.ReadByte();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRAttribute.Inventory];
			switch(updateType){
				case 0x40: // Pet State
					inventory[slotInventory][SRAttribute.PetState] = (Types.PetState)packet.ReadByte();
					break;
			}
		}
		private static Packet storageDataPacket;
		public static void StorageDataBegin(Packet packet)
		{
			Info.Get.Character[SRAttribute.StorageGold] = packet.ReadULong();
			storageDataPacket = new Packet(Agent.Opcode.SERVER_STORAGE_DATA);
		}
		public static void StorageData(Packet packet)
		{
			storageDataPacket.WriteUInt8Array(packet.GetBytes());
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
			Info.Get.Character[SRAttribute.Storage] = storage;
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
				}
			}
			return false;
		}
		private static void InventoryItemMovement_InventoryToInventory(Packet p)
		{
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRAttribute.Inventory];

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if ((ushort)inventory[slotInitial][SRAttribute.QuantityMax] == 1
					|| (ushort)inventory[slotInitial][SRAttribute.Quantity] == quantityMoved)
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
					ushort q = (ushort)inventory[slotInitial][SRAttribute.Quantity];
					inventory[slotFinal][SRAttribute.Quantity] = quantityMoved;
					inventory[slotInitial][SRAttribute.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| quantityMoved == (ushort)inventory[slotFinal][SRAttribute.QuantityMax]
				|| (ushort)inventory[slotFinal][SRAttribute.Quantity] == (ushort)inventory[slotFinal][SRAttribute.QuantityMax])
			{
				// switch
				SRObject temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)inventory[slotInitial][SRAttribute.Quantity] == quantityMoved)
				{
					inventory[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotFinal][SRAttribute.Quantity] + quantityMoved);
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotFinal][SRAttribute.Quantity] + quantityMoved);
					inventory[slotInitial][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotInitial][SRAttribute.Quantity] - quantityMoved);
				}
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

			SRObjectCollection storage = (SRObjectCollection)Info.Get.Character[SRAttribute.Storage];

			// Check if is stack or just switch.. and update it.
			if (storage[slotFinal] == null)
			{
				if ((ushort)storage[slotInitial][SRAttribute.QuantityMax] == 1
					|| (ushort)storage[slotInitial][SRAttribute.Quantity] == quantityMoved)
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
					ushort q = (ushort)storage[slotInitial][SRAttribute.Quantity];
					storage[slotFinal][SRAttribute.Quantity] = quantityMoved;
					storage[slotInitial][SRAttribute.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (storage[slotFinal].ID != storage[slotInitial].ID
				|| quantityMoved == (ushort)storage[slotFinal][SRAttribute.QuantityMax])
			{
				// switch
				SRObject temp = storage[slotFinal];
				storage[slotFinal] = storage[slotInitial];
				storage[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)storage[slotInitial][SRAttribute.Quantity] == quantityMoved)
				{
					storage[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)storage[slotFinal][SRAttribute.Quantity] + quantityMoved);
					storage[slotInitial] = null;
				}
				else
				{
					// fixing
					storage[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)storage[slotFinal][SRAttribute.Quantity] + quantityMoved);
					storage[slotInitial][SRAttribute.Quantity] = (ushort)((ushort)storage[slotInitial][SRAttribute.Quantity] - quantityMoved);
				}
			}
		}
		private static void InventoryItemMovement_InventoryToStorage(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotStorage = p.ReadByte();
			// End of Packet
			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];
			SRObjectCollection storage = (SRObjectCollection)i.Character[SRAttribute.Storage];

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
			SRObjectCollection storage = (SRObjectCollection)i.Character[SRAttribute.Storage];
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];

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
				SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRAttribute.Inventory];
				inventory[slotInventory] = ItemParsing(p);
			}
		}
		private static void InventoryItemMovement_InventoryToGround(Packet p)
		{
			byte slotInventory = p.ReadByte();
			// End of Packet

			SRObjectCollection inventory = (SRObjectCollection)Info.Get.Character[SRAttribute.Inventory];
			inventory[slotInventory] = null;
		}
		private static void InventoryItemMovement_ShopToInventory(Packet p)
		{
			byte tabNumber = p.ReadByte();
			byte tabSlot = p.ReadByte();
			byte packageCount = p.ReadByte();

			// Select the item from the shop specified
			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];
			SRObject NPCEntity = i.GetEntity(Bot.Get.GetEntitySelected());
			SRObject item = i.GetItemFromShop((string)NPCEntity[SRAttribute.Servername], tabNumber, tabSlot);

			if (packageCount == 1)
			{
				byte slotInventory = p.ReadByte();
				item[SRAttribute.Quantity] = p.ReadUShort();
				item[SRAttribute.unkUInt01] = p.ReadUInt();
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
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];

			if (!i.Character.Contains(SRAttribute.ShopBuyBack))
				i.Character[SRAttribute.ShopBuyBack] = new SRObjectCollection();
			SRObjectCollection buyBack = (SRObjectCollection)i.Character[SRAttribute.ShopBuyBack];

			// Sync max. quantity to buy back
			if (slotBuyBack == 5 && slotBuyBack == buyBack.Count)
				buyBack.RemoveAt(0);

			if ((ushort)inventory[slotInventory][SRAttribute.Quantity] == quantitySold)
			{
				buyBack[slotBuyBack - 1] = inventory[slotInventory];
				inventory[slotInventory] = null;
			}
			else
			{
				buyBack[slotBuyBack - 1] = inventory[slotInventory].Clone();
				buyBack[slotBuyBack - 1][SRAttribute.Quantity] = quantitySold;
				inventory[slotInventory][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotInventory][SRAttribute.Quantity] - quantitySold);
      }
		}
		private static void InventoryItemMovement_PetToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRAttribute.Inventory];

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if ((ushort)inventory[slotInitial][SRAttribute.QuantityMax] == 1
					|| (ushort)inventory[slotInitial][SRAttribute.Quantity] == quantityMoved)
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
					ushort q = (ushort)inventory[slotInitial][SRAttribute.Quantity];
					inventory[slotFinal][SRAttribute.Quantity] = quantityMoved;
					inventory[slotInitial][SRAttribute.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| quantityMoved == (ushort)inventory[slotFinal][SRAttribute.QuantityMax]
				|| (ushort)inventory[slotFinal][SRAttribute.Quantity] == (ushort)inventory[slotFinal][SRAttribute.QuantityMax])
			{
				// switch
				SRObject temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)inventory[slotInitial][SRAttribute.Quantity] == quantityMoved)
				{
					inventory[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotFinal][SRAttribute.Quantity] + quantityMoved);
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotFinal][SRAttribute.Quantity] + quantityMoved);
					inventory[slotInitial][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotInitial][SRAttribute.Quantity] - quantityMoved);
				}
			}
		}
		private static void InventoryItemMovement_PetToInventory(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotPetInventory = p.ReadByte();
			byte slotMyInventory = p.ReadByte();

			Info i = Info.Get;
			SRObjectCollection myInventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];
			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection petInventory = (SRObjectCollection)pet[SRAttribute.Inventory];

			myInventory[slotMyInventory] = petInventory[slotPetInventory];
			petInventory[slotPetInventory] = null;
		}
		private static void InventoryItemMovement_InventoryToPet(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotMyInventory = p.ReadByte();
			byte slotPetInventory = p.ReadByte();

			Info i = Info.Get;
			SRObjectCollection myInventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];
			SRObject pet = i.GetEntity(uniqueID);
			SRObjectCollection petInventory = (SRObjectCollection)pet[SRAttribute.Inventory];

			petInventory[slotPetInventory] = myInventory[slotMyInventory];
			myInventory[slotMyInventory] = null;
		}
		private static void InventoryItemMovement_TransportToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInitial = p.ReadByte();
			byte slotFinal = p.ReadByte();
			ushort quantityMoved = p.ReadUShort();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRAttribute.Inventory];

			// Check if is stack or just switch.. and update it.
			if (inventory[slotFinal] == null)
			{
				if ((ushort)inventory[slotInitial][SRAttribute.QuantityMax] == 1
					|| (ushort)inventory[slotInitial][SRAttribute.Quantity] == quantityMoved)
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
					ushort q = (ushort)inventory[slotInitial][SRAttribute.Quantity];
					inventory[slotFinal][SRAttribute.Quantity] = quantityMoved;
					inventory[slotInitial][SRAttribute.Quantity] = (ushort)(q - quantityMoved);
				}
			}
			else if (inventory[slotFinal].ID != inventory[slotInitial].ID
				|| quantityMoved == (ushort)inventory[slotFinal][SRAttribute.QuantityMax]
				|| (ushort)inventory[slotFinal][SRAttribute.Quantity] == (ushort)inventory[slotFinal][SRAttribute.QuantityMax])
			{
				// switch
				SRObject temp = inventory[slotFinal];
				inventory[slotFinal] = inventory[slotInitial];
				inventory[slotInitial] = temp;
			}
			else
			{
				// stacking
				if ((ushort)inventory[slotInitial][SRAttribute.Quantity] == quantityMoved)
				{
					inventory[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotFinal][SRAttribute.Quantity] + quantityMoved);
					inventory[slotInitial] = null;
				}
				else
				{
					// fixing
					inventory[slotFinal][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotFinal][SRAttribute.Quantity] + quantityMoved);
					inventory[slotInitial][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotInitial][SRAttribute.Quantity] - quantityMoved);
				}
			}
		}
		private static void InventoryItemMovement_GroundToTransport(Packet p)
		{
			uint uniqueID = p.ReadUInt();
			byte slotInventory = p.ReadByte();

			SRObject item = new SRObject();
			item[SRAttribute.unkUInt01] = p.ReadUInt();
			item.Load(p.ReadUInt(), SRObject.Type.Item);
			item[SRAttribute.Quantity] = p.ReadUShort();
			item[SRAttribute.OwnerName] = p.ReadAscii();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			SRObjectCollection inventory = (SRObjectCollection)pet[SRAttribute.Inventory];
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
			SRObjectCollection inventory = (SRObjectCollection)pet[SRAttribute.Inventory];
			SRObject NPCEntity = i.GetEntity(Bot.Get.GetEntitySelected());
			SRObject item = i.GetItemFromShop((string)NPCEntity[SRAttribute.Servername], tabNumber, tabSlot);
			item[SRAttribute.OwnerName] = i.Charname;

			if (packageCount == 1)
			{
				byte slotInventory = p.ReadByte();
				item[SRAttribute.Quantity] = p.ReadUShort();
				item[SRAttribute.unkUInt01] = p.ReadUInt();
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
			SRObjectCollection inventory = (SRObjectCollection)pet[SRAttribute.Inventory];

			if ((ushort)inventory[slotInventory][SRAttribute.Quantity] == quantitySold)
				inventory[slotInventory] = null;
			else
				inventory[slotInventory][SRAttribute.Quantity] = (ushort)((ushort)inventory[slotInventory][SRAttribute.Quantity] - quantitySold);
		}
		private static void InventoryItemMovement_ShopBuyBack(Packet p)
		{
			byte slotInventory = p.ReadByte();
			byte slotBuyBack = p.ReadByte();
			ushort quantitySold = p.ReadUShort();

			Info i = Info.Get;
			SRObjectCollection inventory = (SRObjectCollection)i.Character[SRAttribute.Inventory];
			SRObjectCollection buyBack = (SRObjectCollection)i.Character[SRAttribute.ShopBuyBack];

			inventory[slotInventory] = buyBack[slotBuyBack];
			buyBack.RemoveAt(slotBuyBack);
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
						pet[SRAttribute.HP] = packet.ReadUInt();
						pet[SRAttribute.HPMax] = packet.ReadUInt();
						SRObjectCollection Inventory = new SRObjectCollection(packet.ReadByte());
						if (Inventory.Capacity > 0)
						{
							// TRANSPORT
							byte itemsCount = packet.ReadByte();
							for(byte j = 0; j < itemsCount; j++)
							{
								byte slot = packet.ReadByte();

								SRObject item = new SRObject();
								item[SRAttribute.unkUInt01] = packet.ReadUInt();
								item.Load(packet.ReadUInt(),SRObject.Type.Item);
								item[SRAttribute.Quantity] = packet.ReadUShort();
								item[SRAttribute.OwnerName] = packet.ReadAscii();

								Inventory[slot] = item;
							}
							pet[SRAttribute.Inventory] = Inventory;
							//uint ownerUniqueID = packet.ReadUInt();
						}
					}
					else if (pet.ID4 == 3)
					{
						// ATTACK PET
						pet[SRAttribute.HP] = packet.ReadUInt();
            pet[SRAttribute.unkUInt01] = packet.ReadUInt();
						pet[SRAttribute.Exp] = packet.ReadULong();
						pet[SRAttribute.Level] = packet.ReadByte();
						pet[SRAttribute.ExpMax] = i.GetExpMax((byte)pet[SRAttribute.Level]);
						pet[SRAttribute.HGP] = packet.ReadUShort();
						pet[SRAttribute.AttackSettingsFlags] = (Types.PetAttackSettings)packet.ReadUInt();
						string PetName = packet.ReadAscii();
						pet[SRAttribute.unkByte07] = packet.ReadByte();
						uint ownerUniqueID = packet.ReadUInt();
						pet[SRAttribute.unkByte08] = packet.ReadByte();
					}
					else if (pet.ID4 == 4)
					{
						// GRAB PET
						pet[SRAttribute.unkUInt01] = packet.ReadUInt();
						pet[SRAttribute.unkUInt02] = packet.ReadUInt();
						pet[SRAttribute.PickSettingFlags] = (Types.PetPickSettings)packet.ReadUInt();
						string PetName = packet.ReadAscii();

						SRObjectCollection inventory = new SRObjectCollection(packet.ReadByte());
						byte itemsCount = packet.ReadByte();
						for (byte j = 0; j < itemsCount; j++)
						{
							byte slot = packet.ReadByte();
							inventory[slot] = ItemParsing(packet);
						}
						pet[SRAttribute.Inventory] = inventory;
						//uint ownerUniqueID = packet.ReadUInt();
					}
				}
			}
			Bot.Get._Event_PetSummoned(uniqueID);
		}
		public static void PetUpdate(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			byte updateType = packet.ReadByte();

			SRObject pet = Info.Get.GetEntity(uniqueID);
			switch (updateType)
			{
				case 1: // Unsummoned
					Bot.Get._Event_PetUnsummoned(uniqueID);
					break;
				case 3: // Exp
					long ExpReceived = packet.ReadLong();
					CalculatePetExp(ref pet, ExpReceived, (long)((ulong)pet[SRAttribute.Exp]), (long)((ulong)pet[SRAttribute.ExpMax]), (byte)pet[SRAttribute.Level]);
					break;
				case 4: // Hungry
					pet[SRAttribute.HGP] = packet.ReadUShort();
					break;
				case 7: // Model changed
					pet.Load(packet.ReadUInt(), SRObject.Type.Model);
          break;
			}
		}
		private static void CalculatePetExp(ref SRObject pet,long ExpReceived, long Exp, long ExpMax, byte level)
		{
			if (ExpReceived + Exp >= ExpMax)
			{
				// Level Up
				pet[SRAttribute.Level] = (byte)(level + 1);
				// Update new ExpMax
				pet[SRAttribute.ExpMax] = Info.Get.GetExpMax((byte)pet[SRAttribute.Level]);
				// Continue recursivity
				CalculatePetExp(ref pet,(Exp + ExpReceived) - ExpMax, 0L, (long)((ulong)pet[SRAttribute.ExpMax]), (byte)(level + 1));
			}
			else if (ExpReceived + Exp < 0)
			{
				// Level Down
				pet[SRAttribute.Level] = (byte)(level - 1);
				// Update new ExpMax
				pet[SRAttribute.ExpMax] = Info.Get.GetExpMax((byte)pet[SRAttribute.Level]);
				CalculatePetExp(ref pet, Exp + ExpReceived, (long)((ulong)pet[SRAttribute.ExpMax]), (long)((ulong)pet[SRAttribute.ExpMax]), (byte)(level - 1));
			}
			else
			{
				// Increase/Decrease Exp
				pet[SRAttribute.Exp] = (ulong)(Exp + ExpReceived);
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
					pet[SRAttribute.AttackSettingsFlags] = (Types.PetAttackSettings)packet.ReadUInt();
					break;
			}
		}
		public static void PetPlayerMounted(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if(success){
				SRObject player = Info.Get.GetEntity(packet.ReadUInt());
				
				player[SRAttribute.isRiding] = packet.ReadByte() == 1;
				if ((bool)player[SRAttribute.isRiding]){
					// Avoid reading when it's not necessary
					player[SRAttribute.RidingUniqueID] = packet.ReadUInt();
				}
			}
		}
		public static void StallOpenResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
				Bot.Get._Event_StallOpened(true);
		}
		public static void StallCloseResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
				Bot.Get._Event_StallClosed();
		}
		public static void StallOtherOpened(Packet packet)
		{
			Bot.Get._Event_StallOpened(false);
		}
		public static void StallOtherClosed(Packet packet)
		{
			Bot.Get._Event_StallClosed();
		}
		public static void StallPlayerCreated(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();

			SRObject entity = Info.Get.GetEntity(uniqueID);
			entity[SRAttribute.StallTitle] = packet.ReadAscii();
			entity[SRAttribute.StallDecorationType] = packet.ReadUInt();
			entity[SRAttribute.InteractMode] = Types.PlayerMode.OnStall;
		}
		public static void StallPlayerClosed(Packet packet)
		{
			uint uniqueID = packet.ReadUInt();
			//ushort unkUshort01 = packet.ReadUShort();

			SRObject entity = Info.Get.GetEntity(uniqueID);
			entity[SRAttribute.InteractMode] = Types.PlayerMode.None;
		}
		public static void SkillLevelUpResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				SRObject newSkill = new SRObject(packet.ReadUInt(),SRObject.Type.Skill);

				// Update skills
				Info i = Info.Get;
				SRObjectCollection skills = (SRObjectCollection)i.Character[SRAttribute.Skills];

				// Look for the skill with the last category name
				uint skillID = i.GetLastSkillID(newSkill);

				if (skillID != 0)
				{
					// Update/override if an skill is sharing the same groupname
					SRObject skill = skills.Find(m => m.ID == skillID);
					skill.CopyAttributes(newSkill, true);
				}
				else
				{
					// Add new skill
					skills.Add(newSkill);
        }
			}
		}
		public static void MasteryLevelUpResponse(Packet packet)
		{
			bool success = packet.ReadByte() == 1;
			if (success)
			{
				uint masteryID = packet.ReadUInt();

				SRObjectCollection masteries = (SRObjectCollection)Info.Get.Character[SRAttribute.Masteries];
				SRObject mastery = masteries.Find(m => m.ID == masteryID);
				if(mastery != null){
					mastery[SRAttribute.Level] = packet.ReadByte();
				}
			}
		}
	}
}