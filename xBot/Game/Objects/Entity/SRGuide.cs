using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Entity
{
	public class SRGuide:SRNpc
	{
		public SRGuide(uint ID) : base(ID) { }
		public SRGuide(string ServerName) : base(ServerName) { }
		public SRGuide(SRNpc value) : base(value) { }
	}
}
