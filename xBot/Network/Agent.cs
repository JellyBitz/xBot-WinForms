﻿using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Threading;
using xBot.App;
using xBot.Game;

namespace xBot.Network
{
	public class Agent
	{
		public static class Opcode
		{
			// static opcodes could be edited at realtime (for different vSRO types)
			public static ushort
				CLIENT_HWID_RESPONSE = 0,
				SERVER_HWID_REQUEST = 0;
			public const ushort
				CLIENT_AUTH_REQUEST = 0x6103,
				CLIENT_CHARACTER_SELECTION_JOIN_REQUEST = 0x7001,
				CLIENT_CHARACTER_SELECTION_ACTION_REQUEST = 0x7007,
				CLIENT_CHARACTER_CONFIRM_SPAWN = 0x3012,
				CLIENT_CHARACTER_MOVEMENT = 0x7021,
				CLIENT_CHARACTER_TRANSPORT_MOVEMENT = 0x70C5,
				CLIENT_CHARACTER_ADD_STR_REQUEST = 0x7050,
				CLIENT_CHARACTER_ADD_INT_REQUEST = 0x7051,
				CLIENT_CHARACTER_EMOTE_USE = 0x3091,
				CLIENT_CHARACTER_AUTORESURRECTION = 0x3053,
				CLIENT_CHARACTER_ACTION_REQUEST = 0x7074,
        CLIENT_INVENTORY_ITEM_USE = 0x704C,
				CLIENT_INVENTORY_ITEM_MOVEMENT = 0x7034,
				CLIENT_STORAGE_DATA_REQUEST = 0x703C,
				CLIENT_ENTITY_SELECTION = 0x7045,
				CLIENT_CHAT_REQUEST = 0x7025,
				CLIENT_MAIL_SEND_REQUEST = 0x7309,
        CLIENT_PLAYER_INVITATION_RESPONSE = 0x3080,
				CLIENT_PARTY_CREATION_REQUEST = 0x7060,
				CLIENT_PARTY_LEAVE = 0x7061,
				CLIENT_PARTY_INVITATION_REQUEST = 0x7062,
				CLIENT_PARTY_BANISH_REQUEST = 0x7063,
				CLIENT_PARTY_MATCH_CREATION_REQUEST = 0x7069,
				CLIENT_PARTY_MATCH_EDITED_REQUEST = 0x706A,
				CLIENT_PARTY_MATCH_DELETE_REQUEST = 0x706B,
				CLIENT_PARTY_MATCH_LIST_REQUEST = 0x706C,
				CLIENT_PARTY_MATCH_JOIN_REQUEST = 0x706D,
				CLIENT_PARTY_MATCH_JOIN_RESPONSE = 0x306E,
				CLIENT_ACADEMY_NOTICE_EDITED_REQUEST = 0x7477,
				CLIENT_PET_UNSUMMON_REQUEST = 0x7116,
				CLIENT_PET_SETTINGS_CHANGE_REQUEST = 0x7420,
				CLIENT_PET_MOUNTED = 0x70CB,
				CLIENT_PET_DESTROY = 0x706C,
				CLIENT_STALL_CREATE_REQUEST = 0x70B1,
				CLIENT_STALL_DESTROY_REQUEST = 0x70B2,
				CLIENT_STALL_TALK_REQUEST = 0x70B3,
				CLIENT_STALL_BUY_REQUEST = 0x70B4,
				CLIENT_STALL_LEAVE_REQUEST = 0x70B5,
				CLIENT_STALL_UPDATE_REQUEST = 0x70BA,
				CLIENT_MASTERY_SKILL_LEVELUP_REQUEST = 0x70A1,
				CLIENT_MASTERY_LEVELUP_REQUEST = 0x70A2,
				CLIENT_TELEPORT_USE_REQUEST = 0x705A,
				CLIENT_TELEPORT_READY_RESPONSE = 0x34B6,
				CLIENT_CONSIGNMENT_LIST_REQUEST = 0x750E,

