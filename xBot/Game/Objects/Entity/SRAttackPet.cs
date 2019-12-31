using System;
namespace xBot.Game.Objects.Entity
{
	public class SRAttackPet : SRCoService
	{
		private byte m_Level;

		#region (Properties)
		public uint unkUInt01 { get; internal set; }
		public byte Level
		{
			get { return m_Level; }
			set
			{
				m_Level = value;
				ExpMax = DataManager.GetPetExpMax(value);
				HP = HPMax;
			}
		}
		public ulong Exp { get; internal set; }
		public ulong ExpMax { get; private set; }
		public ushort HGP { get; internal set; }
		public Settings SettingsFlags { get; internal set; }
		public byte unkByte03 { get; internal set; }
		public byte unkByte04 { get; internal set; }
		#endregion

		#region (Constructor)
		public SRAttackPet(uint ID) : base(ID) { }
		public SRAttackPet(string ServerName) : base(ServerName) { }
		public SRAttackPet(SRCoService value) : base(value) { }
		#endregion

		[Flags]
		public enum Settings : uint
		{
			Disabled = 0,
			Offensive = 1
		}
	}
}
