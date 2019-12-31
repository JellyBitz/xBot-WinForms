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
			Unsummoned = 3,
			Dead = 4
		}
		public enum MovementSpeed : byte
		{
			Walking = 0,
			Running = 1
		}
		public enum MovementAction : byte
		{
			Spinning = 0,
			KeyWalking = 1
		}
		public enum LifeState : byte
		{
			/// <summary>
			/// The character is not connected to the game.
			/// </summary>
			None = 0,
			Alive = 1,
			Dead = 2
		}
		public enum MotionState : byte
		{
			StandUp = 0,
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
			Neutral = 0,
			Assaulter = 1,
			PlayerKiller = 2
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
		/// All possible bad status types.
		/// <para>Flags</para>
		/// </summary>
		

		
		/// <summary>
		/// All possible pet pick settings (handled by the client).
		/// <para>Flags</para>
		/// </summary>
		
		/// <summary>
		/// All possible pet attack settings (handled by the client).
		/// <para>Flags</para>
		/// </summary>
		
		/// <summary>
		/// Game consola commands.
		/// </summary>
		public enum GMCommandAction : ushort
		{
			FindUser = 1,
			GoTown = 2,
			ToTown = 3,
			WorldStatus = 4,
			CreateMob = 6,
			MakeItem = 7,
			MoveToUser = 8,
			Warp = 10,
			Zoe = 12,
			Ban = 13,
			Invisible = 14,
			Invincible = 15,
			RecallUser = 17,
			RecallGuild = 18,
			LieName = 19,
			KillMob = 20,
			ResetQ = 28,
			MoveToNPC = 31,
			MakeRentItem = 38,
			SpawnUniqueLocation = 42
		}
		/// <summary>
		/// States found on damage skill.
		/// <para>Flags</para>
		/// </summary>

		/// <summary>
		/// States found on damage skill.
		/// <para>Flags</para>
		/// </summary>

		/// <summary>
		/// All weapons types handled.
		/// </summary>
	
		public enum StallUpdate
		{
			ItemUpdate = 1,
			ItemAdded = 2,
			ItemRemoved = 3,
			FleaMarketMode = 4, // ?
			State = 5,
			Note = 6,
			Title = 7
		}
	}
}