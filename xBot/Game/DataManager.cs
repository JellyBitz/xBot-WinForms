using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using xBot.App;
using xBot.PK2Extractor;
using xBot.Game.Objects;
using xBot.Game.Objects.Guild;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Item;
using xBot.Game.Objects.Common;

namespace xBot.Game
{
	public static class DataManager
	{
		#region (Properties)
		/// <summary>
		/// The current path to the SR_Client.
		/// </summary>
		public static string ClientPath { get; set; }
		/// <summary>
		/// Unique name from the Silkroad.
		/// </summary>
		public static string SilkroadName { get; private set; }
		/// <summary>
		/// Silkroad Locale
		/// </summary>
		public static byte Locale { get; set; }
		/// <summary>
		/// SR_Client name
		/// </summary>
		public static string SR_Client { get; set; }
		/// <summary>
		/// Silkroad Version
		/// </summary>
		public static uint Version { get; set; }
		/// <summary>
		/// Gets the database previouly selected.
		/// </summary>
		private static SQLDatabase Database { get; set; }
		#endregion

		/// <summary>
		/// Select the database if exists. Return success.
		/// </summary>
		/// <param name="name">Database unique name</param>
		public static bool ConnectToDatabase(string SilkroadName)
		{
			if (Pk2Extractor.DirectoryExists(SilkroadName))
			{
				Database = new SQLDatabase(Pk2Extractor.GetDatabasePath(SilkroadName));
				bool connected = Database.Connect();
				if (connected)
					DataManager.SilkroadName = SilkroadName;
				return connected;
			}
			return false;
		}
		public static void DisconnectDatabase()
		{
			Database.Close();
		}

		#region Gets from Database
			/// <summary>
			/// Gets the maximum exp required for the level specified.
			/// </summary>
		public static ulong GetExpMax(byte level)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT player FROM leveldata WHERE level=" + level);
      if (result.Count > 0)
				return ulong.Parse(result[0]["player"]);
			return 0;
		}
		/// <summary>
		/// Gets the maximum exp required for the level specified.
		/// </summary>
		public static ulong GetPetExpMax(byte level)
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
		public static uint GetJobExpMax(byte level, SRPlayer.Job type)
		{
			if (type == SRPlayer.Job.None)
				return 0;
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM leveldata WHERE level=" + level);
      if (result.Count > 0)
				return uint.Parse(result[0][type.ToString().ToLower()]);
			return 0;
		}
		/// <summary>
		/// Get model by id, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetModelData(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM models WHERE id=" + id);
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get model by servername, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetModelData(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM models WHERE servername='" + servername + "'");
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by id, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetTeleport(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportbuildings WHERE id=" + id);
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by servername, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetTeleport(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportbuildings WHERE servername='" + servername + "'");
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by id, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetTeleportLinkByID(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE id=" + id);
      if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get teleportlink by servername, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetTeleportLinkByServerName(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get item by id, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetItemData(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM items WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get item by servername, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetItemData(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM items WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get an magic option from items, by id.
		/// </summary>
		public static NameValueCollection GetMagicOption(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM magicoptions WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get an magic option from items, by servername.
		/// </summary>
		public static NameValueCollection GetMagicOption(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM magicoptions WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get skill by id, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetSkillData(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM skills WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get skill by servername, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetSkillData(string servername)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM skills WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get mastery by id, using the current database loaded.
		/// </summary>
		public static NameValueCollection GetMastery(uint id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM masteries WHERE id=" + id);
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Get region name by id, using the current database loaded.
		/// </summary>
		public static string GetRegion(ushort id)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT name FROM regions WHERE id=" + id + " LIMIT 1");
			if (result.Count > 0)
				return result[0]["name"];
			return "";
		}
		/// <summary>
		/// Gets the UIIT text already formated with params provided. returns an empty string if the servername is not found.
		/// </summary>
		public static string GetUIFormat(string servername, params object[] args)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT text FROM textuisystem WHERE servername='" + servername + "'");
			if (result.Count > 0)
				return string.Format(result[0]["text"], args);
			return "";
		}
		/// <summary>
		/// Get's an item object from the shop at slot specified.
		/// </summary>
		public static SRItem GetItemFromShop(string npc_servername, byte tabNumber, byte tabSlot)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM shops WHERE model_servername='" + npc_servername + "' AND tab=" + tabNumber + " AND slot=" + tabSlot);
			if (result.Count > 0)
			{
				SRItem item = SRItem.Create(result[0]["item_servername"],new SRRentable(0));
				if (item.isEquipable())
				{
					SREquipable equip = (SREquipable)item;
					equip.Plus = byte.Parse(result[0]["plus"]);
					equip.Durability = uint.Parse(result[0]["durability"]);
					// Add magic options
					if (result[0]["magic_params"] != "")
					{
						string[] mParams = result[0]["magic_params"].Split('|');
						xList<SRMagicOption> magicOptions = new xList<SRMagicOption>(mParams.Length);
						for (byte j = 0; j < mParams.Length; j++)
						{
							ulong param = ulong.Parse(mParams[j]);
							magicOptions[j] = new SRMagicOption((uint)(param & uint.MaxValue));
							magicOptions[j].Value = (uint)(param >> 32);
						}
						equip.MagicOptions = magicOptions;
					}
					else
					{
						equip.MagicOptions = new xList<SRMagicOption>();
          }
				}
				return item;
			}
			return null;
		}
		/// <summary>
		/// Gets the last skill ID from the skill updated, returns 0 if none is found.
		/// </summary>
		public static uint GetLastSkillID(SRSkill skill)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT id FROM skills WHERE group_id='" + skill.GroupID + "' AND level<" + skill.Level + " ORDER BY level DESC LIMIT 1");
			if (result.Count > 0)
				return uint.Parse(result[0]["id"]);
			return 0;
		}
		/// <summary>
		/// Gets the next skill ID from the skill to update, returns 0 if none is found.
		/// </summary>
		public static uint GetNextSkillID(SRSkill skill)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT id FROM skills WHERE group_id='" + skill.GroupID + "' AND level>" + skill.Level + " ORDER BY level LIMIT 1");
			if (result.Count > 0)
				return uint.Parse(result[0]["id"]);
			return 0;
		}
		/// <summary>
		/// Gets the teleport link data. Return null if link is not found.
		/// </summary>
		public static NameValueCollection GetTeleportLink(string teleportName)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE name LIKE '" + teleportName + "' LIMIT 1");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Gets the teleport link data. Return null if link is not found.
		/// </summary>
		public static NameValueCollection GetTeleportLink(string sourceTeleportName, string destinationTeleportName)
		{
			List<NameValueCollection> result = Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE name LIKE '" + sourceTeleportName + "' AND destination LIKE '" + destinationTeleportName + "' LIMIT 1");
			if (result.Count > 0)
				return result[0];
			return null;
		}
		/// <summary>
		/// Gets all links from the teleport specified.
		/// </summary>
		public static List<NameValueCollection> GetTeleportLinks(uint sourceTeleportID)
		{
			return Database.GetResultFromQuery("SELECT * FROM teleportlinks WHERE id="+ sourceTeleportID);
    }
		/// <summary>
		/// Gets the teleport destination ID. Return 0 if none is found.
		/// </summary>
		public static uint GetTeleportLinkDestinationID(uint sourceTeleportID, uint destinationTeleportID)
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
		public static uint GetCommonAttack(SRTypes.Weapon type)
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
