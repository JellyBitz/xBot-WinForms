using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Entity
{
	public class SRFortressStruct:SRNpc
	{
		public uint refEventStructID { get; set; }
		public ushort State { get; set; }

		public SRFortressStruct(uint ID) : base(ID) { }
		public SRFortressStruct(string ServerName) : base(ServerName) { }
		public SRFortressStruct(SRNpc value) : base(value) { }

	}
}
