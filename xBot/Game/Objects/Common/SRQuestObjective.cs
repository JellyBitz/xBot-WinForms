using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Common
{
	public class SRQuestObjective
	{
		public uint ID { get; set; }
		public bool isEnabled { get; internal set; }
		public string Name { get; internal set; }
		public uint[] TasksID { get; internal set; }

		public SRQuestObjective(uint ID)
		{
			this.ID = ID;
		}
	}
}
