namespace xBot.Game.Objects.Item
{
	public class SRRentable
	{
		public uint ID { get; }
		public Type RentableType { get { return (Type)ID; } }
		public ushort CanDelete { get; internal set; }
		public uint PeriodBeginTime { get; internal set; }
		public uint PeriodEndTime { get; internal set; }
		public ushort CanRecharge { get; internal set; }
		public uint MeterRateTime { get; internal set; }
		public uint PackingTime { get; internal set; }
		public SRRentable(uint ID = 0)
		{
			this.ID = ID;
		}
		public enum Type : uint
		{
			None = 0,
			LimitedTime = 1,
			LimitedDistance = 2,
			Package = 3
		}
	}
}
