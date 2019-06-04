using System;
using System.Collections.Generic;
using System.Drawing;

namespace xBot.Game
{
	/// <summary>
	/// Enum to Handle all propierties from objects that can be found at the game
	/// </summary>
	public enum SRAttribute
	{
		/// <summary>
		/// Name of the entity.
		/// <para>Data type : <see cref="string"/></para>
		/// <para>Used on : Characters, <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.Objectives"/></para>
		/// </summary>
		Name,
		/// <summary>
		/// Scale of the entity, using four bits for Volume/Height respectly.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		Scale,
		/// <summary>
		/// Level of the entity.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Characters, Masteries</para>
		/// </summary>
		Level,
		/// <summary>
		/// Maximum level reached.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		LevelMax,
		/// <summary>
		/// Experience.
		/// <para>Data type : <see cref="ulong"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		Exp,
		/// <summary>
		/// Maximum experience, used to calculate the current experience percent.
		/// <para>Data type : <see cref="ulong"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		ExpMax,
		/// <summary>
		/// Skill points.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		SP,
		/// <summary>
		/// Skill points experience.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		SPExp,
		/// <summary>
		/// Current strength points.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		STR,
		/// <summary>
		/// Current intelligence points.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		INT,
		/// <summary>
		/// Stat points remaining.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		StatPoints,
		/// <summary>
		/// Current health points.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		HP,
		/// <summary>
		/// Current mana points.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		MP,
		/// <summary>
		/// Check if an character is being deleted.
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		isDeleting,
		/// <summary>
		/// Time in minutes for the character being deleted totally.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		DeletingTime,
		/// <summary>
		/// Position as Guild member.
		/// <para>Data type : <see cref="Types.GuildMember"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		GuildMemberType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		isGuildRenameRequired,
		/// <summary>
		/// Position as Academy member.
		/// <para>Data type : <see cref="Types.AcademyMember"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		AcademyMemberType,
		/// <summary>
		/// Store all info about items (at <see cref="SRObject"/> instances) displayed and stored.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		Inventory,
		/// <summary>
		/// Store all info about avatar items (at <see cref="SRObject"/> instances) displayed.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		InventoryAvatar,
		/// <summary>
		/// Item Plus.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,,<see cref="SRAttribute.InventoryAvatar"/>, <see cref="SRAttribute.AdvanceElixirParams"/></para>
		/// </summary>
		Plus,
		/// <summary>
		/// Gold quantity.
		/// <para>Data type : <see cref="ulong"/></para>
		/// <para>Used on : Characters</para>
		/// </summary>
		Gold,
		/// <summary>
		/// Berserk points. 5 Points means Berserk ready.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		BerserkPoints,
		/// <summary>
		/// Berserk level, duration depends on it.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		BerserkLevel,
		/// <summary>
		/// Maximum health points.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		HPMax,
		/// <summary>
		/// Maximum mana points.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		MPMax,
		/// <summary>
		/// Experience Icon.
		/// <para>Data type : <see cref="Types.Exp"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		ExpType,
		/// <summary>
		/// Daily kills on PK mode.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PKDaily,
		/// <summary>
		/// Total kill points on PK mode.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PKTotal,
		/// <summary>
		/// Penalty kill points to get out from PK mode.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PKPenalty,
		/// <summary>
		/// PVP Cape mode.
		/// <para>Data type : <see cref="Types.PVPCape"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PVPCapeType,
		/// <summary>
		/// Rent type, like items premium, event and more.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentCanDelete,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentCanRecharge,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentPeriodBeginTime,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentPeriodEndTime,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentPackingTime,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		RentMeterRateTime,
		/// <summary>
		/// Display variance.
		/// <para>Data type : <see cref="ulong"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		Variance,
		/// <summary>
		/// Durability points till item break.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		Durability,
		/// <summary>
		/// Magic propierties.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
		/// </summary>
		MagicParams,
		/// <summary>
		/// Type.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.MagicParams"/></para>
		/// </summary>
		Type,
		/// <summary>
		/// Value.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.MagicParams"/>,
		/// <see cref="SRAttribute.SocketParams"/></para>
		/// </summary>
		Value,
		/// <summary>
		/// Socket propierties.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		SocketParams,
		/// <summary>
		/// Slot number
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.SocketParams"/>,
		/// <see cref="SRAttribute.AdvanceElixirParams"/></para>
		/// </summary>
		Slot,
		/// <summary>
		/// Identifier.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.SocketParams"/>, <see cref="SRAttribute.AdvanceElixirParams"/></para>
		/// </summary>
		ID,
		/// <summary>
		/// Advance elixir propierties.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/>,<see cref="SRAttribute.InventoryAvatar"/></para>
		/// </summary>
		AdvanceElixirParams,
		/// <summary>
		/// Pet state.
		/// <para>Data type : <see cref="Types.PetState"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
		/// </summary>
		PetState,
		/// <summary>
		/// Display model reference.
		/// <para>Data type : <see cref="SRObject"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
		/// </summary>
		Model,
		/// <summary>
		/// Do not confuse with <see cref="SRAttribute.Quantity"/> (stack count). This indicates the amount of objects inside.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
		/// </summary>
		Amount,
		/// <summary>
		/// Quantity of objects joined at the same item (Stacks).
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
		/// </summary>
		Quantity,
		/// <summary>
		/// Stone assimilation probability at percentage.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
		/// </summary>
		AssimilationProbability,
		/// <summary>
		/// Masteries tabs from character.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Masteries,
		/// <summary>
		/// List of skill objects
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Skills,
		/// <summary>
		/// Enabled
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.Skills"/></para>
		/// </summary>
		Enabled,
		/// <summary>
		/// ID's Array from quest completed 
		/// <para>Data type : <see cref="byte"/>[]</para>
		/// <para>Used on : Character</para>
		/// </summary>
		QuestsCompletedID,
		/// <summary>
		/// List of all quest activated
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Quests,
		/// <summary>
		/// Quantity of achievements on the quest.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/></para>
		/// </summary>
		Achievements,
		/// <summary>
		/// Check if party mode auto-share is required.
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/></para>
		/// </summary>
		isAutoShareRequired,
		/// <summary>
		/// Type of quest.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/></para>
		/// </summary>
		QuestType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/></para>
		/// </summary>
		TimeRemain,
		/// <summary>
		/// Check if the objective is on.
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/>,<see cref="SRAttribute.Objectives"/></para>
		/// </summary>
		isActive,
		/// <summary>
		/// Quest objectives.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/></para>
		/// </summary>
		Objectives,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : <see cref="SRAttribute.Objectives"/></para>
		/// </summary>
		ObjectiveID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/>[]</para>
		/// <para>Used on : <see cref="SRAttribute.Objectives"/></para>
		/// </summary>
		TaskValues,
		/// <summary>
		/// NPC ID's.
		/// <para>Data type : <see cref="uint"/>[]</para>
		/// <para>Used on : <see cref="SRAttribute.Quests"/></para>
		/// </summary>
		NPCIDs,
		/// <summary>
		/// Collection books from differents Forgotten Worlds.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		CollectionBooks,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.CollectionBooks"/></para>
		/// </summary>
		Number,
		/// <summary>
		/// Book date started (SROTimestamp).
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.CollectionBooks"/></para>
		/// </summary>
		StartedDatetime,
		/// <summary>
		/// Book pages already done. 
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.CollectionBooks"/></para>
		/// </summary>
		Pages,
		/// <summary>
		/// Unique ID to keep the track of all objects in game.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		UniqueID,
		/// <summary>
		/// Reference to the region of the map. Contains bytes for sector X and Y from map.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Region,
		/// <summary>
		/// Position X of the map.
		/// <para>Data type : <see cref="float"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		X,
		/// <summary>
		/// Position Y of the map.
		/// <para>Data type : <see cref="float"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Y,
		/// <summary>
		/// Position Z of the map.
		/// <para>Data type : <see cref="float"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Z,
		/// <summary>
		/// Arrow direction from the character at the map.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Angle,
		/// <summary>
		/// Check if the entity is moving to some point.
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		hasDestination,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		MovementType,
		/// <summary>
		/// Destination region of the object.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		DestinationRegion,
		/// <summary>
		/// Destination offset position X of the map.
		/// <para>Data type : <see cref="uint"/> (if is on dungeons) or <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		DestinationOffsetX,
		/// <summary>
		/// Destination offset position Y of the map.
		/// <para>Data type : <see cref="uint"/> (if is on dungeons) or <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		DestinationOffsetY,
		/// <summary>
		/// Destination offset position Z of the map.
		/// <para>Data type : <see cref="uint"/> (if is on dungeons) or <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		DestinationOffsetZ,
		/// <summary>
		/// Cause of the movement.
		/// <para>Data type : <see cref="Types.MovementSource"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		MovementSource,
		/// <summary>
		/// New arrow direction from the character at the map after the movement.
		/// <para>Data type : <see cref="ushort"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		MovementAngle,
		/// <summary>
		/// Dead or alive entity state.
		/// <para>Data type : <see cref="Types.LifeState"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		LifeState,
		/// <summary>
		/// Graphic animation.
		/// <para>Data type : <see cref="Types.MotionState"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		MotionState,
		/// <summary>
		/// Graphic animation.
		/// <para>Data type : <see cref="Types.PlayerState"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PlayerStatus,
		/// <summary>
		/// Walking speed at percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		SpeedWalking,
		/// <summary>
		/// Running speed at percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		SpeedRunning,
		/// <summary>
		/// Speed using berserk mode, at percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		SpeedBerserk,
		/// <summary>
		/// Active buffs.
		/// <para>Data type : <see cref="SRList"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		Buffs,
		/// <summary>
		/// Time left.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : <see cref="SRAttribute.Buffs"/></para>
		/// </summary>
		Duration,
		/// <summary>
		/// Check if is creator of the current object reference.
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : <see cref="SRAttribute.Buffs"/></para>
		/// </summary>
		isCreator,
		/// <summary>
		/// Mob type. 
		/// <para>Data type : <see cref="Types.Mob"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		MobType,
		/// <summary>
		/// Name used on job mode.
		/// <para>Data type : <see cref="string"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JobName,
		/// <summary>
		/// Job type class.
		/// <para>Data type : <see cref="Types.Job"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JobType,
		/// <summary>
		/// Level on job mode.
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JobLevel,
		/// <summary>
		/// Current experience on job mode.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JobExp,
		/// <summary>
		/// Job points gained.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JobContribution,
		/// <summary>
		/// Job reward.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JobReward,
		/// <summary>
		/// PVP combat state.
		/// <para>Data type : <see cref="Types.PVPState"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PVPState,
		/// <summary>
		/// Check if has transport activated
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		hasTransport,
		/// <summary>
		/// Check if is on combat mode.
		/// <para>Data type : <see cref="bool"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		isFighting,
		/// <summary>
		/// Transport Unique ID from Job.
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		TransportUniqueID,
		/// <summary>
		/// Info about CTF Event.
		/// <para>Data type : <see cref="Types.CaptureTheFlag"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		PVPCaptureTheFlagType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="ulong"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		GuideFlag,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		JID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		GMFlag,




