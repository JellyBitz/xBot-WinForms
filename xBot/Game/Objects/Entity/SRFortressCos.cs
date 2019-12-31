using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Entity
{
	public class SRFortressCos : SRNpc
	{
		public uint GuildID { get; set; }
		public string GuildName { get; set; }

		public SRFortressCos(uint ID) : base(ID) { }
		public SRFortressCos(string ServerName) : base(ServerName) { }
		public SRFortressCos(SRNpc value) : base(value) { }

	}
}
