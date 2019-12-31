using System;
using xBot.Game.Objects.Item;
namespace xBot.Game.Objects.Entity
{
	public class SRCoService:SRNpc
	{
		#region (Properties)
		public SRPlayer.Job JobType { get; set; }
		public SRPlayer.PVPState PVPStateType { get; internal set; }
		public uint OwnerUniqueID { get; internal set; }
		public uint OwnerObjectID { get; internal set; }
		public string OwnerName { get; internal set; }
		public xList<SRItem> Inventory { get; internal set; }
		#endregion

		#region (Constructor)
		public SRCoService(uint ID) : base(ID) { }
		public SRCoService(string ServerName) : base(ServerName) { }
		public SRCoService(SRNpc value) : base(value) {

		}
		#endregion

		#region (Methods)
		public bool isHorse()
		{
			return ID4 == 1;
		}
		public bool isTransport()
		{
			return ID4 == 2;
		}
		public bool isAttackPet()
		{
			return ID4 == 3;
		}
		public bool isPickPet()
		{
			return ID4 == 4;
		}
		public bool isGuildGuard()
		{
			return ID4 == 5;
		}
		public int GetHPPercent()
		{
			return (int)Math.Round(HP * 100f / HPMax);
		}
		#endregion

		public enum Action:byte
		{
			Movement = 1,
			ItemPickUp = 8
		}
	}
}
