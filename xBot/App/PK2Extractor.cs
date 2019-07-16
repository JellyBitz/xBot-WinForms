using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using xBot.Game;
using SecurityAPI;
using PK2ReaderAPI;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace xBot
{
	public partial class PK2Extractor : Form
	{
		private Thread tGenerator;
		private const byte CPU_BREAK = 100;
		private string pk2FileName { get; }
		private string dbName { get; }
		private PK2Reader pk2;
		private Database db;
		/// <summary>
		/// Keep all name references from the game.
		/// </summary>
		private Dictionary<string, string> NameReferences;
		private Dictionary<string, string> UITextReferences;
		/// <summary>
		///  Switch the language detected
		/// </summary>
		private byte LanguageIndex = 8; // English
		/// <summary>
		/// Keep all teleport data used for linking.
		/// </summary>
		private Dictionary<string, string[]> TeleportData;
		private Dictionary<string, string[]> TeleportBuildings;
		/// <summary>
		/// Window dialog to generate a database sqlite file.
		/// </summary>
		/// <param name="pk2FileName">Media.pk2 file location</param>
		/// <param name="dbName">Database name</param>
		public PK2Extractor(string pk2FileName, string dbName)
		{
			InitializeComponent();
			InitializeFonts(this);
			// Stuffs ...
			this.pk2FileName = pk2FileName;
			this.dbName = dbName;
			rtbxLogs.Text = WinAPI.getDate() + rtbxLogs.Text;
			cmbxLanguage.SelectedIndex = 0;
			LogState();
		}
		private void InitializeFonts(Control c)
		{
			for (int i = 0; i < c.Controls.Count; i++)
			{
				// Using fontName as TAG to be selected from WinForms
				c.Controls[i].Font = Fonts.Get.Load(c.Controls[i].Font, (string)c.Controls[i].Tag);
				InitializeFonts(c.Controls[i]);
			}
		}
		/// <summary>
		/// Log a message to historial.
		/// </summary>
		public void Log(string Message)
		{
			try
			{
				WinAPI.InvokeIfRequired(rtbxLogs, () => {
					rtbxLogs.Text += "\n" + WinAPI.getDate() + " " + Message;
				});
			}
			catch {/* Window closed */}
		}
		/// <summary>
		/// Log process messages (to calm down the user while very long process)
		/// </summary>
		public void LogState(string Message = "Ready")
		{
			try
			{
				WinAPI.InvokeIfRequired(rtbxLogs, () => {
					lblProcessState.Text = Message;
				});
			}
			catch {/* Window closed */}
		}
		private void RichTextBox_TextChanged_AutoScroll(object sender, EventArgs e)
		{
			RichTextBox r = (RichTextBox)sender;
			WinAPI.SendMessage(r.Handle, WinAPI.WM_VSCROLL, WinAPI.SB_PAGEBOTTOM, 0);
			r.SelectionStart = r.Text.Length;
		}
		/// <summary>
		/// Tries to generate the database.
		/// </summary>
		public void GenerateDatabase()
		{
			Log("Opening PK2 file using " + (tbxBlowfishKey.Text != "" ? "blowfish key: " + tbxBlowfishKey.Text : "default blowfish key"));
			LogState("Opening PK2 file...");
			try
			{
				pk2 = new PK2Reader(pk2FileName, tbxBlowfishKey.Text);
			}
			catch
			{
				Log("Error opening PK2 file. Possibly wrong blowfish key");
				LogState("Error");
				WinAPI.InvokeIfRequired(btnStart, () => {
					btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
				});
				return;
			}
			Log("PK2 file opened!");
			LogState();
			Thread.Sleep(CPU_BREAK);
			// Fill info to Main GUI
			Window w = Window.Get;
			PacketReader pReader = null;

			try
			{
				Log("Extracting Silkroad Version");
				LogState("Extracting...");
				pReader = new PacketReader(pk2.GetFileBytes("SV.T"));
				int versionLength = pReader.ReadInt32();
				byte[] versionBuffer = pReader.ReadBytes(versionLength);
				Blowfish bf = new Blowfish();
				bf.Initialize(Encoding.ASCII.GetBytes("SILKROADVERSION"), 0, versionLength);
				byte[] versionDecoded = bf.Decode(versionBuffer);
				uint version = uint.Parse(Encoding.ASCII.GetString(versionDecoded, 0, 4));
				WinAPI.InvokeIfRequired(w.Settings_tbxVersion, () => {
					w.Settings_tbxVersion.Tag = version;
					w.Settings_tbxVersion.Text = version.ToString();
				});
				LogState();
			}
			catch (Exception ex)
			{
				Log("Extracting error, the version cannot be readed. "+ ex.Message);
				LogState("Error");
				WinAPI.InvokeIfRequired(btnStart, () => {
					btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
				});
				return;
			}

			try
			{
				Log("Extracting Locale & Gateway");
				LogState("Extracting...");
				pReader = new PacketReader(pk2.GetFileBytes("DivisionInfo.txt"));
				WinAPI.InvokeIfRequired(w.Settings_tbxLocale, () => {
					w.Settings_tbxLocale.Tag = pReader.ReadByte();
					w.Settings_tbxLocale.Text = w.Settings_tbxLocale.Tag.ToString();
				});
				byte dvs = pReader.ReadByte();
				for (int i = 0; i < dvs; i++)
				{
					pReader.ReadChars(pReader.ReadInt32()); // DivisionName
					pReader.ReadByte(); // 0

					byte gws = pReader.ReadByte();
					WinAPI.InvokeIfRequired(w.Settings_lstvHost, () => {
						w.Settings_lstvHost.Items.Clear();
						for (int j = 0; j < gws; j++)
						{
							w.Settings_lstvHost.Items.Add(Encoding.ASCII.GetString(pReader.ReadBytes(pReader.ReadInt32())));
							pReader.ReadByte(); // 0
						}
					});
				}
				LogState();
				Thread.Sleep(CPU_BREAK);
			}
			catch (Exception ex)
			{
				Log("Extracting error, gateways cannot be readed. " + ex.Message);
				LogState("Error");
				WinAPI.InvokeIfRequired(btnStart, () => {
					btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
				});
			}

			try
			{
				Log("Extracting Gateport");
				LogState("Extracting...");
				WinAPI.InvokeIfRequired(w.Settings_tbxPort, () => {
					w.Settings_tbxPort.Text = pk2.GetFileText("Gateport.txt").Trim();
					w.Settings_tbxPort.Tag = ushort.Parse(w.Settings_tbxPort.Text);
				});
				LogState();
			}
			catch (Exception ex)
			{
				Log("Extracting error, the gateport cannot be readed. " + ex.Message);
				LogState("Error");
				WinAPI.InvokeIfRequired(btnStart, () => {
					btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
				});
			}

			// Recreating database
			Log("Creating Database...");
			LogState("Creating database");
			if (Database.Exists(dbName))
			{
				if (!Database.Delete(dbName))
				{
					// Deleting issues
					Log("The current database \"" + dbName + "\" is being used by another program. Please, close all the bots and try again!");
					LogState("Error");
					WinAPI.InvokeIfRequired(btnStart, () => {
						btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
					});
					return;
				}
			}

			// Generating the database
			db = new Database(dbName);
			if (!db.Create())
			{
				Log("Error creating the database. Please, close all the bots and try again!");
				LogState("Error");
				WinAPI.InvokeIfRequired(btnStart, () => {
					btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
				});
				return;
			}
			Log("Database \"" + db.Name + "\" has been created!");
			LogState("Connecting to database");
			if (!db.Connect())
			{
				Log("Database connection error!");
				Log("Error");
				return;
			}
			Thread.Sleep(CPU_BREAK);
			LogState("Connected");

			// Generating records
			Log("Generating database (this may take a while)");
			Thread.Sleep(CPU_BREAK);
			Log("Loading Text references...");
			LoadNameReferences();
			Thread.Sleep(CPU_BREAK);
			Log("Loading & adding System Text references...");
			LoadUITextReferences();
			AddTextUISystem();
      Thread.Sleep(CPU_BREAK);
			Log("Adding Items...");
			AddItems();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Characters & Mobs...");
			AddModels();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Masteries & Skills...");
			AddMasteries();
			AddSkills();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Exp. & Levels...");
			AddLevelExperience();
			Thread.Sleep(CPU_BREAK);
			Log("Loading Teleport references");
			LoadTeleportData();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Teleports & Structures...");
			AddTeleportBuildings();
			AddTeleportLinks();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Regions...");
			AddRegions();
			Thread.Sleep(CPU_BREAK);
			Log("Database \"" + db.Name + "\" has been generated sucessfully!");
			LogState("Database generated sucessfully");
			db.Close();
			pk2.Dispose();
			w.EnableControl(w.Settings_btnAddSilkroad, true);
			WinAPI.InvokeIfRequired(this, () => {
				WinAPI.SetForegroundWindow(this.Handle);
			});
			WinAPI.InvokeIfRequired(w, () => {
				w.Control_Click(w.Settings_btnLauncherPath, null);
			});
			WinAPI.InvokeIfRequired(w, () => {
				w.Control_Click(w.Settings_btnClientPath, null);
			});
			WinAPI.InvokeIfRequired(w, () => {
				w.Control_Click(w.Settings_btnAddSilkroad, null);
				WinAPI.SetForegroundWindow(w.Handle);
			});
			WinAPI.InvokeIfRequired(this, () => {
				Close();
			});
		}
		private void LoadNameReferences()
		{
			NameReferences = new Dictionary<string, string>();
			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };
			string[] data;

			bool languageSelected = false;
			// Select language
			WinAPI.InvokeIfRequired(cmbxLanguage, ()=> {
				if (cmbxLanguage.SelectedIndex != 0)
				{
					switch (cmbxLanguage.Text)
					{
						case "English":
							LanguageIndex = 8;
							break;
						case "Vietnam":
							LanguageIndex = 9;
							break;
						case "Korean":
							LanguageIndex = 2;
							break;
					}
					Log("Using "+ cmbxLanguage.Text + " as language index");
					languageSelected = true;
        }
			});
			

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("TextDataName.txt", "server_dep/silkroad/textdata").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string file in files)
			{
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream(file, "server_dep/silkroad/textdata")))
				{
					while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
					{
						// Data enabled in game
						if (line.StartsWith("1\t"))
						{
							data = line.Split(split, StringSplitOptions.None);

							if (!languageSelected)
							{
								if (data[LanguageIndex] == "0" || data[LanguageIndex] == "")
								{
									Log("English language empty. Switching to Vietnam");
                  LanguageIndex = 9; // Set Vietnam language
									if (data[LanguageIndex] == "0" || data[LanguageIndex] == "")
									{
										Log("Vietnam language empty. Switching to Korean");
										LanguageIndex = 2; // Set Korean language
									}
								}
								else {
									Log("Using English as language");
                }
								languageSelected = true;
              }
							
							// 10% display
							if (rand.Next(1, 1000) <= 100)
								LogState("Loading " + data[1]);
							
							if (data.Length > LanguageIndex && data[LanguageIndex] != "0")
								NameReferences[data[1]] = data[LanguageIndex];

							// CPU break
							Thread.Sleep(1);
						}
					}
				}
			}
		}
		private string GetName(string ServerName)
		{
			if (NameReferences.ContainsKey(ServerName))
				return NameReferences[ServerName];
			return "";
		}
		private void LoadUITextReferences()
		{
			UITextReferences = new Dictionary<string, string>();
			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };

			string text;
			string[] data, c_formats = new string[] { "%d", "%s", "%ld", "%u", "%08x", "%I64d" , "%l64d" };
			int formatIndex, formatCount;

			// Keep memory safe
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TextUISystem.txt", "server_dep/silkroad/textdata")))
			{
				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data enabled in game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);

						// 10% display
						if (rand.Next(1, 1000) <= 100)
							LogState("Loading " + data[1]);
						
						if (data.Length > LanguageIndex && data[LanguageIndex] != "0")
						{
							text = data[LanguageIndex];
							formatCount = 0;
							// Convert from C++ to C# format
							foreach (string c_format in c_formats)
							{
								while ( (formatIndex = text.IndexOf(c_format)) != -1)
								{
									text = text.Remove(formatIndex) + "{"+ formatCount + "}"+ text.Substring(formatIndex + c_format.Length);
									formatCount++;
                }
							}
							UITextReferences[data[1]] = text;
						}

						// CPU break
						Thread.Sleep(1);
					}
				}
			}
		}
		private string GetUIText(string UITextName)
		{
			if (UITextReferences.ContainsKey(UITextName))
				return UITextReferences[UITextName];
			return "";
		}
		public void AddTextUISystem()
		{
			string sql = "CREATE TABLE textuisystem (";
			sql += "ID INTEGER PRIMARY KEY AUTOINCREMENT,";
			sql += "servername VARCHAR(64) UNIQUE,";
			sql += "text VARCHAR(256)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			Random rand = new Random();

			// using faster sqlite performance
			db.Begin();
			foreach (string key in UITextReferences.Keys)
			{
				// 10% display
				if (rand.Next(1, 1000) <= 100)
					LogState("Adding " + key);
				// INSERT
				db.Prepare("INSERT INTO textuisystem (servername,text) VALUES (?,?);");
				db.Bind("servername", key);
				db.Bind("text", UITextReferences[key]);
				db.ExecuteQuery();

				// CPU break
				Thread.Sleep(1);
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
			sql += "icon VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			Random rand = new Random();
			string file, line, name;
			char[] split = new char[] { '\t' };
			string[] data;

			using (StreamReader sr = new StreamReader(pk2.GetFileStream("ItemData.txt", "server_dep/silkroad/textdata")))
			{
				while ((file = WinAPI.ReadToString(sr, "\r\n")) != null)
				{
					file = file.Trim();
					if (file == "")
						continue;
					// Keep memory safe
					using (StreamReader reader = new StreamReader(pk2.GetFileStream(file, "server_dep/silkroad/textdata")))
					{
						// using faster sqlite performance
						db.Begin();

						while ((line = WinAPI.ReadToString(reader, "\n")) != null)
						{
							// Data is enabled in game
							if (line.StartsWith("1\t"))
							{
								data = line.Split(split, StringSplitOptions.None);
								// Extract name if has one

								name = "";
                if (data[5] != "xxx")
									name = GetName(data[5]);

								// 15% display
								if (rand.Next(1, 1000) <= 150)
									LogState("Adding " + data[2]);
								// INSERT OR UPDATE
								db.ExecuteQuery("SELECT id FROM items WHERE id=" + data[1]);
								if (db.getResult().Count == 0)
								{
									// New
									db.Prepare("INSERT INTO items (id,servername,name,stack,tid2,tid3,tid4,icon) VALUES (?,?,?,?,?,?,?,?);");
									db.Bind("id", data[1]);
								}
								else
								{
									// Override
									db.Prepare("UPDATE items SET servername=?,name=?,stack=?,tid2=?,tid3=?,tid4=?,icon=? WHERE id=" + data[1]);
								}
								db.Bind("servername", data[2]);
								db.Bind("name", name);
								db.Bind("stack", data[57]);
								db.Bind("tid2", data[10]);
								db.Bind("tid3", data[11]);
								db.Bind("tid4", data[12]);
								db.Bind("icon", data[54]);
								db.ExecuteQuery();

								// CPU break
								Thread.Sleep(1);
							}
						}
						db.End();
					}
				}
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
			sql += "level INTEGER,";
			sql += "skills VARCHAR(256)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			Random rand = new Random();
			string line, name, skills;
			char[] split = new char[] { '\t' };
			string[] data;
			byte index;

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("CharacterData.txt", "server_dep/silkroad/textdata").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Trim();
				if (files[i] == "")
					continue;
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream(files[i], "server_dep/silkroad/textdata")))
				{
					// using faster sqlite performance
					db.Begin();

					while ((line = WinAPI.ReadToString(reader, "\n")) != null)
					{
						// Data is enabled in game
						if (line.StartsWith("1\t"))
						{
							data = line.Split(split, StringSplitOptions.None);
							// Extract name if has one
							name = "";
							if (data[5] != "xxx")
								name = GetName(data[5]);
							if (name == "")
								name = data[2];
							// Extract attacking skills if has one
							skills = "";
							index = 83;
							while (data[index] != "0")
							{
								skills += data[index] + "|";
								index++;
							}
							if (index != 83)
								skills = skills.Remove(skills.Length - 1);

							// 15% display
							if (rand.Next(1, 1000) <= 150)
								LogState("Adding " + data[2]);
							
							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT id FROM models WHERE id=" + data[1]);
							if (db.getResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO models (id,servername,name,tid2,tid3,tid4,hp,level,skills) VALUES (?,?,?,?,?,?,?,?,?)");
								db.Bind("id", data[1]);
							}
							else
							{
								// Override
								db.Prepare("UPDATE models SET servername=?,name=?,tid2=?,tid3=?,tid4=?,hp=?,level=?,skills=? WHERE id=" + data[1]);
							}
							db.Bind("servername", data[2]);
							db.Bind("name", name);
							db.Bind("tid2", data[10]);
							db.Bind("tid3", data[11]);
							db.Bind("tid4", data[12]);
							db.Bind("hp", data[59]);
							db.Bind("level", data[57]);
							db.Bind("skills", skills);
							db.ExecuteQuery();

							// CPU break
							Thread.Sleep(1);
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
			string line, name, desc, type;
			char[] split = new char[] { '\t' };
			string[] data;

			// Keep memory safe
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("SkillMasteryData.txt", "server_dep/silkroad/textdata")))
			{
				// using faster sqlite performance
				db.Begin();

				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					if (line.StartsWith("//"))
						continue;

					data = line.Split(split, StringSplitOptions.None);
					// Avoid wrong data
					if (data.Length == 13 && data[2] != "xxx")
					{
						// Extract name if has one
						name = GetUIText(data[2]);
						if (name == "")
							name = GetName(data[2]);
						if (name == "")
							name = data[2];
						// Extract description if has one
						desc = GetUIText(data[4]);
						if (desc == "")
							desc = GetName(data[4]);
						if (desc == "")
							desc = data[4];
						// Extract type if has one
						type = GetUIText(data[5]);
						if (desc == "")
							type = GetName(data[5]);
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

						// CPU long break
						Thread.Sleep(10);
					}
				}
				db.End();
			}
		}
		public void AddSkills()
		{
			string sql = "CREATE TABLE skills (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "servername VARCHAR(64), ";
			sql += "name VARCHAR(64),";
			sql += "description VARCHAR(1024),";
			sql += "cooldown INTEGER,";
			sql += "duration INTEGER,";
			sql += "casttime INTEGER,";
			sql += "level INTEGER,";
			sql += "mastery_id INTEGER,";
			sql += "sp INTEGER,";
			sql += "mana INTEGER,";
			sql += "icon VARCHAR(64),";
			sql += "target_required BOOLEAN,";
			sql += "attributes VARCHAR (256)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			Random rand = new Random();
			string line, name, desc, duration, attributes;
			int index;
			char[] split = new char[] { '\t' };
			string[] data;

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("SkillDataEnc.txt", "server_dep/silkroad/textdata").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Trim();
				if (files[i] == "")
					continue;

				LogState("Decoding " + files[i]);
				// Decrypt and save the file to be used as stream
				File.WriteAllBytes("Data//"+ files[i] + ".tmp",DecryptSkillData(pk2.GetFileStream(files[i],"server_dep/silkroad/textdata")));
				
				// Keep memory safe
				using (StreamReader reader = new StreamReader("Data//" + files[i] + ".tmp"))
				{
					// using faster sqlite performance
					db.Begin();

					while ((line = WinAPI.ReadToString(reader, "\n")) != null)
					{
						// Data is enabled in game
						if (line.StartsWith("1\t"))
						{
							data = line.Split(split, StringSplitOptions.None);
							// Extract name if has one
							name = "";
							if (data[62] != "xxx")
								name = GetName(data[62]);
							if (name == "")
								name = data[3];
							// Extract descriptione if has one
							if (data[64] != "xxx")
								desc = GetName(data[64]);
							else
								desc = "";
							// Check if is an buff/debuff
							if (data[68] == "3")
								duration = data[70];
							else
								duration = "0";
							// Extract values
							attributes = "";
							index = 69;
							while (data[index] != "0")
							{
								attributes += data[index] + "|";
								index++;
							}
							if (index != 69)
								attributes = attributes.Remove(attributes.Length - 1);

							// 15% display
							if (rand.Next(1, 1000) <= 150)
								LogState("Adding " + data[3]);
							// INSERT

							// INSERT OR UPDATE
							db.ExecuteQuery("SELECT id FROM skills WHERE id=" + data[1]);
							if (db.getResult().Count == 0)
							{
								// New
								db.Prepare("INSERT INTO skills (id,servername,name,description,casttime,cooldown,duration,mana,level,sp,icon,mastery_id,target_required,attributes) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
								db.Bind("id", data[1]);
							}
							else
							{
								// Override
								db.Prepare("UPDATE skills SET servername=?,name=?,description=?,casttime=?,cooldown=?,duration=?,mana=?,level=?,sp=?,icon=?,mastery_id=?,target_required=?,attributes=? WHERE id=" + data[1]);
							}
							db.Bind("servername", data[3]);
							db.Bind("name", name);
							db.Bind("description", desc);
							db.Bind("casttime", data[13]);
							db.Bind("cooldown", data[14]);
							db.Bind("duration", duration);
							db.Bind("mana", data[53]);
							db.Bind("level", data[36]);
							db.Bind("sp", data[46]);
							db.Bind("icon", data[61]);
							db.Bind("mastery_id", data[34]);
							db.Bind("target_required", data[22]);
							db.Bind("attributes", attributes);
							db.ExecuteQuery();

							// CPU break
							Thread.Sleep(1);
						}
					}
					db.End();
				}
				
				//  Delete temporal skilldata decoded
				File.Delete("Data//" + files[i] + ".tmp");
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

			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };
			string[] data;

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("LevelData.txt", "server_dep/silkroad/textdata")))
			{
				// using faster sqlite performance
				db.Begin();

				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					if (!line.StartsWith("//"))
					{
						data = line.Split(split, StringSplitOptions.None);
						
						// 100% display
						LogState("Adding Lv." + data[0]);
						db.Prepare("INSERT INTO leveldata (level,player,sp,pet,trader,thief,hunter) VALUES (?,?,?,?,?,?,?)");
						db.Bind("level", data[0]);
						db.Bind("player", data[1]);
						db.Bind("sp", data[2]);
						db.Bind("pet", data[5]);
						db.Bind("trader",data[6] == "-1" ? "0" : data[6]); // safe ulong casting
						db.Bind("thief", data[7] == "-1" ? "0" : data[7]);
						db.Bind("hunter", data[8] == "-1" ? "0" : data[8]);
						db.ExecuteQuery();

						// CPU Break
						Thread.Sleep(1);
					}
				}
				db.End();
			}
		}
		/// <summary>
		/// Used to join teleport link table (since will be required always).
		/// </summary>
		private void LoadTeleportData()
		{
			TeleportData = new Dictionary<string, string[]>();
			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };
			string[] data;

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportData.txt", "server_dep/silkroad/textdata")))
			{
				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled in game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);

						// 80% display
						if (rand.Next(1, 1000) <= 800)
							LogState("Loading " + data[2]);
						TeleportData[data[1]] = data;

						// CPU break
						Thread.Sleep(1);
					}
				}
			}
			TeleportBuildings = new Dictionary<string, string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportBuilding.txt", "server_dep/silkroad/textdata")))
			{
				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled in game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);

						// 50% display
						if (rand.Next(1, 1000) <= 500)
							LogState("Loading " + data[2]);
						TeleportBuildings[data[1]] = data;

						// CPU break
						Thread.Sleep(1);
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
			Random rand = new Random();
			string line, name;
			char[] split = new char[] { '\t' };
			string[] data;

			TeleportBuildings = new Dictionary<string, string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportBuilding.txt", "server_dep/silkroad/textdata")))
			{
				// using faster sqlite performance
				db.Begin();
				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled in game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);
						// Extract name if has one
						name = "";
            if (data[5] != "xxx")
							name = GetName(data[5]);
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

						// CPU break
						Thread.Sleep(1);
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
			sql += "region INTEGER,";
			sql += "spawn_x INTEGER,";
			sql += "spawn_y INTEGER,";
			sql += "pos_x INTEGER,";
			sql += "pos_y INTEGER,";
			sql += "PRIMARY KEY (sourceid, destinationid)";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			Random rand = new Random();
			string line, name, destination, tid1, tid2, tid3, tid4;
			char[] split = new char[] { '\t' };
			string[] data;
			SRObject tp = new SRObject();

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportLink.txt", "server_dep/silkroad/textdata")))
			{
				// using faster sqlite performance
				db.Begin();

				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled on the game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);

						// Extract name
						try
						{
							name = GetName(TeleportBuildings[TeleportData[data[1]][3]][5]);
							tid1 = TeleportBuildings[TeleportData[data[1]][3]][9];
							tid2 = TeleportBuildings[TeleportData[data[1]][3]][10];
							tid3 = TeleportBuildings[TeleportData[data[1]][3]][11];
							tid4 = TeleportBuildings[TeleportData[data[1]][3]][12];
						}
						catch
						{
							db.ExecuteQuery("SELECT name,tid2,tid3,tid4 FROM models WHERE id=" + TeleportData[data[1]][3]);
							List<NameValueCollection> result = db.getResult();
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
								name = GetName(TeleportData[data[1]][4]);
								tid1 = "4";
								tid2 = tid3 = tid4 = "0";
							}
						}
						if (name == "")
							name = TeleportData[data[1]][2]; // Just in case

						// Extract destination
						destination = GetName(TeleportData[data[2]][4]);
						if (destination == "")
						{
							db.ExecuteQuery("SELECT name FROM teleportlinks WHERE sourceid=" + data[2]);
							List<NameValueCollection> result = db.getResult();
							if (result.Count != 0)
								destination = result[0]["name"];
							else
								destination = TeleportData[data[1]][2];
						}

						// Calculating game coords
						ushort region = (ushort)short.Parse(TeleportData[data[1]][5]);
						int x = int.Parse(TeleportData[data[1]][6]);
						int z = int.Parse(TeleportData[data[1]][7]);
						int y = int.Parse(TeleportData[data[1]][8]);
						Point pSpawn = tp.GetPosition(region,x,y,z);
						region = (ushort)short.Parse(TeleportData[data[2]][5]);
						x = int.Parse(TeleportData[data[2]][6]);
						y = int.Parse(TeleportData[data[2]][7]);
						z = int.Parse(TeleportData[data[2]][8]);
						Point pLoc = tp.GetPosition(region, x, y, z);

						// 30% display
						if (rand.Next(1, 1000) <= 300)
							LogState("Adding " + TeleportData[data[1]][2]);
						// INSERT
						db.Prepare("INSERT INTO teleportlinks (sourceid,destinationid,id,servername,name,destination,tid1,tid2,tid3,tid4,gold,level,region,spawn_x,spawn_y,pos_x,pos_y) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
						db.Bind("sourceid", data[1]);
						db.Bind("destinationid", data[2]);
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
						db.Bind("region", TeleportData[data[1]][3]);
						db.Bind("spawn_x", pSpawn.X);
						db.Bind("spawn_y", pSpawn.Y);
						db.Bind("pos_x", pLoc.X);
						db.Bind("pos_y", pLoc.Y);
						db.ExecuteQuery();

						// CPU break
						Thread.Sleep(1);
					}
				}
				db.End();
			}
		}
		private void AddRegions()
		{
			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };
			string[] data;

			// Load Region names
			Dictionary<string, string> RegionReferences = new Dictionary<string, string>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TextZoneName.txt", "server_dep/silkroad/textdata")))
			{
				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled on the game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);
						
						// 15% display
						if (rand.Next(1, 1000) <= 150)
							LogState("Loading " + data[1]);

						if (data[LanguageIndex] != "0")
							RegionReferences[data[1]] = data[LanguageIndex];

						// CPU break
						Thread.Sleep(1);
					}
				}
			}

			// Add Region names
			string sql = "CREATE TABLE regions (";
			sql += "id INTEGER PRIMARY KEY,";
			sql += "name VARCHAR(64)";
			sql += ");";
			db.ExecuteQuery(sql);

			string name;
      using (StreamReader reader = new StreamReader(pk2.GetFileStream("RegionCode.txt", "server_dep/silkroad/textdata")))
			{
				// using faster sqlite performance
				db.Begin();

				while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled on the game
					if (line.StartsWith("1\t"))
					{
						data = line.Split(split, StringSplitOptions.None);

						// Extract region name
						name = "";
						if (data[2] != "xxx" && RegionReferences.ContainsKey(data[2] + "_01"))
						{
							// _01 = Name; _02 = Description; _03 = Monster lvls.
							name = RegionReferences[data[2] + "_01"];
						}
						else if (RegionReferences.ContainsKey(data[1]))
						{
							name = RegionReferences[data[1]];
						}

						// 15% display
						if (rand.Next(1, 1000) <= 150)
							LogState("Adding " + data[1]);

						// INSERT OR UPDATE
						db.ExecuteQuery("SELECT id FROM regions WHERE id=" + data[1]);
						if (db.getResult().Count == 0)
						{
							// New
							db.Prepare("INSERT INTO regions (id,name) VALUES (?,?)");
							db.Bind("id", data[1]);
						}
						else
						{
							// Override
							db.Prepare("UPDATE regions SET name=? WHERE id=" + data[1]);
						}
						db.Bind("name", name);
						db.ExecuteQuery();
						
						// CPU break
						Thread.Sleep(1);
					}
				}
				db.End();
			}
		}
		private void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Font.Strikeout)
			{
				// Control is disabled
				return;
			}
			switch (c.Name)
			{
				case "btnWinExit":
					if (tGenerator != null)
					{
						if(tGenerator.ThreadState == ThreadState.WaitSleepJoin || tGenerator.ThreadState == ThreadState.Running){
							if(MessageBox.Show(this, "The process still running. Are you sure?", "xBot - PK2 Extractor", MessageBoxButtons.YesNo) != DialogResult.Yes)
								return;
						}
						tGenerator.Abort();
						if (db != null)
						{
							db.Close();
							Database.Delete(db.Name);
						}
					}
					this.Close();
					break;
				case "btnStart":
					btnStart.Font = new Font(btnStart.Font, FontStyle.Strikeout);
					tGenerator = new Thread(GenerateDatabase);
					tGenerator.Priority = ThreadPriority.Highest;
					tGenerator.Start();
					break;
			}
		}
		#region (GUI Design)
		private void Control_FocusEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = Color.FromArgb(30, 150, 220);
					break;
				}
			}
		}
		private void Control_FocusLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = c.Parent.BackColor;
					break;
				}
			}
		}
		private void Window_Drag_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WinAPI.ReleaseCapture();
				WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
			}
		}
		#endregion
	}
}