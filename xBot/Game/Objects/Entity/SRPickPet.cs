using System;
namespace xBot.Game.Objects.Entity
{
	public class SRPickPet:SRCoService
	{
		#region (Properties)
		public uint unkUInt01 { get; internal set; }
		public uint unkUInt02 { get; internal set; }
		public Settings SettingsFlags { get; internal set; }
		#endregion

		#region (Constructor)
		public SRPickPet(uint ID) : base(ID) { }
		public SRPickPet(string ServerName) : base(ServerName) { }
		public SRPickPet(SRCoService value) : base(value) { }
		#endregion

		[Flags]
		public enum Settings : uint
		{
			Disabled = 0,
			Gold = 1,
			Equipment = 2,
			OtherItems = 4,
			GrabAllItems = 64,
			Enabled = 128
		}
	}
}
