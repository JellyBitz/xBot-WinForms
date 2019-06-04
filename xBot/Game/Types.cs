namespace xBot.Game
{
	// Class used to wrap all required to sync game action/types/states
	public static class Types
	{
		public static class GuildMember
		{
			public const byte
				None = 0,
				Member = 1,
				Master = 2;
		}
		public static class AcademyMember
		{
			public const byte
				 None = 0;
		}
		public static class Exp
		{
			public const byte
				Beginner = 0,
				Helpful = 1,
				BeginnerAndHelpful = 2;
		}
		public static class PVPCape
		{
			public const byte
				None = 0,
				Red = 1,
				Gray = 2,
				Blue = 3,
				White = 4,
				Yellow = 5;
		}
		public static class PetState
		{
			public const byte
				NotSummoned = 1,
				Summoned = 2,
				Alive = 3,
				Dead = 4;
		}
		public static class MovementSource
		{
			public const byte
				Spinning = 0,
				Sky = 1,
				KeyWalking = 1;
		}
		public static class LifeState
		{
			public const byte
				Alive = 1,
				Dead = 2;
		}
		public static class MotionState
		{
			public const byte
				None = 0,
				Walking = 2,
				Running = 3,
				Sitting = 4;
		}
		public static class PlayerState
		{
			public const byte
				None = 0,
				Berserk = 1,
				Untouchable = 2,
				GameMasterInvincible = 3,
				GameMasterInvisible = 5,
				Stealth = 6,
				Invisible = 7;
		}
		public static class Job
		{
			public const byte
				None = 0,
				Trader = 1,
				Thief = 2,
				Hunter = 3;
		}
		public static class PVPState
		{
			/// <summary>
			/// Also known as Neutral.
			/// </summary>
			public const byte	White = 0;
			/// <summary>
			/// Also known as Assaulter.
			/// </summary>
			public const byte Purple = 1;
			/// <summary>
			/// PK or temporaly PK.
			/// </summary>
			public const byte Red = 2;
		}
		public static class CaptureTheFlag
		{
			public const byte
				None = 0xFF,
				Red = 1,
				Blue = 2;
		}
		public static class Chat
		{
			public const byte
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
				Academy = 16;
		}
		
		public static class CharacterSelectionAction
		{
			public const byte
				Create = 1,
				List = 2,
				Delete = 3,
				CheckName = 4,
				Restore = 5;
		}
		public static class Mob
		{
			public const byte
				General = 0,
				Champion = 1,
				Giant = 4,
				Titan = 5,
				Strong = 6,
				Elite = 7,
				Unique = 8,
				Party = 0x10,
				PartyChampion = 0x11,
				PartyGiant = 0x14;
		}
		
		public static class ScrollMode
		{
			public const byte
				None = 0,
				ReturnScroll = 1,
				BanditReturnScroll = 2;
		}
		public static class InteractMode
		{
			public const byte
				None = 0,
				P2P = 2,
				P2N_TALK = 4,
				OPNMKT_DEAL = 6;
		}

    public static class GuildMemberAuthority
		{
			public const byte
			 None = 0xFF;
		}
  }
}
