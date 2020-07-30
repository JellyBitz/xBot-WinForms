using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using xBot.Game;
using xBot.PK2Extractor;
using xBot.Game.Objects;
using SecurityAPI;
using xBot.Network;
using xGraphics;
using AutoUpdaterDotNET;
using System.Reflection;
using xBot.Game.Objects.Party;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Entity;
using xBot.Game.Objects.Item;
using xBot.Game.Objects.Guild;

namespace xBot.App
{
	/// <summary>
	/// Main bot window GUI that handle all bot events.
	/// </summary>
	public partial class Window : Form
	{
		/// <summary>
		/// Unique instance of this class.
		/// </summary>
		private static Window _this = null;
		/// <summary>
		/// Advertising window.
		/// </summary>
		private Ads adsWindow;
		private Thread tAdsWindow;
		private bool isUpdateAvailable;
		private Window()
		{
            InitializeComponent();
			InitializeFonts(this);
			InitializePerformance(this);
			InitializeValues();
		}
		/// <summary>
		/// GetInstance. Secures an unique class creation for being used anywhere at the project.
		/// </summary>
		public static Window Get
		{
			get
			{
				if (_this == null)
					_this = new Window();
				return _this;
			}
		}
		/// <summary>
		/// Initialize fonts natives or not to the controls specified.
		/// </summary>
		private void InitializeFonts(Control c)
		{
			// Using fontName as TAG to be selected from WinForms
			c.Font = Fonts.GetFont(c.Font, (string)c.Tag);
			c.Tag = null;
			for (int j = 0; j < c.Controls.Count; j++)
				InitializeFonts(c.Controls[j]);
		}
		private void InitializePerformance(Control c)
		{
			if(typeof(Panel) == c.GetType())
			{
				typeof(Panel).InvokeMember("DoubleBuffered",
					BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
					null, c, new object[] { true });
			}
			for (int j = 0; j < c.Controls.Count; j++)
				InitializeFonts(c.Controls[j]);
		}
		/// <summary>
		/// Initialize all default values required by the GUI.
		/// </summary>
		private void InitializeValues()
		{
			// Window Texts
			SetTitle();
			this.lblHeaderText01.Text = this.ProductName + " -";
			this.lblHeaderText02.Location = new Point(this.lblHeaderText01.Location.X + this.lblHeaderText01.Size.Width, this.lblHeaderText01.Location.Y);
			this.lblHeaderText02.Text = "v" + this.ProductVersion;
			adsWindow = new Ads(this);

			// Vertical tabs
			// Login
			TabPageV_Option_Click(this.TabPageV_Control01_Login, null);
			// Horizontal tabs
			TabPageH_Option_Click(this.TabPageH_Character_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Inventory_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Players_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Party_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Guild_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Skills_Option01, null);
			Skills_cmbxAttackMobType.SelectedIndex = Skills_cmbxBuffMobType.SelectedIndex = 0;
			TabPageH_Option_Click(this.TabPageH_Training_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Stall_Option01, null);
			TabPageH_Option_Click(this.TabPageH_Chat_Option01, null);
			Chat_cmbxMsgType.SelectedIndex = 0;
			TabPageH_Option_Click(this.TabPageH_Settings_Option01, null);
			Settings_cmbxCreateCharRace.SelectedIndex =
			Settings_cmbxCreateCharGenre.SelectedIndex =
			Settings_cmbxInjectTo.SelectedIndex = 0;
		}
		/// <summary>
		/// Load command arguments to the App.
		/// </summary>
		private void LoadCommandLine()
		{
			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				string cmd = args[i].ToLower();
				// Check data
				if (cmd.StartsWith("-silkroad="))
				{
					Login_cmbxSilkroad.Text = args[i].Substring(10);
				}
				else if (cmd.StartsWith("-username="))
				{
					Login_tbxUsername.Text = args[i].Substring(10);
				}
				else if (cmd.StartsWith("-password="))
				{
					Login_tbxPassword.Text = args[i].Substring(10);
				}
				else if (cmd.StartsWith("-captcha="))
				{
					Login_tbxCaptcha.Text = args[i].Substring(9);
				}
				else if (cmd.StartsWith("-server="))
				{
					// Saving to Tag because cannot be set as text yet
					Login_cmbxServer.Tag = args[i].Substring(8);
				}
				else if (cmd.StartsWith("-character="))
				{
					// Saving to Tag because cannot be set as text yet
					Login_cmbxCharacter.Tag = args[i].Substring(11);
				}
				else if (cmd.Equals("--clientless"))
				{
					Login_rbnClientless.Checked = true;
				}
				else if (cmd.Equals("--goclientless"))
				{
					Login_cbxGoClientless.Checked = true;
				}
				else if (cmd.Equals("--relogin"))
				{
					Login_cbxRelogin.Checked = true;
				}
				else if (cmd.Equals("--usereturn"))
				{
					Login_cbxUseReturnScroll.Checked = true;
				}
			}
			// Check if minimum neccesary is correct to start auto login
			if (Login_cmbxSilkroad.Text != ""
				&& Login_tbxUsername.Text != ""
				&& Login_tbxPassword.Text != "" && Login_cmbxServer.Tag != null)
			{
				Bot.Get.hasAutoLoginMode = true;
				Control_Click(Login_btnStart, null);
			}
		}
		private void ShowAdvertising()
		{
			tAdsWindow = new Thread((ThreadStart)delegate{
				try
				{
					if (!adsWindow.isLoaded() && adsWindow.TryLoad())
					{
						// Load the banner in background
						Login_pbxAds.InvokeIfRequired(() => {
							Login_pbxAds.LoadAsync(adsWindow.GetData(Ads.EXCEL.URL_MINIBANNER));
							ToolTips.SetToolTip(Login_pbxAds, adsWindow.GetData(Ads.EXCEL.TITLE));
						});
					}
					if (adsWindow.isLoaded())
					{
						// Show Advertising
						this.InvokeIfRequired(() => {
							adsWindow.ShowDialog(this);
						});
					}
				}
				catch { /*Window closed or something else..*/ }
			});
			tAdsWindow.Start();
		}
		public void LogProcess(string text = "Ready", ProcessState state = ProcessState.Default)
		{
			lblBotState.InvokeIfRequired(() => {
				lblBotState.Text = text;
				switch (state)
				{
					case ProcessState.Warning:
						SetProcessColor(Color.FromArgb(202, 81, 0));
						break;
					case ProcessState.Disconnected:
						SetProcessColor(Color.FromArgb(104, 33, 122));
						break;
					case ProcessState.Error:
						SetProcessColor(Color.Red);
						break;
					default:
						SetProcessColor(Color.FromArgb(0, 122, 204));
						break;
				}
			});
		}
		private void SetProcessColor(Color newColor)
		{
			if (newColor == BackColor)
				return;
			lblBotState.BackColor = newColor;
			this.InvokeIfRequired(() => {
				BackColor = newColor;
			});
		}
		public void Log(string text)
		{
			try
			{
				rtbxLogs.InvokeIfRequired(() => {
					rtbxLogs.AppendText(Environment.NewLine + WinAPI.GetDate() + " " + text);
				});
			}
			catch { }
		}
		public void SetTitle()
		{
			this.InvokeIfRequired(() => {
				this.Text = this.ProductName;
				this.lblHeaderText02.Text = "v" + this.ProductVersion;
				this.NotifyIcon.Text = this.ProductName + " v" + this.ProductVersion + "\nMade by JellyBitz";
			});
		}
		public void SetTitle(string server, string charname,Process client = null)
		{
			string wTitle = this.ProductName + " - [" + server + "] " + charname;
			this.InvokeIfRequired(() => {
				this.Text = wTitle;
				this.lblHeaderText02.Text = charname;
				this.NotifyIcon.Text = this.ProductName+" v"+this.ProductVersion+"\n[" + server + "] " + charname;
			});
			if (client != null)
				WinAPI.SetWindowText(client.MainWindowHandle, wTitle);
		}
		public void LogMessageFilter(string message)
		{
			try
			{
				Character_rtbxMessageFilter.InvokeIfRequired(() => {
					Character_rtbxMessageFilter.AppendText(WinAPI.GetDate() + " " + message + Environment.NewLine);
				});
			}
			catch { }
		}
		public void LogPacket(string text)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				if (Menu_rtbxPackets_AddTimestamp.Checked)
					sb.Append(WinAPI.GetDate());
				sb.AppendLine(text);

