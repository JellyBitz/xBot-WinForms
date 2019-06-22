using SecurityAPI;
using System.Collections.Generic;
using xBot.Game;

namespace xBot.Network
{
	public class Gateway
	{
		public static class Opcode
		{
			// static opcodes could be edited at realtime (for different vSRO types)
			public static ushort
				CLIENT_HWID = 0,
				SERVER_HWID = 0;
			public const ushort
				CLIENT_PATCH_REQUEST = 0x6100,
				CLIENT_SHARD_LIST_REQUEST = 0x6101,
				CLIENT_LOGIN_REQUEST = 0x6102,

				SERVER_PATCH_RESPONSE = 0xA100,
				SERVER_SHARD_LIST_RESPONSE = 0xA101,
				SERVER_LOGIN_RESPONSE = 0xA102,

				GLOBAL_HANDSHAKE = 0x5000,
				GLOBAL_HANDSHAKE_OK = 0x9000,
				GLOBAL_IDENTIFICATION = 0x2001,
				GLOBAL_PING = 0x2002;
    }
		public Context Local { get; }
		public Context Remote { get; }
		public List<ushort> IgnoreOpcodeClient { get; }
		public List<ushort> IgnoreOpcodeServer { get; }
		public string Host { get; }
		public ushort Port { get; }
		public Gateway(string host,ushort port)
		{
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
					if (Bot.Get.SaveHWIDFrom == "Gateway" || Bot.Get.SaveHWIDFrom == "Both")
					{
						Bot.Get.SaveHWID(packet.GetBytes());
					}
				}
				return Local_PacketHandler(packet);
			}
			// HWID setup (sending data to server)
			if (packet.Opcode == Opcode.SERVER_HWID && ClientlessMode && Bot.Get.hasHWID)
			{
				if (Bot.Get.SendHWIDTo == "Gateway" || Bot.Get.SaveHWIDFrom == "Both")
				{
					byte[] hwidData = Bot.Get.LoadHWID();
					if (hwidData != null)
					{
						Packet p = new Packet(Opcode.CLIENT_HWID, false, false, hwidData);
						Bot.Get.Proxy.Gateway.InjectToServer(p);
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
			if (packet.Opcode == Opcode.CLIENT_PATCH_REQUEST)
			{
				string service = packet.ReadAscii();
				if (service == "GatewayServer")
				{
					byte locale = packet.ReadByte();
					packet.ReadAscii(); // SR_CLIENT
					uint version = packet.ReadUInt();
					
					Info i = Info.Get;
					if (i.Version != version)
						Window.Get.Log("Warning: The bot database is outdate, try to update to avoid future errors");
					i.Version = version;
				}
			}
			else if (packet.Opcode == Opcode.CLIENT_LOGIN_REQUEST)
			{
				packet.ReadByte(); // locale
				packet.ReadAscii(); // id
				packet.ReadAscii(); // psw
				ushort serverID = packet.ReadUShort(); // server id
				
				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Login_lstvServers, ()=> {
					Info.Get.Server = w.Login_lstvServers.Items.Find(serverID.ToString(), false)[0].Text;
        });
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
			if (packet.Opcode == Opcode.GLOBAL_IDENTIFICATION && ClientlessMode)
			{
				string service = packet.ReadAscii();
				if (service == "GatewayServer")
				{
					Packet p = new Packet(Opcode.CLIENT_PATCH_REQUEST, true);
					p.WriteUInt8(Info.Get.Locale);
					p.WriteAscii("SR_Client");
          p.WriteUInt32(Info.Get.Version);
					this.InjectToServer(p);
				}
			}
			else if (packet.Opcode == Opcode.SERVER_PATCH_RESPONSE && ClientlessMode)
			{
				switch (packet.ReadByte()) {
					case 1:
						Packet p = new Packet(Opcode.CLIENT_SHARD_LIST_REQUEST, true);
						this.InjectToServer(p);
						break;
					case 2:
						Window.Get.Log("Client Version incorrect (outdated) or the server is down");
						Bot.Get.Proxy.Stop();
						break;
				}
			}
			else if (packet.Opcode == Opcode.SERVER_SHARD_LIST_RESPONSE)
			{
				Bot.Get.LoginFromBot = false;
				PacketParser.ShardListResponse(packet);
			}
			return false;
		}
		public void InjectToServer(Packet p)
		{
			this.Remote.Security.Send(p);
		}
		public void InjectToClient(Packet p)
		{
			this.Local.Security.Send(p);
		}
	}
}