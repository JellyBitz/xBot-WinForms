using xBot.Game.Objects.Common;

namespace xBot.Game.Objects.Party
{
	public class SRPartyMember
	{
		public uint ID { get; set; }
		public string Name { get; set; }
		public string GuildName { get; set; }
		public byte Level { get; set; }
		public SRCoord Position { get; set; }
		public uint ModelID { get; set; }
		public byte HPMP { get; set; }
		public byte HPPercent { get { return (byte)((HPMP & 15) * 10); } }
		public byte MPPercent { get { return (byte)((HPMP >> 4) * 10); } }
		public uint MasteryPrimaryID { get; set; }
		public uint MasterySecondaryID { get; set; }
		public SRPartyMember()
		{

		}
	}
}
