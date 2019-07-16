using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Threading;
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
				CLIENT_ENVIROMENT_WEATHER_REQUEST = 0x750E,
				CLIENT_CHARACTER_MOVEMENT = 0x7021,
				CLIENT_CHARACTER_ADD_STR_REQUEST = 0x7050,
				CLIENT_CHARACTER_ADD_INT_REQUEST = 0x7051,
				CLIENT_CHAT_REQUEST = 0x7025,
				CLIENT_PLAYER_INVITATION_RESPONSE = 0x3080,
				CLIENT_PARTY_LEAVE = 0x7061,
				CLIENT_PARTY_MATCH_REQUEST = 0x706C,
				CLIENT_PARTY_MATCH_JOIN = 0x706D,
				CLIENT_ENTITY_SELECTION = 0x7045,

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
				SERVER_ENTITY_SPAWN = 0x3015,
				SERVER_ENTITY_DESPAWN = 0x3016,
				SERVER_ENTITY_GROUPSPAWN_BEGIN = 0x3017,
				SERVER_ENTITY_GROUPSPAWN_END = 0x3018,
				SERVER_ENTITY_GROUPSPAWN_DATA = 0x3019,
				SERVER_ENTITY_MOVEMENT = 0xB021,
				SERVER_ENTITY_LEVEL_UP = 0x3054,
				SERVER_ENTITY_STATE_UPDATE = 0x3057,
				SERVER_ENVIROMENT_CELESTIAL_POSITION = 0x3020,
				SERVER_ENVIROMENT_CELESTIAL_UPDATE = 0x3027,
				SERVER_ENVIROMENT_WHEATER_UPDATE = 0x3809,
				SERVER_CHAT_UPDATE = 0x3026,
				SERVER_UNIQUE_UPDATE = 0x300C,
				SERVER_PLAYER_INVITATION_REQUEST = 0x3080,
				SERVER_PARTY_INVITATION_RESPONSE = 0xB060,
				SERVER_PARTY_DATA = 0x3065,
				SERVER_PARTY_UPDATE = 0x3864,
				SERVER_ENTITY_SELECTION = 0xB045,
				SERVER_PARTY_MATCH_RESPONSE = 0xB06C,

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
				// HWID setup (saving/updating data from client)
				if (packet.Opcode == Opcode.CLIENT_HWID_RESPONSE)
				{
					if (Bot.Get.HWIDSaveFrom == "Agent" || Bot.Get.HWIDSaveFrom == "Both")
					{
						Bot.Get.SaveHWID(packet.GetBytes());
					}
				}
				return Local_PacketHandler(packet);
			}
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
						Window.Get.LogProcess("HWID Sent : " + WinAPI.BytesToHexString(hwidData));
					}
				}
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
			if (packet.Opcode == Opcode.CLIENT_AUTH_REQUEST && Bot.Get.LoginFromBot)
			{
				return true;
			}
			else if (packet.Opcode == Opcode.CLIENT_CHARACTER_SELECTION_JOIN_REQUEST)
			{
				Info.Get.Charname = packet.ReadAscii();
				Window w = Window.Get;
				w.EnableControl(w.Login_btnStart, false);
			}
			else if (packet.Opcode == Opcode.CLIENT_CHARACTER_CONFIRM_SPAWN && !ClientlessMode)
			{
				Bot.Get._Event_Teleported();
			}
			else if (packet.Opcode == Opcode.CLIENT_CHAT_REQUEST)
			{
				// Keep on track all private messages sent
				if(packet.ReadByte() == (byte)Types.Chat.Private)
				{
					packet.ReadByte(); // chat index
					Window w = Window.Get;
					w.LogChatMessage(w.Chat_rtbxPrivate, packet.ReadAscii() + "(To)", packet.ReadAscii());
				}
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
			if (packet.Opcode == Opcode.GLOBAL_IDENTIFICATION && Bot.Get.LoginFromBot)
			{
				string service = packet.ReadAscii();
				if (service == "AgentServer")
				{
					Packet p = new Packet(Opcode.CLIENT_AUTH_REQUEST, true);
					p.WriteUInt32(id);
					p.WriteAscii(Window.Get.Login_tbxUsername.Text);
					p.WriteAscii(Window.Get.Login_tbxPassword.Text);
					p.WriteUInt16(Info.Get.Locale);
					// MAC
					p.WriteUInt32(0);
					p.WriteUInt16(0);
					this.InjectToServer(p);
				}
			}
			else if (packet.Opcode == Opcode.SERVER_AUTH_RESPONSE)
			{
				Window w = Window.Get;
				byte success = packet.ReadByte();
				if (success == 1)
				{
					w.Log("Logged successfully!");
					w.LogProcess("Logged");
					w.EnableControl(w.Login_btnStart, false);
					if (ClientlessMode)
						PacketBuilder.RequestCharacterList();

					// Generating Bot Event to keep this method clean
					Bot.Get._Event_Connected();
				}
				else
				{
					byte error = packet.ReadByte();
					w.Log("Login error [" + error + "]");
				}
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_SELECTION_ACTION_RESPONSE)
			{
				PacketParser.CharacterSelectionActionResponse(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_SELECTION_JOIN_RESPONSE)
			{
				Window w = Window.Get;
				byte success = packet.ReadByte();
				if (success == 1)
				{
					w.Log("Character selected ["+Info.Get.Charname+"]");
				}
				else
				{
					int error = packet.ReadUShort();
					w.Log("Error: " + error);
					w.LogProcess("Error", Window.ProcessState.Error);
				}
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_DATA_BEGIN)
			{
				PacketParser.CharacterDataBegin(packet);
				Bot.Get._Event_Teleporting();
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_DATA)
			{
				PacketParser.CharacterData(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_DATA_END)
			{
				PacketParser.CharacterDataEnd(packet);
				if (ClientlessMode)
				{
					// Confirm spawn after loading with some delay
					Packet protocol = new Packet(Opcode.CLIENT_CHARACTER_CONFIRM_SPAWN);
					InjectToServer(protocol);

					// Generating Bot Events to keep methods clean
					Bot.Get._Event_Teleported();

					protocol = new Packet(Opcode.CLIENT_ENVIROMENT_WEATHER_REQUEST);
					InjectToServer(protocol);
				}
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_STATS_UPDATE)
			{
				PacketParser.CharacterStatsUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_EXPERIENCE_UPDATE)
			{
				PacketParser.CharacterExperienceUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_INFO_UPDATE)
			{
				PacketParser.CharacterInfoUpdate(packet);
			}
			else if (packet.Opcode == Opcode.GLOBAL_XTRAP_IDENTIFICATION && ClientlessMode)
			{
				Packet protocol = new Packet(Opcode.GLOBAL_XTRAP_IDENTIFICATION);
				protocol.WriteUInt8(2);
				protocol.WriteUInt8(2);
				//p.WriteUInt8Array(new byte[1024]);
				protocol.WriteUInt64Array(new ulong[128]);
				InjectToServer(protocol);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_SPAWN)
			{
				PacketParser.EntitySpawn(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_DESPAWN)
			{
				PacketParser.EntityDespawn(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_GROUPSPAWN_BEGIN)
			{
				PacketParser.EntityGroupSpawnBegin(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_GROUPSPAWN_DATA)
			{
				PacketParser.EntityGroupSpawnData(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_GROUPSPAWN_END)
			{
				PacketParser.EntityGroupSpawnEnd(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENVIROMENT_CELESTIAL_POSITION)
			{
				PacketParser.EnviromentCelestialPosition(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHAT_UPDATE)
			{
				PacketParser.ChatUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENVIROMENT_CELESTIAL_UPDATE)
			{
				PacketParser.EnviromentCelestialUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_MOVEMENT)
			{
				PacketParser.EntityMovement(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_LEVEL_UP)
			{
				PacketParser.EntityLevelUp(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_STATE_UPDATE)
			{
				PacketParser.EntityStateUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENVIROMENT_WHEATER_UPDATE)
			{
				PacketParser.EnviromentWheaterUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_UNIQUE_UPDATE)
			{
				PacketParser.UniqueUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_PLAYER_INVITATION_REQUEST)
			{
				PacketParser.PlayerInvitationRequest(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_PARTY_DATA)
			{
				PacketParser.PartyData(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_PARTY_UPDATE)
			{
				PacketParser.PartyUpdate(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_PARTY_MATCH_RESPONSE)
			{
				PacketParser.PartyMatchResponse(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_ENTITY_SELECTION)
			{
				PacketParser.EntitySelection(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_ADD_INT_RESPONSE)
			{
				PacketParser.CharacterAddStatPointResponse(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_ADD_STR_RESPONSE)
			{
				PacketParser.CharacterAddStatPointResponse(packet);
			}
			return false;
		}
		/// <summary>
		/// Inject a packet to the server. The delay is used to no lock the main thread.
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
			Local.Security.Send(p);
		}
	}
}