using System;
using System.Collections.Generic;

namespace xBot.Game.Objects.Party
{
	public class SRParty
	{
		public Purpose PurposeType { get; set; }
		public Setup SetupFlags { get; set; }
		public xDictionary<uint,SRPartyMember> Members { get; set; }
		public SRPartyMember Master {	get { return Members.GetAt(0); }	}
		public byte Capacity { get { return (byte)(SetupFlags.HasFlag(Setup.ExpShared) ? 8 : 4); } }
		public bool isFull { get { return Members.Count >= Capacity; } }
		public SRParty()
		{
			Members = new xDictionary<uint, SRPartyMember>();
    }
		public enum Purpose : byte
		{
			Hunting = 0,
			Quest = 1,
			Trader = 2,
			Thief = 3
		}
		[Flags]
		public enum Setup : byte
		{
			ExpShared = 1,
			ItemShared = 2,
			AnyoneCanInvite = 4
		}
	}
}
