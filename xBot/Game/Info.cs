using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using xBot.Game.Objects;

namespace xBot.Game
{
	/// <summary>
	/// Keep tracking of everything about the Silkroad actually selected.
	/// </summary>
	public class Info
	{
		private static Info _this = null;
		/// <summary>
		/// Unique name from the Silkroad.
		/// </summary>
		public string Silkroad {
			get {
				if (db != null)
					return db.Name;
				return "";
			}
		}
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
		public Database Database { get { return db; } }
		private Database db;
		/// <summary>
		/// The current path to the SR_Client.
		/// </summary>
		public string ClientPath { get; set; }
		/// <summary>
		/// Reference to the selected character for playing.
		/// </summary>
		public SRObject Character { get; set; }
		/// <summary>
		/// Gets all pets from character.
		/// </summary>
		public Dictionary<uint, SRObject> Pets { get; }
		/// <summary>
		/// Gets all pets from character.
		/// </summary>
		public Dictionary<string, SRObject> PlayersNear { get; }
		/// <summary>
		/// Gets all entity that spawn closer.
		/// </summary>
		public Dictionary<uint,SRObject> EntityList { get; }
		/// <summary>
		/// Gets all party members. The master will be at the first position.
		/// </summary>
		public List<SRObject> PartyList { get; }
		/// <summary>
		/// SROTimestamp.
		/// </summary>
		public uint ServerTime {
			get {
				return _ServerTime;
      }
			set {
				_ServerTimeDate = DateTime.UtcNow;
				_ServerTime = value;
      }
		}
		private uint _ServerTime;
    private DateTime _ServerTimeDate;
		/// <summary>
		/// Graphic reference used to display the moon.
		/// </summary>
		public ushort Moonphase { get; set; }
		/// <summary>
		/// Graphic reference to display day/night times.
		/// </summary>
		public byte Hour { get; set; }
		/// <summary>
		/// Graphic reference to display day/night times.
		/// </summary>
		public byte Minute { get; set; }
		/// <summary>
		/// Graphic reference to display wheater.
		/// </summary>
		public byte WheaterType { get; set; }
		/// <summary>
		/// Graphic reference to display wheater.
		/// </summary>
		public byte WheaterIntensity { get; set; }
		private Info()
		{
			Character = null;
			Pets = new Dictionary<uint, SRObject>();
			PlayersNear = new Dictionary<string, SRObject>();
			EntityList = new Dictionary<uint, SRObject>();
			PartyList = new List<SRObject>();
			db = null;
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
		/// Wheater time to display graphics. Returns format "00:00"
		/// </summary>
		public string GetWheaterTime()
		{
			return Hour.ToString().PadLeft(2,'0')+":"+Minute.ToString().PadLeft(2, '0');
		}
		/// <summary>
		/// Moonphase to display graphics.
		/// </summary>
		public string GetDayTime()
		{
			return Moonphase.ToString();
		}
		/// <summary>
		/// Wheater to display graphics.
		/// </summary>
		public string GetWheater()
		{
			int intensity = (WheaterIntensity * 100 / 255);
			return ((Types.Wheater)WheaterType).ToString() + " (" + intensity + "%)";
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
				DateTime time = new DateTime(year, month, day,hour,minute,second);
				// Sync time lapsed from last time saved
				time = time.Add(DateTime.UtcNow.Subtract(_ServerTimeDate));
				return time.ToString("HH:mm:ss | dd/MM/yyyy");
			}
			return "??:??:?? | ??/??/????";
		}
		/// <summary>
		/// Select the database if exists. Return success.
		/// </summary>
		/// <param name="name">Database unique name</param>
		/// <returns></returns>
		public bool SelectDatabase(string name)
		{
			if (Database.Exists(name))
			{
				db = new Database(name);
				return db.Connect();
			}
			return false;
		}
		/// <summary>
		/// Gets the maximum exp required for the level specified.
		/// </summary>
		public ulong GetExpMax(byte level)
		{
			string sql = "SELECT player FROM leveldata WHERE level=" + level;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if(result.Count > 0)
				return ulong.Parse(result[0]["player"]);
			return 0;
		}
		/// <summary>
		/// Gets the maximum exp required for the level specified.
		/// </summary>
		public ulong GetPetExpMax(byte level)
		{
			string sql = "SELECT pet FROM leveldata WHERE level=" + level;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return ulong.Parse(result[0]["pet"]);
			return 0;
		}
		/// <summary>
		/// Gets the maximum exp required for the job level specified.
		/// </summary>
		/// <param name="level">Job level</param>
		/// <param name="type">Trader, Thief or Hunter</param>
		public uint GetJobExpMax(byte level,Types.Job type)
		{
			if(type == Types.Job.None)
				return 0;
			string sql = "SELECT * FROM leveldata WHERE level=" + level;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return uint.Parse(result[0][type.ToString().ToLower()]);
			return 0;
		}
		/// <summary>
		/// Get model by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetModel(uint id)
		{
			string sql = "SELECT * FROM models WHERE id=" + id;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get model by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetModel(string servername)
		{
			string sql = "SELECT * FROM models WHERE servername='" + servername+"'";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleport(uint id)
		{
			string sql = "SELECT * FROM teleportbuildings WHERE id=" + id;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleport(string servername)
		{
			string sql = "SELECT * FROM teleportbuildings WHERE servername='" + servername+"'";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleportLink(uint id)
		{
			string sql = "SELECT * FROM teleportlinks WHERE id=" + id;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetTeleportLink(string servername)
		{
			string sql = "SELECT * FROM teleportlinks WHERE servername='" + servername + "'";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get item by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetItem(uint id)
		{
			string sql = "SELECT * FROM items WHERE id=" + id;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get item by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetItem(string servername)
		{
			string sql = "SELECT * FROM items WHERE servername='" + servername+"'";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get skill by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetSkill(uint id)
		{
			string sql = "SELECT * FROM skills WHERE id=" + id;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get skill by servername, using the current database loaded.
		/// </summary>
		public NameValueCollection GetSkill(string servername)
		{
			string sql = "SELECT * FROM skills WHERE servername='" + servername + "'";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get mastery by id, using the current database loaded.
		/// </summary>
		public NameValueCollection GetMastery(uint id)
		{
			string sql = "SELECT * FROM masteries WHERE id=" + id;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get region name by id, using the current database loaded.
		/// </summary>
		public string GetRegion(ushort id)
		{
			string sql = "SELECT name FROM regions WHERE id=" + id + " LIMIT 1";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return result[0]["name"];
			return "";
		}
		/// <summary>
		/// Gets the UIIT text already formated with params provided. returns an empty string if the servername is not found.
		/// </summary>
		public string GetUIFormat(string servername, params object[] args)
		{
			string sql = "SELECT text FROM textuisystem WHERE servername='" + servername + "'";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
				return string.Format(result[0]["text"], args);
			return "";
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
			SRObject entity = null;
			if (EntityList.TryGetValue(uniqueID, out entity) // Entity near
				|| Pets.TryGetValue(uniqueID,out entity)) // Pet near
				return entity;
			return null;
		}
		/// <summary>
		/// Returns all near players.
		/// </summary>
		public List<SRObject> GetPlayers()
		{
			return new List<SRObject>(PlayersNear.Values);
		}
		/// <summary>
		/// Returns all summoned pets players.
		/// </summary>
		public List<SRObject> GetPets()
		{
			return new List<SRObject>(Pets.Values);
		}
		/// <summary>
		/// Get's a party member by his joinID. Returns null if is not found.
		/// </summary>
		public SRObject GetPartyMember(uint joinID)
		{
			return PartyList.Find(member => (uint)member[SRProperty.JoinID] == joinID);
		}
		/// <summary>
		/// Find a party member by his nickname. Returns null if is not found.
		/// </summary>
		public SRObject GetPartyMember(string name)
		{
			return PartyList.Find(member => (name.Equals(member.Name,StringComparison.OrdinalIgnoreCase)));
		}
		/// <summary>
		/// Get's an item object from the shop at slot specified.
		/// </summary>
		public SRObject GetItemFromShop(string npc_servername,byte tabNumber, byte tabSlot)
		{
			string sql = "SELECT * FROM shops WHERE model_servername='"+ npc_servername + "' AND tab="+ tabNumber+ " AND slot="+tabSlot;
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
			{
				SRObject item = new SRObject(result[0]["item_servername"],SRType.Item);
        if (item.ID1 == 3 && item.ID2 == 1)
				{
					item[SRProperty.Plus] = byte.Parse(result[0]["plus"]);
					item[SRProperty.Durability] = uint.Parse(result[0]["durability"]);
				}
				if(result[0]["magic_params"] != "0")
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
			string sql = "SELECT * FROM skills WHERE group_name='" + skill[SRProperty.GroupName] + "' AND level<" + skill[SRProperty.Level]+" ORDER BY level DESC LIMIT 1";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0){
				return uint.Parse(result[0]["id"]);
			}
			return 0;
		}
		/// <summary>
		/// Gets the next skill ID from the skill to update, returns 0 if none is found.
		/// </summary>
		public uint GetNextSkillID(SRObject skill)
		{
			string sql = "SELECT * FROM skills WHERE group_name='" + skill[SRProperty.GroupName] + "' AND level>" + skill[SRProperty.Level] + " ORDER BY level LIMIT 1";
			Database.ExecuteQuery(sql);
			List<NameValueCollection> result = Database.GetResult();
			if (result.Count > 0)
			{
				return uint.Parse(result[0]["id"]);
			}
			return 0;
		}
	}
}
