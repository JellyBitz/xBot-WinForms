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
		private const byte CPU_BREAK = 50;
		private string pk2FileName { get; }
		private string dbName { get; }
		private PK2Reader pk2;
		private Database db;
		/// <summary>
		/// Keep all name references from the game.
		/// </summary>
		private Dictionary<string, string> NameReferences;
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
		public PK2Extractor(string pk2FileName,string dbName)
		{
      InitializeComponent();
			InitializeFonts(this);
			// Stuffs ...
      this.pk2FileName = pk2FileName;
			this.dbName = dbName;
			rtbxLogs.Text = WinAPI.getDate() + rtbxLogs.Text;
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
				WinAPI.InvokeIfRequired(w.General_tbxVersion,()=>{
					w.General_tbxVersion.Tag = version;
					w.General_tbxVersion.Text = version.ToString();
				});
				LogState();
      }
			catch (Exception ex)
			{
				// Not a big deal, can be added manually
				Log("Extracting error: " + ex.Message);
			}
			
			try
			{
				Log("Extracting Locale & Gateway");
				LogState("Extracting...");
				pReader = new PacketReader(pk2.GetFileBytes("DivisionInfo.txt"));
				WinAPI.InvokeIfRequired(w.General_tbxLocale, () => {
					w.General_tbxLocale.Tag = pReader.ReadByte();
					w.General_tbxLocale.Text = w.General_tbxLocale.Tag.ToString();
				});
				byte dvs = pReader.ReadByte();
				for (int i = 0; i < dvs; i++)
				{
					pReader.ReadChars(pReader.ReadInt32()); // DivisionName
					pReader.ReadByte(); // 0

					byte gws = pReader.ReadByte();
					WinAPI.InvokeIfRequired(w.General_lstvHost, () => {
						w.General_lstvHost.Items.Clear();
						for (int j = 0; j < gws; j++)
						{
							w.General_lstvHost.Items.Add(Encoding.ASCII.GetString(pReader.ReadBytes(pReader.ReadInt32())));
							pReader.ReadByte(); // 0
						}
					});
				}
				LogState();
				Thread.Sleep(CPU_BREAK);
			}
			catch (Exception ex){
				// Not a big deal
				Log("Extracting error: " + ex.Message);
			}

			try
			{
				Log("Extracting Gateport");
				LogState("Extracting...");
				WinAPI.InvokeIfRequired(w.General_tbxPort, () => {
					w.General_tbxPort.Text = pk2.GetFileText("Gateport.txt").Trim();
					w.General_tbxPort.Tag = ushort.Parse(w.General_tbxPort.Text);
				});
				LogState();
			}
			catch (Exception ex)
			{
				// Not a big deal
				Log("Extracting error: " + ex.Message);
			}

			// Recreating database
      Log("Creating Database...");
			LogState("Creating database");
			if (Database.Exists(dbName))
			{
				if (!Database.Delete(dbName))
				{
					// Deleting issues
					Log("The current database \"" + dbName+"\" is being used by another program. Please, close all the bots and try again!");
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
			Log("Loading Language references (English)");
			LoadNameReferences();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Items...");
			AddItems();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Characters & Mobs...");
			AddModels();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Masteries & Skills...");
			AddSkills();
			Thread.Sleep(CPU_BREAK);
			Log("Loading Teleport references");
			LoadTeleportData();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Teleports & Structures...");
			AddTeleports();
			Thread.Sleep(CPU_BREAK);
			Log("Adding Levels...");
			AddLevels();
			Thread.Sleep(CPU_BREAK);
			Log("Database \"" + db.Name + "\" has been generated sucessfully!");
			LogState("Database generated sucessfully");
			db.Close();

			WinAPI.InvokeIfRequired(w.General_btnAddSilkroad, () => {
				w.General_btnAddSilkroad.Font = new Font(w.General_btnAddSilkroad.Font,FontStyle.Regular);
			});
			WinAPI.InvokeIfRequired(w,()=> {
				w.Control_Click(w.General_btnAddSilkroad, null);
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
			byte language = 8; // English

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("TextDataName.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string file in files)
			{
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream(file)))
				{
					while ((line = WinAPI.ReadToString(reader, "\r\n")) != null)
					{
						// Data enabled in game
						if (line.StartsWith("1\t"))
						{
							data = line.Split(split, StringSplitOptions.None);

							// 15% display
							if (rand.Next(1, 1000) <= 150)
								LogState("Loading " + data[1]);

							// Check if has translation
							if (data[language] != "0")
								NameReferences[data[1]] = data[language];

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
		public void AddItems()
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
			string file,line, name;
			char[] split = new char[] { '\t' };
			string[] data;

      using (StreamReader sr = new StreamReader(pk2.GetFileStream("ItemData.txt")))
			{
				while ((file = WinAPI.ReadToString(sr, "\r\n")) != null)
				{
					file = file.Trim();
					if (file == "")
						continue;
					// Keep memory safe
					using (StreamReader reader = new StreamReader(pk2.GetFileStream(file)))
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
								if(name == "")
									name = data[2];

								// 15% display
								if (rand.Next(1,1000) <= 150)
									LogState("Adding " + data[2]);
								// INSERT 
								db.Prepare("INSERT INTO items (id,servername,name,stack,tid2,tid3,tid4,icon) VALUES (?,?,?,?,?,?,?,?);");
								db.Bind("id", data[1]);
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
			string[] files = pk2.GetFileText("CharacterData.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length;i++)
			{
				files[i] = files[i].Trim();
				if (files[i] == "")
					continue;
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream(files[i])))
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
							if(name == "")
								name = data[2];
							// Extract attacking skills if it has one
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
							// INSERT
							db.Prepare("INSERT INTO models (id,servername,name,tid2,tid3,tid4,hp,level,skills) VALUES (?,?,?,?,?,?,?,?,?)");
							db.Bind("id", data[1]);
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
			string line, name, desc,duration,attributes;
			int index;
			char[] split = new char[] { '\t' };
			string[] data;

			// short file, load all lines to memory
			string[] files = pk2.GetFileText("SkillData.txt").Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Trim();
				if (files[i] == "")
					continue;
				// Keep memory safe
				using (StreamReader reader = new StreamReader(pk2.GetFileStream(files[i])))
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
							db.Prepare("INSERT INTO skills (id,servername,name,description,casttime,cooldown,duration,mana,level,sp,icon,mastery_id,target_required,attributes) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
							db.Bind("id", data[1]);
							db.Bind("servername", data[3]);
							db.Bind("name", name);
							db.Bind("description", desc);
							db.Bind("casttime", data[13]);
							db.Bind("cooldown", data[14]);
							db.Bind("duration", duration);
							db.Bind("mana", data[53]);
							db.Bind("level",data[36]);
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
			}
		}
		public void AddMasteries()
		{
			
		}
		/// <summary>
		/// Used to join teleport link table (since will be required always).
		/// </summary>
		private void LoadTeleportData()
		{
			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };
			string[] data;

			TeleportData = new Dictionary<string, string[]>();
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportData.txt")))
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
			using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportBuilding.txt")))
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
						TeleportBuildings[data[1]] = data;

						// CPU break
						Thread.Sleep(1);
					}
				}
			}
		}
		public void AddTeleports()
		{
			// JOIN teleports & links (since will be required all time)
			string sql = "CREATE TABLE teleports (";
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
			string line, name, destination,tid1,tid2,tid3,tid4;
			char[] split = new char[] { '\t' };
      string[] data;
			SRObject tp = new SRObject();

      using (StreamReader reader = new StreamReader(pk2.GetFileStream("TeleportLink.txt")))
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
              db.ExecuteQuery("SELECT name,tid2,tid3,tid4 FROM models WHERE id="+ TeleportData[data[1]][3]);
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
								tid1 = tid2 =	tid3 = tid4 = "0";
							}
						}
						if (name == "")
							name = TeleportData[data[1]][2]; // Just in case

						// Extract destination
						destination = GetName(TeleportData[data[2]][4]);
						if(destination == "")
						{
							db.ExecuteQuery("SELECT name FROM teleports WHERE sourceid=" + data[2]);
							List<NameValueCollection> result = db.getResult();
							if (result.Count != 0)
								destination = result[0]["name"];
							else
								destination = TeleportData[data[1]][2];
						}

						// Calculating game coords
						tp[SRAttribute.Region] = (ushort)short.Parse(TeleportData[data[1]][5]);
						tp[SRAttribute.X] = float.Parse(TeleportData[data[1]][6]);
						tp[SRAttribute.Y] = float.Parse(TeleportData[data[1]][8]);
						tp[SRAttribute.Z] = 0f;
						Point pSpawn = tp.getPosition();
						tp[SRAttribute.Region] = (ushort)short.Parse(TeleportData[data[2]][5]);
						tp[SRAttribute.X] = float.Parse(TeleportData[data[2]][6]);
						tp[SRAttribute.Y] = float.Parse(TeleportData[data[2]][8]);
						tp[SRAttribute.Z] = 0f;
						Point pLoc = tp.getPosition();

						// 30% display
						if (rand.Next(1, 1000) <= 300)
							LogState("Adding " + TeleportData[data[1]][2]);
						// INSERT
						db.Prepare("INSERT INTO teleports (sourceid,destinationid,id,servername,name,destination,tid1,tid2,tid3,tid4,gold,level,region,spawn_x,spawn_y,pos_x,pos_y) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
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
		private void AddLevels()
		{
			string sql = "CREATE TABLE levels (";
			sql += "level INTEGER PRIMARY KEY,";
			sql += "exp INTEGER";
			sql += ");";
			db.ExecuteQuery(sql);

			// vars constantly used
			Random rand = new Random();
			string line;
			char[] split = new char[] { '\t' };
			string[] data;

			using (StreamReader reader = new StreamReader(pk2.GetFileStream("LevelData.txt")))
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
						db.Prepare("INSERT INTO levels (level,exp) VALUES (?,?)");
						db.Bind("level", data[0]);
						db.Bind("exp", data[1]);
						db.ExecuteQuery();

						// CPU Break
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
					if (tGenerator != null 
						&& tGenerator.ThreadState == ThreadState.Running 
						&& MessageBox.Show(this, "The process still running. Are you sure?", "xBot - PK2 Extractor", MessageBoxButtons.YesNo) != DialogResult.Yes) {
						return;
					}
					if (db != null)
					{
						db.Close();
						Database.Delete(db.Name);
					}
					if (tGenerator != null)
						tGenerator.Abort();
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
