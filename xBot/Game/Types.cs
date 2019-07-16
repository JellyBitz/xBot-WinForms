using System;

namespace xBot.Game
{
	/// <summary>
	/// Class used to wrap all required to sync game actions/types/flags/states/etc ...
	/// </summary>
	public static class Types
	{
		public enum GuildMember
		{
			None = 0,
			Member = 1,
			Master = 2
		}
		public enum AcademyMember
		{
			None = 0
		}
		public enum ExpIcon
		{
			Beginner = 0,
			Helpful = 1,
			BeginnerAndHelpful = 2
		}
		public enum PVPCape
		{
			None = 0,
			Red = 1,
			Gray = 2,
			Blue = 3,
			White = 4,
			Yellow = 5
		}
		public enum PetState
		{
			NeverSummoned = 1,
			Summoned = 2,
			Alive = 3,
			Dead = 4
		}
		public enum MovementAction
		{
			Spinning = 0,
			KeyWalking = 1
		}
		public enum LifeState
		{
			Unborn = 0,
			Alive = 1,
			Dead = 2
		}
		public enum MotionState
		{
			None = 0,
			Walking = 2,
			Running = 3,
			Sitting = 4
		}
		public enum PlayerState
		{
			None = 0,
			Berserk = 1,
			Untouchable = 2,
			GameMasterInvincible = 3,
			GameMasterInvisible = 5,
			Stealth = 6,
			Invisible = 7
		}
		public enum Job
		{
			None = 0,
			Trader = 1,
			Thief = 2,
			Hunter = 3
		}
		public enum PVPState
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
		public enum CaptureTheFlag
		{
			None = 0xFF,
			Red = 1,
			Blue = 2
		}
		public enum Chat
		{
			All = 1,
			Private = 2,
			GM = 3,
			Party = 4,
			Guild = 5,
			Global = 6,
			Notice = 7,
			Stall = 9,
			Union = 11,
			NPC = 13,
			Academy = 16
		}
		public enum EntityStateUpdate {
			HP = 1,
			MP = 2,
			HPMP = 3,
			BadStatus = 4,
			EntityHPMP = 5
		}
		public enum CharacterSelectionAction
		{
			Create = 1,
			List = 2,
			Delete = 3,
			CheckName = 4,
			Restore = 5
		}
		public enum Mob
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
		public static class GuildMemberAuthority
		{
			public const byte
			 None = 0xFF;
		}
		public static class Moonphase
		{
			public const ushort
			 NewMoon = 0;
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
		/// <para>[Flags]</para>
		/// </summary>
		public static class PartySetup
		{
			public const byte
				ExpShared = 1,
				ItemShared = 2,
				AnyoneCanInvite = 4;
		}
		/// <summary>
		/// Party purpose.
		/// </summary>
		public enum PartyPurpose
		{
			Hunting = 0,
			Quest = 1,
			Trade = 2,
			Thief = 3
		}
		public enum PartyUpdate
		{
			Dismissed = 1,
      MemberJoined = 2,
			MemberLeave = 3,
			MemberUpdate = 6
		}
		public enum PlayerInvitation
		{
			PartyCreation = 2,
			PartyInvitation = 3,
			GuildInvitation = 5,
			UnionInvitation = 6,
			AcademyInvitation = 9
		}
		/// <summary>
		/// Check if the current flag is on the data provided.
		/// </summary>
		/// <param name="data">Data to compare</param>
		/// <param name="flag">Some flag taken from<seealso cref="Types"/></param>
		public static bool HasFlag(int data, int flag)
		{
			return (data & flag) == flag;
		}
	}
}
