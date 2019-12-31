using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using xBot.App;
using xBot.Game;

namespace xBot.PK2Extractor
{
	partial class Pk2Extractor
	{
		/// <summary>
		/// Skill data description from positions
		/// </summary>
		private static class DSKILL
		{
			public const byte
			Service = 0,
			ID = 1,
			GroupID = 2,
			Basic_Code = 3,
			Basic_Name = 4,
			Basic_Group = 5,
			Basic_Original = 6,
			Basic_Level = 7,
			Basic_Activity = 8,
			Basic_ChainCode = 9,
			Basic_RecycleCost = 10,
			Action_PreparingTime = 11,
			Action_CastingTime = 12,
			Action_ActionDuration = 13,
			Action_ReuseDelay = 14,
			Action_CoolTime = 15,
			Action_FlyingSpeed = 16,
			Action_Interruptable = 17,
			Action_Overlap = 18,
			Action_AutoAttackType = 19,
			Action_InTown = 20,
			Action_Range = 21,
			Target_Required = 22,
			TargetType_Animal = 23,
			TargetType_Land = 24,
			TargetType_Building = 25,
			TargetGroup_Self = 26,
			TargetGroup_Ally = 27,
			TargetGroup_Party = 28,
			TargetGroup_Enemy_M = 29,
			TargetGroup_Enemy_P = 30,
			TargetGroup_Neutral = 31,
			TargetGroup_DontCare = 32,
			TargetEtc_SelectDeadBody = 33,
			ReqCommon_Mastery1 = 34,
			ReqCommon_Mastery2 = 35,
			ReqCommon_MasteryLevel1 = 36,
			ReqCommon_MasteryLevel2 = 37,
			ReqCommon_Str = 38,
			ReqCommon_Int = 39,
			ReqLearn_Skill1 = 40,
			ReqLearn_Skill2 = 41,
			ReqLearn_Skill3 = 42,
			ReqLearn_SkillLevel1 = 43,
			ReqLearn_SkillLevel2 = 44,
			ReqLearn_SkillLevel3 = 45,
			ReqLearn_SP = 46,
			ReqLearn_Race = 47,
			Req_Restriction1 = 48,
			Req_Restriction2 = 49,
			ReqCast_Weapon1 = 50,
			ReqCast_Weapon2 = 51,
			Consume_HP = 52,
			Consume_MP = 53,
			Consume_HPRatio = 54,
			Consume_MPRatio = 55,
			Consume_WHAN = 56,
			UI_SkillTab = 57,
			UI_SkillPage = 58,
			UI_SkillColumn = 59,
			UI_SkillRow = 60,
			UI_IconFile = 61,
			UI_SkillName = 62,
			UI_SkillToolTip = 63,
			UI_SkillToolTip_Desc = 64,
			UI_SkillStudy_Desc = 65,
			AI_AttackChance = 66,
			AI_SkillType = 67,
			Param1 = 68,
			Param2 = 69,
			Param3 = 70,
			Param4 = 71,
			Param5 = 72,
			Param6 = 73,
			Param7 = 74,
			Param8 = 75,
			Param9 = 76,
			Param10 = 77,
			Param11 = 78,
			Param12 = 79,
			Param13 = 80,
			Param14 = 81,
			Param15 = 82,
			Param16 = 83,
			Param17 = 84,
			Param18 = 85,
			Param19 = 86,
			Param20 = 87,
			Param21 = 88,
			Param22 = 89,
			Param23 = 90,
			Param24 = 91,
			Param25 = 92,
			Param26 = 93,
			Param27 = 94,
			Param28 = 95,
			Param29 = 96,
			Param30 = 97,
			Param31 = 98,
			Param32 = 99,
			Param33 = 100,
			Param34 = 101,
			Param35 = 102,
			Param36 = 103,
			Param37 = 104,
			Param38 = 105,
			Param39 = 106,
			Param40 = 107,
			Param41 = 108,
			Param42 = 109,
			Param43 = 110,
			Param44 = 111,
			Param45 = 112,
			Param46 = 113,
			Param47 = 114,
			Param48 = 115,
			Param49 = 116,
			Param50 = 117;
		}
		/// <summary>
		/// Keep all name references from the game.
		/// </summary>
		private Dictionary<string, string> NameReferences;
		private Dictionary<string, string> TextReferences;
		/// <summary>
		///  Switch the language detected.
		/// </summary>
		private byte LanguageIndex = 8;
		/// <summary>
		/// Keep all teleport data used for linking.
		/// </summary>
		private Dictionary<string, string[]> TeleportData;
		private Dictionary<string, string[]> TeleportBuildings;
		// Variables frequently used.
		readonly char[] pk2_split = new char[] { '\t' };
		const string pk2_lineSplitN = "\n";
		const string pk2_lineSplit = "\r\n";
		const string pk2_lineEnabled = "1\t";