		/// <summary>
		/// Unknown byte from Item.	
		/// </summary>
		unkByte01,
		/// <summary>
		/// Unknown byte from Character (Not a counter)
		/// </summary>
		unkByte02,
		/// <summary>
		/// Unknown byte from Character (Not a counter)
		/// </summary>
		unkByte03,
		/// <summary>
		/// Unknown byte from Character (Structure changes?)
		/// </summary>
		unkByte04,
		/// <summary>
		/// Unknown byte from Character (Possibly some status)
		/// </summary>
		unkByte05,


















	


		/// <summary>
		/// <para>Data type : <see cref="xBot.Game.SRList"/></para>
		/// </summary>
		MaskItems,
		PVPCape,
		ItemOptLevel,
		Mask,
		hasMask,
		refEventStructID,
		unkByte3,
		WalkSpeed,
		RunSpeed,
		BerserkSpeed,
		GuildName,
		ScrollMode,
		/// <summary>
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		InteractMode,
		unkByte4,
		/// <summary>
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		GuildID,
		GuildMemberName,
		GuildLastCrestRev,
		UnionID,
		GuildisFriendly,
		UnionLastCrestRev,
		GuildMemberAuthorityType,
		StallName,
		DecorationItemID,
		EquipmentCooldown,
		DropSource,
		unkByte6,
		hasTalk,
		TalkOptions,
		Rarity,
		Appearance,
		OwnerName,
		OwnerRefObjID,
		OwnerUniqueID,
		hasOwner,
		OwnerJID,
		unkUShort0,
		refSkillID,
		unkByte1,
		unkByte2,
		unkByte0,
		unkUInt0,
		unkUInt1,
		unkUInt2,
		unkByte5,
	}
	/// <summary>
	/// <para> Funny and customizable class to handle all game objects
	/// in a dynamic way without falling too much at performance.</para>
	/// </summary>
	public class SRObject
	{
		public enum Type
		{
			Model,
			Item,
			Mastery,
			Skill,
			Quest
		}
		private Dictionary<string, object> _attributes;
		private uint _ID;
		private byte _ID1, _ID2, _ID3, _ID4;
		private Type _type;
		private string[] _data;
		public uint ID { get { return _ID; } }
		public Type type { get { return _type; } }
		public byte ID1 { get { return _ID1; } }
		public byte ID2 { get { return _ID2; } }
		public byte ID3 { get { return _ID3; } }
		public byte ID4 { get { return _ID4; } }
		public string[] data { get { return _data; } }
		/// <summary>
		/// Undefined game object, that has no identifiers or are not known yet.
		/// Also can be used as property from another object.
		/// </summary>
		public SRObject()
		{
			_attributes = new Dictionary<string, object>();
			_data = null;
			_ID = _ID1 = _ID2 = _ID3 = _ID4 = 0;
		}
		/// <summary>
		/// Creates a game object from the ID specified.
		/// </summary>
		/// <param name="ID">Object identifier</param>
		/// <param name="type">Object type</param>
		public SRObject(uint ID, Type type)
		{
			_attributes = new Dictionary<string, object>();
			Load(ID, type);
		}
		/// <summary>
		/// Creates a game object from the name specified.
		/// </summary>
		/// <param name="Name">Object name</param>
		/// <param name="type">Object type</param>
		public SRObject(string Name, Type type)
		{
			_attributes = new Dictionary<string, object>();
			Load(Name, type);
		}
		/// <summary>
		/// Load the object keeping all attributes previously saved
		/// </summary>
		/// <param name="ID">Object identifier</param>
		/// <param name="type">Object type</param>
		public void Load(uint ID, Type type)
		{
			_ID = ID;
			_type = type;
			switch (type)
			{
				case Type.Model:
					_data = Data.Get.getDataFromFiles(ID.ToString(), 1, "Data\\Media\\server_dep\\silkroad\\textdata", "characterdata.txt");
					if (_data == null)
					{
						// Model not found. Then check teleport models
						_data = Data.Get.getDataFromFile(ID.ToString(), 1, "Data\\Media\\server_dep\\silkroad\\textdata", "teleportbuilding.txt");
					}
					_ID1 = byte.Parse(data[9]);
					_ID2 = byte.Parse(data[10]);
					_ID3 = byte.Parse(data[11]);
					_ID4 = byte.Parse(data[12]);
					break;
				case Type.Item:
					_data = Data.Get.getDataFromFiles(ID.ToString(), 1, "Data\\Media\\server_dep\\silkroad\\textdata", "itemdata.txt");
					_ID1 = byte.Parse(data[9]);
					_ID2 = byte.Parse(data[10]);
					_ID3 = byte.Parse(data[11]);
					_ID4 = byte.Parse(data[12]);
					break;
				case Type.Mastery:
					_data = Data.Get.getDataFromFile(ID.ToString(), 0, "Data\\Media\\server_dep\\silkroad\\textdata", "skillmasterydata.txt");
					break;
				case Type.Skill:
					_data = Data.Get.getDataFromFiles(ID.ToString(), 1, "Data\\Media\\server_dep\\silkroad\\textdata", "skilldata.txt");
					break;
				case Type.Quest:
					_data = Data.Get.getDataFromFile(ID.ToString(), 1, "Data\\Media\\server_dep\\silkroad\\textdata", "questdata.txt");
					break;
			}
		}
		/// <summary>
		/// Load the object keeping all attributes previously saved
		/// </summary>
		/// <param name="Name">Name of the object, for example : CHAR_CH_MAN_ADVENTURER</param>
		/// <param name="type">type</param>
		public void Load(string Name, Type type)
		{
			_type = type;
			switch (type)
			{
				case Type.Model:
					_data = Data.Get.getDataFromFiles(Name, 2, "Data\\Media\\server_dep\\silkroad\\textdata", "characterdata.txt");
					if(_data == null){
						// Model not found. Then check teleport models
						_data = Data.Get.getDataFromFile(Name, 2, "Data\\Media\\server_dep\\silkroad\\textdata", "teleportbuilding.txt");
					}
					_ID = uint.Parse(_data[1]);
					_ID1 = byte.Parse(data[9]);
					_ID2 = byte.Parse(data[10]);
					_ID3 = byte.Parse(data[11]);
					_ID4 = byte.Parse(data[12]);
					break;
				case Type.Item:
					_data = Data.Get.getDataFromFiles(Name, 2, "Data\\Media\\server_dep\\silkroad\\textdata", "itemdata.txt");
					_ID = uint.Parse(_data[1]);
					_ID1 = byte.Parse(data[9]);
					_ID2 = byte.Parse(data[10]);
					_ID3 = byte.Parse(data[11]);
					_ID4 = byte.Parse(data[12]);
					break;
				case Type.Mastery:
					_data = Data.Get.getDataFromFile(Name, 2, "Data\\Media\\server_dep\\silkroad\\textdata", "skillmasterydata.txt");
					_ID = uint.Parse(_data[0]);
					break;
				case Type.Skill:
					_data = Data.Get.getDataFromFiles(Name, 2, "Data\\Media\\server_dep\\silkroad\\textdata", "skilldata.txt");
					_ID = uint.Parse(_data[1]);
					break;
				case Type.Quest:
					_data = Data.Get.getDataFromFile(Name, 2, "Data\\Media\\server_dep\\silkroad\\textdata", "questdata.txt");
					_ID = uint.Parse(_data[1]);
					break;
			}
		}
		/// <summary>
		/// Get or set the attributes from game object.
		/// </summary>
		/// <param name="Attribute">Name of the attribute</param>
		/// <returns>Null if attributte is not found</returns>
		public object this[SRAttribute Attribute]
		{
			get
			{
				if (_attributes.ContainsKey(Attribute.ToString()))
					return _attributes[Attribute.ToString()];
				return null;
			}
			set
			{
				_attributes[Attribute.ToString()] = value;
			}
		}
		/// <summary>
		/// Check if the object has the attribute specified.
		/// </summary>
		public bool Contains(SRAttribute Attribute)
		{
			return _attributes.ContainsKey(Attribute.ToString());
		}
		/// <summary>
		/// Copy all the attributes from another game object.
		/// </summary>
		/// <param name="obj">Game object copied</param>
		/// <param name="replace">Specify if the current attribute will be replaced</param>
		public void CopyAttributes(SRObject obj, bool replace = false)
		{
			foreach (string key in obj._attributes.Keys)
			{
				if (!this._attributes.ContainsKey(key) || replace)
				{
					this._attributes[key] = obj._attributes[key];
				}
			}
		}
		/// <summary>
		///  Clone all objects attributes keeping a new object reference.
		/// </summary>
		/// <returns></returns>
		public SRObject Clone()
		{
			return (SRObject)MemberwiseClone();
		}
		public List<string> ToArray()
		{
			List<string> array = new List<string>();
			array.Add("ID:" + ID + ",ID1:" + ID1 + ",ID2:" + ID2 + ",ID3:" + ID3 + ",ID4:" + ID4);
			array.Add("data:" + string.Join(",",data));
			foreach ( string key in _attributes.Keys)
			{
				array.Add("\""+key+"\":" + _attributes[key]);
			}
			return array;
		}
		#region Methods that depends on object type
		/// <summary>
		/// Gets the current experience at percent of the character.
		/// <para>Used on : Characters</para>
		/// </summary>
		public ulong getExpPercent()
		{
			if (this.Contains(SRAttribute.Exp) && this.Contains(SRAttribute.ExpMax))
			{
				return (ulong)this[SRAttribute.Exp] * 100 / (ulong)this[SRAttribute.ExpMax];
			}
			return 0;
		}
		/// <summary>
		/// Check if skill has auto transfer effect like Recovery division.
		/// <para>Used on : Skills</para>
		/// </summary>
		public bool isAutoTransferEffect()
		{
			if (type == Type.Skill)
			{
				return _data[69] == "1701213281";
			}
			return false;
		}
		/// <summary>
		/// Check if an entity is at dungeon like Cave, Forgotten World, or Temple.
		/// </summary>
		public bool inDungeon(ushort region)
		{
			return (region >= short.MaxValue);
		}
		/// <summary>
		/// Check if an entity is at dungeon like Cave, Forgotten World,
		/// or Temple, using his saved previously region. 
		/// </summary>
		public bool inDungeon()
		{
			return ( (ushort)this[SRAttribute.Region] >= short.MaxValue );
		}
		/// <summary>
		/// Get entity position with in game coordinates.
		/// </summary>
		/// <returns><see cref="Point.Empty"/> if cannot find enougth data</returns>
		public Point getPosition()
		{
			if (this.Contains(SRAttribute.Region)
				&& this.Contains(SRAttribute.X)
				&& this.Contains(SRAttribute.Y)
				&& this.Contains(SRAttribute.Z))
			{
				if (!this.inDungeon())
				{
					// Map: {yTile}x{xTile}.jpg
					byte yTile = (byte)((ushort)this[SRAttribute.Region] >> 8);
					byte xTile = (byte)((ushort)this[SRAttribute.Region] & 255);
					// World tiles offsets. Y: 135; X: 92;
					// 256x256 (image size) but taken as 192x192 units (zoom scale)
					Point p = new Point();
					p.X = (int)((xTile - 135) * 192 + ((float)this[SRAttribute.X]) / 10);
					p.Y = (int)((yTile - 92) * 192 + ((float)this[SRAttribute.Y]) / 10);
					return p;
				}
				else
				{

					return Point.Empty;
				}
			}
			throw new Exception("Not enough data to generate position.");
		}
		public Point getDestinationPosition()
		{
			if (this.Contains(SRAttribute.DestinationRegion)
				&& this.Contains(SRAttribute.DestinationOffsetX)
				&& this.Contains(SRAttribute.DestinationOffsetY)
				&& this.Contains(SRAttribute.DestinationOffsetZ))
			{
				if (!this.inDungeon((ushort)this[SRAttribute.DestinationRegion]))
				{
					// Map: {yTile}x{xTile}.jpg
					byte yTile = (byte)((ushort)this[SRAttribute.DestinationRegion] >> 8);
					byte xTile = (byte)((ushort)this[SRAttribute.DestinationRegion] & 255);
					
					// World tiles offsets. Y: 135; X: 92;
					// 256x256 (image size) but taken as 192x192 units (zoom scale)
					Point p = new Point();
					p.X = (xTile - 135) * 192 + ((ushort)this[SRAttribute.DestinationOffsetX]) / 10;
					p.Y = (yTile - 92) * 192 + (ushort.MaxValue - (ushort)this[SRAttribute.DestinationOffsetY]) / 10;
					return p;
				}
				else
				{
					return Point.Empty;
				}
			}
			throw new Exception("Not enough data to generate destination position.");
		}
		#endregion
	}
	/// <summary>
	/// Class to keep control of dinamyc & static arrays where the index matter.
	/// </summary>
	public class SRList
	{
		#region Attributes & Propierties
		/// <summary>
		/// Maximum capacity.
		/// </summary>
		public int Capacity
		{
			get
			{
				return _items.Count;
			}
		}
		private int _count;
		/// <summary>
		/// Counter of objects not null.
		/// </summary>
		public int Count
		{
			get { return _count; }
		}
		private List<SRObject> _items;
		public SRObject this[int index]
		{
			get
			{
				return _items[index];
			}
			set
			{
				if (index >= _items.Count)
				{
					// Expand the list
					for (int i = _items.Count; i <= index; i++)
					{
						_items.Add(null);
					}
				}
				// Keep control about real objects at list
				if (value == null && _items[index] != null)
				{
					_count--;
				}
				else if (value != null && _items[index] == null)
				{
					_count++;
				}
				// Set new value
				_items[index] = value;
			}
		}
		#endregion
		#region Constructor
		/// <summary>
		/// Creates a <see cref="List{T}"/> that will be handled as static.
		/// </summary>
		public SRList()
		{
			_items = new List<SRObject>();
			_count = 0;
		}
		/// <summary>
		/// Create a <see cref="List{T}"/> that will be handled as static.
		/// <para>If the Capacity is excedded will be automagically expanded to his new maximum index.</para>
		/// </summary>
		/// <param name="Capacity">Maximum length of the list</param>
		public SRList(uint Capacity)
		{
			_items = new List<SRObject>((int)Capacity);
			for (int i = 0; i < Capacity; i++)
				_items.Add(null);
			_count = 0;
		}
		#endregion
		#region Methods
		public void Clear()
		{
			_items.Clear();
			_count = 0;
		}
		/// <summary>
		/// Check if the list has somw job equipment
		/// </summary>
		/// <returns></returns>
		public bool ContainsJobEquipment()
		{
			for (int i = 0; i < Capacity; i++)
			{
				if (_items[i] != null && _items[i].type == SRObject.Type.Item)
				{
					if (_items[i].ID1 == 3 && _items[i].ID2 == 1 && _items[i].ID3 == 7)
					{
						//if (_items[i].ID4 == 1 || _items[i].ID4 == 2 || _items[i].ID4 == 3)
						//{
							return true;
						//}
					}
				}
			}
			return false;
		}
		#endregion
	}
}