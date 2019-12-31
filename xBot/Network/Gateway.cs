using SecurityAPI;
using System.Collections.Generic;
using xBot.App;
using xBot.Game;

namespace xBot.Network
{
	public class Gateway
	{
		public static class Opcode
		{
			// static opcodes could be edited at realtime (for different vSRO types)
			public static ushort
				CLIENT_HWID_RESPONSE = 0,
				SERVER_HWID_REQUEST = 0;
			public const ushort
				CLIENT_PATCH_REQUEST = 0x6100,
				CLIENT_SHARD_LIST_REQUEST = 0x6101,
				CLIENT_LOGIN_REQUEST = 0x6102,
				CLIENT_CAPTCHA_SOLVED_REQUEST = 0x6323,

				SERVER_PATCH_RESPONSE = 0xA100,
				SERVER_SHARD_LIST_RESPONSE = 0xA101,
				SERVER_LOGIN_RESPONSE = 0xA102,
				SERVER_CAPTCHA_DATA = 0x2322,
				SERVER_CAPTCHA_SOLVED_RESPONSE = 0xA323,

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
			if (context == Local) {
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
			if (packet.Opcode == Opcode.CLIENT_PATCH_REQUEST)
			{
				string service = packet.ReadAscii();
				if (service == "GatewayServer")
				{
					byte locale = packet.ReadByte();
					DataManager.SR_Client = packet.ReadAscii(); // SR_CLIENT
				  uint version = packet.ReadUInt();

					if (DataManager.Version != version)
					{
						DataManager.Version = version;
						Window.Get.Log("Warning: The bot database is outdate, try to update it at first to avoid parsing errors");
					}
					else if (DataManager.Locale != locale)
					{
						DataManager.Locale = locale;
						Window.Get.Log("A new Locale has been found [" + locale + "]");
					}
				}
			}
			else if (packet.Opcode == Opcode.CLIENT_LOGIN_REQUEST)
			{
				packet.ReadByte(); // locale
				packet.ReadAscii(); // id
				packet.ReadAscii(); // psw
				InfoManager.ServerID = packet.ReadUShort().ToString();

				Window w = Window.Get;
				w.Login_lstvServers.InvokeIfRequired(() => {
					InfoManager.ServerName = w.Login_lstvServers.Items[InfoManager.ServerID].Text;
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
			switch(packet.Opcode)
			{
				case Opcode.GLOBAL_IDENTIFICATION:
					if(ClientlessMode){
						string service = packet.ReadAscii();
						if (service == "GatewayServer")
						{
							Packet p = new Packet(Opcode.CLIENT_PATCH_REQUEST, true);
							p.WriteByte(DataManager.Locale);
							p.WriteAscii(DataManager.SR_Client);
							p.WriteUInt(DataManager.Version);
							InjectToServer(p);
						}
					}
					break;
				case Opcode.SERVER_PATCH_RESPONSE:
					if(ClientlessMode){
						switch (packet.ReadByte()) {
							case 1:
								Packet p = new Packet(Opcode.CLIENT_SHARD_LIST_REQUEST, true);
								this.InjectToServer(p);
								break;
							case 2:
								byte errorCode = packet.ReadByte();
								if (errorCode == 2)
								{
									string DownloadServerIP = packet.ReadAscii();
									ushort DownloadServerPort = packet.ReadUShort();
									uint DownloadServerCurVersion = packet.ReadUInt();
									Window.Get.Log("Version outdate. Please, verify that client and database (v" + DataManager.Version + ") are up to date (v" + DownloadServerCurVersion + ")");
								}
								else
								{
									Window.Get.Log("Patch error: [" + errorCode + "]");
								}
								Bot.Get.Proxy.Stop();
								break;
							}
					}
					break;
				case Opcode.SERVER_SHARD_LIST_RESPONSE:
					PacketParser.ShardListResponse(packet);
					break;
				case Opcode.SERVER_CAPTCHA_DATA:
					PacketParser.CaptchaData(packet);
					break;
				case Opcode.SERVER_CAPTCHA_SOLVED_RESPONSE:
					// success
					if(packet.ReadBool())
					{
						Window.Get.Log("Captcha entered successfully!");
					}
					else
					{
						uint maxAttempts = packet.ReadUInt();
						uint attempts = packet.ReadUInt();
						Window.Get.Log("Captcha entry has failed (" + attempts + " / " + maxAttempts + " attempts)");
					}
					break;
			}
			return false;
		}
		public void InjectToServer(Packet p)
		{
			this.Remote.Security.Send(p);
		}
		public void InjectToClient(Packet p)
		{
			this.Remote.RelaySecurity.Send(p);
		}
	}
}