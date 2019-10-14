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
			Walking,
			Running
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
			Unborn = 0,
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
		public enum PlayerMode : byte
		{
			None = 0,
			OnExchangeProbably = 2,
			OnStall = 4,
			OnShop = 6
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
		/// <summary>
		/// All possible bad status types.
		/// <para>Flags</para>
		/// </summary>
		[Flags]
		public enum BadStatus : uint
		{
			None = 0,
			Freezing = 0x1, // Universal
			Frostbite = 0x2, // None
			ElectricShock = 0x4, // Universal
			Burn = 0x8, // Universal
			Poisoning = 0x10, // Universal
			Zombie = 0x20, // Universal
			Sleep = 0x40, // None
			Bind = 0x80, // None
			Dull = 0x100, // Purification
			Fear = 0x200, // Purification
			ShortSight = 0x400, // Purification
			Bleed = 0x800, // Purification
			Petrify = 0x1000, // None
			Darkness = 0x2000, // Purification
			Stun = 0x4000, // None
			Disease = 0x8000, // Purification
			Confusion = 0x10000, // Purification
			Decay = 0x20000, // Purification
			Weaken = 0x40000, // Purification
			Impotent = 0x80000, // Purification
			Division = 0x100000, // Purification
			Panic = 0x200000, // Purification
			Combustion = 0x400000, // Purification
			Unk01 = 0x800000,
			Hidden = 0x1000000, // Purification
			Unk02 = 0x2000000,
			Unk03 = 0x4000000,
			Unk04 = 0x8000000,
			Unk05 = 0x10000000,
			Unk06 = 0x20000000,
			Unk07 = 0x40000000,
			Unk08 = 0x80000000
		}
		/// <summary>
		/// Players interacting petitions.
		/// </summary>
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
		/// Character inventory item movement.
		/// </summary>
		public enum InventoryItemMovement : byte
		{
			InventoryToInventory = 0,
			StorageToStorage = 1,
			InventoryToStorage = 2,
			StorageToInventory = 3,

			GroundToInventory = 6,
			InventoryToGround = 7,

			ShopToInventory = 8,
			InventoryToShop = 9,

			PetToPet = 10,
			PetToInventory = 11,
			InventoryToPet = 12,

			QuestToInventory = 14,
			InventoryToQuest = 15,

			TransportToTransport = 16,
			GroundToTransport = 17,
			ShopToTransport = 19,
			TransportToShop = 20,

			ShopBuyBack = 34
		}
		/// <summary>
		/// All possible pet pick settings (handled by the client).
		/// <para>Flags</para>
		/// </summary>
		[Flags]
		public enum PetPickSettings : uint
		{
			Disabled = 0,
			Gold = 1,
			Equipment = 2,
			OtherItems = 4,
			GrabAllItems = 64,
			Enabled = 128
		}
		/// <summary>
		/// All possible pet attack settings (handled by the client).
		/// <para>Flags</para>
		/// </summary>
		[Flags]
		public enum PetAttackSettings : uint
		{
			Disabled = 0,
			Offensive = 1
		}
		/// <summary>
		/// Game consola commands.
		/// </summary>
		public enum GMConsoleAction : byte
		{
			FindUser = 1,
			GoTown = 2,
			ToTown = 3,
			WorldStatus = 4,
			LoadMonster = 6,
			MakeItem = 7,
			MoveToUser = 8,
			WP = 10,
			Zoe = 12,
			Ban = 13,
			Invisible = 14,
			Invincible = 15,
			RecallUser = 17,
			RecallGuild = 18,
			LieName = 19,
			MobKill = 20,
			ResetQ = 28,
			MoveToNPC = 31,
			MakeRentItem = 38,
			SpawnUniqueLocation = 42
		}

		public enum CharacterAction : byte
		{
			BasicAttack = 1,
			ItemPickUp = 2,
      SkillCast = 4,
			SkillRemove = 5
		}
		/// <summary>
		/// All weapons types handled.
		/// </summary>
		public enum Weapon : byte
		{
			None = byte.MaxValue,
			Bow = 6,
			OneHandSword = 7,
			TwoHandSword = 8,
			DualAxes = 9,
			TwoHandStaff = 11,
			Crossbow = 12,
			Daggers = 13
		}
	}
}