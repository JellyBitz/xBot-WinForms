using SecurityAPI;
using System.Net.Sockets;

namespace xBot.Network
{
	public class Context
	{
		public Security Security { get; set; }
		public Security RelaySecurity { get; set; }
		public TransferBuffer Buffer { get; set; }
		public Socket Socket { get; set; }
		public Context()
		{
			Socket = null;
			Security = new Security();
			RelaySecurity = null;
			Buffer = new TransferBuffer(8192);
		}
	}
}
