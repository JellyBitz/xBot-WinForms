using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using xBot.App;
using xBot.App.PK2Extractor;
using xBot.Game.Objects;

namespace xBot.Game
{
	/// <summary>
	/// Keep tracking of everything about the Silkroad actually selected.
	/// </summary>
	public class Info
	{
		/// <summary>
		/// Unique instance of this class.
		/// </summary>
		private static Info _this = null;
		#region Basic Properties
		/// <summary>
		/// Unique name from the Silkroad.
		/// </summary>
		public string Silkroad { get; private set; }
		/// <summary>
		/// Server name.
		/// </summary>
		public string Server { get; set; }
		/// <summary>
		/// Server id. Returns empty string if is not selected yet.
		/// </summary>
		public string ServerID { get; set; }
		/// <summary>
		/// Character name. Will be available just right before the character is selected.
		/// </summary>
		public string Charname { get; set; }
		/// <summary>
		/// Silkroad Locale
		/// </summary>
		public byte Locale { get; set; }
		/// <summary>
		/// SR_Client name
		/// </summary>
		public string SR_Client { get; set; }
		/// <summary>
		/// Silkroad Version
		/// </summary>
		public uint Version { get; set; }
		/// <summary>
		/// Gets the database previouly selected.
		/// </summary>
		public SQLDatabase Database { get; private set; }
		/// <summary>
		/// The current path to the SR_Client.
		/// </summary>
		public string ClientPath { get; set; }
		#endregion

		#region Game Properties
		/// <summary>
		/// Reference to the selected character for playing.
		/// </summary>
		public SRObject Character { get; set; }
		/// <summary>
		/// Gets all pets from character.
		/// </summary>
		public SRObjectDictionary<uint> MyPets { get; }
		/// <summary>
		/// Gets all players near the character.
		/// </summary>
		public SRObjectDictionary<string> Players { get; }
		/// <summary>
		/// Gets all mobs near the character.
		/// </summary>
		public SRObjectDictionary<uint> Mobs { get; }
		/// <summary>
		/// Gets all mobs near the character.
		/// </summary>
		public SRObjectDictionary<uint> Teleports { get; }
		/// <summary>
		/// Gets all entity that spawn closer.
		/// </summary>
		public SRObjectDictionary<uint> SpawnList { get; }
		/// <summary>
		/// Keep on track all buffs from near entity.
		/// </summary>
		public SRObjectDictionary<uint> BuffList { get; }
		/// <summary>
		/// Gets all party members. The master will be always at the first position.
		/// </summary>
		public SRObjectDictionary<uint> PartyMembers { get; }
		/// <summary>
		/// SROTimestamp.
		/// </summary>
		public uint ServerTime
		{
			get
			{
				return _ServerTime;
			}
			set
			{
				_ServerTimeDate = DateTime.UtcNow;
				_ServerTime = value;
			}
		}
		private uint _ServerTime;
		private DateTime _ServerTimeDate;
		/// <summary>
		/// Keeps the most recent PING test.
		/// </summary>
		public Stopwatch Ping { get; set; }
		#endregion
		private Info()
		{
			Character = null;
			MyPets = new SRObjectDictionary<uint>();
			Players = new SRObjectDictionary<string>();
			Mobs = new SRObjectDictionary<uint>();
			Teleports = new SRObjectDictionary<uint>();
			SpawnList = new SRObjectDictionary<uint>();
			BuffList = new SRObjectDictionary<uint>();
			PartyMembers = new SRObjectDictionary<uint>();
			Database = null;
		}
		/// <summary>
		/// GetInstance. Secures an unique class creation for being used anywhere at the project.
		/// </summary>
		public static Info Get
		{
			get
			{
				if (_this == null)
					_this = new Info();
				return _this;
			}
		}
		/// <summary>
		/// Select the database if exists. Return success.
		/// </summary>
		/// <param name="name">Database unique name</param>
		public bool ConnectToDatabase(string SilkroadName)
		{
			if (Pk2Extractor.DirectoryExists(SilkroadName))
			{
				this.Database = new SQLDatabase(Pk2Extractor.GetDatabasePath(SilkroadName));
				bool connected = this.Database.Connect();
				if (connected)
					this.Silkroad = SilkroadName;
				return connected;
			}
			return false;
		}