		string[] data;
		string line;
		private void SetLanguageIndex()
		{
			string[] lines = pk2.GetFileText("type.txt").Split(new string[] { pk2_lineSplitN },StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Contains("Language"))
				{
					string type = lines[i].Split('=')[1].Replace("\"", "").Trim();
					switch (type)
					{
						case "English":
							LanguageIndex = 8;
							break;
						case "Vietnam":
							LanguageIndex = 9;
							break;
						case "Russia":
							LanguageIndex = 10;
							break;
						default:
							LanguageIndex = 8;
							break;
					}
					Log("Selecting " + type + " (" + LanguageIndex + ") as packet language");
					return;
				}
			}
		}
		private void LoadNameReferences()
		{
			NameReferences = new Dictionary<string, string>();
			// short file, load all lines to memory
			string[] files = pk2.GetFileText("server_dep\\silkroad\\textdata\\TextDataName.txt").Split(new string[] { pk2_lineSplit }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string file in files)
			{
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\"+file)))
				{
					while (!reader.EndOfStream)
					{
						if ((line = reader.ReadLine()) == null)
							continue;

						// Data enabled in game
						if (line.StartsWith(pk2_lineEnabled))
						{
							data = line.Split(pk2_split, StringSplitOptions.None);

							// 10% display
							if (rand.Next(1, 1000) <= 100)
								LogState("Loading " + data[1]);

							if (data.Length > LanguageIndex && data[LanguageIndex] != "0")
								NameReferences[data[1]] = data[LanguageIndex];
						}
					}
				}
			}
		}
		private string GetNameReference(string ServerName)
		{
			if (NameReferences.ContainsKey(ServerName))
				return NameReferences[ServerName];
			return "";
		}
		private void LoadTextReferences()
		{
			TextReferences = new Dictionary<string, string>();
			// vars constantly used

			string text;
			string[] c_formats = new string[] { "%d", "%s", "%ld", "%u", "%08x", "%I64d", "%l64d" };
			int formatIndex, formatCount;

			// Keep memory safe
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\TextUISystem.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine(pk2_lineSplit)) == null)
						continue;

					// Data enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 10% display
						if (rand.Next(1, 1000) <= 100)
							LogState("Loading " + data[1]);

						if (data.Length > LanguageIndex && data[LanguageIndex] != "0")
						{
							text = data[LanguageIndex];
							formatCount = 0;
							// Convert from C++ to C# format
							for (byte i = 0; i < c_formats.Length; i++)
							{
								while ((formatIndex = text.IndexOf(c_formats[i])) != -1)
								{
									text = text.Remove(formatIndex) + "{" + formatCount + "}" + text.Substring(formatIndex + c_formats[i].Length);
									formatCount++;
								}
							}
							TextReferences[data[1]] = text;
						}
					}
				}
			}
		}
		private string GetTextReference(string TextName)
		{
			if (TextReferences.ContainsKey(TextName))
				return TextReferences[TextName];
			return "";
		}
		public void AddTextReferences()
		{
			string sql = "CREATE TABLE textuisystem (";
			sql += "fakeID INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64) UNIQUE,";
			sql += "text VARCHAR(256)";
			sql += ");";
			db.ExecuteQuery(sql);

			// using faster sqlite performance
			db.Begin();
			int j = 0;
			foreach (string key in TextReferences.Keys)
			{
				// 10% display
				if (rand.Next(1, 1000) <= 100)
					LogState("Adding " + key);
				// INSERT
				db.Prepare("INSERT INTO textuisystem (fakeID,servername,text) VALUES (?,?,?);");
				db.Bind("fakeID", j++);
				db.Bind("servername", key);
				db.Bind("text", TextReferences[key]);
				db.ExecuteQuery();
			}
			db.End();
		}
		private void AddItems()
		{
			string sql = "CREATE TABLE items (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64),";
			sql += "name VARCHAR(64),";
			sql += "stack INTEGER,";
			sql += "tid2 INTEGER,";
			sql += "tid3 INTEGER,";
			sql += "tid4 INTEGER,";
			sql += "level INTEGER,";
			sql += "icon VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			string name;

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("server_dep\\silkroad\\textdata\\ItemData.txt").Split(new string[] { pk2_lineSplit }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\" + files[i])))
				{
					// using faster sqlite performance
					db.Begin();

					while (!reader.EndOfStream)
					{
						if ((line = reader.ReadLine()) == null)
							continue;

						// Data is enabled in game
						if (line.StartsWith(pk2_lineEnabled))
						{
							data = line.Split(pk2_split, StringSplitOptions.None);
							// Extract name if has one

							name = "";
							if (data[5] != "xxx")
								name = GetNameReference(data[5]);

							// 15% display
							if (rand.Next(1, 1000) <= 150)
								LogState("Adding " + data[2]);
							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT id FROM items WHERE id=" + data[1]);
							if (db.GetResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO items (id,servername,name,stack,tid2,tid3,tid4,level,icon) VALUES (?,?,?,?,?,?,?,?,?);");
								db.Bind("id", data[1]);
							}
							else
							{
								// Override
								db.Prepare("UPDATE items SET servername=?,name=?,stack=?,tid2=?,tid3=?,tid4=?,level=?,icon=? WHERE id=" + data[1]);
							}
							db.Bind("servername", data[2]);
							db.Bind("name", name);
							db.Bind("stack", data[57]);
							db.Bind("tid2", data[10]);
							db.Bind("tid3", data[11]);
							db.Bind("tid4", data[12]);
							db.Bind("level", data[33]);
							// Normal data has 160 positions approx. 
							db.Bind("icon", (data.Length > 150 ? data[54] : data[50]).ToLower() );
							db.ExecuteQuery();
						}
					}
					db.End();
				}
			}
		}
		public void AddMagicOptions()
		{
			string sql = "CREATE TABLE magicoptions (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64),";
			sql += "name VARCHAR(64),";
			sql += "degree INTEGER,";
			sql += "value FLOAT,";
			sql += "maxvalue INTEGER,";
			sql += "increase BOOLEAN,";
			sql += "usage VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			string name;
			List<string> items = new List<string>();

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\MagicOption.txt")))
			{
				// using faster sqlite performance
				db.Begin();

				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);
						switch (data[2])
						{
							case "MATTR_INT":
								name = GetTextReference("PARAM_INT");
								break;
							case "MATTR_STR":
								name = GetTextReference("PARAM_STR");
								break;
							case "MATTR_DUR":
								name = GetTextReference("PARAM_DUR");
								break;
							case "MATTR_SOLID":
								name = GetTextReference("PARAM_SOLID");
								break;
							case "MATTR_LUCK":
								name = GetTextReference("PARAM_LUCK");
								break;
							case "MATTR_ASTRAL":
								name = GetTextReference("PARAM_ASTRAL");
								break;
							case "MATTR_ATHANASIA":
								name = GetTextReference("PARAM_ATHANASIA");
								break;
							// Armor
							case "MATTR_HP":
								name = GetTextReference("PARAM_HP");
								break;
							case "MATTR_MP":
								name = GetTextReference("PARAM_MP");
								break;
							case "MATTR_ER":
								name = GetTextReference("PARAM_ER");
								break;
							// Weapons & shield
							case "MATTR_HR":
								name = GetTextReference("PARAM_HR");
								break;
							case "MATTR_EVADE_BLOCK":
								name = GetTextReference("PARAM_IGNORE_BLOCKING");
								break;
							case "MATTR_EVADE_CRITICAL":
								name = GetTextReference("PARAM_EVADE_CRITICAL");
								break;
							// Accesories
							case "MATTR_RESIST_FROSTBITE":
								name = GetTextReference("PARAM_COLD_RESIST");
								break;
							case "MATTR_RESIST_ESHOCK":
								name = GetTextReference("PARAM_ESHOCK_RESIST");
								break;
							case "MATTR_RESIST_BURN":
								name = GetTextReference("PARAM_BURN_RESIST");
								break;
							case "MATTR_RESIST_POISON":
								name = GetTextReference("PARAM_POISON_RESIST");
								break;
							case "MATTR_RESIST_ZOMBIE":
								name = GetTextReference("PARAM_ZOMBI_RESIST");
								break;
							default:
								name = "";
								break;
						}

						// Add all items assignables to the stone
						items.Clear();
						for (byte j = 30; j < data.Length; j += 2)
						{
							if (data[j] == "1")
								items.Add(data[j - 1]);
							else
								break;
						}

						// 100% display
						LogState("Adding " + data[2]);

						// INSERT
						db.Prepare("INSERT INTO magicoptions (id,servername,name,degree,value,maxvalue,increase,usage) VALUES (?,?,?,?,?,?,?,?)");
						db.Bind("id", data[1]);
						db.Bind("servername", data[2]);
						db.Bind("name", name);
						db.Bind("degree", data[4]);
						db.Bind("value", data[5]);
						db.Bind("maxvalue", int.Parse(data[10]) & ushort.MaxValue);
						db.Bind("increase", data[3] == "+");
						db.Bind("usage", string.Join("|",items));
						db.ExecuteQuery();
					}
				}
				db.End();
			}
		}
		public void AddModels()
		{
			string sql = "CREATE TABLE models (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64), ";
			sql += "name VARCHAR(64),";
			sql += "tid2 INTEGER,";
			sql += "tid3 INTEGER,";
			sql += "tid4 INTEGER,";
			sql += "hp INTEGER,";
			sql += "level INTEGER";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			string name;

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("server_dep\\silkroad\\textdata\\CharacterData.txt").Split(new string[] { pk2_lineSplit }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\"+files[i])))
				{
					// using faster sqlite performance
					db.Begin();

					while (!reader.EndOfStream)
					{
						if ((line = reader.ReadLine()) == null)
							continue;

						// Data is enabled in game
						if (line.StartsWith(pk2_lineEnabled))
						{
							data = line.Split(pk2_split, StringSplitOptions.None);
							// Extract name if has one
							name = "";
							if (data[5] != "xxx")
								name = GetNameReference(data[5]);
							if (name == "")
								name = data[2];

							// 15% display
							if (rand.Next(1, 1000) <= 150)
								LogState("Adding " + data[2]);

							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT id FROM models WHERE id=" + data[1]);
							if (db.GetResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO models (id,servername,name,tid2,tid3,tid4,hp,level) VALUES (?,?,?,?,?,?,?,?)");
								db.Bind("id", data[1]);
							}
							else
							{
								// Override
								db.Prepare("UPDATE models SET servername=?,name=?,tid2=?,tid3=?,tid4=?,hp=?,level=? WHERE id=" + data[1]);
							}
							db.Bind("servername", data[2]);
							db.Bind("name", name);
							db.Bind("tid2", data[10]);
							db.Bind("tid3", data[11]);
							db.Bind("tid4", data[12]);
							db.Bind("hp", data[59]);
							db.Bind("level", data[57]);
							db.ExecuteQuery();
						}
					}
					db.End();
				}
			}
		}
		public void AddMasteries()
		{
			string sql = "CREATE TABLE masteries (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "name VARCHAR(64),";
			sql += "description VARCHAR(256),";
			sql += "type VARCHAR(64),";
			sql += "weapons VARCHAR(12),";
			sql += "icon VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			string name, desc, type;

			// Keep memory safe
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\SkillMasteryData.txt")))
			{
				// using faster sqlite performance
				db.Begin();
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					if (!line.StartsWith("//"))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);
						// Avoid wrong data
						if (data.Length == 13 && data[2] != "xxx")
						{
							// Extract name if has one
							name = GetTextReference(data[2]);
							if (name == "")
								name = GetNameReference(data[2]);
							if (name == "")
								name = data[2];
							// Extract description if has one
							desc = GetTextReference(data[4]);
							if (desc == "")
								desc = GetNameReference(data[4]);
							if (desc == "")
								desc = data[4];
							// Extract type if has one
							type = GetTextReference(data[5]);
							if (desc == "")
								type = GetNameReference(data[5]);
							if (type == "")
								type = data[5];

							// 100% display
							LogState("Adding " + name);

							// INSERT 
							db.Prepare("INSERT INTO masteries (id,name,description,type,weapons,icon) VALUES (?,?,?,?,?,?)");
							db.Bind("id", data[0]);
							db.Bind("name", name);
							db.Bind("description", desc);
							db.Bind("type", type);
							db.Bind("weapons", data[8] + "," + data[9] + "," + data[10]);
							db.Bind("icon", data[11]);
							db.ExecuteQuery();
						}
					}
				}
				db.End();
			}
		}
		public void AddSkills()
		{
			string sql = "CREATE TABLE skills (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64),";
			sql += "name VARCHAR(64),";
			sql += "description VARCHAR(1024),";
			sql += "casttime INTEGER,";
			sql += "duration INTEGER,";
			sql += "cooldown INTEGER,";
			sql += "mana INTEGER,";
			sql += "level INTEGER,";
			sql += "mastery_id INTEGER,";
			sql += "sp INTEGER,";
			sql += "group_id INTEGER,";
			sql += "group_name VARCHAR(64),";
			sql += "skill_chain_id INTEGER,";
			sql += "weapon_first INTEGER,";
			sql += "weapon_second INTEGER,";
			sql += "target_required BOOLEAN,";
			sql += "params VARCHAR(256),";
			sql += "icon VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			string name, desc, duration;
			string[] skillparams = new string[30];

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("server_dep\\silkroad\\textdata\\SkillDataEnc.txt").Split(new string[] { pk2_lineSplit }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{

				LogState("Decoding " + files[i]);
				// Decrypt and save the file to be used as stream
				File.WriteAllBytes(SilkroadPath + "\\" + files[i] + ".tmp", DecryptSkillData(pk2.GetFileStream("server_dep\\silkroad\\textdata\\"+files[i])));

				// Keep memory safe
				using (StreamReader reader = new StreamReader(SilkroadPath+"\\"+ files[i] + ".tmp"))
				{
					// using faster sqlite performance
					db.Begin();

					while (!reader.EndOfStream)
					{
						if ((line = reader.ReadLine()) == null)
							continue;

						// Data is enabled in game
						if (line.StartsWith(pk2_lineEnabled))
						{
							data = line.Split(pk2_split, StringSplitOptions.None);
							// Extract name if has one
							name = "";
							if (data[DSKILL.UI_SkillName] != "xxx")
								name = GetNameReference(data[DSKILL.UI_SkillName]);

							// Extract description if has one
							if (data[DSKILL.UI_SkillToolTip_Desc] != "xxx")
								desc = GetNameReference(data[DSKILL.UI_SkillToolTip_Desc]);
							else
								desc = "";
							
							// Add a few params to check stuffs
							for (byte j = 0; j < skillparams.Length && j < data.Length; j++)
								skillparams[j] = data[DSKILL.Param1+j];

							// filter extraction
							switch (data[DSKILL.Param1])
							{
								case "3":
								case "10":
									// Buff
									duration = Params.ReadValue(skillparams, Params.Type.SKILL_DURATION);
									if (duration == "")
									{
										duration = "1"; // Infinite
									}
									else if (duration.StartsWith("-"))
									{
										// requires negative value fix
										duration = ((uint)int.Parse(duration)).ToString();
									}
									break;
								default:
									duration = "0";
									break;
							}

							// 10% display
							if (rand.Next(1, 1000) <= 100)
								LogState("Adding " + data[DSKILL.Basic_Code]);
							// INSERT

							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT id FROM skills WHERE id=" + data[DSKILL.ID]);
							if (db.GetResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO skills (id,servername,name,description,casttime,duration,cooldown,mana,level,mastery_id,sp,group_id,group_name,skill_chain_id,weapon_first,weapon_second,target_required,params,icon) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
								db.Bind("id", data[DSKILL.ID]);
							}
							else
							{
								// Override
								db.Prepare("UPDATE skills SET servername=?,name=?,description=?,casttime=?,duration=?,cooldown=?,mana=?,level=?,mastery_id=?,sp=?,group_id=?,group_name=?,skill_chain_id=?,weapon_first=?,weapon_second=?,target_required=?,params=?,icon=? WHERE id=" + data[DSKILL.ID]);
							}
							db.Bind("servername", data[DSKILL.Basic_Code]);
							db.Bind("name", name);
							db.Bind("description", desc);
							db.Bind("casttime", int.Parse(data[DSKILL.Action_PreparingTime])+ int.Parse(data[DSKILL.Action_CastingTime])+ int.Parse(data[DSKILL.Action_ActionDuration]));
							db.Bind("duration", duration);
							db.Bind("cooldown", data[DSKILL.Action_ReuseDelay]);
							db.Bind("mana", data[DSKILL.Consume_MP]);
							db.Bind("level", data[DSKILL.ReqCommon_MasteryLevel1]);
							db.Bind("mastery_id", data[DSKILL.ReqCommon_Mastery1]);
							db.Bind("sp", data[DSKILL.ReqLearn_SP]);
							db.Bind("group_id", data[DSKILL.GroupID]);
							db.Bind("group_name", data[DSKILL.Basic_Group]);
							db.Bind("skill_chain_id", data[DSKILL.Basic_ChainCode]);
							db.Bind("weapon_first", data[DSKILL.ReqCast_Weapon1]);
							db.Bind("weapon_second", data[DSKILL.ReqCast_Weapon2]);
							db.Bind("target_required", data[DSKILL.Target_Required]);
							db.Bind("params", string.Join("|", skillparams));
							db.Bind("icon", data[DSKILL.UI_IconFile].ToLower());
							db.ExecuteQuery();
						}
					}
					db.End();
				}

				//  Delete temporal skilldata decoded
				File.Delete(SilkroadPath+"\\"+ files[i] + ".tmp");
			}
		}
		public byte[] DecryptSkillData(Stream SkillDataEncrypted)
		{
			byte[] HashTable1 = new byte[]{
				0x07, 0x83, 0xBC, 0xEE, 0x4B, 0x79, 0x19, 0xB6, 0x2A, 0x53, 0x4F, 0x3A, 0xCF, 0x71, 0xE5, 0x3C,
				0x2D, 0x18, 0x14, 0xCB, 0xB6, 0xBC, 0xAA, 0x9A, 0x31, 0x42, 0x3A, 0x13, 0x42, 0xC9, 0x63, 0xFC,
				0x54, 0x1D, 0xF2, 0xC1, 0x8A, 0xDD, 0x1C, 0xB3, 0x52, 0xEA, 0x9B, 0xD7, 0xC4, 0xBA, 0xF8, 0x12,
				0x74, 0x92, 0x30, 0xC9, 0xD6, 0x56, 0x15, 0x52, 0x53, 0x60, 0x11, 0x33, 0xC5, 0x9D, 0x30, 0x9A,
				0xE5, 0xD2, 0x93, 0x99, 0xEB, 0xCF, 0xAA, 0x79, 0xE3, 0x78, 0x6A, 0xB9, 0x02, 0xE0, 0xCE, 0x8E,
				0xF3, 0x63, 0x5A, 0x73, 0x74, 0xF3, 0x72, 0xAA, 0x2C, 0x9F, 0xBB, 0x33, 0x91, 0xDE, 0x5F, 0x91,
				0x66, 0x48, 0xD1, 0x7A, 0xFD, 0x3F, 0x91, 0x3E, 0x5D, 0x22, 0xEC, 0xEF, 0x7C, 0xA5, 0x43, 0xC0,
				0x1D, 0x4F, 0x60, 0x7F, 0x0B, 0x4A, 0x4B, 0x2A, 0x43, 0x06, 0x46, 0x14, 0x45, 0xD0, 0xC5, 0x83,
				0x92, 0xE4, 0x16, 0xD0, 0xA3, 0xA1, 0x13, 0xDA, 0xD1, 0x51, 0x07, 0xEB, 0x7D, 0xCE, 0xA5, 0xDB,
				0x78, 0xE0, 0xC1, 0x0B, 0xE5, 0x8E, 0x1C, 0x7C, 0xB4, 0xDF, 0xED, 0xB8, 0x53, 0xBA, 0x2C, 0xB5,
				0xBB, 0x56, 0xFB, 0x68, 0x95, 0x6E, 0x65, 0x00, 0x60, 0xBA, 0xE3, 0x00, 0x01, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x9C, 0xB5, 0xD5, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2E, 0x3F, 0x41, 0x56,
				0x43, 0x45, 0x53, 0x63, 0x72, 0x69, 0x70, 0x74, 0x40, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x64, 0xBB, 0xE3, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
			byte[] HashTable2 = new byte[]{
				0x0D, 0x05, 0x90, 0x41, 0xF9, 0xD0, 0x65, 0xBF, 0xF9, 0x0B, 0x15, 0x93, 0x80, 0xFB, 0x01, 0x02,
				0xB6, 0x08, 0xC4, 0x3C, 0xC1, 0x49, 0x94, 0x4D, 0xCE, 0x1D, 0xFD, 0x69, 0xEA, 0x19, 0xC9, 0x57,
				0x9C, 0x4D, 0x84, 0x62, 0xE3, 0x67, 0xF9, 0x87, 0xF4, 0xF9, 0x93, 0xDA, 0xE5, 0x15, 0xF1, 0x4C,
				0xA4, 0xEC, 0xBC, 0xCF, 0xDD, 0xB3, 0x6F, 0x04, 0x3D, 0x70, 0x1C, 0x74, 0x21, 0x6B, 0x00, 0x71,
				0x31, 0x7F, 0x54, 0xB3, 0x72, 0x6C, 0xAA, 0x42, 0xC1, 0x78, 0x61, 0x3E, 0xD5, 0xF2, 0xE1, 0x27,
				0x36, 0x71, 0x3A, 0x25, 0x36, 0x57, 0xD1, 0xF8, 0x70, 0x86, 0xBD, 0x0E, 0x58, 0xB3, 0x76, 0x6D,
				0xC3, 0x50, 0xF6, 0x6C, 0xA0, 0x10, 0x06, 0x64, 0xA2, 0xD6, 0x2C, 0xD4, 0x27, 0x30, 0xA5, 0x36,
				0x1C, 0x1E, 0x3E, 0x58, 0x9D, 0x59, 0x76, 0x9D, 0xA7, 0x42, 0x5A, 0xF0, 0x00, 0xBC, 0x69, 0x31,
				0x40, 0x1E, 0xFA, 0x09, 0x1D, 0xE7, 0xEE, 0xE4, 0x54, 0x89, 0x36, 0x7C, 0x67, 0xC8, 0x65, 0x22,
				0x7E, 0xA3, 0x60, 0x44, 0x1E, 0xBC, 0x68, 0x6F, 0x15, 0x2A, 0xFD, 0x9D, 0x3F, 0x36, 0x6B, 0x28,
				0x06, 0x67, 0xFE, 0xC6, 0x49, 0x6B, 0x9B, 0x3F, 0x80, 0x2A, 0xD2, 0xD4, 0xD3, 0x20, 0x1B, 0x96,
				0xF4, 0xD2, 0xCA, 0x8C, 0x74, 0xEE, 0x0B, 0x6A, 0xE1, 0xE9, 0xC6, 0xD2, 0x6E, 0x33, 0x63, 0xC0,
				0xE9, 0xD0, 0x37, 0xA9, 0x3C, 0xF7, 0x18, 0xF2, 0x4A, 0x74, 0xEC, 0x41, 0x61, 0x7A, 0x19, 0x47,
				0x8F, 0xA0, 0xBB, 0x94, 0x8F, 0x3D, 0x11, 0x11, 0x26, 0xCF, 0x69, 0x18, 0x1B, 0x2C, 0x87, 0x6D,
				0xB3, 0x22, 0x6C, 0x78, 0x41, 0xCC, 0xC2, 0x84, 0xC5, 0xCB, 0x01, 0x6A, 0x37, 0x00, 0x01, 0x65,
				0x4F, 0xA7, 0x85, 0x85, 0x15, 0x59, 0x05, 0x67, 0xF2, 0x4F, 0xAB, 0xB7, 0x88, 0xFA, 0x69, 0x24,
				0x9E, 0xC6, 0x7B, 0x3F, 0xD5, 0x0E, 0x4D, 0x7B, 0xFB, 0xB1, 0x21, 0x3C, 0xB0, 0xC0, 0xCB, 0x2C,
				0xAA, 0x26, 0x8D, 0xCC, 0xDD, 0xDA, 0xC1, 0xF8, 0xCA, 0x7F, 0x6A, 0x3F, 0x2A, 0x61, 0xE7, 0x60,
				0x5C, 0xCE, 0xD3, 0x4C, 0xAC, 0x45, 0x40, 0x62, 0xEA, 0x51, 0xF1, 0x66, 0x5D, 0x2C, 0x45, 0xD6,
				0x8B, 0x7D, 0xCE, 0x9C, 0xF5, 0xBB, 0xF7, 0x52, 0x24, 0x1A, 0x13, 0x02, 0x2B, 0x00, 0xBB, 0xA1,
				0x8F, 0x6E, 0x7A, 0x33, 0xAD, 0x5F, 0xF4, 0x4A, 0x82, 0x76, 0xAB, 0xDE, 0x80, 0x98, 0x8B, 0x26,
				0x4F, 0x33, 0xD8, 0x68, 0x1E, 0xD9, 0xAE, 0x06, 0x6B, 0x7E, 0xA9, 0x95, 0x67, 0x60, 0xEB, 0xE8,
				0xD0, 0x7D, 0x07, 0x4B, 0xF1, 0xAA, 0x9A, 0xC5, 0x29, 0x93, 0x9D, 0x5C, 0x92, 0x3F, 0x15, 0xDE,
				0x48, 0xF1, 0xCA, 0xEA, 0xC9, 0x78, 0x3C, 0x28, 0x7E, 0xB0, 0x46, 0xD3, 0x71, 0x6C, 0xD7, 0xBD,
				0x2C, 0xF7, 0x25, 0x2F, 0xC7, 0xDD, 0xB4, 0x6D, 0x35, 0xBB, 0xA7, 0xDA, 0x3E, 0x3D, 0xA7, 0xCA,
				0xBD, 0x87, 0xDD, 0x9F, 0x22, 0x3D, 0x50, 0xD2, 0x30, 0xD5, 0x14, 0x5B, 0x8F, 0xF4, 0xAF, 0xAA,
				0xA0, 0xFC, 0x17, 0x3D, 0x33, 0x10, 0x99, 0xDC, 0x76, 0xA9, 0x40, 0x1B, 0x64, 0x14, 0xDF, 0x35,
				0x68, 0x66, 0x5B, 0x49, 0x05, 0x33, 0x68, 0x26, 0xC8, 0xBA, 0xD1, 0x8D, 0x39, 0x2B, 0xFB, 0x3E,
				0x24, 0x52, 0x2F, 0x9A, 0x69, 0xBC, 0xF2, 0xB2, 0xAC, 0xB8, 0xEF, 0xA1, 0x17, 0x29, 0x2D, 0xEE,
				0xF5, 0x23, 0x21, 0xEC, 0x81, 0xC7, 0x5B, 0xC0, 0x82, 0xCC, 0xD2, 0x91, 0x9D, 0x29, 0x93, 0x0C,
				0x9D, 0x5D, 0x57, 0xAD, 0xD4, 0xC6, 0x40, 0x93, 0x8D, 0xE9, 0xD3, 0x35, 0x9D, 0xC6, 0xD3, 0x00
			};
			uint key = 0x8C1F;
			bool encrypted = false;

			// Data decoded
			byte[] buffer = new byte[SkillDataEncrypted.Length + 1];
			SkillDataEncrypted.Read(buffer, 0, 2);
			SkillDataEncrypted.Seek(0, SeekOrigin.Begin);

			// Check if the data is truly encoded
			if (buffer[0] == 0xE2 && buffer[1] == 0xB0)
				encrypted = true;

			if (encrypted)
			{
				byte buff;
				for (int i = 0; i <= SkillDataEncrypted.Length; i++)
				{
					buff = (byte)(HashTable1[key % 0xA7] - HashTable2[key % 0x1Ef]);
					key++;
					buffer[i] = (byte)(SkillDataEncrypted.ReadByte() + buff);
				}
			}
			else
			{
				SkillDataEncrypted.Read(buffer, 0, (int)SkillDataEncrypted.Length);
			}
			return buffer;
		}
		private void AddLevelExperience()
		{
			string sql = "CREATE TABLE leveldata (";
			sql += "level INTEGER PRIMARY KEY,";
			sql += "player INTEGER,";
			sql += "sp INTEGER,";
			sql += "pet INTEGER,";
			sql += "trader INTEGER,";
			sql += "thief INTEGER,";
			sql += "hunter INTEGER";
			sql += ");";
			db.ExecuteQuery(sql);

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\LevelData.txt")))
			{
				// using faster sqlite performance
				db.Begin();

				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					if (!line.StartsWith("//"))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 100% display
						LogState("Adding Lv." + data[0]);
						db.Prepare("INSERT INTO leveldata (level,player,sp,pet,trader,thief,hunter) VALUES (?,?,?,?,?,?,?)");
						db.Bind("level", data[0]);
						db.Bind("player", data[1]);
						db.Bind("sp", data[2]);
						db.Bind("pet", data[5]);
						db.Bind("trader", data[6] == "-1" ? "0" : data[6]); // safe ulong casting
						db.Bind("thief", data[7] == "-1" ? "0" : data[7]);
						db.Bind("hunter", data[8] == "-1" ? "0" : data[8]);
						db.ExecuteQuery();
					}
				}
				db.End();
			}
		}
		private void AddShops()
		{
			List<Shop> shops = new List<Shop>();

			LogState("Loading refShopGroup.txt");
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\refShopGroup.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						Shop shop = new Shop();
						if (data[3].StartsWith("GROUP_MALL_"))
							shop.StoreGroupName = data[3].Substring(6);
						else
							shop.StoreGroupName = data[3];
						shop.NPCName = data[4];
						shops.Add(shop);
					}
				}
			}
			LogState("Loading refMappingShopGroup.txt");
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\refMappingShopGroup.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;
					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						foreach (Shop shop in shops)
						{
							if (shop.StoreGroupName.StartsWith("MALL"))
							{
								if (shop.StoreGroupName == data[3])
								{
									shop.StoreName = data[3];
									shop.StoreGroupName = data[2];
								}
							}
							else if (shop.StoreGroupName == data[2])
							{
								shop.StoreName = data[3];
							}
						}
					}
				}
			}
			LogState("Loading refMappingShopWithTab.txt");
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\refMappingShopWithTab.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;
					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						foreach (Shop shop in shops)
						{
							if (shop.StoreName == data[2])
							{
								Shop.Group group = new Shop.Group();
								group.Name = data[3];
								shop.Groups.Add(group);
							}
						}
					}
				}
			}
			LogState("Loading refShopTab.txt");
			List<string[]> refShopTab = new List<string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\refShopTab.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;
					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 0 = name, 1 = group, 2 = title
						refShopTab.Add(new string[] { data[3], data[4], data[5] });
					}
				}
			}
			foreach (Shop shop in shops)
			{
				foreach (Shop.Group group in shop.Groups)
				{
					for (int j = 0; j < refShopTab.Count; j++)
					{
						if (group.Name == refShopTab[j][1])
						{
							Shop.Group.Tab tab = new Shop.Group.Tab();
							tab.Name = refShopTab[j][0];
							tab.Title = GetTextReference(refShopTab[j][2]);
							group.Tabs.Add(tab);
						}
					}
				}
			}
			LogState("Loading refShopGoods.txt");
			List<string[]> refShopGoods = new List<string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\refShopGoods.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 0 = tab, 1 = itemPackageName, 2 = tabSlot
						refShopGoods.Add(new string[] { data[2], data[3], data[4] });
					}
				}
			}
			LogState("Loading refScrapOfPackageItem.txt");
      Dictionary<string, string[]> refScrapOfPackageItem = new Dictionary<string, string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\refScrapOfPackageItem.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);
						// Extract Magic options
						string[] magicOptions = new string[byte.Parse(data[7])];
						for (byte j = 0; j < magicOptions.Length; j++)
							magicOptions[j] = data[j + 8];

						// 0 = itemServerName, 1 = plus, 2 = durability or buyStack (ID's behaviour), 3 = MagicParams
						refScrapOfPackageItem[data[2]] = new string[] { data[3], data[4], data[6], string.Join("|",magicOptions) };
					}
				}
			}
			LogState("Generating store items...");
			// Finally add items to shops
			foreach (Shop shop in shops)
			{
				foreach (Shop.Group group in shop.Groups)
				{
					foreach (Shop.Group.Tab tab in group.Tabs)
					{
						for (int j = 0; j < refShopGoods.Count; j++)
						{
							if (tab.Name == refShopGoods[j][0])
							{
								string[] _refScrapOfPackageItem;
								if (refScrapOfPackageItem.TryGetValue(refShopGoods[j][1], out _refScrapOfPackageItem))
								{
									// Create item image
									Shop.Group.Tab.Item item = new Shop.Group.Tab.Item();
									item.Name = _refScrapOfPackageItem[0];
									item.Slot = refShopGoods[j][2];
									item.Plus = _refScrapOfPackageItem[1];
									item.Durability = _refScrapOfPackageItem[2];
									item.MagicParams = _refScrapOfPackageItem[3];
									tab.Items.Add(item);
								}
							}
						}
					}
				}
			}

			// Adding to database
			string sql = "CREATE TABLE shops (";
			sql += "model_servername VARCHAR(64),";
			sql += "tab INTEGER,";
			sql += "slot INTEGER,";
			sql += "item_servername VARCHAR(64),";
			sql += "plus INTEGER,";
			sql += "durability INTEGER,";
			sql += "magic_params VARCHAR(256),";
			sql += "PRIMARY KEY (model_servername,tab,slot)";
			sql += ");";
			db.ExecuteQuery(sql);

			// using faster sqlite performance
			db.Begin();
			foreach (Shop shop in shops)
			{
				int tabCount = 0;
				for (int g = 0; g < shop.Groups.Count; g++)
				{
					for (int t = 0; t < shop.Groups[g].Tabs.Count; t++)
					{
						for (int i = 0; i < shop.Groups[g].Tabs[t].Items.Count; i++)
						{
							Shop.Group.Tab.Item item = shop.Groups[g].Tabs[t].Items[i];

							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT * FROM shops WHERE model_servername='" + shop.NPCName + "' AND tab=" + tabCount + " AND slot=" + i);
							if (db.GetResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO shops (model_servername,tab,slot,item_servername,plus,durability,magic_params) VALUES (?,?,?,?,?,?,?)");
								db.Bind("model_servername", shop.NPCName);
								db.Bind("tab", tabCount);
								db.Bind("slot", i);
							}
							else
							{
								// Override
								db.Prepare("UPDATE shops SET item_servername=?,plus=?,durability=?,magic_params=? WHERE model_servername='" + shop.NPCName + "' AND tab=" + tabCount + " AND slot=" + i);
							}
							db.Bind("item_servername", item.Name);
							db.Bind("plus", item.Plus);
							db.Bind("durability", item.Durability);
							db.Bind("magic_params", item.MagicParams);
							db.ExecuteQuery();
							
							// 50% display
							if (rand.Next(1, 1000) <= 500)
								LogState("Adding " + item.Name);
							
						}
						tabCount++;
					}
				}
			}
			db.End();
		}
		private class Shop
		{
			public string StoreGroupName { get; set; }
			public string StoreName { get; set; }
			public string NPCName { get; set; }
			public List<Group> Groups { get; }
			public Shop() { Groups = new List<Group>(); }
			internal class Group
			{
				public string Name { get; set; }
				public List<Tab> Tabs { get; }
				public Group() { Tabs = new List<Tab>(); }
				internal class Tab
				{
					public string Name { get; set; }
					public string Title { get; set; }
					public List<Item> Items { get; }
					public Tab() { Items = new List<Item>(); }
					internal class Item
					{
						public string Name { get; set; }
						public string Slot { get; set; }
						public string Plus { get; set; }
						public string RentType { get; set; }
						public string Durability { get; set; }
						public string MagicParams { get; set; }
					}
				}
			}
		}
		/// <summary>
		/// Used to join teleport link table (since will be required always).
		/// </summary>
		private void LoadTeleportData()
		{
			TeleportData = new Dictionary<string, string[]>();

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\TeleportData.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;
					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 80% display
						if (rand.Next(1, 1000) <= 800)
							LogState("Loading " + data[2]);
						TeleportData[data[1]] = data;
					}
				}
			}
			TeleportBuildings = new Dictionary<string, string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\TeleportBuilding.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;
					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 50% display
						if (rand.Next(1, 1000) <= 500)
							LogState("Loading " + data[2]);
						TeleportBuildings[data[1]] = data;
					}
				}
			}
		}
		public void AddTeleportBuildings()
		{
			string sql = "CREATE TABLE teleportbuildings (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64), ";
			sql += "name VARCHAR(64),";
			sql += "tid1 INTEGER,";
			sql += "tid2 INTEGER,";
			sql += "tid3 INTEGER,";
			sql += "tid4 INTEGER";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			string name;

			TeleportBuildings = new Dictionary<string, string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\TeleportBuilding.txt")))
			{
				// using faster sqlite performance
				db.Begin();

				while (!reader.EndOfStream)
				{
					if((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled in game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);
						// Extract name if has one
						name = "";
						if (data[5] != "xxx")
							name = GetNameReference(data[5]);
						if (name == "")
							name = data[2];

						// 50% display
						if (rand.Next(1, 1000) <= 500)
							LogState("Adding " + data[2]);
						// INSERT
						db.Prepare("INSERT INTO teleportbuildings (id,servername,name,tid1,tid2,tid3,tid4) VALUES(?,?,?,?,?,?,?)");
						db.Bind("id", data[1]);
						db.Bind("servername", data[2]);
						db.Bind("name", name);
						db.Bind("tid1", data[9]);
						db.Bind("tid2", data[10]);
						db.Bind("tid3", data[11]);
						db.Bind("tid4", data[12]);
						db.ExecuteQuery();
					}
				}
				db.End();
			}
		}
		public void AddTeleportLinks()
		{
			// JOIN teleports & links (since will be required all time)
			string sql = "CREATE TABLE teleportlinks (";
			sql += "sourceid INTEGER,";
			sql += "destinationid INTEGER,";
			sql += "id INTEGER,";
			sql += "servername VARCHAR(64),";
			sql += "name VARCHAR(64),";
			sql += "destination VARCHAR(64),";
			sql += "tid1 INTEGER,";
			sql += "tid2 INTEGER,";
			sql += "tid3 INTEGER,";
			sql += "tid4 INTEGER,";
			sql += "gold INTEGER,";
			sql += "level INTEGER,";
			sql += "spawn_region INTEGER,";
			sql += "spawn_x INTEGER,";
			sql += "spawn_y INTEGER,";
			sql += "spawn_z INTEGER,";
			sql += "pos_region INTEGER,";
			sql += "pos_x INTEGER,";
			sql += "pos_y INTEGER,";
			sql += "pos_z INTEGER,";
			sql += "PRIMARY KEY (sourceid, destinationid)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			string name, destination, tid1, tid2, tid3, tid4;

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\TeleportLink.txt")))
			{
				// using faster sqlite performance
				db.Begin();

				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled on the game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// Extract name
						db.ExecuteQuery("SELECT name,tid2,tid3,tid4 FROM models WHERE id=" + TeleportData[data[1]][3]);
						List<NameValueCollection> result = db.GetResult();
						if (result.Count != 0)
						{
							name = result[0]["name"];
							tid1 = "1";
							tid2 = result[0]["tid2"];
							tid3 = result[0]["tid3"];
							tid4 = result[0]["tid4"];
						}
						else
						{
							// Teleports without gate
							name = GetNameReference(TeleportData[data[1]][4]);
							tid1 = "4";
							tid2 = tid3 = tid4 = "0";
						}
						if (name == "")
							name = TeleportData[data[1]][2]; // Just in case
						
						// Extract destination
						db.ExecuteQuery("SELECT name FROM models WHERE id=" + TeleportData[data[2]][3]);
						result = db.GetResult();
						if (result.Count != 0)
						{
							destination = result[0]["name"];
						}
						else
						{
							// Teleports without gate
							destination = GetNameReference(TeleportData[data[2]][4]);
						}
						if (destination == "")
							destination = TeleportData[data[2]][2]; // Just in case

						// 30% display
						if (rand.Next(1, 1000) <= 300)
							LogState("Adding " + TeleportData[data[1]][2]);
						
						// INSERT
						db.Prepare("INSERT INTO teleportlinks (sourceid,destinationid,id,servername,name,destination,tid1,tid2,tid3,tid4,gold,level,spawn_region,spawn_x,spawn_y,spawn_z,pos_region,pos_x,pos_y,pos_z) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
						try
						{
							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT * FROM teleportlinks WHERE sourceid=" + data[1] + " AND destinationid=" + data[2]);
							if (db.GetResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO teleportlinks (sourceid,destinationid,id,servername,name,destination,tid1,tid2,tid3,tid4,gold,level,spawn_region,spawn_x,spawn_y,spawn_z,pos_region,pos_x,pos_y,pos_z) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
								db.Bind("sourceid", data[1]);
								db.Bind("destinationid", data[2]);
							}
							else
							{
								// Override
								db.Prepare("UPDATE teleportlinks SET id=?,servername=?,name=?,destination=?,tid1=?,tid2=?,tid3=?,tid4=?,gold=?,level=?,spawn_region=?,spawn_x=?,spawn_y=?,spawn_z=?,pos_region=?,pos_x=?,pos_y=?,pos_z=? WHERE sourceid=" + data[1] + " AND destinationid=" + data[2]);
							}
							db.Bind("id", TeleportData[data[1]][3]);
							db.Bind("servername", TeleportData[data[1]][2]);
							db.Bind("name", name);
							db.Bind("destination", destination);
							db.Bind("tid1", tid1);
							db.Bind("tid2", tid2);
							db.Bind("tid3", tid3);
							db.Bind("tid4", tid4);
							db.Bind("gold", data[3]);
							db.Bind("level", data[8]);
							db.Bind("spawn_region", (ushort)short.Parse(TeleportData[data[1]][5]));
							db.Bind("spawn_x", int.Parse(TeleportData[data[1]][6]));
							db.Bind("spawn_y", int.Parse(TeleportData[data[1]][8]));
							db.Bind("spawn_z", int.Parse(TeleportData[data[1]][7]));
							db.Bind("pos_region", (ushort)short.Parse(TeleportData[data[2]][5]));
							db.Bind("pos_x", int.Parse(TeleportData[data[2]][6]));
							db.Bind("pos_y", int.Parse(TeleportData[data[2]][8]));
							db.Bind("pos_z", int.Parse(TeleportData[data[2]][7]));
							db.ExecuteQuery();
						}
						catch
						{
							 // Wrong data index or something else
						}
					}
				}
				db.End();
			}
		}
		private void AddRegions()
		{
			// Load Region names
			Dictionary<string, string> RegionReferences = new Dictionary<string, string>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("server_dep\\silkroad\\textdata\\TextZoneName.txt")))
			{
				while (!reader.EndOfStream)
				{
					if ((line = reader.ReadLine()) == null)
						continue;

					// Data is enabled on the game
					if (line.StartsWith(pk2_lineEnabled))
					{
						data = line.Split(pk2_split, StringSplitOptions.None);

						// 15% display
						if (rand.Next(1, 1000) <= 150)
							LogState("Loading " + data[1]);

						if (data[LanguageIndex] != "0")
							RegionReferences[data[1]] = data[LanguageIndex];
					}
				}
			}

			// Add Region names
			string sql = "CREATE TABLE regions (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "name VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			// using faster sqlite performance
			db.Begin();
			foreach (string key in RegionReferences.Keys)
			{
				uint dummy;
				if (uint.TryParse(key, out dummy))
				{
					// 15% display
					if (rand.Next(1, 1000) <= 150)
						LogState("Adding " + key);

					// INSERT OR UPDATE
					db.ExecuteQuery("SELECT id FROM regions WHERE id=" + key);
					if (db.GetResult().Count == 0)
					{
						// New
						db.Prepare("INSERT INTO regions (id,name) VALUES (?,?)");
						db.Bind("id", key);
					}
					else
					{
						// Override
						db.Prepare("UPDATE regions SET name=? WHERE id=" + key);
					}
					db.Bind("name", RegionReferences[key]);
					db.ExecuteQuery();
				}
			}
			db.End();
		}
	}
}