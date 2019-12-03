using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects
{
	public enum SRProperty
	{
		/// <summary>
		/// Scale, using 4 bits for Volume/Height respectly.
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		Scale,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		Level,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		LevelMax,
		/// <summary>
		/// Experience.
		/// <para>Type : <see cref="ulong"/></para>
		/// </summary>
		Exp,
		/// <summary>
		/// Maximum experience.
		/// <para>Type : <see cref="ulong"/></para>
		/// </summary>
		ExpMax,
		/// <summary>
		/// Skill points.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		SP,
		/// <summary>
		/// Skill points experience.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		SPExp,
		/// <summary>
		/// Current strength points.
		/// <para>Type : <see cref="ushort"/></para>
		/// </summary>
		STR,
		/// <summary>
		/// Current intelligence points.
		/// <para>Type : <see cref="ushort"/></para>
		/// </summary>
		INT,
		/// <summary>
		/// Stat points remaining.
		/// <para>Type : <see cref="ushort"/></para>
		/// </summary>
		StatPoints,
		/// <summary>
		/// Current health points.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		HP,
		/// <summary>
		/// Current mana points.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		MP,
		/// <summary>
		/// Check if an character is being deleted.
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		isDeleting,
		/// <summary>
		/// Time for the character being deleted totally.
		/// <para>Type : <see cref="DateTime"/></para>
		/// </summary>
		DeletingDate,
		/// <summary>
		/// Position as Guild member.
		/// <para>Type : <see cref="Types.GuildMember"/></para>
		/// </summary>
		GuildMemberType,
		/// <summary>
		/// ?
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		isGuildRenameRequired,
		/// <summary>
		/// <para>Type : <see cref="Types.AcademyMember"/></para>
		/// </summary>
		AcademyMemberType,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Inventory,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		InventoryAvatar,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		Plus,
		/// <summary>
		/// <para>Type : <see cref="ulong"/></para>
		/// </summary>
		Gold,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		BerserkPoints,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		BerserkLevel,
		/// <summary>
		/// Maximum health points.
		/// <para>Type <see cref="uint"/></para>
		/// </summary>
		HPMax,
		/// <summary>
		/// Maximum mana points.
		/// <para>Type <see cref="uint"/></para>
		/// </summary>
		MPMax,
		/// <summary>
		/// <para>Type <see cref="Types.ExpIcon"/></para>
		/// </summary>
		ExpIconType,
		GatheredExpPoint,
		PKDaily,
		PKTotal,
		PKPenalty,
		PVPCapeType,
		RentType,
		RentCanDelete,
		RentCanRecharge,
		RentPeriodBeginTime,
		RentPeriodEndTime,
		RentPackingTime,
		RentMeterRateTime,
		/// <summary>
		/// <para>Type : <see cref="ulong"/></para>
		/// </summary>
		Variance,
		/// <summary>
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		Durability,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		MagicParams,
		/// <summary>
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		Value,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		SocketParams,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		AdvanceElixirParams,
		PetState,
		PetModelID,
		PetName,
		Amount,
		/// <summary>
		/// <para>Type : <see cref="ushort"/></para>
		/// </summary>
		Quantity,
		/// <summary>
		/// <para>Type : <see cref="ushort"/></para>
		/// </summary>
		QuantityMax,
		/// <summary>
		/// Magic and Attribute stone assimilation chance.
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		AssimilationProbability,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Masteries,
		/// <summary>
		/// Contains all skills mastered by the character.
		/// <para>Type : <see cref="SRObjectDictionary{T}"/> Where <see cref="{T}"/> : <see cref="uint"/></para>
		/// </summary>
		Skills,
		/// <summary>
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		isEnabled,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		QuestsCompleted,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Quests,
		Achievements,
		isAutoShareRequired,
		QuestType,
		TimeRemain,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Objectives,
		/// <summary>
		/// Tasks ID.
		/// <para>Type : <see cref="uint"/>[]</para>
		/// </summary>
		TasksID,
		NPCIDs,
		CollectionBooks,
		Number,
		StartedDatetime,
		Pages,
		/// <summary>
		/// ID to keep on track all near objects.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		UniqueID,
		/// <summary>
		/// Last positon known, it updates everytime <see cref="SRObject.GetPosition"/> is called.
		/// <para>Type : <see cref="SRCoord"/></para>
		/// </summary>
		Position,
		/// <summary>
		/// Last movement position known.
		/// <para>Type : <see cref="SRCoord"/></para>
		/// </summary>
		MovementPosition,
    Angle,
		hasMovement,
		MovementSpeedType,
		MovementActionType,
		MovementAngle,
		LifeState,
		MotionStateType,
		PlayerStateType,
		/// <summary>
		/// <para>Type : <see cref="float"/></para>
		/// </summary>
		SpeedWalking,
		/// <summary>
		/// <para>Type : <see cref="float"/></para>
		/// </summary>
		SpeedRunning,
		/// <summary>
		/// <para>Type : <see cref="float"/></para>
		/// </summary>
		SpeedBerserk,
		/// <summary>
		/// Contains all buffs casted on character.
		/// <para>Type : <see cref="SRObjectDictionary{T}"/> Where <see cref="{T}"/> : <see cref="uint"/></para>
		/// </summary>
		Buffs,
		/// <summary>
		/// NPC models ID.
		/// <para>Type : <see cref="uint"/>[]</para>
		/// </summary>
		NPCsID,
		/// <summary>
		/// Duration at miliseconds.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		Duration,
		/// <summary>
		/// Max. duration at miliseconds.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		DurationMax,
		/// <summary>
		/// Time for re-using it.
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		Cooldown,
		/// <summary>
		/// <para>Type : <see cref="int"/></para>
		/// </summary>
		Casttime,
		isOwner,
		/// <summary>
		/// <para>Type : <see cref="Types.Mob"/></para>
		/// </summary>
		MobType,
		/// <summary>
		/// <para>Type : <see cref="string"/></para>
		/// </summary>
		JobName,
		/// <summary>
		/// <para>Type : <see cref="Types.Job"/></para>
		/// </summary>
		JobType,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		JobLevel,
		/// <summary>
		/// Job experience.
		/// <para>Type : <see cref="ulong"/></para>
		/// </summary>
		JobExp,
		/// <summary>
		/// Maximum experience from Job currently used.
		/// <para>Type : <see cref="ulong"/></para>
		/// </summary>
		JobExpMax,
		JobContribution,
		JobReward,
		/// <summary>
		/// <para>Type : <see cref="Types.PVPState"/></para>
		/// </summary>
		PVPStateType,
		/// <summary>
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		isRiding,
		/// <summary>
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		inCombat,
		RidingUniqueID,
		CaptureTheFlagType,
		GuideFlag,
		JoinID,
		/// <summary>
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		isGameMaster,
		/// <summary>
		/// <para>Type : <see cref="List{T}"/> where T : <see cref="string"/></para>
		/// </summary>
		SkillParams,
		DropUniqueID,
		/// <summary>
		/// <para>Type : <see cref="Types.BadStatus"/></para>
		/// </summary>
		BadStatusFlags,
		Description,
		hasMask,
		Mask,
		MaskItems,
		refEventStructID,
		ScrollMode,
		InteractMode,
		GuildName,
		GuildID,
		GuildMemberName,
		GuildLastCrestRev,
		UnionID,
		UnionLastCrestRev,
		GuildisFriendly,
    GuildMemberAuthorityType,
		StallTitle,
		StallDecorationType,
		EquipmentCooldown,
		SkillID,
		/// <summary>
		/// <para>Type : <see cref="Types.Weapon"/></para>
		/// </summary>
		WeaponRequired01,
		/// <summary>
		/// <para>Type : <see cref="Types.Weapon"/></para>
		/// </summary>
		WeaponRequired02,
		/// <summary>
		/// Last update at utc time.
		/// <para>Type : <see cref="System.Diagnostics.Stopwatch"/></para>
		/// </summary>
		LastUpdateTime,
		HPMP,
		/// <summary>
		/// icon path from Pk2.
		/// <para>Type : <see cref="string"/></para>
		/// </summary>
		Icon,
		hasTalk,
		TalkOptions,
		Storage,
		StorageGold,
		ShopBuyBack,
		PickSettingFlags,
		AttackSettingsFlags,
		hasOwner,
		/// <summary>
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		OwnerUniqueID,
		/// <summary>
		/// <para>Type : <see cref="bool"/></para>
		/// </summary>
		isTargetRequired,
		/// <summary>
		/// <para>Type : <see cref="string"/></para>
		/// </summary>
		OwnerName,
		/// <summary>
		/// <para>Type : <see cref="ushort"/></para>
		/// </summary>
		HGP,
		/// <summary>
		/// <para>Type : <see cref="uint"/></para>
		/// </summary>
		GroupID,
		/// <summary>
		/// <para>Type : <see cref="string"/></para>
		/// </summary>
		GroupName,
		PhyAtkMin,
		PhyAtkMax,
		MagAtkMin,
		MagAtkMax,
		PhyDefense,
		MagDefense,
		HitRate,
		ParryRatio,
		OwnerJoinID,

		TeleportName,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		TeleportOptions,
		/// <summary>
		/// <para>Type : <see cref="byte"/></para>
		/// </summary>
		Slot,
		/// <summary>
		/// <para>Type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		InventoryExchange,
		
    DropSource,
		Rarity,
		Appearance,
		OwnerRefObjID,
		
		unkByte01,
		unkByte02,
		unkByte03,
		unkByte04,
		unkByte05,
		unkByte06,
		unkByte07,
		unkByte08,
		unkUShort01,
		unkUShort02,
		unkUInt01,
		unkUInt02,
		unkUInt03,
		unkUInt04,
		InventoryStall,
		/// <summary>
		/// Stall unique ID from viewers 
		/// <para>Type : <see cref="uint"/>[]</para>
		/// </summary>
		StallViewersID
	}
}