		/// <summary>
		/// Get an entity by his unique ID.
		/// </summary>
		/// <param name="uniqueid">Spawn object reference</param>
		/// <returns><see cref="null"/> if cannot be found</returns>
		public SRObject GetEntity(uint uniqueID)
		{
			if ((uint)Character[SRProperty.UniqueID] == uniqueID)
				return Character;
			SRObject entity = MyPets[uniqueID];
			if (entity == null)
				entity = SpawnList[uniqueID];
			return entity;
		}
		/// <summary>
		/// Server time generated with the SROTimeStamp.
		/// </summary>
		public string GetServerTime()
		{
			if (_ServerTimeDate != null)
			{
				// Reading timestamp
				int year = (int)(ServerTime & 63) + 2000;
				int month = (int)(ServerTime >> 6) & 15;
				int day = (int)(ServerTime >> 10) & 31;
				int hour = (int)(ServerTime >> 15) & 31;
				int minute = (int)(ServerTime >> 20) & 63;
				int second = (int)(ServerTime >> 26) & 63;
				DateTime time = new DateTime(year, month, day, hour, minute, second);
				// Sync time lapsed from last time saved
				time = time.Add(DateTime.UtcNow.Subtract(_ServerTimeDate));
				return time.ToString("HH:mm:ss | dd/MM/yyyy");
			}
			return "??:??:?? | ??/??/????";
		}