				SERVER_AUTH_RESPONSE = 0xA103,
				SERVER_CHARACTER_SELECTION_JOIN_RESPONSE = 0xB001,
				SERVER_CHARACTER_SELECTION_ACTION_RESPONSE = 0xB007,
				SERVER_CHARACTER_DATA_BEGIN = 0x34A5,
				SERVER_CHARACTER_DATA = 0x3013,
				SERVER_CHARACTER_DATA_END = 0x34A6,
				SERVER_CHARACTER_STATS_UPDATE = 0x303D,
				SERVER_CHARACTER_EXPERIENCE_UPDATE = 0x3056,
				SERVER_CHARACTER_ADD_STR_RESPONSE = 0xB050,
				SERVER_CHARACTER_ADD_INT_RESPONSE = 0xB051,
				SERVER_CHARACTER_INFO_UPDATE = 0x304E,
				SERVER_CHARACTER_DIED = 0x3011,
				SERVER_INVENTORY_ITEM_USE = 0xB04C,
				SERVER_INVENTORY_ITEM_MOVEMENT = 0xB034,
				SERVER_INVENTORY_ITEM_DURABILITY_UPDATE = 0x3052,
				SERVER_INVENTORY_ITEM_STATE_UPDATE = 0x3040,
				SERVER_STORAGE_DATA_BEGIN = 0x3047,
				SERVER_STORAGE_DATA = 0x3049,
				SERVER_STORAGE_DATA_END = 0x3048,
				SERVER_ENTITY_SELECTION = 0xB045,
				SERVER_ENTITY_SKILL_USE = 0xB070,
				SERVER_ENTITY_SKILL_BUFF_ADDED = 0xB0BD,
				SERVER_ENTITY_SKILL_BUFF_REMOVED = 0xB072,
				SERVER_ENTITY_SPAWN = 0x3015,
				SERVER_ENTITY_DESPAWN = 0x3016,
				SERVER_ENTITY_GROUPSPAWN_BEGIN = 0x3017,
				SERVER_ENTITY_GROUPSPAWN_END = 0x3018,
				SERVER_ENTITY_GROUPSPAWN_DATA = 0x3019,
				SERVER_ENTITY_MOVEMENT = 0xB021,
				SERVER_ENTITY_MOVEMENT_STUCK = 0xB023,
				SERVER_ENTITY_LEVEL_UP = 0x3054,
				SERVER_ENTITY_STATE_UPDATE = 0x3057,
				SERVER_ENTITY_DISPLAY_EFFECT = 0x305C,
				SERVER_ENTITY_SPEED_UPDATE = 0x30D0,
				SERVER_ENTITY_MOTION_UPDATE = 0x30BF,
				SERVER_ENTITY_STALL_CREATE = 0x30B8,
				SERVER_ENTITY_STALL_DESTROY = 0x30B9,
				SERVER_ENTITY_EMOTE_USE = 0x3091,
				SERVER_PLAYER_PETITION_REQUEST = 0x3080,
				SERVER_CHAT_UPDATE = 0x3026,
				SERVER_MAIL_SEND_RESPONSE = 0xB309,
				SERVER_NOTICE_UNIQUE_UPDATE = 0x300C,
				SERVER_ENVIROMENT_CELESTIAL_POSITION = 0x3020,
				SERVER_ENVIROMENT_CELESTIAL_UPDATE = 0x3027,
				SERVER_ENVIROMENT_WHEATER_UPDATE = 0x3809,
				SERVER_PARTY_INVITATION_RESPONSE = 0xB060,
				SERVER_PARTY_DATA = 0x3065,
				SERVER_PARTY_UPDATE = 0x3864,
				SERVER_PARTY_MATCH_LIST_RESPONSE = 0xB06C,
				SERVER_PARTY_MATCH_CREATION_RESPONSE = 0xB069,
				SERVER_PARTY_MATCH_EDITED_RESPONSE = 0xB06A,
				SERVER_PARTY_MATCH_DELETE_RESPONSE = 0xB06B,
				SERVER_PARTY_MATCH_JOIN_REQUEST = 0x706D,
				SERVER_PET_DATA = 0x30C8,
				SERVER_PET_UPDATE = 0x30C9,
				SERVER_PET_SETTINGS_CHANGE_RESPONSE = 0xB420,
				SERVER_PET_PLAYER_MOUNTED = 0xB0CB,
				SERVER_STALL_CREATE_RESPONSE = 0xB0B1,
				SERVER_STALL_DESTROY_RESPONSE = 0xB0B2,
				SERVER_STALL_TALK_RESPONSE = 0xB0B3,
				SERVER_STALL_BUY_RESPONSE = 0xB0B4,
				SERVER_STALL_LEAVE_RESPONSE = 0xB0B5,
				SERVER_STALL_UPDATE_RESPONSE = 0xB0BA,
				SERVER_STALL_CLOSED = 0x30B9,
				SERVER_MASTERY_SKILL_LEVELUP_RESPONSE = 0xB0A1,
				SERVER_MASTERY_LEVELUP_RESPONSE = 0xB0A2,
				SERVER_TELEPORT_USE_RESPONSE = 0xB05A,
				SERVER_TELEPORT_READY_REQUEST = 0x34B5,

