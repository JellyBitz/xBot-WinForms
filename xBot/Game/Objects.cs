using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

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
		/// </summary>
		Name,
		/// <summary>
		/// Server name of the entity.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		Servername,
		/// <summary>
		/// Scale of the entity, using four bits for Volume/Height respectly.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		Scale,
		/// <summary>
		/// Level of the entity.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		Level,
		/// <summary>
		/// Maximum level reached.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		LevelMax,
		/// <summary>
		/// Experience.
		/// <para>Data type : <see cref="ulong"/></para>
		/// </summary>
		Exp,
		/// <summary>
		/// Maximum experience, used to calculate the current experience percent.
		/// <para>Data type : <see cref="ulong"/></para>
		/// </summary>
		ExpMax,
		/// <summary>
		/// Maximum experience from Job currently used.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		JobExpMax,
		/// <summary>
		/// Skill points.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		SP,
		/// <summary>
		/// Skill points experience.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		SPExp,
		/// <summary>
		/// Current strength points.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		STR,
		/// <summary>
		/// Current intelligence points.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		INT,
		/// <summary>
		/// Stat points remaining.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		StatPoints,
		/// <summary>
		/// Current health points.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		HP,
		/// <summary>
		/// Current mana points.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		MP,
		/// <summary>
		/// Check if an character is being deleted.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isDeleting,
		/// <summary>
		/// Time in minutes for the character being deleted totally.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		DeletingTime,
		/// <summary>
		/// Position as Guild member.
		/// <para>Data type : <see cref="Types.GuildMember"/></para>
		/// </summary>
		GuildMemberType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isGuildRenameRequired,
		/// <summary>
		/// Position as Academy member.
		/// <para>Data type : <see cref="Types.AcademyMember"/></para>
		/// </summary>
		AcademyMemberType,
		/// <summary>
		/// Store all info about items (at <see cref="SRObject"/> instances) displayed and stored.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Inventory,
		/// <summary>
		/// Store all info about avatar items (at <see cref="SRObject"/> instances) displayed.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		InventoryAvatar,
		/// <summary>
		/// Item Plus. Value increased by using elixirs.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		Plus,
		/// <summary>
		/// Gold quantity.
		/// <para>Data type : <see cref="ulong"/></para>
		/// </summary>
		Gold,
		/// <summary>
		/// Berserk points. 5 Points means Berserk ready.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		BerserkPoints,
		/// <summary>
		/// Berserk level, duration depends on it.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		BerserkLevel,
		/// <summary>
		/// Maximum health points.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		HPMax,
		/// <summary>
		/// Maximum mana points.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		MPMax,
		/// <summary>
		/// Experience Icon.
		/// <para>Data type : <see cref="Types.ExpIcon"/></para>
		/// </summary>
		ExpIconType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		GatheredExpPoint,
		/// <summary>
		/// Daily kills on PK mode.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		PKDaily,
		/// <summary>
		/// Total kill points on PK mode.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		PKTotal,
		/// <summary>
		/// Penalty kill points to get out from PK mode.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		PKPenalty,
		/// <summary>
		/// PVP Cape mode.
		/// <para>Data type : <see cref="Types.PVPCape"/></para>
		/// </summary>
		PVPCapeType,
		/// <summary>
		/// Rent type, like items premium, event and more.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		RentType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		RentCanDelete,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		RentCanRecharge,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		RentPeriodBeginTime,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		RentPeriodEndTime,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		RentPackingTime,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		RentMeterRateTime,
		/// <summary>
		/// Display variance.
		/// <para>Data type : <see cref="ulong"/></para>
		/// </summary>
		Variance,
		/// <summary>
		/// Durability points till item break.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Durability,
		/// <summary>
		/// Magic propierties.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		MagicParams,
		/// <summary>
		/// Type.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Type,
		/// <summary>
		/// Value.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Value,
		/// <summary>
		/// Socket propierties.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		SocketParams,
		/// <summary>
		/// Slot number
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		Slot,
		/// <summary>
		/// Identifier.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		ID,
		/// <summary>
		/// Advance elixir propierties.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		AdvanceElixirParams,
		/// <summary>
		/// Pet state.
		/// <para>Data type : <see cref="Types.PetState"/></para>
		/// </summary>
		PetState,
		/// <summary>
		/// Display model reference.
		/// <para>Data type : <see cref="SRObject"/></para>
		/// </summary>
		Model,
		/// <summary>
		/// Do not confuse with <see cref="SRAttribute.Quantity"/> (stack count). This indicates the amount of objects inside.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Amount,
		/// <summary>
		/// Quantity of objects joined at the same item (Stacks).
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		Quantity,
		/// <summary>
		/// Stone assimilation probability at percentage.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		AssimilationProbability,
		/// <summary>
		/// Masteries tabs from character.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Masteries,
		/// <summary>
		/// List of skill objects
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Skills,
		/// <summary>
		/// Enabled
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		Enabled,
		/// <summary>
		/// ID's Array from quest completed 
		/// <para>Data type : <see cref="byte"/>[]</para>
		/// </summary>
		QuestsCompletedID,
		/// <summary>
		/// List of all quest activated
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Quests,
		/// <summary>
		/// Quantity of achievements on the quest.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		Achievements,
		/// <summary>
		/// Check if party mode auto-share is required.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isAutoShareRequired,
		/// <summary>
		/// Type of quest.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		QuestType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		TimeRemain,
		/// <summary>
		/// Check if the objective is on.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isActive,
		/// <summary>
		/// Quest objectives.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Objectives,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		ObjectiveID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/>[]</para>
		/// </summary>
		TaskValues,
		/// <summary>
		/// NPC ID's.
		/// <para>Data type : <see cref="uint"/>[]</para>
		/// </summary>
		NPCIDs,
		/// <summary>
		/// Collection books from differents Forgotten Worlds.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		CollectionBooks,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Number,
		/// <summary>
		/// Book date started (SROTimestamp).
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		StartedDatetime,
		/// <summary>
		/// Book pages already done. 
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Pages,
		/// <summary>
		/// Unique ID to keep the track of all objects in game.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		UniqueID,
		/// <summary>
		/// Reference to the region of the map. Contains bytes for sector X and Y from map.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		Region,
		/// <summary>
		/// Position X of the map.
		/// <para>Data type : <see cref="int"/></para>
		/// </summary>
		X,
		/// <summary>
		/// Position Y of the map.
		/// <para>Data type : <see cref="int"/></para>
		/// </summary>
		Y,
		/// <summary>
		/// Position Z of the map.
		/// <para>Data type : <see cref="int"/></para>
		/// </summary>
		Z,
		/// <summary>
		/// Arrow direction from the character at the map.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		Angle,
		/// <summary>
		/// Check if the entity is moving to some point.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		hasMovement,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		MovementType,
		/// <summary>
		/// Movement destination region of the object.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		MovementRegion,
		/// <summary>
		/// Movement offset position X of the map.
		/// <para>Data type : <see cref="int"/></para>
		/// </summary>
		MovementOffsetX,
		/// <summary>
		/// Movement offset position Y of the map.
		/// <para>Data type : <see cref="int"/></para>
		/// </summary>
		MovementOffsetY,
		/// <summary>
		/// Movement offset position Z of the map.
		/// <para>Data type : <see cref="int"/></para>
		/// </summary>
		MovementOffsetZ,
		/// <summary>
		/// Movement action.
		/// <para>Data type : <see cref="Types.MovementAction"/></para>
		/// </summary>
		MovementActionType,
		/// <summary>
		/// New arrow direction from the character at the map after the movement.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		MovementAngle,
		/// <summary>
		/// Current entity state.
		/// <para>Data type : <see cref="Types.LifeState"/></para>
		/// </summary>
		LifeState,
		/// <summary>
		/// Motion being used.
		/// <para>Data type : <see cref="Types.MotionState"/></para>
		/// </summary>
		MotionState,
		/// <summary>
		/// Game restrictions comes with client graphic animation.
		/// <para>Data type : <see cref="Types.PlayerState"/></para>
		/// </summary>
		PlayerState,
		/// <summary>
		/// Walking speed percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// </summary>
		SpeedWalking,
		/// <summary>
		/// Running speed percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// </summary>
		SpeedRunning,
		/// <summary>
		/// Speed using berserk mode, at percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// </summary>
		SpeedBerserk,
		/// <summary>
		/// Active buffs.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		Buffs,
		/// <summary>
		/// Time left.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		Duration,
		/// <summary>
		/// Check if is creator of the current object reference.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isCreator,
		/// <summary>
		/// Mob type. 
		/// <para>Data type : <see cref="Types.Mob"/></para>
		/// </summary>
		MobType,
		/// <summary>
		/// Name used on job mode.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		JobName,
		/// <summary>
		/// Job type class.
		/// <para>Data type : <see cref="Types.Job"/></para>
		/// </summary>
		JobType,
		/// <summary>
		/// Level on job mode.
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		JobLevel,
		/// <summary>
		/// Current experience on job mode.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		JobExp,
		/// <summary>
		/// Job points gained.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		JobContribution,
		/// <summary>
		/// Job reward.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		JobReward,
		/// <summary>
		/// PVP combat state.
		/// <para>Data type : <see cref="Types.PVPState"/></para>
		/// </summary>
		PVPState,
		/// <summary>
		/// Check if has transport activated
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		hasTransport,
		/// <summary>
		/// Check if is on combat mode.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isFighting,
		/// <summary>
		/// Transport Unique ID.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		TransportUniqueID,
		/// <summary>
		/// Check the CTF Event.
		/// <para>Data type : <see cref="Types.CaptureTheFlag"/></para>
		/// </summary>
		CaptureTheFlagType,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="ulong"/></para>
		/// </summary>
		GuideFlag,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		JID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		GMFlag,
		/// <summary>
		/// Attributes used by the skill, concatenated by "|" symbol.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		SkillAttributes,
		/// <summary>
		/// Drop group by unique ID.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		DropUniqueID,
		/// <summary>
		/// Check the current Bad status.
		/// <para>Data type : <see cref="Types.BadStatus"/></para>
		/// </summary>
		BadStatusType,
		/// <summary>
		/// Short description for users.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		Description,
		/// <summary>
		/// Check if the player is using mask (rogue item)
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		hasMask,
		/// <summary>
		/// The exact model of the mask using.
		/// <para>Data type : <see cref="SRObject"/></para>
		/// </summary>
		Mask,
		/// <summary>
		/// Used to display exactly the clone.
		/// <para>Data type : <see cref="SRObjectCollection"/></para>
		/// </summary>
		MaskItems,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		refEventStructID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="Types.ScrollMode"/></para>
		/// </summary>
		ScrollMode,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="Types.InteractMode"/></para>
		/// </summary>
		InteractMode,
		/// <summary>
		/// Guild name, can be empty.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		GuildName,
		/// <summary>
		/// Guild unique Identifier. 
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		GuildID,
		/// <summary>
		/// Guild member name set by the master.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		GuildMemberName,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		GuildLastCrestRev,
		/// <summary>
		/// Union unique Identifier. 
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		UnionID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		UnionLastCrestRev,
		/// <summary>
		/// Check if the guild is friendly (Fortress War).
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		GuildisFriendly,
		/// <summary>
		/// Guild member authority. (Fortress War)
		/// <para>Data type : <see cref="Types.GuildMemberAuthority"/></para>
		/// </summary>
		GuildMemberAuthorityType,
		/// <summary>
		/// Stall name.
		/// <para>Data type : <see cref="string"/></para>
		/// </summary>
		StallName,
		/// <summary>
		/// Stall decoration identifier.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		DecorationItemID,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		EquipmentCooldown,
		/// <summary>
		/// Skill reference.
		/// <para>Data type : <see cref="SRObject"/></para>
		/// </summary>
		Skill,
		/// <summary>
		/// Last movement date. Used to calculate the position with accuracy.
		/// <para>Data type : <see cref="DateTime"/></para>
		/// </summary>
		MovementDate,
		/// <summary>
		/// Health and Mana in percentage into a byte. (4/4 bits)
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		HPMP,


		DropSource,
		hasTalk,
		TalkOptions,
		Rarity,
		Appearance,
		OwnerName,
		OwnerRefObjID,
		OwnerUniqueID,
		hasOwner,
		OwnerJID,
		

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
		unkUInt03
	}
	/// <summary>
	/// <para> Funny and customizable class to handle all game objects
	/// in a dynamic way without falling too much at performance.</para>
	/// </summary>
	public class SRObject
	{
		public enum Type
		{
			Entity,
			Model,
			Teleport,
			Item,
			Mastery,
			Skill,
			Quest,
			BuffZone
		}
		private Dictionary<string, object> _attributes;
		private uint id;
		public uint ID { get { return id; } }
		private Type _type;
		public Type type { get { return _type; } }
		private byte tid1, tid2, tid3, tid4;
		public byte ID1 { get { return tid1; } }
		public byte ID2 { get { return tid2; } }
		public byte ID3 { get { return tid3; } }
		public byte ID4 { get { return tid4; } }
		/// <summary>
		/// Undefined game object, that has no identifiers or are not known yet.
		/// Also can be used as property from another object.
		/// </summary>
		public SRObject()
		{
			_attributes = new Dictionary<string, object>();
			id = tid1 = tid2 = tid3 = tid4 = 0;
    }
		/// <summary>
		/// Creates a game object from the ID specified.
		/// </summary>
		/// <param name="ID">Object identifier</param>
		/// <param name="Type">Object type</param>
		public SRObject(uint ID, Type Type)
		{
			_attributes = new Dictionary<string, object>();
			Load(ID, Type);
		}
		/// <summary>
		/// Load the object overriding all attributes previously saved.
		/// </summary>
		/// <param name="ID">Object identifier</param>
		/// <param name="type">Object type</param>
		public void Load(uint ID, Type type)
		{
			id = ID;
			_type = type;
			NameValueCollection data = null;
      switch (type)
			{
				case Type.Entity:
					data = Info.Get.GetModel(ID);
					if (data != null)
					{
						_type = Type.Model;
						goto case Type.Model;
					}
					data = Info.Get.GetTeleport(ID);
					if (data != null)
					{
						_type = Type.Teleport;
						goto case Type.Teleport;
					}
					data = Info.Get.GetTeleportLink(ID);
					if (data != null)
					{
						_type = Type.Teleport;
						goto case Type.Teleport;
					}
					data = Info.Get.GetItem(ID);
					if (data != null)
					{
						_type = Type.Item;
						goto case Type.Item;
					}
					if(id == uint.MaxValue)
					{
						_type = Type.BuffZone;
					}
					break;
				case Type.Model:
					if(data == null)
						data = Info.Get.GetModel(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					tid1 = 1;
					tid2 = byte.Parse(data["tid2"]);
					tid3 = byte.Parse(data["tid3"]);
					tid4 = byte.Parse(data["tid4"]);
					break;
				case Type.Item:
					if (data == null)
						data = Info.Get.GetItem(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					tid1 = 3;
					tid2 = byte.Parse(data["tid2"]);
					tid3 = byte.Parse(data["tid3"]);
					tid4 = byte.Parse(data["tid4"]);
					break;
				case Type.Teleport:
					if (data == null)
						data = Info.Get.GetTeleport(ID);
					if (data == null)
						data = Info.Get.GetTeleportLink(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					tid1 = 4;
					tid2 = byte.Parse(data["tid2"]);
					tid3 = byte.Parse(data["tid3"]);
					tid4 = byte.Parse(data["tid4"]);
					break;
				case Type.Skill:
					if (data == null)
						data = Info.Get.GetSkill(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					this[SRAttribute.SkillAttributes] = data["attributes"];
					break;
				case Type.Mastery:
					if (ID != 0)
					{
						data = Info.Get.GetMastery(ID);
						this[SRAttribute.Name] = data["name"];
						this[SRAttribute.Description] = data["description"];
					}
					else
					{
						this[SRAttribute.Name] = "";
						this[SRAttribute.Description] = "";
					}
					break;
				case Type.Quest:

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
		public bool Equals(byte ID1, byte ID2, byte ID3, byte ID4)
		{
			return (this.ID1 == ID1) && (this.ID2 == ID2) && (this.ID3 == ID3) && (this.ID4 == ID4);
		}
		/// <summary>
		///  Clone all objects attributes keeping a new object reference.
		/// </summary>
		public SRObject Clone()
		{
			return (SRObject)MemberwiseClone();
		}
		/// <summary>
		/// Converts all his <see cref="SRAttribute"/> to nodes for an easy view.
		/// </summary>
		public TreeNode ToNode()
		{
			string text = (string)this[SRAttribute.Name];
			TreeNode root = new TreeNode(text==""?"No name": text);
			if (Contains(SRAttribute.UniqueID))
				root.Name = ((uint)this[SRAttribute.UniqueID]).ToString();
			root.Nodes.Add(new TreeNode("ID : " + ID + " (" + type.ToString() + ")"));
			root.Nodes.Add(new TreeNode("Type IDs [" + ID1 + "][" + ID2 + "][" + ID3 + "][" + ID4 + "]"));
			foreach (string key in _attributes.Keys)
			{
				switch (_attributes[key].GetType().Name)
				{
					case "SRObject":
						root.Nodes.Add(((SRObject)_attributes[key]).ToNode());
						break;
					case "SRObjectCollection":
						TreeNode obj = new TreeNode(key);
						obj.Nodes.AddRange(((SRObjectCollection)_attributes[key]).ToNodes());
						root.Nodes.Add(obj);
						break;
					case "Byte[]": // Will be temporaly till find understandable data.
						TreeNode bytes = new TreeNode(key);
						foreach (byte _byte in (byte[])_attributes[key])
							bytes.Nodes.Add(_byte.ToString());
						root.Nodes.Add(bytes);
						break;
					case "Int32[]": // Will be temporaly till find understandable data.
						TreeNode ints = new TreeNode(key);
						foreach (int _int in (int[])_attributes[key])
							ints.Nodes.Add(_int.ToString());
						root.Nodes.Add(ints);
						break;
					default:
						root.Nodes.Add(new TreeNode("\"" + key + "\" : " + _attributes[key]));
						break;
				}
			}
			return root;
		}
		#region Methods that depends on object type
		/// <summary>
		/// Return the ountry type of the item
		/// </summary>
		/// <returns></returns>
		public string GetCountry()
		{
			if (((string)this[SRAttribute.Servername]).Contains("_CH_"))
			{
				return "CH";
			}
			else if (((string)this[SRAttribute.Servername]).Contains("_EU_"))
			{
				return "EU";
      }
			return "";
		}
		/// <summary>
		/// Gets the current experience at percent.
		/// </summary>
		public double GetExpPercent()
		{
			return (ulong)this[SRAttribute.Exp] * 100d / (ulong)this[SRAttribute.ExpMax];
		}
		/// <summary>
		/// Gets the current health points at percentage.
		/// </summary>
		public double GetHPPercent()
		{
			return (uint)this[SRAttribute.HP] * 100d / (uint)this[SRAttribute.HPMax];
		}
		/// <summary>
		/// Gets the current mana points at percentage.
		/// </summary>
		public double GetMPPercent()
		{
			return (uint)this[SRAttribute.MP] * 100d / (uint)this[SRAttribute.MPMax];
		}
		/// <summary>
		/// Check if <see cref="Type.Skill"/> has auto transfer effect like Recovery division.
		/// </summary>
		public bool isAutoTransferEffect()
		{
			if (type == Type.Skill)
				return ((string)this[SRAttribute.SkillAttributes]).Contains("1701213281");
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
		/// Get entity position in game coordinates.
		/// </summary>
		public Point GetPosition(ushort Region,int X,int Y,int Z)
		{
			if (!this.inDungeon(Region))
			{
				// Map: {yTile}x{xTile}.jpg
				byte yTile = (byte)(Region >> 8);
				byte xTile = (byte)(Region & 255);
				// World tiles offsets. Y: 135; X: 92;
				// 192x192 units / 10:1 scale 
				Point p = new Point();
				p.X = (xTile - 135) * 192 + (X / 10);
				p.Y = (yTile - 92) * 192 + (Y / 10);
				return p;
			}
			else
			{
				// Not confirmed yet...

				//// Map: {yTile}x{xTile}.jpg
				//byte yTile = (byte)(Region >> 8);
				//byte xTile = (byte)(Region & 255);
				//// World tiles offsets. Y: 128; X: 128;
				//switch (xTile)
				//{
				//	case 1: // Cave Downhang

				//		break;
				//}
				Point p = new Point();
				//p.X = (xTile - 128) * 192 + (X / 10);
				//p.Y = (yTile - 128) * 192 + (Y/ 10);
				return p;
			}
		}
		/// <summary>
		/// Converts and return an degree (angle) with 360° as max.
		/// </summary>
		public int GetDegree(ushort Angle){
			int degree = (Angle * 360 / 0xFFFF);
			// Reduce to 360 as max
			while(degree >= 360){
				degree -= 360;
			}
			return degree;
		}
		#endregion
	}
	/// <summary>
	/// Class to keep control of dinamyc & static arrays where the index matter.
	/// </summary>
	public class SRObjectCollection
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
		public SRObjectCollection()
		{
			_items = new List<SRObject>();
			_count = 0;
		}
		/// <summary>
		/// Create a <see cref="List{T}"/> that will be handled as static.
		/// <para>If the Capacity is excedded will be automagically expanded to his new maximum index.</para>
		/// </summary>
		/// <param name="Capacity">Maximum length of the list</param>
		public SRObjectCollection(uint Capacity)
		{
			_items = new List<SRObject>((int)Capacity);
			for (int i = 0; i < Capacity; i++)
				_items.Add(null);
			_count = 0;
		}
		#endregion
		#region Methods
		public void Add(SRObject obj)
		{
			this[Capacity] = obj;
    }
		public void Clear()
		{
			_items.Clear();
			_count = 0;
		}
		public TreeNode[] ToNodes()
		{
			List<TreeNode> nodes = new List<TreeNode>();
			foreach (SRObject obj in _items)
			{
				if (obj == null)
					nodes.Add(new TreeNode("Empty"));
				else
					nodes.Add(obj.ToNode());
			}
			return nodes.ToArray();
		}
		/// <summary>
		///  Clone all objects attributes keeping a new object reference.
		/// </summary>
		public SRObjectCollection Clone()
		{
			return (SRObjectCollection)MemberwiseClone();
		}
		/// <summary>
		/// Check if the list has some job equipment
		/// </summary>
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