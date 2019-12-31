using xBot.Game.Objects.Item;
namespace xBot.Game.Objects.Common
{
	public class SRStall
	{
		public string Title { get; set; }
		public uint DecorationID { get; set; }
		public string Note { get; set; }
		public xList<SRItemStall> Inventory { get; set; }

		public SRStall()
		{

		}
	}
}
