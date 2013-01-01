namespace xBot.Game.Objects
{
	public class SRPartyMatch
	{
		public uint Number { get; }
		public uint OwnerJoinID { get; set; }
		public string Owner { get; set; }
		public string Title { get; set; }
		public byte LevelMin { get; set; }
		public byte LevelMax { get; set; }
		public Types.PartySetup Setup { get; set; }
		public Types.PartyPurpose Purpose { get; set; }
		public byte MemberCount { get; set; }
		public byte MemberMax
		{
			get{
				return (byte)(Setup.HasFlag(Types.PartySetup.ExpShared) ? 8 : 4);
			}
		}
		public bool isJobMode
		{
			get{
				return Purpose == Types.PartyPurpose.Thief || Purpose == Types.PartyPurpose.Trader;
			}
		}
		public SRPartyMatch(uint Number)
		{
			this.Number = Number;
		}
	}
}