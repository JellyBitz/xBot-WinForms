using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xBot.Game.Objects.Item;

namespace xBot.Game.Objects.Guild
{
	public class SRGuild
	{
		public uint ID { get; set; }
		public string Name { get; set; }
		public byte Level { get; set; }
		public uint GPoints { get; set; }
		public string Notice { get; set; }
		public string Message { get; set; }
		public xDictionary<uint, SRGuildMember> Members { get; private set; }
		public SRGuildMember Master { get; set; }
		public ulong StorageGold { get; set; }
		public xList<SRItem> Storage { get; set; }

		public SRGuild()
		{
			Members = new xDictionary<uint, SRGuildMember>();
		}
	}
}
