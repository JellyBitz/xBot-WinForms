using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Common
{
	public static class SRTypes
	{
		public enum CharacterSelectionAction : byte
		{
			Create = 1,
			List = 2,
			Delete = 3,
			CheckName = 4,
			Restore = 5
		}
		/// <summary>
		/// All basic actions that character can do.
		/// </summary>
		public enum CharacterAction : byte
		{
			CommonAttack = 1,
			ItemPickUp = 2,
			SkillCast = 4,
			SkillRemove = 5
		}
		public enum Weapon : byte
		{
			None = byte.MaxValue,
			Sword = 2,
			Blade = 3,
			Spear = 4,
			Glaive = 5,
			Bow = 6,
			OneHandSword = 7,
			TwoHandSword = 8,
			DualAxes = 9,
			Warlock = 10,
			TwoHandStaff = 11,
			Crossbow = 12,
			Daggers = 13,
			Harp = 14,
			Cleric = 15
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
			Union = 11,
			NPC = 13,
			Academy = 16
		}
		public enum EntityStateUpdate : byte
		{
			HP = 1,
			MP = 2,
			HPMP = 3,
			BadStatus = 4,
			EntityHPMP = 5
		}
		/// <summary>
		/// Players interacting petitions.
		/// </summary>
		public enum PlayerPetition : byte
		{
			ExchangeRequest = 1,
			PartyCreation = 2,
			PartyInvitation = 3,
			Resurrection = 4,
			GuildInvitation = 5,
			UnionInvitation = 6, // Not confirmed
			AcademyInvitation = 9 // Not confirmed
		}
		public enum Equipable : byte
		{
			Garment = 1,
			Protector = 2,
			Armor = 3,
			Shield = 4,
			AccesoriesCH = 5,
			Weapon = 6,
			Job = 7,
			Robe = 9,
			LightArmor = 10,
			HeavyArmor = 11,
			AccesoriesEU = 12,
			Avatar = 13,
			DevilSpirit = 14
		}
		public enum SetPart : byte
		{
			Head = 1,
			Shoulders = 2,
			Chest = 3,
			Pants = 4,
			Gloves = 5,
			Boots = 6
		}
		public enum AvatarPart : byte
		{
			Hat = 1,
			Dress = 2,
			Accessory = 3,
			Flag = 4
		}
		public enum AccesoriesPart : byte
		{
			Earring = 1,
			Necklace = 2,
			Ring = 3
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

			InventoryToExchange = 4,
			ExchangeToInventory = 5,

			GroundToInventory = 6,
			InventoryToGround = 7,

			ShopToInventory = 8,
			InventoryToShop = 9,

			InventoryGoldToGround = 10,
			StorageGoldToInventory = 11,
			InventoryGoldToStorage = 12,
			InventoryGoldToExchange = 13,

			QuestToInventory = 14,
			InventoryToQuest = 15,

			TransportToTransport = 16,
			GroundToPet = 17,
			ShopToTransport = 19,
			TransportToShop = 20,

			PetToPet = 25,
			PetToInventory = 26,
			InventoryToPet = 27,
			GroundToPetToInventory = 28,

			GuildToGuild = 29,
			InventoryToGuild = 30,
			GuildToInventory = 31,
			InventoryGoldToGuild = 32,
			GuildGoldToInventory = 33,

			ShopBuyBack = 34,

			AvatarToInventory = 35,
			InventoryToAvatar = 36
		}
		public enum StallUpdate:byte
		{
			ItemUpdate = 1,
			ItemAdded = 2,
			ItemRemoved = 3,
			FleaMarketMode = 4, // ???
			State = 5,
			Note = 6,
			Title = 7
		}
		public enum SkillCast : byte
		{
			Buff = 0,
			Attack = 2
		}
		[Flags]
		public enum DamageEffect : byte
		{
			None = 0,
			KnockBack = 1,
			Block = 2,
			Position = 4,
			Cancel = 8
		}
		[Flags]
		public enum Damage : byte
		{
			None = 0,
			Normal = 1,
			Critical = 2,
			Status = 4
		}

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

		#region (Extensions)
		public static bool HasFlags(this ulong flags, ulong desiredFlags)
		{
			return (flags & desiredFlags) != 0;
		}
		public static bool HasFlags(this uint flags, uint desiredFlags)
		{
			return (flags & desiredFlags) != 0;
		}
		public static bool HasFlags(this ushort flags, ushort desiredFlags)
		{
			return (flags & desiredFlags) != 0;
		}
		public static bool HasFlags(this byte flags, byte desiredFlags)
		{
			return (flags & desiredFlags) != 0;
		}
		#endregion
	}
}
