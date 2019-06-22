using SecurityAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using xBot.Game;

namespace xBot.Network
{
	public class Proxy
	{
		private Gateway _gateway;
		public Gateway Gateway { get { return _gateway; } }
		private Agent _agent;
		public Agent Agent { get { return _agent; } }
		private Thread ThreadWaitConnection;
		private int _LocalReconnectionNumber;
		private int _RemoteReconnectionNumber;
		public bool ClientlessMode { get; }

		private Thread PingHandler;
		private bool _running;
		public bool isRunning
		{
			get
			{
				return _running;
			}
		}
		private int lastPortIndexSelected;
		private int lastHostIndexSelected;
		public List<ushort> GatewayPorts { get; }
		public List<string> GatewayHosts { get; }
		public Proxy(bool ClientlessMode, List<string> Hosts,List<ushort> Ports)
		{
      this.ClientlessMode = ClientlessMode;
			GatewayHosts = Hosts;
			RandomHost = false;
			GatewayPorts = Ports;
			lastPortIndexSelected = lastHostIndexSelected = -1;
		}
		private Random rand;
		/// <summary>
		/// Gets o sets the host selection to random or sequence.
		/// </summary>
		public bool RandomHost {
			get {
				return (rand != null);
			} set {
				if (value && rand == null)
				{
					rand = new Random();
				}
				else if(!value && rand != null)
				{
					rand = null;
        }
			}
		}
		/// <summary>
		/// Start the connection.
		/// </summary>
		public void Start()
		{
			_running = true;
			Thread gwThread = (new Thread(ThreadGateway));
			gwThread.Priority = ThreadPriority.AboveNormal;
			gwThread.Start();
		}
		private string SelectHost()
		{
			if (RandomHost)
				lastHostIndexSelected = rand.Next(GatewayHosts.Count);
			else
				lastHostIndexSelected++;
			if (lastHostIndexSelected == GatewayHosts.Count)
				lastHostIndexSelected = 0;
			return GatewayHosts[lastHostIndexSelected];
		}
		private ushort SelectPort()
		{
			lastPortIndexSelected++;
			if (lastPortIndexSelected == GatewayPorts.Count)
				lastPortIndexSelected = 0;
			return GatewayPorts[lastPortIndexSelected];
		}
		private void ThreadGateway()
		{
			_gateway = new Gateway(this.SelectHost(), this.SelectPort());

			Window w = Window.Get;
			Socket SocketBinded = bindSocket("127.0.0.1", 20190);
      if (!ClientlessMode)
			{
				Gateway.Local.Socket = SocketBinded;
				try
				{
					w.LogProcess("Executing EdxLoader...");
					EdxLoader loader = new EdxLoader(Bot.Get.ClientPath);
					loader.SetPatches(true,true,false);
					loader.StartClient(false, Info.Get.Locale,0, lastHostIndexSelected, ((IPEndPoint)Gateway.Local.Socket.LocalEndPoint).Port);
					
					WaitConnection(60,ref _LocalReconnectionNumber, 10);
					w.Log("Waiting for client connection [" + Gateway.Local.Socket.LocalEndPoint.ToString() + "]");
					w.LogProcess("Waiting client connection...", Window.ProcessState.Warning);
					Gateway.Local.Socket = Gateway.Local.Socket.Accept();
					w.LogProcess("Connected");
					ThreadWaitConnection.Abort();
					_LocalReconnectionNumber = 0;
        }
				catch { return; }
			}
			try
			{
				Gateway.Remote.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				w.Log("Connecting to Gateway server [" + Gateway.Host + ":" + Gateway.Port + "]");
				w.LogProcess("Waiting server connection...");
				
				WaitConnection(60, ref _RemoteReconnectionNumber, 10);
				Gateway.Remote.Socket.Connect(Gateway.Host, Gateway.Port);
				w.Log("Connected");
				w.LogProcess("Connected");
				ThreadWaitConnection.Abort();
				_RemoteReconnectionNumber = 0;
			}
			catch { return; }
			try
			{
				// Handle easily by iterating
				List<Context> gws = new List<Context>();
				gws.Add(Gateway.Remote);
				if (!ClientlessMode){
					gws.Add(Gateway.Local);
				}else{
					PingHandler = new Thread(ThreadPing);
					PingHandler.Start();
				}
				// Running process
				while (_running)
				{
					// Network input event processing
					foreach (Context context in gws)
					{
						if (context.Socket.Poll(0, SelectMode.SelectRead))
						{
							int count = context.Socket.Receive(context.Buffer.Buffer);
							if (count == 0)
							{
								throw new Exception("The remote connection has been lost.");
							}
							context.Security.Recv(context.Buffer.Buffer, 0, count);
						}
					}
					// Logic event processing
					foreach (Context context in gws)
					{
						List<Packet> packets = context.Security.TransferIncoming();
						if (packets != null)
						{
							foreach (Packet packet in packets)
							{
								// Show all incoming packets on analizer
								if (context == Gateway.Remote && w.General_cbxShowPacketServer.Checked)
								{
									bool opcodeFound = false;
									WinAPI.InvokeIfRequired(w.General_lstvOpcodes, () =>
									{
										if (w.General_lstvOpcodes.Items.Find("0x" + packet.Opcode.ToString("X4"), false).Length != 0)
											opcodeFound = true;
									});
									if (opcodeFound && w.General_rbnPacketOnlyShow.Checked
										|| !opcodeFound && !w.General_rbnPacketOnlyShow.Checked)
									{
										w.LogPacket(string.Format("[G][{0}][{1:X4}][{2} bytes]{3}{4}{6}{5}{6}", "S->C", packet.Opcode, packet.GetBytes().Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Utility.HexDump(packet.GetBytes()), Environment.NewLine));
									}
								}
								// Switch from gateway to agent process
								if (packet.Opcode == Gateway.Opcode.SERVER_LOGIN_RESPONSE)
								{
									byte result = packet.ReadByte();
									if (result == 1)
									{
										_agent = new Agent(packet.ReadUInt(), packet.ReadAscii(), packet.ReadUShort());

										string[] ip_port = SocketBinded.LocalEndPoint.ToString().Split(':');
										Thread agThread = new Thread((ThreadStart)delegate() {
											ThreadAgent(ip_port[0], int.Parse(ip_port[1])+1);
                    });
										agThread.Priority = ThreadPriority.AboveNormal;
										agThread.Start();

										Thread.Sleep(100);
										// Send packet about client listeninig
										Packet agPacket = new Packet(Gateway.Opcode.SERVER_LOGIN_RESPONSE, true);
										agPacket.WriteUInt8(result);
										agPacket.WriteUInt32(Agent.id);
										agPacket.WriteAscii(((IPEndPoint)SocketBinded.LocalEndPoint).Address.ToString());
										agPacket.WriteUInt16(((IPEndPoint)SocketBinded.LocalEndPoint).Port + 1);
										context.RelaySecurity.Send(agPacket);
									}
									else if (result == 2)
									{
										byte error = packet.ReadByte();
										switch (error)
										{
											case 1:
												{
													uint maxAttempts = packet.ReadUInt();
													uint attempts = packet.ReadUInt();
													w.Log("Password entry has failed (" + attempts + " / " + maxAttempts + " attempts)");
													w.LogProcess("Password failed", Window.ProcessState.Warning);
													w.EnableControl(w.Login_btnStart, true);
													break;
												}
											case 2:
												byte blockType = packet.ReadByte();
                        if (blockType == 1)
												{
													string blockedReason = packet.ReadAscii();
													ushort endYear = packet.ReadUShort();
													ushort endMonth = packet.ReadUShort();
													ushort endDay = packet.ReadUShort();
													ushort endHour = packet.ReadUShort();
													ushort endMinute = packet.ReadUShort();
													ushort endSecond = packet.ReadUShort();
													w.Log("Account banned till [" + endDay + "/" + endMonth + "/" + endYear + " " + endHour + "/" + endMinute + "/" + endSecond + "]. Reason: " + blockedReason);
													w.EnableControl(w.Login_btnStart, true);
												}
												break;
											default:
												w.Log("Login error [" + error+"]");
												break;
										}
										context.RelaySecurity.Send(packet);
									}
								}
								else if (!Gateway.PacketHandler(context, packet)
									&& !Gateway.IgnoreOpcode(packet.Opcode, context))
								{
									// Send normally through proxy
									context.RelaySecurity.Send(packet);
								}
							}
						}
					}
					// Network output event processing
					foreach (Context context in gws)
					{
						if (context.Socket.Poll(0, SelectMode.SelectWrite))
						{
							List<KeyValuePair<TransferBuffer, Packet>> buffers = context.Security.TransferOutgoing();
							if (buffers != null)
							{
								foreach (KeyValuePair<TransferBuffer, Packet> kvp in buffers)
								{
									TransferBuffer buffer = kvp.Key;
									Packet packet = kvp.Value;

									byte[] packet_bytes = packet.GetBytes();
									// Show outcoming packets on analizer
									if (context == Gateway.Remote && w.General_cbxShowPacketClient.Checked)
									{
										bool opcodeFound = false;
										WinAPI.InvokeIfRequired(w.General_lstvOpcodes, () =>
										{
											if (w.General_lstvOpcodes.Items.Find("0x" + packet.Opcode.ToString("X4"), false).Length != 0)
												opcodeFound = true;
										});
										if (opcodeFound && w.General_rbnPacketOnlyShow.Checked
											|| !opcodeFound && !w.General_rbnPacketOnlyShow.Checked)
										{
											w.LogPacket(string.Format("[G][{0}][{1:X4}][{2} bytes]{3}{4}{6}{5}{6}", "C->S", packet.Opcode, packet.GetBytes().Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Utility.HexDump(packet.GetBytes()), Environment.NewLine));
										}
									}
									while (true)
									{
										int count = context.Socket.Send(buffer.Buffer, buffer.Offset, buffer.Size, SocketFlags.None);
										buffer.Offset += count;
										if (buffer.Offset == buffer.Size)
										{
											break;
										}
										Thread.Sleep(1);
									}
								}
							}
						}
					}
					Thread.Sleep(1); // Cycle complete, prevent 100% CPU usage
				}
			}
			catch (Exception ex)
			{
				CloseGateway();
				w.LogPacket("[G] Error: " + ex.Message);
				if (_agent == null)
				{
					// Reset proxy
					Bot.Get.LogError(ex.ToString());
					Stop();
				}
			}
		}
		private void ThreadAgent(string Host, int Port)
		{
			Window w = Window.Get;
			// Connect AgentClient to Proxy
			if (!ClientlessMode)
			{
				Agent.Local.Socket = bindSocket(Host, Port);
				w.LogProcess("Waiting client connection...", Window.ProcessState.Warning);
				try
				{
					Agent.Local.Socket = Agent.Local.Socket.Accept();
					w.LogProcess();
				}
				catch { Reset(); return; }
			}
			Agent.Remote.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				w.LogProcess("Waiting server connection...");
				Agent.Remote.Socket.Connect(Agent.Host, Agent.Port);
			}
			catch
			{
				w.Log("Failed to connect to the server");
				w.LogProcess();
				Reset();
				return;
			}
			List<Context> ags = new List<Context>();
			if (!Agent.ClientlessMode)
			{
				ags.Add(Agent.Local);
			}
			ags.Add(Agent.Remote);
			try
			{
				while (_running)
				{
					// Network input event processing
					foreach (Context context in ags)
					{
						if (context.Socket.Poll(0, SelectMode.SelectRead))
						{
							int count = context.Socket.Receive(context.Buffer.Buffer);
							if (count == 0)
							{
								throw new Exception("The remote connection has been lost");
							}
							context.Security.Recv(context.Buffer.Buffer, 0, count);
						}
					}
					// Logic event processing
					foreach (Context context in ags)
					{
						List<Packet> packets = context.Security.TransferIncoming();
						if (packets != null)
						{
							foreach (Packet packet in packets)
							{
								// Show all incoming packets on analizer
								if (context == Agent.Remote && w.General_cbxShowPacketServer.Checked)
								{
									bool opcodeFound = false;
									WinAPI.InvokeIfRequired(w.General_lstvOpcodes, () =>
									{
										if (w.General_lstvOpcodes.Items.Find("0x" + packet.Opcode.ToString("X4"), false).Length != 0)
											opcodeFound = true;
									});
									if (opcodeFound && w.General_rbnPacketOnlyShow.Checked
										|| !opcodeFound && !w.General_rbnPacketOnlyShow.Checked)
									{
										w.LogPacket(string.Format("[A][{0}][{1:X4}][{2} bytes]{3}{4}{6}{5}{6}", "S->C", packet.Opcode, packet.GetBytes().Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Utility.HexDump(packet.GetBytes()), Environment.NewLine));
									}
								}
								if (!Agent.PacketHandler(context, packet) && !Agent.IgnoreOpcode(packet.Opcode, context))
								{
									// Send normally through proxy
									context.RelaySecurity.Send(packet);
								}
							}
						}
					}
					// Network output event processing
					foreach (Context context in ags)
					{
						if (context.Socket.Poll(0, SelectMode.SelectWrite))
						{
							List<KeyValuePair<TransferBuffer, Packet>> buffers = context.Security.TransferOutgoing();
							if (buffers != null)
							{
								foreach (KeyValuePair<TransferBuffer, Packet> kvp in buffers)
								{
									TransferBuffer buffer = kvp.Key;
									Packet packet = kvp.Value;

									byte[] packet_bytes = packet.GetBytes();
									// Show outcoming packets on analizer
									if (context == Agent.Remote && w.General_cbxShowPacketClient.Checked)
									{
										bool opcodeFound = false;
										WinAPI.InvokeIfRequired(w.General_lstvOpcodes, () =>
										{
											if (w.General_lstvOpcodes.Items.Find("0x" + packet.Opcode.ToString("X4"), false).Length != 0)
												opcodeFound = true;
										});
										if (opcodeFound && w.General_rbnPacketOnlyShow.Checked
											|| !opcodeFound && !w.General_rbnPacketOnlyShow.Checked)
										{
											w.LogPacket(string.Format("[A][{0}][{1:X4}][{2} bytes]{3}{4}{6}{5}{6}", "C->S", packet.Opcode, packet.GetBytes().Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Utility.HexDump(packet.GetBytes()), Environment.NewLine));
										}
									}
									while (true)
									{
										int count = context.Socket.Send(buffer.Buffer, buffer.Offset, buffer.Size, SocketFlags.None);
										buffer.Offset += count;
										if (buffer.Offset == buffer.Size)
										{
											break;
										}
										Thread.Sleep(1);
									}
								}
							}
						}
					}
					Thread.Sleep(1); // Cycle complete, prevent 100% CPU usage
				}
			}
			catch (Exception ex)
			{
				w.Log("Disconnected");
				w.LogPacket("[A] Error: " + ex.Message + Environment.NewLine);
				Bot.Get.LogError(ex.ToString());
				Stop();
			}
		}
		private Socket bindSocket(string ip, int port)
		{
			Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			for (int i = port; i < ushort.MaxValue; i += 2)
			{
				try
				{
					s.Bind(new IPEndPoint(IPAddress.Parse(ip), i));
					s.Listen(1);
					return s;
				}
				catch (SocketException)
				{ /* ignore and continue */
				}
			}
			return null;
		}
		/// <summary>
		/// Wait a connection and try to reconnect the proxy if is necessary.
		/// </summary>
		/// <param name="seconds">Maximum time for waiting</param>
		/// <param name="nReconnection">Current connection counter</param>
		/// <param name="maxReconection">Max connections to stop</param>
		private void WaitConnection(int seconds,ref int nReconnection,int maxReconection)
		{
			int refnReconnection = nReconnection;
      ThreadWaitConnection = (new Thread((ThreadStart)delegate {
				while (true)
				{
					if (seconds == 0)
					{
						Reset();
						Start();
						refnReconnection++;
						return;
					}
					Thread.Sleep(1000);
					seconds--;
				}
			}));
      ThreadWaitConnection.Start();
		}
		private void ThreadPing()
		{
			while (_running)
			{
				// Keep only one connection alive at clientless mode
				if (Agent != null)
				{
					try
					{
						Packet p = new Packet(Agent.Opcode.GLOBAL_PING);
						Agent.InjectToServer(p);
					}
					catch { /*Connection closed*/ _agent = null; }
				}
			  if (Gateway != null)
				{
					try
					{
						Packet p = new Packet(Gateway.Opcode.GLOBAL_PING);
						Gateway.InjectToServer(p);
					}
					catch { /*Connection closed*/ _gateway = null; }
				}
				Thread.Sleep(6666);
			}
		}
		private void CloseGateway()
		{
			if (Gateway != null)
			{
				if (Gateway.Local.Socket != null)
				{
					if (Gateway.Local.Socket.Connected)
					{
						Gateway.Local.Socket.Disconnect(true);
					}
					Gateway.Local.Socket.Close();
				}
				if (Gateway.Remote.Socket != null)
				{
					Gateway.Remote.Socket.Close();
				}
				_gateway = null;
			}
		}
		private void CloseAgent()
		{
			if (Agent != null)
			{
				if (Agent.Local.Socket != null)
				{
					_agent.Local.Socket.Close();
					_agent.Local.Socket = null;
				}
				if (Agent.Remote.Socket != null)
				{
					_agent.Remote.Socket.Close();
					_agent.Remote.Socket = null;
				}
				_agent = null;
			}
		}
		private void Reset()
		{
			_running = false;
			if (PingHandler != null)
				PingHandler.Abort();
			Bot.Get.CloseSROClient();
			CloseGateway();
			CloseAgent();
		}
		public void Stop()
		{
			if (ThreadWaitConnection != null)
				ThreadWaitConnection.Abort();
			Reset();
			
      Info.Get.Database.Close();
			Window w = Window.Get;
			w.LogProcess("Disconnected");
			// Reset locket controls
			WinAPI.InvokeIfRequired(w.Login_cmbxSilkroad, () => {
				w.Login_cmbxSilkroad.Enabled = true;
			});
			w.EnableControl(w.General_btnAddSilkroad, true);
			WinAPI.InvokeIfRequired(w.Login_btnStart, () => {
				w.Login_btnStart.Text = "START";
				w.EnableControl(w.Login_btnStart, true);
			});
			w.EnableControl(w.Login_btnLauncher, true);
			
			WinAPI.InvokeIfRequired(w.Login_gbxCharacters, () => {
				w.Login_gbxCharacters.Visible = false;
			});
			WinAPI.InvokeIfRequired(w.Login_gbxServers, () => {
				w.Login_gbxServers.Visible = true;
			});
		}
		/// <summary>
		/// Send packet to the server if exists connection (Gateway/Agent).
		/// </summary>
		/// <param name="p">Packet to inject</param>
		public void InjectToServer(Packet packet)
		{
			if (Agent != null && Agent.Remote != null && Agent.Remote.Socket.Connected)
			{
				Agent.InjectToServer(packet);
			}
			else if (Gateway != null && Gateway.Remote != null && Gateway.Remote.Socket.Connected)
			{
				Gateway.InjectToServer(packet);
			}
		}
		/// <summary>
		/// Send packet to the client if exists connection (Gateway/Agent).
		/// </summary>
		/// <param name="p">Packet to inject</param>
		public void InjectToClient(Packet packet)
		{
			if (Agent != null && Agent.Remote != null && Agent.Remote.Socket.Connected)
			{
				Agent.InjectToServer(packet);
			}
			else if (Gateway != null && Gateway.Remote != null && Gateway.Remote.Socket.Connected)
			{
				Gateway.InjectToServer(packet);
			}
		}
	}
}