		#region Gets from Database
		/// <summary>
		/// Gets the maximum exp required for the level specified.
		/// </summary>
		public ulong GetExpMax(byte level)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT player FROM leveldata WHERE level=" + level);
      if (result.Count > 0)
				return ulong.Parse(result[0]["player"]);
			return 0;
		}
		/// <summary>
		/// Gets the maximum exp required for the level specified.
		/// </summary>
		public ulong GetPetExpMax(byte level)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT pet FROM leveldata WHERE level=" + level);
      if (result.Count > 0)
				return ulong.Parse(result[0]["pet"]);
			return 0;
		}
		/// <summary>
		/// Gets the maximum exp required for the job level specified.
		/// </summary>
		/// <param name="level">Job level</param>
		/// <param name="type">Trader, Thief or Hunter</param>
		public uint GetJobExpMax(byte level, Types.Job type)
		{
			if (type == Types.Job.None)
				return 0;
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM leveldata WHERE level=" + level);
      if (result.Count > 0)
				return uint.Parse(result[0][type.ToString().ToLower()]);
			return 0;
		}
		/// <summary>
		/// Get model by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetModel(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM models WHERE id=" + id);
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get model by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetModel(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM models WHERE servername='" + servername + "'");
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleport(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportbuildings WHERE id=" + id);
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleport(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportbuildings WHERE servername='" + servername + "'");
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleportLinkByID(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE id=" + id);
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleportLinkByServerName(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get item by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetItem(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM items WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get item by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetItem(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM items WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get skill by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetSkill(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM skills WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get skill by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetSkill(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM skills WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get mastery by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetMastery(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM masteries WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get region name by id, using the current database loaded.
		/// </summary>
		public string GetRegion(ushort id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT name FROM regions WHERE id=" + id + " LIMIT 1");
			if (result.Count > 0)
				return result[0]["name"];
			return "";
		}
		/// <summary>
		/// Gets the UIIT text already formated with params provided. returns an empty string if the servername is not found.
		/// </summary>
		public string GetUIFormat(string servername, params object[] args)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT text FROM textuisystem WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return string.Format(result[0]["text"], args);
			return "";
		}
		/// <summary>
		/// Get's an item object from the shop at slot specified.
		/// </summary>
		public SRObject GetItemFromShop(string npc_servername, byte tabNumber, byte tabSlot)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM shops WHERE model_servername='" + npc_servername + "' AND tab=" + tabNumber + " AND slot=" + tabSlot);
			if (result.Count > 0)
			{
				SRObject item = new SRObject(result[0]["item_servername"], SRType.Item);
				if (item.ID1 == 3 && item.ID2 == 1)
				{
					item[SRProperty.Plus] = byte.Parse(result[0]["plus"]);
					item[SRProperty.Durability] = uint.Parse(result[0]["durability"]);
				}
				if (result[0]["magic_params"] != "0")
				{
					string[] mParams = result[0]["magic_params"].Split('|');
					SRObjectCollection MagicParams = new SRObjectCollection((uint)mParams.Length);
					for (byte j = 0; j < mParams.Length; j++)
					{
						ulong param = ulong.Parse(mParams[j]);
						MagicParams[j] = new SRObject((uint)(param & uint.MaxValue), SRType.Param);
						MagicParams[j][SRProperty.Value] = (uint)(param >> 32);
					}
					item[SRProperty.MagicParams] = MagicParams;
				}
				return item;
			}
			return null;
		}
		/// <summary>
		/// Gets the last skill ID from the skill updated, returns 0 if none is found.
		/// </summary>
		public uint GetLastSkillID(SRObject skill)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT id FROM skills WHERE group_id='" + skill[SRProperty.GroupID] + "' AND level<" + skill[SRProperty.Level] + " ORDER BY level DESC LIMIT 1");
			if (result.Count > 0)
			{
				return uint.Parse(result[0]["id"]);
			}
			return 0;
		}
		/// <summary>
		/// Gets the next skill ID from the skill to update, returns 0 if none is found.
		/// </summary>
		public uint GetNextSkillID(SRObject skill)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT id FROM skills WHERE group_id='" + skill[SRProperty.GroupID] + "' AND level>" + skill[SRProperty.Level] + " ORDER BY level LIMIT 1");
			if (result.Count > 0)
			{
				return uint.Parse(result[0]["id"]);
			}
			return 0;
		}
		/// <summary>
		/// Gets the teleport link data. Return null if link is not found.
		/// </summary>
		public NameValueCollection GetTeleportLink(string teleportName)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE name LIKE '" + teleportName + "' LIMIT 1");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Gets the teleport link data. Return null if link is not found.
		/// </summary>
		public NameValueCollection GetTeleportLink(string sourceTeleportName, string destinationTeleportName)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE name LIKE '" + sourceTeleportName + "' AND destination LIKE '" + destinationTeleportName + "' LIMIT 1");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Gets all links from the teleport specified.
		/// </summary>
		public List<NameValueCollection> GetTeleportLinks(uint sourceTeleportID)
		{
			return Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE id="+ sourceTeleportID);
    }
		/// <summary>
		/// Gets the teleport destination ID. Return 0 if none is found.
		/// </summary>
		public uint GetTeleportLinkDestinationID(uint sourceTeleportID, uint destinationTeleportID)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT t1.destinationid FROM teleportlinks AS t1 JOIN teleportlinks AS t2 WHERE t1.destination=t2.name AND t2.destination=t1.name AND t1.id=" + sourceTeleportID + " AND t2.id=" + destinationTeleportID+" LIMIT 1");
			if (result.Count > 0)
			{
				return uint.Parse(result[0]["destinationid"]);
			}
			return 0;
		}
		/// <summary>
		/// Gets the next skill ID from the skill to update, returns 0 if none is found.
		/// </summary>
		public uint GetCommonAttack(Types.Weapon type)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT id FROM skills WHERE (weapon_first = " + ((byte)type) + " or weapon_second = " + ((byte)type) + ") and group_name LIKE '%_BASE'");
			if (result.Count > 0)
			{
				return uint.Parse(result[0]["id"]);
			}
			return 0;
		}
		#endregion
	}
}