				GLOBAL_HANDSHAKE = 0x5000,
				GLOBAL_HANDSHAKE_OK = 0x9000,
				GLOBAL_IDENTIFICATION = 0x2001,
				GLOBAL_PING = 0x2002,
				GLOBAL_XTRAP_IDENTIFICATION = 0x2113;
		}
		public Context Local { get; }
		public Context Remote { get; }
		public List<ushort> IgnoreOpcodeClient { get; }
		public List<ushort> IgnoreOpcodeServer { get; }
		public uint id { get; }
		public string Host { get; }
		public ushort Port { get; }
		public Agent(uint queqeID, string host, ushort port)
		{
			id = queqeID;
			Host = host;
			Port = port;

			Local = new Context();
			Local.Security.GenerateSecurity(true, true, true);
			Remote = new Context();
			// Setup cycle : (Client < > Proxy < > Server)
			Remote.RelaySecurity = Local.Security; // Client < Proxy < Server
			Local.RelaySecurity = Remote.Security; // Client > Proxy > Server
																						 
			IgnoreOpcodeClient = new List<ushort>(); // ignore list
			IgnoreOpcodeClient.Add(Opcode.GLOBAL_HANDSHAKE);
			IgnoreOpcodeClient.Add(Opcode.GLOBAL_HANDSHAKE_OK);
			IgnoreOpcodeClient.Add(Opcode.GLOBAL_IDENTIFICATION); // proxy to remote is handled by API
			IgnoreOpcodeClient.Add(Opcode.GLOBAL_PING); // handling ping manually

			IgnoreOpcodeServer = new List<ushort>(); // ignore list
			IgnoreOpcodeServer.Add(Opcode.GLOBAL_HANDSHAKE);
			IgnoreOpcodeServer.Add(Opcode.GLOBAL_HANDSHAKE_OK);
		}
		public bool ClientlessMode { get { return Local.Socket == null; } }
		public bool IgnoreOpcode(ushort opcode, Context c)
		{
			if (c == Local)
				return IgnoreOpcodeClient.Contains(opcode);
			return IgnoreOpcodeServer.Contains(opcode);
		}
		/// <summary>
		/// Analyze packets.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="packet">Packet to analyze</param>
		/// <returns>True if the packet is handled by the bot</returns>
		public bool PacketHandler(Context context, Packet packet)
		{
			if (context == Local)
			{
				return Local_PacketHandler(packet);
			}
			return Remote_PacketHandler(packet);
		}
		/// <summary>
		/// Analyze client packets.
		/// </summary>
		/// <param name="packet">Client packet</param>
		/// <returns>True if the packet won't be sent to the server</returns>
		private bool Local_PacketHandler(Packet packet)
		{
			// HWID setup (saving/updating data from client)
			if (packet.Opcode == Opcode.CLIENT_HWID_RESPONSE)
			{
				if (Bot.Get.HWIDSaveFrom == "Agent" || Bot.Get.HWIDSaveFrom == "Both"){
					Bot.Get.SaveHWID(packet.GetBytes());
				}
			}
			// Opcode filter
			switch (packet.Opcode)
			{
				case Opcode.CLIENT_AUTH_REQUEST:
					if(Bot.Get.LoggedFromBot)
						return true;
					break;
				case Opcode.CLIENT_CHARACTER_SELECTION_JOIN_REQUEST:
					{
						Info.Get.Charname = packet.ReadAscii();
						Window w = Window.Get;
						w.EnableControl(w.Login_btnStart, false);
					}
					break;
				case Opcode.CLIENT_CHARACTER_CONFIRM_SPAWN:
					if(!ClientlessMode)
						Bot.Get._OnTeleported();
					break;
				case Opcode.CLIENT_CHAT_REQUEST:
					{
						// Keep on track all private messages sent
						Types.Chat t = (Types.Chat)packet.ReadByte();
						byte chatIndex = packet.ReadByte();
						if (t == Types.Chat.Private)
						{
							Window w = Window.Get;
							w.LogChatMessage(w.Chat_rtbxPrivate, packet.ReadAscii() + "(To)", packet.ReadAscii());
						}
					}
					break;
      }
			return false;
		}
		/// <summary>
		/// Analyze server packets.
		/// </summary>
		/// <param name="packet">Server packet</param>
		/// <returns>True if the packet will be ignored by the client</returns>
		private bool Remote_PacketHandler(Packet packet)
		{
			// HWID setup (sending data to server)
			if (packet.Opcode == Opcode.SERVER_HWID_REQUEST && ClientlessMode)
			{
				if (Bot.Get.HWIDSendTo == "Agent" || Bot.Get.HWIDSendTo == "Both")
				{
					byte[] hwidData = Bot.Get.LoadHWID();
					if (hwidData != null)
					{
						Packet p = new Packet(Opcode.CLIENT_HWID_RESPONSE, false, false, hwidData);
						InjectToServer(p);
						Window.Get.LogProcess("HWID Sent : " + WinAPI.ToHexString(hwidData));
					}
				}
			}
			// Opcode filter
			switch (packet.Opcode)
			{
				case Opcode.GLOBAL_IDENTIFICATION:
					if (Bot.Get.LoggedFromBot)
					{
						string service = packet.ReadAscii();
						if (service == "AgentServer")
						{
							Packet protocol = new Packet(Opcode.CLIENT_AUTH_REQUEST, true);
							protocol.WriteUInt(id);
							protocol.WriteAscii(Window.Get.Login_tbxUsername.Text);
							protocol.WriteAscii(Window.Get.Login_tbxPassword.Text);
							protocol.WriteUShort(Info.Get.Locale);
							// "MAC"
							protocol.WriteUInt(0u);
							protocol.WriteUShort(0);
							this.InjectToServer(protocol);
						}
					}
          break;
				case Opcode.SERVER_AUTH_RESPONSE:
					{
						byte success = packet.ReadByte();
						if (success == 1)
						{
							// Protocol
							if (ClientlessMode)
								PacketBuilder.RequestCharacterList();

							// Generating Bot Event to keep this method clean
							Bot.Get._OnConnected();
						}
						else
						{
							byte error = packet.ReadByte();
							Window.Get.Log("Login error [" + error + "]");
						}
					}
					break;
				case Opcode.SERVER_CHARACTER_SELECTION_ACTION_RESPONSE:
					PacketParser.CharacterSelectionActionResponse(packet);
					break;
				case Opcode.SERVER_CHARACTER_SELECTION_JOIN_RESPONSE:
					PacketParser.CharacterSelectionJoinResponse(packet);
					break;
				case Opcode.SERVER_CHARACTER_DATA_BEGIN:
					PacketParser.CharacterDataBegin(packet);
					break;
				case Opcode.SERVER_CHARACTER_DATA:
					PacketParser.CharacterData(packet);
					break;
				case Opcode.SERVER_CHARACTER_DATA_END:
					PacketParser.CharacterDataEnd(packet);
					if (ClientlessMode)
					{
						// Confirm spawn after loading with some delay
						Packet protocol = new Packet(Opcode.CLIENT_CHARACTER_CONFIRM_SPAWN);
						InjectToServer(protocol);

						// Generating Bot Events to keep methods clean
						Bot.Get._OnTeleported();

						protocol = new Packet(Opcode.CLIENT_CONSIGNMENT_LIST_REQUEST);
						InjectToServer(protocol);
					}
					break;
				case Opcode.SERVER_CHARACTER_STATS_UPDATE:
					PacketParser.CharacterStatsUpdate(packet);
					break;
				case Opcode.SERVER_CHARACTER_EXPERIENCE_UPDATE:
					PacketParser.CharacterExperienceUpdate(packet);
					break;
				case Opcode.SERVER_CHARACTER_INFO_UPDATE:
					PacketParser.CharacterInfoUpdate(packet);
					break;
				case Opcode.SERVER_CHARACTER_DIED:
					PacketParser.CharacterDied(packet);
					break;
				case Opcode.GLOBAL_XTRAP_IDENTIFICATION:
					if (ClientlessMode)
					{
						Packet protocol = new Packet(Opcode.GLOBAL_XTRAP_IDENTIFICATION);
						protocol.WriteByte(2);
						protocol.WriteByte(2);
						//p.WriteUInt8Array(new byte[1024]);
						protocol.WriteUInt64Array(new ulong[128]);
						InjectToServer(protocol);
					}
					break;
				case Opcode.SERVER_ENTITY_SPAWN:
					PacketParser.EntitySpawn(packet);
					break;
				case Opcode.SERVER_ENTITY_DESPAWN:
					PacketParser.EntityDespawn(packet);
					break;
				case Opcode.SERVER_ENTITY_GROUPSPAWN_BEGIN:
					PacketParser.EntityGroupSpawnBegin(packet);
					break;
				case Opcode.SERVER_ENTITY_GROUPSPAWN_DATA:
					PacketParser.EntityGroupSpawnData(packet);
					break;
				case Opcode.SERVER_ENTITY_GROUPSPAWN_END:
					PacketParser.EntityGroupSpawnEnd(packet);
					break;
				case Opcode.SERVER_ENTITY_LEVEL_UP:
					PacketParser.EntityLevelUp(packet);
					break;
				case Opcode.SERVER_ENTITY_STATE_UPDATE:
					PacketParser.EntityStateUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_MOVEMENT:
					PacketParser.EntityMovement(packet);
					break;
				case Opcode.SERVER_ENTITY_MOVEMENT_STUCK:
					PacketParser.EntityMovementStuck(packet);
					break;
				case Opcode.SERVER_ENTITY_SPEED_UPDATE:
					PacketParser.EntitySpeedUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_MOTION_UPDATE:
					PacketParser.EntityMotionUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_STALL_CREATE:
					PacketParser.EntityStallCreate(packet);
					break;
				case Opcode.SERVER_ENTITY_STALL_DESTROY:
					PacketParser.EntityStallDestroy(packet);
					break;
				case Opcode.SERVER_ENTITY_SKILL_USE:
					PacketParser.EntitySkillUse(packet);
					break;
				case Opcode.SERVER_ENTITY_SKILL_BUFF_ADDED:
					PacketParser.EntitySkillBuffAdded(packet);
					break;
				case Opcode.SERVER_ENTITY_SKILL_BUFF_REMOVED:
					PacketParser.EntitySkillBuffRemoved(packet);
					break;
				case Opcode.SERVER_ENVIROMENT_CELESTIAL_POSITION:
					PacketParser.EnviromentCelestialPosition(packet);
					break;
				case Opcode.SERVER_ENVIROMENT_CELESTIAL_UPDATE:
					PacketParser.EnviromentCelestialUpdate(packet);
					break;
				case Opcode.SERVER_ENVIROMENT_WHEATER_UPDATE:
					PacketParser.EnviromentWheaterUpdate(packet);
					break;
				case Opcode.SERVER_CHAT_UPDATE:
					PacketParser.ChatUpdate(packet);
					break;
				case Opcode.SERVER_NOTICE_UNIQUE_UPDATE:
					PacketParser.NoticeUniqueUpdate(packet);
					break;
				case Opcode.SERVER_PLAYER_PETITION_REQUEST:
					PacketParser.PlayerPetitionRequest(packet);
					break;
				case Opcode.SERVER_PARTY_DATA:
					PacketParser.PartyData(packet);
					break;
				case Opcode.SERVER_PARTY_UPDATE:
					PacketParser.PartyUpdate(packet);
					break;
				case Opcode.SERVER_PARTY_MATCH_LIST_RESPONSE:
					PacketParser.PartyMatchListResponse(packet);
					break;
				case Opcode.SERVER_PARTY_MATCH_CREATION_RESPONSE:
					PacketParser.PartyMatchCreationResponse(packet);
					break;
				case Opcode.SERVER_PARTY_MATCH_DELETE_RESPONSE:
					PacketParser.PartyMatchDeleteResponse(packet);
					break;
				case Opcode.SERVER_PARTY_MATCH_JOIN_REQUEST:
					PacketParser.PartyMatchJoinRequest(packet);
					break;
				case Opcode.SERVER_ENTITY_SELECTION:
					PacketParser.EntitySelection(packet);
					break;
				case Opcode.SERVER_CHARACTER_ADD_INT_RESPONSE:
					PacketParser.CharacterAddStatPointResponse(packet);
					break;
				case Opcode.SERVER_CHARACTER_ADD_STR_RESPONSE:
					PacketParser.CharacterAddStatPointResponse(packet);
					break;
				case Opcode.SERVER_INVENTORY_ITEM_USE:
					PacketParser.InventoryItemUse(packet);
					break;
				case Opcode.SERVER_INVENTORY_ITEM_MOVEMENT:
					return PacketParser.InventoryItemMovement(packet);
				case Opcode.SERVER_INVENTORY_ITEM_DURABILITY_UPDATE:
					PacketParser.InventoryItemDurabilityUpdate(packet);
					break;
				case Opcode.SERVER_INVENTORY_ITEM_STATE_UPDATE:
					PacketParser.InventoryItemStateUpdate(packet);
					break;
				case Opcode.SERVER_STALL_CREATE_RESPONSE:
					PacketParser.StallCreateResponse(packet);
					break;
				case Opcode.SERVER_STALL_DESTROY_RESPONSE:
					PacketParser.StallDestroyResponse(packet);
					break;
				case Opcode.SERVER_STALL_TALK_RESPONSE:
					PacketParser.StallTalkResponse(packet);
					break;
				case Opcode.SERVER_STALL_LEAVE_RESPONSE:
					PacketParser.StallLeaveResponse(packet);
					break;
				case Opcode.SERVER_STORAGE_DATA_BEGIN:
					PacketParser.StorageDataBegin(packet);
					break;
				case Opcode.SERVER_STORAGE_DATA:
					PacketParser.StorageData(packet);
					break;
				case Opcode.SERVER_STORAGE_DATA_END:
					PacketParser.StorageDataEnd(packet);
					break;
				case Opcode.SERVER_PET_DATA:
					PacketParser.PetData(packet);
					break;
				case Opcode.SERVER_PET_UPDATE:
					PacketParser.PetUpdate(packet);
					break;
				case Opcode.SERVER_PET_SETTINGS_CHANGE_RESPONSE:
					PacketParser.PetSettingsChangeResponse(packet);
					break;
				case Opcode.SERVER_PET_PLAYER_MOUNTED:
					PacketParser.PetPlayerMounted(packet);
					break;
				case Opcode.SERVER_MASTERY_SKILL_LEVELUP_RESPONSE:
					PacketParser.MasterySkillLevelUpResponse(packet);
					break;
				case Opcode.SERVER_MASTERY_LEVELUP_RESPONSE:
					PacketParser.MasteryLevelUpResponse(packet);
					break;
			}
			return false;
		}
		/// <summary>
		/// Inject a packet to the server. The delay is used to avoid locking the current thread.
		/// </summary>
		/// <param name="p">Packet with the info</param>
		/// <param name="delay">Delay in miliseconds to be executed in other thread</param>
		public void InjectToServer(Packet p, int delay = 0)
		{
			if(delay > 0)
			{
				(new Thread((ThreadStart)delegate{
					Thread.Sleep(delay);
					Remote.Security.Send(p);
				})).Start();
			}
			else
			{
				Remote.Security.Send(p);
			}
		}
		public void InjectToClient(Packet p)
		{
			this.Remote.RelaySecurity.Send(p);
		}
	}
}