				Settings_rtbxPackets.InvokeIfRequired(()=>{
					Settings_rtbxPackets.AppendText(sb.ToString());
				});
			}
			catch { }
		}
		public void LogChatMessage(RichTextBox chat, string player, string message)
		{
			try
			{
				chat.InvokeIfRequired(()=> {
					chat.AppendText(WinAPI.GetDate() + " " + player + ": " + message + Environment.NewLine);
				});
			}
			catch { }
		}
		private bool isValidFilename(string FileName)
		{
			try
			{
				Path.GetFileName(FileName);
			}
			catch
			{
				return false;
			}
			return true;
		}
		public void SetIngameButtons(bool enable)
		{
			Stall_btnIGCreateModify.InvokeIfRequired(() => {
				Stall_btnIGCreateModify.Enabled = enable;
			});
		}
		public void AddBuff(SRBuff Buff)
		{
			Label item = new Label();
			item.Size = lstimgIcons.ImageSize;
			item.Name = "pnlBuffs_"+ Buff.UniqueID;
			item.ImageList = this.lstimgIcons;
			item.ImageKey = GetImageKeyIcon(Buff.Icon);
			item.MouseClick += new MouseEventHandler(this.Label_pnlBuffs_MouseClick);
			ToolTips.SetToolTip(item, Buff.Name);
			// Keep a whole reference, easier skill checks
			item.Tag = Buff;
			Character_pnlBuffs.InvokeIfRequired(() =>{
				Character_pnlBuffs.Controls.Add(item);
			});
		}
		public void RemoveBuff(uint buffUniqueID)
		{
			Character_pnlBuffs.InvokeIfRequired(() => {
				Character_pnlBuffs.Controls.RemoveByKey("pnlBuffs_"+buffUniqueID);
			});
		}
		public void Character_Buffs_Clear()
		{
			Character_pnlBuffs.InvokeIfRequired(() => {
				Character_pnlBuffs.Controls.Clear();
			});
		}
		/// <summary>
		/// Add an skill (learned) to the skill list
		/// </summary>
		public void AddSkill(SRSkill Skill)
		{
			ListViewItem item = new ListViewItem(Skill.Name);
			item.Name = Skill.ID.ToString();
			// Keep a whole reference, easier skill checks
			item.Tag = Skill;
			item.ImageKey = GetImageKeyIcon(Skill.Icon);
			Skills_lstvSkills.InvokeIfRequired(() => {
				Skills_lstvSkills.Items.Add(item);
			});
		}
		public void UpdateSkill(uint lastSkillID, SRSkill newSkill)
		{
			ListViewItem temp;
			string key = lastSkillID.ToString();
			string newkey = newSkill.ID.ToString();
			// Invoke the TabPageV - contains all skill lists
			Skills_lstvSkills.Parent.InvokeIfRequired(() => {
				if ((temp = this.Skills_lstvSkills.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				// An array of references cannot be possible.. Using the long way "copy & paste" code :(
				if ((temp = this.Skills_lstvAttackMobType_General.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_Champion.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_Giant.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_PartyGeneral.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_PartyChampion.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_PartyGiant.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_Unique.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_Elite.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
				if ((temp = this.Skills_lstvAttackMobType_Event.Items[key]) != null)
				{ temp.Name = newkey; temp.Tag = newSkill; }
			});
		}
		public void RemoveSkill(uint SkillID)
		{
			string key = SkillID.ToString();
			// Invoke the TabPageV that contains the Skill list to drag
			Skills_lstvSkills.Parent.InvokeIfRequired(() => {
				this.Skills_lstvSkills.Items.RemoveByKey(key);
				// An array of references cannot be possible.. Using the long way "copy & paste" code :(
				this.Skills_lstvAttackMobType_General.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_Champion.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_Giant.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_PartyGeneral.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_PartyChampion.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_PartyGiant.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_Unique.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_Elite.Items.RemoveByKey(key);
				this.Skills_lstvAttackMobType_Event.Items.RemoveByKey(key);
			});
			Skills_lstvSkills.InvokeIfRequired(() => {
				Skills_lstvSkills.Items.RemoveByKey(key);
			});
		}
		public void Skills_Clear()
		{
			Skills_lstvSkills.InvokeIfRequired(() => {
				Skills_lstvSkills.Items.Clear();
			});
		}
		public void TrainingAreas_Clear()
		{
			Training_lstvAreas.InvokeIfRequired(() => {
				Training_lstvAreas.Items.Clear();
			});
		}
		public string GetImageKeyIcon(string Pk2Path)
		{
			// Check if image is not loaded
			if (!lstimgIcons.Images.ContainsKey(Pk2Path))
			{
				// Check if the file exists
				string FullPath = Pk2Extractor.GetDirectory(DataManager.SilkroadName) + "icon\\" + Pk2Path;
				FullPath = Path.ChangeExtension(FullPath, "png");
				if (File.Exists(FullPath))
				{
					// The image list it's being used by multiples controls
					this.InvokeIfRequired(()=> {
						lstimgIcons.Images.Add(Pk2Path, Image.FromFile(FullPath));
					});
				}
				else
				{
					// Try to load the default image
					FullPath = Pk2Extractor.GetDirectory(DataManager.SilkroadName) + "icon\\icon_default.png";
					if (File.Exists(FullPath))
					{
						this.InvokeIfRequired(() => {
							lstimgIcons.Images.Add(Pk2Path, Image.FromFile(FullPath));
						});
					}
					else
					{
						return "";
					}
				}
			}
			return Pk2Path;
		}

		/// <summary>
		/// Fix the text using a pattern restriction. Returns empty if the pattern is not found.
		/// </summary>
		private string FixTextRestriction(string Text, string Pattern, bool FirstMatch = false)
		{
			MatchCollection matches = Regex.Matches(Text, Pattern);
			if (matches.Count > 0)
			{
				if (FirstMatch)
					return matches[0].Value;
				StringBuilder fixedText = new StringBuilder();
				foreach (Match m in matches)
					fixedText.Append(m.Value);
				return fixedText.ToString();
			}
			return "";
		}
		/// <summary>
		/// Set the gold in Silkroad format color.
		/// </summary>
		public void Character_SetGold(ulong gold)
		{
			// 1000000 to 1.000.000
			string gText = gold.ToString("#,0");
			// Visual color
			Color gForeColor = GetGoldColor(gold);
			Character_lblGold.InvokeIfRequired(() => {
				Character_lblGold.ForeColor = gForeColor;
				Character_lblGold.Text = gText;
			});
		}
		public Color GetGoldColor(ulong gold)
		{
			int GoldDigits = gold.ToString().Length;

			if (GoldDigits <= 4)
				return Color.White;
			switch (GoldDigits)
			{
				case 5:
					return Color.FromArgb(255, 250, 133); // Light Yellow
				case 6:
					return Color.FromArgb(255, 211, 72); // Yellow
				case 7:
					return Color.FromArgb(255, 173, 92); // Dark Orange
				case 8:
					return Color.FromArgb(255, 154, 161); // Pink
				case 9:
					return Color.FromArgb(235, 161, 255); // Purple
				case 10:
					return Color.FromArgb(184, 187, 255); // Blue
				case 11:
					return Color.FromArgb(149, 222, 255); // Light Blue 
				case 12:
					return Color.FromArgb(139, 255, 229); // White Blue
				default:
					return Color.White;
			}
		}
		public void Character_SetPosition(SRCoord p)
		{
			Character_lblLocation.InvokeIfRequired(() => {
				if (Character_lblLocation.Tag == null || p.Region != (ushort)Character_lblLocation.Tag){
					Character_lblLocation.Tag = p.Region;
					Character_lblLocation.Text = DataManager.GetRegion(p.Region);
				}
			});
			Minimap_panelCoords.InvokeIfRequired(() => {
				Minimap_tbxX.Text = p.X.ToString();
				Minimap_tbxY.Text = p.Y.ToString();
				Minimap_tbxZ.Text = p.Z.ToString();
				Minimap_tbxRegion.Text = p.Region.ToString();
			});
			Character_lblCoordX.InvokeIfRequired(() => {
				Character_lblCoordX.Text = "X : " + Math.Round(p.PosX);
			});
			Character_lblCoordY.InvokeIfRequired(() => {
				Character_lblCoordY.Text = "Y : " + Math.Round(p.PosY);
			});
		}
		public void Inventory_ItemsRefresh()
		{
			Inventory_lstvItems.InvokeIfRequired(() => {
				Inventory_lstvItems.Items.Clear();
				Inventory_lstvItems.BeginUpdate();
			});
			if (InfoManager.inGame)
			{
				Inventory_lblCapacity.InvokeIfRequired(() => {
					Inventory_lblCapacity.Text = "Loading (0%) ...";
				});

				xList<SRItem> inventory = InfoManager.Character.Inventory;

				for (int j = 0; j < inventory.Capacity; j++)
				{
					ListViewItem item = new ListViewItem();
					item.Name = item.Text = j.ToString();
					if (inventory[j] != null)
					{
						item.SubItems.Add(inventory[j].GetFullName());
						item.SubItems.Add(inventory[j].isEquipable()? "1" : inventory[j].Quantity + "/" + inventory[j].QuantityMax);
						item.SubItems.Add(inventory[j].ServerName);
						item.ImageKey = GetImageKeyIcon(inventory[j].Icon);
						item.ToolTipText = inventory[j].GetTooltip();
					}
					else
					{
						item.SubItems.Add("Empty");
					}

					// Add
					Inventory_lstvItems.InvokeIfRequired(() => {
						Inventory_lstvItems.Items.Add(item);
					});
					Inventory_lblCapacity.InvokeIfRequired(() => {
						Inventory_lblCapacity.Text = "Loading  (" + Math.Round(j * 100.0 / inventory.Capacity) + "%) ...";
					});
				}
				Inventory_lblCapacity.InvokeIfRequired(() => {
					Inventory_lblCapacity.Text = "Capacity : " + inventory.Count + "/" + inventory.Capacity;
				});
			}
			else
			{
				Inventory_lblCapacity.InvokeIfRequired(() => {
					Inventory_lblCapacity.Text = "";
				});
			}
			Inventory_lstvItems.InvokeIfRequired(() => {
				Inventory_lstvItems.EndUpdate();
			});

			// Made to run with no locks
			Inventory_btnItemsRefresh.InvokeIfRequired(() => {
				Inventory_btnItemsRefresh.Tag = null;
			});
    }
		public void Inventory_AvatarItemsRefresh()
		{
			Inventory_lstvAvatarItems.InvokeIfRequired( () => {
				Inventory_lstvAvatarItems.Items.Clear();
				Inventory_lstvAvatarItems.BeginUpdate();
			});

			if (InfoManager.inGame)
			{
				xList<SRItem> inventory = InfoManager.Character.InventoryAvatar;

				for (int j = 0; j < inventory.Capacity; j++)
				{
					ListViewItem item = new ListViewItem();
					item.Name = item.Text = j.ToString();
					if (inventory[j] != null)
					{
						item.SubItems.Add(inventory[j].Name);
						item.SubItems.Add(inventory[j].ServerName);
						item.ImageKey = GetImageKeyIcon(inventory[j].Icon);
						item.ToolTipText = inventory[j].GetTooltip();
					}
					else
					{
						item.SubItems.Add("Empty");
					}
					// Add
					Inventory_lstvAvatarItems.InvokeIfRequired(() => {
						Inventory_lstvAvatarItems.Items.Add(item);
					});
				}
			}

			Inventory_lstvAvatarItems.InvokeIfRequired(() => {
				Inventory_lstvAvatarItems.EndUpdate();
			});

			// Made to run with no locks
			Inventory_btnAvatarItemsRefresh.InvokeIfRequired(() => {
				Inventory_btnAvatarItemsRefresh.Tag = null;
			});
		}
		public void Inventory_StorageRefresh()
		{
			Inventory_lstvStorageItems.InvokeIfRequired(() => {
				Inventory_lstvStorageItems.Items.Clear();
				Inventory_lstvStorageItems.BeginUpdate();
			});

			if (InfoManager.inGame && InfoManager.Character.Storage != null)
			{
				Inventory_lblStorageCapacity.InvokeIfRequired(() => {
					Inventory_lblStorageCapacity.Text = "Loading (0%) ...";
				});

				xList<SRItem> inventory = InfoManager.Character.Storage;

				for (int j = 0; j < inventory.Capacity; j++)
				{
					ListViewItem item = new ListViewItem();
					item.Name = item.Text = j.ToString();
					if (inventory[j] != null)
					{
						item.SubItems.Add(inventory[j].GetFullName());
						item.SubItems.Add(inventory[j].isEquipable() ? "1" : inventory[j].Quantity + "/" + inventory[j].QuantityMax);
						item.SubItems.Add(inventory[j].ServerName);
						item.ImageKey = GetImageKeyIcon(inventory[j].Icon);
						item.ToolTipText = inventory[j].GetTooltip();
					}
					else
					{
						item.SubItems.Add("Empty");
					}

					// Add
					Inventory_lstvStorageItems.InvokeIfRequired(() => {
						Inventory_lstvStorageItems.Items.Add(item);
					});
					Inventory_lblStorageCapacity.InvokeIfRequired(() => {
						Inventory_lblStorageCapacity.Text = "Loading  (" + Math.Round(j * 100.0 / inventory.Capacity) + "%) ...";
					});
				}
				Inventory_lblStorageCapacity.InvokeIfRequired(() => {
					Inventory_lblStorageCapacity.Text = "Capacity : " + inventory.Count + "/" + inventory.Capacity;
				});
			}
			else
			{
				Inventory_lblStorageCapacity.InvokeIfRequired(() => {
					Inventory_lblStorageCapacity.Text = "";
				});
			}
			Inventory_lstvStorageItems.InvokeIfRequired(() => {
				Inventory_lstvStorageItems.EndUpdate();
			});

			// Made to run with no locks
			Inventory_btnStorageRefresh.InvokeIfRequired(() => {
				Inventory_btnStorageRefresh.Tag = null;
			});
		}
		public void Inventory_PetRefresh()
		{
			Inventory_lstvPet.InvokeIfRequired(() => {
				Inventory_lstvPet.Items.Clear();
				Inventory_lstvPet.BeginUpdate();
			});

			SRCoService pet = InfoManager.MyPets.Find(p => p.isPickPet());
			if (pet != null)
			{
				Inventory_lblPetCapacity.InvokeIfRequired(() => {
					Inventory_lblPetCapacity.Text = "Loading (0%) ...";
				});

				xList<SRItem> inventory = pet.Inventory;

				for (int j = 0; j < inventory.Capacity; j++)
				{
					ListViewItem item = new ListViewItem();
					item.Name = item.Text = j.ToString();
					if (inventory[j] != null)
					{
						item.SubItems.Add(inventory[j].GetFullName());
						item.SubItems.Add(inventory[j].isEquipable() ? "1" : inventory[j].Quantity + "/" + inventory[j].QuantityMax);
						item.SubItems.Add(inventory[j].ServerName);
						item.ImageKey = GetImageKeyIcon(inventory[j].Icon);
						item.ToolTipText = inventory[j].GetTooltip();
					}
					else
					{
						item.SubItems.Add("Empty");
					}

					// Add
					Inventory_lstvPet.InvokeIfRequired(() => {
						Inventory_lstvPet.Items.Add(item);
					});
					Inventory_lblPetCapacity.InvokeIfRequired(() => {
						Inventory_lblPetCapacity.Text = "Loading  (" + Math.Round(j * 100.0 / inventory.Capacity) + "%) ...";
					});
				}
				Inventory_lblPetCapacity.InvokeIfRequired(() => {
					Inventory_lblPetCapacity.Text = "Capacity : " + inventory.Count + "/" + inventory.Capacity;
				});
			}
			else
			{
				Inventory_lblPetCapacity.InvokeIfRequired(() => {
					Inventory_lblPetCapacity.Text = "";
				});
			}

			Inventory_lstvPet.InvokeIfRequired(() => {
				Inventory_lstvPet.EndUpdate();
			});

			// Made to run with no locks
			Inventory_btnPetRefresh.InvokeIfRequired(() => {
				Inventory_btnPetRefresh.Tag = null;
			});
		}
		public void Players_Refresh()
		{
			Players_tvwPlayers.InvokeIfRequired(() => {
				Players_tvwPlayers.Nodes.Clear();
			});

			if (InfoManager.inGame)
			{
				Players_tvwPlayers.InvokeIfRequired(() => {
					Players_tvwPlayers.BeginUpdate();
				});

				Players_lblPlayerCount.InvokeIfRequired(() => {
					Players_lblPlayerCount.Text = "Loading (0%) ...";
				});

				xDictionary<string, SRPlayer> players = InfoManager.Players;

				for (int j = 0; j < players.Count; j++)
				{
					SRPlayer player = players.GetAt(j);
					// Create root node
					TreeNode root = new TreeNode();
					root.ImageKey = "None";
					root.Tag = player;
					root.Name = player.Name;
					// Create full nick info
					root.Text = player.GetFullName();
					if (player.InteractionType == SRPlayer.Interaction.OnStall)
					{
						root.Text += " - [" + player.Stall.Title + " ]";
						root.ForeColor = ColorItemHighlight;
					}
					// Add equipment view
					xList<SRItem> inventory = player.Inventory;
					TreeNode equipment = new TreeNode("Equipment");
					for (byte k = 0; k < inventory.Capacity; k++)
					{
						TreeNode itemNode = new TreeNode(inventory[k].GetFullName());
						itemNode.ImageKey = GetImageKeyIcon(inventory[k].Icon);
						itemNode.SelectedImageKey = itemNode.ImageKey;
						equipment.Nodes.Add(itemNode);
					}
					equipment.ImageKey = root.ImageKey;
					root.Nodes.Add(equipment);
					// Add avatar equipment view if it's normal mode
					if (player.InventoryAvatar.Count > 0)
					{
						TreeNode avatarEquipment = new TreeNode("Avatar Equipment");
						for (byte k = 0; k < player.InventoryAvatar.Capacity; k++)
						{
							if (player.InventoryAvatar[k] != null)
							{
								TreeNode itemNode = new TreeNode();
								itemNode.Text = player.InventoryAvatar[k].Name;
								itemNode.ImageKey = GetImageKeyIcon(player.InventoryAvatar[k].Icon);
								itemNode.SelectedImageKey = itemNode.ImageKey;
								avatarEquipment.Nodes.Add(itemNode);
							}
						}
						avatarEquipment.ImageKey = root.ImageKey;
						root.Nodes.Add(avatarEquipment);
					}

					Players_tvwPlayers.InvokeIfRequired(() => {
						Players_tvwPlayers.Nodes.Add(root);
					});
					Players_lblPlayerCount.InvokeIfRequired(() => {
						Players_lblPlayerCount.Text = "Loading (" + Math.Round(j * 100.0 / players.Count) + "%) ...";
					});
				}
				Players_tvwPlayers.InvokeIfRequired(() => {
					Players_tvwPlayers.EndUpdate();
					Players_lblPlayerCount.InvokeIfRequired(() => {
						Players_lblPlayerCount.Text = "Players around: " + Players_tvwPlayers.Nodes.Count;
					});
				});
			}
			else
			{
				Players_lblPlayerCount.InvokeIfRequired(() => {
					Players_lblPlayerCount.Text = "";
				});
			}

			// Made to run with no locks
			Players_btnRefreshPlayers.InvokeIfRequired(() => {
				Players_btnRefreshPlayers.Tag = null;
			});
		}
		public void Players_ClearExchange()
		{
			string resetText = "- - -";
			// Exchanger
			Players_lblExchangerName.InvokeIfRequired(() => {
				Players_lblExchangerName.Text = resetText;
			});
			Players_tbxExchangerGold.InvokeIfRequired(() => {
				Players_tbxExchangerGold.Text = resetText;
				Players_tbxExchangerGold.ForeColor = Color.White;
			});
			// Me
			Players_lblExchangerMyName.InvokeIfRequired(() => {
				Players_lblExchangerMyName.Text = resetText;
			});
			Players_tbxExchangingGold.InvokeIfRequired(() => {
				Players_tbxExchangingGold.Text = resetText;
				Players_tbxExchangingGold.ForeColor = Color.White;
				Players_tbxExchangingGold.ReadOnly = true;
			});
			Players_tbxGoldRemain.InvokeIfRequired(() => {
				Players_tbxGoldRemain.Text = resetText;
				Players_tbxGoldRemain.ForeColor = Color.White;
			});
			// Inventories
			Players_lstvInventoryExchange.InvokeIfRequired(() => {
				Players_lstvInventoryExchange.Items.Clear();
			});
			Players_lstvExchangerItems.InvokeIfRequired(() => {
				Players_lstvExchangerItems.Items.Clear();
			});
			Players_lstvExchangingItems.InvokeIfRequired(() => {
				Players_lstvExchangingItems.Items.Clear();
			});
			// turn off
			Players_btnExchange.InvokeIfRequired(() => {
				Players_btnExchange.Text = "Confirm";
				Players_btnExchange.Enabled = false;
			});
			Players_btnCancelExchange.InvokeIfRequired(() => {
				Players_btnCancelExchange.Enabled = false;
			});
			Players_btnExchangingGoldEdit.InvokeIfRequired(() => {
				Players_btnExchangingGoldEdit.Enabled = false;
			});
		}
		/// <summary>
		/// Returns the current party setup used by the GUI.
		/// </summary>
		public SRParty.Setup GetPartySetup()
		{
			return ((Party_rbnSetupExpShared.Checked ? SRParty.Setup.ExpShared : 0)
				| (Party_rbnSetupItemShared.Checked ? SRParty.Setup.ItemShared : 0)
				| (Party_cbxSetupMasterInvite.Checked ? 0 : SRParty.Setup.AnyoneCanInvite));
		}

		/// <summary>
		/// Returns the current party match setup used by the GUI.
		/// </summary>
		public SRPartyMatch GetPartyMatchSetup()
		{
			SRPartyMatch match = new SRPartyMatch();

			Party_tbxMatchTitle.InvokeIfRequired(() => {
				match.Title = Party_tbxMatchTitle.Text;
			});
			Party_tbxMatchFrom.InvokeIfRequired( () => {
				match.LevelMin = byte.Parse(Party_tbxMatchFrom.Text);
			});
			Party_tbxMatchTo.InvokeIfRequired( () => {
				match.LevelMax = byte.Parse(Party_tbxMatchTo.Text);
			});

			if (InfoManager.inParty)
				match.Setup = InfoManager.Party.SetupFlags;
			else
				match.Setup = GetPartySetup();
			
			if (InfoManager.Character.hasJobMode())
			{
				if (InfoManager.Character.JobType == SRPlayer.Job.Thief)
					match.Purpose = SRParty.Purpose.Thief;
				else
					match.Purpose = SRParty.Purpose.Trader;
			}
			else
			{
				match.Purpose = SRParty.Purpose.Hunting;
      }
			return match;
		}
		public void Party_AddMember(SRPartyMember member,bool myself)
		{
			ListViewItem item = new ListViewItem(member.Name);
			item.Name = member.ID.ToString();
			item.SubItems.Add(member.GuildName);
			item.SubItems.Add(member.Level.ToString());
			if (myself)
				item.SubItems.Add("- - -");
			else
				item.SubItems.Add(string.Format("{0}% / {1}%", member.HPPercent, member.MPPercent));
			item.SubItems.Add(DataManager.GetRegion(member.Position.Region));

			Party_lstvPartyMembers.InvokeIfRequired(() => {
				Party_lstvPartyMembers.Items.Add(item);
			});
		}
		public void Party_Clear()
		{
			Party_lstvPartyMembers.InvokeIfRequired(() => {
				Party_lstvPartyMembers.Items.Clear();
			});
			Party_lblCurrentSetup.InvokeIfRequired(() => {
				Party_lblCurrentSetup.Text = "";
			});
			this.InvokeIfRequired(() => {
				ToolTips.SetToolTip(Party_lblCurrentSetup, "");
			});
		}
		public void Guild_InfoRefresh()
		{
			if (InfoManager.inGame && InfoManager.inGuild)
			{
				Guild_lblName.InvokeIfRequired(() => {
					Guild_lblName.Text = InfoManager.Guild.Name;
				});
				Guild_lblNotice.InvokeIfRequired(() => {
					Guild_lblNotice.Text = InfoManager.Guild.Notice;
				});
				ToolTips.SetToolTip(Guild_lblNotice, InfoManager.Guild.Message);

				Guild_lblLevel.InvokeIfRequired(() => {
					Guild_lblLevel.Text = "Level " + InfoManager.Guild.Level;
				});
				// Recreate members
				Guild_lstvInfo.InvokeIfRequired(() => {
					Guild_lstvInfo.BeginUpdate();
					Guild_lstvInfo.Items.Clear();
				});

				for (byte i = 0; i < InfoManager.Guild.Members.Count; i++)
				{
					SRGuildMember member = InfoManager.Guild.Members.GetAt(i);

					ListViewItem item = new ListViewItem();
					item.Name = member.ID.ToString();
          item.Text = member.Name + (member.Nickname == "" ? "" : " * " + member.Nickname);
					item.SubItems.Add(member.Level.ToString());
					item.SubItems.Add(member.PermissionsFlags.ToString());
					item.SubItems.Add(member.GPoints.ToString());
					// Add online status as color
					if (member.Name == InfoManager.CharName)
						item.BackColor = ColorItemHighlight;
					else if (!member.isOffline)
					{
						item.UseItemStyleForSubItems = false;
						item.SubItems[0].ForeColor = Color.FromArgb(0, 220, 120);
					}

					Guild_lstvInfo.InvokeIfRequired(() => {
						Guild_lstvInfo.Items.Add(item);
					});
				}
				Guild_lstvInfo.InvokeIfRequired(() => {
					Guild_lstvInfo.EndUpdate();
				});
			}
			else
			{
				Guild_Clear();
      }

			// Made to run with no locks
			Guild_btnInfoRefresh.InvokeIfRequired(() => {
				Guild_btnInfoRefresh.Tag = null;
			});
		}
		public void Guild_Clear()
		{
			string textReset = "- - -";

			Guild_lblName.InvokeIfRequired(() => {
				Guild_lblName.Text = textReset;
      });
			Guild_lblNotice.InvokeIfRequired(() => {
				Guild_lblNotice.Text = textReset;
      });
			ToolTips.SetToolTip(Guild_lblNotice, "");
			Guild_lblLevel.InvokeIfRequired(() => {
				Guild_lblLevel.Text = textReset;
      });
			Guild_lstvInfo.InvokeIfRequired(() => {
				Guild_lstvInfo.Items.Clear();
			});
		}
		public void Guild_StorageRefresh()
		{
			Guild_lstvStorage.InvokeIfRequired(() => {
				Guild_lstvStorage.Items.Clear();
				Guild_lstvStorage.BeginUpdate();
			});

			if (InfoManager.inGame && InfoManager.inGuild && InfoManager.Guild.Storage != null)
			{
				Guild_lblStorageCapacity.InvokeIfRequired(() => {
					Guild_lblStorageCapacity.Text = "Loading (0%) ...";
				});

				xList<SRItem> storage = InfoManager.Guild.Storage;

				for (int j = 0; j < storage.Capacity; j++)
				{
					ListViewItem item = new ListViewItem();
					item.Name = item.Text = j.ToString();
					if (storage[j] != null)
					{
						item.SubItems.Add(storage[j].GetFullName());
						item.SubItems.Add(storage[j].isEquipable() ? "1" : storage[j].Quantity + "/" + storage[j].QuantityMax);
						item.SubItems.Add(storage[j].ServerName);
						item.ImageKey = GetImageKeyIcon(storage[j].Icon);
						item.ToolTipText = storage[j].GetTooltip();
					}
					else
					{
						item.SubItems.Add("Empty");
					}

					// Add
					Guild_lstvStorage.InvokeIfRequired(() => {
						Guild_lstvStorage.Items.Add(item);
					});
					Guild_lblStorageCapacity.InvokeIfRequired(() => {
						Guild_lblStorageCapacity.Text = "Loading  (" + Math.Round(j * 100.0 / storage.Capacity) + "%) ...";
					});
				}
				Guild_lblStorageCapacity.InvokeIfRequired(() => {
					Guild_lblStorageCapacity.Text = "Capacity : " + storage.Count + "/" + storage.Capacity;
				});
			}
			else
			{
				Guild_lblStorageCapacity.InvokeIfRequired(() => {
					Guild_lblStorageCapacity.Text = "";
				});
			}
			Guild_lstvStorage.InvokeIfRequired(() => {
				Guild_lstvStorage.EndUpdate();
			});

			// Made to run with no locks
			Guild_btnStorageRefresh.InvokeIfRequired(() => {
				Guild_btnStorageRefresh.Tag = null;
			});
		}
		/// <summary>
		/// Get the active training coordinate or null if none is found.
		/// </summary>
		public SRCoord TrainingArea_GetPosition()
		{
			SRCoord result = null;
			Training_lstvAreas.InvokeIfRequired(() =>
			{
				if (this.Training_lstvAreas.Tag != null){
					ListViewItem item = (ListViewItem)this.Training_lstvAreas.Tag;
					result = new SRCoord((ushort)item.SubItems[1].Tag, (int)item.SubItems[2].Tag, (int)item.SubItems[4].Tag, (int)item.SubItems[3].Tag);
				}
			});
			return result;
		}
		public int TrainingArea_GetRadius()
		{
			int result = 0;
			Training_lstvAreas.InvokeIfRequired(() => {
				if (this.Training_lstvAreas.Tag != null)
					result = (int)((ListViewItem)this.Training_lstvAreas.Tag).SubItems[5].Tag;
			});
			return result;
		}
		public string TrainingArea_GetScript()
		{
			string result = "";
			this.Training_lstvAreas.InvokeIfRequired(() => {
				if (this.Training_lstvAreas.Tag != null)
					result = ((ListViewItem)this.Training_lstvAreas.Tag).SubItems[6].Text;
			});
			return result;
		}
		/// <summary>
		/// Get all skillshots used for an specific mob type. If it's an empty list, it will try to add from a lower mob type.
		/// Returns null if the mob type is unknown.
		/// </summary>
		public SRSkill[] Skills_GetSkillShots(SRMob.Mob type)
		{
			SRSkill[] SkillShots = null;
			switch (type)
			{
				case SRMob.Mob.General:
					Skills_lstvAttackMobType_General.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_General.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_General.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_General.Items[j].Tag;
					});
					break;
				case SRMob.Mob.Champion:
					Skills_lstvAttackMobType_Champion.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_Champion.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_Champion.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_Champion.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.General;
					else
						break;
				case SRMob.Mob.Giant:
					Skills_lstvAttackMobType_Giant.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_Giant.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_Giant.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_Giant.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.Champion;
					else
						break;
				case SRMob.Mob.PartyGeneral:
					Skills_lstvAttackMobType_PartyGeneral.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_PartyGeneral.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_PartyGeneral.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_PartyGeneral.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.Giant;
					else
						break;
				case SRMob.Mob.PartyChampion:
					Skills_lstvAttackMobType_PartyChampion.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_PartyChampion.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_PartyChampion.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_PartyChampion.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.PartyGeneral;
					else
						break;
				case SRMob.Mob.PartyGiant:
					Skills_lstvAttackMobType_PartyGiant.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_PartyGiant.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_PartyGiant.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_PartyGiant.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.PartyChampion;
					else
						break;
				case SRMob.Mob.Unique:
				case SRMob.Mob.Titan:
					Skills_lstvAttackMobType_Unique.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_Unique.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_Unique.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_Unique.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.PartyGiant;
					else
						break;
				case SRMob.Mob.Elite:
				case SRMob.Mob.Strong:
					Skills_lstvAttackMobType_Elite.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_Elite.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_Elite.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_Elite.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.Unique;
					else
						break;
				case SRMob.Mob.Event:
					Skills_lstvAttackMobType_Event.InvokeIfRequired(() => {
						SkillShots = new SRSkill[Skills_lstvAttackMobType_Event.Items.Count];
						for (int j = 0; j < Skills_lstvAttackMobType_Event.Items.Count; j++)
							SkillShots[j] = (SRSkill)Skills_lstvAttackMobType_Event.Items[j].Tag;
					});
					if (SkillShots.Length == 0)
						goto case SRMob.Mob.Elite;
					else
						break;
				default:
					goto case SRMob.Mob.Event;
			}
			return SkillShots;
		}
		public void Stall_Create(xList<SRItemStall> inventoryStall)
		{
			Stall_lstvStall.InvokeIfRequired(() => {
				Stall_lstvStall.BeginUpdate();
				Stall_lstvStall.Items.Clear();
			});
			for (byte j = 0; j < 10 || j < inventoryStall.Capacity; j++)
			{
				ListViewItem listViewItem;
				if (inventoryStall[j] != null)
				{
					SRItem item = inventoryStall[j].Item;
					// Name
					listViewItem = new ListViewItem(item.Name);
					listViewItem.Tag = inventoryStall[j];
					listViewItem.ImageKey = GetImageKeyIcon(item.Icon);
					listViewItem.ToolTipText = item.GetTooltip();
					// quantity
					listViewItem.SubItems.Add(item.Quantity.ToString());
					// price
					ulong price = inventoryStall[j].Price;
					ListViewItem.ListViewSubItem ListViewSubItem = new ListViewItem.ListViewSubItem();
					ListViewSubItem.Text = price.ToString("#,0");
					listViewItem.SubItems.Add(ListViewSubItem);
					listViewItem.UseItemStyleForSubItems = false;
					ListViewSubItem.ForeColor = GetGoldColor(price);
				}
				else
				{
					listViewItem = new ListViewItem("Empty");
				}
				// Add to stall
				Stall_lstvStall.InvokeIfRequired(() => {
					Stall_lstvStall.Items.Add(listViewItem);
				});
			}
			Stall_lstvStall.InvokeIfRequired(() => {
				Stall_lstvStall.EndUpdate();
			});
		}
		public void Stall_Clear()
		{
			string textReset = "- - -";
			Stall_btnIGCreateModify.InvokeIfRequired(() => {
				Stall_btnIGCreateModify.Text = "Create";
			});
			Stall_lblState.InvokeIfRequired(() => {
				Stall_lblState.Text = textReset;
			});
			this.InvokeIfRequired(() => {
				ToolTips.SetToolTip(Stall_lblState,"");
			});
			Stall_tbxTitle.InvokeIfRequired(() => {
				Stall_tbxTitle.Text = textReset;
				Stall_tbxTitle.ReadOnly = true;
			});
			Stall_btnTitleEdit.InvokeIfRequired(() => {
				Stall_btnTitleEdit.Enabled = false;
			});
			Stall_btnClose.InvokeIfRequired(()=> {
				Stall_btnClose.Enabled = false;
      });
			Stall_tbxNote.InvokeIfRequired(() => {
				Stall_tbxNote.Text = textReset;
				Stall_tbxNote.ReadOnly = true;
			});
			Stall_btnNoteEdit.InvokeIfRequired(() => {
				Stall_btnNoteEdit.Enabled = false;
			});
			Stall_lstvInventoryStall.InvokeIfRequired(() => {
				Stall_lstvInventoryStall.Items.Clear();
      });
			Stall_tbxPrice.InvokeIfRequired(() => {
				Stall_tbxPrice.Text = textReset;
				Stall_tbxPrice.ReadOnly = true;
			});
			Stall_tbxQuantity.InvokeIfRequired(() => {
				Stall_tbxQuantity.Text = textReset;
			});
			Stall_btnAddItem.InvokeIfRequired(() => {
				Stall_btnAddItem.Enabled = false;
			});
			Stall_lstvStall.InvokeIfRequired(() => {
				Stall_lstvStall.Items.Clear();
				Stall_lstvStall.ContextMenuStrip = null;
			});
		}
		public void Minimap_Character_View(SRCoord position,double degreeAngle)
		{
			// Update the view if it's minimap selected
			if(TabPageV_Control01_Minimap_Panel.Visible)
			{
				Minimap_pnlMap.SetView(position);
				// Update arrow direction
				Minimap_Character_Angle(degreeAngle);
			}
		}
		double lastDegreeAngle = 0;
		public void Minimap_Character_Angle(double degreeAngle)
		{
			// Update if it's necessary only
			if (lastDegreeAngle != degreeAngle)
			{
				lastDegreeAngle = degreeAngle;
				Bitmap mm_sign_character = Properties.Resources.mm_sign_character;
				// Generate an image rotation
				Bitmap mm_sign_character_rotated = new Bitmap(mm_sign_character.Width, mm_sign_character.Height);
				mm_sign_character_rotated.SetResolution(mm_sign_character.HorizontalResolution, mm_sign_character.VerticalResolution);
				Graphics g = Graphics.FromImage(mm_sign_character_rotated);
				g.TranslateTransform(mm_sign_character.Width / 2, mm_sign_character.Height / 2);
				g.RotateTransform(-(float)degreeAngle);
				g.TranslateTransform(-mm_sign_character.Width / 2, -mm_sign_character.Height / 2);
				g.DrawImage(mm_sign_character, new Point(0, 0));
				// Update pointer Image angle
				Minimap_xmcCharacterMark.InvokeIfRequired(() => {
					Minimap_xmcCharacterMark.Image = mm_sign_character_rotated;
				});
			}
		}
		public void Minimap_Object_Add(uint uniqueID, SREntity entity)
		{
			xMapControl marker = new xMapControl();
			// Add image
			if (entity.isModel())
			{
				SRModel model = (SRModel)entity;
				if (model.isNPC())
				{
					SRNpc npc = (SRNpc)model;

					if (npc.isGuide())
					{
						marker.Image = Properties.Resources.mm_sign_npc;

						ToolTips.SetToolTip(marker, npc.Name);
					}
					else if (npc.isMob())
					{
						SRMob mob = (SRMob)npc;

						if(mob.MobType == SRMob.Mob.Unique || mob.MobType == SRMob.Mob.Titan)
							marker.Image = Properties.Resources.mm_sign_unique;
						else
							marker.Image = Properties.Resources.mm_sign_monster;

						ToolTips.SetToolTip(marker, mob.Name);
					}
					else if (npc.isCOS())
					{
						SRCoService cos = (SRCoService)npc;

						if(cos.isAttackPet() || cos.isPickPet())
							marker.Image = Properties.Resources.mm_sign_animal;
					}
				}
				else if (model.isPlayer()) {
					marker.Image = Properties.Resources.mm_sign_otherplayer;

					ToolTips.SetToolTip(marker, model.Name);
				}
			}
			else if (entity.isTeleport())
			{
				SRTeleport tp = (SRTeleport)entity;
				// Add if has teleport options only
				if(tp.TeleportOptions.Capacity > 0)
				{
					ContextMenuStrip menuTeleport = new ContextMenuStrip();
					for (int j = 0; j < tp.TeleportOptions.Count; j++)
					{
						ToolStripMenuItem item = new ToolStripMenuItem();
						item.Text = tp.TeleportOptions[j].Name;
						item.Name = tp.TeleportOptions[j].ID.ToString();
						item.Tag = tp;
						item.Click += Menu_Minimap_Teleport_Click;

						menuTeleport.Items.Add(item);
					}

					marker.ContextMenuStrip = menuTeleport;
					marker.Image = Properties.Resources.xy_gate;
				}
			}
			// Add only if has icon ready
			if (marker.Image != null)
			{
				// Expand PictureBox size
				marker.Size = marker.Image.Size;
				// Fix center
				Point location = Minimap_pnlMap.GetPoint(entity.GetRealtimePosition());
				location.X -= marker.Image.Size.Width/2;
				location.Y -= marker.Image.Size.Height/2;
				marker.Location = location;
				// Save full reference
				marker.Tag = entity;
				Minimap_pnlMap.AddMarker(uniqueID, marker);
			}
		}
		public void Minimap_Object_Remove(uint UniqueID)
		{
			Minimap_pnlMap.RemoveMarker(UniqueID);
		}
		public void Minimap_Objects_Clear()
		{
			Minimap_pnlMap.ClearMarkers();
		}

		#region (GUI theme design behavior)
		/// <summary>
		/// Set the control to be used as window drag.
		/// </summary>
		private void Window_Drag_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WinAPI.ReleaseCapture();
				WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
			}
		}
		/// <summary>
		/// Color the label associated (by name) to the current control focused.
		/// </summary>
		private void Control_Focus_Enter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv", "btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if (c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = Color.FromArgb(30, 150, 220);
					}
					break;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="Control_Focus_Enter(object, EventArgs)"/>
		/// </summary>
		private void Control_Focus_Leave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv", "btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if (c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = c.Parent.BackColor;
					}
					break;
				}
			}
		}
		/// <summary>
		/// Colors used on TabPage Vertical.
		/// </summary>
		private Color TabPageV_ColorHover = Color.FromArgb(74, 74, 76),
			TabPageV_ColorSelected = Color.FromArgb(0, 122, 204);
		/// <summary>
		///  TabPage Vertical option click.
		/// </summary>
		private void TabPageV_Option_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			List<Control> currentOption;
			if (c.Parent.Tag != null)
			{
				currentOption = (List<Control>)c.Parent.Tag;
				if (currentOption[0].Name == c.Name || currentOption[1].Name == c.Name)
					return;
				currentOption[0].BackColor = c.Parent.BackColor;
				currentOption[1].BackColor = c.Parent.BackColor;
				c.Parent.Parent.Controls[currentOption[0].Name + "_Panel"].Visible = false;
			}
			currentOption = new List<Control>();
			currentOption.Add(c.Parent.Controls[c.Name.Replace("_Icon", "")]);
			currentOption.Add(c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"]);
			c.Parent.Tag = currentOption;
			currentOption[0].BackColor = TabPageV_ColorSelected;
			currentOption[1].BackColor = TabPageV_ColorSelected;
			c.Parent.Parent.Controls[currentOption[1].Name.Replace("Icon", "Panel")].Visible = true;
		}
		/// <summary>
		/// Creates a custom color focus on icon and option used on TabPage Vertical.
		/// <para>Results are not as expected because Windows native focus interference...</para>
		/// </summary>
		private void TabPageV_Option_MouseEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorHover;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"].BackColor = TabPageV_ColorHover;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="TabPageV_Option_MouseEnter(object, EventArgs)"/>
		/// </summary>
		private void TabPageV_Option_MouseLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = c.Parent.BackColor;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"].BackColor = c.Parent.BackColor;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		/// <summary>
		///  TabPage Horizontal option click.
		/// </summary>
		private void TabPageH_Option_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				Control currentOption = (Control)c.Parent.Tag;
				if (currentOption.Name == c.Name)
					return;
				currentOption.BackColor = c.Parent.Parent.BackColor;
				c.Parent.Parent.Controls[currentOption.Name + "_Panel"].Visible = false;
			}
			c.Parent.Tag = c;
			c.BackColor = c.Parent.BackColor;
			c.Parent.Parent.Controls[c.Name + "_Panel"].Visible = true;
		}
		/// <summary>
		/// Modified TabPage Horizontal behavior for better chat UX.
		/// </summary>
		private void TabPageH_ChatOption_Click(object sender, EventArgs e)
		{
			TabPageH_Option_Click(sender, e);
			Control option = (Control)sender;
			if (option.Font.Bold)
				option.Font = new Font(option.Font, FontStyle.Regular);
			Chat_cmbxMsgType.Text = option.Text; // If is not into the options, then will not change.
		}
		/// <summary>
		/// Activate notify on chat.
		/// </summary>
		public void TabPageH_ChatOption_Notify(Control c)
		{
			c.Parent.InvokeIfRequired(() => {
				if (c.Parent.Tag != c && !c.Font.Bold)
					c.Font = new Font(c.Font, FontStyle.Bold);
			});
		}
		/// <summary>
		/// Forces the listview header to keep his width.
		/// </summary>
		private void ListView_ColumnWidthChanging_Cancel(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
		}
		public Color ColorItemHighlight = Color.FromArgb(120, 120, 120);
		#endregion

		#region (GUI events generated)
		/// <summary>
		/// Load all components (not visuals) to the App. Like settings and stuffs.
		/// </summary>
		private void Window_Load(object sender, EventArgs e)
		{
			// Welcome
			rtbxLogs.AppendText(string.Format("{0} Welcome to {1} v{2} | Made by Engels \"JellyBitz\" Quintero{3}{0} Discord : JellyBitz#7643 | FaceBook : @ImJellyBitz", WinAPI.GetDate(), base.ProductName, base.ProductVersion, Environment.NewLine));
			LogProcess();
			Settings.LoadBotSettings();
			// Load basic
			LoadCommandLine();
			// Force visible
			Activate();
			BringToFront();
			// Check for updates
			AutoUpdater.ReportErrors = true;
			AutoUpdater.OpenDownloadPage = true;
			AutoUpdater.CheckForUpdateEvent += new AutoUpdater.CheckForUpdateEventHandler(this.CheckUpdates_Completed);
			AutoUpdater.Start("http://bit.ly/xBot-update-check");
		}
		/// <summary>
		/// Close all necessary to not leaving any background process.
		/// </summary>
		private void Window_Closing(object sender, FormClosingEventArgs e)
		{
			if (Bot.Get.Proxy != null && Bot.Get.Proxy.isRunning)
				Bot.Get.Proxy.Stop();
			if(tAdsWindow != null && tAdsWindow.ThreadState == System.Threading.ThreadState.Running)
				tAdsWindow.Abort();
		}
		/// <summary>
		/// Updates checked.
		/// </summary>
		private void CheckUpdates_Completed(UpdateInfoEventArgs e)
		{
			// Try to load adverstising after checking updates
			if(e != null){
				this.isUpdateAvailable = e.IsUpdateAvailable;
				ShowAdvertising();
			}
		}
		/// <summary>
		/// Control OnClick event.
		/// </summary>
		public void Control_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			// Check his name
			switch (c.Name)
			{
				case "btnWinMinimize":
					this.WindowState = FormWindowState.Minimized;
					break;
				case "btnWinRestore":
					if (this.WindowState != FormWindowState.Normal)
						this.WindowState = FormWindowState.Normal;
					this.WindowState = FormWindowState.Maximized;
					break;
				case "btnWinExit":
					Application.Exit();
					break;
				case "btnBotStart":
					if (Bot.Get.isBotting){
						Bot.Get.Stop();
					}else{
						Bot.Get.Start();
					}
					break;
				case "btnAnalyzer":
					TabPageV_Option_Click(TabPageV_Control01_Settings, null);
					TabPageH_Option_Click(TabPageH_Settings_Option04, null);
					break;
				case "Login_btnAddSilkroad":
					TabPageV_Option_Click(TabPageV_Control01_Settings, null);
					TabPageH_Option_Click(TabPageH_Settings_Option01, null);
					break;
				case "Login_btnStart":
					switch (c.Text)
					{
						case "START":
							if (Login_cmbxSilkroad.Text == ""){
								MessageBox.Show(this, "Select your Silkroad at first!", "xBot", MessageBoxButtons.OK);
								return;
							}
							// Check if database has been generated previously
							if (!DataManager.ConnectToDatabase(Login_cmbxSilkroad.Text))
							{
								MessageBox.Show(this, "The database \"" + Login_cmbxSilkroad.Text + "\" needs to be created.", "xBot", MessageBoxButtons.OK);
								TabPageV_Option_Click(TabPageV_Control01_Settings, null);
								TabPageH_Option_Click(TabPageH_Settings_Option01, null);
								return;
							}
							
							ListViewItem silkroad = Settings_lstvSilkroads.Items[Login_cmbxSilkroad.Text];

							// SR_Client Path check
							if (!Login_rbnClientless.Checked)
							{
								if ((string)silkroad.SubItems[7].Tag == "")
								{
									MessageBox.Show(this, "You need to select the SRO_Client path first.", "xBot", MessageBoxButtons.OK);
									TabPageV_Option_Click(TabPageV_Control01_Settings, null);
									TabPageH_Option_Click(TabPageH_Settings_Option01, null);
									return;
								}
								DataManager.ClientPath = (string)silkroad.SubItems[7].Tag;
							}

							// Add possibles Gateways/Ports
							List<string> hosts = new List<string>((List<string>)silkroad.SubItems[4].Tag);
							if (hosts.Count == 0)
							{
								return; // Just in case
							}
							List<ushort> ports = new List<ushort>();
							ports.Add((ushort)silkroad.SubItems[3].Tag);

							DataManager.Locale = (byte)silkroad.SubItems[1].Tag;
							DataManager.SR_Client = "SR_Client";
							DataManager.Version = (uint)silkroad.SubItems[2].Tag;

							// Lock Silkroad selection
							Login_btnLauncher.Enabled = false;
							Login_cmbxSilkroad.Enabled = false;

							// Extended protocol Setup
							Bot b = Bot.Get;
							b.SetExtendedProtocol();

							// Creating Proxy
							b.Proxy = new Proxy(Login_rbnClientless.Checked, hosts, ports);
							b.Proxy.RandomHost = (bool)silkroad.SubItems[5].Tag;
							b.Proxy.Start();
							break;
						case "STOP":
							Bot.Get.Proxy.Stop();
							break;
						case "LOGIN":
							if (Login_tbxUsername.Text == "" || Login_tbxPassword.Text == "" || Login_cmbxServer.Text == "")
								return;
							c.Enabled = false;
							InfoManager.SetCredentials(Login_tbxUsername.Text, Login_tbxPassword.Text,Login_cmbxServer.Text);
							break;
						case "SELECT":
							if (Login_cmbxCharacter.Text == "")
								return;
							InfoManager.SetCharacter(Login_cmbxCharacter.Text);
							break;
					}
					break;
				case "Login_btnLauncher":
					if (Login_cmbxSilkroad.Text != "")
					{
						ListViewItem sro = Settings_lstvSilkroads.Items[Login_cmbxSilkroad.Text];
						if ((string)sro.SubItems[6].Tag == "")
						{
							MessageBox.Show(this, "You need to select the Launcher path first.", "xBot", MessageBoxButtons.OK);
							TabPageV_Option_Click(TabPageV_Control01_Settings, null);
							TabPageH_Option_Click(TabPageH_Settings_Option01, null);
						}
						else
						{
							Process.Start((string)sro.SubItems[6].Tag);
						}
					}
					break;
				case "Login_pbxAds":
					ShowAdvertising();
					break;
				case "Character_pgbHP":
				case "Character_pgbMP":
					xProgressBar xProgressBar = (xProgressBar)c;
					if (xProgressBar.Display == xProgressBarDisplay.Percentage)
					{
						xProgressBar.Display = xProgressBarDisplay.Values;
					}
					else
					{
						xProgressBar.Display = xProgressBarDisplay.Percentage;
					}
					xProgressBar.Invalidate();
					break;
				case "Character_btnAddINT":
					if (InfoManager.inGame)
						PacketBuilder.AddStatPointINT();
					break;
				case "Character_btnAddSTR":
					if (InfoManager.inGame)
						PacketBuilder.AddStatPointSTR();
					break;
				case "Inventory_btnItemsRefresh":
					if (c.Tag == null)
					{
						Thread t = new Thread(Inventory_ItemsRefresh);
						c.Tag = t;
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Inventory_btnItemsSort":
					{
						Bot b = Bot.Get;
						if (b.isSorting)
							b.StopInventorySort();
						else
							b.StartInventorySort();
					}
					break;
				case "Inventory_btnAvatarItemsRefresh":
					if (c.Tag == null)
					{
						Thread t = new Thread(Inventory_AvatarItemsRefresh);
						c.Tag = t;
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Inventory_btnOpenCloseStorage":
					{
						SRNpc npc = InfoManager.Npcs.Find(f => f.ServerName.Contains("_WAREHOUSE"));
						if (npc == null)
						{
							LogProcess("NPC storage is not near!");
						}
						else
						{
							if (InfoManager.inStorage)
							{
								PacketBuilder.CloseNPC(npc.UniqueID);
							}
							else
							{
								if (Bot.Get.WaitSelectEntity(npc.UniqueID, 4, 250, "Selecting storage " + npc.Name + "..."))
								{
									if (InfoManager.isStorageLoaded)
										PacketBuilder.TalkNPC(npc.UniqueID, 3);
									else
										PacketBuilder.RequestStorageData(npc.UniqueID);
								}
								else
								{
									LogProcess("Error selecting NPC storage");
								}
							}
						}
					}
					break;
				case "Inventory_btnStorageRefresh":
					if (c.Tag == null)
					{
						Thread t = new Thread(Inventory_StorageRefresh);
						c.Tag = t;
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Inventory_btnPetRefresh":
					if (c.Tag == null)
					{
						Thread t = new Thread(Inventory_PetRefresh);
						c.Tag = t;
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Players_btnRefreshPlayers":
					if(c.Tag == null)
					{
						Thread t = new Thread(Players_Refresh);
						c.Tag = t; // Run max 1 thread
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Players_btnCancelExchange":
					{
						Bot.Get.Proxy.InjectToServer(new Packet(Agent.Opcode.CLIENT_EXCHANGE_EXIT_REQUEST));
					}
					break;
				case "Players_btnExchange":
					{
						switch (c.Text)
						{
							case "Confirm":
								PacketBuilder.ConfirmExchange();
								break;
							case "Approve":
								PacketBuilder.ApproveExchange();
								break;
						}
					}
					break;
				case "Players_btnExchangingGoldEdit":
					{
						if (Players_btnExchange.Text == "Confirm")
						{
							string text = Players_tbxExchangingGold.Text.Replace(".", "");
							ulong gold;
							if (ulong.TryParse(text, out gold))
							{
								if (gold <= InfoManager.Character.Gold)
									PacketBuilder.EditGoldExchange(gold);
								else
									MessageBox.Show(this, "Your gold it's lower than value selected.", "xBot - Exchange", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
							else
							{
								MessageBox.Show(this, "Please, check the value correctly.", "xBot - Exchange", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
					break;

				case "Party_btnAddPlayer":
					if (InfoManager.inGame)
					{
						// Check if already exists
						if (Party_tbxPlayer.Text != "" && !Party_lstvPartyList.Items.ContainsKey(Party_tbxPlayer.Text.ToUpper()))
						{
							ListViewItem player = new ListViewItem(Party_tbxPlayer.Text);
							player.Name = player.Text.ToUpper();
							Party_lstvPartyList.Items.Add(player);

							Party_tbxPlayer.Text = "";
							Settings.SaveCharacterSettings();
							Bot.Get.CheckAutoParty();
						}
					}
					break;
				case "Party_btnAddLeader":
					if (InfoManager.inGame)
					{
						if (Party_tbxLeader.Text != "" && !Party_lstvLeaderList.Items.ContainsKey(Party_tbxLeader.Text.ToUpper()))
						{
							ListViewItem leader = new ListViewItem(Party_tbxLeader.Text);
							leader.Name = leader.Text.ToUpper();
							Party_lstvLeaderList.Items.Add(leader);

							Party_tbxLeader.Text = "";
							Settings.SaveCharacterSettings();
							Bot.Get.CheckAutoParty();
						}
					}
					break;
				case "Party_btnRefreshMatch":
					if (InfoManager.inGame)
					{
						PacketBuilder.RequestPartyMatch();
					}
					break;
				case "Party_btnJoinMatch":
					if (Party_tbxJoinToNumber.Text != "")
					{
						if (InfoManager.inGame && !InfoManager.inParty)
						{
							PacketBuilder.JoinToPartyMatch(uint.Parse(Party_tbxJoinToNumber.Text));
						}
					}
					break;
				case "Party_btnLastPage":
					Party_btnLastPage.Enabled = false;
					if (InfoManager.inGame)
					{
						PacketBuilder.RequestPartyMatch((byte)(byte.Parse(Party_lblPageNumber.Text) - 2));
					}
					break;
				case "Party_btnNextPage":
					Party_btnNextPage.Enabled = false;
					if (InfoManager.inGame)
					{
						PacketBuilder.RequestPartyMatch(byte.Parse(Party_lblPageNumber.Text));
					}
					break;
				case "Guild_btnInfoRefresh":
					if (c.Tag == null)
					{
						Thread t = new Thread(Guild_InfoRefresh);
						c.Tag = t;
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Guild_btnStorageRefresh":
					if (c.Tag == null)
					{
						Thread t = new Thread(Guild_StorageRefresh);
						c.Tag = t;
						t.Priority = ThreadPriority.BelowNormal;
						t.Start();
					}
					break;
				case "Skills_btnAddAttack":
					{
						if (Skills_lstvSkills.SelectedItems.Count > 0) {
							ListView lstvAttackMobType = (ListView)Skills_cmbxAttackMobType.Tag;
							bool itemUpdated = false;
							foreach (ListViewItem item in Skills_lstvSkills.SelectedItems)
							{
								if (!lstvAttackMobType.Items.ContainsKey(item.Name))
								{
									SRSkill skill = (SRSkill)item.Tag;
									// Check if is an attacking skill
									if (skill.isAttackingSkill())
									{
										ListViewItem copy = (ListViewItem)item.Clone();
										copy.Name = item.Name;
										lstvAttackMobType.Items.Add(copy);
										itemUpdated = true;
									}
								}
							}
							if (itemUpdated)
								Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Skills_btnRemAttack":
					{
						ListView lstvAttackMobType = (ListView)Skills_cmbxAttackMobType.Tag;
						if (lstvAttackMobType.SelectedItems.Count > 0)
						{
							foreach (ListViewItem item in lstvAttackMobType.SelectedItems)
								item.Remove();
							if(InfoManager.inGame)
								Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Training_btnGetCoordinates":
					if (InfoManager.inGame && Training_lstvAreas.SelectedItems.Count == 1)
					{
						SRCoord Position = InfoManager.Character.GetRealtimePosition();
						Training_tbxRegion.Text = Position.Region.ToString();
						Training_tbxX.Text = Position.X.ToString();
						Training_tbxY.Text = Position.Y.ToString();
						Training_tbxZ.Text = Position.Z.ToString();
						
						Training_lstvAreas.SelectedItems[0].SubItems[1].Tag = Position.Region;
						Training_lstvAreas.SelectedItems[0].SubItems[2].Tag = Position.X;
						Training_lstvAreas.SelectedItems[0].SubItems[3].Tag = Position.Y;
						Training_lstvAreas.SelectedItems[0].SubItems[4].Tag = Position.Z;

						Settings.SaveCharacterSettings();
					}
					break;
				case "Training_btnLoadScriptPath":
					if (InfoManager.inGame && Training_lstvAreas.SelectedItems.Count == 1)
					{
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your Script file";
							fileDialog.Filter = "Script files (*.xcript)|*.xcript|All files (*.*)|*.*";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								Training_tbxScriptPath.Text = fileDialog.FileName;
								Training_lstvAreas.SelectedItems[0].SubItems[6].Text = fileDialog.FileName;
								Settings.SaveCharacterSettings();
							}
						}
					}
					break;
				case "Training_btnTraceStart":
					if (c.Text == "START")
						Bot.Get.StartTrace(Training_cmbxTracePlayer.Text);
					else
						Bot.Get.StopTrace();
					break;
				case "Stall_btnIGCreateModify":
          switch (c.Text)
					{
            case "Create":
							PacketBuilder.CreateStall(Stall_tbxStallTitle.Text, Stall_tbxStallNote.Text);
							break;
						case "Open":
							PacketBuilder.EditStallState(true);
							break;
						case "Modify":
							PacketBuilder.EditStallState(false);
							break;
					}
					break;
				case "Stall_btnTitleEdit":
					PacketBuilder.EditStallTitle(Stall_tbxTitle.Text);
					break;
				case "Stall_btnClose":
					{
						if (InfoManager.StallerPlayer == null)
							PacketBuilder.CloseStall();
						else
							PacketBuilder.ExitStall();
					}
					break;
				case "Stall_btnNoteEdit":
					PacketBuilder.EditStallNote(Stall_tbxNote.Text);
					break;
				case "Stall_btnAddItem":
					{
						if(Stall_lstvInventoryStall.SelectedItems.Count == 1){
							ulong price;
							if (ulong.TryParse(Stall_tbxPrice.Text.Replace(".", ""), out price))
							{
								byte slotInventory = byte.Parse(Stall_lstvInventoryStall.SelectedItems[0].Name);
								if(Stall_lstvInventoryStall.SelectedItems[0].BackColor == ColorItemHighlight)
								{
									MessageBox.Show(this, "This item is at stall, you have to remove it at first.", "xBot - Stall", MessageBoxButtons.OK, MessageBoxIcon.Error);
									return;
								}
								// Search empty stall slot
								byte slotStall = byte.MaxValue;
								for (byte j = 0; j < Stall_lstvStall.Items.Count; j++)
								{
									if (Stall_lstvStall.Items[j].Tag == null)
									{
										slotStall = j;
										break;
									}
								}
								// check if stall is full
								if (slotStall == byte.MaxValue)
									MessageBox.Show(this, "The stall is full.", "xBot - Stall", MessageBoxButtons.OK, MessageBoxIcon.Error);
								else
									PacketBuilder.AddItemStall(slotStall, slotInventory, ushort.Parse(Stall_tbxQuantity.Text), price);
							}
							else
							{
								MessageBox.Show(this, "Price incorrect. Please, check again.", "xBot - Stall", MessageBoxButtons.OK, MessageBoxIcon.Error);
								Stall_tbxPrice.Focus();
							}
						}
						else
						{
							MessageBox.Show(this, "Select the item to sell at first.", "xBot - Stall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
					break;
				case "GameInfo_btnRefresh":
					GameInfo_tvwObjects.Nodes.Clear();
					if (InfoManager.inGame)
					{
						// Pause drawing, possibly long data
						GameInfo_tvwObjects.BeginUpdate();
						// Add character always
						GameInfo_tvwObjects.Nodes.Add(InfoManager.Character.ToTreeNode());
						// Filter and add entities
						for (int i = 0; i < InfoManager.Entities.Count; i++)
						{
							SREntity entity = InfoManager.Entities.GetAt(i);
							if (entity.isModel())
							{
								SRModel model = (SRModel)entity;
								if (model.isPlayer())
								{
									if (this.GameInfo_cbxPlayer.Checked)
										GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
								}
								else if (model.isNPC())
								{
									SRNpc npc = (SRNpc)model;
									if (npc.isMob())
									{
										if (this.GameInfo_cbxMob.Checked)
											GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
									}
									else if (npc.isGuide())
									{
										if (this.GameInfo_cbxNPC.Checked)
											GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
									}
									else if (npc.isCOS())
									{
										if (this.GameInfo_cbxPet.Checked)
											GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
									}
									else
									{
										if (this.GameInfo_cbxOthers.Checked)
											GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
									}
								}
							}
							else if (entity.isDrop())
							{
								if (this.GameInfo_cbxDrop.Checked)
									GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
							}
							else
							{
								if (this.GameInfo_cbxOthers.Checked)
									GameInfo_tvwObjects.Nodes.Add(entity.ToTreeNode());
							}
						}
						GameInfo_tvwObjects.EndUpdate();
						GameInfo_tbxServerTime.Text = InfoManager.GetServerTime().ToString("HH:mm:ss | dd/MM/yyyy");
					}
					break;
				case "Settings_btnGenerateDatabase":
					if (Settings_lstvSilkroads.SelectedItems.Count == 1)
					{
						ListViewItem sro = Settings_lstvSilkroads.SelectedItems[0];
						// Update database
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your Media.pk2 file";
							fileDialog.FileName = "Media.pk2";
							fileDialog.Filter = "Media.pk2|media.pk2|pk2 files (*.pk2)|*.pk2|All files (*.*)|*.*";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								// Keep memory clean
								using (Pk2Extractor pk2 = new Pk2Extractor(fileDialog.FileName, sro.Name))
								{
									if (pk2.ShowDialog(this) == DialogResult.OK)
									{
										sro.SubItems[1].Tag = pk2.Locale;
										sro.SubItems[2].Tag = pk2.Version;
										sro.SubItems[3].Tag = pk2.Gateport;
										sro.SubItems[4].Tag = pk2.Gateways;

										// Force fill all data to GUI
										sro.Selected = false;
										sro.Selected = true;
										Settings.SaveBotSettings();
									}
								}
							}
						}
					}
					break;
				case "Settings_btnLauncherPath":
					if (Settings_lstvSilkroads.SelectedItems.Count == 1) {
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your Silkroad Launcher";
							fileDialog.FileName = "Silkroad.exe";
							fileDialog.Filter = "Silkroad.exe (*.exe)|silkroad.exe|All executables (*.exe)|*.exe";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								Settings_lstvSilkroads.SelectedItems[0].SubItems[6].Tag = fileDialog.FileName;
								Settings.SaveBotSettings();
							}
						}
					}
					break;
				case "Settings_btnClientPath":
					if (Settings_lstvSilkroads.SelectedItems.Count == 1)
					{
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your SRO_Client.exe";
							fileDialog.FileName = "SRO_Client.exe";
							fileDialog.Filter = "SRO_Client.exe (sro_client.exe)|sro_client.exe|SRO_Client.exe (*.exe)|*.exe";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								Settings_lstvSilkroads.SelectedItems[0].SubItems[7].Tag = fileDialog.FileName;
								Settings.SaveBotSettings();
							}
						}
					}
					break;
				case "Settings_btnAddOpcode":
					if (Settings_tbxFilterOpcode.Text != "")
					{
						ushort opcode;
						string text = Settings_tbxFilterOpcode.Text.ToLower();
						if (text.StartsWith("0x"))
						{
							if (!ushort.TryParse(text.Substring(2, text.Length-2), System.Globalization.NumberStyles.HexNumber,null,out opcode))
								return;
						}
						else
						{
							if (!ushort.TryParse(text, out opcode))
								return;
						}

						// Check if exists
						string key = opcode.ToString();
						if (!Settings_lstvOpcodes.Items.ContainsKey(key))
						{
							ListViewItem item = new ListViewItem(opcode.ToString("X4"));
							item.Name = key;
              Settings_lstvOpcodes.Items.Add(item);
							Settings_tbxFilterOpcode.Text = "";
							Settings.SaveBotSettings();
						}
					}
					break;
				case "Settings_btnInjectPacket":
					{
						Bot b = Bot.Get;
						if (Settings_tbxInjectOpcode.Text != "" && b.Proxy != null && b.Proxy.isRunning)
						{
							// Check opcode
							ushort opcode;
							string text = Settings_tbxInjectOpcode.Text.ToLower();
							if (text.StartsWith("0x"))
							{
								if (!ushort.TryParse(text.Substring(2, text.Length - 2), System.Globalization.NumberStyles.HexNumber, null, out opcode))
									return;
							}
							else
							{
								if (!ushort.TryParse(text, out opcode))
								{
									MessageBox.Show(this, "Error: the opcode is not ushort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									return;
								}
							}
							// Check data
							byte[] data = new byte[0];
							if (Settings_tbxInjectData.Text != "")
							{
								try
								{
									data = Settings_tbxInjectData.Text.ToByteArray();
								}
								catch
								{
									MessageBox.Show(this, "Error: The data is not a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
									return;
								}
							}

							LogPacket("Injecting Packet [0x" + opcode.ToString("X4") + "]");
							Packet p = new Packet(opcode, Settings_cbxInjectEncrypted.Checked, Settings_cbxInjectMassive.Checked, data);
							if (Settings_cmbxInjectTo.SelectedIndex == 0)
							{
								b.Proxy.InjectToServer(p);
							}
							else
							{
								LogPacket(Utility.HexDump(p.GetBytes()) + Environment.NewLine);
								b.Proxy.InjectToClient(p);
							}
						}
					}
					break;
			}
		}
		private void ListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListView l = (ListView)sender;
			switch (l.Name)
			{
				case "Login_lstvServers":
					if (l.SelectedItems.Count == 1)
						Login_cmbxServer.Text = l.SelectedItems[0].Text;
					break;
				case "Login_lstvCharacters":
					if (l.SelectedItems.Count == 1)
						Login_cmbxCharacter.Text = l.SelectedItems[0].Name;
					break;
				case "Training_lstvAreas":
					if (l.SelectedItems.Count == 1)
					{
						ListViewItem area = l.SelectedItems[0];

						Training_tbxRegion.Text = area.SubItems[1].Tag.ToString();
						Training_tbxX.Text = area.SubItems[2].Tag.ToString();
						Training_tbxY.Text = area.SubItems[3].Tag.ToString();
						Training_tbxZ.Text = area.SubItems[4].Tag.ToString();
						Training_tbxRadius.Text = area.SubItems[5].Tag.ToString();
						Training_tbxScriptPath.Text = area.SubItems[6].Text;
					}
					break;
				case "Stall_lstvInventoryStall":
					if (l.SelectedItems.Count == 1)
					{
						SRItem item = ((SRItem)l.SelectedItems[0].Tag);
						Stall_tbxQuantity.Text = item.Quantity.ToString();
					}
					break;
				case "Settings_lstvSilkroads":
					if (l.SelectedItems.Count == 1)
					{
						ListViewItem sro = l.SelectedItems[0];

						Settings_tbxLocale.Text = sro.SubItems[1].Tag.ToString();
						Settings_tbxVersion.Text = sro.SubItems[2].Tag.ToString();
						Settings_tbxPort.Text = sro.SubItems[3].Tag.ToString();
						Settings_lstvHost.Items.Clear();
						foreach (string gateway in (List<string>)sro.SubItems[4].Tag)
							Settings_lstvHost.Items.Add(gateway);
						Settings_cbxRandomHost.Checked = (bool)sro.SubItems[5].Tag;
					}
					break;
			}
		}
		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox c = (ComboBox)sender;
			switch (c.Name)
			{
				case "Login_cmbxServer":
                    // Keep updated the last server selection made
                    foreach (ListViewItem server in Login_lstvServers.Items)
						if (Login_cmbxServer.Text == server.Text)
							InfoManager.ServerID = server.Name;
					break;
				case "Skills_cmbxAttackMobType":
				case "Skills_cmbxBuffMobType":
					// Get the listview control related to the selection of this combobox and show it
					string lstvName = c.Name.Replace("_cmbx","_lstv") + "_"+ c.Text;
					Control lstvControl = c.Parent.Controls[lstvName];
					lstvControl.Visible = true;
					// Check if exists a visible listview control already to hide it
					if (c.Tag != null)
					{
						Control listview = (Control)c.Tag;
						if (listview.Name == lstvName)
							return;
						listview.Visible = false;
					}
					// Save the new listview control activated
					c.Tag = lstvControl;
					break;
				case "Chat_cmbxMsgType":
                    // Activate the Private box on Private chat only
                    Chat_tbxMsgPlayer.Enabled = Chat_cmbxMsgType.Text == "Private";
					break;
			}
		}
		private void Control_CheckedChanged(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			// Save settings
			switch (c.Name)
			{
				case "Login_rbnClient":
					if (Login_rbnClient.Checked)
					{
						Login_btnLauncher.Visible = true;
						Login_btnStart.Location = new Point(110, 15);
					}
					else
					{
						Login_btnLauncher.Visible = false;
						Login_btnStart.Location = new Point(110, 29);
					}
					break;
				case "Character_cbxUseHP":
				case "Character_cbxUseHPGrain":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingHP();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUseMP":
				case "Character_cbxUseMPGrain":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingMP();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUseHPVigor":
				case "Character_cbxUseMPVigor":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingVigor();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePillUniversal":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingUniversal();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePillPurification":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingPurification();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePetHP":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingRecoveryKit();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUseTransportHP":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingRecoveryKit();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePetsPill":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingAbnormalPill();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxUsePetHGP":
					if (InfoManager.inGame)
						Bot.Get.CheckUsingHGP();
					Settings.SaveCharacterSettings();
					break;
				case "Party_rbnSetupExpFree":
				case "Party_rbnSetupItemFree":
				case "Party_cbxSetupMasterInvite":
				case "Party_cbxInviteAll":
				case "Party_cbxInvitePartyList":
					if (InfoManager.inGame)
						Bot.Get.CheckAutoParty();
					Settings.SaveCharacterSettings();
					break;
				case "Party_cbxLeavePartyNoneLeader":
					if (InfoManager.inGame)
						Bot.Get.CheckPartyLeaving();
					Settings.SaveCharacterSettings();
					break;
				case "Party_cbxMatchAutoReform":
					if (InfoManager.inGame)
						Bot.Get.CheckPartyMatchAutoReform();
					Settings.SaveCharacterSettings();
					break;
				case "Character_cbxMessageExp":
				case "Character_cbxMessageUniques":
				case "Character_cbxMessageEvents":
				case "Character_cbxMessagePicks":
				case "Character_cbxAcceptRess":
				case "Character_cbxAcceptRessPartyOnly":
				case "Character_cbxRefuseExchange":
				case "Character_cbxAcceptExchange":
				case "Character_cbxAcceptExchangeLeaderOnly":
				case "Character_cbxConfirmExchange":
				case "Character_cbxApproveExchange":
				case "Party_cbxAcceptOnlyPartySetup":
				case "Party_cbxInviteOnlyPartySetup":
				case "Party_cbxAcceptAll":
				case "Party_cbxAcceptPartyList":
				case "Party_cbxAcceptLeaderList":
				case "Party_cbxRefusePartys":
				case "Party_cbxActivateLeaderCommands":
				case "Party_cbxMatchAcceptAll":
				case "Party_cbxMatchAcceptPartyList":
				case "Party_cbxMatchAcceptLeaderList":
				case "Party_cbxMatchRefuse":
				case "Training_cbxWalkToCenter":
				case "Training_cbxTraceMaster":
				case "Training_cbxTraceDistance":
					Settings.SaveCharacterSettings();
					break;
				case "Settings_cbxRandomHost":
					if (Settings_lstvSilkroads.SelectedItems.Count == 1)
					{
						Settings_lstvSilkroads.SelectedItems[0].SubItems[5].Tag = Settings_cbxRandomHost.Checked;
						Settings.SaveBotSettings();
					}
					break;
				case "Settings_cbxSelectFirstChar":
				case "Settings_cbxCreateChar":
				case "Settings_cbxCreateCharBelow40":
				case "Settings_cbxDeleteChar40to50":
				case "Settings_cbxLoadDefaultConfigs":
					Settings.SaveBotSettings();
					break;
				case "Settings_cbxInjectEncrypted":
					if (Settings_cbxInjectEncrypted.Checked && Settings_cbxInjectMassive.Checked)
						Settings_cbxInjectMassive.Checked = false;
					break;
				case "Settings_cbxInjectMassive":
					if (Settings_cbxInjectMassive.Checked && Settings_cbxInjectEncrypted.Checked)
						Settings_cbxInjectEncrypted.Checked = false;
					break;
			}
		}
		private void Control_TextChanged(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "Character_tbxUseHP":
				case "Character_tbxUseHPVigor":
				case "Character_tbxUseMP":
				case "Character_tbxUseMPVigor":
				case "Character_tbxUsePetHP":
				case "Character_tbxUseTransportHP":
				case "Character_tbxUsePetHGP":
				case "Training_tbxTraceDistance":
					{
						if (c.Text != "")
						{
							// check percentage 0 - 100
							ulong value;
							if (ulong.TryParse(c.Text, out value))
							{
								if (value > 100)
								{
									c.Text = "100";
								}
							}
							else
							{
								string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
								if (fixedText != c.Text)
								{
									c.Text = fixedText;
								}
							}
						}
						else
						{
							c.Text = "0";
						}
						if (InfoManager.inGame)
						{
							switch (c.Name)
							{
								case "Character_tbxUseHP":
									Bot.Get.CheckUsingHP();
									break;
								case "Character_tbxUseHPVigor":
								case "Character_tbxUseMPVigor":
									Bot.Get.CheckUsingVigor();
									break;
								case "Character_tbxUseMP":
									Bot.Get.CheckUsingMP();
									break;
								case "Character_tbxUsePetHP":
								case "Character_tbxUseTransportHP":
									Bot.Get.CheckUsingRecoveryKit();
									break;
								case "Character_tbxUsePetHGP":
									Bot.Get.CheckUsingHGP();
									break;
							}
						}
						Settings.SaveCharacterSettings();
					}
					break;
				case "Players_tbxExchangingGold":
				case "Stall_tbxPrice":
					{
						// Gold color
						ulong gold;
						if (ulong.TryParse(c.Text.Replace(".", ""),out gold))
							c.ForeColor = GetGoldColor(gold);
						else
							c.ForeColor = Color.White;
					}
					break;
				case "Party_tbxJoinToNumber":
					{
						// check uint
						if (c.Text != "")
						{
							ulong value;
							if (ulong.TryParse(c.Text, out value))
							{
								if (value > uint.MaxValue)
								{
									c.Text = uint.MaxValue.ToString();
								}
							}
							else
							{
								string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
								if (fixedText != c.Text)
								{
									c.Text = fixedText;
								}
							}
						}
						else
						{
							c.Text = "0";
						}
					}
					break;
				case "Party_tbxMatchTo":
				case "Party_tbxMatchFrom":
					if (c.Text != "")
					{
						// check byte
						ulong value;
						if (ulong.TryParse(c.Text, out value))
						{
							if (value > 255)
							{
								c.Text = byte.MaxValue.ToString();
							}
						}
						else
						{
							string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
							if (fixedText != c.Text)
							{
								c.Text = fixedText;
							}
						}
					}
					else
					{
						c.Text = "0";
					}
					if (InfoManager.inGame)
					{
						Settings.SaveCharacterSettings();
					}
					break;
				case "Training_cmbxTracePlayer":
					Bot.Get.SetTraceName(c.Text);
					break;
				case "Training_tbxRadius":
					if (InfoManager.inGame && Training_lstvAreas.SelectedItems.Count == 1)
					{
						// Check it's positive number
						ushort dummy;
						if (c.Text == "" || !ushort.TryParse(c.Text,out dummy))
						{
							// Trigger to recursive it
							c.Text = "0";
						}
						else
						{
							Training_lstvAreas.SelectedItems[0].SubItems[5].Tag = int.Parse(c.Text);
							Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Settings_tbxCustomSequence":
					if (c.Text != "")
					{
						string fixedText = FixTextRestriction(c.Text.Trim(), "[0-9]*");
						if (fixedText != c.Text)
						{
							c.Text = fixedText;
							Settings.SaveBotSettings();
						}
					}
					break;
				case "Settings_tbxCustomName":
					if (c.Text != "")
					{
						string fixedText = FixTextRestriction(c.Text.Trim(), "[a-zA-Z0-9_]*");
						if (fixedText != c.Text)
						{
							c.Text = fixedText;
							Settings.SaveBotSettings();
						}
					}
					break;
			}
		}
		private void ComboBox_DropDown(object sender, EventArgs e)
		{
			ComboBox c = (ComboBox)sender;
			switch (c.Name)
			{
				case "Training_cmbxTracePlayer":
					c.Items.Clear();
					if (InfoManager.inGame)
						for (int j = 0; j < InfoManager.Players.Count; j++)
							c.Items.Add(InfoManager.Players.GetAt(j).Name);
					break;
			}
		}
		private void Menu_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem t = (ToolStripMenuItem)sender;
			switch (t.Name)
			{
				case "Menu_NotifyIcon_Exit":
					this.Control_Click(btnWinExit, null);
					break;
				case "Menu_NotifyIcon_About":
					using (About about = new About(this))
						about.ShowDialog(this);
					break;
				case "Menu_NotifyIcon_Update":
					if (isUpdateAvailable)
						AutoUpdater.ShowUpdateForm();
					else if(adsWindow.isLoaded())
						MessageBox.Show(this,"Hey, You have the most recent version!", "xBot - Updates", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case "Menu_NotifyIcon_HideShow":
					if (this.Visible)
					{
						this.Visible = false;
						t.Text = "Show";
					}
					else
					{
						this.Visible = true;
						t.Text = "Hide";
						// Quick window fix
						if (this.WindowState == FormWindowState.Minimized)
							this.WindowState = FormWindowState.Normal;
						this.Activate();
					}
					break;
				case "Menu_btnClientOptions_ShowHide":
					if (InfoManager.inGame)
					{
						Process client = Bot.Get.Proxy.SRO_Client;
						if (client != null)
						{
							(new Thread(() => {
								Color ClientVisible = Color.FromArgb(0, 128, 255);
								IntPtr[] clientWindows = WinAPI.GetProcessWindows(client.Id);
								if (btnClientOptions.ForeColor == ClientVisible)
								{
									LogProcess("Hiding client...");
									// visible > hide and reduce the memory usage
									foreach (IntPtr p in clientWindows)
									{
										WinAPI.ShowWindow(p, WinAPI.SW_HIDE);
										WinAPI.EmptyWorkingSet(p);
									}
									btnClientOptions.InvokeIfRequired(() => {
										btnClientOptions.ForeColor = Color.FromArgb(0, 64, 191);
									});
								}
								else
								{
									LogProcess("Showing client...");
									// hiden > show and make it front
									foreach (IntPtr p in clientWindows)
									{
										WinAPI.ShowWindow(p, WinAPI.SW_SHOW);
										WinAPI.SetForegroundWindow(p);
									}
									btnClientOptions.InvokeIfRequired(()=> {
										btnClientOptions.ForeColor = ClientVisible;
									});
								}
							})).Start();
						}
					}
					break;
				case "Menu_btnClientOptions_GoClientless":
					if (InfoManager.inGame)
					{
						Bot.Get.GoClientless();
					}
					break;
				case "Menu_lstvItems_Use":
					if (InfoManager.inGame && Inventory_lstvItems.SelectedItems.Count == 1)
					{
						if (!Bot.Get.UseItem(byte.Parse(Inventory_lstvItems.SelectedItems[0].Name)))
						{
							Log("This item cannot to be used");
						}
					}
					break;
				case "Menu_lstvItems_Drop":
					if (InfoManager.inGame && Inventory_lstvItems.SelectedItems.Count == 1)
					{
						byte index = (byte)Inventory_lstvItems.SelectedIndices[0];
						if (index >= 13)
						{
							SRItem item = InfoManager.Character.Inventory[index];
							if (item != null)
							{
								if (MessageBox.Show(this, "Are you sure you want to drop \"" + item.Name + "\"?", "xBot - Inventory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									PacketBuilder.DropItem(index);
								}
							}
						}
					}
					break;
				case "Menu_lstvItems_Equip":
					if (InfoManager.inGame && Inventory_lstvItems.SelectedItems.Count == 1)
					{
						if (!Bot.Get.EquipItem((byte)Inventory_lstvItems.SelectedIndices[0]))
						{
							Log("Item cannot be equiped/unequiped");
						}
					}
					break;
				case "Menu_lstvAvatarItems_UnEquip":
					if (InfoManager.inGame && Inventory_lstvAvatarItems.SelectedItems.Count == 1)
					{
						if (!Bot.Get.EquipItem((byte)Inventory_lstvAvatarItems.SelectedIndices[0],true))
						{
							Log("Item cannot be unequiped");
						}
					}
					break;
				case "Menu_lstvStorage_Take":
					if (InfoManager.inStorage && Inventory_lstvStorageItems.SelectedItems.Count == 1)
					{
						int slotstorage = Inventory_lstvStorageItems.SelectedIndices[0];
						if(InfoManager.Character.Storage[slotstorage] != null)
						{
							int emptySlot = InfoManager.Character.Inventory.FindIndex(i => i == null, 13);
							if (emptySlot == -1)
							{
								Log("Inventory is full");
							}
							else
							{
								PacketBuilder.MoveItem((byte)slotstorage,(byte)emptySlot, SRTypes.InventoryItemMovement.StorageToInventory,InfoManager.SelectedEntityUniqueID);
	            }
						}
					}
					break;
				case "Menu_lstvPartyMembers_AddToPartyList":
					if (Party_lstvPartyMembers.SelectedItems.Count == 1)
					{
						Party_tbxPlayer.Text = Party_lstvPartyMembers.SelectedItems[0].Text;
						Control_Click(Party_btnAddPlayer, null);
					}
					break;
				case "Menu_lstvPartyMembers_AddToLeaderList":
					if (Party_lstvPartyMembers.SelectedItems.Count == 1)
					{
						Party_tbxLeader.Text = Party_lstvPartyMembers.SelectedItems[0].Text;
						Control_Click(Party_btnAddLeader, null);
					}
					break;
				case "Menu_lstvPartyMembers_KickPlayer":
					if (Party_lstvPartyMembers.SelectedItems.Count == 1)
					{
						if (InfoManager.inParty)
						{
							PacketBuilder.BanFromParty(uint.Parse(Party_lstvPartyMembers.SelectedItems[0].Name));
						}
					}
					break;
				case "Menu_lstvPartyMembers_LeaveParty":
					if (InfoManager.inParty)
						PacketBuilder.LeaveParty();
					break;
				case "Menu_lstvPartyList_Remove":
					if (Party_lstvPartyList.SelectedItems.Count == 1)
					{
						Party_lstvPartyList.SelectedItems[0].Remove();
						Settings.SaveCharacterSettings();
					}
					break;
				case "Menu_lstvPartyList_RemoveAll":
					if (Party_lstvPartyList.SelectedItems.Count > 0)
					{
						Party_lstvPartyList.Items.Clear();
						Settings.SaveCharacterSettings();
					}
					break;
				case "Menu_lstvLeaderList_Remove":
					if (Party_lstvLeaderList.SelectedItems.Count == 1)
					{
						Party_lstvLeaderList.SelectedItems[0].Remove();
						Settings.SaveCharacterSettings();
						if (InfoManager.inGame)
							Bot.Get.CheckPartyLeaving();
					}
					break;
				case "Menu_lstvLeaderList_RemoveAll":
					if (Party_lstvLeaderList.SelectedItems.Count > 0)
					{
						Party_lstvLeaderList.Items.Clear();
						Settings.SaveCharacterSettings();
						if (InfoManager.inGame)
							Bot.Get.CheckPartyLeaving();
					}
					break;
				case "Menu_lstvPartyMatch_JoinToParty":
					if (Party_lstvPartyMatch.SelectedItems.Count > 0)
					{
						if (InfoManager.inGame && !InfoManager.inParty)
							PacketBuilder.JoinToPartyMatch(uint.Parse(Party_lstvPartyMatch.SelectedItems[0].Name));
					}
					break;
				case "Menu_lstvPartyMatch_PrivateMsg":
					if (Party_lstvPartyMatch.SelectedItems.Count == 1)
					{
						Chat_cmbxMsgType.Text = "Private";
						Chat_tbxMsgPlayer.Text = Party_lstvPartyMatch.SelectedItems[0].SubItems[1].Text;
						TabPageV_Option_Click(TabPageV_Control01_Chat, e); // Go to chat
						TabPageH_Option_Click(TabPageH_Chat_Option02, e); // Go to private
						Chat_tbxMsg.Focus();
					}
					break;
				case "Menu_lstvInventoryExchange_Add":
					if (Players_lstvInventoryExchange.SelectedItems.Count == 1)
					{
						ListViewItem item = Players_lstvInventoryExchange.SelectedItems[0];
						// Check if it's not added yet
						if (item.Tag == null)
							PacketBuilder.AddItemExchange(byte.Parse(item.Name));
					}
					break;
				case "Menu_lstvExchangingItems_Remove":
					if (Players_lstvExchangingItems.SelectedItems.Count == 1)
					{
						int slotExchange = Players_lstvExchangingItems.SelectedIndices[0];
						PacketBuilder.RemoveItemExchange((byte)slotExchange);
					}
					break;
				case "Menu_lstvArea_Add":
					if (InfoManager.inGame)
					{
						// Create 
						int defaultKey = 1;
						string defaultAreaName;
						while (Training_lstvAreas.Items.ContainsKey(defaultAreaName = "Area #" + defaultKey))
						{
							defaultKey++;
						}

						ListViewItem newArea = new ListViewItem(defaultAreaName);
						newArea.Name = defaultAreaName;

						// Name,Region,X,Y,Z,Radius,Script
						// Handling almost everything as TAG since will be required a lot
						ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = (ushort)0; // Region
						newArea.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = 0; // X
						newArea.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = 0; // Y
						newArea.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = 0; // Y
						newArea.SubItems.Add(subitem);
						subitem = new ListViewItem.ListViewSubItem();
						subitem.Tag = 0; // Radius
						newArea.SubItems.Add(subitem);
						newArea.SubItems.Add(""); // Path

						Training_lstvAreas.Items.Add(newArea);
						newArea.Selected = true;

						Settings.SaveCharacterSettings();
					}
					break;
				case "Menu_lstvArea_Remove":
					if (InfoManager.inGame)
					{
						if (Training_lstvAreas.SelectedItems.Count == 1)
						{
							ListViewItem area = Training_lstvAreas.SelectedItems[0];
							// Remove from activated area if it's the same
							if (Training_lstvAreas.Tag != null && area == (ListViewItem)Training_lstvAreas.Tag)
								Training_lstvAreas.Tag = null;
							area.Remove();
							Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Menu_lstvArea_Activate":
					if (InfoManager.inGame)
					{
						if (Training_lstvAreas.SelectedItems.Count == 1)
						{
							// Deactivate
							if(Training_lstvAreas.Tag != null)
							{
								ListViewItem lastActivated = (ListViewItem)Training_lstvAreas.Tag;
								lastActivated.ForeColor = Training_lstvAreas.ForeColor;
							}
							// Activate it
							Training_lstvAreas.SelectedItems[0].ForeColor = Color.FromArgb(0, 180, 255);
							Training_lstvAreas.Tag = Training_lstvAreas.SelectedItems[0];
							Settings.SaveCharacterSettings();
						}
					}
					break;
				case "Menu_lstvSilkroads_Add":
					{
						// Create default name
						int defaultKey = 1;
						string defaultName;
						while (Settings_lstvSilkroads.Items.ContainsKey(defaultName = "Silkroad #" + defaultKey))
							defaultKey++;
						ListViewItem newSro = new ListViewItem(defaultName);
						newSro.Name = defaultName;
						// Create FIRST TIME database
						using (OpenFileDialog fileDialog = new OpenFileDialog())
						{
							fileDialog.Multiselect = false;
							fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
							fileDialog.ValidateNames = true;
							fileDialog.Title = "Select your Media.pk2 file";
							fileDialog.FileName = "Media.pk2";
							fileDialog.Filter = "Media.pk2|media.pk2|pk2 files (*.pk2)|*.pk2|All files (*.*)|*.*";
							fileDialog.FilterIndex = 0;
							if (fileDialog.ShowDialog() == DialogResult.OK)
							{
								// Keep memory clean
								using (Pk2Extractor pk2 = new Pk2Extractor(fileDialog.FileName, newSro.Name))
								{
									if (pk2.ShowDialog(this) == DialogResult.OK)
									{
										ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = pk2.Locale;
										newSro.SubItems.Add(subitem); // 1

										subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = pk2.Version;
										newSro.SubItems.Add(subitem); // 2

										subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = pk2.Gateport;
										newSro.SubItems.Add(subitem); // 3

										subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = pk2.Gateways;
										newSro.SubItems.Add(subitem); // 4

										subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = false; // Random gateway
										newSro.SubItems.Add(subitem); // 5

										subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = ""; // Launcher path
										newSro.SubItems.Add(subitem); // 6

										subitem = new ListViewItem.ListViewSubItem();
										subitem.Tag = ""; // Client path
										newSro.SubItems.Add(subitem); // 7

										Settings_lstvSilkroads.Items.Add(newSro);
										Login_cmbxSilkroad.Items.Add(newSro.Name);
										newSro.Selected = true; // Fill all data to GUI

										// Load paths
										Control_Click(Settings_btnLauncherPath, null);
										Control_Click(Settings_btnClientPath, null);

										Settings.SaveBotSettings();
									}
								}
							}
						}
					}
					break;
				case "Menu_lstvSilkroads_Remove":
					if (Settings_lstvSilkroads.SelectedItems.Count == 1)
					{
						ListViewItem sro = Settings_lstvSilkroads.SelectedItems[0];

						if (!Login_cmbxSilkroad.Enabled && Login_cmbxSilkroad.Text == sro.Name)
							return; // Is actually being used
						Login_cmbxSilkroad.Items.Remove(sro.Name);
						Pk2Extractor.DirectoryDelete(sro.Name);
						
						sro.Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvOpcodes_Remove":
					if (Settings_lstvOpcodes.SelectedItems.Count == 1)
					{
						Settings_lstvOpcodes.SelectedItems[0].Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvOpcodes_RemoveAll":
					if (Settings_lstvOpcodes.Items.Count > 0)
					{
						Settings_lstvOpcodes.Items.Clear();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_lstvOpcodes_Sort":
					if(this.Settings_lstvOpcodes.Sorting == SortOrder.Descending || this.Settings_lstvOpcodes.Sorting == SortOrder.None)
						this.Settings_lstvOpcodes.Sorting = SortOrder.Ascending;
					else
						this.Settings_lstvOpcodes.Sorting = SortOrder.Descending;
					Settings_lstvOpcodes.Sort();
					Settings.SaveBotSettings();
					break;
				case "Menu_rtbxPackets_AutoScroll":
					Settings_rtbxPackets.AutoScroll = t.Checked;
					break;
				case "Menu_rtbxPackets_Clear":
					Settings_rtbxPackets.Clear();
					break;
				case "Menu_lstvHost_Remove":
					if (Settings_lstvHost.SelectedItems.Count == 1)
					{
						Settings_lstvHost.SelectedItems[0].Remove();
						Settings.SaveBotSettings();
					}
					break;
				case "Menu_tvwPlayers_Trace":
					if (InfoManager.inGame)
					{
						SRPlayer player = (SRPlayer)Players_tvwPlayers.SelectedNode.Tag;
						Bot.Get.StartTrace(player.Name);
					}
					break;
				case "Menu_tvwPlayers_Whisper":
					{
						SRPlayer player = (SRPlayer)Players_tvwPlayers.SelectedNode.Tag;
						Chat_cmbxMsgType.Text = "Private";
						Chat_tbxMsgPlayer.Text = player.Name;
						TabPageV_Option_Click(TabPageV_Control01_Chat, e); // Go to chat
						TabPageH_Option_Click(TabPageH_Chat_Option02, e); // Go to private
						Chat_tbxMsg.Focus();
					}
					break;
				case "Menu_tvwPlayers_Exchange":
					{
						SRPlayer player = (SRPlayer)Players_tvwPlayers.SelectedNode.Tag;
						if (InfoManager.inGame && !InfoManager.inExchange)
						{
							PacketBuilder.InviteToExchange(player.UniqueID);
						}
					}
					break;
				case "Menu_tvwPlayers_InviteToParty":
					{
						if (InfoManager.inGame)
						{
							SRPlayer player = (SRPlayer)Players_tvwPlayers.SelectedNode.Tag;
							if (InfoManager.isEntityNear(player.UniqueID))
							{
								if (InfoManager.inParty)
									PacketBuilder.InviteToParty(player.UniqueID);
								else
									PacketBuilder.CreateParty(player.UniqueID, GetPartySetup());
							}
						}
					}
					break;
				case "Menu_tvwPlayers_InviteToGuild":
					{
						if (InfoManager.inGuild)
						{
							SRPlayer player = (SRPlayer)Players_tvwPlayers.SelectedNode.Tag;
							if (InfoManager.isEntityNear(player.UniqueID))
							{
								PacketBuilder.InviteToGuild(player.UniqueID);
							}
						}
					}
					break;
				case "Menu_tvwPlayers_InviteToAcademy":
					{
						//Bot b = Bot.Get;
						//if (b.inAcademy)
						//{
						//SRObject player = (SRObject)Players_tvwPlayers.SelectedNode.Tag;
						//if (Info.Get.isNear((uint)player[SRProperty.UniqueID]))
						//{
						//	PacketBuilder.InviteToAcademy((uint)player[SRProperty.UniqueID]);
						//}
						//}
					}
					break;
				case "Menu_tvwPlayers_Stall":
					{
						SRPlayer player = (SRPlayer)Players_tvwPlayers.SelectedNode.Tag;
						if (InfoManager.isEntityNear(player.UniqueID))
						{
							if (player.InteractionType == SRPlayer.Interaction.OnStall)
							{
								SRCoord playerPosition = player.GetRealtimePosition();
                if (InfoManager.Character.GetRealtimePosition().DistanceTo(playerPosition) > 9.0)
								{
									if (MessageBox.Show(this, "The player is too far away. Do you want to get closer?", "xBot - Players", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
										Bot.Get.MoveTo(playerPosition);
									return;
								}
								else
								{
									PacketBuilder.SelectEntity(player.UniqueID);
									PacketBuilder.OpenStall(player.UniqueID);
									// Change tab automatically (UX)
									TabPageV_Option_Click(TabPageV_Control01_Stall, null);
									TabPageH_Option_Click(TabPageH_Stall_Option01, null);
								}
							}
							else
							{
								MessageBox.Show(this, "The player has no stall activated.", "xBot - Players", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							MessageBox.Show(this, "The player is not near!", "xBot - Players", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					break;
				case "Menu_lstvStall_Buying_Buy":
					if (InfoManager.inGame)
					{
						if (Stall_lstvStall.SelectedItems.Count == 1)
						{
							// 
							if (Stall_lstvStall.SelectedItems[0].Tag != null)
							{
								SRItemStall item = (SRItemStall)Stall_lstvStall.SelectedItems[0].Tag;
								if (item.Price > InfoManager.Character.Gold)
									MessageBox.Show(this, "Your gold is not enough to buy this item.", "xBot - Stall", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								else
									PacketBuilder.BuyStallItem((byte)Stall_lstvStall.SelectedIndices[0]);
							}
						}
					}
					break;
				case "Menu_lstvStall_Selling_Edit":
					if (Stall_btnTitleEdit.Enabled)
					{
						if (Stall_lstvStall.SelectedItems.Count == 1)
						{
							if (Stall_lstvStall.SelectedItems[0].Tag != null)
							{
								ulong newPrice;
								if (ulong.TryParse(Stall_tbxPrice.Text.Replace(".",""), out newPrice))
								{
									SRItemStall item = (SRItemStall)Stall_lstvStall.SelectedItems[0].Tag;
									PacketBuilder.EditItemStall((byte)Stall_lstvStall.SelectedIndices[0], item.Item.Quantity, newPrice);
								}
								else
								{
									MessageBox.Show(this, "Price incorrect. Please, check again.", "xBot - Stall", MessageBoxButtons.OK, MessageBoxIcon.Error);
									Stall_tbxPrice.Focus();
								}
							}
						}
					}
					break;
				case "Menu_lstvStall_Selling_Remove":
					if (Stall_btnTitleEdit.Enabled)
					{
						if (Stall_lstvStall.SelectedItems.Count == 1)
						{
							if (Stall_lstvStall.SelectedItems[0].Tag != null)
							{
								PacketBuilder.RemoveItemStall((byte)Stall_lstvStall.SelectedIndices[0]);
							}
						}
					}
					break;
			}
		}
		private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Menu_Click(this.Menu_NotifyIcon_HideShow, e);
		}
		private void Control_KeyDown(object sender, KeyEventArgs e)
		{
			Control c = (Control)sender;
			if (e.KeyCode == Keys.Enter)
			{
				switch (c.Name)
				{
					case "Chat_tbxMsg":
						if (Chat_tbxMsg.Text != "" && InfoManager.inGame)
						{
							switch (Chat_cmbxMsgType.Text)
							{
								case "All":
									if (!Bot.Get.OnChatSending(Chat_tbxMsg.Text))
										PacketBuilder.SendChatAll(Chat_tbxMsg.Text);
									break;
								case "Private":
									Chat_tbxMsgPlayer.Text = Chat_tbxMsgPlayer.Text.Trim();
									if (Chat_tbxMsgPlayer.Text == "" || Chat_tbxMsgPlayer.Text.StartsWith("*"))
										return;
									PacketBuilder.SendChatPrivate(Chat_tbxMsgPlayer.Text, Chat_tbxMsg.Text);
									LogChatMessage(Chat_rtbxPrivate, Chat_tbxMsgPlayer.Text + "(To)", Chat_tbxMsg.Text);
									break;
								case "Party":
									PacketBuilder.SendChatParty(Chat_tbxMsg.Text);
									break;
								case "Guild":
									PacketBuilder.SendChatGuild(Chat_tbxMsg.Text);
									break;
								case "Union":
									PacketBuilder.SendChatUnion(Chat_tbxMsg.Text);
									break;
								case "Academy":
									PacketBuilder.SendChatAcademy(Chat_tbxMsg.Text);
									break;
								case "Stall":
									if (!InfoManager.inStall)
										return;
									PacketBuilder.SendChatStall(Chat_tbxMsg.Text);
									break;
								case "Global":
									{
										int slotGlobal = InfoManager.Character.Inventory.FindIndex(item => item != null && item.isEtc() && item.ID3 == 3 && item.ID2 == 5, 13);
										if(slotGlobal != -1)
											PacketBuilder.SendChatGlobal((byte)slotGlobal, InfoManager.Character.Inventory[slotGlobal], Chat_tbxMsg.Text);
									}
									break;
							}
							Chat_tbxMsg.Text = "";
						}
						break;
				}
			}
		}
		private void Control_MouseClick(object sender, MouseEventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "btnClientOptions":
					c.ContextMenuStrip.Show(c, new Point(e.X, e.Y));
					break;
			}
		}
		private void Label_pnlBuffs_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && InfoManager.inGame)
			{
				Label l = (Label)sender;
				SRBuff buff = (SRBuff)l.Tag;
				// Avoid disconnect while fixing it
				if (!buff.isTargetRequired)
					PacketBuilder.RemoveBuff(buff.ID);
			}
		}
		private void ListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListView l = (ListView)sender;
			switch (l.Name)
			{
				case "Training_lstvAreas":
					if (e.Label != null)
					{
						if (e.Label == string.Empty)
						{
							e.CancelEdit = true;
							MessageBox.Show(this, "Please, insert a valid name!", "xBot", MessageBoxButtons.OK);
						}
						else
						{
							ListViewItem item = Training_lstvAreas.Items[e.Item];
							if (item.Text != e.Label && Training_lstvAreas.Items.ContainsKey(e.Label))
							{
								e.CancelEdit = true;
								MessageBox.Show(this, "This name is being used!", "xBot", MessageBoxButtons.OK);
							}
							else
							{
								item.Name = e.Label;
								Settings.SaveCharacterSettings();
							}
						}
					}
					break;
				case "Settings_lstvSilkroads":
					if (e.Label != null)
					{
						if (e.Label.Trim() == "")
						{
							e.CancelEdit = true;
							MessageBox.Show(this, "Please, insert a valid name!", "xBot", MessageBoxButtons.OK);
						}
						else
						{
							ListViewItem item = Settings_lstvSilkroads.Items[e.Item];
							if (item.Text != e.Label && Settings_lstvSilkroads.Items.ContainsKey(e.Label))
							{
								e.CancelEdit = true;
								MessageBox.Show(this, "This name is being used!", "xBot", MessageBoxButtons.OK);
							}
							else
							{
								if (!isValidFilename(e.Label)) {
									e.CancelEdit = true;
									MessageBox.Show(this, "This name cannot be used as folder name", "xBot", MessageBoxButtons.OK);
								}
								else
								{
									if(e.Label != item.Text){
										try
										{
											// Change folder name
											string dir = Path.GetDirectoryName(Pk2Extractor.GetDirectory(item.Name));
											string parentDir = Path.GetDirectoryName(dir);
											Directory.Move(dir, Path.Combine(parentDir, e.Label));
										}
										catch {
											e.CancelEdit = true;
											MessageBox.Show(this, "Error while changing name! Please, close all bots at first, and/or try with a different name", "xBot", MessageBoxButtons.OK);
											return;
										}
										// Change combobox
										Login_cmbxSilkroad.Items.Remove(item.Name);
										Login_cmbxSilkroad.Items.Add(e.Label);

										item.Name = e.Label;
										Settings.SaveBotSettings();
									}
								}
							}
						}
					}
					break;
			}
		}
		private void Menu_Minimap_Teleport_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem t = (ToolStripMenuItem)sender;
			SRTeleport teleport = (SRTeleport)t.Tag;
			// Select teleport
			Bot.Get.UseTeleportAsync(teleport, uint.Parse(t.Name));
		}
		private void xListView_DragItemAdding_Cancel(object sender, xListView.DragItemEventArgs e)
		{
			e.Cancel = true;
		}
		private void xListView_DragItemAdding_AttackSkill(object sender, xListView.DragItemEventArgs e)
		{
			xListView l = (xListView)sender;
			SRSkill skill = (SRSkill)e.Item.Tag;
			// Check if is an attacking skill
			if (!skill.isAttackingSkill() || l.Items.ContainsKey(e.Item.Name))
				e.Cancel = true;
		}
		private void xListView_DragItemsChanged(object sender, EventArgs e)
		{
			if (InfoManager.inGame){
				Settings.SaveCharacterSettings();
			}
		}
		private void TrackBar_ValueChanged(object sender, EventArgs e)
		{
			Minimap_pnlMap.Zoom = (byte)Minimap_tbrZoom.Value;
		}
		private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeView t = (TreeView)sender;
			if (e.Button == MouseButtons.Right)
				t.SelectedNode = e.Node;

			switch (t.Name)
			{
				case "Players_tvwPlayers":
					if (e.Node.Level == 0)
					{
						if (e.Button == MouseButtons.Right)
						{
							e.Node.ContextMenuStrip = Menu_tvwPlayers;
						}
					}
					break;
			}
		}
		private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Control c = (Control)sender;
			switch (c.Name)
			{
				case "Stall_tbxStallTitle":
				case "Stall_tbxStallNote":
					Settings.SaveCharacterSettings();
					break;
			}
    }
		private void TabPageV_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				Control tabPageV = (Control)sender;
				tabPageV.Controls[0].Location = new Point(tabPageV.Controls[0].Location.X, tabPageV.Controls[0].Location.Y + e.NewValue);
			}
		}
		#endregion

		public enum ProcessState
		{
			Default,
			Warning,
			Disconnected,
			Error
		}
	}
}