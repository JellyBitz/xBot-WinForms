using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Common
{
	public class SRQuest
	{
		public uint ID { get; set; }
		public byte Achievements { get; internal set; }
		public bool isAutoShareRequired { get; internal set; }
		public byte QuestType { get; internal set; }
		public uint TimeRemain { get; internal set; }
		public byte State { get; internal set; }
		public uint[] NpcsID { get; internal set; }
		public xList<SRQuestObjective> Objectives { get; internal set; }

		public SRQuest(uint ID)
		{
			this.ID = ID;
		}
		public enum Type:byte
		{
			HasObjectives = 8,
			TimeLimit = 28,
			HasNPCs = 88
		}
	}
}
