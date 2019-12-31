using System;
using xBot.Game.Objects.Item;

namespace xBot.Game.Objects.Common
{
	public class SRCharSelection
	{
		private byte m_Level;
		public DateTime DeletingDate { get; internal set; }
		public bool isDeleting { get { return DeletingDate != default(DateTime); } }
		public uint HP { get; internal set; }
		public ushort INT { get; internal set; }
		public byte Level
		{
			get { return m_Level; }
			set
			{
				m_Level = value;
				ExpMax = DataManager.GetExpMax(value);
			}
		}
		public ulong Exp { get; internal set; }
		public ulong ExpMax { get; private set; }
		public uint ModelID { get; internal set; }
		public uint MP { get; internal set; }
		public string Name { get; internal set; }
		public byte Scale { get; internal set; }
		public ushort StatPoints { get; internal set; }
		public ushort STR { get; internal set; }
		public GuildMember GuildMemberType { get; internal set; }
		public AcademyMember AcademyMemberType { get; internal set; }
		public string GuildName { get; internal set; }
		public xList<SRItem> Inventory { get; internal set; }
		public xList<SREquipable> InventoryAvatar { get; internal set; }

		public float GetExpPercent()
		{
			return Exp * 100f / ExpMax;
		}

		public enum GuildMember : byte
		{
			None = 0,
			Member = 1,
			Master = 2
		}
		public enum AcademyMember : byte
		{
			None = 0
		}
	}
}
