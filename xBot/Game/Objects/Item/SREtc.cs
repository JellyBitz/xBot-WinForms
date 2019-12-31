namespace xBot.Game.Objects.Item
{
	public class SREtc:SRItem
	{
		public byte AssimilationProbability { get; internal set; }

		#region (Constructor)
		public SREtc(SRItem value) : base(value) { }
		public SREtc(SREtc value) : base(value) {
			AssimilationProbability = value.AssimilationProbability;
    }
		#endregion

		#region (Methods)
		public bool isAlchemy()
		{
			return ID3 == 11;
    }

		public override SRItem Clone()
		{
			return new SREtc(this);
		}
		#endregion
	}
}
