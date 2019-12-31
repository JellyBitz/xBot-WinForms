using System;

namespace xBot.Game.Objects.Item
{
	public class SRCoS : SRItem
	{
		public State StateType { get; set; }
		public uint ModelID { get; internal set; }
		public string ModelName { get; internal set; }
		public byte unkByte01 { get; internal set; }

		#region (Constructor)
		public SRCoS(SRItem value) : base(value) { }
		public SRCoS(SRCoS value) : base(value) {
			StateType = value.StateType;
			ModelID = value.ModelID;
			ModelName = value.ModelName;
			unkByte01 = value.unkByte01;
    }
		#endregion

		#region (Methods)
		public bool isPet()
		{
			return ID3 == 1;
    }

		public bool isTransform()
		{
			return ID3 == 2;
		}

		public bool isCube()
		{
			return ID3 == 3;
		}
		public override SRItem Clone()
		{
			return new SRCoS(this);
		}
		#endregion

		public enum State : byte
		{
			NeverSummoned = 1,
			Summoned = 2,
			Unsummoned = 3,
			Dead = 4
		}
	}
}
