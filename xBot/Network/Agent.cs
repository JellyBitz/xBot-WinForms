using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Threading;
using xBot.App;
using xBot.Game;
using xBot.Game.Objects.Common;

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
				CLIENT_GAMEMASTER_COMMAND_REQUEST = 0x7010,
				CLIENT_CHARACTER_SELECTION_JOIN_REQUEST = 0x7001,
				CLIENT_CHARACTER_SELECTION_ACTION_REQUEST = 0x7007,
				CLIENT_CHARACTER_CONFIRM_SPAWN = 0x3012,
				CLIENT_CHARACTER_MOVEMENT = 0x7021,
				CLIENT_CHARACTER_MOVEMENT_ANGLE = 0x7024,
				CLIENT_CHARACTER_PET_ACTION = 0x70C5,
				CLIENT_CHARACTER_ADD_STR_REQUEST = 0x7050,
				CLIENT_CHARACTER_ADD_INT_REQUEST = 0x7051,
				CLIENT_CHARACTER_EMOTE_USE = 0x3091,
				CLIENT_CHARACTER_AUTORESURRECTION = 0x3053,
				CLIENT_CHARACTER_ACTION_REQUEST = 0x7074,
				CLIENT_INVENTORY_ITEM_USE = 0x704C,
				CLIENT_INVENTORY_ITEM_MOVEMENT = 0x7034,
				CLIENT_STORAGE_DATA_REQUEST = 0x703C,
				CLIENT_ENTITY_SELECTION = 0x7045,
				CLIENT_ENTITY_TALK_REQUEST = 0x7046,
				CLIENT_NPC_CLOSE_REQUEST = 0x704B,
				CLIENT_CHAT_REQUEST = 0x7025,
				CLIENT_MAIL_SEND_REQUEST = 0x7309,
				CLIENT_PLAYER_INVITATION_RESPONSE = 0x3080,
				CLIENT_PARTY_CREATION_REQUEST = 0x7060,
				CLIENT_PARTY_LEAVE = 0x7061,
				CLIENT_PARTY_INVITATION_REQUEST = 0x7062,
				CLIENT_PARTY_KICK_REQUEST = 0x7063,
				CLIENT_PARTY_MATCH_CREATION_REQUEST = 0x7069,
				CLIENT_PARTY_MATCH_EDITED_REQUEST = 0x706A,
				CLIENT_PARTY_MATCH_DELETE_REQUEST = 0x706B,
				CLIENT_PARTY_MATCH_LIST_REQUEST = 0x706C,
				CLIENT_PARTY_MATCH_JOIN_REQUEST = 0x706D,
				CLIENT_PARTY_MATCH_JOIN_RESPONSE = 0x306E,
				CLIENT_GUILD_INVITATION_REQUEST = 0x70F3,
				CLIENT_GUILD_NOTICE_EDIT_REQUEST = 0x70F9,
				CLIENT_GUILD_STORAGE_REQUEST = 0x7250,
				CLIENT_ACADEMY_INVITATION_REQUEST = 0x7472,
				CLIENT_ACADEMY_MATCH_LIST_REQUEST = 0x747D,
				CLIENT_ACADEMY_NOTICE_EDIT_REQUEST = 0x7477,
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
				CLIENT_MASTERY_SKILL_LEVELDOWN_REQUEST = 0x7202,
				CLIENT_MASTERY_LEVELUP_REQUEST = 0x70A2,
				CLIENT_MASTERY_LEVELDOWN_REQUEST = 0x7203,
				CLIENT_TELEPORT_USE_REQUEST = 0x705A,
				CLIENT_TELEPORT_RECALL_REQUEST = 0x7059,
				CLIENT_TELEPORT_READY_RESPONSE = 0x34B6,
				CLIENT_CONSIGNMENT_LIST_REQUEST = 0x750E,
				CLIENT_CONSIGNMENT_REGISTER_REQUEST = 0x7508,
				CLIENT_CONSIGNMENT_UNREGISTER_REQUEST = 0x7509,
				CLIENT_EXCHANGE_INVITATION_REQUEST = 0x7081,
				CLIENT_EXCHANGE_CONFIRM_REQUEST = 0x7082,
				CLIENT_EXCHANGE_APPROVE_REQUEST = 0x7083,
				CLIENT_EXCHANGE_EXIT_REQUEST = 0x7084,

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
				SERVER_INVENTORY_ITEM_UPDATE = 0x3040,
				SERVER_INVENTORY_CAPACITY_UPDATE = 0x3092,
				SERVER_STORAGE_DATA_BEGIN = 0x3047,
				SERVER_STORAGE_DATA = 0x3049,
				SERVER_STORAGE_DATA_END = 0x3048,
				SERVER_ENTITY_SELECTION = 0xB045,
				SERVER_ENTITY_SKILL_START = 0xB070,
				SERVER_ENTITY_SKILL_END = 0xB071,
				SERVER_ENTITY_SKILL_BUFF_ADDED = 0xB0BD,
				SERVER_ENTITY_SKILL_BUFF_REMOVED = 0xB072,
				SERVER_ENTITY_SPAWN = 0x3015,
				SERVER_ENTITY_DESPAWN = 0x3016,
				SERVER_ENTITY_GROUPSPAWN_BEGIN = 0x3017,
				SERVER_ENTITY_GROUPSPAWN_END = 0x3018,
				SERVER_ENTITY_GROUPSPAWN_DATA = 0x3019,
				SERVER_ENTITY_MOVEMENT = 0xB021,
				SERVER_ENTITY_MOVEMENT_STUCK = 0xB023,
				SERVER_ENTITY_MOVEMENT_ANGLE = 0xB024,
				SERVER_ENTITY_TALK_RESPONSE = 0xB046,
				SERVER_ENTITY_LEVEL_UP = 0x3054,
				SERVER_ENTITY_STATUS_UPDATE = 0x3057,
				SERVER_ENTITY_DISPLAY_EFFECT = 0x305C,
				SERVER_ENTITY_SPEED_UPDATE = 0x30D0,
				SERVER_ENTITY_STATE_UPDATE = 0x30BF,
				SERVER_ENTITY_STALL_CREATE = 0x30B8,
				SERVER_ENTITY_STALL_DESTROY = 0x30B9,
				SERVER_ENTITY_STALL_TITLE_UPDATE = 0x30BB,
				SERVER_ENTITY_EMOTE_USE = 0x3091,
				SERVER_CHAT_RESPONSE = 0xB025,
				SERVER_CHAT_UPDATE = 0x3026,
				SERVER_PLAYER_PETITION_REQUEST = 0x3080,
				SERVER_MAIL_SEND_RESPONSE = 0xB309,
				SERVER_NOTICE_UPDATE = 0x300C,
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
				SERVER_STALL_ENTITY_ACTION = 0x30B7,
				SERVER_STALL_CLOSED = 0x30B9,
				SERVER_MASTERY_SKILL_LEVELUP_RESPONSE = 0xB0A1,
				SERVER_MASTERY_SKILL_LEVELDOWN_RESPONSE = 0xB202,
				SERVER_MASTERY_LEVELUP_RESPONSE = 0xB0A2,
				SERVER_MASTERY_LEVELDOWN_RESPONSE = 0xB203,
				SERVER_TELEPORT_USE_RESPONSE = 0xB05A,
				SERVER_TELEPORT_RECALL_RESPONSE = 0xB059,
				SERVER_TELEPORT_READY_REQUEST = 0x34B5,
				SERVER_CONSIGNMENT_REGISTER_RESPONSE = 0xB508,
				SERVER_CONSIGNMENT_UNREGISTER_RESPONSE = 0xB509,
				SERVER_GUILD_CREATED_DATA = 0xB0F0,
				SERVER_GUILD_DATA_BEGIN = 0x34B3,
				SERVER_GUILD_DATA = 0x3101,
				SERVER_GUILD_DATA_END = 0x34B4,
				SERVER_GUILD_UPDATE = 0x38F5,
				SERVER_GUILD_PLAYER_LOG = 0x30FF,
				SERVER_GUILD_STORAGE_RESPONSE = 0xB250,
				SERVER_GUILD_STORAGE_DATA_BEGIN = 0x3253,
				SERVER_GUILD_STORAGE_DATA = 0x3255,
				SERVER_GUILD_STORAGE_DATA_END = 0x3254,
				SERVER_ACADEMY_DATA = 0x3C81,
				SERVER_ACADEMY_MATCH_LIST_RESPONSE = 0xB47D,
				SERVER_EXCHANGE_STARTED = 0x3085,
				SERVER_EXCHANGE_PLAYER_CONFIRMED = 0x3086,
				SERVER_EXCHANGE_COMPLETED = 0x3087,
				SERVER_EXCHANGE_CANCELED = 0x3088,
				SERVER_EXCHANGE_GOLD_UPDATE = 0x3089,
				SERVER_EXCHANGE_ITEMS_UPDATE = 0x308C,
				SERVER_EXCHANGE_INVITATION_RESPONSE = 0xB081,
				SERVER_EXCHANGE_CONFIRM_RESPONSE = 0xB082,
				SERVER_EXCHANGE_APPROVE_RESPONSE = 0xB083,
				SERVER_EXCHANGE_EXIT_RESPONSE = 0xB084,
				SERVER_DROP_UNLOCKED = 0x304D,
				SERVER_NPC_CLOSE_RESPONSE = 0xB04B,

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
		public bool ClientlessMode { get { return Bot.Get.Proxy.ClientlessMode; } }
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
			try
			{
				if (context == Local)
					return Local_PacketHandler(packet);
				return Remote_PacketHandler(packet);
			}
			catch(Exception ex)
			{
				Bot.Get.LogError("Parsing Error",ex, packet);
				throw ex;
      }
		}
		/// <summary>
		/// Analyze client packets.
		/// </summary>
		/// <param name="packet">Client packet</param>
		/// <returns>True if the packet won't be sent to the server</returns>
		private bool Local_PacketHandler(Packet packet)
		{
			// Opcode filter
			switch (packet.Opcode)
			{
				case Opcode.CLIENT_AUTH_REQUEST:
					if(Bot.Get.LoggedFromBot)
						return true;
					break;
				case Opcode.CLIENT_CHARACTER_SELECTION_JOIN_REQUEST:
						InfoManager.SetCharacter(packet.ReadAscii());
					return true;
				case Opcode.CLIENT_CHARACTER_CONFIRM_SPAWN:
					if (!ClientlessMode)
						InfoManager.OnTeleported();
					break;
				case Opcode.CLIENT_CHARACTER_MOVEMENT:

					break;
				case Opcode.CLIENT_CHARACTER_MOVEMENT_ANGLE:

					break;
				case Opcode.CLIENT_CHAT_REQUEST:
					{
						// Keep on track all private messages sent
						SRTypes.Chat t = (SRTypes.Chat)packet.ReadByte();
						byte chatIndex = packet.ReadByte();
						if (t == SRTypes.Chat.All)
						{
							string message = packet.ReadAscii();
							return Bot.Get.OnChatSending(message);
						}
						else if (t == SRTypes.Chat.Private)
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
							protocol.WriteUShort(DataManager.Locale);
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

							InfoManager.OnConnected();
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
					PacketParser.CharacterDataEnd();
					if (ClientlessMode)
					{
						// Confirm spawn after loading with some delay
						Packet protocol = new Packet(Opcode.CLIENT_CHARACTER_CONFIRM_SPAWN);
						InjectToServer(protocol);

						// Generating Bot Events to keep methods clean
						InfoManager.OnTeleported();

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
				case Opcode.SERVER_ENTITY_SELECTION:
					PacketParser.EntitySelection(packet);
					break;
				case Opcode.SERVER_ENTITY_LEVEL_UP:
					PacketParser.EntityLevelUp(packet);
					break;
				case Opcode.SERVER_ENTITY_STATUS_UPDATE:
					PacketParser.EntityStatusUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_MOVEMENT:
					PacketParser.EntityMovement(packet);
					break;
				case Opcode.SERVER_ENTITY_MOVEMENT_STUCK:
					PacketParser.EntityMovementStuck(packet);
					break;
				case Opcode.SERVER_ENTITY_MOVEMENT_ANGLE:
					PacketParser.EntityMovementAngle(packet);
					break;
				case Opcode.SERVER_ENTITY_SPEED_UPDATE:
					PacketParser.EntitySpeedUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_STATE_UPDATE:
					PacketParser.EntityStateUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_STALL_CREATE:
					PacketParser.EntityStallCreate(packet);
					break;
				case Opcode.SERVER_ENTITY_STALL_DESTROY:
					PacketParser.EntityStallDestroy(packet);
					break;
				case Opcode.SERVER_ENTITY_STALL_TITLE_UPDATE:
					PacketParser.EntityStallTitleUpdate(packet);
					break;
				case Opcode.SERVER_ENTITY_SKILL_START:
					PacketParser.EntitySkillStart(packet);
					break;
				case Opcode.SERVER_ENTITY_SKILL_END:
					PacketParser.EntitySkillEnd(packet);
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
				case Opcode.SERVER_NOTICE_UPDATE:
					PacketParser.NoticeUniqueUpdate(packet);
					break;
				case Opcode.SERVER_PLAYER_PETITION_REQUEST:
					PacketParser.PlayerPetitionRequest(packet);
					break;
				case Opcode.SERVER_EXCHANGE_STARTED:
					PacketParser.ExchangeStarted(packet);
					break;
				case Opcode.SERVER_EXCHANGE_PLAYER_CONFIRMED:
					PacketParser.ExchangePlayerConfirmed(packet);
					break;
				case Opcode.SERVER_EXCHANGE_COMPLETED:
					PacketParser.ExchangeCompleted(packet);
					break;
				case Opcode.SERVER_EXCHANGE_CANCELED:
					PacketParser.ExchangeCanceled(packet);
					break;
				case Opcode.SERVER_EXCHANGE_ITEMS_UPDATE:
					PacketParser.ExchangeItemsUpdate(packet);
					break;
				case Opcode.SERVER_EXCHANGE_GOLD_UPDATE:
					PacketParser.ExchangeGoldUpdate(packet);
					break;
				case Opcode.SERVER_EXCHANGE_INVITATION_RESPONSE:
					PacketParser.ExchangeInvitationResponse(packet);
					break;
				case Opcode.SERVER_EXCHANGE_CONFIRM_RESPONSE:
					PacketParser.ExchangeConfirmResponse(packet);
					break;
				case Opcode.SERVER_EXCHANGE_APPROVE_RESPONSE:
					PacketParser.ExchangeApproveResponse(packet);
					break;
				case Opcode.SERVER_EXCHANGE_EXIT_RESPONSE:
					PacketParser.ExchangeExitResponse(packet);
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
				case Opcode.SERVER_PARTY_MATCH_DELETE_RESPONSE:
					PacketParser.PartyMatchDeleteResponse(packet);
					break;
				case Opcode.SERVER_PARTY_MATCH_JOIN_REQUEST:
					PacketParser.PartyMatchJoinRequest(packet);
					break;
				case Opcode.SERVER_GUILD_CREATED_DATA:
					PacketParser.GuildCreatedData(packet);
					break;	
				case Opcode.SERVER_GUILD_DATA_BEGIN:
					PacketParser.GuildDataBegin();
					break;
				case Opcode.SERVER_GUILD_DATA:
					PacketParser.GuildData(packet);
					break;
				case Opcode.SERVER_GUILD_DATA_END:
					PacketParser.GuildDataEnd();
					break;
				case Opcode.SERVER_GUILD_UPDATE:
					PacketParser.GuildUpdate(packet);
					break;
				case Opcode.SERVER_GUILD_STORAGE_DATA_BEGIN:
					PacketParser.GuildStorageDataBegin(packet);
					break;
				case Opcode.SERVER_GUILD_STORAGE_DATA:
					PacketParser.GuildStorageData(packet);
					break;
				case Opcode.SERVER_GUILD_STORAGE_DATA_END:
					PacketParser.GuildStorageDataEnd(packet);
					break;
				case Opcode.SERVER_ACADEMY_DATA:
					PacketParser.AcademyData(packet);
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
				case Opcode.SERVER_INVENTORY_ITEM_UPDATE:
					PacketParser.InventoryItemUpdate(packet);
					break;
				case Opcode.SERVER_INVENTORY_CAPACITY_UPDATE:
					PacketParser.InventoryCapacityUpdate(packet);
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
				case Opcode.SERVER_CONSIGNMENT_REGISTER_RESPONSE:
					PacketParser.ConsigmentRegisterResponse(packet);
					break;
				case Opcode.SERVER_CONSIGNMENT_UNREGISTER_RESPONSE:
					PacketParser.ConsigmentUnregisterResponse(packet);
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
				case Opcode.SERVER_STALL_BUY_RESPONSE:
					PacketParser.StallBuyResponse(packet);
					break;
				case Opcode.SERVER_STALL_LEAVE_RESPONSE:
					PacketParser.StallLeaveResponse(packet);
					break;
				case Opcode.SERVER_STALL_UPDATE_RESPONSE:
					PacketParser.StallUpdateResponse(packet);
					break;
				case Opcode.SERVER_STALL_ENTITY_ACTION:
					PacketParser.StalleEntityAction(packet);
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
				case Opcode.SERVER_MASTERY_SKILL_LEVELDOWN_RESPONSE:
					PacketParser.MasterySkillLevelDownResponse(packet);
					break;
				case Opcode.SERVER_MASTERY_LEVELUP_RESPONSE:
					PacketParser.MasteryLevelUpResponse(packet);
					break;
				case Opcode.SERVER_MASTERY_LEVELDOWN_RESPONSE:
					PacketParser.MasteryLevelDownResponse(packet);
					break;
				case Opcode.SERVER_TELEPORT_READY_REQUEST:
					if (ClientlessMode)
					{
						Packet protocol = new Packet(Opcode.CLIENT_TELEPORT_READY_RESPONSE);
						this.InjectToServer(protocol);
					}
					break;
				case Opcode.SERVER_ENTITY_TALK_RESPONSE:
					PacketParser.EntityTalkResponse(packet);
					break;
				case Opcode.SERVER_NPC_CLOSE_RESPONSE:
					PacketParser.NpcCloseResponse(packet);
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