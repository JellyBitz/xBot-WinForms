using System;
using System.Text;

namespace xBot.Game.Objects.Party
{
	public class SRPartyMatch
	{
		public uint Number { get; set; }
		public uint MasterJoinID { get; set; }
		public string MasterName { get; set; }
		public string Title { get; set; }
		public byte LevelMin { get; set; }
		public byte LevelMax { get; set; }
		public SRParty.Setup Setup { get; set; }
		public SRParty.Purpose Purpose { get; set; }
		public byte MemberCount { get; set; }
		public byte MemberMax
		{
			get { return (byte)(Setup.HasFlag(SRParty.Setup.ExpShared) ? 8 : 4); }
		}
		public bool isJobMode
		{
			get { return Purpose == SRParty.Purpose.Thief || Purpose == SRParty.Purpose.Trader; }
		}
		public SRPartyMatch()
		{

		}

		public string GetTooltip()
		{
			StringBuilder tooltip = new StringBuilder();
			tooltip.Append("Exp. ");
      if (Setup.HasFlag(SRParty.Setup.ExpShared))
				tooltip.AppendLine("Shared");
			else
				tooltip.AppendLine("Free-For-All");
			tooltip.Append("Item ");
			if (Setup.HasFlag(SRParty.Setup.ItemShared))
				tooltip.AppendLine("Shared");
			else
				tooltip.AppendLine("Free-For-All");
			if (Setup.HasFlag(SRParty.Setup.AnyoneCanInvite))
				tooltip.Append("Anyone");
			else
				tooltip.Append("Only Master");
			tooltip.Append(" Can Invite");
			return tooltip.ToString();
		}
	}
}