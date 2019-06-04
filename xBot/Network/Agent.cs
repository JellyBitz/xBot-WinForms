using SecurityAPI;
using System.Collections.Generic;
using System.Drawing;
using xBot.Network.Packets;

namespace xBot.Network
{
	public class Agent
	{
		public static class Opcode
		{
			// static opcodes could be edited at realtime (for different vSRO types)
			public static ushort
				CLIENT_HWID = 0,
				SERVER_HWID = 0;
			public const ushort
				CLIENT_AUTH_REQUEST = 0x6103,
				CLIENT_CHARACTER_SELECTION_JOIN_REQUEST = 0x7001,
				CLIENT_CHARACTER_SELECTION_ACTION_REQUEST = 0x7007,
				CLIENT_CHAT_REQUEST = 0x7025,
				CLIENT_CONFIRM_UNKNOWN = 0x750E,
				CLIENT_CONFIRM_SPAWN = 0x3012,

				SERVER_AUTH_RESPONSE = 0xA103,
				SERVER_CHARACTER_SELECTION_JOIN_RESPONSE = 0xB001,
				SERVER_CHARACTER_SELECTION_ACTION_RESPONSE = 0xB007,
				SERVER_ENTITY_MOVEMENT = 0xB021,
				SERVER_CHARACTER_DATA_BEGIN = 0x34A5,
				SERVER_CHARACTER_DATA = 0x3013,
				SERVER_CHARACTER_DATA_END = 0x34A6,
				SERVER_ENTITY_SPAWN = 0x3015,
				SERVER_ENTITY_DESPAWN = 0x3016,
				SERVER_ENTITY_GROUPSPAWN_BEGIN = 0x3017,
				SERVER_ENTITY_GROUPSPAWN_END = 0x3018,
				SERVER_ENTITY_GROUPSPAWN_DATA = 0x3019,
				SERVER_ENVIROMENT_CELESTIAL_POSITION = 0x3020,
				SERVER_CHAT_UPDATE = 0x3026,
				SERVER_ENVIROMENT_CELESTIAL_UPDATE = 0x3027,

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
		public Agent(uint queqeID,string host,ushort port)
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
																						 // ignore list
			IgnoreOpcodeClient = new List<ushort>(new ushort[] { Opcode.GLOBAL_HANDSHAKE, Opcode.GLOBAL_HANDSHAKE_OK });
			IgnoreOpcodeClient.Add(Opcode.GLOBAL_IDENTIFICATION); // proxy to remote is handled by API
			IgnoreOpcodeServer = new List<ushort>(new ushort[] { Opcode.GLOBAL_HANDSHAKE, Opcode.GLOBAL_HANDSHAKE_OK });
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
			if (context == Local) {
				// HWID setup (saving/updating data from client)
				if (packet.Opcode == Opcode.CLIENT_HWID)
				{
					if (Bot.Get.SaveHWIDFrom == "Agent" || Bot.Get.SaveHWIDFrom == "Both")
					{
						Bot.Get.SaveHWID(packet.GetBytes());
					}
				}
				return Local_PacketHandler(packet);
			}
			// HWID setup (sending data to server)
			if (packet.Opcode == Opcode.SERVER_HWID && ClientlessMode && Bot.Get.hasHWID)
			{
				if (Bot.Get.SendHWIDTo == "Agent" || Bot.Get.SaveHWIDFrom == "Both")
				{
					byte[] hwidData = Bot.Get.LoadHWID();
					if (hwidData != null)
					{
						Packet p = new Packet(Opcode.CLIENT_HWID, false, false, hwidData);
						Bot.Get.Proxy.Agent.InjectToServer(p);
						Window w = Window.Get;
						w.Log("HWID Sent!");
						Window.InvokeIfRequired(w.General_rtbxHWIDdata, () =>
						{
							w.ToolTips.SetToolTip(w.General_rtbxHWIDdata, "HWID Sent!");
						});
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
			if (packet.Opcode == Opcode.CLIENT_AUTH_REQUEST && Bot.Get.LoginWithBot)
			{
				return true;
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
			if (packet.Opcode == Opcode.GLOBAL_IDENTIFICATION && Bot.Get.LoginWithBot) {
				string service = packet.ReadAscii();
				if (service == "AgentServer")
				{
					Packet p = new Packet(Opcode.CLIENT_AUTH_REQUEST, true);
					p.WriteUInt32(id);
					p.WriteAscii(Window.Get.Login_tbxUsername.Text);
					p.WriteAscii(Window.Get.Login_tbxPassword.Text);
					p.WriteUInt16(Game.Data.Get.Locale);
					// MAC
					p.WriteUInt32(0);
					p.WriteUInt16(0);
					this.InjectToServer(p);
				}
			}
			else if (packet.Opcode == Opcode.SERVER_AUTH_RESPONSE)
			{
				byte success = packet.ReadByte();
				if (success == 1)
				{
					Window.Get.Log("Logged successfully!");
					Window.Get.setState("Logged");
					Window.InvokeIfRequired(Window.Get.Login_btnStart, () =>
					{
						Window.Get.Login_btnStart.Font = new Font(Window.Get.Login_btnStart.Font, FontStyle.Strikeout);
					});
					if (ClientlessMode)
					{
						PacketBuilder.RequestCharacterList();
					}
				}
				else
				{
					byte error = packet.ReadByte();
					Window.Get.Log("Login error [" + error + "]");
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
					w.Log("Character selected");
					Window.InvokeIfRequired(w.Login_btnStart, () =>
					{
						w.Login_btnStart.Font = new Font(w.Login_btnStart.Font, FontStyle.Strikeout);
					});
					w.setState("Loading...");
				}
				else
				{
					int error = packet.ReadUShort();
					w.Log("Error: " + error);
					w.setState("Error", Window.BotState.Error);
				}
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_DATA_BEGIN)
			{
				PacketParser.CharacterDataBegin(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_DATA)
			{
				PacketParser.CharacterData(packet);
			}
			else if (packet.Opcode == Opcode.SERVER_CHARACTER_DATA_END)
			{
				// Generating Bot Event to keep this method clean
				Bot.Get._Event_Teleported();

				PacketParser.CharacterDataEnd(packet);
        if (ClientlessMode)
				{
					// Confirm spawn after loading
					Packet protocol = new Packet(Opcode.CLIENT_CONFIRM_UNKNOWN);
					InjectToServer(protocol);

					protocol = new Packet(Opcode.CLIENT_CONFIRM_SPAWN);
					InjectToServer(protocol);
				}
			}
			else if (packet.Opcode == Opcode.GLOBAL_XTRAP_IDENTIFICATION && ClientlessMode)
			{
				Packet p = new Packet(Opcode.GLOBAL_XTRAP_IDENTIFICATION);
				p.WriteUInt8(2);
				p.WriteUInt8(2);
				//p.WriteUInt8Array(new byte[1024]);
				p.WriteUInt64Array(new ulong[128]);
				InjectToServer(p);
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
			return false;
		}
		public void InjectToServer(Packet p)
		{
			Remote.Security.Send(p);
		}
		public void InjectToClient(Packet p)
		{
			Local.Security.Send(p);
		}
	}
}