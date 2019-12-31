using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Item
{
	public class SRAdvancedElixir
	{
		public uint ID { get; set; }
		public byte Slot { get; set; }
		public uint Value { get; set; }
		public SRAdvancedElixir(byte Slot, uint ID)
		{
			this.Slot = Slot;
			this.ID = ID;
		}
	}
}
