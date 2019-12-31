using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Common
{
	public class SRCollectionBook
	{
		public uint ID { get; set; }
		public SRTimeStamp StartedDatetime { get; internal set; }
		public uint Pages { get; internal set; }

		public SRCollectionBook(uint ID)
		{
			this.ID = ID;
		}
	}
}
