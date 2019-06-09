using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
		/// Skill points.
		/// </summary>
		SP,
		/// <summary>
		/// Skill points experience.
		/// </summary>
		SPExp,
		/// <summary>
		/// Current strength points.
		/// </summary>
		STR,
		/// <summary>
		/// Current intelligence points.
		/// </summary>
		INT,
		/// <summary>
		/// Stat points remaining.
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
		/// Item Plus.
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
		/// <para>Data type : <see cref="Types.Exp"/></para>
		/// <para>Used on : Character</para>
		/// </summary>
		ExpType,
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
		/// <para>Used on : <see cref="SRAttribute.Inventory"/></para>
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
		/// <para>Data type : <see cref="float"/></para>
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
		hasDestination,
		/// <summary>
		/// ...
		/// <para>Data type : <see cref="byte"/></para>
		/// </summary>
		MovementType,
		/// <summary>
		/// Destination region of the object.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		DestinationRegion,
		/// <summary>
		/// Destination offset position X of the map.
		/// <para>Data type : <see cref="uint"/> (if is on dungeons) or <see cref="ushort"/></para>
		/// </summary>
		DestinationOffsetX,
		/// <summary>
		/// Destination offset position Y of the map.
		/// <para>Data type : <see cref="uint"/> (if is on dungeons) or <see cref="ushort"/></para>
		/// </summary>
		DestinationOffsetY,
		/// <summary>
		/// Destination offset position Z of the map.
		/// <para>Data type : <see cref="uint"/> (if is on dungeons) or <see cref="ushort"/></para>
		/// </summary>
		DestinationOffsetZ,
		/// <summary>
		/// Cause of the movement.
		/// <para>Data type : <see cref="Types.MovementSource"/></para>
		/// </summary>
		MovementSource,
		/// <summary>
		/// New arrow direction from the character at the map after the movement.
		/// <para>Data type : <see cref="ushort"/></para>
		/// </summary>
		MovementAngle,
		/// <summary>
		/// Dead or alive entity state.
		/// <para>Data type : <see cref="Types.LifeState"/></para>
		/// </summary>
		LifeState,
		/// <summary>
		/// Graphic animation.
		/// <para>Data type : <see cref="Types.MotionState"/></para>
		/// </summary>
		MotionState,
		/// <summary>
		/// Graphic animation.
		/// <para>Data type : <see cref="Types.PlayerState"/></para>
		/// </summary>
		PlayerStatus,
		/// <summary>
		/// Walking speed at percentage.
		/// <para>Data type : <see cref="float"/></para>
		/// </summary>
		SpeedWalking,
		/// <summary>
		/// Running speed at percentage.
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
		/// <para>Used on : Character</para>
		/// </summary>
		hasTransport,
		/// <summary>
		/// Check if is on combat mode.
		/// <para>Data type : <see cref="bool"/></para>
		/// </summary>
		isFighting,
		/// <summary>
		/// Transport Unique ID from Job.
		/// <para>Data type : <see cref="uint"/></para>
		/// </summary>
		TransportUniqueID,
		/// <summary>
		/// Info about CTF Event.
		/// <para>Data type : <see cref="Types.CaptureTheFlag"/></para>
		/// </summary>
		PVPCaptureTheFlagType,
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
		/// <para>Data type : <see cref="xBot.Game.SRObjectCollection"/></para>
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
		SkillAttributes,
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
			Quest
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
		/// Creates a game object from the name specified.
		/// </summary>
		/// <param name="ServerName">Object name</param>
		/// <param name="Type">Object type</param>
		public SRObject(string ServerName, Type Type)
		{
			_attributes = new Dictionary<string, object>();
			Load(ServerName, Type);
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
					data = Info.Get.getModel(ID);
					if (data != null)
					{
						_type = Type.Model;
						goto case Type.Model;
					}
					data = Info.Get.getTeleport(ID);
					if (data != null)
					{
						_type = Type.Teleport;
						goto case Type.Teleport;
					}
					data = Info.Get.getItem(ID);
					if (data != null)
					{
						_type = Type.Item;
						goto case Type.Item;
					}
					break;
				case Type.Model:
					if(data == null)
						data = Info.Get.getModel(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					tid1 = 1;
					tid2 = byte.Parse(data["tid2"]);
					tid3 = byte.Parse(data["tid3"]);
					tid4 = byte.Parse(data["tid4"]);




					break;
				case Type.Item:
					if (data == null)
						data = Info.Get.getItem(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					tid1 = 3;
					tid2 = byte.Parse(data["tid2"]);
					tid3 = byte.Parse(data["tid3"]);
					tid4 = byte.Parse(data["tid4"]);



					break;
				case Type.Teleport:
					if (data == null)
						data = Info.Get.getTeleport(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					tid1 = byte.Parse(data["tid1"]);
					tid2 = byte.Parse(data["tid2"]);
					tid3 = byte.Parse(data["tid3"]);
					tid4 = byte.Parse(data["tid4"]);


					break;
				case Type.Skill:
					if (data == null)
						data = Info.Get.getSkill(ID);
					this[SRAttribute.Servername] = data["servername"];
					this[SRAttribute.Name] = data["name"];
					this[SRAttribute.SkillAttributes] = data["attributes"];

					break;
				case Type.Mastery:

					break;
				case Type.Quest:

					break;
			}


		}
		/// <summary>
		/// Load the object keeping all attributes previously saved
		/// </summary>
		/// <param name="Name">Name of the object, for example : CHAR_CH_MAN_ADVENTURER</param>
		/// <param name="type">type</param>
		public void Load(string ServerName, Type Type)
		{
			
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
			array.Add("ID:" + ID + " | ID1:" + ID1 + " | ID2:" + ID2 + " | ID3:" + ID3 + " | ID4:" + ID4);
			array.Add("Type:" +type.ToString());
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
					// 192x192 units / 10:1 scale 
					Point p = new Point();
					p.X = (int)((xTile - 135) * 192 + ((float)this[SRAttribute.X]) / 10);
					p.Y = (int)((yTile - 92) * 192 + ((float)this[SRAttribute.Y]) / 10);
					return p;
				}
				else
				{
					// Map: {yTile}x{xTile}.jpg
					byte yTile = (byte)((ushort)this[SRAttribute.Region] >> 8);
					byte xTile = (byte)((ushort)this[SRAttribute.Region] & 255);
					// World tiles offsets. Y: 128; X: 128;
					switch (xTile)
					{
						case 1: // Cave Downhang
							break;
					}
					Point p = new Point();
					p.X = (int)((xTile - 128) * 192 + ((float)this[SRAttribute.X]) / 10);
					p.Y = (int)((yTile - 128) * 192 + ((float)this[SRAttribute.Y]) / 10);
					return p;
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
		public void Clear()
		{
			_items.Clear();
			_count = 0;
		}
		public override string ToString()
		{
			string result = "[";
			foreach (SRObject i in _items)
			{
				if(i == null)
				{
					result += "null|";
        }
				else
				{
					result += i.ToString()+"|";
				}
			}
			if (result == "[")
			{
				return "[None]";
			}
			else
			{
				return result.Remove(result.Length-1) + "]";
			}
		}
		/// <summary>
		/// Check if the list has some job equipment
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