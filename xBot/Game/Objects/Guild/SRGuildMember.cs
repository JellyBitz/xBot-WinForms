using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Guild
{
	public class SRGuildMember
	{
		public uint ID { get; set; }
		public string Name { get; set; }
		public byte Level { get; set; }
		public uint GPoints { get; set; }
		public string Nickname { get; set; }
		public uint ModelID { get; set; }
		public Permissions PermissionsFlags { get; set; }
		public bool isOffline { get; internal set; }
		public byte unkByte01 { get; internal set; }
		public uint unkUInt01 { get; internal set; }
		public uint unkUInt02 { get; internal set; }
		public uint unkUInt03 { get; internal set; }

		public SRGuildMember()
		{

		}
		[Flags]
		public enum Permissions : uint
		{
			None = 0,
			Join = 1,
			Kick = 2,
			UnionChat = 4,
			Storage = 8,
			Notice = 16,
			All = Join | Kick | UnionChat | Storage | Notice,
			Master = uint.MaxValue
		}
	}
}
