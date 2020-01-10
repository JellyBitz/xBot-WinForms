using System;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Item;

namespace xBot.Game.Objects.Entity
{
	public class SRCharacter:SRPlayer
	{
		private byte m_Level;
		private byte m_JobLevel;

		#region (Properties)
		public byte Level
		{
			get { return m_Level; }
			set {
				m_Level = value;
				ExpMax = DataManager.GetExpMax(value);
			}
		}
		public byte LevelMax { get; internal set; }
		public ulong Exp { get; internal set; }
		public ulong ExpMax { get; private set; }
		public uint SPExp { get; internal set; }
		public ulong Gold { get; internal set; }
		public uint SP { get; internal set; }
		public ushort StatPoints { get; internal set; }
		public byte BerserkPoints { get; internal set; }
		public uint GatheredExpPoint { get; internal set; }
		public uint MPMax { get; internal set; }
		public byte PKDaily { get; internal set; }
		public ushort PKTotal { get; internal set; }
		public uint PKPenalty { get; internal set; }
		public xDictionary<uint, SRMastery> Masteries { get; internal set; }
		public xDictionary<uint, SRSkill> Skills { get; internal set; }
		public xDictionary<uint, SRQuest> QuestsCompleted { get; internal set; }
		public xDictionary<uint, SRQuest> Quests { get; internal set; }
		public xDictionary<uint, SRCollectionBook> CollectionBooks { get; internal set; }
		public byte unkByte04 { get; internal set; }
		public string JobName { get; internal set; }
		new public byte JobLevel
		{
			get { return m_JobLevel; }
			set
			{
				m_JobLevel = value;
				JobExpMax = DataManager.GetJobExpMax(value,JobType);
			}
		}
		public uint JobExp { get; set; }
		public uint JobExpMax { get; private set; }
		public uint JobContribution { get; internal set; }
		public uint JobReward { get; internal set; }
		public uint JoinID { get; internal set; }
		public ulong GuideFlag { get; internal set; }
		public bool isGameMaster { get; internal set; }
		public uint PhyAtkMin { get; internal set; }
		public uint PhyAtkMax { get; internal set; }
		public uint MagAtkMin { get; internal set; }
		public uint MagAtkMax { get; internal set; }
		public ushort PhyDefense { get; internal set; }
		public ushort MagDefense { get; internal set; }
		public ushort HitRate { get; internal set; }
		public ushort ParryRatio { get; internal set; }
		public ushort STR { get; internal set; }
		public ushort INT { get; internal set; }
		public xList<SRItem> Storage { get; internal set; }
		public ulong StorageGold { get; internal set; }
		public xList<SRItem> ShopBuyBack { get; internal set; }
		#endregion

		#region (Constructor)
		public SRCharacter(uint ID) : base(ID) { }
		public SRCharacter(string ServerName) : base(ServerName) { }
		public SRCharacter(SRPlayer value) : base(value) { }
		#endregion

		#region (Methods)
		public int GetHPPercent()
		{
			return (int)Math.Round(HP * 100f / HPMax);
		}
		public int GetMPPercent()
		{
			return (int)Math.Round(MP * 100f / MPMax);
		}
		public override bool hasJobMode()
		{
			return Inventory.FindIndex(item => item != null && item.isEquipable() && ((SREquipable)item).isJob(),0,13) != -1;
		}
		#endregion
	}
}
