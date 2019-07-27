using System;

namespace xBot.Game
{
	/// <summary>
	/// Class used to wrap all required to sync game actions/types/flags/states/etc ...
	/// </summary>
	public static class Types
	{
		public enum GuildMember : byte
		{
			None = 0,
			Member = 1,
			Master = 2
		}
		public enum AcademyMember : byte
		{
			None = 0
		}
		public enum ExpIcon : byte
		{
			Beginner = 0,
			Helpful = 1,
			BeginnerAndHelpful = 2
		}
		public enum PVPCape : byte
		{
			None = 0,
			Red = 1,
			Gray = 2,
			Blue = 3,
			White = 4,
			Yellow = 5
		}
		public enum PetState : byte
		{
			NeverSummoned = 1,
			Summoned = 2,
			Alive = 3,
			Dead = 4
		}
		public enum MovementAction : byte
		{
			Spinning = 0,
			KeyWalking = 1
		}
		public enum LifeState : byte
		{
			/// <summary>
			/// The character never has been connected.
			/// </summary>
			Unborn = 0,
			Alive = 1,
			Dead = 2
		}
		public enum MotionState : byte
		{
			None = 0,
			Walking = 2,
			Running = 3,
			Sitting = 4
		}
		public enum PlayerState : byte
		{
			None = 0,
			Berserk = 1,
			Untouchable = 2,
			GameMasterInvincible = 3,
			GameMasterInvisible = 5,
			Stealth = 6,
			Invisible = 7
		}
		public enum Job : byte
		{
			None = 0,
			Trader = 1,
			Thief = 2,
			Hunter = 3
		}
		public enum PVPState : byte
		{
			/// <summary>
			/// Also known as Neutral.
			/// </summary>
			White = 0,
			/// <summary>
			/// Also known as Assaulter.
			/// </summary>
			Purple = 1,
			/// <summary>
			/// PK or temporaly PK.
			/// </summary>
			Red = 2
		}
		public enum CaptureTheFlag : byte
		{
			None = 0xFF,
			Red = 1,
			Blue = 2
		}
		public enum Chat : byte
		{
			All = 1,
			Private = 2,
			GM = 3,
			Party = 4,
			Guild = 5,
			Global = 6,
			Notice = 7,
			Stall = 9,
			Union = 0xB,
			NPC = 0xD,
			Academy = 0x10
		}
		public enum EntityStateUpdate : byte
		{
			HP = 1,
			MP = 2,
			HPMP = 3,
			BadStatus = 4,
			EntityHPMP = 5
		}
		public enum CharacterSelectionAction : byte
		{
			Create = 1,
			List = 2,
			Delete = 3,
			CheckName = 4,
			Restore = 5
		}
		public enum Mob : byte
		{
			General = 0,
			Champion = 1,
			Giant = 4,
			Titan = 5,
			Strong = 6,
			Elite = 7,
			Unique = 8,
			Party = 0x10,
			PartyChampion = 0x11,
			PartyGiant = 0x14
		}
		public enum ScrollMode
		{
			None = 0,
			ReturnScroll = 1,
			BanditReturnScroll = 2
		}
		public enum InteractMode
		{
			None = 0,
			P2P = 2,
			P2N_TALK = 4,
			OPNMKT_DEAL = 6
		}
		public enum GuildMemberAuthority
		{
			None = 0xFF
		}
		/// <summary>
		/// Wheater displayed graphicaly.
		/// </summary>
		public enum Wheater
		{
			Clear = 1,
			Rainy = 2,
			Snow = 3
		}
		/// <summary>
		/// Party basic setup.
		/// <para>Flags</para>
		/// </summary>
		[Flags]
		public enum PartySetup : byte
		{
			ExpShared = 1,
			ItemShared = 2,
			AnyoneCanInvite = 4
		}
		/// <summary>
		/// Party purpose.
		/// </summary>
		public enum PartyPurpose : byte
		{
			Hunting = 0,
			Quest = 1,
			Trader = 2,
			Thief = 3
		}
		public enum PlayerPetition : byte
		{
			PartyCreation = 2,
			PartyInvitation = 3,
			Resurrection = 4,
			GuildInvitation = 5, // Not confirmed
			UnionInvitation = 6, // Not confirmed
			AcademyInvitation = 9, // Not confirmed
		}
		/// <summary>
		/// All possible bad status types.
		/// <para>Flags</para>
		/// </summary>
		[Flags]
		public enum BadStatus : uint
		{
			None = 0,
			Freezing = 1, // universal
			Frostbite = 2, // universal
			ElectricShock = 4, // universal
			Burn = 8, // universal
			Poisoning = 16, // universal
			Unk02 = 32,
			Sleep = 64, // nope
			Bind = 128, // ??
			Dull = 256, // ??
			Fear = 512, // purification
			Unk07 = 1024,
			Bleed = 2048, // universal
			Unk09 = 4096,
			Unk10 = 8192,
			Stun = 16384, // nope
			Disease = 32768, // ??
			Unk13 = 65536,
			Decay = 131072, // ??
			Weaken = 262144, // ??
			Impotent = 524288, // ??
			Division = 1048576, // ??
			Unk18 = 2097152,
			Combustion = 4194304, // ??
			Unk20 = 8388608,
			Unk21 = 16777216,
			Unk22 = 33554432,
			Unk23 = 67108864,
			Unk24 = 134217728,
			Unk25 = 268435456,
			Unk26 = 536870912,
			Unk27 = 1073741824,
			Unk28 = 2147483648
		}
	}
}