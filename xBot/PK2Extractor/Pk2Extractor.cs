using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using SecurityAPI;
using System.Text;
using xBot.PK2Extractor.PK2ReaderAPI;
using xBot.App;

namespace xBot.PK2Extractor
{
	public partial class Pk2Extractor : Form
	{
		private const string DirectoryBase = "Data";
		private const string DatabaseFileName = "Database.sqlite3";
		/// <summary>
		/// Random to display logs.
		/// </summary>
		private Random rand = new Random();
		/// <summary>
		/// Directory base to allocate all files used by the bot.
		/// </summary>
		private string SilkroadPath;
		/// <summary>
		/// Name of the silkroad.
		/// </summary>
		private string SilkroadName;
		/// <summary>
		/// Path to the media pk2.
		/// </summary>
		private string MediaPk2Path;
		private Thread tGenerateData;
		private Pk2Reader pk2;
		private SQLDatabase db;
		// Media pk2 data
		public byte Locale { get; private set; }
		public uint Version { get; private set; }
		public System.Collections.Generic.List<string> Gateways { get; private set; }
		public ushort Gateport { get; private set; }

		/// <summary>
		/// Window dialog to generate a database sqlite file.
		/// </summary>
		/// <param name="MediaPK2Path">Media.pk2 file location</param>
		/// <param name="SilkroadName">Database name</param>
		public Pk2Extractor(string MediaPk2Path, string SilkroadName)
		{
			InitializeComponent();
			InitializeFonts(this);
			// Create base directory
			this.SilkroadPath = GetDirectory(SilkroadName);
			if (!Directory.Exists(SilkroadPath))
				Directory.CreateDirectory(SilkroadPath);
			// Stuffs ...
			this.SilkroadName = SilkroadName;
			this.MediaPk2Path = MediaPk2Path;

			rtbxLogs.Text = WinAPI.GetDate() + rtbxLogs.Text;
			LogState();
		}
		private void InitializeFonts(Control c)
		{
			// Using fontName as TAG to be selected from WinForms
			c.Font = Fonts.GetFont(c.Font, (string)c.Tag);
			c.Tag = null;
			for (int j = 0; j < c.Controls.Count; j++)
				InitializeFonts(c.Controls[j]);
		}
		/// <summary>
		/// Log a message to historial.
		/// </summary>
		public void Log(string Message)
		{
			try {
				WinAPI.InvokeIfRequired(rtbxLogs, () => {
					rtbxLogs.Text += "\n" + WinAPI.GetDate() + " " + Message;
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
		public static string GetDirectory(string SilkroadName)
		{
			return DirectoryBase + "\\" + SilkroadName + "\\";
		}
		public static string GetDatabasePath(string SilkroadName)
		{
			return GetDirectory(SilkroadName) + DatabaseFileName;
		}
		public static bool DirectoryExists(string SilkroadName)
		{
			return Directory.Exists(GetDirectory(SilkroadName));
		}
		public static void DirectoryDelete(string SilkroadName)
		{
			string dir = GetDirectory(SilkroadName);
      if (Directory.Exists(dir))
				WinAPI.DirectoryTryDelete(dir);
		}
		/// <summary>
		/// Try to generate the database.
		/// </summary>
		public void ThreadGenerateData()
		{
			Log("Opening Pk2 file using " + (tbxBlowfishKey.Text != "" ? "blowfish key: " + tbxBlowfishKey.Text : "default blowfish key"));
			LogState("Opening Pk2 file...");
			try
			{
				pk2 = new Pk2Reader(MediaPk2Path, tbxBlowfishKey.Text);
			}
			catch
			{
				Log("Error opening Pk2 file. Possibly wrong blowfish key");
				LogState("Error");
				btnStart.InvokeIfRequired(() => {
					btnStart.Enabled = true;
				});
				return;
			}
			Log("Pk2 file opened!");
			LogState();

			// Fill info to Main GUI
			Window w = Window.Get;
			try
			{
				Log("Extracting Silkroad Version");
				LogState("Extracting...");
				// Reading
				Packet p = new Packet(0,false,false,pk2.GetFileBytes("SV.T"));
				p.Lock();
				int dataLength = p.ReadInt();
				byte[] dataBuffer = p.ReadByteArray(dataLength);
				// Decoding
				Blowfish bf = new Blowfish();
				bf.Initialize(Encoding.ASCII.GetBytes("SILKROADVERSION"), 0, dataLength);
				byte[] dataDecoded = bf.Decode(dataBuffer);
				this.Version = uint.Parse(Encoding.ASCII.GetString(dataDecoded, 0, 4));
			}
			catch (Exception ex)
			{
				Log("Extracting error, the version cannot be readed. "+ ex.Message);
				LogState("Error");
				btnStart.InvokeIfRequired(() => {
					btnStart.Enabled = true;
				});
				return;
			}

			try
			{
				Log("Extracting Locale & Gateway");
				LogState("Extracting...");
				// Reading
				Packet p = new Packet(0, false, false, pk2.GetFileBytes("DivisionInfo.txt"));
				p.Lock();
				this.Locale = p.ReadByte();
				byte divisionCount = p.ReadByte();
				for (int i = 0; i < divisionCount; i++)
				{
					string DivisionName = p.ReadString(p.ReadInt());
					p.ReadByte(); // 0

					byte gatewayCount = p.ReadByte();
					Gateways = new System.Collections.Generic.List<string>(gatewayCount);
					for (int j = 0; j < gatewayCount; j++)
					{
						string gatewayHost = p.ReadString(p.ReadInt());
						p.ReadByte(); // 0

						Gateways.Add(gatewayHost);
					}
				}
			}
			catch (Exception ex)
			{
				Log("Extracting error, gateways cannot be readed. " + ex.Message);
				LogState("Error");
				btnStart.InvokeIfRequired(() => {
					btnStart.Enabled = true;
				});
				return;
			}

			try
			{
				Log("Extracting Gateport");
				LogState("Extracting...");
				// Reading
				Packet p = new Packet(0, false, false, pk2.GetFileBytes("Gateport.txt"));
				p.Lock();

				string test = p.ReadString(p.RemainingRead());
				this.Gateport = ushort.Parse(test);
			}
			catch (Exception ex)
			{
				Log("Extracting error, the gateport cannot be readed. " + ex.Message);
				LogState("Error");
				btnStart.InvokeIfRequired(() => {
					btnStart.Enabled = true;
				});
				return;
			}

			// Updating database
			Log("Creating Database...");
			string dbPath = GetDatabasePath(SilkroadName);
			if (File.Exists(dbPath))
			{
				LogState("Deleting old database");
				if (!WinAPI.FileTryDelete(dbPath))
				{
					// Deleting issues
					Log("The database from \"" + SilkroadName + "\" is being used by another program. Please, close all the bots and try again!");
					LogState("Error");
					btnStart.InvokeIfRequired(() => {
						btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
					});
					return;
				}
			}
			// Creating the database
			LogState("Creating database");
			db = new SQLDatabase(dbPath);
			if (!db.Create())
			{
				Log("Error creating the database. Please, close all the bots and try again!");
				LogState("Error");
				btnStart.InvokeIfRequired(() => {
					btnStart.Font = new Font(btnStart.Font, FontStyle.Regular);
				});
				return;
			}
			Log("Database has been created!");
			
			// Create connection
			LogState("Connecting to database");
			if (!db.Connect())
			{
				Log("Database connection error!");
				Log("Error");
				return;
			}
			LogState("Connected");

			// Generating database
			Log("Generating database (this may take a while)");
			SetLanguageIndex();
			Log("Loading name references...");
			LoadNameReferences();
			Log("Loading & Adding text references...");
			LoadTextReferences();
			AddTextReferences();
			Log("Adding Items...");
			AddItems();
			Log("Adding Magic options...");
			AddMagicOptions();
			Log("Adding Characters & Mobs...");
			AddModels();
			Log("Adding Masteries & Skills...");
			AddMasteries();
			AddSkills();
			Log("Adding Exp. & Levels...");
			AddLevelExperience();
			Log("Adding Shops...");
			AddShops();
			Log("Loading Teleport references");
			LoadTeleportData();
			Log("Adding Teleports & Structures...");
			AddTeleportBuildings();
			AddTeleportLinks();
			Log("Adding Regions...");
			AddRegions();
			Log("Database has been generated correctly!");
			
			Log("Creating Item icons...");
			AddItemIcons();
			Log("Creating Skill icons...");
			AddSkillIcons();
			if(this.cbxMinimap.Checked)
			{
				Log("Creating minimap images to the bot folder...");
				AddMinimap();
			}

			Log("All has been generated succesfully, Enjoy! :)");
			db.Close();
			pk2.Close();
			pk2.Dispose();
			LogState("Closing Pk2 file...");
			Thread.Sleep(1000);

			WinAPI.InvokeIfRequired(this, () => {
				this.DialogResult = DialogResult.OK;
				this.Activate();
				this.Close();
			});
		}
		private void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "btnWinExit":
					if (tGenerateData != null)
					{
						if(tGenerateData.ThreadState == ThreadState.WaitSleepJoin || tGenerateData.ThreadState == ThreadState.Running){
							if(MessageBox.Show(this, "The process still running. Are you sure?", "xBot - Pk2 Extractor", MessageBoxButtons.YesNo) != DialogResult.Yes)
								return;
						}
						tGenerateData.Abort();
						if (db != null)
						{
							db.Close();
						}
						if(pk2 != null){
							pk2.Close();
							pk2.Dispose();
						}
					}
					this.Close();
					break;
				case "btnStart":
					btnStart.Enabled = false;
					tGenerateData = new Thread(ThreadGenerateData);
					tGenerateData.Priority = ThreadPriority.Highest;
					tGenerateData.Start();
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