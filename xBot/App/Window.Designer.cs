using System.Windows.Forms;
using xGraphics;

namespace xBot.App
{
	partial class Window
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("TEST");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
			this.TabPageV_Control01 = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option14_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option14 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option13_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option13 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option12_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option12 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option10_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option10 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option09_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option09 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option08_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option08 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option07_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option07 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option06_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option06 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option01_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option01 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option02_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option02 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option05_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option05 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option04_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option04 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option03_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option11_Icon = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option03 = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option11 = new System.Windows.Forms.Button();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.lblHeaderText01 = new System.Windows.Forms.Label();
			this.lblHeaderIcon = new System.Windows.Forms.Label();
			this.btnWinMinimize = new System.Windows.Forms.Button();
			this.btnWinRestore = new System.Windows.Forms.Button();
			this.btnWinExit = new System.Windows.Forms.Button();
			this.lblHeaderText02 = new System.Windows.Forms.Label();
			this.TabPageV_Control01_Option14_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Settings = new System.Windows.Forms.Panel();
			this.TabPageH_Settings_Option04 = new System.Windows.Forms.Button();
			this.TabPageH_Settings_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Settings_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Settings_Option01 = new System.Windows.Forms.Button();
			this.TabPageH_Settings_Option04_Panel = new System.Windows.Forms.Panel();
			this.Settings_gbxPacketInject = new System.Windows.Forms.GroupBox();
			this.Settings_cbxInjectMassive = new System.Windows.Forms.CheckBox();
			this.Settings_cbxInjectEncrypted = new System.Windows.Forms.CheckBox();
			this.Settings_cmbxInjectTo = new System.Windows.Forms.ComboBox();
			this.Settings_tbxInjectData = new System.Windows.Forms.TextBox();
			this.Settings_lblInjectData = new System.Windows.Forms.Label();
			this.Settings_btnInjectPacket = new System.Windows.Forms.Button();
			this.Settings_tbxInjectOpcode = new System.Windows.Forms.TextBox();
			this.Settings_lblInjectOpcode = new System.Windows.Forms.Label();
			this.Settings_gbxPacketFilter = new System.Windows.Forms.GroupBox();
			this.Settings_rbnPacketNotShow = new System.Windows.Forms.RadioButton();
			this.Settings_rbnPacketOnlyShow = new System.Windows.Forms.RadioButton();
			this.Settings_lstvOpcodes = new System.Windows.Forms.ListView();
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvOpcodes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvOpcodes_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvOpcodes_RemoveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.Settings_btnAddOpcode = new System.Windows.Forms.Button();
			this.Settings_tbxFilterOpcode = new System.Windows.Forms.TextBox();
			this.Settings_lblFilterOpcode = new System.Windows.Forms.Label();
			this.Menu_rtbxPackets = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_rtbxPackets_AutoScroll = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_rtbxPackets_AddTimestamp = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_rtbxPackets_Clear = new System.Windows.Forms.ToolStripMenuItem();
			this.Settings_cbxShowPacketClient = new System.Windows.Forms.CheckBox();
			this.Settings_cbxShowPacketServer = new System.Windows.Forms.CheckBox();
			this.TabPageH_Settings_Option01_Panel = new System.Windows.Forms.Panel();
			this.Settings_btnLauncherPath = new System.Windows.Forms.Button();
			this.Settings_btnClientPath = new System.Windows.Forms.Button();
			this.Settings_cbxRandomHost = new System.Windows.Forms.CheckBox();
			this.Settings_btnPK2Path = new System.Windows.Forms.Button();
			this.Settings_lstvHost = new System.Windows.Forms.ListView();
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvHost = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvHost_remove = new System.Windows.Forms.ToolStripMenuItem();
			this.Settings_lblSilkroadName = new System.Windows.Forms.Label();
			this.Settings_tbxSilkroadName = new System.Windows.Forms.TextBox();
			this.Settings_lstrSilkroads = new System.Windows.Forms.TreeView();
			this.Menu_lstrSilkroads = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstrSilkroads_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.Settings_gbxHWID = new System.Windows.Forms.GroupBox();
			this.Settings_panelHWIDdata = new System.Windows.Forms.Panel();
			this.Settings_cbxHWIDSendOnlyOnce = new System.Windows.Forms.CheckBox();
			this.Settings_cmbxHWIDServerSendTo = new System.Windows.Forms.ComboBox();
			this.Settings_cmbxHWIDClientSaveFrom = new System.Windows.Forms.ComboBox();
			this.Settings_tbxHWIDClientOp = new System.Windows.Forms.TextBox();
			this.Settings_lblHWIDClientOp = new System.Windows.Forms.Label();
			this.Settings_tbxHWIDServerOp = new System.Windows.Forms.TextBox();
			this.Settings_lblHWIDServerOp = new System.Windows.Forms.Label();
			this.Settings_btnAddSilkroad = new System.Windows.Forms.Button();
			this.Settings_tbxLocale = new System.Windows.Forms.TextBox();
			this.Settings_lblLocale = new System.Windows.Forms.Label();
			this.Settings_lblHost = new System.Windows.Forms.Label();
			this.Settings_tbxVersion = new System.Windows.Forms.TextBox();
			this.Settings_lblVersion = new System.Windows.Forms.Label();
			this.Settings_lblPort = new System.Windows.Forms.Label();
			this.Settings_tbxPort = new System.Windows.Forms.TextBox();
			this.TabPageH_Settings_Option02_Panel = new System.Windows.Forms.Panel();
			this.Settings_gbxCharacterSelection = new System.Windows.Forms.GroupBox();
			this.Settings_cmbxCreateCharGenre = new System.Windows.Forms.ComboBox();
			this.Settings_cbxLoadDefaultConfigs = new System.Windows.Forms.CheckBox();
			this.Settings_lblCreateCharGenre = new System.Windows.Forms.Label();
			this.Settings_lblCreateCharRace = new System.Windows.Forms.Label();
			this.Settings_cmbxCreateCharRace = new System.Windows.Forms.ComboBox();
			this.Settings_tbxCustomSequence = new System.Windows.Forms.TextBox();
			this.Settings_cbxSelectFirstChar = new System.Windows.Forms.CheckBox();
			this.Settings_cbxCreateChar = new System.Windows.Forms.CheckBox();
			this.Settings_tbxCustomName = new System.Windows.Forms.TextBox();
			this.Settings_cbxDeleteChar40to50 = new System.Windows.Forms.CheckBox();
			this.Settings_lblCustomSequence = new System.Windows.Forms.Label();
			this.Settings_cbxCreateCharBelow40 = new System.Windows.Forms.CheckBox();
			this.Settings_lblCustomName = new System.Windows.Forms.Label();
			this.TabPageH_Settings_Option03_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option03_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Inventory = new System.Windows.Forms.Panel();
			this.TabPageH_Inventory_Option04 = new System.Windows.Forms.Button();
			this.TabPageH_Inventory_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Inventory_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Inventory_Option01 = new System.Windows.Forms.Button();
			this.TabPageH_Inventory_Option01_Panel = new System.Windows.Forms.Panel();
			this.Inventory_lblCapacity = new System.Windows.Forms.Label();
			this.Inventory_btnRefresh = new System.Windows.Forms.Button();
			this.Inventory_lstvItems = new System.Windows.Forms.ListView();
			this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvItems = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvItems_Use = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvItems_Drop = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvItems_Separator01 = new System.Windows.Forms.ToolStripSeparator();
			this.Menu_lstvItems_Equip = new System.Windows.Forms.ToolStripMenuItem();
			this.lstimgIcons = new System.Windows.Forms.ImageList(this.components);
			this.TabPageH_Inventory_Option03_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Inventory_Option02_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Inventory_Option04_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option04_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Party = new System.Windows.Forms.Panel();
			this.TabPageH_Party_Option04 = new System.Windows.Forms.Button();
			this.TabPageH_Party_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Party_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Party_Option01 = new System.Windows.Forms.Button();
			this.TabPageH_Party_Option03_Panel = new System.Windows.Forms.Panel();
			this.Party_btnJoinMatch = new System.Windows.Forms.Button();
			this.Party_tbxJoinToNumber = new System.Windows.Forms.TextBox();
			this.Party_lblJoinToNumber = new System.Windows.Forms.Label();
			this.Party_btnLastPage = new System.Windows.Forms.Button();
			this.Party_btnNextPage = new System.Windows.Forms.Button();
			this.Party_lblPageNumber = new System.Windows.Forms.Label();
			this.Party_btnRefreshMatch = new System.Windows.Forms.Button();
			this.Party_lstvPartyMatch = new System.Windows.Forms.ListView();
			this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvPartyMatch = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvPartyMatch_JoinToParty = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvPartyMatch_PrivateMsg = new System.Windows.Forms.ToolStripMenuItem();
			this.Party_pnlAutoFormMatch = new System.Windows.Forms.Panel();
			this.Party_cbxMatchRefuse = new System.Windows.Forms.CheckBox();
			this.Party_cbxMatchAcceptLeaderList = new System.Windows.Forms.CheckBox();
			this.Party_cbxMatchAcceptPartyList = new System.Windows.Forms.CheckBox();
			this.Party_cbxMatchAcceptAll = new System.Windows.Forms.CheckBox();
			this.Party_cbxMatchAutoReform = new System.Windows.Forms.CheckBox();
			this.Party_tbxMatchTo = new System.Windows.Forms.TextBox();
			this.Party_lblMatchTo = new System.Windows.Forms.Label();
			this.Party_lblMatchTitle = new System.Windows.Forms.Label();
			this.Party_tbxMatchFrom = new System.Windows.Forms.TextBox();
			this.Party_tbxMatchTitle = new System.Windows.Forms.TextBox();
			this.Party_lblMatchFrom = new System.Windows.Forms.Label();
			this.TabPageH_Party_Option02_Panel = new System.Windows.Forms.Panel();
			this.Party_gbxLeaderList = new System.Windows.Forms.GroupBox();
			this.Party_lstvLeaderList = new System.Windows.Forms.ListView();
			this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvLeaderList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvLeaderList_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvLeaderList_RemoveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.Party_btnAddLeader = new System.Windows.Forms.Button();
			this.Party_tbxLeader = new System.Windows.Forms.TextBox();
			this.Party_gbxSetup = new System.Windows.Forms.GroupBox();
			this.Party_cbxSetupMasterInvite = new System.Windows.Forms.CheckBox();
			this.Party_pnlSetupItem = new System.Windows.Forms.Panel();
			this.Party_gbxSetupItem = new System.Windows.Forms.GroupBox();
			this.Party_rbnSetupItemShared = new System.Windows.Forms.RadioButton();
			this.Party_rbnSetupItemFree = new System.Windows.Forms.RadioButton();
			this.Party_pnlSetupExp = new System.Windows.Forms.Panel();
			this.Party_gbxSetupExp = new System.Windows.Forms.GroupBox();
			this.Party_rbnSetupExpShared = new System.Windows.Forms.RadioButton();
			this.Party_rbnSetupExpFree = new System.Windows.Forms.RadioButton();
			this.Party_gbxAcceptInvite = new System.Windows.Forms.GroupBox();
			this.Party_cbxActivateLeaderCommands = new System.Windows.Forms.CheckBox();
			this.Party_cbxLeavePartyNoneLeader = new System.Windows.Forms.CheckBox();
			this.Party_cbxAcceptOnlyPartySetup = new System.Windows.Forms.CheckBox();
			this.Party_cbxInviteOnlyPartySetup = new System.Windows.Forms.CheckBox();
			this.Party_cbxRefuseInvitations = new System.Windows.Forms.CheckBox();
			this.Party_cbxInviteAll = new System.Windows.Forms.CheckBox();
			this.Party_cbxInvitePartyList = new System.Windows.Forms.CheckBox();
			this.Party_cbxAcceptLeaderList = new System.Windows.Forms.CheckBox();
			this.Party_cbxAcceptAll = new System.Windows.Forms.CheckBox();
			this.Party_cbxAcceptPartyList = new System.Windows.Forms.CheckBox();
			this.Party_gbxPlayerList = new System.Windows.Forms.GroupBox();
			this.Party_lstvPartyList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvPartyList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvPartyList_Remove = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvPartyList_RemoveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.Party_btnAddPlayer = new System.Windows.Forms.Button();
			this.Party_tbxPlayer = new System.Windows.Forms.TextBox();
			this.TabPageH_Party_Option01_Panel = new System.Windows.Forms.Panel();
			this.Party_lblCurrentSetup = new System.Windows.Forms.Label();
			this.Party_cbxShowFGWInvites = new System.Windows.Forms.CheckBox();
			this.Party_lstvPartyMembers = new System.Windows.Forms.ListView();
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Menu_lstvPartyMembers = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_lstvPartyMembers_AddTo = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvPartyMembers_AddToPartyList = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvPartyMembers_AddToLeaderList = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvPartyMembers_KickPlayer = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_lstvPartyMembers_Separator01 = new System.Windows.Forms.ToolStripSeparator();
			this.Menu_lstvPartyMembers_LeaveParty = new System.Windows.Forms.ToolStripMenuItem();
			this.TabPageH_Party_Option04_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option05_Panel = new System.Windows.Forms.Panel();
			this.pnlWindow = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option08_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Training = new System.Windows.Forms.Panel();
			this.TabPageH_Training_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Training_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Training_Option01 = new System.Windows.Forms.Button();
			this.TabPageH_Training_Option01_Panel = new System.Windows.Forms.Panel();
			this.Training_btnLoadScriptPath = new System.Windows.Forms.Button();
			this.Training_lblZ = new System.Windows.Forms.Label();
			this.Training_tbxZ = new System.Windows.Forms.TextBox();
			this.Training_lblY = new System.Windows.Forms.Label();
			this.Training_tbxY = new System.Windows.Forms.TextBox();
			this.Training_lblX = new System.Windows.Forms.Label();
			this.Training_tbxX = new System.Windows.Forms.TextBox();
			this.Training_lblRegion = new System.Windows.Forms.Label();
			this.Training_tbxRegion = new System.Windows.Forms.TextBox();
			this.Training_lblScriptPath = new System.Windows.Forms.Label();
			this.Training_tbxScriptPath = new System.Windows.Forms.TextBox();
			this.Training_btnGetCoordinates = new System.Windows.Forms.Button();
			this.Training_lstvAreas = new System.Windows.Forms.ListView();
			this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TabPageH_Training_Option03_Panel = new System.Windows.Forms.Panel();
			this.Training_gbxTrace = new System.Windows.Forms.GroupBox();
			this.Training_cmbxTracePlayer = new System.Windows.Forms.ComboBox();
			this.Training_btnTraceStart = new System.Windows.Forms.Button();
			this.Training_tbxTraceDistance = new System.Windows.Forms.TextBox();
			this.Training_cbxTraceDistance = new System.Windows.Forms.CheckBox();
			this.Training_lblTracePlayer = new System.Windows.Forms.Label();
			this.Training_cbxTraceMaster = new System.Windows.Forms.CheckBox();
			this.TabPageH_Training_Option02_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option01_Panel = new System.Windows.Forms.Panel();
			this.Login_gbxConnection = new System.Windows.Forms.GroupBox();
			this.Login_cbxUseReturnScroll = new System.Windows.Forms.CheckBox();
			this.Login_cbxRelogin = new System.Windows.Forms.CheckBox();
			this.Login_cbxGoClientless = new System.Windows.Forms.CheckBox();
			this.Login_btnStart = new System.Windows.Forms.Button();
			this.Login_rbnClientless = new System.Windows.Forms.RadioButton();
			this.Login_rbnClient = new System.Windows.Forms.RadioButton();
			this.Login_btnLauncher = new System.Windows.Forms.Button();
			this.Login_gbxLogin = new System.Windows.Forms.GroupBox();
			this.Login_tbxCaptcha = new System.Windows.Forms.TextBox();
			this.Login_lblCaptcha = new System.Windows.Forms.Label();
			this.Login_cmbxCharacter = new System.Windows.Forms.ComboBox();
			this.Login_cmbxServer = new System.Windows.Forms.ComboBox();
			this.Login_tbxPassword = new System.Windows.Forms.TextBox();
			this.Login_lblPassword = new System.Windows.Forms.Label();
			this.Login_lblCharacter = new System.Windows.Forms.Label();
			this.Login_lblServer = new System.Windows.Forms.Label();
			this.Login_tbxUsername = new System.Windows.Forms.TextBox();
			this.Login_lblUsername = new System.Windows.Forms.Label();
			this.Login_btnAddSilkroad = new System.Windows.Forms.Button();
			this.Login_lblSilkroad = new System.Windows.Forms.Label();
			this.Login_cmbxSilkroad = new System.Windows.Forms.ComboBox();
			this.Login_gbxAdvertising = new System.Windows.Forms.GroupBox();
			this.Login_pbxAds = new System.Windows.Forms.PictureBox();
			this.Login_gbxServers = new System.Windows.Forms.GroupBox();
			this.Login_lstvServers = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Login_gbxCharacters = new System.Windows.Forms.GroupBox();
			this.Login_lstvCharacters = new System.Windows.Forms.ListView();
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TabPageV_Control01_Option07_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Skills = new System.Windows.Forms.Panel();
			this.TabPageH_Skills_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Skills_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Skills_Option01 = new System.Windows.Forms.Button();
			this.Skills_lstvSkills = new System.Windows.Forms.ListView();
			this.columnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TabPageH_Skills_Option01_Panel = new System.Windows.Forms.Panel();
			this.Skills_lstvAttackMobType_Event = new System.Windows.Forms.ListView();
			this.columnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_Unique = new System.Windows.Forms.ListView();
			this.columnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_Elite = new System.Windows.Forms.ListView();
			this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_PartyGiant = new System.Windows.Forms.ListView();
			this.columnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_PartyChampion = new System.Windows.Forms.ListView();
			this.columnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_PartyGeneral = new System.Windows.Forms.ListView();
			this.columnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_Giant = new System.Windows.Forms.ListView();
			this.columnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_lstvAttackMobType_Champion = new System.Windows.Forms.ListView();
			this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Skills_cmbxAttackMobType = new System.Windows.Forms.ComboBox();
			this.Skills_lstvAttackMobType_General = new System.Windows.Forms.ListView();
			this.columnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TabPageH_Skills_Option02_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Skills_Option03_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option02_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Character = new System.Windows.Forms.Panel();
			this.TabPageH_Character_Option04 = new System.Windows.Forms.Button();
			this.TabPageH_Character_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Character_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Character_Option01 = new System.Windows.Forms.Button();
			this.TabPageH_Character_Option01_Panel = new System.Windows.Forms.Panel();
			this.Character_lstvBuffs = new System.Windows.Forms.ListView();
			this.Character_lblCoordY = new System.Windows.Forms.Label();
			this.Character_lblCoordX = new System.Windows.Forms.Label();
			this.Character_lblCoords = new System.Windows.Forms.Label();
			this.Character_lblLocation = new System.Windows.Forms.Label();
			this.Character_lblLocationText = new System.Windows.Forms.Label();
			this.Character_lblSP = new System.Windows.Forms.Label();
			this.Character_lblSPText = new System.Windows.Forms.Label();
			this.Character_lblGold = new System.Windows.Forms.Label();
			this.Character_lblGoldText = new System.Windows.Forms.Label();
			this.Character_lblJobLevel = new System.Windows.Forms.Label();
			this.Character_gbxStatPoints = new System.Windows.Forms.GroupBox();
			this.Character_lblStatPoints = new System.Windows.Forms.Label();
			this.Character_lblINT = new System.Windows.Forms.Label();
			this.Character_lblSTR = new System.Windows.Forms.Label();
			this.Character_btnAddSTR = new System.Windows.Forms.Button();
			this.Character_lblAddINT = new System.Windows.Forms.Label();
			this.Character_lblAddSTR = new System.Windows.Forms.Label();
			this.Character_btnAddINT = new System.Windows.Forms.Button();
			this.Character_lblLevel = new System.Windows.Forms.Label();
			this.Character_gbxMessageFilter = new System.Windows.Forms.GroupBox();
			this.Character_cbxMessageEvents = new System.Windows.Forms.CheckBox();
			this.Character_cbxMessagePicks = new System.Windows.Forms.CheckBox();
			this.Character_cbxMessageUniques = new System.Windows.Forms.CheckBox();
			this.Character_cbxMessageExp = new System.Windows.Forms.CheckBox();
			this.TabPageH_Character_Option02_Panel = new System.Windows.Forms.Panel();
			this.Character_gbxPotionPet = new System.Windows.Forms.GroupBox();
			this.Character_tbxUsePetHGP = new System.Windows.Forms.TextBox();
			this.Character_cbxUsePetHGP = new System.Windows.Forms.CheckBox();
			this.Character_cbxUsePetsPill = new System.Windows.Forms.CheckBox();
			this.Character_tbxUseTransportHP = new System.Windows.Forms.TextBox();
			this.Character_cbxUseTransportHP = new System.Windows.Forms.CheckBox();
			this.Character_tbxUsePetHP = new System.Windows.Forms.TextBox();
			this.Character_cbxUsePetHP = new System.Windows.Forms.CheckBox();
			this.Character_gbxPotionsPlayer = new System.Windows.Forms.GroupBox();
			this.Character_tbxUseHP = new System.Windows.Forms.TextBox();
			this.Character_tbxUseMP = new System.Windows.Forms.TextBox();
			this.Character_cbxUsePillPurification = new System.Windows.Forms.CheckBox();
			this.Character_cbxUseMPGrain = new System.Windows.Forms.CheckBox();
			this.Character_cbxUsePillUniversal = new System.Windows.Forms.CheckBox();
			this.Character_tbxUseMPVigor = new System.Windows.Forms.TextBox();
			this.Character_tbxUseHPVigor = new System.Windows.Forms.TextBox();
			this.Character_cbxUseMPVigor = new System.Windows.Forms.CheckBox();
			this.Character_cbxUseHP = new System.Windows.Forms.CheckBox();
			this.Character_cbxUseHPGrain = new System.Windows.Forms.CheckBox();
			this.Character_cbxUseHPVigor = new System.Windows.Forms.CheckBox();
			this.Character_cbxUseMP = new System.Windows.Forms.CheckBox();
			this.TabPageH_Character_Option04_Panel = new System.Windows.Forms.Panel();
			this.Character_cbxAcceptRessPartyOnly = new System.Windows.Forms.CheckBox();
			this.Character_cbxAcceptRess = new System.Windows.Forms.CheckBox();
			this.TabPageH_Character_Option03_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option13_Panel = new System.Windows.Forms.Panel();
			this.GameInfo_cbxOthers = new System.Windows.Forms.CheckBox();
			this.GameInfo_cbxPet = new System.Windows.Forms.CheckBox();
			this.GameInfo_cbxDrop = new System.Windows.Forms.CheckBox();
			this.GameInfo_cbxMob = new System.Windows.Forms.CheckBox();
			this.GameInfo_cbxPlayer = new System.Windows.Forms.CheckBox();
			this.GameInfo_btnRefresh = new System.Windows.Forms.Button();
			this.GameInfo_tbxServerTime = new System.Windows.Forms.TextBox();
			this.GameInfo_lblServerTime = new System.Windows.Forms.Label();
			this.GameInfo_lstrObjects = new System.Windows.Forms.TreeView();
			this.GameInfo_cbxNPC = new System.Windows.Forms.CheckBox();
			this.TabPageV_Control01_Option11_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option08 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option07 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option06 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option05 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option04 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option03 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option02 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option01 = new System.Windows.Forms.Button();
			this.TabPageH_Chat_Option01_Panel = new System.Windows.Forms.Panel();
			this.Chat_panel = new System.Windows.Forms.Panel();
			this.Chat_tbxMsgPlayer = new System.Windows.Forms.TextBox();
			this.Chat_cmbxMsgType = new System.Windows.Forms.ComboBox();
			this.Chat_tbxMsg = new System.Windows.Forms.TextBox();
			this.TabPageH_Chat_Option08_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option07_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option06_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option05_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option04_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option03_Panel = new System.Windows.Forms.Panel();
			this.TabPageH_Chat_Option02_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option12_Panel = new System.Windows.Forms.Panel();
			this.Minimap_panelMap = new System.Windows.Forms.Panel();
			this.Minimap_btnLoadMap = new System.Windows.Forms.Button();
			this.Minimap_panelCoords = new System.Windows.Forms.Panel();
			this.Minimap_tbxRegion = new System.Windows.Forms.TextBox();
			this.Minimap_lblX = new System.Windows.Forms.Label();
			this.Minimap_tbxX = new System.Windows.Forms.TextBox();
			this.Minimap_lblY = new System.Windows.Forms.Label();
			this.Minimap_tbxY = new System.Windows.Forms.TextBox();
			this.Minimap_lblRegion = new System.Windows.Forms.Label();
			this.Minimap_lblZ = new System.Windows.Forms.Label();
			this.Minimap_tbxZ = new System.Windows.Forms.TextBox();
			this.TabPageV_Control01_Option10_Panel = new System.Windows.Forms.Panel();
			this.TabPageV_Control01_Option09_Panel = new System.Windows.Forms.Panel();
			this.btnClientOptions = new System.Windows.Forms.Button();
			this.Menu_btnClientOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_btnClientOptions_ShowHide = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_btnClientOptions_GoClientless = new System.Windows.Forms.ToolStripMenuItem();
			this.btnBotStart = new System.Windows.Forms.Button();
			this.TabPageV_Control01_Option06_Panel = new System.Windows.Forms.Panel();
			this.btnAnalyzer = new System.Windows.Forms.Button();
			this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
			this.lblBotState = new System.Windows.Forms.Label();
			this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.Menu_NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Menu_NotifyIcon_HideShow = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_NotifyIcon_About = new System.Windows.Forms.ToolStripMenuItem();
			this.Menu_NotifyIcon_Separator01 = new System.Windows.Forms.ToolStripSeparator();
			this.Menu_NotifyIcon_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.Character_pgbJobExp = new xGraphics.xProgressBar();
			this.Character_rtbxMessageFilter = new xGraphics.xRichTextBox();
			this.Character_pgbExp = new xGraphics.xProgressBar();
			this.Character_pgbMP = new xGraphics.xProgressBar();
			this.Character_pgbHP = new xGraphics.xProgressBar();
			this.Settings_rtbxPackets = new xGraphics.xRichTextBox();
			this.Settings_rtbxHWIDdata = new xGraphics.xRichTextBox();
			this.Chat_rtbxAll = new xGraphics.xRichTextBox();
			this.Chat_rtbxGlobal = new xGraphics.xRichTextBox();
			this.Chat_rtbxStall = new xGraphics.xRichTextBox();
			this.Chat_rtbxAcademy = new xGraphics.xRichTextBox();
			this.Chat_rtbxUnion = new xGraphics.xRichTextBox();
			this.Chat_rtbxGuild = new xGraphics.xRichTextBox();
			this.Chat_rtbxParty = new xGraphics.xRichTextBox();
			this.Chat_rtbxPrivate = new xGraphics.xRichTextBox();
			this.rtbxLogs = new xGraphics.xRichTextBox();
			this.TabPageV_Control01.SuspendLayout();
			this.pnlHeader.SuspendLayout();
			this.TabPageV_Control01_Option14_Panel.SuspendLayout();
			this.TabPageH_Settings.SuspendLayout();
			this.TabPageH_Settings_Option04_Panel.SuspendLayout();
			this.Settings_gbxPacketInject.SuspendLayout();
			this.Settings_gbxPacketFilter.SuspendLayout();
			this.Menu_lstvOpcodes.SuspendLayout();
			this.Menu_rtbxPackets.SuspendLayout();
			this.TabPageH_Settings_Option01_Panel.SuspendLayout();
			this.Menu_lstvHost.SuspendLayout();
			this.Menu_lstrSilkroads.SuspendLayout();
			this.Settings_gbxHWID.SuspendLayout();
			this.Settings_panelHWIDdata.SuspendLayout();
			this.TabPageH_Settings_Option02_Panel.SuspendLayout();
			this.Settings_gbxCharacterSelection.SuspendLayout();
			this.TabPageV_Control01_Option03_Panel.SuspendLayout();
			this.TabPageH_Inventory.SuspendLayout();
			this.TabPageH_Inventory_Option01_Panel.SuspendLayout();
			this.Menu_lstvItems.SuspendLayout();
			this.TabPageV_Control01_Option04_Panel.SuspendLayout();
			this.TabPageH_Party.SuspendLayout();
			this.TabPageH_Party_Option03_Panel.SuspendLayout();
			this.Menu_lstvPartyMatch.SuspendLayout();
			this.Party_pnlAutoFormMatch.SuspendLayout();
			this.TabPageH_Party_Option02_Panel.SuspendLayout();
			this.Party_gbxLeaderList.SuspendLayout();
			this.Menu_lstvLeaderList.SuspendLayout();
			this.Party_gbxSetup.SuspendLayout();
			this.Party_pnlSetupItem.SuspendLayout();
			this.Party_gbxSetupItem.SuspendLayout();
			this.Party_pnlSetupExp.SuspendLayout();
			this.Party_gbxSetupExp.SuspendLayout();
			this.Party_gbxAcceptInvite.SuspendLayout();
			this.Party_gbxPlayerList.SuspendLayout();
			this.Menu_lstvPartyList.SuspendLayout();
			this.TabPageH_Party_Option01_Panel.SuspendLayout();
			this.Menu_lstvPartyMembers.SuspendLayout();
			this.pnlWindow.SuspendLayout();
			this.TabPageV_Control01_Option08_Panel.SuspendLayout();
			this.TabPageH_Training.SuspendLayout();
			this.TabPageH_Training_Option01_Panel.SuspendLayout();
			this.TabPageH_Training_Option03_Panel.SuspendLayout();
			this.Training_gbxTrace.SuspendLayout();
			this.TabPageV_Control01_Option01_Panel.SuspendLayout();
			this.Login_gbxConnection.SuspendLayout();
			this.Login_gbxLogin.SuspendLayout();
			this.Login_gbxAdvertising.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Login_pbxAds)).BeginInit();
			this.Login_gbxServers.SuspendLayout();
			this.Login_gbxCharacters.SuspendLayout();
			this.TabPageV_Control01_Option07_Panel.SuspendLayout();
			this.TabPageH_Skills.SuspendLayout();
			this.TabPageH_Skills_Option01_Panel.SuspendLayout();
			this.TabPageV_Control01_Option02_Panel.SuspendLayout();
			this.TabPageH_Character.SuspendLayout();
			this.TabPageH_Character_Option01_Panel.SuspendLayout();
			this.Character_gbxStatPoints.SuspendLayout();
			this.Character_gbxMessageFilter.SuspendLayout();
			this.TabPageH_Character_Option02_Panel.SuspendLayout();
			this.Character_gbxPotionPet.SuspendLayout();
			this.Character_gbxPotionsPlayer.SuspendLayout();
			this.TabPageH_Character_Option04_Panel.SuspendLayout();
			this.TabPageV_Control01_Option13_Panel.SuspendLayout();
			this.TabPageV_Control01_Option11_Panel.SuspendLayout();
			this.TabPageH_Chat.SuspendLayout();
			this.TabPageH_Chat_Option01_Panel.SuspendLayout();
			this.Chat_panel.SuspendLayout();
			this.TabPageH_Chat_Option08_Panel.SuspendLayout();
			this.TabPageH_Chat_Option07_Panel.SuspendLayout();
			this.TabPageH_Chat_Option06_Panel.SuspendLayout();
			this.TabPageH_Chat_Option05_Panel.SuspendLayout();
			this.TabPageH_Chat_Option04_Panel.SuspendLayout();
			this.TabPageH_Chat_Option03_Panel.SuspendLayout();
			this.TabPageH_Chat_Option02_Panel.SuspendLayout();
			this.TabPageV_Control01_Option12_Panel.SuspendLayout();
			this.Minimap_panelMap.SuspendLayout();
			this.Minimap_panelCoords.SuspendLayout();
			this.Menu_btnClientOptions.SuspendLayout();
			this.Menu_NotifyIcon.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabPageV_Control01
			// 
			this.TabPageV_Control01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.TabPageV_Control01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option14_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option14);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option13_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option13);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option12_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option12);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option10_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option10);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option09_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option09);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option08_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option08);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option07_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option07);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option06_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option06);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option01_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option01);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option02_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option02);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option05_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option05);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option04_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option04);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option03_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option11_Icon);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option03);
			this.TabPageV_Control01.Controls.Add(this.TabPageV_Control01_Option11);
			this.TabPageV_Control01.Location = new System.Drawing.Point(5, 45);
			this.TabPageV_Control01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01.Name = "TabPageV_Control01";
			this.TabPageV_Control01.Size = new System.Drawing.Size(125, 422);
			this.TabPageV_Control01.TabIndex = 3;
			// 
			// TabPageV_Control01_Option14_Icon
			// 
			this.TabPageV_Control01_Option14_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option14_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option14_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option14_Icon.Location = new System.Drawing.Point(5, 396);
			this.TabPageV_Control01_Option14_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option14_Icon.Name = "TabPageV_Control01_Option14_Icon";
			this.TabPageV_Control01_Option14_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option14_Icon.TabIndex = 28;
			this.TabPageV_Control01_Option14_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option14_Icon.Text = "";
			this.TabPageV_Control01_Option14_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option14_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option14_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option14_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option14_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option14
			// 
			this.TabPageV_Control01_Option14.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option14.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option14.Location = new System.Drawing.Point(0, 390);
			this.TabPageV_Control01_Option14.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option14.Name = "TabPageV_Control01_Option14";
			this.TabPageV_Control01_Option14.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option14.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option14.TabIndex = 27;
			this.TabPageV_Control01_Option14.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option14.Text = "Settings";
			this.TabPageV_Control01_Option14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option14.UseVisualStyleBackColor = false;
			this.TabPageV_Control01_Option14.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option14.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option14.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option13_Icon
			// 
			this.TabPageV_Control01_Option13_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option13_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option13_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option13_Icon.Location = new System.Drawing.Point(5, 366);
			this.TabPageV_Control01_Option13_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option13_Icon.Name = "TabPageV_Control01_Option13_Icon";
			this.TabPageV_Control01_Option13_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option13_Icon.TabIndex = 26;
			this.TabPageV_Control01_Option13_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option13_Icon.Text = "";
			this.TabPageV_Control01_Option13_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option13_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option13_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option13_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option13_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option13
			// 
			this.TabPageV_Control01_Option13.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option13.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option13.Location = new System.Drawing.Point(0, 360);
			this.TabPageV_Control01_Option13.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option13.Name = "TabPageV_Control01_Option13";
			this.TabPageV_Control01_Option13.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option13.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option13.TabIndex = 25;
			this.TabPageV_Control01_Option13.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option13.Text = "Game Info";
			this.TabPageV_Control01_Option13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option13.UseVisualStyleBackColor = false;
			this.TabPageV_Control01_Option13.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option13.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option13.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option12_Icon
			// 
			this.TabPageV_Control01_Option12_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option12_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option12_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option12_Icon.Location = new System.Drawing.Point(5, 336);
			this.TabPageV_Control01_Option12_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option12_Icon.Name = "TabPageV_Control01_Option12_Icon";
			this.TabPageV_Control01_Option12_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option12_Icon.TabIndex = 24;
			this.TabPageV_Control01_Option12_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option12_Icon.Text = "";
			this.TabPageV_Control01_Option12_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option12_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option12_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option12_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option12_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option12
			// 
			this.TabPageV_Control01_Option12.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option12.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option12.Location = new System.Drawing.Point(0, 330);
			this.TabPageV_Control01_Option12.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option12.Name = "TabPageV_Control01_Option12";
			this.TabPageV_Control01_Option12.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option12.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option12.TabIndex = 23;
			this.TabPageV_Control01_Option12.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option12.Text = "Minimap";
			this.TabPageV_Control01_Option12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option12.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option12.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option12.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option12.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option10_Icon
			// 
			this.TabPageV_Control01_Option10_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option10_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option10_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option10_Icon.Location = new System.Drawing.Point(5, 276);
			this.TabPageV_Control01_Option10_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option10_Icon.Name = "TabPageV_Control01_Option10_Icon";
			this.TabPageV_Control01_Option10_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option10_Icon.TabIndex = 22;
			this.TabPageV_Control01_Option10_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option10_Icon.Text = "";
			this.TabPageV_Control01_Option10_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option10_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option10_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option10_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option10_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option10
			// 
			this.TabPageV_Control01_Option10.Cursor = System.Windows.Forms.Cursors.Default;
			this.TabPageV_Control01_Option10.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option10.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option10.Location = new System.Drawing.Point(0, 270);
			this.TabPageV_Control01_Option10.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option10.Name = "TabPageV_Control01_Option10";
			this.TabPageV_Control01_Option10.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option10.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option10.TabIndex = 10;
			this.TabPageV_Control01_Option10.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option10.Text = "Stall";
			this.TabPageV_Control01_Option10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option10.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option10.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option10.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option10.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option09_Icon
			// 
			this.TabPageV_Control01_Option09_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option09_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option09_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option09_Icon.Location = new System.Drawing.Point(5, 246);
			this.TabPageV_Control01_Option09_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option09_Icon.Name = "TabPageV_Control01_Option09_Icon";
			this.TabPageV_Control01_Option09_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option09_Icon.TabIndex = 20;
			this.TabPageV_Control01_Option09_Icon.Tag = "Font Awesome 5 Pro Regular";
			this.TabPageV_Control01_Option09_Icon.Text = "";
			this.TabPageV_Control01_Option09_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option09_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option09_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option09_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option09_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option09
			// 
			this.TabPageV_Control01_Option09.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option09.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option09.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option09.Location = new System.Drawing.Point(0, 240);
			this.TabPageV_Control01_Option09.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option09.Name = "TabPageV_Control01_Option09";
			this.TabPageV_Control01_Option09.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option09.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option09.TabIndex = 9;
			this.TabPageV_Control01_Option09.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option09.Text = "Town";
			this.TabPageV_Control01_Option09.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option09.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option09.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option09.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option09.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option08_Icon
			// 
			this.TabPageV_Control01_Option08_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option08_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option08_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option08_Icon.Location = new System.Drawing.Point(5, 216);
			this.TabPageV_Control01_Option08_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option08_Icon.Name = "TabPageV_Control01_Option08_Icon";
			this.TabPageV_Control01_Option08_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option08_Icon.TabIndex = 18;
			this.TabPageV_Control01_Option08_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option08_Icon.Text = "";
			this.TabPageV_Control01_Option08_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option08_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option08_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option08_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option08_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option08
			// 
			this.TabPageV_Control01_Option08.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option08.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option08.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option08.Location = new System.Drawing.Point(0, 210);
			this.TabPageV_Control01_Option08.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option08.Name = "TabPageV_Control01_Option08";
			this.TabPageV_Control01_Option08.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option08.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option08.TabIndex = 8;
			this.TabPageV_Control01_Option08.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option08.Text = "Training";
			this.TabPageV_Control01_Option08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option08.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option08.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option08.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option08.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option07_Icon
			// 
			this.TabPageV_Control01_Option07_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option07_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option07_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option07_Icon.Location = new System.Drawing.Point(5, 186);
			this.TabPageV_Control01_Option07_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option07_Icon.Name = "TabPageV_Control01_Option07_Icon";
			this.TabPageV_Control01_Option07_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option07_Icon.TabIndex = 16;
			this.TabPageV_Control01_Option07_Icon.Tag = "Font Awesome 5 Pro Regular";
			this.TabPageV_Control01_Option07_Icon.Text = "";
			this.TabPageV_Control01_Option07_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option07_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option07_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option07_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option07_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option07
			// 
			this.TabPageV_Control01_Option07.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option07.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option07.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option07.Location = new System.Drawing.Point(0, 180);
			this.TabPageV_Control01_Option07.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option07.Name = "TabPageV_Control01_Option07";
			this.TabPageV_Control01_Option07.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option07.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option07.TabIndex = 7;
			this.TabPageV_Control01_Option07.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option07.Text = "Skills";
			this.TabPageV_Control01_Option07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option07.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option07.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option07.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option07.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option06_Icon
			// 
			this.TabPageV_Control01_Option06_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option06_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option06_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.TabPageV_Control01_Option06_Icon.Location = new System.Drawing.Point(5, 156);
			this.TabPageV_Control01_Option06_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option06_Icon.Name = "TabPageV_Control01_Option06_Icon";
			this.TabPageV_Control01_Option06_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option06_Icon.TabIndex = 14;
			this.TabPageV_Control01_Option06_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option06_Icon.Text = "";
			this.TabPageV_Control01_Option06_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option06_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option06_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option06_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option06_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option06
			// 
			this.TabPageV_Control01_Option06.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option06.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option06.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option06.Location = new System.Drawing.Point(0, 150);
			this.TabPageV_Control01_Option06.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option06.Name = "TabPageV_Control01_Option06";
			this.TabPageV_Control01_Option06.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option06.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option06.TabIndex = 6;
			this.TabPageV_Control01_Option06.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option06.Text = "Academy";
			this.TabPageV_Control01_Option06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option06.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option06.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option06.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option06.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option01_Icon
			// 
			this.TabPageV_Control01_Option01_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option01_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option01_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option01_Icon.Location = new System.Drawing.Point(5, 5);
			this.TabPageV_Control01_Option01_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option01_Icon.Name = "TabPageV_Control01_Option01_Icon";
			this.TabPageV_Control01_Option01_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option01_Icon.TabIndex = 13;
			this.TabPageV_Control01_Option01_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option01_Icon.Text = "";
			this.TabPageV_Control01_Option01_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option01_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option01_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option01_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option01_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option01
			// 
			this.TabPageV_Control01_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option01.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageV_Control01_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option01.Name = "TabPageV_Control01_Option01";
			this.TabPageV_Control01_Option01.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option01.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option01.TabIndex = 1;
			this.TabPageV_Control01_Option01.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option01.Text = "Login";
			this.TabPageV_Control01_Option01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option01.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option01.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option01.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option01.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option02_Icon
			// 
			this.TabPageV_Control01_Option02_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option02_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option02_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option02_Icon.Location = new System.Drawing.Point(5, 35);
			this.TabPageV_Control01_Option02_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option02_Icon.Name = "TabPageV_Control01_Option02_Icon";
			this.TabPageV_Control01_Option02_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option02_Icon.TabIndex = 10;
			this.TabPageV_Control01_Option02_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option02_Icon.Text = "";
			this.TabPageV_Control01_Option02_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option02_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option02_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option02_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option02_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option02
			// 
			this.TabPageV_Control01_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageV_Control01_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option02.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option02.Location = new System.Drawing.Point(0, 30);
			this.TabPageV_Control01_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option02.Name = "TabPageV_Control01_Option02";
			this.TabPageV_Control01_Option02.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option02.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option02.TabIndex = 2;
			this.TabPageV_Control01_Option02.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option02.Text = "Character";
			this.TabPageV_Control01_Option02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option02.UseVisualStyleBackColor = false;
			this.TabPageV_Control01_Option02.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option02.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option02.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option05_Icon
			// 
			this.TabPageV_Control01_Option05_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option05_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option05_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option05_Icon.Location = new System.Drawing.Point(5, 126);
			this.TabPageV_Control01_Option05_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option05_Icon.Name = "TabPageV_Control01_Option05_Icon";
			this.TabPageV_Control01_Option05_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option05_Icon.TabIndex = 8;
			this.TabPageV_Control01_Option05_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option05_Icon.Text = "";
			this.TabPageV_Control01_Option05_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option05_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option05_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option05_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option05_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option05
			// 
			this.TabPageV_Control01_Option05.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option05.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option05.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option05.Location = new System.Drawing.Point(0, 120);
			this.TabPageV_Control01_Option05.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option05.Name = "TabPageV_Control01_Option05";
			this.TabPageV_Control01_Option05.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option05.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option05.TabIndex = 5;
			this.TabPageV_Control01_Option05.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option05.Text = "Guild";
			this.TabPageV_Control01_Option05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option05.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option05.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option05.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option05.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option04_Icon
			// 
			this.TabPageV_Control01_Option04_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option04_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option04_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option04_Icon.Location = new System.Drawing.Point(5, 96);
			this.TabPageV_Control01_Option04_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option04_Icon.Name = "TabPageV_Control01_Option04_Icon";
			this.TabPageV_Control01_Option04_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option04_Icon.TabIndex = 6;
			this.TabPageV_Control01_Option04_Icon.Tag = "Font Awesome 5 Pro Solid";
			this.TabPageV_Control01_Option04_Icon.Text = "";
			this.TabPageV_Control01_Option04_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option04_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option04_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option04_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option04_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option04
			// 
			this.TabPageV_Control01_Option04.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option04.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option04.Location = new System.Drawing.Point(0, 90);
			this.TabPageV_Control01_Option04.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option04.Name = "TabPageV_Control01_Option04";
			this.TabPageV_Control01_Option04.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option04.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option04.TabIndex = 4;
			this.TabPageV_Control01_Option04.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option04.Text = "Party";
			this.TabPageV_Control01_Option04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option04.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option04.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option04.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option04.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option03_Icon
			// 
			this.TabPageV_Control01_Option03_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option03_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option03_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option03_Icon.Location = new System.Drawing.Point(5, 66);
			this.TabPageV_Control01_Option03_Icon.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
			this.TabPageV_Control01_Option03_Icon.Name = "TabPageV_Control01_Option03_Icon";
			this.TabPageV_Control01_Option03_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option03_Icon.TabIndex = 2;
			this.TabPageV_Control01_Option03_Icon.Tag = "Font Awesome 5 Pro Regular";
			this.TabPageV_Control01_Option03_Icon.Text = "";
			this.TabPageV_Control01_Option03_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option03_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option03_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option03_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option03_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option11_Icon
			// 
			this.TabPageV_Control01_Option11_Icon.BackColor = System.Drawing.Color.Transparent;
			this.TabPageV_Control01_Option11_Icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option11_Icon.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option11_Icon.Location = new System.Drawing.Point(5, 306);
			this.TabPageV_Control01_Option11_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option11_Icon.Name = "TabPageV_Control01_Option11_Icon";
			this.TabPageV_Control01_Option11_Icon.Size = new System.Drawing.Size(24, 21);
			this.TabPageV_Control01_Option11_Icon.TabIndex = 0;
			this.TabPageV_Control01_Option11_Icon.Text = "";
			this.TabPageV_Control01_Option11_Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TabPageV_Control01_Option11_Icon.UseCompatibleTextRendering = true;
			this.TabPageV_Control01_Option11_Icon.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option11_Icon.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option11_Icon.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option03
			// 
			this.TabPageV_Control01_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option03.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option03.Location = new System.Drawing.Point(0, 60);
			this.TabPageV_Control01_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option03.Name = "TabPageV_Control01_Option03";
			this.TabPageV_Control01_Option03.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option03.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option03.TabIndex = 3;
			this.TabPageV_Control01_Option03.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option03.Text = "Inventory";
			this.TabPageV_Control01_Option03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option03.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option03.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option03.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option03.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// TabPageV_Control01_Option11
			// 
			this.TabPageV_Control01_Option11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageV_Control01_Option11.FlatAppearance.BorderSize = 0;
			this.TabPageV_Control01_Option11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageV_Control01_Option11.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageV_Control01_Option11.Location = new System.Drawing.Point(0, 300);
			this.TabPageV_Control01_Option11.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageV_Control01_Option11.Name = "TabPageV_Control01_Option11";
			this.TabPageV_Control01_Option11.Padding = new System.Windows.Forms.Padding(29, 0, 0, 0);
			this.TabPageV_Control01_Option11.Size = new System.Drawing.Size(123, 30);
			this.TabPageV_Control01_Option11.TabIndex = 11;
			this.TabPageV_Control01_Option11.Tag = "Source Sans Pro";
			this.TabPageV_Control01_Option11.Text = "Chat";
			this.TabPageV_Control01_Option11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TabPageV_Control01_Option11.UseVisualStyleBackColor = true;
			this.TabPageV_Control01_Option11.Click += new System.EventHandler(this.TabPageV_Option_Click);
			this.TabPageV_Control01_Option11.MouseEnter += new System.EventHandler(this.TabPageV_Option_MouseEnter);
			this.TabPageV_Control01_Option11.MouseLeave += new System.EventHandler(this.TabPageV_Option_MouseLeave);
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.pnlHeader.Controls.Add(this.lblHeaderText01);
			this.pnlHeader.Controls.Add(this.lblHeaderIcon);
			this.pnlHeader.Controls.Add(this.btnWinMinimize);
			this.pnlHeader.Controls.Add(this.btnWinRestore);
			this.pnlHeader.Controls.Add(this.btnWinExit);
			this.pnlHeader.Controls.Add(this.lblHeaderText02);
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(800, 42);
			this.pnlHeader.TabIndex = 0;
			this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_Drag_MouseDown);
			// 
			// lblHeaderText01
			// 
			this.lblHeaderText01.AutoSize = true;
			this.lblHeaderText01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblHeaderText01.Font = new System.Drawing.Font("Source Sans Pro", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblHeaderText01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblHeaderText01.Location = new System.Drawing.Point(48, 4);
			this.lblHeaderText01.Margin = new System.Windows.Forms.Padding(0);
			this.lblHeaderText01.Name = "lblHeaderText01";
			this.lblHeaderText01.Size = new System.Drawing.Size(69, 35);
			this.lblHeaderText01.TabIndex = 9;
			this.lblHeaderText01.Text = "xBot -";
			this.lblHeaderText01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblHeaderText01.UseCompatibleTextRendering = true;
			// 
			// lblHeaderIcon
			// 
			this.lblHeaderIcon.Image = global::xBot.Properties.Resources.ProjexNET_40x40;
			this.lblHeaderIcon.Location = new System.Drawing.Point(4, 1);
			this.lblHeaderIcon.Name = "lblHeaderIcon";
			this.lblHeaderIcon.Size = new System.Drawing.Size(40, 40);
			this.lblHeaderIcon.TabIndex = 0;
			this.lblHeaderIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_Drag_MouseDown);
			// 
			// btnWinMinimize
			// 
			this.btnWinMinimize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnWinMinimize.FlatAppearance.BorderSize = 0;
			this.btnWinMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnWinMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnWinMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWinMinimize.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnWinMinimize.Location = new System.Drawing.Point(728, 0);
			this.btnWinMinimize.Margin = new System.Windows.Forms.Padding(0);
			this.btnWinMinimize.Name = "btnWinMinimize";
			this.btnWinMinimize.Size = new System.Drawing.Size(24, 24);
			this.btnWinMinimize.TabIndex = 1;
			this.btnWinMinimize.TabStop = false;
			this.btnWinMinimize.Tag = "Font Awesome 5 Pro Regular";
			this.btnWinMinimize.Text = "";
			this.btnWinMinimize.UseCompatibleTextRendering = true;
			this.btnWinMinimize.UseVisualStyleBackColor = true;
			this.btnWinMinimize.Click += new System.EventHandler(this.Control_Click);
			// 
			// btnWinRestore
			// 
			this.btnWinRestore.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnWinRestore.Enabled = false;
			this.btnWinRestore.FlatAppearance.BorderSize = 0;
			this.btnWinRestore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnWinRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnWinRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWinRestore.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnWinRestore.Location = new System.Drawing.Point(752, 0);
			this.btnWinRestore.Margin = new System.Windows.Forms.Padding(0);
			this.btnWinRestore.Name = "btnWinRestore";
			this.btnWinRestore.Size = new System.Drawing.Size(24, 24);
			this.btnWinRestore.TabIndex = 2;
			this.btnWinRestore.TabStop = false;
			this.btnWinRestore.Tag = "Font Awesome 5 Pro Regular";
			this.btnWinRestore.Text = "";
			this.btnWinRestore.UseCompatibleTextRendering = true;
			this.btnWinRestore.UseVisualStyleBackColor = true;
			this.btnWinRestore.Click += new System.EventHandler(this.Control_Click);
			// 
			// btnWinExit
			// 
			this.btnWinExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnWinExit.FlatAppearance.BorderSize = 0;
			this.btnWinExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnWinExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnWinExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWinExit.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnWinExit.Location = new System.Drawing.Point(776, 0);
			this.btnWinExit.Margin = new System.Windows.Forms.Padding(0);
			this.btnWinExit.Name = "btnWinExit";
			this.btnWinExit.Size = new System.Drawing.Size(24, 24);
			this.btnWinExit.TabIndex = 3;
			this.btnWinExit.TabStop = false;
			this.btnWinExit.Tag = "Font Awesome 5 Pro Regular";
			this.btnWinExit.Text = "";
			this.btnWinExit.UseCompatibleTextRendering = true;
			this.btnWinExit.UseVisualStyleBackColor = true;
			this.btnWinExit.Click += new System.EventHandler(this.Control_Click);
			// 
			// lblHeaderText02
			// 
			this.lblHeaderText02.AutoSize = true;
			this.lblHeaderText02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblHeaderText02.Font = new System.Drawing.Font("Source Sans Pro", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblHeaderText02.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(255)))));
			this.lblHeaderText02.Location = new System.Drawing.Point(114, 4);
			this.lblHeaderText02.Margin = new System.Windows.Forms.Padding(0);
			this.lblHeaderText02.Name = "lblHeaderText02";
			this.lblHeaderText02.Size = new System.Drawing.Size(99, 35);
			this.lblHeaderText02.TabIndex = 6;
			this.lblHeaderText02.Text = "JellyBitz";
			this.lblHeaderText02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblHeaderText02.UseCompatibleTextRendering = true;
			this.lblHeaderText02.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_Drag_MouseDown);
			// 
			// TabPageV_Control01_Option14_Panel
			// 
			this.TabPageV_Control01_Option14_Panel.Controls.Add(this.TabPageH_Settings);
			this.TabPageV_Control01_Option14_Panel.Controls.Add(this.TabPageH_Settings_Option04_Panel);
			this.TabPageV_Control01_Option14_Panel.Controls.Add(this.TabPageH_Settings_Option01_Panel);
			this.TabPageV_Control01_Option14_Panel.Controls.Add(this.TabPageH_Settings_Option02_Panel);
			this.TabPageV_Control01_Option14_Panel.Controls.Add(this.TabPageH_Settings_Option03_Panel);
			this.TabPageV_Control01_Option14_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option14_Panel.Name = "TabPageV_Control01_Option14_Panel";
			this.TabPageV_Control01_Option14_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option14_Panel.TabIndex = 5;
			this.TabPageV_Control01_Option14_Panel.Visible = false;
			// 
			// TabPageH_Settings
			// 
			this.TabPageH_Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Settings.Controls.Add(this.TabPageH_Settings_Option04);
			this.TabPageH_Settings.Controls.Add(this.TabPageH_Settings_Option03);
			this.TabPageH_Settings.Controls.Add(this.TabPageH_Settings_Option02);
			this.TabPageH_Settings.Controls.Add(this.TabPageH_Settings_Option01);
			this.TabPageH_Settings.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Settings.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Settings.Name = "TabPageH_Settings";
			this.TabPageH_Settings.Size = new System.Drawing.Size(657, 28);
			this.TabPageH_Settings.TabIndex = 0;
			// 
			// TabPageH_Settings_Option04
			// 
			this.TabPageH_Settings_Option04.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Settings_Option04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Settings_Option04.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option04.FlatAppearance.BorderSize = 0;
			this.TabPageH_Settings_Option04.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Settings_Option04.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Settings_Option04.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Settings_Option04.Location = new System.Drawing.Point(493, 0);
			this.TabPageH_Settings_Option04.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Settings_Option04.Name = "TabPageH_Settings_Option04";
			this.TabPageH_Settings_Option04.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Settings_Option04.TabIndex = 4;
			this.TabPageH_Settings_Option04.Tag = "Source Sans Pro";
			this.TabPageH_Settings_Option04.Text = "Packet Analyzer";
			this.TabPageH_Settings_Option04.UseVisualStyleBackColor = false;
			this.TabPageH_Settings_Option04.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Settings_Option03
			// 
			this.TabPageH_Settings_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Settings_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Settings_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Settings_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Settings_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Settings_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Settings_Option03.Location = new System.Drawing.Point(329, 0);
			this.TabPageH_Settings_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Settings_Option03.Name = "TabPageH_Settings_Option03";
			this.TabPageH_Settings_Option03.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Settings_Option03.TabIndex = 3;
			this.TabPageH_Settings_Option03.Tag = "Source Sans Pro";
			this.TabPageH_Settings_Option03.Text = ". . .";
			this.TabPageH_Settings_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Settings_Option03.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Settings_Option02
			// 
			this.TabPageH_Settings_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Settings_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Settings_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Settings_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Settings_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Settings_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Settings_Option02.Location = new System.Drawing.Point(165, 0);
			this.TabPageH_Settings_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Settings_Option02.Name = "TabPageH_Settings_Option02";
			this.TabPageH_Settings_Option02.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Settings_Option02.TabIndex = 2;
			this.TabPageH_Settings_Option02.Tag = "Source Sans Pro";
			this.TabPageH_Settings_Option02.Text = "Auto Magically";
			this.TabPageH_Settings_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Settings_Option02.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Settings_Option01
			// 
			this.TabPageH_Settings_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Settings_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Settings_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Settings_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Settings_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Settings_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Settings_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Settings_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Settings_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Settings_Option01.Name = "TabPageH_Settings_Option01";
			this.TabPageH_Settings_Option01.Size = new System.Drawing.Size(165, 26);
			this.TabPageH_Settings_Option01.TabIndex = 1;
			this.TabPageH_Settings_Option01.Tag = "Source Sans Pro";
			this.TabPageH_Settings_Option01.Text = "Silkroad";
			this.TabPageH_Settings_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Settings_Option01.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Settings_Option04_Panel
			// 
			this.TabPageH_Settings_Option04_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Settings_Option04_Panel.Controls.Add(this.Settings_gbxPacketInject);
			this.TabPageH_Settings_Option04_Panel.Controls.Add(this.Settings_gbxPacketFilter);
			this.TabPageH_Settings_Option04_Panel.Controls.Add(this.Settings_rtbxPackets);
			this.TabPageH_Settings_Option04_Panel.Controls.Add(this.Settings_cbxShowPacketClient);
			this.TabPageH_Settings_Option04_Panel.Controls.Add(this.Settings_cbxShowPacketServer);
			this.TabPageH_Settings_Option04_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Settings_Option04_Panel.Name = "TabPageH_Settings_Option04_Panel";
			this.TabPageH_Settings_Option04_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Settings_Option04_Panel.TabIndex = 11;
			this.TabPageH_Settings_Option04_Panel.Visible = false;
			// 
			// Settings_gbxPacketInject
			// 
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_cbxInjectMassive);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_cbxInjectEncrypted);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_cmbxInjectTo);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_tbxInjectData);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_lblInjectData);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_btnInjectPacket);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_tbxInjectOpcode);
			this.Settings_gbxPacketInject.Controls.Add(this.Settings_lblInjectOpcode);
			this.Settings_gbxPacketInject.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_gbxPacketInject.ForeColor = System.Drawing.Color.LightGray;
			this.Settings_gbxPacketInject.Location = new System.Drawing.Point(5, 253);
			this.Settings_gbxPacketInject.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Settings_gbxPacketInject.Name = "Settings_gbxPacketInject";
			this.Settings_gbxPacketInject.Size = new System.Drawing.Size(645, 54);
			this.Settings_gbxPacketInject.TabIndex = 17;
			this.Settings_gbxPacketInject.TabStop = false;
			this.Settings_gbxPacketInject.Tag = "Source Sans Pro";
			this.Settings_gbxPacketInject.Text = "Packet Injection";
			// 
			// Settings_cbxInjectMassive
			// 
			this.Settings_cbxInjectMassive.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxInjectMassive.FlatAppearance.BorderSize = 0;
			this.Settings_cbxInjectMassive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxInjectMassive.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxInjectMassive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxInjectMassive.Location = new System.Drawing.Point(507, 19);
			this.Settings_cbxInjectMassive.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxInjectMassive.Name = "Settings_cbxInjectMassive";
			this.Settings_cbxInjectMassive.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Settings_cbxInjectMassive.Size = new System.Drawing.Size(80, 24);
			this.Settings_cbxInjectMassive.TabIndex = 18;
			this.Settings_cbxInjectMassive.Tag = "Source Sans Pro";
			this.Settings_cbxInjectMassive.Text = "Massive";
			this.Settings_cbxInjectMassive.UseVisualStyleBackColor = false;
			this.Settings_cbxInjectMassive.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_cbxInjectEncrypted
			// 
			this.Settings_cbxInjectEncrypted.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxInjectEncrypted.FlatAppearance.BorderSize = 0;
			this.Settings_cbxInjectEncrypted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxInjectEncrypted.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxInjectEncrypted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxInjectEncrypted.Location = new System.Drawing.Point(425, 19);
			this.Settings_cbxInjectEncrypted.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxInjectEncrypted.Name = "Settings_cbxInjectEncrypted";
			this.Settings_cbxInjectEncrypted.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Settings_cbxInjectEncrypted.Size = new System.Drawing.Size(90, 24);
			this.Settings_cbxInjectEncrypted.TabIndex = 17;
			this.Settings_cbxInjectEncrypted.Tag = "Source Sans Pro";
			this.Settings_cbxInjectEncrypted.Text = "Encryped";
			this.Settings_cbxInjectEncrypted.UseVisualStyleBackColor = false;
			this.Settings_cbxInjectEncrypted.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_cmbxInjectTo
			// 
			this.Settings_cmbxInjectTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_cmbxInjectTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Settings_cmbxInjectTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cmbxInjectTo.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cmbxInjectTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cmbxInjectTo.FormattingEnabled = true;
			this.Settings_cmbxInjectTo.Items.AddRange(new object[] {
            "Server",
            "Client"});
			this.Settings_cmbxInjectTo.Location = new System.Drawing.Point(357, 18);
			this.Settings_cmbxInjectTo.MaxDropDownItems = 5;
			this.Settings_cmbxInjectTo.Name = "Settings_cmbxInjectTo";
			this.Settings_cmbxInjectTo.Size = new System.Drawing.Size(67, 27);
			this.Settings_cmbxInjectTo.TabIndex = 16;
			this.Settings_cmbxInjectTo.Tag = "Source Sans Pro";
			// 
			// Settings_tbxInjectData
			// 
			this.Settings_tbxInjectData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxInjectData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxInjectData.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxInjectData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxInjectData.Location = new System.Drawing.Point(154, 18);
			this.Settings_tbxInjectData.Name = "Settings_tbxInjectData";
			this.Settings_tbxInjectData.Size = new System.Drawing.Size(198, 26);
			this.Settings_tbxInjectData.TabIndex = 15;
			this.Settings_tbxInjectData.Tag = "Source Sans Pro";
			this.Settings_tbxInjectData.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxInjectData.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblInjectData
			// 
			this.Settings_lblInjectData.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblInjectData.Location = new System.Drawing.Point(118, 18);
			this.Settings_lblInjectData.Name = "Settings_lblInjectData";
			this.Settings_lblInjectData.Size = new System.Drawing.Size(36, 26);
			this.Settings_lblInjectData.TabIndex = 14;
			this.Settings_lblInjectData.Tag = "Source Sans Pro";
			this.Settings_lblInjectData.Text = "Data";
			this.Settings_lblInjectData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_btnInjectPacket
			// 
			this.Settings_btnInjectPacket.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnInjectPacket.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_btnInjectPacket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnInjectPacket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_btnInjectPacket.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_btnInjectPacket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_btnInjectPacket.Location = new System.Drawing.Point(589, 18);
			this.Settings_btnInjectPacket.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_btnInjectPacket.Name = "Settings_btnInjectPacket";
			this.Settings_btnInjectPacket.Size = new System.Drawing.Size(49, 26);
			this.Settings_btnInjectPacket.TabIndex = 6;
			this.Settings_btnInjectPacket.Tag = "Source Sans Pro";
			this.Settings_btnInjectPacket.Text = "Inject";
			this.Settings_btnInjectPacket.UseCompatibleTextRendering = true;
			this.Settings_btnInjectPacket.UseVisualStyleBackColor = false;
			this.Settings_btnInjectPacket.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_tbxInjectOpcode
			// 
			this.Settings_tbxInjectOpcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxInjectOpcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxInjectOpcode.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxInjectOpcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxInjectOpcode.Location = new System.Drawing.Point(60, 18);
			this.Settings_tbxInjectOpcode.Name = "Settings_tbxInjectOpcode";
			this.Settings_tbxInjectOpcode.Size = new System.Drawing.Size(54, 26);
			this.Settings_tbxInjectOpcode.TabIndex = 5;
			this.Settings_tbxInjectOpcode.Tag = "Source Sans Pro";
			this.Settings_tbxInjectOpcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxInjectOpcode.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxInjectOpcode.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblInjectOpcode
			// 
			this.Settings_lblInjectOpcode.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblInjectOpcode.Location = new System.Drawing.Point(6, 18);
			this.Settings_lblInjectOpcode.Name = "Settings_lblInjectOpcode";
			this.Settings_lblInjectOpcode.Size = new System.Drawing.Size(54, 26);
			this.Settings_lblInjectOpcode.TabIndex = 13;
			this.Settings_lblInjectOpcode.Tag = "Source Sans Pro";
			this.Settings_lblInjectOpcode.Text = "Opcode";
			this.Settings_lblInjectOpcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_gbxPacketFilter
			// 
			this.Settings_gbxPacketFilter.Controls.Add(this.Settings_rbnPacketNotShow);
			this.Settings_gbxPacketFilter.Controls.Add(this.Settings_rbnPacketOnlyShow);
			this.Settings_gbxPacketFilter.Controls.Add(this.Settings_lstvOpcodes);
			this.Settings_gbxPacketFilter.Controls.Add(this.Settings_btnAddOpcode);
			this.Settings_gbxPacketFilter.Controls.Add(this.Settings_tbxFilterOpcode);
			this.Settings_gbxPacketFilter.Controls.Add(this.Settings_lblFilterOpcode);
			this.Settings_gbxPacketFilter.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_gbxPacketFilter.ForeColor = System.Drawing.Color.LightGray;
			this.Settings_gbxPacketFilter.Location = new System.Drawing.Point(5, 24);
			this.Settings_gbxPacketFilter.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Settings_gbxPacketFilter.Name = "Settings_gbxPacketFilter";
			this.Settings_gbxPacketFilter.Size = new System.Drawing.Size(168, 228);
			this.Settings_gbxPacketFilter.TabIndex = 16;
			this.Settings_gbxPacketFilter.TabStop = false;
			this.Settings_gbxPacketFilter.Tag = "Source Sans Pro";
			this.Settings_gbxPacketFilter.Text = "Filter";
			// 
			// Settings_rbnPacketNotShow
			// 
			this.Settings_rbnPacketNotShow.Checked = true;
			this.Settings_rbnPacketNotShow.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_rbnPacketNotShow.Location = new System.Drawing.Point(5, 15);
			this.Settings_rbnPacketNotShow.Name = "Settings_rbnPacketNotShow";
			this.Settings_rbnPacketNotShow.Size = new System.Drawing.Size(79, 19);
			this.Settings_rbnPacketNotShow.TabIndex = 4;
			this.Settings_rbnPacketNotShow.TabStop = true;
			this.Settings_rbnPacketNotShow.Text = "Not show";
			this.Settings_rbnPacketNotShow.UseVisualStyleBackColor = false;
			this.Settings_rbnPacketNotShow.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_rbnPacketOnlyShow
			// 
			this.Settings_rbnPacketOnlyShow.Location = new System.Drawing.Point(84, 15);
			this.Settings_rbnPacketOnlyShow.Name = "Settings_rbnPacketOnlyShow";
			this.Settings_rbnPacketOnlyShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Settings_rbnPacketOnlyShow.Size = new System.Drawing.Size(79, 19);
			this.Settings_rbnPacketOnlyShow.TabIndex = 3;
			this.Settings_rbnPacketOnlyShow.Text = "Only show";
			this.Settings_rbnPacketOnlyShow.UseVisualStyleBackColor = false;
			this.Settings_rbnPacketOnlyShow.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_lstvOpcodes
			// 
			this.Settings_lstvOpcodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Settings_lstvOpcodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Settings_lstvOpcodes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9});
			this.Settings_lstvOpcodes.ContextMenuStrip = this.Menu_lstvOpcodes;
			this.Settings_lstvOpcodes.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lstvOpcodes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lstvOpcodes.FullRowSelect = true;
			this.Settings_lstvOpcodes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Settings_lstvOpcodes.Location = new System.Drawing.Point(2, 70);
			this.Settings_lstvOpcodes.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_lstvOpcodes.MultiSelect = false;
			this.Settings_lstvOpcodes.Name = "Settings_lstvOpcodes";
			this.Settings_lstvOpcodes.Size = new System.Drawing.Size(164, 156);
			this.Settings_lstvOpcodes.TabIndex = 10;
			this.Settings_lstvOpcodes.Tag = "Source Sans Pro";
			this.Settings_lstvOpcodes.TileSize = new System.Drawing.Size(201, 30);
			this.Settings_lstvOpcodes.UseCompatibleStateImageBehavior = false;
			this.Settings_lstvOpcodes.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Opcodes";
			this.columnHeader9.Width = 147;
			// 
			// Menu_lstvOpcodes
			// 
			this.Menu_lstvOpcodes.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Menu_lstvOpcodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvOpcodes_Remove,
            this.Menu_lstvOpcodes_RemoveAll});
			this.Menu_lstvOpcodes.Name = "Menu_lstvOpcodes";
			this.Menu_lstvOpcodes.Size = new System.Drawing.Size(165, 48);
			// 
			// Menu_lstvOpcodes_Remove
			// 
			this.Menu_lstvOpcodes_Remove.Name = "Menu_lstvOpcodes_Remove";
			this.Menu_lstvOpcodes_Remove.Size = new System.Drawing.Size(164, 22);
			this.Menu_lstvOpcodes_Remove.Text = "Remove Selected";
			this.Menu_lstvOpcodes_Remove.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvOpcodes_RemoveAll
			// 
			this.Menu_lstvOpcodes_RemoveAll.Name = "Menu_lstvOpcodes_RemoveAll";
			this.Menu_lstvOpcodes_RemoveAll.Size = new System.Drawing.Size(164, 22);
			this.Menu_lstvOpcodes_RemoveAll.Text = "Remove All";
			this.Menu_lstvOpcodes_RemoveAll.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Settings_btnAddOpcode
			// 
			this.Settings_btnAddOpcode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Settings_btnAddOpcode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnAddOpcode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_btnAddOpcode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnAddOpcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_btnAddOpcode.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_btnAddOpcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_btnAddOpcode.Location = new System.Drawing.Point(123, 37);
			this.Settings_btnAddOpcode.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_btnAddOpcode.Name = "Settings_btnAddOpcode";
			this.Settings_btnAddOpcode.Size = new System.Drawing.Size(39, 26);
			this.Settings_btnAddOpcode.TabIndex = 6;
			this.Settings_btnAddOpcode.Tag = "Source Sans Pro";
			this.Settings_btnAddOpcode.Text = "Add";
			this.Settings_btnAddOpcode.UseCompatibleTextRendering = true;
			this.Settings_btnAddOpcode.UseVisualStyleBackColor = false;
			this.Settings_btnAddOpcode.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_tbxFilterOpcode
			// 
			this.Settings_tbxFilterOpcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxFilterOpcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxFilterOpcode.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxFilterOpcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxFilterOpcode.Location = new System.Drawing.Point(59, 37);
			this.Settings_tbxFilterOpcode.Name = "Settings_tbxFilterOpcode";
			this.Settings_tbxFilterOpcode.Size = new System.Drawing.Size(60, 26);
			this.Settings_tbxFilterOpcode.TabIndex = 5;
			this.Settings_tbxFilterOpcode.Tag = "Source Sans Pro";
			this.Settings_tbxFilterOpcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxFilterOpcode.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxFilterOpcode.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblFilterOpcode
			// 
			this.Settings_lblFilterOpcode.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblFilterOpcode.Location = new System.Drawing.Point(6, 37);
			this.Settings_lblFilterOpcode.Name = "Settings_lblFilterOpcode";
			this.Settings_lblFilterOpcode.Size = new System.Drawing.Size(54, 26);
			this.Settings_lblFilterOpcode.TabIndex = 13;
			this.Settings_lblFilterOpcode.Tag = "Source Sans Pro";
			this.Settings_lblFilterOpcode.Text = "Opcode";
			this.Settings_lblFilterOpcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Menu_rtbxPackets
			// 
			this.Menu_rtbxPackets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_rtbxPackets_AutoScroll,
            this.Menu_rtbxPackets_AddTimestamp,
            this.Menu_rtbxPackets_Clear});
			this.Menu_rtbxPackets.Name = "Menu_rtbxPackets";
			this.Menu_rtbxPackets.Size = new System.Drawing.Size(174, 70);
			// 
			// Menu_rtbxPackets_AutoScroll
			// 
			this.Menu_rtbxPackets_AutoScroll.CheckOnClick = true;
			this.Menu_rtbxPackets_AutoScroll.Name = "Menu_rtbxPackets_AutoScroll";
			this.Menu_rtbxPackets_AutoScroll.Size = new System.Drawing.Size(173, 22);
			this.Menu_rtbxPackets_AutoScroll.Text = "Auto Scroll";
			this.Menu_rtbxPackets_AutoScroll.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_rtbxPackets_AddTimestamp
			// 
			this.Menu_rtbxPackets_AddTimestamp.CheckOnClick = true;
			this.Menu_rtbxPackets_AddTimestamp.Name = "Menu_rtbxPackets_AddTimestamp";
			this.Menu_rtbxPackets_AddTimestamp.Size = new System.Drawing.Size(173, 22);
			this.Menu_rtbxPackets_AddTimestamp.Text = "Add Timestamp (!)";
			this.Menu_rtbxPackets_AddTimestamp.ToolTipText = "Can slow down the performance";
			// 
			// Menu_rtbxPackets_Clear
			// 
			this.Menu_rtbxPackets_Clear.Name = "Menu_rtbxPackets_Clear";
			this.Menu_rtbxPackets_Clear.Size = new System.Drawing.Size(173, 22);
			this.Menu_rtbxPackets_Clear.Text = "Clear";
			this.Menu_rtbxPackets_Clear.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Settings_cbxShowPacketClient
			// 
			this.Settings_cbxShowPacketClient.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxShowPacketClient.FlatAppearance.BorderSize = 0;
			this.Settings_cbxShowPacketClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxShowPacketClient.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxShowPacketClient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxShowPacketClient.Location = new System.Drawing.Point(86, 2);
			this.Settings_cbxShowPacketClient.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxShowPacketClient.Name = "Settings_cbxShowPacketClient";
			this.Settings_cbxShowPacketClient.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Settings_cbxShowPacketClient.Size = new System.Drawing.Size(87, 24);
			this.Settings_cbxShowPacketClient.TabIndex = 2;
			this.Settings_cbxShowPacketClient.Tag = "Source Sans Pro";
			this.Settings_cbxShowPacketClient.Text = "Client";
			this.ToolTips.SetToolTip(this.Settings_cbxShowPacketClient, "Show Client Packets");
			this.Settings_cbxShowPacketClient.UseVisualStyleBackColor = false;
			// 
			// Settings_cbxShowPacketServer
			// 
			this.Settings_cbxShowPacketServer.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxShowPacketServer.FlatAppearance.BorderSize = 0;
			this.Settings_cbxShowPacketServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxShowPacketServer.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxShowPacketServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxShowPacketServer.Location = new System.Drawing.Point(5, 2);
			this.Settings_cbxShowPacketServer.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxShowPacketServer.Name = "Settings_cbxShowPacketServer";
			this.Settings_cbxShowPacketServer.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Settings_cbxShowPacketServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Settings_cbxShowPacketServer.Size = new System.Drawing.Size(81, 24);
			this.Settings_cbxShowPacketServer.TabIndex = 1;
			this.Settings_cbxShowPacketServer.Tag = "Source Sans Pro";
			this.Settings_cbxShowPacketServer.Text = "Server";
			this.ToolTips.SetToolTip(this.Settings_cbxShowPacketServer, "Show Server Packets");
			this.Settings_cbxShowPacketServer.UseVisualStyleBackColor = false;
			// 
			// TabPageH_Settings_Option01_Panel
			// 
			this.TabPageH_Settings_Option01_Panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Settings_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_btnLauncherPath);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_btnClientPath);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_cbxRandomHost);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_btnPK2Path);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lstvHost);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lblSilkroadName);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_tbxSilkroadName);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lstrSilkroads);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_gbxHWID);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_btnAddSilkroad);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_tbxLocale);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lblLocale);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lblHost);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_tbxVersion);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lblVersion);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_lblPort);
			this.TabPageH_Settings_Option01_Panel.Controls.Add(this.Settings_tbxPort);
			this.TabPageH_Settings_Option01_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Settings_Option01_Panel.Name = "TabPageH_Settings_Option01_Panel";
			this.TabPageH_Settings_Option01_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Settings_Option01_Panel.TabIndex = 0;
			this.TabPageH_Settings_Option01_Panel.Visible = false;
			// 
			// Settings_btnLauncherPath
			// 
			this.Settings_btnLauncherPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Settings_btnLauncherPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnLauncherPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_btnLauncherPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnLauncherPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_btnLauncherPath.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_btnLauncherPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_btnLauncherPath.Location = new System.Drawing.Point(6, 71);
			this.Settings_btnLauncherPath.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_btnLauncherPath.Name = "Settings_btnLauncherPath";
			this.Settings_btnLauncherPath.Size = new System.Drawing.Size(76, 28);
			this.Settings_btnLauncherPath.TabIndex = 19;
			this.Settings_btnLauncherPath.Tag = "Source Sans Pro";
			this.Settings_btnLauncherPath.Text = "Launcher";
			this.ToolTips.SetToolTip(this.Settings_btnLauncherPath, "Select Launcher Path");
			this.Settings_btnLauncherPath.UseCompatibleTextRendering = true;
			this.Settings_btnLauncherPath.UseVisualStyleBackColor = false;
			this.Settings_btnLauncherPath.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_btnClientPath
			// 
			this.Settings_btnClientPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Settings_btnClientPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnClientPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_btnClientPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnClientPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_btnClientPath.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_btnClientPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_btnClientPath.Location = new System.Drawing.Point(86, 71);
			this.Settings_btnClientPath.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_btnClientPath.Name = "Settings_btnClientPath";
			this.Settings_btnClientPath.Size = new System.Drawing.Size(55, 28);
			this.Settings_btnClientPath.TabIndex = 18;
			this.Settings_btnClientPath.Tag = "Source Sans Pro";
			this.Settings_btnClientPath.Text = "Client";
			this.ToolTips.SetToolTip(this.Settings_btnClientPath, "Select Client Path");
			this.Settings_btnClientPath.UseCompatibleTextRendering = true;
			this.Settings_btnClientPath.UseVisualStyleBackColor = false;
			this.Settings_btnClientPath.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_cbxRandomHost
			// 
			this.Settings_cbxRandomHost.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxRandomHost.FlatAppearance.BorderSize = 0;
			this.Settings_cbxRandomHost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxRandomHost.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxRandomHost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxRandomHost.Location = new System.Drawing.Point(6, 264);
			this.Settings_cbxRandomHost.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxRandomHost.Name = "Settings_cbxRandomHost";
			this.Settings_cbxRandomHost.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Settings_cbxRandomHost.Size = new System.Drawing.Size(135, 45);
			this.Settings_cbxRandomHost.TabIndex = 17;
			this.Settings_cbxRandomHost.Tag = "Source Sans Pro";
			this.Settings_cbxRandomHost.Text = "Use random host to connect";
			this.Settings_cbxRandomHost.UseVisualStyleBackColor = false;
			// 
			// Settings_btnPK2Path
			// 
			this.Settings_btnPK2Path.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Settings_btnPK2Path.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnPK2Path.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_btnPK2Path.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnPK2Path.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_btnPK2Path.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_btnPK2Path.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_btnPK2Path.Location = new System.Drawing.Point(6, 39);
			this.Settings_btnPK2Path.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_btnPK2Path.Name = "Settings_btnPK2Path";
			this.Settings_btnPK2Path.Size = new System.Drawing.Size(135, 28);
			this.Settings_btnPK2Path.TabIndex = 16;
			this.Settings_btnPK2Path.Tag = "Source Sans Pro";
			this.Settings_btnPK2Path.Text = "Load Media.pk2";
			this.ToolTips.SetToolTip(this.Settings_btnPK2Path, "( Load / Update ) Bot database");
			this.Settings_btnPK2Path.UseCompatibleTextRendering = true;
			this.Settings_btnPK2Path.UseVisualStyleBackColor = false;
			this.Settings_btnPK2Path.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_lstvHost
			// 
			this.Settings_lstvHost.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.Settings_lstvHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_lstvHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_lstvHost.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10});
			this.Settings_lstvHost.ContextMenuStrip = this.Menu_lstvHost;
			this.Settings_lstvHost.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lstvHost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lstvHost.FullRowSelect = true;
			this.Settings_lstvHost.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Settings_lstvHost.HotTracking = true;
			this.Settings_lstvHost.HoverSelection = true;
			this.Settings_lstvHost.Location = new System.Drawing.Point(6, 195);
			this.Settings_lstvHost.MultiSelect = false;
			this.Settings_lstvHost.Name = "Settings_lstvHost";
			this.Settings_lstvHost.ShowGroups = false;
			this.Settings_lstvHost.Size = new System.Drawing.Size(135, 28);
			this.Settings_lstvHost.TabIndex = 0;
			this.Settings_lstvHost.TabStop = false;
			this.Settings_lstvHost.Tag = "Source Sans Pro";
			this.Settings_lstvHost.UseCompatibleStateImageBehavior = false;
			this.Settings_lstvHost.View = System.Windows.Forms.View.Details;
			this.Settings_lstvHost.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_lstvHost.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Hosts";
			this.columnHeader10.Width = 115;
			// 
			// Menu_lstvHost
			// 
			this.Menu_lstvHost.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvHost_remove});
			this.Menu_lstvHost.Name = "Menu_lstvHost";
			this.Menu_lstvHost.Size = new System.Drawing.Size(146, 26);
			// 
			// Menu_lstvHost_remove
			// 
			this.Menu_lstvHost_remove.Name = "Menu_lstvHost_remove";
			this.Menu_lstvHost_remove.Size = new System.Drawing.Size(145, 22);
			this.Menu_lstvHost_remove.Text = "Remove Host";
			this.Menu_lstvHost_remove.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Settings_lblSilkroadName
			// 
			this.Settings_lblSilkroadName.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblSilkroadName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblSilkroadName.Location = new System.Drawing.Point(6, 6);
			this.Settings_lblSilkroadName.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblSilkroadName.Name = "Settings_lblSilkroadName";
			this.Settings_lblSilkroadName.Size = new System.Drawing.Size(48, 28);
			this.Settings_lblSilkroadName.TabIndex = 1;
			this.Settings_lblSilkroadName.Tag = "Source Sans Pro";
			this.Settings_lblSilkroadName.Text = "Name";
			this.Settings_lblSilkroadName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Settings_lblSilkroadName, "Unique name");
			// 
			// Settings_tbxSilkroadName
			// 
			this.Settings_tbxSilkroadName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxSilkroadName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxSilkroadName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Settings_tbxSilkroadName.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxSilkroadName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxSilkroadName.Location = new System.Drawing.Point(54, 6);
			this.Settings_tbxSilkroadName.Name = "Settings_tbxSilkroadName";
			this.Settings_tbxSilkroadName.Size = new System.Drawing.Size(87, 28);
			this.Settings_tbxSilkroadName.TabIndex = 2;
			this.Settings_tbxSilkroadName.Tag = "Source Sans Pro";
			this.Settings_tbxSilkroadName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ToolTips.SetToolTip(this.Settings_tbxSilkroadName, "Silkroad unique name");
			this.Settings_tbxSilkroadName.TextChanged += new System.EventHandler(this.Control_TextChanged);
			this.Settings_tbxSilkroadName.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxSilkroadName.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lstrSilkroads
			// 
			this.Settings_lstrSilkroads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Settings_lstrSilkroads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_lstrSilkroads.ContextMenuStrip = this.Menu_lstrSilkroads;
			this.Settings_lstrSilkroads.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lstrSilkroads.FullRowSelect = true;
			this.Settings_lstrSilkroads.HideSelection = false;
			this.Settings_lstrSilkroads.Indent = 20;
			this.Settings_lstrSilkroads.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_lstrSilkroads.Location = new System.Drawing.Point(286, 6);
			this.Settings_lstrSilkroads.Name = "Settings_lstrSilkroads";
			this.Settings_lstrSilkroads.Size = new System.Drawing.Size(365, 303);
			this.Settings_lstrSilkroads.TabIndex = 15;
			this.Settings_lstrSilkroads.Tag = "Source Sans Pro";
			this.Settings_lstrSilkroads.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
			// 
			// Menu_lstrSilkroads
			// 
			this.Menu_lstrSilkroads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstrSilkroads_Remove});
			this.Menu_lstrSilkroads.Name = "Menu_lstrServers";
			this.Menu_lstrSilkroads.Size = new System.Drawing.Size(163, 26);
			// 
			// Menu_lstrSilkroads_Remove
			// 
			this.Menu_lstrSilkroads_Remove.Name = "Menu_lstrSilkroads_Remove";
			this.Menu_lstrSilkroads_Remove.Size = new System.Drawing.Size(162, 22);
			this.Menu_lstrSilkroads_Remove.Text = "Remove Silkroad";
			this.Menu_lstrSilkroads_Remove.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Settings_gbxHWID
			// 
			this.Settings_gbxHWID.Controls.Add(this.Settings_panelHWIDdata);
			this.Settings_gbxHWID.Controls.Add(this.Settings_cbxHWIDSendOnlyOnce);
			this.Settings_gbxHWID.Controls.Add(this.Settings_cmbxHWIDServerSendTo);
			this.Settings_gbxHWID.Controls.Add(this.Settings_cmbxHWIDClientSaveFrom);
			this.Settings_gbxHWID.Controls.Add(this.Settings_tbxHWIDClientOp);
			this.Settings_gbxHWID.Controls.Add(this.Settings_lblHWIDClientOp);
			this.Settings_gbxHWID.Controls.Add(this.Settings_tbxHWIDServerOp);
			this.Settings_gbxHWID.Controls.Add(this.Settings_lblHWIDServerOp);
			this.Settings_gbxHWID.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_gbxHWID.ForeColor = System.Drawing.Color.LightGray;
			this.Settings_gbxHWID.Location = new System.Drawing.Point(147, 1);
			this.Settings_gbxHWID.Name = "Settings_gbxHWID";
			this.Settings_gbxHWID.Size = new System.Drawing.Size(134, 277);
			this.Settings_gbxHWID.TabIndex = 13;
			this.Settings_gbxHWID.TabStop = false;
			this.Settings_gbxHWID.Tag = "Source Sans Pro";
			this.Settings_gbxHWID.Text = "HWID setup (opcodes)";
			this.ToolTips.SetToolTip(this.Settings_gbxHWID, "If the server uses an static HWID, then will be possible use it on clientless");
			// 
			// Settings_panelHWIDdata
			// 
			this.Settings_panelHWIDdata.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_panelHWIDdata.Controls.Add(this.Settings_rtbxHWIDdata);
			this.Settings_panelHWIDdata.Location = new System.Drawing.Point(6, 183);
			this.Settings_panelHWIDdata.Name = "Settings_panelHWIDdata";
			this.Settings_panelHWIDdata.Size = new System.Drawing.Size(121, 88);
			this.Settings_panelHWIDdata.TabIndex = 8;
			// 
			// Settings_cbxHWIDSendOnlyOnce
			// 
			this.Settings_cbxHWIDSendOnlyOnce.Checked = true;
			this.Settings_cbxHWIDSendOnlyOnce.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Settings_cbxHWIDSendOnlyOnce.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxHWIDSendOnlyOnce.FlatAppearance.BorderSize = 0;
			this.Settings_cbxHWIDSendOnlyOnce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxHWIDSendOnlyOnce.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxHWIDSendOnlyOnce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxHWIDSendOnlyOnce.Location = new System.Drawing.Point(6, 138);
			this.Settings_cbxHWIDSendOnlyOnce.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxHWIDSendOnlyOnce.Name = "Settings_cbxHWIDSendOnlyOnce";
			this.Settings_cbxHWIDSendOnlyOnce.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Settings_cbxHWIDSendOnlyOnce.Size = new System.Drawing.Size(121, 45);
			this.Settings_cbxHWIDSendOnlyOnce.TabIndex = 7;
			this.Settings_cbxHWIDSendOnlyOnce.Tag = "Source Sans Pro";
			this.Settings_cbxHWIDSendOnlyOnce.Text = "Send HWID only once";
			this.Settings_cbxHWIDSendOnlyOnce.UseVisualStyleBackColor = false;
			// 
			// Settings_cmbxHWIDServerSendTo
			// 
			this.Settings_cmbxHWIDServerSendTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_cmbxHWIDServerSendTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Settings_cmbxHWIDServerSendTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cmbxHWIDServerSendTo.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cmbxHWIDServerSendTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cmbxHWIDServerSendTo.FormattingEnabled = true;
			this.Settings_cmbxHWIDServerSendTo.Items.AddRange(new object[] {
            "Agent",
            "Gateway",
            "Both"});
			this.Settings_cmbxHWIDServerSendTo.Location = new System.Drawing.Point(6, 110);
			this.Settings_cmbxHWIDServerSendTo.MaxDropDownItems = 5;
			this.Settings_cmbxHWIDServerSendTo.Name = "Settings_cmbxHWIDServerSendTo";
			this.Settings_cmbxHWIDServerSendTo.Size = new System.Drawing.Size(121, 28);
			this.Settings_cmbxHWIDServerSendTo.TabIndex = 5;
			this.Settings_cmbxHWIDServerSendTo.Tag = "Source Sans Pro";
			this.ToolTips.SetToolTip(this.Settings_cmbxHWIDServerSendTo, "Send HWID to");
			// 
			// Settings_cmbxHWIDClientSaveFrom
			// 
			this.Settings_cmbxHWIDClientSaveFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_cmbxHWIDClientSaveFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Settings_cmbxHWIDClientSaveFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cmbxHWIDClientSaveFrom.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cmbxHWIDClientSaveFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cmbxHWIDClientSaveFrom.FormattingEnabled = true;
			this.Settings_cmbxHWIDClientSaveFrom.Items.AddRange(new object[] {
            "Agent",
            "Gateway",
            "Both"});
			this.Settings_cmbxHWIDClientSaveFrom.Location = new System.Drawing.Point(6, 48);
			this.Settings_cmbxHWIDClientSaveFrom.MaxDropDownItems = 5;
			this.Settings_cmbxHWIDClientSaveFrom.Name = "Settings_cmbxHWIDClientSaveFrom";
			this.Settings_cmbxHWIDClientSaveFrom.Size = new System.Drawing.Size(121, 28);
			this.Settings_cmbxHWIDClientSaveFrom.TabIndex = 2;
			this.Settings_cmbxHWIDClientSaveFrom.Tag = "Source Sans Pro";
			this.ToolTips.SetToolTip(this.Settings_cmbxHWIDClientSaveFrom, "Save HWID from");
			// 
			// Settings_tbxHWIDClientOp
			// 
			this.Settings_tbxHWIDClientOp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxHWIDClientOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxHWIDClientOp.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxHWIDClientOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxHWIDClientOp.Location = new System.Drawing.Point(54, 17);
			this.Settings_tbxHWIDClientOp.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_tbxHWIDClientOp.Name = "Settings_tbxHWIDClientOp";
			this.Settings_tbxHWIDClientOp.Size = new System.Drawing.Size(73, 28);
			this.Settings_tbxHWIDClientOp.TabIndex = 1;
			this.Settings_tbxHWIDClientOp.Tag = "Source Sans Pro";
			this.Settings_tbxHWIDClientOp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxHWIDClientOp.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxHWIDClientOp.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblHWIDClientOp
			// 
			this.Settings_lblHWIDClientOp.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblHWIDClientOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblHWIDClientOp.Location = new System.Drawing.Point(6, 17);
			this.Settings_lblHWIDClientOp.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblHWIDClientOp.Name = "Settings_lblHWIDClientOp";
			this.Settings_lblHWIDClientOp.Size = new System.Drawing.Size(48, 28);
			this.Settings_lblHWIDClientOp.TabIndex = 0;
			this.Settings_lblHWIDClientOp.Tag = "Source Sans Pro";
			this.Settings_lblHWIDClientOp.Text = "Client";
			this.Settings_lblHWIDClientOp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_tbxHWIDServerOp
			// 
			this.Settings_tbxHWIDServerOp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxHWIDServerOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxHWIDServerOp.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxHWIDServerOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxHWIDServerOp.Location = new System.Drawing.Point(59, 79);
			this.Settings_tbxHWIDServerOp.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_tbxHWIDServerOp.Name = "Settings_tbxHWIDServerOp";
			this.Settings_tbxHWIDServerOp.Size = new System.Drawing.Size(68, 28);
			this.Settings_tbxHWIDServerOp.TabIndex = 4;
			this.Settings_tbxHWIDServerOp.Tag = "Source Sans Pro";
			this.Settings_tbxHWIDServerOp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxHWIDServerOp.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxHWIDServerOp.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblHWIDServerOp
			// 
			this.Settings_lblHWIDServerOp.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblHWIDServerOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblHWIDServerOp.Location = new System.Drawing.Point(6, 79);
			this.Settings_lblHWIDServerOp.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblHWIDServerOp.Name = "Settings_lblHWIDServerOp";
			this.Settings_lblHWIDServerOp.Size = new System.Drawing.Size(53, 28);
			this.Settings_lblHWIDServerOp.TabIndex = 3;
			this.Settings_lblHWIDServerOp.Tag = "Source Sans Pro";
			this.Settings_lblHWIDServerOp.Text = "Server";
			this.Settings_lblHWIDServerOp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_btnAddSilkroad
			// 
			this.Settings_btnAddSilkroad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Settings_btnAddSilkroad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnAddSilkroad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Settings_btnAddSilkroad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Settings_btnAddSilkroad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_btnAddSilkroad.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_btnAddSilkroad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_btnAddSilkroad.Location = new System.Drawing.Point(147, 284);
			this.Settings_btnAddSilkroad.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_btnAddSilkroad.Name = "Settings_btnAddSilkroad";
			this.Settings_btnAddSilkroad.Size = new System.Drawing.Size(134, 25);
			this.Settings_btnAddSilkroad.TabIndex = 14;
			this.Settings_btnAddSilkroad.Tag = "Source Sans Pro";
			this.Settings_btnAddSilkroad.Text = "Edit Settings";
			this.Settings_btnAddSilkroad.UseCompatibleTextRendering = true;
			this.Settings_btnAddSilkroad.UseVisualStyleBackColor = false;
			this.Settings_btnAddSilkroad.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_tbxLocale
			// 
			this.Settings_tbxLocale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxLocale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxLocale.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxLocale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxLocale.Location = new System.Drawing.Point(66, 136);
			this.Settings_tbxLocale.Name = "Settings_tbxLocale";
			this.Settings_tbxLocale.ReadOnly = true;
			this.Settings_tbxLocale.Size = new System.Drawing.Size(75, 28);
			this.Settings_tbxLocale.TabIndex = 6;
			this.Settings_tbxLocale.Tag = "Source Sans Pro";
			this.Settings_tbxLocale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxLocale.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxLocale.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblLocale
			// 
			this.Settings_lblLocale.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblLocale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblLocale.Location = new System.Drawing.Point(6, 136);
			this.Settings_lblLocale.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblLocale.Name = "Settings_lblLocale";
			this.Settings_lblLocale.Size = new System.Drawing.Size(60, 28);
			this.Settings_lblLocale.TabIndex = 5;
			this.Settings_lblLocale.Tag = "Source Sans Pro";
			this.Settings_lblLocale.Text = "Locale";
			this.Settings_lblLocale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_lblHost
			// 
			this.Settings_lblHost.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblHost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblHost.Location = new System.Drawing.Point(6, 167);
			this.Settings_lblHost.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblHost.Name = "Settings_lblHost";
			this.Settings_lblHost.Size = new System.Drawing.Size(135, 28);
			this.Settings_lblHost.TabIndex = 7;
			this.Settings_lblHost.Tag = "Source Sans Pro";
			this.Settings_lblHost.Text = "Host(s)";
			this.Settings_lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Settings_tbxVersion
			// 
			this.Settings_tbxVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxVersion.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxVersion.Location = new System.Drawing.Point(66, 105);
			this.Settings_tbxVersion.Name = "Settings_tbxVersion";
			this.Settings_tbxVersion.ReadOnly = true;
			this.Settings_tbxVersion.Size = new System.Drawing.Size(75, 28);
			this.Settings_tbxVersion.TabIndex = 4;
			this.Settings_tbxVersion.Tag = "Source Sans Pro";
			this.Settings_tbxVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxVersion.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxVersion.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_lblVersion
			// 
			this.Settings_lblVersion.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblVersion.Location = new System.Drawing.Point(6, 105);
			this.Settings_lblVersion.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblVersion.Name = "Settings_lblVersion";
			this.Settings_lblVersion.Size = new System.Drawing.Size(60, 28);
			this.Settings_lblVersion.TabIndex = 3;
			this.Settings_lblVersion.Tag = "Source Sans Pro";
			this.Settings_lblVersion.Text = "Version";
			this.Settings_lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_lblPort
			// 
			this.Settings_lblPort.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblPort.Location = new System.Drawing.Point(6, 230);
			this.Settings_lblPort.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblPort.Name = "Settings_lblPort";
			this.Settings_lblPort.Size = new System.Drawing.Size(45, 28);
			this.Settings_lblPort.TabIndex = 10;
			this.Settings_lblPort.Tag = "Source Sans Pro";
			this.Settings_lblPort.Text = "Port";
			this.Settings_lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Settings_tbxPort
			// 
			this.Settings_tbxPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxPort.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_tbxPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxPort.Location = new System.Drawing.Point(51, 230);
			this.Settings_tbxPort.Name = "Settings_tbxPort";
			this.Settings_tbxPort.ReadOnly = true;
			this.Settings_tbxPort.Size = new System.Drawing.Size(90, 28);
			this.Settings_tbxPort.TabIndex = 11;
			this.Settings_tbxPort.Tag = "Source Sans Pro";
			this.Settings_tbxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxPort.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxPort.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// TabPageH_Settings_Option02_Panel
			// 
			this.TabPageH_Settings_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Settings_Option02_Panel.Controls.Add(this.Settings_gbxCharacterSelection);
			this.TabPageH_Settings_Option02_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Settings_Option02_Panel.Name = "TabPageH_Settings_Option02_Panel";
			this.TabPageH_Settings_Option02_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Settings_Option02_Panel.TabIndex = 9;
			this.TabPageH_Settings_Option02_Panel.Visible = false;
			// 
			// Settings_gbxCharacterSelection
			// 
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cmbxCreateCharGenre);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cbxLoadDefaultConfigs);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_lblCreateCharGenre);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_lblCreateCharRace);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cmbxCreateCharRace);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_tbxCustomSequence);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cbxSelectFirstChar);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cbxCreateChar);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_tbxCustomName);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cbxDeleteChar40to50);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_lblCustomSequence);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_cbxCreateCharBelow40);
			this.Settings_gbxCharacterSelection.Controls.Add(this.Settings_lblCustomName);
			this.Settings_gbxCharacterSelection.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Settings_gbxCharacterSelection.ForeColor = System.Drawing.Color.LightGray;
			this.Settings_gbxCharacterSelection.Location = new System.Drawing.Point(6, 0);
			this.Settings_gbxCharacterSelection.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_gbxCharacterSelection.Name = "Settings_gbxCharacterSelection";
			this.Settings_gbxCharacterSelection.Size = new System.Drawing.Size(218, 269);
			this.Settings_gbxCharacterSelection.TabIndex = 4;
			this.Settings_gbxCharacterSelection.TabStop = false;
			this.Settings_gbxCharacterSelection.Tag = "Source Sans Pro";
			this.Settings_gbxCharacterSelection.Text = "Character selection";
			// 
			// Settings_cmbxCreateCharGenre
			// 
			this.Settings_cmbxCreateCharGenre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_cmbxCreateCharGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Settings_cmbxCreateCharGenre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cmbxCreateCharGenre.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cmbxCreateCharGenre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cmbxCreateCharGenre.FormattingEnabled = true;
			this.Settings_cmbxCreateCharGenre.Items.AddRange(new object[] {
            "Random",
            "Male",
            "Female"});
			this.Settings_cmbxCreateCharGenre.Location = new System.Drawing.Point(136, 201);
			this.Settings_cmbxCreateCharGenre.MaxDropDownItems = 5;
			this.Settings_cmbxCreateCharGenre.Name = "Settings_cmbxCreateCharGenre";
			this.Settings_cmbxCreateCharGenre.Size = new System.Drawing.Size(71, 25);
			this.Settings_cmbxCreateCharGenre.TabIndex = 12;
			this.Settings_cmbxCreateCharGenre.Tag = "Source Sans Pro";
			this.Settings_cmbxCreateCharGenre.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_cmbxCreateCharGenre.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_cbxLoadDefaultConfigs
			// 
			this.Settings_cbxLoadDefaultConfigs.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxLoadDefaultConfigs.FlatAppearance.BorderSize = 0;
			this.Settings_cbxLoadDefaultConfigs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxLoadDefaultConfigs.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxLoadDefaultConfigs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxLoadDefaultConfigs.Location = new System.Drawing.Point(8, 227);
			this.Settings_cbxLoadDefaultConfigs.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxLoadDefaultConfigs.Name = "Settings_cbxLoadDefaultConfigs";
			this.Settings_cbxLoadDefaultConfigs.Size = new System.Drawing.Size(199, 40);
			this.Settings_cbxLoadDefaultConfigs.TabIndex = 4;
			this.Settings_cbxLoadDefaultConfigs.Tag = "Source Sans Pro";
			this.Settings_cbxLoadDefaultConfigs.Text = "Load default configs if cannot be found";
			this.Settings_cbxLoadDefaultConfigs.UseVisualStyleBackColor = false;
			this.Settings_cbxLoadDefaultConfigs.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_lblCreateCharGenre
			// 
			this.Settings_lblCreateCharGenre.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblCreateCharGenre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblCreateCharGenre.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Settings_lblCreateCharGenre.Location = new System.Drawing.Point(91, 201);
			this.Settings_lblCreateCharGenre.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblCreateCharGenre.Name = "Settings_lblCreateCharGenre";
			this.Settings_lblCreateCharGenre.Size = new System.Drawing.Size(45, 25);
			this.Settings_lblCreateCharGenre.TabIndex = 11;
			this.Settings_lblCreateCharGenre.Tag = "Source Sans Pro";
			this.Settings_lblCreateCharGenre.Text = "Genre";
			this.Settings_lblCreateCharGenre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Settings_lblCreateCharGenre, "Create character genre");
			// 
			// Settings_lblCreateCharRace
			// 
			this.Settings_lblCreateCharRace.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblCreateCharRace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblCreateCharRace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Settings_lblCreateCharRace.Location = new System.Drawing.Point(8, 201);
			this.Settings_lblCreateCharRace.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblCreateCharRace.Name = "Settings_lblCreateCharRace";
			this.Settings_lblCreateCharRace.Size = new System.Drawing.Size(40, 25);
			this.Settings_lblCreateCharRace.TabIndex = 10;
			this.Settings_lblCreateCharRace.Tag = "Source Sans Pro";
			this.Settings_lblCreateCharRace.Text = "Race";
			this.Settings_lblCreateCharRace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Settings_lblCreateCharRace, "Create character race");
			// 
			// Settings_cmbxCreateCharRace
			// 
			this.Settings_cmbxCreateCharRace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_cmbxCreateCharRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Settings_cmbxCreateCharRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cmbxCreateCharRace.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cmbxCreateCharRace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cmbxCreateCharRace.FormattingEnabled = true;
			this.Settings_cmbxCreateCharRace.Items.AddRange(new object[] {
            "CH",
            "EU"});
			this.Settings_cmbxCreateCharRace.Location = new System.Drawing.Point(48, 201);
			this.Settings_cmbxCreateCharRace.MaxDropDownItems = 5;
			this.Settings_cmbxCreateCharRace.Name = "Settings_cmbxCreateCharRace";
			this.Settings_cmbxCreateCharRace.Size = new System.Drawing.Size(41, 25);
			this.Settings_cmbxCreateCharRace.TabIndex = 6;
			this.Settings_cmbxCreateCharRace.Tag = "Source Sans Pro";
			this.Settings_cmbxCreateCharRace.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_cmbxCreateCharRace.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_tbxCustomSequence
			// 
			this.Settings_tbxCustomSequence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxCustomSequence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxCustomSequence.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.Settings_tbxCustomSequence.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_tbxCustomSequence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxCustomSequence.Location = new System.Drawing.Point(176, 175);
			this.Settings_tbxCustomSequence.MaxLength = 3;
			this.Settings_tbxCustomSequence.Name = "Settings_tbxCustomSequence";
			this.Settings_tbxCustomSequence.Size = new System.Drawing.Size(31, 25);
			this.Settings_tbxCustomSequence.TabIndex = 9;
			this.Settings_tbxCustomSequence.Tag = "Source Sans Pro";
			this.Settings_tbxCustomSequence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Settings_tbxCustomSequence.TextChanged += new System.EventHandler(this.Control_TextChanged);
			this.Settings_tbxCustomSequence.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxCustomSequence.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_cbxSelectFirstChar
			// 
			this.Settings_cbxSelectFirstChar.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxSelectFirstChar.FlatAppearance.BorderSize = 0;
			this.Settings_cbxSelectFirstChar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxSelectFirstChar.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxSelectFirstChar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxSelectFirstChar.Location = new System.Drawing.Point(9, 15);
			this.Settings_cbxSelectFirstChar.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxSelectFirstChar.Name = "Settings_cbxSelectFirstChar";
			this.Settings_cbxSelectFirstChar.Size = new System.Drawing.Size(199, 25);
			this.Settings_cbxSelectFirstChar.TabIndex = 5;
			this.Settings_cbxSelectFirstChar.Tag = "Source Sans Pro";
			this.Settings_cbxSelectFirstChar.Text = "Select the first available";
			this.ToolTips.SetToolTip(this.Settings_cbxSelectFirstChar, "Select the first character not being deleted");
			this.Settings_cbxSelectFirstChar.UseVisualStyleBackColor = false;
			this.Settings_cbxSelectFirstChar.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_cbxCreateChar
			// 
			this.Settings_cbxCreateChar.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxCreateChar.FlatAppearance.BorderSize = 0;
			this.Settings_cbxCreateChar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxCreateChar.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxCreateChar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxCreateChar.Location = new System.Drawing.Point(8, 82);
			this.Settings_cbxCreateChar.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxCreateChar.Name = "Settings_cbxCreateChar";
			this.Settings_cbxCreateChar.Size = new System.Drawing.Size(199, 25);
			this.Settings_cbxCreateChar.TabIndex = 1;
			this.Settings_cbxCreateChar.Tag = "Source Sans Pro";
			this.Settings_cbxCreateChar.Text = "Create if not exists";
			this.Settings_cbxCreateChar.UseVisualStyleBackColor = false;
			this.Settings_cbxCreateChar.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_tbxCustomName
			// 
			this.Settings_tbxCustomName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_tbxCustomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Settings_tbxCustomName.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_tbxCustomName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_tbxCustomName.Location = new System.Drawing.Point(127, 149);
			this.Settings_tbxCustomName.MaxLength = 12;
			this.Settings_tbxCustomName.Name = "Settings_tbxCustomName";
			this.Settings_tbxCustomName.Size = new System.Drawing.Size(80, 25);
			this.Settings_tbxCustomName.TabIndex = 7;
			this.Settings_tbxCustomName.Tag = "Source Sans Pro";
			this.Settings_tbxCustomName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ToolTips.SetToolTip(this.Settings_tbxCustomName, "Leave it empty to use a random nickname");
			this.Settings_tbxCustomName.TextChanged += new System.EventHandler(this.Control_TextChanged);
			this.Settings_tbxCustomName.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Settings_tbxCustomName.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Settings_cbxDeleteChar40to50
			// 
			this.Settings_cbxDeleteChar40to50.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxDeleteChar40to50.FlatAppearance.BorderSize = 0;
			this.Settings_cbxDeleteChar40to50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxDeleteChar40to50.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxDeleteChar40to50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxDeleteChar40to50.Location = new System.Drawing.Point(9, 41);
			this.Settings_cbxDeleteChar40to50.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxDeleteChar40to50.Name = "Settings_cbxDeleteChar40to50";
			this.Settings_cbxDeleteChar40to50.Size = new System.Drawing.Size(199, 40);
			this.Settings_cbxDeleteChar40to50.TabIndex = 3;
			this.Settings_cbxDeleteChar40to50.Tag = "Source Sans Pro";
			this.Settings_cbxDeleteChar40to50.Text = "Delete if any between Lv. 40 and 50 is found";
			this.Settings_cbxDeleteChar40to50.UseVisualStyleBackColor = false;
			this.Settings_cbxDeleteChar40to50.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_lblCustomSequence
			// 
			this.Settings_lblCustomSequence.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblCustomSequence.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblCustomSequence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Settings_lblCustomSequence.Location = new System.Drawing.Point(8, 175);
			this.Settings_lblCustomSequence.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblCustomSequence.Name = "Settings_lblCustomSequence";
			this.Settings_lblCustomSequence.Size = new System.Drawing.Size(168, 25);
			this.Settings_lblCustomSequence.TabIndex = 8;
			this.Settings_lblCustomSequence.Tag = "Source Sans Pro";
			this.Settings_lblCustomSequence.Text = "Current sequence number";
			this.Settings_lblCustomSequence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Settings_lblCustomSequence, "Current custom sequence nickname number");
			// 
			// Settings_cbxCreateCharBelow40
			// 
			this.Settings_cbxCreateCharBelow40.Cursor = System.Windows.Forms.Cursors.Default;
			this.Settings_cbxCreateCharBelow40.FlatAppearance.BorderSize = 0;
			this.Settings_cbxCreateCharBelow40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Settings_cbxCreateCharBelow40.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_cbxCreateCharBelow40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_cbxCreateCharBelow40.Location = new System.Drawing.Point(8, 108);
			this.Settings_cbxCreateCharBelow40.Margin = new System.Windows.Forms.Padding(0);
			this.Settings_cbxCreateCharBelow40.Name = "Settings_cbxCreateCharBelow40";
			this.Settings_cbxCreateCharBelow40.Size = new System.Drawing.Size(199, 40);
			this.Settings_cbxCreateCharBelow40.TabIndex = 2;
			this.Settings_cbxCreateCharBelow40.Tag = "Source Sans Pro";
			this.Settings_cbxCreateCharBelow40.Text = "Create if none below Lv. 40 is found";
			this.Settings_cbxCreateCharBelow40.UseVisualStyleBackColor = false;
			this.Settings_cbxCreateCharBelow40.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Settings_lblCustomName
			// 
			this.Settings_lblCustomName.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Settings_lblCustomName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_lblCustomName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Settings_lblCustomName.Location = new System.Drawing.Point(8, 149);
			this.Settings_lblCustomName.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Settings_lblCustomName.Name = "Settings_lblCustomName";
			this.Settings_lblCustomName.Size = new System.Drawing.Size(119, 25);
			this.Settings_lblCustomName.TabIndex = 6;
			this.Settings_lblCustomName.Tag = "Source Sans Pro";
			this.Settings_lblCustomName.Text = "Use custom name";
			this.Settings_lblCustomName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Settings_lblCustomName, "Create character with a custom sequence nickname");
			// 
			// TabPageH_Settings_Option03_Panel
			// 
			this.TabPageH_Settings_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Settings_Option03_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Settings_Option03_Panel.Name = "TabPageH_Settings_Option03_Panel";
			this.TabPageH_Settings_Option03_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Settings_Option03_Panel.TabIndex = 10;
			this.TabPageH_Settings_Option03_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option03_Panel
			// 
			this.TabPageV_Control01_Option03_Panel.Controls.Add(this.TabPageH_Inventory);
			this.TabPageV_Control01_Option03_Panel.Controls.Add(this.TabPageH_Inventory_Option01_Panel);
			this.TabPageV_Control01_Option03_Panel.Controls.Add(this.TabPageH_Inventory_Option03_Panel);
			this.TabPageV_Control01_Option03_Panel.Controls.Add(this.TabPageH_Inventory_Option02_Panel);
			this.TabPageV_Control01_Option03_Panel.Controls.Add(this.TabPageH_Inventory_Option04_Panel);
			this.TabPageV_Control01_Option03_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option03_Panel.Name = "TabPageV_Control01_Option03_Panel";
			this.TabPageV_Control01_Option03_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option03_Panel.TabIndex = 7;
			this.TabPageV_Control01_Option03_Panel.Visible = false;
			// 
			// TabPageH_Inventory
			// 
			this.TabPageH_Inventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Inventory.Controls.Add(this.TabPageH_Inventory_Option04);
			this.TabPageH_Inventory.Controls.Add(this.TabPageH_Inventory_Option03);
			this.TabPageH_Inventory.Controls.Add(this.TabPageH_Inventory_Option02);
			this.TabPageH_Inventory.Controls.Add(this.TabPageH_Inventory_Option01);
			this.TabPageH_Inventory.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Inventory.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Inventory.Name = "TabPageH_Inventory";
			this.TabPageH_Inventory.Size = new System.Drawing.Size(657, 28);
			this.TabPageH_Inventory.TabIndex = 9;
			// 
			// TabPageH_Inventory_Option04
			// 
			this.TabPageH_Inventory_Option04.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Inventory_Option04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Inventory_Option04.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option04.FlatAppearance.BorderSize = 0;
			this.TabPageH_Inventory_Option04.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Inventory_Option04.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Inventory_Option04.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Inventory_Option04.Location = new System.Drawing.Point(493, 0);
			this.TabPageH_Inventory_Option04.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Inventory_Option04.Name = "TabPageH_Inventory_Option04";
			this.TabPageH_Inventory_Option04.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Inventory_Option04.TabIndex = 15;
			this.TabPageH_Inventory_Option04.Text = ". . .";
			this.TabPageH_Inventory_Option04.UseVisualStyleBackColor = false;
			this.TabPageH_Inventory_Option04.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Inventory_Option03
			// 
			this.TabPageH_Inventory_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Inventory_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Inventory_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Inventory_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Inventory_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Inventory_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Inventory_Option03.Location = new System.Drawing.Point(329, 0);
			this.TabPageH_Inventory_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Inventory_Option03.Name = "TabPageH_Inventory_Option03";
			this.TabPageH_Inventory_Option03.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Inventory_Option03.TabIndex = 14;
			this.TabPageH_Inventory_Option03.Text = ". . .";
			this.TabPageH_Inventory_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Inventory_Option03.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Inventory_Option02
			// 
			this.TabPageH_Inventory_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Inventory_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Inventory_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Inventory_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Inventory_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Inventory_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Inventory_Option02.Location = new System.Drawing.Point(165, 0);
			this.TabPageH_Inventory_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Inventory_Option02.Name = "TabPageH_Inventory_Option02";
			this.TabPageH_Inventory_Option02.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Inventory_Option02.TabIndex = 13;
			this.TabPageH_Inventory_Option02.Text = ". . .";
			this.TabPageH_Inventory_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Inventory_Option02.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Inventory_Option01
			// 
			this.TabPageH_Inventory_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Inventory_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Inventory_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Inventory_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Inventory_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Inventory_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Inventory_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Inventory_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Inventory_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Inventory_Option01.Name = "TabPageH_Inventory_Option01";
			this.TabPageH_Inventory_Option01.Size = new System.Drawing.Size(165, 26);
			this.TabPageH_Inventory_Option01.TabIndex = 12;
			this.TabPageH_Inventory_Option01.Text = "Info";
			this.TabPageH_Inventory_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Inventory_Option01.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Inventory_Option01_Panel
			// 
			this.TabPageH_Inventory_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Inventory_Option01_Panel.Controls.Add(this.Inventory_lblCapacity);
			this.TabPageH_Inventory_Option01_Panel.Controls.Add(this.Inventory_btnRefresh);
			this.TabPageH_Inventory_Option01_Panel.Controls.Add(this.Inventory_lstvItems);
			this.TabPageH_Inventory_Option01_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Inventory_Option01_Panel.Name = "TabPageH_Inventory_Option01_Panel";
			this.TabPageH_Inventory_Option01_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Inventory_Option01_Panel.TabIndex = 10;
			this.TabPageH_Inventory_Option01_Panel.Visible = false;
			// 
			// Inventory_lblCapacity
			// 
			this.Inventory_lblCapacity.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Inventory_lblCapacity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Inventory_lblCapacity.Location = new System.Drawing.Point(6, 281);
			this.Inventory_lblCapacity.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Inventory_lblCapacity.Name = "Inventory_lblCapacity";
			this.Inventory_lblCapacity.Size = new System.Drawing.Size(130, 28);
			this.Inventory_lblCapacity.TabIndex = 13;
			this.Inventory_lblCapacity.Tag = "Source Sans Pro";
			this.Inventory_lblCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Inventory_btnRefresh
			// 
			this.Inventory_btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Inventory_btnRefresh.FlatAppearance.BorderSize = 0;
			this.Inventory_btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Inventory_btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Inventory_btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Inventory_btnRefresh.Font = new System.Drawing.Font("Font Awesome 5 Pro Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Inventory_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Inventory_btnRefresh.Location = new System.Drawing.Point(621, 281);
			this.Inventory_btnRefresh.Margin = new System.Windows.Forms.Padding(0);
			this.Inventory_btnRefresh.Name = "Inventory_btnRefresh";
			this.Inventory_btnRefresh.Size = new System.Drawing.Size(28, 28);
			this.Inventory_btnRefresh.TabIndex = 12;
			this.Inventory_btnRefresh.Tag = "Font Awesome 5 Pro Light";
			this.Inventory_btnRefresh.Text = "";
			this.Inventory_btnRefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ToolTips.SetToolTip(this.Inventory_btnRefresh, "Refresh");
			this.Inventory_btnRefresh.UseCompatibleTextRendering = true;
			this.Inventory_btnRefresh.UseVisualStyleBackColor = false;
			this.Inventory_btnRefresh.Click += new System.EventHandler(this.Control_Click);
			// 
			// Inventory_lstvItems
			// 
			this.Inventory_lstvItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Inventory_lstvItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Inventory_lstvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader23,
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader27});
			this.Inventory_lstvItems.ContextMenuStrip = this.Menu_lstvItems;
			this.Inventory_lstvItems.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Inventory_lstvItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Inventory_lstvItems.FullRowSelect = true;
			this.Inventory_lstvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.Inventory_lstvItems.HideSelection = false;
			this.Inventory_lstvItems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Inventory_lstvItems.Location = new System.Drawing.Point(6, 7);
			this.Inventory_lstvItems.Margin = new System.Windows.Forms.Padding(0);
			this.Inventory_lstvItems.MultiSelect = false;
			this.Inventory_lstvItems.Name = "Inventory_lstvItems";
			this.Inventory_lstvItems.ShowGroups = false;
			this.Inventory_lstvItems.ShowItemToolTips = true;
			this.Inventory_lstvItems.Size = new System.Drawing.Size(643, 270);
			this.Inventory_lstvItems.SmallImageList = this.lstimgIcons;
			this.Inventory_lstvItems.TabIndex = 3;
			this.Inventory_lstvItems.Tag = "Source Sans Pro";
			this.Inventory_lstvItems.TileSize = new System.Drawing.Size(201, 50);
			this.Inventory_lstvItems.UseCompatibleStateImageBehavior = false;
			this.Inventory_lstvItems.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "Slot";
			this.columnHeader23.Width = 64;
			// 
			// columnHeader25
			// 
			this.columnHeader25.Text = "Name";
			this.columnHeader25.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader25.Width = 230;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Text = "Quantity";
			this.columnHeader26.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader26.Width = 80;
			// 
			// columnHeader27
			// 
			this.columnHeader27.Text = "Servername";
			this.columnHeader27.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader27.Width = 250;
			// 
			// Menu_lstvItems
			// 
			this.Menu_lstvItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvItems_Use,
            this.Menu_lstvItems_Drop,
            this.Menu_lstvItems_Separator01,
            this.Menu_lstvItems_Equip});
			this.Menu_lstvItems.Name = "Menu_lstvHost";
			this.Menu_lstvItems.Size = new System.Drawing.Size(161, 76);
			// 
			// Menu_lstvItems_Use
			// 
			this.Menu_lstvItems_Use.Name = "Menu_lstvItems_Use";
			this.Menu_lstvItems_Use.Size = new System.Drawing.Size(160, 22);
			this.Menu_lstvItems_Use.Text = "Use";
			this.Menu_lstvItems_Use.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvItems_Drop
			// 
			this.Menu_lstvItems_Drop.Name = "Menu_lstvItems_Drop";
			this.Menu_lstvItems_Drop.Size = new System.Drawing.Size(160, 22);
			this.Menu_lstvItems_Drop.Text = "Drop";
			this.Menu_lstvItems_Drop.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvItems_Separator01
			// 
			this.Menu_lstvItems_Separator01.Name = "Menu_lstvItems_Separator01";
			this.Menu_lstvItems_Separator01.Size = new System.Drawing.Size(157, 6);
			// 
			// Menu_lstvItems_Equip
			// 
			this.Menu_lstvItems_Equip.Name = "Menu_lstvItems_Equip";
			this.Menu_lstvItems_Equip.Size = new System.Drawing.Size(160, 22);
			this.Menu_lstvItems_Equip.Text = "Equip / Unequip";
			this.Menu_lstvItems_Equip.Click += new System.EventHandler(this.Menu_Click);
			// 
			// lstimgIcons
			// 
			this.lstimgIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.lstimgIcons.ImageSize = new System.Drawing.Size(24, 24);
			this.lstimgIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TabPageH_Inventory_Option03_Panel
			// 
			this.TabPageH_Inventory_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Inventory_Option03_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Inventory_Option03_Panel.Name = "TabPageH_Inventory_Option03_Panel";
			this.TabPageH_Inventory_Option03_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Inventory_Option03_Panel.TabIndex = 11;
			this.TabPageH_Inventory_Option03_Panel.Visible = false;
			// 
			// TabPageH_Inventory_Option02_Panel
			// 
			this.TabPageH_Inventory_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Inventory_Option02_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Inventory_Option02_Panel.Name = "TabPageH_Inventory_Option02_Panel";
			this.TabPageH_Inventory_Option02_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Inventory_Option02_Panel.TabIndex = 13;
			this.TabPageH_Inventory_Option02_Panel.Visible = false;
			// 
			// TabPageH_Inventory_Option04_Panel
			// 
			this.TabPageH_Inventory_Option04_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Inventory_Option04_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Inventory_Option04_Panel.Name = "TabPageH_Inventory_Option04_Panel";
			this.TabPageH_Inventory_Option04_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Inventory_Option04_Panel.TabIndex = 12;
			this.TabPageH_Inventory_Option04_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option04_Panel
			// 
			this.TabPageV_Control01_Option04_Panel.Controls.Add(this.TabPageH_Party);
			this.TabPageV_Control01_Option04_Panel.Controls.Add(this.TabPageH_Party_Option03_Panel);
			this.TabPageV_Control01_Option04_Panel.Controls.Add(this.TabPageH_Party_Option02_Panel);
			this.TabPageV_Control01_Option04_Panel.Controls.Add(this.TabPageH_Party_Option01_Panel);
			this.TabPageV_Control01_Option04_Panel.Controls.Add(this.TabPageH_Party_Option04_Panel);
			this.TabPageV_Control01_Option04_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option04_Panel.Name = "TabPageV_Control01_Option04_Panel";
			this.TabPageV_Control01_Option04_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option04_Panel.TabIndex = 8;
			this.TabPageV_Control01_Option04_Panel.Visible = false;
			// 
			// TabPageH_Party
			// 
			this.TabPageH_Party.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Party.Controls.Add(this.TabPageH_Party_Option04);
			this.TabPageH_Party.Controls.Add(this.TabPageH_Party_Option03);
			this.TabPageH_Party.Controls.Add(this.TabPageH_Party_Option02);
			this.TabPageH_Party.Controls.Add(this.TabPageH_Party_Option01);
			this.TabPageH_Party.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Party.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Party.Name = "TabPageH_Party";
			this.TabPageH_Party.Size = new System.Drawing.Size(657, 28);
			this.TabPageH_Party.TabIndex = 4;
			// 
			// TabPageH_Party_Option04
			// 
			this.TabPageH_Party_Option04.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Party_Option04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Party_Option04.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option04.FlatAppearance.BorderSize = 0;
			this.TabPageH_Party_Option04.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Party_Option04.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Party_Option04.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Party_Option04.Location = new System.Drawing.Point(493, 0);
			this.TabPageH_Party_Option04.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Party_Option04.Name = "TabPageH_Party_Option04";
			this.TabPageH_Party_Option04.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Party_Option04.TabIndex = 15;
			this.TabPageH_Party_Option04.Text = ". . .";
			this.TabPageH_Party_Option04.UseVisualStyleBackColor = false;
			this.TabPageH_Party_Option04.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Party_Option03
			// 
			this.TabPageH_Party_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Party_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Party_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Party_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Party_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Party_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Party_Option03.Location = new System.Drawing.Point(329, 0);
			this.TabPageH_Party_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Party_Option03.Name = "TabPageH_Party_Option03";
			this.TabPageH_Party_Option03.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Party_Option03.TabIndex = 14;
			this.TabPageH_Party_Option03.Text = "Match";
			this.TabPageH_Party_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Party_Option03.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Party_Option02
			// 
			this.TabPageH_Party_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Party_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Party_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Party_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Party_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Party_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Party_Option02.Location = new System.Drawing.Point(165, 0);
			this.TabPageH_Party_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Party_Option02.Name = "TabPageH_Party_Option02";
			this.TabPageH_Party_Option02.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Party_Option02.TabIndex = 13;
			this.TabPageH_Party_Option02.Text = "Options";
			this.TabPageH_Party_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Party_Option02.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Party_Option01
			// 
			this.TabPageH_Party_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Party_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Party_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Party_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Party_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Party_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Party_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Party_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Party_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Party_Option01.Name = "TabPageH_Party_Option01";
			this.TabPageH_Party_Option01.Size = new System.Drawing.Size(165, 26);
			this.TabPageH_Party_Option01.TabIndex = 12;
			this.TabPageH_Party_Option01.Text = "Info";
			this.TabPageH_Party_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Party_Option01.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Party_Option03_Panel
			// 
			this.TabPageH_Party_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_btnJoinMatch);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_tbxJoinToNumber);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_lblJoinToNumber);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_btnLastPage);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_btnNextPage);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_lblPageNumber);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_btnRefreshMatch);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_lstvPartyMatch);
			this.TabPageH_Party_Option03_Panel.Controls.Add(this.Party_pnlAutoFormMatch);
			this.TabPageH_Party_Option03_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Party_Option03_Panel.Name = "TabPageH_Party_Option03_Panel";
			this.TabPageH_Party_Option03_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Party_Option03_Panel.TabIndex = 7;
			this.TabPageH_Party_Option03_Panel.Visible = false;
			// 
			// Party_btnJoinMatch
			// 
			this.Party_btnJoinMatch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_btnJoinMatch.FlatAppearance.BorderSize = 0;
			this.Party_btnJoinMatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Party_btnJoinMatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnJoinMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_btnJoinMatch.Font = new System.Drawing.Font("Font Awesome 5 Pro Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_btnJoinMatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_btnJoinMatch.Location = new System.Drawing.Point(166, 281);
			this.Party_btnJoinMatch.Margin = new System.Windows.Forms.Padding(0);
			this.Party_btnJoinMatch.Name = "Party_btnJoinMatch";
			this.Party_btnJoinMatch.Size = new System.Drawing.Size(28, 28);
			this.Party_btnJoinMatch.TabIndex = 5;
			this.Party_btnJoinMatch.Tag = "Font Awesome 5 Pro Light";
			this.Party_btnJoinMatch.Text = "";
			this.Party_btnJoinMatch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ToolTips.SetToolTip(this.Party_btnJoinMatch, "Join to Party Match");
			this.Party_btnJoinMatch.UseCompatibleTextRendering = true;
			this.Party_btnJoinMatch.UseVisualStyleBackColor = false;
			this.Party_btnJoinMatch.Click += new System.EventHandler(this.Control_Click);
			// 
			// Party_tbxJoinToNumber
			// 
			this.Party_tbxJoinToNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Party_tbxJoinToNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_tbxJoinToNumber.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_tbxJoinToNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_tbxJoinToNumber.Location = new System.Drawing.Point(73, 281);
			this.Party_tbxJoinToNumber.MaxLength = 5;
			this.Party_tbxJoinToNumber.Name = "Party_tbxJoinToNumber";
			this.Party_tbxJoinToNumber.Size = new System.Drawing.Size(90, 28);
			this.Party_tbxJoinToNumber.TabIndex = 4;
			this.Party_tbxJoinToNumber.Tag = "Source Sans Pro";
			this.Party_tbxJoinToNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Party_tbxJoinToNumber.TextChanged += new System.EventHandler(this.Control_TextChanged);
			this.Party_tbxJoinToNumber.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Party_tbxJoinToNumber.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Party_lblJoinToNumber
			// 
			this.Party_lblJoinToNumber.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lblJoinToNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lblJoinToNumber.Location = new System.Drawing.Point(6, 281);
			this.Party_lblJoinToNumber.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Party_lblJoinToNumber.Name = "Party_lblJoinToNumber";
			this.Party_lblJoinToNumber.Size = new System.Drawing.Size(67, 28);
			this.Party_lblJoinToNumber.TabIndex = 3;
			this.Party_lblJoinToNumber.Tag = "Source Sans Pro";
			this.Party_lblJoinToNumber.Text = "Join to #";
			this.Party_lblJoinToNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Party_btnLastPage
			// 
			this.Party_btnLastPage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_btnLastPage.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_btnLastPage.Enabled = false;
			this.Party_btnLastPage.FlatAppearance.BorderSize = 0;
			this.Party_btnLastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Party_btnLastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_btnLastPage.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_btnLastPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_btnLastPage.Location = new System.Drawing.Point(543, 281);
			this.Party_btnLastPage.Margin = new System.Windows.Forms.Padding(0);
			this.Party_btnLastPage.Name = "Party_btnLastPage";
			this.Party_btnLastPage.Size = new System.Drawing.Size(20, 28);
			this.Party_btnLastPage.TabIndex = 6;
			this.Party_btnLastPage.Tag = "Font Awesome 5 Pro Solid";
			this.Party_btnLastPage.Text = "";
			this.Party_btnLastPage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ToolTips.SetToolTip(this.Party_btnLastPage, "Last Page");
			this.Party_btnLastPage.UseCompatibleTextRendering = true;
			this.Party_btnLastPage.UseVisualStyleBackColor = false;
			this.Party_btnLastPage.Click += new System.EventHandler(this.Control_Click);
			// 
			// Party_btnNextPage
			// 
			this.Party_btnNextPage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_btnNextPage.Enabled = false;
			this.Party_btnNextPage.FlatAppearance.BorderSize = 0;
			this.Party_btnNextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Party_btnNextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_btnNextPage.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_btnNextPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_btnNextPage.Location = new System.Drawing.Point(596, 281);
			this.Party_btnNextPage.Margin = new System.Windows.Forms.Padding(0);
			this.Party_btnNextPage.Name = "Party_btnNextPage";
			this.Party_btnNextPage.Size = new System.Drawing.Size(20, 28);
			this.Party_btnNextPage.TabIndex = 8;
			this.Party_btnNextPage.Tag = "Font Awesome 5 Pro Solid";
			this.Party_btnNextPage.Text = "";
			this.Party_btnNextPage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ToolTips.SetToolTip(this.Party_btnNextPage, "Next Page");
			this.Party_btnNextPage.UseCompatibleTextRendering = true;
			this.Party_btnNextPage.UseVisualStyleBackColor = false;
			this.Party_btnNextPage.Click += new System.EventHandler(this.Control_Click);
			// 
			// Party_lblPageNumber
			// 
			this.Party_lblPageNumber.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lblPageNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lblPageNumber.Location = new System.Drawing.Point(563, 281);
			this.Party_lblPageNumber.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Party_lblPageNumber.Name = "Party_lblPageNumber";
			this.Party_lblPageNumber.Size = new System.Drawing.Size(33, 28);
			this.Party_lblPageNumber.TabIndex = 7;
			this.Party_lblPageNumber.Tag = "Source Sans Pro";
			this.Party_lblPageNumber.Text = "0";
			this.Party_lblPageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ToolTips.SetToolTip(this.Party_lblPageNumber, "Current Page");
			this.Party_lblPageNumber.UseMnemonic = false;
			// 
			// Party_btnRefreshMatch
			// 
			this.Party_btnRefreshMatch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_btnRefreshMatch.FlatAppearance.BorderSize = 0;
			this.Party_btnRefreshMatch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Party_btnRefreshMatch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnRefreshMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_btnRefreshMatch.Font = new System.Drawing.Font("Font Awesome 5 Pro Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_btnRefreshMatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_btnRefreshMatch.Location = new System.Drawing.Point(621, 281);
			this.Party_btnRefreshMatch.Margin = new System.Windows.Forms.Padding(0);
			this.Party_btnRefreshMatch.Name = "Party_btnRefreshMatch";
			this.Party_btnRefreshMatch.Size = new System.Drawing.Size(28, 28);
			this.Party_btnRefreshMatch.TabIndex = 9;
			this.Party_btnRefreshMatch.Tag = "Font Awesome 5 Pro Light";
			this.Party_btnRefreshMatch.Text = "";
			this.Party_btnRefreshMatch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.ToolTips.SetToolTip(this.Party_btnRefreshMatch, "Refresh Party Match");
			this.Party_btnRefreshMatch.UseCompatibleTextRendering = true;
			this.Party_btnRefreshMatch.UseVisualStyleBackColor = false;
			this.Party_btnRefreshMatch.Click += new System.EventHandler(this.Control_Click);
			// 
			// Party_lstvPartyMatch
			// 
			this.Party_lstvPartyMatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Party_lstvPartyMatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_lstvPartyMatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22});
			this.Party_lstvPartyMatch.ContextMenuStrip = this.Menu_lstvPartyMatch;
			this.Party_lstvPartyMatch.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_lstvPartyMatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lstvPartyMatch.FullRowSelect = true;
			this.Party_lstvPartyMatch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.Party_lstvPartyMatch.HideSelection = false;
			this.Party_lstvPartyMatch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Party_lstvPartyMatch.Location = new System.Drawing.Point(6, 67);
			this.Party_lstvPartyMatch.Margin = new System.Windows.Forms.Padding(0);
			this.Party_lstvPartyMatch.MultiSelect = false;
			this.Party_lstvPartyMatch.Name = "Party_lstvPartyMatch";
			this.Party_lstvPartyMatch.ShowGroups = false;
			this.Party_lstvPartyMatch.ShowItemToolTips = true;
			this.Party_lstvPartyMatch.Size = new System.Drawing.Size(643, 210);
			this.Party_lstvPartyMatch.TabIndex = 2;
			this.Party_lstvPartyMatch.Tag = "Source Sans Pro";
			this.Party_lstvPartyMatch.TileSize = new System.Drawing.Size(201, 50);
			this.Party_lstvPartyMatch.UseCompatibleStateImageBehavior = false;
			this.Party_lstvPartyMatch.View = System.Windows.Forms.View.Details;
			this.Party_lstvPartyMatch.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging_Cancel);
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "#";
			this.columnHeader17.Width = 55;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Master";
			this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader18.Width = 110;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "Title";
			this.columnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader19.Width = 262;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Level";
			this.columnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader20.Width = 62;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Capacity";
			this.columnHeader21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader21.Width = 68;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "Purpose";
			this.columnHeader22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader22.Width = 65;
			// 
			// Menu_lstvPartyMatch
			// 
			this.Menu_lstvPartyMatch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvPartyMatch_JoinToParty,
            this.Menu_lstvPartyMatch_PrivateMsg});
			this.Menu_lstvPartyMatch.Name = "Menu_lstvHost";
			this.Menu_lstvPartyMatch.Size = new System.Drawing.Size(160, 48);
			// 
			// Menu_lstvPartyMatch_JoinToParty
			// 
			this.Menu_lstvPartyMatch_JoinToParty.Name = "Menu_lstvPartyMatch_JoinToParty";
			this.Menu_lstvPartyMatch_JoinToParty.Size = new System.Drawing.Size(159, 22);
			this.Menu_lstvPartyMatch_JoinToParty.Text = "Join to Party";
			this.Menu_lstvPartyMatch_JoinToParty.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvPartyMatch_PrivateMsg
			// 
			this.Menu_lstvPartyMatch_PrivateMsg.Name = "Menu_lstvPartyMatch_PrivateMsg";
			this.Menu_lstvPartyMatch_PrivateMsg.Size = new System.Drawing.Size(159, 22);
			this.Menu_lstvPartyMatch_PrivateMsg.Text = "Private Message";
			this.Menu_lstvPartyMatch_PrivateMsg.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Party_pnlAutoFormMatch
			// 
			this.Party_pnlAutoFormMatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_cbxMatchRefuse);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_cbxMatchAcceptLeaderList);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_cbxMatchAcceptPartyList);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_cbxMatchAcceptAll);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_cbxMatchAutoReform);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_tbxMatchTo);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_lblMatchTo);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_lblMatchTitle);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_tbxMatchFrom);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_tbxMatchTitle);
			this.Party_pnlAutoFormMatch.Controls.Add(this.Party_lblMatchFrom);
			this.Party_pnlAutoFormMatch.Location = new System.Drawing.Point(6, 6);
			this.Party_pnlAutoFormMatch.Name = "Party_pnlAutoFormMatch";
			this.Party_pnlAutoFormMatch.Size = new System.Drawing.Size(643, 55);
			this.Party_pnlAutoFormMatch.TabIndex = 1;
			// 
			// Party_cbxMatchRefuse
			// 
			this.Party_cbxMatchRefuse.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxMatchRefuse.FlatAppearance.BorderSize = 0;
			this.Party_cbxMatchRefuse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxMatchRefuse.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxMatchRefuse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxMatchRefuse.Location = new System.Drawing.Point(452, 28);
			this.Party_cbxMatchRefuse.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxMatchRefuse.Name = "Party_cbxMatchRefuse";
			this.Party_cbxMatchRefuse.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxMatchRefuse.Size = new System.Drawing.Size(189, 25);
			this.Party_cbxMatchRefuse.TabIndex = 16;
			this.Party_cbxMatchRefuse.Tag = "Source Sans Pro";
			this.Party_cbxMatchRefuse.Text = "Refuse if there is no option";
			this.Party_cbxMatchRefuse.UseVisualStyleBackColor = false;
			this.Party_cbxMatchRefuse.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxMatchAcceptLeaderList
			// 
			this.Party_cbxMatchAcceptLeaderList.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxMatchAcceptLeaderList.FlatAppearance.BorderSize = 0;
			this.Party_cbxMatchAcceptLeaderList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxMatchAcceptLeaderList.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxMatchAcceptLeaderList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxMatchAcceptLeaderList.Location = new System.Drawing.Point(266, 28);
			this.Party_cbxMatchAcceptLeaderList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxMatchAcceptLeaderList.Name = "Party_cbxMatchAcceptLeaderList";
			this.Party_cbxMatchAcceptLeaderList.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxMatchAcceptLeaderList.Size = new System.Drawing.Size(185, 25);
			this.Party_cbxMatchAcceptLeaderList.TabIndex = 14;
			this.Party_cbxMatchAcceptLeaderList.Tag = "Source Sans Pro";
			this.Party_cbxMatchAcceptLeaderList.Text = "Accept all from Leader list";
			this.Party_cbxMatchAcceptLeaderList.UseVisualStyleBackColor = false;
			this.Party_cbxMatchAcceptLeaderList.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxMatchAcceptPartyList
			// 
			this.Party_cbxMatchAcceptPartyList.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxMatchAcceptPartyList.FlatAppearance.BorderSize = 0;
			this.Party_cbxMatchAcceptPartyList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxMatchAcceptPartyList.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxMatchAcceptPartyList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxMatchAcceptPartyList.Location = new System.Drawing.Point(90, 28);
			this.Party_cbxMatchAcceptPartyList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxMatchAcceptPartyList.Name = "Party_cbxMatchAcceptPartyList";
			this.Party_cbxMatchAcceptPartyList.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxMatchAcceptPartyList.Size = new System.Drawing.Size(175, 25);
			this.Party_cbxMatchAcceptPartyList.TabIndex = 8;
			this.Party_cbxMatchAcceptPartyList.Tag = "Source Sans Pro";
			this.Party_cbxMatchAcceptPartyList.Text = "Accept all from Party list";
			this.Party_cbxMatchAcceptPartyList.UseVisualStyleBackColor = false;
			this.Party_cbxMatchAcceptPartyList.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxMatchAcceptAll
			// 
			this.Party_cbxMatchAcceptAll.Checked = true;
			this.Party_cbxMatchAcceptAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Party_cbxMatchAcceptAll.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxMatchAcceptAll.FlatAppearance.BorderSize = 0;
			this.Party_cbxMatchAcceptAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxMatchAcceptAll.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxMatchAcceptAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxMatchAcceptAll.Location = new System.Drawing.Point(1, 28);
			this.Party_cbxMatchAcceptAll.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxMatchAcceptAll.Name = "Party_cbxMatchAcceptAll";
			this.Party_cbxMatchAcceptAll.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxMatchAcceptAll.Size = new System.Drawing.Size(88, 25);
			this.Party_cbxMatchAcceptAll.TabIndex = 7;
			this.Party_cbxMatchAcceptAll.Tag = "Source Sans Pro";
			this.Party_cbxMatchAcceptAll.Text = "Accept all";
			this.Party_cbxMatchAcceptAll.UseVisualStyleBackColor = false;
			this.Party_cbxMatchAcceptAll.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxMatchAutoReform
			// 
			this.Party_cbxMatchAutoReform.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxMatchAutoReform.FlatAppearance.BorderSize = 0;
			this.Party_cbxMatchAutoReform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxMatchAutoReform.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxMatchAutoReform.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxMatchAutoReform.Location = new System.Drawing.Point(535, 3);
			this.Party_cbxMatchAutoReform.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxMatchAutoReform.Name = "Party_cbxMatchAutoReform";
			this.Party_cbxMatchAutoReform.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxMatchAutoReform.Size = new System.Drawing.Size(103, 25);
			this.Party_cbxMatchAutoReform.TabIndex = 6;
			this.Party_cbxMatchAutoReform.Tag = "Source Sans Pro";
			this.Party_cbxMatchAutoReform.Text = "Auto reform";
			this.ToolTips.SetToolTip(this.Party_cbxMatchAutoReform, "Only works if you are the party master");
			this.Party_cbxMatchAutoReform.UseVisualStyleBackColor = false;
			this.Party_cbxMatchAutoReform.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_tbxMatchTo
			// 
			this.Party_tbxMatchTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Party_tbxMatchTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_tbxMatchTo.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_tbxMatchTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_tbxMatchTo.Location = new System.Drawing.Point(500, 3);
			this.Party_tbxMatchTo.MaxLength = 3;
			this.Party_tbxMatchTo.Name = "Party_tbxMatchTo";
			this.Party_tbxMatchTo.Size = new System.Drawing.Size(35, 25);
			this.Party_tbxMatchTo.TabIndex = 5;
			this.Party_tbxMatchTo.Tag = "Source Sans Pro";
			this.Party_tbxMatchTo.Text = "0";
			this.Party_tbxMatchTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Party_tbxMatchTo.TextChanged += new System.EventHandler(this.Control_TextChanged);
			this.Party_tbxMatchTo.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Party_tbxMatchTo.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Party_lblMatchTo
			// 
			this.Party_lblMatchTo.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lblMatchTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lblMatchTo.Location = new System.Drawing.Point(477, 3);
			this.Party_lblMatchTo.Margin = new System.Windows.Forms.Padding(3);
			this.Party_lblMatchTo.Name = "Party_lblMatchTo";
			this.Party_lblMatchTo.Size = new System.Drawing.Size(23, 25);
			this.Party_lblMatchTo.TabIndex = 4;
			this.Party_lblMatchTo.Tag = "Source Sans Pro";
			this.Party_lblMatchTo.Text = "to";
			this.Party_lblMatchTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Party_lblMatchTitle
			// 
			this.Party_lblMatchTitle.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lblMatchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lblMatchTitle.Location = new System.Drawing.Point(3, 3);
			this.Party_lblMatchTitle.Margin = new System.Windows.Forms.Padding(3);
			this.Party_lblMatchTitle.Name = "Party_lblMatchTitle";
			this.Party_lblMatchTitle.Size = new System.Drawing.Size(41, 25);
			this.Party_lblMatchTitle.TabIndex = 0;
			this.Party_lblMatchTitle.Tag = "Source Sans Pro";
			this.Party_lblMatchTitle.Text = "Title";
			this.Party_lblMatchTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Party_tbxMatchFrom
			// 
			this.Party_tbxMatchFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Party_tbxMatchFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_tbxMatchFrom.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_tbxMatchFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_tbxMatchFrom.Location = new System.Drawing.Point(442, 3);
			this.Party_tbxMatchFrom.MaxLength = 3;
			this.Party_tbxMatchFrom.Name = "Party_tbxMatchFrom";
			this.Party_tbxMatchFrom.Size = new System.Drawing.Size(35, 25);
			this.Party_tbxMatchFrom.TabIndex = 3;
			this.Party_tbxMatchFrom.Tag = "Source Sans Pro";
			this.Party_tbxMatchFrom.Text = "0";
			this.Party_tbxMatchFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Party_tbxMatchFrom.TextChanged += new System.EventHandler(this.Control_TextChanged);
			this.Party_tbxMatchFrom.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Party_tbxMatchFrom.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Party_tbxMatchTitle
			// 
			this.Party_tbxMatchTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Party_tbxMatchTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_tbxMatchTitle.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_tbxMatchTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_tbxMatchTitle.Location = new System.Drawing.Point(44, 3);
			this.Party_tbxMatchTitle.Name = "Party_tbxMatchTitle";
			this.Party_tbxMatchTitle.Size = new System.Drawing.Size(317, 25);
			this.Party_tbxMatchTitle.TabIndex = 1;
			this.Party_tbxMatchTitle.Tag = "Source Sans Pro";
			this.Party_tbxMatchTitle.Text = "xBot | Fear cuts deeper than swords...";
			this.Party_tbxMatchTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Party_tbxMatchTitle.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Party_tbxMatchTitle.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Party_lblMatchFrom
			// 
			this.Party_lblMatchFrom.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lblMatchFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lblMatchFrom.Location = new System.Drawing.Point(364, 3);
			this.Party_lblMatchFrom.Margin = new System.Windows.Forms.Padding(3);
			this.Party_lblMatchFrom.Name = "Party_lblMatchFrom";
			this.Party_lblMatchFrom.Size = new System.Drawing.Size(79, 25);
			this.Party_lblMatchFrom.TabIndex = 2;
			this.Party_lblMatchFrom.Tag = "Source Sans Pro";
			this.Party_lblMatchFrom.Text = "From level";
			this.Party_lblMatchFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TabPageH_Party_Option02_Panel
			// 
			this.TabPageH_Party_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Party_Option02_Panel.Controls.Add(this.Party_gbxLeaderList);
			this.TabPageH_Party_Option02_Panel.Controls.Add(this.Party_gbxSetup);
			this.TabPageH_Party_Option02_Panel.Controls.Add(this.Party_gbxAcceptInvite);
			this.TabPageH_Party_Option02_Panel.Controls.Add(this.Party_gbxPlayerList);
			this.TabPageH_Party_Option02_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Party_Option02_Panel.Name = "TabPageH_Party_Option02_Panel";
			this.TabPageH_Party_Option02_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Party_Option02_Panel.TabIndex = 6;
			this.TabPageH_Party_Option02_Panel.Visible = false;
			// 
			// Party_gbxLeaderList
			// 
			this.Party_gbxLeaderList.Controls.Add(this.Party_lstvLeaderList);
			this.Party_gbxLeaderList.Controls.Add(this.Party_btnAddLeader);
			this.Party_gbxLeaderList.Controls.Add(this.Party_tbxLeader);
			this.Party_gbxLeaderList.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_gbxLeaderList.ForeColor = System.Drawing.Color.LightGray;
			this.Party_gbxLeaderList.Location = new System.Drawing.Point(438, 155);
			this.Party_gbxLeaderList.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Party_gbxLeaderList.Name = "Party_gbxLeaderList";
			this.Party_gbxLeaderList.Size = new System.Drawing.Size(211, 152);
			this.Party_gbxLeaderList.TabIndex = 7;
			this.Party_gbxLeaderList.TabStop = false;
			this.Party_gbxLeaderList.Text = "Leader list";
			// 
			// Party_lstvLeaderList
			// 
			this.Party_lstvLeaderList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Party_lstvLeaderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_lstvLeaderList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15});
			this.Party_lstvLeaderList.ContextMenuStrip = this.Menu_lstvLeaderList;
			this.Party_lstvLeaderList.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lstvLeaderList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lstvLeaderList.FullRowSelect = true;
			this.Party_lstvLeaderList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Party_lstvLeaderList.Location = new System.Drawing.Point(2, 51);
			this.Party_lstvLeaderList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_lstvLeaderList.MultiSelect = false;
			this.Party_lstvLeaderList.Name = "Party_lstvLeaderList";
			this.Party_lstvLeaderList.Size = new System.Drawing.Size(207, 99);
			this.Party_lstvLeaderList.TabIndex = 10;
			this.Party_lstvLeaderList.TileSize = new System.Drawing.Size(201, 30);
			this.Party_lstvLeaderList.UseCompatibleStateImageBehavior = false;
			this.Party_lstvLeaderList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Charname";
			this.columnHeader15.Width = 188;
			// 
			// Menu_lstvLeaderList
			// 
			this.Menu_lstvLeaderList.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Menu_lstvLeaderList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvLeaderList_Remove,
            this.Menu_lstvLeaderList_RemoveAll});
			this.Menu_lstvLeaderList.Name = "Menu_lstvOpcodes";
			this.Menu_lstvLeaderList.Size = new System.Drawing.Size(165, 48);
			// 
			// Menu_lstvLeaderList_Remove
			// 
			this.Menu_lstvLeaderList_Remove.Name = "Menu_lstvLeaderList_Remove";
			this.Menu_lstvLeaderList_Remove.Size = new System.Drawing.Size(164, 22);
			this.Menu_lstvLeaderList_Remove.Text = "Remove Selected";
			this.Menu_lstvLeaderList_Remove.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvLeaderList_RemoveAll
			// 
			this.Menu_lstvLeaderList_RemoveAll.Name = "Menu_lstvLeaderList_RemoveAll";
			this.Menu_lstvLeaderList_RemoveAll.Size = new System.Drawing.Size(164, 22);
			this.Menu_lstvLeaderList_RemoveAll.Text = "Remove All";
			this.Menu_lstvLeaderList_RemoveAll.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Party_btnAddLeader
			// 
			this.Party_btnAddLeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_btnAddLeader.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnAddLeader.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Party_btnAddLeader.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnAddLeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_btnAddLeader.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_btnAddLeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_btnAddLeader.Location = new System.Drawing.Point(133, 18);
			this.Party_btnAddLeader.Margin = new System.Windows.Forms.Padding(0);
			this.Party_btnAddLeader.Name = "Party_btnAddLeader";
			this.Party_btnAddLeader.Size = new System.Drawing.Size(70, 26);
			this.Party_btnAddLeader.TabIndex = 9;
			this.Party_btnAddLeader.Tag = "Source Sans Pro";
			this.Party_btnAddLeader.Text = "Add";
			this.Party_btnAddLeader.UseCompatibleTextRendering = true;
			this.Party_btnAddLeader.UseVisualStyleBackColor = false;
			this.Party_btnAddLeader.Click += new System.EventHandler(this.Control_Click);
			// 
			// Party_tbxLeader
			// 
			this.Party_tbxLeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Party_tbxLeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_tbxLeader.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_tbxLeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_tbxLeader.Location = new System.Drawing.Point(10, 18);
			this.Party_tbxLeader.MaxLength = 12;
			this.Party_tbxLeader.Name = "Party_tbxLeader";
			this.Party_tbxLeader.Size = new System.Drawing.Size(117, 26);
			this.Party_tbxLeader.TabIndex = 1;
			this.Party_tbxLeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Party_gbxSetup
			// 
			this.Party_gbxSetup.Controls.Add(this.Party_cbxSetupMasterInvite);
			this.Party_gbxSetup.Controls.Add(this.Party_pnlSetupItem);
			this.Party_gbxSetup.Controls.Add(this.Party_pnlSetupExp);
			this.Party_gbxSetup.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_gbxSetup.ForeColor = System.Drawing.Color.LightGray;
			this.Party_gbxSetup.Location = new System.Drawing.Point(6, 0);
			this.Party_gbxSetup.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Party_gbxSetup.Name = "Party_gbxSetup";
			this.Party_gbxSetup.Size = new System.Drawing.Size(427, 73);
			this.Party_gbxSetup.TabIndex = 6;
			this.Party_gbxSetup.TabStop = false;
			this.Party_gbxSetup.Text = "Setup";
			// 
			// Party_cbxSetupMasterInvite
			// 
			this.Party_cbxSetupMasterInvite.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxSetupMasterInvite.FlatAppearance.BorderSize = 0;
			this.Party_cbxSetupMasterInvite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxSetupMasterInvite.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxSetupMasterInvite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxSetupMasterInvite.Location = new System.Drawing.Point(305, 15);
			this.Party_cbxSetupMasterInvite.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxSetupMasterInvite.Name = "Party_cbxSetupMasterInvite";
			this.Party_cbxSetupMasterInvite.Size = new System.Drawing.Size(112, 51);
			this.Party_cbxSetupMasterInvite.TabIndex = 16;
			this.Party_cbxSetupMasterInvite.Tag = "Source Sans Pro";
			this.Party_cbxSetupMasterInvite.Text = "Only master can invite";
			this.Party_cbxSetupMasterInvite.UseVisualStyleBackColor = false;
			this.Party_cbxSetupMasterInvite.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_pnlSetupItem
			// 
			this.Party_pnlSetupItem.Controls.Add(this.Party_gbxSetupItem);
			this.Party_pnlSetupItem.Location = new System.Drawing.Point(150, 15);
			this.Party_pnlSetupItem.Name = "Party_pnlSetupItem";
			this.Party_pnlSetupItem.Size = new System.Drawing.Size(147, 51);
			this.Party_pnlSetupItem.TabIndex = 14;
			// 
			// Party_gbxSetupItem
			// 
			this.Party_gbxSetupItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_gbxSetupItem.Controls.Add(this.Party_rbnSetupItemShared);
			this.Party_gbxSetupItem.Controls.Add(this.Party_rbnSetupItemFree);
			this.Party_gbxSetupItem.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_gbxSetupItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_gbxSetupItem.Location = new System.Drawing.Point(-2, -10);
			this.Party_gbxSetupItem.Margin = new System.Windows.Forms.Padding(5);
			this.Party_gbxSetupItem.Name = "Party_gbxSetupItem";
			this.Party_gbxSetupItem.Size = new System.Drawing.Size(151, 63);
			this.Party_gbxSetupItem.TabIndex = 1;
			this.Party_gbxSetupItem.TabStop = false;
			// 
			// Party_rbnSetupItemShared
			// 
			this.Party_rbnSetupItemShared.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_rbnSetupItemShared.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_rbnSetupItemShared.Location = new System.Drawing.Point(8, 36);
			this.Party_rbnSetupItemShared.Name = "Party_rbnSetupItemShared";
			this.Party_rbnSetupItemShared.Size = new System.Drawing.Size(135, 25);
			this.Party_rbnSetupItemShared.TabIndex = 1;
			this.Party_rbnSetupItemShared.Text = "Item Shared";
			this.Party_rbnSetupItemShared.UseVisualStyleBackColor = false;
			// 
			// Party_rbnSetupItemFree
			// 
			this.Party_rbnSetupItemFree.Checked = true;
			this.Party_rbnSetupItemFree.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_rbnSetupItemFree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_rbnSetupItemFree.Location = new System.Drawing.Point(8, 10);
			this.Party_rbnSetupItemFree.Name = "Party_rbnSetupItemFree";
			this.Party_rbnSetupItemFree.Size = new System.Drawing.Size(135, 25);
			this.Party_rbnSetupItemFree.TabIndex = 0;
			this.Party_rbnSetupItemFree.TabStop = true;
			this.Party_rbnSetupItemFree.Text = "Item Free-For-All";
			this.Party_rbnSetupItemFree.UseVisualStyleBackColor = false;
			this.Party_rbnSetupItemFree.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_pnlSetupExp
			// 
			this.Party_pnlSetupExp.Controls.Add(this.Party_gbxSetupExp);
			this.Party_pnlSetupExp.Location = new System.Drawing.Point(2, 15);
			this.Party_pnlSetupExp.Name = "Party_pnlSetupExp";
			this.Party_pnlSetupExp.Size = new System.Drawing.Size(147, 51);
			this.Party_pnlSetupExp.TabIndex = 13;
			// 
			// Party_gbxSetupExp
			// 
			this.Party_gbxSetupExp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_gbxSetupExp.Controls.Add(this.Party_rbnSetupExpShared);
			this.Party_gbxSetupExp.Controls.Add(this.Party_rbnSetupExpFree);
			this.Party_gbxSetupExp.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_gbxSetupExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_gbxSetupExp.Location = new System.Drawing.Point(-2, -10);
			this.Party_gbxSetupExp.Margin = new System.Windows.Forms.Padding(5);
			this.Party_gbxSetupExp.Name = "Party_gbxSetupExp";
			this.Party_gbxSetupExp.Size = new System.Drawing.Size(151, 63);
			this.Party_gbxSetupExp.TabIndex = 1;
			this.Party_gbxSetupExp.TabStop = false;
			// 
			// Party_rbnSetupExpShared
			// 
			this.Party_rbnSetupExpShared.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_rbnSetupExpShared.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_rbnSetupExpShared.Location = new System.Drawing.Point(8, 36);
			this.Party_rbnSetupExpShared.Name = "Party_rbnSetupExpShared";
			this.Party_rbnSetupExpShared.Size = new System.Drawing.Size(135, 25);
			this.Party_rbnSetupExpShared.TabIndex = 1;
			this.Party_rbnSetupExpShared.Text = "Exp. Shared";
			this.Party_rbnSetupExpShared.UseVisualStyleBackColor = false;
			// 
			// Party_rbnSetupExpFree
			// 
			this.Party_rbnSetupExpFree.Checked = true;
			this.Party_rbnSetupExpFree.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_rbnSetupExpFree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_rbnSetupExpFree.Location = new System.Drawing.Point(8, 10);
			this.Party_rbnSetupExpFree.Name = "Party_rbnSetupExpFree";
			this.Party_rbnSetupExpFree.Size = new System.Drawing.Size(135, 25);
			this.Party_rbnSetupExpFree.TabIndex = 0;
			this.Party_rbnSetupExpFree.TabStop = true;
			this.Party_rbnSetupExpFree.Text = "Exp. Free-For-All";
			this.Party_rbnSetupExpFree.UseVisualStyleBackColor = false;
			this.Party_rbnSetupExpFree.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_gbxAcceptInvite
			// 
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxActivateLeaderCommands);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxLeavePartyNoneLeader);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxAcceptOnlyPartySetup);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxInviteOnlyPartySetup);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxRefuseInvitations);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxInviteAll);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxInvitePartyList);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxAcceptLeaderList);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxAcceptAll);
			this.Party_gbxAcceptInvite.Controls.Add(this.Party_cbxAcceptPartyList);
			this.Party_gbxAcceptInvite.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_gbxAcceptInvite.ForeColor = System.Drawing.Color.LightGray;
			this.Party_gbxAcceptInvite.Location = new System.Drawing.Point(6, 73);
			this.Party_gbxAcceptInvite.Margin = new System.Windows.Forms.Padding(0);
			this.Party_gbxAcceptInvite.Name = "Party_gbxAcceptInvite";
			this.Party_gbxAcceptInvite.Size = new System.Drawing.Size(427, 234);
			this.Party_gbxAcceptInvite.TabIndex = 5;
			this.Party_gbxAcceptInvite.TabStop = false;
			this.Party_gbxAcceptInvite.Tag = "Source Sans Pro";
			this.Party_gbxAcceptInvite.Text = "Accept / Invite";
			// 
			// Party_cbxActivateLeaderCommands
			// 
			this.Party_cbxActivateLeaderCommands.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxActivateLeaderCommands.FlatAppearance.BorderSize = 0;
			this.Party_cbxActivateLeaderCommands.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxActivateLeaderCommands.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxActivateLeaderCommands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxActivateLeaderCommands.Location = new System.Drawing.Point(2, 199);
			this.Party_cbxActivateLeaderCommands.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxActivateLeaderCommands.Name = "Party_cbxActivateLeaderCommands";
			this.Party_cbxActivateLeaderCommands.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxActivateLeaderCommands.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxActivateLeaderCommands.TabIndex = 19;
			this.Party_cbxActivateLeaderCommands.Tag = "Source Sans Pro";
			this.Party_cbxActivateLeaderCommands.Text = "Activate Leader commands";
			this.Party_cbxActivateLeaderCommands.UseVisualStyleBackColor = false;
			this.Party_cbxActivateLeaderCommands.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxLeavePartyNoneLeader
			// 
			this.Party_cbxLeavePartyNoneLeader.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxLeavePartyNoneLeader.FlatAppearance.BorderSize = 0;
			this.Party_cbxLeavePartyNoneLeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxLeavePartyNoneLeader.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxLeavePartyNoneLeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxLeavePartyNoneLeader.Location = new System.Drawing.Point(2, 132);
			this.Party_cbxLeavePartyNoneLeader.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxLeavePartyNoneLeader.Name = "Party_cbxLeavePartyNoneLeader";
			this.Party_cbxLeavePartyNoneLeader.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxLeavePartyNoneLeader.Size = new System.Drawing.Size(211, 40);
			this.Party_cbxLeavePartyNoneLeader.TabIndex = 18;
			this.Party_cbxLeavePartyNoneLeader.Tag = "Source Sans Pro";
			this.Party_cbxLeavePartyNoneLeader.Text = "Leave party if there is none Leader found";
			this.Party_cbxLeavePartyNoneLeader.UseVisualStyleBackColor = false;
			this.Party_cbxLeavePartyNoneLeader.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxAcceptOnlyPartySetup
			// 
			this.Party_cbxAcceptOnlyPartySetup.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxAcceptOnlyPartySetup.FlatAppearance.BorderSize = 0;
			this.Party_cbxAcceptOnlyPartySetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxAcceptOnlyPartySetup.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxAcceptOnlyPartySetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxAcceptOnlyPartySetup.Location = new System.Drawing.Point(2, 15);
			this.Party_cbxAcceptOnlyPartySetup.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxAcceptOnlyPartySetup.Name = "Party_cbxAcceptOnlyPartySetup";
			this.Party_cbxAcceptOnlyPartySetup.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxAcceptOnlyPartySetup.Size = new System.Drawing.Size(211, 40);
			this.Party_cbxAcceptOnlyPartySetup.TabIndex = 17;
			this.Party_cbxAcceptOnlyPartySetup.Tag = "Source Sans Pro";
			this.Party_cbxAcceptOnlyPartySetup.Text = "Accept only using the current party setup";
			this.Party_cbxAcceptOnlyPartySetup.UseVisualStyleBackColor = false;
			this.Party_cbxAcceptOnlyPartySetup.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxInviteOnlyPartySetup
			// 
			this.Party_cbxInviteOnlyPartySetup.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxInviteOnlyPartySetup.FlatAppearance.BorderSize = 0;
			this.Party_cbxInviteOnlyPartySetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxInviteOnlyPartySetup.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxInviteOnlyPartySetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxInviteOnlyPartySetup.Location = new System.Drawing.Point(214, 15);
			this.Party_cbxInviteOnlyPartySetup.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxInviteOnlyPartySetup.Name = "Party_cbxInviteOnlyPartySetup";
			this.Party_cbxInviteOnlyPartySetup.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxInviteOnlyPartySetup.Size = new System.Drawing.Size(211, 40);
			this.Party_cbxInviteOnlyPartySetup.TabIndex = 17;
			this.Party_cbxInviteOnlyPartySetup.Tag = "Source Sans Pro";
			this.Party_cbxInviteOnlyPartySetup.Text = "Invite only using the current party setup";
			this.Party_cbxInviteOnlyPartySetup.UseVisualStyleBackColor = false;
			this.Party_cbxInviteOnlyPartySetup.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxRefuseInvitations
			// 
			this.Party_cbxRefuseInvitations.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxRefuseInvitations.FlatAppearance.BorderSize = 0;
			this.Party_cbxRefuseInvitations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxRefuseInvitations.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxRefuseInvitations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxRefuseInvitations.Location = new System.Drawing.Point(2, 173);
			this.Party_cbxRefuseInvitations.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxRefuseInvitations.Name = "Party_cbxRefuseInvitations";
			this.Party_cbxRefuseInvitations.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxRefuseInvitations.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxRefuseInvitations.TabIndex = 15;
			this.Party_cbxRefuseInvitations.Tag = "Source Sans Pro";
			this.Party_cbxRefuseInvitations.Text = "Refuse if there is no option";
			this.Party_cbxRefuseInvitations.UseVisualStyleBackColor = false;
			this.Party_cbxRefuseInvitations.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxInviteAll
			// 
			this.Party_cbxInviteAll.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxInviteAll.FlatAppearance.BorderSize = 0;
			this.Party_cbxInviteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxInviteAll.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxInviteAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxInviteAll.Location = new System.Drawing.Point(214, 56);
			this.Party_cbxInviteAll.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxInviteAll.Name = "Party_cbxInviteAll";
			this.Party_cbxInviteAll.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxInviteAll.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxInviteAll.TabIndex = 5;
			this.Party_cbxInviteAll.Tag = "Source Sans Pro";
			this.Party_cbxInviteAll.Text = "Invite all";
			this.Party_cbxInviteAll.UseVisualStyleBackColor = false;
			this.Party_cbxInviteAll.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxInvitePartyList
			// 
			this.Party_cbxInvitePartyList.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxInvitePartyList.FlatAppearance.BorderSize = 0;
			this.Party_cbxInvitePartyList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxInvitePartyList.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxInvitePartyList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxInvitePartyList.Location = new System.Drawing.Point(214, 80);
			this.Party_cbxInvitePartyList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxInvitePartyList.Name = "Party_cbxInvitePartyList";
			this.Party_cbxInvitePartyList.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxInvitePartyList.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxInvitePartyList.TabIndex = 3;
			this.Party_cbxInvitePartyList.Tag = "Source Sans Pro";
			this.Party_cbxInvitePartyList.Text = "Invite all from Party list";
			this.Party_cbxInvitePartyList.UseVisualStyleBackColor = false;
			this.Party_cbxInvitePartyList.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxAcceptLeaderList
			// 
			this.Party_cbxAcceptLeaderList.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxAcceptLeaderList.FlatAppearance.BorderSize = 0;
			this.Party_cbxAcceptLeaderList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxAcceptLeaderList.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxAcceptLeaderList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxAcceptLeaderList.Location = new System.Drawing.Point(2, 106);
			this.Party_cbxAcceptLeaderList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxAcceptLeaderList.Name = "Party_cbxAcceptLeaderList";
			this.Party_cbxAcceptLeaderList.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxAcceptLeaderList.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxAcceptLeaderList.TabIndex = 13;
			this.Party_cbxAcceptLeaderList.Tag = "Source Sans Pro";
			this.Party_cbxAcceptLeaderList.Text = "Accept all from Leader list";
			this.Party_cbxAcceptLeaderList.UseVisualStyleBackColor = false;
			this.Party_cbxAcceptLeaderList.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxAcceptAll
			// 
			this.Party_cbxAcceptAll.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxAcceptAll.FlatAppearance.BorderSize = 0;
			this.Party_cbxAcceptAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxAcceptAll.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxAcceptAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxAcceptAll.Location = new System.Drawing.Point(2, 56);
			this.Party_cbxAcceptAll.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxAcceptAll.Name = "Party_cbxAcceptAll";
			this.Party_cbxAcceptAll.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxAcceptAll.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxAcceptAll.TabIndex = 5;
			this.Party_cbxAcceptAll.Tag = "Source Sans Pro";
			this.Party_cbxAcceptAll.Text = "Accept all";
			this.Party_cbxAcceptAll.UseVisualStyleBackColor = false;
			this.Party_cbxAcceptAll.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_cbxAcceptPartyList
			// 
			this.Party_cbxAcceptPartyList.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxAcceptPartyList.FlatAppearance.BorderSize = 0;
			this.Party_cbxAcceptPartyList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxAcceptPartyList.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxAcceptPartyList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxAcceptPartyList.Location = new System.Drawing.Point(2, 80);
			this.Party_cbxAcceptPartyList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxAcceptPartyList.Name = "Party_cbxAcceptPartyList";
			this.Party_cbxAcceptPartyList.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Party_cbxAcceptPartyList.Size = new System.Drawing.Size(211, 25);
			this.Party_cbxAcceptPartyList.TabIndex = 3;
			this.Party_cbxAcceptPartyList.Tag = "Source Sans Pro";
			this.Party_cbxAcceptPartyList.Text = "Accept all from Party list";
			this.Party_cbxAcceptPartyList.UseVisualStyleBackColor = false;
			this.Party_cbxAcceptPartyList.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Party_gbxPlayerList
			// 
			this.Party_gbxPlayerList.Controls.Add(this.Party_lstvPartyList);
			this.Party_gbxPlayerList.Controls.Add(this.Party_btnAddPlayer);
			this.Party_gbxPlayerList.Controls.Add(this.Party_tbxPlayer);
			this.Party_gbxPlayerList.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_gbxPlayerList.ForeColor = System.Drawing.Color.LightGray;
			this.Party_gbxPlayerList.Location = new System.Drawing.Point(438, 0);
			this.Party_gbxPlayerList.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Party_gbxPlayerList.Name = "Party_gbxPlayerList";
			this.Party_gbxPlayerList.Size = new System.Drawing.Size(211, 152);
			this.Party_gbxPlayerList.TabIndex = 1;
			this.Party_gbxPlayerList.TabStop = false;
			this.Party_gbxPlayerList.Text = "Party list";
			// 
			// Party_lstvPartyList
			// 
			this.Party_lstvPartyList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Party_lstvPartyList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_lstvPartyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.Party_lstvPartyList.ContextMenuStrip = this.Menu_lstvPartyList;
			this.Party_lstvPartyList.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lstvPartyList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lstvPartyList.FullRowSelect = true;
			this.Party_lstvPartyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Party_lstvPartyList.Location = new System.Drawing.Point(2, 51);
			this.Party_lstvPartyList.Margin = new System.Windows.Forms.Padding(0);
			this.Party_lstvPartyList.MultiSelect = false;
			this.Party_lstvPartyList.Name = "Party_lstvPartyList";
			this.Party_lstvPartyList.Size = new System.Drawing.Size(207, 99);
			this.Party_lstvPartyList.TabIndex = 10;
			this.Party_lstvPartyList.TileSize = new System.Drawing.Size(201, 30);
			this.Party_lstvPartyList.UseCompatibleStateImageBehavior = false;
			this.Party_lstvPartyList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Charname";
			this.columnHeader1.Width = 188;
			// 
			// Menu_lstvPartyList
			// 
			this.Menu_lstvPartyList.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Menu_lstvPartyList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvPartyList_Remove,
            this.Menu_lstvPartyList_RemoveAll});
			this.Menu_lstvPartyList.Name = "Menu_lstvOpcodes";
			this.Menu_lstvPartyList.Size = new System.Drawing.Size(165, 48);
			// 
			// Menu_lstvPartyList_Remove
			// 
			this.Menu_lstvPartyList_Remove.Name = "Menu_lstvPartyList_Remove";
			this.Menu_lstvPartyList_Remove.Size = new System.Drawing.Size(164, 22);
			this.Menu_lstvPartyList_Remove.Text = "Remove Selected";
			this.Menu_lstvPartyList_Remove.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvPartyList_RemoveAll
			// 
			this.Menu_lstvPartyList_RemoveAll.Name = "Menu_lstvPartyList_RemoveAll";
			this.Menu_lstvPartyList_RemoveAll.Size = new System.Drawing.Size(164, 22);
			this.Menu_lstvPartyList_RemoveAll.Text = "Remove All";
			this.Menu_lstvPartyList_RemoveAll.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Party_btnAddPlayer
			// 
			this.Party_btnAddPlayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Party_btnAddPlayer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnAddPlayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Party_btnAddPlayer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Party_btnAddPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_btnAddPlayer.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_btnAddPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_btnAddPlayer.Location = new System.Drawing.Point(133, 18);
			this.Party_btnAddPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.Party_btnAddPlayer.Name = "Party_btnAddPlayer";
			this.Party_btnAddPlayer.Size = new System.Drawing.Size(70, 26);
			this.Party_btnAddPlayer.TabIndex = 9;
			this.Party_btnAddPlayer.Tag = "Source Sans Pro";
			this.Party_btnAddPlayer.Text = "Add";
			this.Party_btnAddPlayer.UseCompatibleTextRendering = true;
			this.Party_btnAddPlayer.UseVisualStyleBackColor = true;
			this.Party_btnAddPlayer.Click += new System.EventHandler(this.Control_Click);
			// 
			// Party_tbxPlayer
			// 
			this.Party_tbxPlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Party_tbxPlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_tbxPlayer.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_tbxPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_tbxPlayer.Location = new System.Drawing.Point(10, 18);
			this.Party_tbxPlayer.MaxLength = 12;
			this.Party_tbxPlayer.Name = "Party_tbxPlayer";
			this.Party_tbxPlayer.Size = new System.Drawing.Size(117, 26);
			this.Party_tbxPlayer.TabIndex = 1;
			this.Party_tbxPlayer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TabPageH_Party_Option01_Panel
			// 
			this.TabPageH_Party_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Party_Option01_Panel.Controls.Add(this.Party_lblCurrentSetup);
			this.TabPageH_Party_Option01_Panel.Controls.Add(this.Party_cbxShowFGWInvites);
			this.TabPageH_Party_Option01_Panel.Controls.Add(this.Party_lstvPartyMembers);
			this.TabPageH_Party_Option01_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Party_Option01_Panel.Name = "TabPageH_Party_Option01_Panel";
			this.TabPageH_Party_Option01_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Party_Option01_Panel.TabIndex = 5;
			this.TabPageH_Party_Option01_Panel.Visible = false;
			// 
			// Party_lblCurrentSetup
			// 
			this.Party_lblCurrentSetup.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Party_lblCurrentSetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.Party_lblCurrentSetup.Location = new System.Drawing.Point(6, 281);
			this.Party_lblCurrentSetup.Name = "Party_lblCurrentSetup";
			this.Party_lblCurrentSetup.Size = new System.Drawing.Size(400, 26);
			this.Party_lblCurrentSetup.TabIndex = 15;
			this.Party_lblCurrentSetup.Tag = "Source Sans Pro";
			this.Party_lblCurrentSetup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Party_cbxShowFGWInvites
			// 
			this.Party_cbxShowFGWInvites.Cursor = System.Windows.Forms.Cursors.Default;
			this.Party_cbxShowFGWInvites.FlatAppearance.BorderSize = 0;
			this.Party_cbxShowFGWInvites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Party_cbxShowFGWInvites.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Pixel);
			this.Party_cbxShowFGWInvites.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_cbxShowFGWInvites.Location = new System.Drawing.Point(406, 281);
			this.Party_cbxShowFGWInvites.Margin = new System.Windows.Forms.Padding(0);
			this.Party_cbxShowFGWInvites.Name = "Party_cbxShowFGWInvites";
			this.Party_cbxShowFGWInvites.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Party_cbxShowFGWInvites.Size = new System.Drawing.Size(241, 26);
			this.Party_cbxShowFGWInvites.TabIndex = 4;
			this.Party_cbxShowFGWInvites.Tag = "Source Sans Pro";
			this.Party_cbxShowFGWInvites.Text = "Show forgotten world invitations";
			this.Party_cbxShowFGWInvites.UseVisualStyleBackColor = false;
			// 
			// Party_lstvPartyMembers
			// 
			this.Party_lstvPartyMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Party_lstvPartyMembers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Party_lstvPartyMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader16});
			this.Party_lstvPartyMembers.ContextMenuStrip = this.Menu_lstvPartyMembers;
			this.Party_lstvPartyMembers.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Party_lstvPartyMembers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Party_lstvPartyMembers.FullRowSelect = true;
			this.Party_lstvPartyMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.Party_lstvPartyMembers.HideSelection = false;
			this.Party_lstvPartyMembers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Party_lstvPartyMembers.Location = new System.Drawing.Point(6, 7);
			this.Party_lstvPartyMembers.Margin = new System.Windows.Forms.Padding(0);
			this.Party_lstvPartyMembers.MultiSelect = false;
			this.Party_lstvPartyMembers.Name = "Party_lstvPartyMembers";
			this.Party_lstvPartyMembers.ShowGroups = false;
			this.Party_lstvPartyMembers.Size = new System.Drawing.Size(643, 270);
			this.Party_lstvPartyMembers.TabIndex = 1;
			this.Party_lstvPartyMembers.Tag = "Source Sans Pro";
			this.Party_lstvPartyMembers.TileSize = new System.Drawing.Size(201, 50);
			this.Party_lstvPartyMembers.UseCompatibleStateImageBehavior = false;
			this.Party_lstvPartyMembers.View = System.Windows.Forms.View.Details;
			this.Party_lstvPartyMembers.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging_Cancel);
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Name";
			this.columnHeader11.Width = 150;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Guild";
			this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader12.Width = 150;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Level";
			this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader13.Width = 50;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Health / Mana";
			this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader14.Width = 110;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Location";
			this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader16.Width = 178;
			// 
			// Menu_lstvPartyMembers
			// 
			this.Menu_lstvPartyMembers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvPartyMembers_AddTo,
            this.Menu_lstvPartyMembers_KickPlayer,
            this.Menu_lstvPartyMembers_Separator01,
            this.Menu_lstvPartyMembers_LeaveParty});
			this.Menu_lstvPartyMembers.Name = "Menu_lstvMembers";
			this.Menu_lstvPartyMembers.Size = new System.Drawing.Size(135, 76);
			// 
			// Menu_lstvPartyMembers_AddTo
			// 
			this.Menu_lstvPartyMembers_AddTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_lstvPartyMembers_AddToPartyList,
            this.Menu_lstvPartyMembers_AddToLeaderList});
			this.Menu_lstvPartyMembers_AddTo.Name = "Menu_lstvPartyMembers_AddTo";
			this.Menu_lstvPartyMembers_AddTo.Size = new System.Drawing.Size(134, 22);
			this.Menu_lstvPartyMembers_AddTo.Text = "Add to";
			// 
			// Menu_lstvPartyMembers_AddToPartyList
			// 
			this.Menu_lstvPartyMembers_AddToPartyList.Name = "Menu_lstvPartyMembers_AddToPartyList";
			this.Menu_lstvPartyMembers_AddToPartyList.Size = new System.Drawing.Size(127, 22);
			this.Menu_lstvPartyMembers_AddToPartyList.Text = "Party list";
			this.Menu_lstvPartyMembers_AddToPartyList.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvPartyMembers_AddToLeaderList
			// 
			this.Menu_lstvPartyMembers_AddToLeaderList.Name = "Menu_lstvPartyMembers_AddToLeaderList";
			this.Menu_lstvPartyMembers_AddToLeaderList.Size = new System.Drawing.Size(127, 22);
			this.Menu_lstvPartyMembers_AddToLeaderList.Text = "Leader list";
			this.Menu_lstvPartyMembers_AddToLeaderList.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvPartyMembers_KickPlayer
			// 
			this.Menu_lstvPartyMembers_KickPlayer.Name = "Menu_lstvPartyMembers_KickPlayer";
			this.Menu_lstvPartyMembers_KickPlayer.Size = new System.Drawing.Size(134, 22);
			this.Menu_lstvPartyMembers_KickPlayer.Text = "Kick Player";
			this.Menu_lstvPartyMembers_KickPlayer.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_lstvPartyMembers_Separator01
			// 
			this.Menu_lstvPartyMembers_Separator01.Name = "Menu_lstvPartyMembers_Separator01";
			this.Menu_lstvPartyMembers_Separator01.Size = new System.Drawing.Size(131, 6);
			// 
			// Menu_lstvPartyMembers_LeaveParty
			// 
			this.Menu_lstvPartyMembers_LeaveParty.Name = "Menu_lstvPartyMembers_LeaveParty";
			this.Menu_lstvPartyMembers_LeaveParty.Size = new System.Drawing.Size(134, 22);
			this.Menu_lstvPartyMembers_LeaveParty.Text = "Leave Party";
			this.Menu_lstvPartyMembers_LeaveParty.Click += new System.EventHandler(this.Menu_Click);
			// 
			// TabPageH_Party_Option04_Panel
			// 
			this.TabPageH_Party_Option04_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Party_Option04_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Party_Option04_Panel.Name = "TabPageH_Party_Option04_Panel";
			this.TabPageH_Party_Option04_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Party_Option04_Panel.TabIndex = 8;
			this.TabPageH_Party_Option04_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option05_Panel
			// 
			this.TabPageV_Control01_Option05_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option05_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option05_Panel.Name = "TabPageV_Control01_Option05_Panel";
			this.TabPageV_Control01_Option05_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option05_Panel.TabIndex = 9;
			this.TabPageV_Control01_Option05_Panel.Visible = false;
			// 
			// pnlWindow
			// 
			this.pnlWindow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option08_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option01_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option03_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option04_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option07_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option02_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option13_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option14_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option11_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option12_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option10_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option09_Panel);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option05_Panel);
			this.pnlWindow.Controls.Add(this.btnClientOptions);
			this.pnlWindow.Controls.Add(this.btnBotStart);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01_Option06_Panel);
			this.pnlWindow.Controls.Add(this.pnlHeader);
			this.pnlWindow.Controls.Add(this.TabPageV_Control01);
			this.pnlWindow.Controls.Add(this.rtbxLogs);
			this.pnlWindow.Controls.Add(this.btnAnalyzer);
			this.pnlWindow.Location = new System.Drawing.Point(1, 1);
			this.pnlWindow.Margin = new System.Windows.Forms.Padding(0);
			this.pnlWindow.Name = "pnlWindow";
			this.pnlWindow.Size = new System.Drawing.Size(798, 471);
			this.pnlWindow.TabIndex = 1;
			// 
			// TabPageV_Control01_Option08_Panel
			// 
			this.TabPageV_Control01_Option08_Panel.Controls.Add(this.TabPageH_Training);
			this.TabPageV_Control01_Option08_Panel.Controls.Add(this.TabPageH_Training_Option01_Panel);
			this.TabPageV_Control01_Option08_Panel.Controls.Add(this.TabPageH_Training_Option03_Panel);
			this.TabPageV_Control01_Option08_Panel.Controls.Add(this.TabPageH_Training_Option02_Panel);
			this.TabPageV_Control01_Option08_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option08_Panel.Name = "TabPageV_Control01_Option08_Panel";
			this.TabPageV_Control01_Option08_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option08_Panel.TabIndex = 15;
			this.TabPageV_Control01_Option08_Panel.Visible = false;
			// 
			// TabPageH_Training
			// 
			this.TabPageH_Training.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Training.Controls.Add(this.TabPageH_Training_Option03);
			this.TabPageH_Training.Controls.Add(this.TabPageH_Training_Option02);
			this.TabPageH_Training.Controls.Add(this.TabPageH_Training_Option01);
			this.TabPageH_Training.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Training.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Training.Name = "TabPageH_Training";
			this.TabPageH_Training.Size = new System.Drawing.Size(657, 28);
			this.TabPageH_Training.TabIndex = 24;
			// 
			// TabPageH_Training_Option03
			// 
			this.TabPageH_Training_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Training_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Training_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Training_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Training_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Training_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Training_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Training_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Training_Option03.Location = new System.Drawing.Point(438, 0);
			this.TabPageH_Training_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Training_Option03.Name = "TabPageH_Training_Option03";
			this.TabPageH_Training_Option03.Size = new System.Drawing.Size(219, 26);
			this.TabPageH_Training_Option03.TabIndex = 14;
			this.TabPageH_Training_Option03.Tag = "Source Sans Pro";
			this.TabPageH_Training_Option03.Text = "Trace";
			this.TabPageH_Training_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Training_Option03.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Training_Option02
			// 
			this.TabPageH_Training_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Training_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Training_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Training_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Training_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Training_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Training_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Training_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Training_Option02.Location = new System.Drawing.Point(219, 0);
			this.TabPageH_Training_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Training_Option02.Name = "TabPageH_Training_Option02";
			this.TabPageH_Training_Option02.Size = new System.Drawing.Size(219, 26);
			this.TabPageH_Training_Option02.TabIndex = 13;
			this.TabPageH_Training_Option02.Tag = "Source Sans Pro";
			this.TabPageH_Training_Option02.Text = "Script";
			this.TabPageH_Training_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Training_Option02.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Training_Option01
			// 
			this.TabPageH_Training_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Training_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Training_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Training_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Training_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Training_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Training_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Training_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Training_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Training_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Training_Option01.Name = "TabPageH_Training_Option01";
			this.TabPageH_Training_Option01.Size = new System.Drawing.Size(219, 26);
			this.TabPageH_Training_Option01.TabIndex = 12;
			this.TabPageH_Training_Option01.Tag = "Source Sans Pro";
			this.TabPageH_Training_Option01.Text = "Area";
			this.TabPageH_Training_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Training_Option01.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Training_Option01_Panel
			// 
			this.TabPageH_Training_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_btnLoadScriptPath);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_lblZ);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_tbxZ);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_lblY);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_tbxY);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_lblX);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_tbxX);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_lblRegion);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_tbxRegion);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_lblScriptPath);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_tbxScriptPath);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_btnGetCoordinates);
			this.TabPageH_Training_Option01_Panel.Controls.Add(this.Training_lstvAreas);
			this.TabPageH_Training_Option01_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Training_Option01_Panel.Name = "TabPageH_Training_Option01_Panel";
			this.TabPageH_Training_Option01_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Training_Option01_Panel.TabIndex = 25;
			this.TabPageH_Training_Option01_Panel.Visible = false;
			// 
			// Training_btnLoadScriptPath
			// 
			this.Training_btnLoadScriptPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Training_btnLoadScriptPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.Training_btnLoadScriptPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Training_btnLoadScriptPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Training_btnLoadScriptPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Training_btnLoadScriptPath.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_btnLoadScriptPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_btnLoadScriptPath.Location = new System.Drawing.Point(624, 38);
			this.Training_btnLoadScriptPath.Margin = new System.Windows.Forms.Padding(0);
			this.Training_btnLoadScriptPath.Name = "Training_btnLoadScriptPath";
			this.Training_btnLoadScriptPath.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.Training_btnLoadScriptPath.Size = new System.Drawing.Size(26, 26);
			this.Training_btnLoadScriptPath.TabIndex = 40;
			this.Training_btnLoadScriptPath.Tag = "Font Awesome 5 Pro Solid";
			this.Training_btnLoadScriptPath.Text = "";
			this.ToolTips.SetToolTip(this.Training_btnLoadScriptPath, "Select Script Path");
			this.Training_btnLoadScriptPath.UseCompatibleTextRendering = true;
			this.Training_btnLoadScriptPath.UseVisualStyleBackColor = false;
			this.Training_btnLoadScriptPath.Click += new System.EventHandler(this.Control_Click);
			// 
			// Training_lblZ
			// 
			this.Training_lblZ.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_lblZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lblZ.Location = new System.Drawing.Point(571, 6);
			this.Training_lblZ.Margin = new System.Windows.Forms.Padding(3);
			this.Training_lblZ.Name = "Training_lblZ";
			this.Training_lblZ.Size = new System.Drawing.Size(25, 25);
			this.Training_lblZ.TabIndex = 39;
			this.Training_lblZ.Tag = "Source Sans Pro";
			this.Training_lblZ.Text = "Z";
			this.Training_lblZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Training_tbxZ
			// 
			this.Training_tbxZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_tbxZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_tbxZ.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_tbxZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_tbxZ.Location = new System.Drawing.Point(596, 6);
			this.Training_tbxZ.Name = "Training_tbxZ";
			this.Training_tbxZ.ReadOnly = true;
			this.Training_tbxZ.Size = new System.Drawing.Size(55, 25);
			this.Training_tbxZ.TabIndex = 38;
			this.Training_tbxZ.Tag = "Source Sans Pro";
			this.Training_tbxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Training_tbxZ.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Training_tbxZ.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Training_lblY
			// 
			this.Training_lblY.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_lblY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lblY.Location = new System.Drawing.Point(487, 6);
			this.Training_lblY.Margin = new System.Windows.Forms.Padding(3);
			this.Training_lblY.Name = "Training_lblY";
			this.Training_lblY.Size = new System.Drawing.Size(25, 25);
			this.Training_lblY.TabIndex = 37;
			this.Training_lblY.Tag = "Source Sans Pro";
			this.Training_lblY.Text = "Y";
			this.Training_lblY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Training_tbxY
			// 
			this.Training_tbxY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_tbxY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_tbxY.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_tbxY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_tbxY.Location = new System.Drawing.Point(512, 6);
			this.Training_tbxY.Name = "Training_tbxY";
			this.Training_tbxY.ReadOnly = true;
			this.Training_tbxY.Size = new System.Drawing.Size(55, 25);
			this.Training_tbxY.TabIndex = 36;
			this.Training_tbxY.Tag = "Source Sans Pro";
			this.Training_tbxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Training_tbxY.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Training_tbxY.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Training_lblX
			// 
			this.Training_lblX.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_lblX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lblX.Location = new System.Drawing.Point(403, 6);
			this.Training_lblX.Margin = new System.Windows.Forms.Padding(3);
			this.Training_lblX.Name = "Training_lblX";
			this.Training_lblX.Size = new System.Drawing.Size(25, 25);
			this.Training_lblX.TabIndex = 35;
			this.Training_lblX.Tag = "Source Sans Pro";
			this.Training_lblX.Text = "X";
			this.Training_lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Training_tbxX
			// 
			this.Training_tbxX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_tbxX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_tbxX.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_tbxX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_tbxX.Location = new System.Drawing.Point(428, 6);
			this.Training_tbxX.Name = "Training_tbxX";
			this.Training_tbxX.ReadOnly = true;
			this.Training_tbxX.Size = new System.Drawing.Size(55, 25);
			this.Training_tbxX.TabIndex = 34;
			this.Training_tbxX.Tag = "Source Sans Pro";
			this.Training_tbxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Training_tbxX.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Training_tbxX.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Training_lblRegion
			// 
			this.Training_lblRegion.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_lblRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lblRegion.Location = new System.Drawing.Point(295, 6);
			this.Training_lblRegion.Margin = new System.Windows.Forms.Padding(3);
			this.Training_lblRegion.Name = "Training_lblRegion";
			this.Training_lblRegion.Size = new System.Drawing.Size(56, 25);
			this.Training_lblRegion.TabIndex = 33;
			this.Training_lblRegion.Tag = "Source Sans Pro";
			this.Training_lblRegion.Text = "Region";
			this.Training_lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Training_tbxRegion
			// 
			this.Training_tbxRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_tbxRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_tbxRegion.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_tbxRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_tbxRegion.Location = new System.Drawing.Point(351, 6);
			this.Training_tbxRegion.Name = "Training_tbxRegion";
			this.Training_tbxRegion.ReadOnly = true;
			this.Training_tbxRegion.Size = new System.Drawing.Size(48, 25);
			this.Training_tbxRegion.TabIndex = 32;
			this.Training_tbxRegion.Tag = "Source Sans Pro";
			this.Training_tbxRegion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Training_tbxRegion.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Training_tbxRegion.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Training_lblScriptPath
			// 
			this.Training_lblScriptPath.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_lblScriptPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lblScriptPath.Location = new System.Drawing.Point(159, 38);
			this.Training_lblScriptPath.Margin = new System.Windows.Forms.Padding(3);
			this.Training_lblScriptPath.Name = "Training_lblScriptPath";
			this.Training_lblScriptPath.Size = new System.Drawing.Size(82, 26);
			this.Training_lblScriptPath.TabIndex = 31;
			this.Training_lblScriptPath.Tag = "Source Sans Pro";
			this.Training_lblScriptPath.Text = "Script Path";
			this.Training_lblScriptPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Training_tbxScriptPath
			// 
			this.Training_tbxScriptPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_tbxScriptPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_tbxScriptPath.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.Training_tbxScriptPath.Font = new System.Drawing.Font("Source Sans Pro", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_tbxScriptPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_tbxScriptPath.Location = new System.Drawing.Point(241, 38);
			this.Training_tbxScriptPath.Name = "Training_tbxScriptPath";
			this.Training_tbxScriptPath.ReadOnly = true;
			this.Training_tbxScriptPath.Size = new System.Drawing.Size(384, 26);
			this.Training_tbxScriptPath.TabIndex = 30;
			this.Training_tbxScriptPath.Tag = "Source Sans Pro";
			this.Training_tbxScriptPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Training_btnGetCoordinates
			// 
			this.Training_btnGetCoordinates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Training_btnGetCoordinates.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Training_btnGetCoordinates.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Training_btnGetCoordinates.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Training_btnGetCoordinates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Training_btnGetCoordinates.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_btnGetCoordinates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_btnGetCoordinates.Location = new System.Drawing.Point(159, 6);
			this.Training_btnGetCoordinates.Margin = new System.Windows.Forms.Padding(0);
			this.Training_btnGetCoordinates.Name = "Training_btnGetCoordinates";
			this.Training_btnGetCoordinates.Size = new System.Drawing.Size(132, 26);
			this.Training_btnGetCoordinates.TabIndex = 28;
			this.Training_btnGetCoordinates.Tag = "Source Sans Pro";
			this.Training_btnGetCoordinates.Text = "Get coordinates";
			this.Training_btnGetCoordinates.UseCompatibleTextRendering = true;
			this.Training_btnGetCoordinates.UseVisualStyleBackColor = true;
			this.Training_btnGetCoordinates.Click += new System.EventHandler(this.Control_Click);
			// 
			// Training_lstvAreas
			// 
			this.Training_lstvAreas.AutoArrange = false;
			this.Training_lstvAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Training_lstvAreas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_lstvAreas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader28});
			this.Training_lstvAreas.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_lstvAreas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lstvAreas.FullRowSelect = true;
			this.Training_lstvAreas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.Training_lstvAreas.HideSelection = false;
			this.Training_lstvAreas.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
			this.Training_lstvAreas.LabelEdit = true;
			this.Training_lstvAreas.Location = new System.Drawing.Point(6, 6);
			this.Training_lstvAreas.Margin = new System.Windows.Forms.Padding(0);
			this.Training_lstvAreas.MultiSelect = false;
			this.Training_lstvAreas.Name = "Training_lstvAreas";
			this.Training_lstvAreas.ShowItemToolTips = true;
			this.Training_lstvAreas.Size = new System.Drawing.Size(149, 301);
			this.Training_lstvAreas.TabIndex = 27;
			this.Training_lstvAreas.Tag = "Source Sans Pro";
			this.Training_lstvAreas.TileSize = new System.Drawing.Size(201, 30);
			this.Training_lstvAreas.UseCompatibleStateImageBehavior = false;
			this.Training_lstvAreas.View = System.Windows.Forms.View.Details;
			this.Training_lstvAreas.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging_Cancel);
			this.Training_lstvAreas.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			// 
			// columnHeader28
			// 
			this.columnHeader28.Text = "Identification name";
			this.columnHeader28.Width = 130;
			// 
			// TabPageH_Training_Option03_Panel
			// 
			this.TabPageH_Training_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Training_Option03_Panel.Controls.Add(this.Training_gbxTrace);
			this.TabPageH_Training_Option03_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Training_Option03_Panel.Name = "TabPageH_Training_Option03_Panel";
			this.TabPageH_Training_Option03_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Training_Option03_Panel.TabIndex = 27;
			this.TabPageH_Training_Option03_Panel.Visible = false;
			// 
			// Training_gbxTrace
			// 
			this.Training_gbxTrace.Controls.Add(this.Training_cmbxTracePlayer);
			this.Training_gbxTrace.Controls.Add(this.Training_btnTraceStart);
			this.Training_gbxTrace.Controls.Add(this.Training_tbxTraceDistance);
			this.Training_gbxTrace.Controls.Add(this.Training_cbxTraceDistance);
			this.Training_gbxTrace.Controls.Add(this.Training_lblTracePlayer);
			this.Training_gbxTrace.Controls.Add(this.Training_cbxTraceMaster);
			this.Training_gbxTrace.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_gbxTrace.ForeColor = System.Drawing.Color.LightGray;
			this.Training_gbxTrace.Location = new System.Drawing.Point(6, 0);
			this.Training_gbxTrace.Name = "Training_gbxTrace";
			this.Training_gbxTrace.Size = new System.Drawing.Size(226, 144);
			this.Training_gbxTrace.TabIndex = 24;
			this.Training_gbxTrace.TabStop = false;
			this.Training_gbxTrace.Tag = "Source Sans Pro";
			this.Training_gbxTrace.Text = "Trace";
			// 
			// Training_cmbxTracePlayer
			// 
			this.Training_cmbxTracePlayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_cmbxTracePlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Training_cmbxTracePlayer.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_cmbxTracePlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_cmbxTracePlayer.FormattingEnabled = true;
			this.Training_cmbxTracePlayer.Items.AddRange(new object[] {
            "Female",
            "Male",
            "Random"});
			this.Training_cmbxTracePlayer.Location = new System.Drawing.Point(91, 14);
			this.Training_cmbxTracePlayer.MaxDropDownItems = 10;
			this.Training_cmbxTracePlayer.Name = "Training_cmbxTracePlayer";
			this.Training_cmbxTracePlayer.Size = new System.Drawing.Size(129, 25);
			this.Training_cmbxTracePlayer.TabIndex = 30;
			this.Training_cmbxTracePlayer.Tag = "Source Sans Pro";
			this.Training_cmbxTracePlayer.DropDown += new System.EventHandler(this.ComboBox_DropDown);
			this.Training_cmbxTracePlayer.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Training_btnTraceStart
			// 
			this.Training_btnTraceStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Training_btnTraceStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Training_btnTraceStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Training_btnTraceStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Training_btnTraceStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Training_btnTraceStart.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_btnTraceStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_btnTraceStart.Location = new System.Drawing.Point(8, 110);
			this.Training_btnTraceStart.Margin = new System.Windows.Forms.Padding(0);
			this.Training_btnTraceStart.Name = "Training_btnTraceStart";
			this.Training_btnTraceStart.Size = new System.Drawing.Size(209, 25);
			this.Training_btnTraceStart.TabIndex = 28;
			this.Training_btnTraceStart.Tag = "Source Sans Pro";
			this.Training_btnTraceStart.Text = "START";
			this.Training_btnTraceStart.UseCompatibleTextRendering = true;
			this.Training_btnTraceStart.UseVisualStyleBackColor = false;
			this.Training_btnTraceStart.Click += new System.EventHandler(this.Control_Click);
			// 
			// Training_tbxTraceDistance
			// 
			this.Training_tbxTraceDistance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Training_tbxTraceDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Training_tbxTraceDistance.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_tbxTraceDistance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_tbxTraceDistance.Location = new System.Drawing.Point(158, 79);
			this.Training_tbxTraceDistance.MaxLength = 3;
			this.Training_tbxTraceDistance.Name = "Training_tbxTraceDistance";
			this.Training_tbxTraceDistance.Size = new System.Drawing.Size(35, 25);
			this.Training_tbxTraceDistance.TabIndex = 3;
			this.Training_tbxTraceDistance.Tag = "Source Sans Pro";
			this.Training_tbxTraceDistance.Text = "5";
			this.Training_tbxTraceDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Training_tbxTraceDistance.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Training_cbxTraceDistance
			// 
			this.Training_cbxTraceDistance.Cursor = System.Windows.Forms.Cursors.Default;
			this.Training_cbxTraceDistance.FlatAppearance.BorderSize = 0;
			this.Training_cbxTraceDistance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Training_cbxTraceDistance.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_cbxTraceDistance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_cbxTraceDistance.Location = new System.Drawing.Point(2, 79);
			this.Training_cbxTraceDistance.Margin = new System.Windows.Forms.Padding(0);
			this.Training_cbxTraceDistance.Name = "Training_cbxTraceDistance";
			this.Training_cbxTraceDistance.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Training_cbxTraceDistance.Size = new System.Drawing.Size(221, 25);
			this.Training_cbxTraceDistance.TabIndex = 2;
			this.Training_cbxTraceDistance.Tag = "Source Sans Pro";
			this.Training_cbxTraceDistance.Text = "Keep player distance";
			this.Training_cbxTraceDistance.UseVisualStyleBackColor = false;
			this.Training_cbxTraceDistance.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Training_lblTracePlayer
			// 
			this.Training_lblTracePlayer.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Training_lblTracePlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_lblTracePlayer.Location = new System.Drawing.Point(8, 14);
			this.Training_lblTracePlayer.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Training_lblTracePlayer.Name = "Training_lblTracePlayer";
			this.Training_lblTracePlayer.Size = new System.Drawing.Size(83, 25);
			this.Training_lblTracePlayer.TabIndex = 0;
			this.Training_lblTracePlayer.Tag = "Source Sans Pro";
			this.Training_lblTracePlayer.Text = "Player name";
			this.Training_lblTracePlayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Training_cbxTraceMaster
			// 
			this.Training_cbxTraceMaster.Cursor = System.Windows.Forms.Cursors.Default;
			this.Training_cbxTraceMaster.FlatAppearance.BorderSize = 0;
			this.Training_cbxTraceMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Training_cbxTraceMaster.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Training_cbxTraceMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Training_cbxTraceMaster.Location = new System.Drawing.Point(2, 39);
			this.Training_cbxTraceMaster.Margin = new System.Windows.Forms.Padding(0);
			this.Training_cbxTraceMaster.Name = "Training_cbxTraceMaster";
			this.Training_cbxTraceMaster.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Training_cbxTraceMaster.Size = new System.Drawing.Size(221, 40);
			this.Training_cbxTraceMaster.TabIndex = 29;
			this.Training_cbxTraceMaster.Tag = "Source Sans Pro";
			this.Training_cbxTraceMaster.Text = "Follow party master if the player name is not near";
			this.ToolTips.SetToolTip(this.Training_cbxTraceMaster, "Only works having party");
			this.Training_cbxTraceMaster.UseVisualStyleBackColor = false;
			this.Training_cbxTraceMaster.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// TabPageH_Training_Option02_Panel
			// 
			this.TabPageH_Training_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Training_Option02_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Training_Option02_Panel.Name = "TabPageH_Training_Option02_Panel";
			this.TabPageH_Training_Option02_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Training_Option02_Panel.TabIndex = 26;
			this.TabPageH_Training_Option02_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option01_Panel
			// 
			this.TabPageV_Control01_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_gbxConnection);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_gbxLogin);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_btnAddSilkroad);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_lblSilkroad);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_cmbxSilkroad);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_gbxAdvertising);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_gbxServers);
			this.TabPageV_Control01_Option01_Panel.Controls.Add(this.Login_gbxCharacters);
			this.TabPageV_Control01_Option01_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option01_Panel.Name = "TabPageV_Control01_Option01_Panel";
			this.TabPageV_Control01_Option01_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option01_Panel.TabIndex = 10;
			this.TabPageV_Control01_Option01_Panel.Visible = false;
			// 
			// Login_gbxConnection
			// 
			this.Login_gbxConnection.Controls.Add(this.Login_cbxUseReturnScroll);
			this.Login_gbxConnection.Controls.Add(this.Login_cbxRelogin);
			this.Login_gbxConnection.Controls.Add(this.Login_cbxGoClientless);
			this.Login_gbxConnection.Controls.Add(this.Login_btnStart);
			this.Login_gbxConnection.Controls.Add(this.Login_rbnClientless);
			this.Login_gbxConnection.Controls.Add(this.Login_rbnClient);
			this.Login_gbxConnection.Controls.Add(this.Login_btnLauncher);
			this.Login_gbxConnection.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_gbxConnection.ForeColor = System.Drawing.Color.LightGray;
			this.Login_gbxConnection.Location = new System.Drawing.Point(6, 31);
			this.Login_gbxConnection.Name = "Login_gbxConnection";
			this.Login_gbxConnection.Size = new System.Drawing.Size(222, 153);
			this.Login_gbxConnection.TabIndex = 3;
			this.Login_gbxConnection.TabStop = false;
			this.Login_gbxConnection.Tag = "Source Sans Pro";
			this.Login_gbxConnection.Text = "Connection";
			// 
			// Login_cbxUseReturnScroll
			// 
			this.Login_cbxUseReturnScroll.Cursor = System.Windows.Forms.Cursors.Default;
			this.Login_cbxUseReturnScroll.FlatAppearance.BorderSize = 0;
			this.Login_cbxUseReturnScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_cbxUseReturnScroll.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_cbxUseReturnScroll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_cbxUseReturnScroll.Location = new System.Drawing.Point(2, 98);
			this.Login_cbxUseReturnScroll.Margin = new System.Windows.Forms.Padding(0);
			this.Login_cbxUseReturnScroll.Name = "Login_cbxUseReturnScroll";
			this.Login_cbxUseReturnScroll.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Login_cbxUseReturnScroll.Size = new System.Drawing.Size(217, 25);
			this.Login_cbxUseReturnScroll.TabIndex = 6;
			this.Login_cbxUseReturnScroll.Tag = "Source Sans Pro";
			this.Login_cbxUseReturnScroll.Text = "Use return scroll after login";
			this.Login_cbxUseReturnScroll.UseVisualStyleBackColor = false;
			// 
			// Login_cbxRelogin
			// 
			this.Login_cbxRelogin.Cursor = System.Windows.Forms.Cursors.Default;
			this.Login_cbxRelogin.FlatAppearance.BorderSize = 0;
			this.Login_cbxRelogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_cbxRelogin.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_cbxRelogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_cbxRelogin.Location = new System.Drawing.Point(2, 123);
			this.Login_cbxRelogin.Margin = new System.Windows.Forms.Padding(0);
			this.Login_cbxRelogin.Name = "Login_cbxRelogin";
			this.Login_cbxRelogin.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Login_cbxRelogin.Size = new System.Drawing.Size(217, 25);
			this.Login_cbxRelogin.TabIndex = 7;
			this.Login_cbxRelogin.Tag = "Source Sans Pro";
			this.Login_cbxRelogin.Text = "Relogin on disconnect";
			this.ToolTips.SetToolTip(this.Login_cbxRelogin, "Relog automatically on disconnect");
			this.Login_cbxRelogin.UseVisualStyleBackColor = false;
			// 
			// Login_cbxGoClientless
			// 
			this.Login_cbxGoClientless.Cursor = System.Windows.Forms.Cursors.Default;
			this.Login_cbxGoClientless.FlatAppearance.BorderSize = 0;
			this.Login_cbxGoClientless.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_cbxGoClientless.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_cbxGoClientless.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_cbxGoClientless.Location = new System.Drawing.Point(2, 73);
			this.Login_cbxGoClientless.Margin = new System.Windows.Forms.Padding(0);
			this.Login_cbxGoClientless.Name = "Login_cbxGoClientless";
			this.Login_cbxGoClientless.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.Login_cbxGoClientless.Size = new System.Drawing.Size(217, 25);
			this.Login_cbxGoClientless.TabIndex = 5;
			this.Login_cbxGoClientless.Tag = "Source Sans Pro";
			this.Login_cbxGoClientless.Text = "Go clientless mode after login";
			this.ToolTips.SetToolTip(this.Login_cbxGoClientless, "Go clientless mode after joined to the game");
			this.Login_cbxGoClientless.UseVisualStyleBackColor = false;
			// 
			// Login_btnStart
			// 
			this.Login_btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Login_btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Login_btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Login_btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Login_btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_btnStart.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_btnStart.Location = new System.Drawing.Point(110, 15);
			this.Login_btnStart.Margin = new System.Windows.Forms.Padding(0);
			this.Login_btnStart.Name = "Login_btnStart";
			this.Login_btnStart.Size = new System.Drawing.Size(104, 28);
			this.Login_btnStart.TabIndex = 3;
			this.Login_btnStart.Tag = "Source Sans Pro";
			this.Login_btnStart.Text = "START";
			this.Login_btnStart.UseCompatibleTextRendering = true;
			this.Login_btnStart.UseVisualStyleBackColor = false;
			this.Login_btnStart.Click += new System.EventHandler(this.Control_Click);
			// 
			// Login_rbnClientless
			// 
			this.Login_rbnClientless.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_rbnClientless.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_rbnClientless.Location = new System.Drawing.Point(8, 45);
			this.Login_rbnClientless.Margin = new System.Windows.Forms.Padding(0);
			this.Login_rbnClientless.Name = "Login_rbnClientless";
			this.Login_rbnClientless.Size = new System.Drawing.Size(100, 24);
			this.Login_rbnClientless.TabIndex = 2;
			this.Login_rbnClientless.TabStop = true;
			this.Login_rbnClientless.Tag = "Source Sans Pro";
			this.Login_rbnClientless.Text = "Clientless";
			this.Login_rbnClientless.UseVisualStyleBackColor = false;
			// 
			// Login_rbnClient
			// 
			this.Login_rbnClient.Checked = true;
			this.Login_rbnClient.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_rbnClient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_rbnClient.Location = new System.Drawing.Point(8, 19);
			this.Login_rbnClient.Margin = new System.Windows.Forms.Padding(0);
			this.Login_rbnClient.Name = "Login_rbnClient";
			this.Login_rbnClient.Size = new System.Drawing.Size(100, 24);
			this.Login_rbnClient.TabIndex = 1;
			this.Login_rbnClient.TabStop = true;
			this.Login_rbnClient.Tag = "Source Sans Pro";
			this.Login_rbnClient.Text = "Use Client";
			this.Login_rbnClient.UseVisualStyleBackColor = false;
			this.Login_rbnClient.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Login_btnLauncher
			// 
			this.Login_btnLauncher.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Login_btnLauncher.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Login_btnLauncher.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Login_btnLauncher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Login_btnLauncher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_btnLauncher.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_btnLauncher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_btnLauncher.Location = new System.Drawing.Point(110, 45);
			this.Login_btnLauncher.Margin = new System.Windows.Forms.Padding(0);
			this.Login_btnLauncher.Name = "Login_btnLauncher";
			this.Login_btnLauncher.Size = new System.Drawing.Size(104, 28);
			this.Login_btnLauncher.TabIndex = 4;
			this.Login_btnLauncher.Tag = "Source Sans Pro";
			this.Login_btnLauncher.Text = "LAUNCHER";
			this.ToolTips.SetToolTip(this.Login_btnLauncher, "Execute Silkroad Launcher");
			this.Login_btnLauncher.UseCompatibleTextRendering = true;
			this.Login_btnLauncher.UseVisualStyleBackColor = false;
			this.Login_btnLauncher.Click += new System.EventHandler(this.Control_Click);
			// 
			// Login_gbxLogin
			// 
			this.Login_gbxLogin.Controls.Add(this.Login_tbxCaptcha);
			this.Login_gbxLogin.Controls.Add(this.Login_lblCaptcha);
			this.Login_gbxLogin.Controls.Add(this.Login_cmbxCharacter);
			this.Login_gbxLogin.Controls.Add(this.Login_cmbxServer);
			this.Login_gbxLogin.Controls.Add(this.Login_tbxPassword);
			this.Login_gbxLogin.Controls.Add(this.Login_lblPassword);
			this.Login_gbxLogin.Controls.Add(this.Login_lblCharacter);
			this.Login_gbxLogin.Controls.Add(this.Login_lblServer);
			this.Login_gbxLogin.Controls.Add(this.Login_tbxUsername);
			this.Login_gbxLogin.Controls.Add(this.Login_lblUsername);
			this.Login_gbxLogin.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_gbxLogin.ForeColor = System.Drawing.Color.LightGray;
			this.Login_gbxLogin.Location = new System.Drawing.Point(6, 184);
			this.Login_gbxLogin.Name = "Login_gbxLogin";
			this.Login_gbxLogin.Size = new System.Drawing.Size(222, 151);
			this.Login_gbxLogin.TabIndex = 4;
			this.Login_gbxLogin.TabStop = false;
			this.Login_gbxLogin.Tag = "Source Sans Pro";
			this.Login_gbxLogin.Text = "Login";
			// 
			// Login_tbxCaptcha
			// 
			this.Login_tbxCaptcha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Login_tbxCaptcha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_tbxCaptcha.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_tbxCaptcha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_tbxCaptcha.Location = new System.Drawing.Point(70, 67);
			this.Login_tbxCaptcha.Name = "Login_tbxCaptcha";
			this.Login_tbxCaptcha.PasswordChar = '*';
			this.Login_tbxCaptcha.Size = new System.Drawing.Size(146, 25);
			this.Login_tbxCaptcha.TabIndex = 5;
			this.Login_tbxCaptcha.Tag = "Source Sans Pro";
			this.Login_tbxCaptcha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Login_tbxCaptcha.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Login_tbxCaptcha.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Login_lblCaptcha
			// 
			this.Login_lblCaptcha.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lblCaptcha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lblCaptcha.Location = new System.Drawing.Point(6, 67);
			this.Login_lblCaptcha.Margin = new System.Windows.Forms.Padding(3);
			this.Login_lblCaptcha.Name = "Login_lblCaptcha";
			this.Login_lblCaptcha.Size = new System.Drawing.Size(64, 25);
			this.Login_lblCaptcha.TabIndex = 4;
			this.Login_lblCaptcha.Tag = "Source Sans Pro";
			this.Login_lblCaptcha.Text = "Captcha";
			this.Login_lblCaptcha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Login_cmbxCharacter
			// 
			this.Login_cmbxCharacter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Login_cmbxCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Login_cmbxCharacter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_cmbxCharacter.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_cmbxCharacter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_cmbxCharacter.FormattingEnabled = true;
			this.Login_cmbxCharacter.Location = new System.Drawing.Point(82, 118);
			this.Login_cmbxCharacter.MaxDropDownItems = 5;
			this.Login_cmbxCharacter.Name = "Login_cmbxCharacter";
			this.Login_cmbxCharacter.Size = new System.Drawing.Size(134, 26);
			this.Login_cmbxCharacter.TabIndex = 9;
			this.Login_cmbxCharacter.Tag = "Source Sans Pro";
			this.Login_cmbxCharacter.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Login_cmbxCharacter.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Login_cmbxServer
			// 
			this.Login_cmbxServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Login_cmbxServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Login_cmbxServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_cmbxServer.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_cmbxServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_cmbxServer.FormattingEnabled = true;
			this.Login_cmbxServer.Location = new System.Drawing.Point(60, 92);
			this.Login_cmbxServer.MaxDropDownItems = 5;
			this.Login_cmbxServer.Name = "Login_cmbxServer";
			this.Login_cmbxServer.Size = new System.Drawing.Size(156, 26);
			this.Login_cmbxServer.TabIndex = 7;
			this.Login_cmbxServer.Tag = "Source Sans Pro";
			this.Login_cmbxServer.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			this.Login_cmbxServer.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Login_cmbxServer.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Login_tbxPassword
			// 
			this.Login_tbxPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Login_tbxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_tbxPassword.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_tbxPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_tbxPassword.Location = new System.Drawing.Point(80, 41);
			this.Login_tbxPassword.Name = "Login_tbxPassword";
			this.Login_tbxPassword.PasswordChar = '*';
			this.Login_tbxPassword.Size = new System.Drawing.Size(136, 25);
			this.Login_tbxPassword.TabIndex = 3;
			this.Login_tbxPassword.Tag = "Source Sans Pro";
			this.Login_tbxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Login_tbxPassword.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Login_tbxPassword.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Login_lblPassword
			// 
			this.Login_lblPassword.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lblPassword.Location = new System.Drawing.Point(6, 41);
			this.Login_lblPassword.Margin = new System.Windows.Forms.Padding(3);
			this.Login_lblPassword.Name = "Login_lblPassword";
			this.Login_lblPassword.Size = new System.Drawing.Size(74, 25);
			this.Login_lblPassword.TabIndex = 2;
			this.Login_lblPassword.Tag = "Source Sans Pro";
			this.Login_lblPassword.Text = "Password";
			this.Login_lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Login_lblCharacter
			// 
			this.Login_lblCharacter.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lblCharacter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lblCharacter.Location = new System.Drawing.Point(6, 118);
			this.Login_lblCharacter.Margin = new System.Windows.Forms.Padding(3);
			this.Login_lblCharacter.Name = "Login_lblCharacter";
			this.Login_lblCharacter.Size = new System.Drawing.Size(75, 26);
			this.Login_lblCharacter.TabIndex = 8;
			this.Login_lblCharacter.Tag = "Source Sans Pro";
			this.Login_lblCharacter.Text = "Character";
			this.Login_lblCharacter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Login_lblServer
			// 
			this.Login_lblServer.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lblServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lblServer.Location = new System.Drawing.Point(6, 92);
			this.Login_lblServer.Margin = new System.Windows.Forms.Padding(3);
			this.Login_lblServer.Name = "Login_lblServer";
			this.Login_lblServer.Size = new System.Drawing.Size(53, 26);
			this.Login_lblServer.TabIndex = 6;
			this.Login_lblServer.Tag = "Source Sans Pro";
			this.Login_lblServer.Text = "Server";
			this.Login_lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Login_tbxUsername
			// 
			this.Login_tbxUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Login_tbxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_tbxUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.Login_tbxUsername.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_tbxUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_tbxUsername.Location = new System.Drawing.Point(84, 15);
			this.Login_tbxUsername.Name = "Login_tbxUsername";
			this.Login_tbxUsername.Size = new System.Drawing.Size(132, 25);
			this.Login_tbxUsername.TabIndex = 1;
			this.Login_tbxUsername.Tag = "Source Sans Pro";
			this.Login_tbxUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Login_tbxUsername.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Login_tbxUsername.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Login_lblUsername
			// 
			this.Login_lblUsername.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lblUsername.Location = new System.Drawing.Point(6, 15);
			this.Login_lblUsername.Margin = new System.Windows.Forms.Padding(3);
			this.Login_lblUsername.Name = "Login_lblUsername";
			this.Login_lblUsername.Size = new System.Drawing.Size(78, 25);
			this.Login_lblUsername.TabIndex = 0;
			this.Login_lblUsername.Tag = "Source Sans Pro";
			this.Login_lblUsername.Text = "Username";
			this.Login_lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Login_btnAddSilkroad
			// 
			this.Login_btnAddSilkroad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Login_btnAddSilkroad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.Login_btnAddSilkroad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Login_btnAddSilkroad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Login_btnAddSilkroad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_btnAddSilkroad.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_btnAddSilkroad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_btnAddSilkroad.Location = new System.Drawing.Point(202, 5);
			this.Login_btnAddSilkroad.Margin = new System.Windows.Forms.Padding(0);
			this.Login_btnAddSilkroad.Name = "Login_btnAddSilkroad";
			this.Login_btnAddSilkroad.Size = new System.Drawing.Size(26, 26);
			this.Login_btnAddSilkroad.TabIndex = 2;
			this.Login_btnAddSilkroad.Tag = "Font Awesome 5 Pro Regular";
			this.Login_btnAddSilkroad.Text = "";
			this.ToolTips.SetToolTip(this.Login_btnAddSilkroad, "Add Silkroad");
			this.Login_btnAddSilkroad.UseCompatibleTextRendering = true;
			this.Login_btnAddSilkroad.UseVisualStyleBackColor = false;
			this.Login_btnAddSilkroad.Click += new System.EventHandler(this.Control_Click);
			// 
			// Login_lblSilkroad
			// 
			this.Login_lblSilkroad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_lblSilkroad.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lblSilkroad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lblSilkroad.Location = new System.Drawing.Point(6, 5);
			this.Login_lblSilkroad.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.Login_lblSilkroad.Name = "Login_lblSilkroad";
			this.Login_lblSilkroad.Size = new System.Drawing.Size(112, 26);
			this.Login_lblSilkroad.TabIndex = 0;
			this.Login_lblSilkroad.Tag = "Source Sans Pro";
			this.Login_lblSilkroad.Text = "Select Silkroad";
			this.Login_lblSilkroad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Login_cmbxSilkroad
			// 
			this.Login_cmbxSilkroad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Login_cmbxSilkroad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Login_cmbxSilkroad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Login_cmbxSilkroad.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Login_cmbxSilkroad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_cmbxSilkroad.FormattingEnabled = true;
			this.Login_cmbxSilkroad.Location = new System.Drawing.Point(118, 5);
			this.Login_cmbxSilkroad.MaxDropDownItems = 5;
			this.Login_cmbxSilkroad.Name = "Login_cmbxSilkroad";
			this.Login_cmbxSilkroad.Size = new System.Drawing.Size(84, 26);
			this.Login_cmbxSilkroad.Sorted = true;
			this.Login_cmbxSilkroad.TabIndex = 1;
			this.Login_cmbxSilkroad.Tag = "Source Sans Pro";
			this.Login_cmbxSilkroad.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Login_cmbxSilkroad.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Login_gbxAdvertising
			// 
			this.Login_gbxAdvertising.Controls.Add(this.Login_pbxAds);
			this.Login_gbxAdvertising.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_gbxAdvertising.ForeColor = System.Drawing.Color.LightGray;
			this.Login_gbxAdvertising.Location = new System.Drawing.Point(234, 184);
			this.Login_gbxAdvertising.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Login_gbxAdvertising.Name = "Login_gbxAdvertising";
			this.Login_gbxAdvertising.Size = new System.Drawing.Size(415, 151);
			this.Login_gbxAdvertising.TabIndex = 6;
			this.Login_gbxAdvertising.TabStop = false;
			this.Login_gbxAdvertising.Tag = "Source Sans Pro";
			this.Login_gbxAdvertising.Text = "Advertising";
			// 
			// Login_pbxAds
			// 
			this.Login_pbxAds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_pbxAds.Image = global::xBot.Properties.Resources.banner_example;
			this.Login_pbxAds.ImageLocation = "";
			this.Login_pbxAds.Location = new System.Drawing.Point(6, 15);
			this.Login_pbxAds.Name = "Login_pbxAds";
			this.Login_pbxAds.Size = new System.Drawing.Size(403, 129);
			this.Login_pbxAds.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.Login_pbxAds.TabIndex = 0;
			this.Login_pbxAds.TabStop = false;
			this.Login_pbxAds.WaitOnLoad = true;
			this.Login_pbxAds.Click += new System.EventHandler(this.Control_Click);
			// 
			// Login_gbxServers
			// 
			this.Login_gbxServers.Controls.Add(this.Login_lstvServers);
			this.Login_gbxServers.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_gbxServers.ForeColor = System.Drawing.Color.LightGray;
			this.Login_gbxServers.Location = new System.Drawing.Point(234, 0);
			this.Login_gbxServers.Name = "Login_gbxServers";
			this.Login_gbxServers.Size = new System.Drawing.Size(415, 184);
			this.Login_gbxServers.TabIndex = 4;
			this.Login_gbxServers.TabStop = false;
			this.Login_gbxServers.Text = "Server list";
			// 
			// Login_lstvServers
			// 
			this.Login_lstvServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Login_lstvServers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_lstvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.Login_lstvServers.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lstvServers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lstvServers.FullRowSelect = true;
			this.Login_lstvServers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.Login_lstvServers.HideSelection = false;
			this.Login_lstvServers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Login_lstvServers.Location = new System.Drawing.Point(7, 15);
			this.Login_lstvServers.MultiSelect = false;
			this.Login_lstvServers.Name = "Login_lstvServers";
			this.Login_lstvServers.ShowGroups = false;
			this.Login_lstvServers.Size = new System.Drawing.Size(401, 162);
			this.Login_lstvServers.TabIndex = 10;
			this.Login_lstvServers.TileSize = new System.Drawing.Size(201, 30);
			this.Login_lstvServers.UseCompatibleStateImageBehavior = false;
			this.Login_lstvServers.View = System.Windows.Forms.View.Details;
			this.Login_lstvServers.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging_Cancel);
			this.Login_lstvServers.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 160;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Capacity";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 130;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "State";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader4.Width = 90;
			// 
			// Login_gbxCharacters
			// 
			this.Login_gbxCharacters.Controls.Add(this.Login_lstvCharacters);
			this.Login_gbxCharacters.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_gbxCharacters.ForeColor = System.Drawing.Color.LightGray;
			this.Login_gbxCharacters.Location = new System.Drawing.Point(234, 0);
			this.Login_gbxCharacters.Name = "Login_gbxCharacters";
			this.Login_gbxCharacters.Size = new System.Drawing.Size(415, 184);
			this.Login_gbxCharacters.TabIndex = 5;
			this.Login_gbxCharacters.TabStop = false;
			this.Login_gbxCharacters.Tag = "Source Sans Pro";
			this.Login_gbxCharacters.Text = "Character list";
			this.Login_gbxCharacters.Visible = false;
			// 
			// Login_lstvCharacters
			// 
			this.Login_lstvCharacters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Login_lstvCharacters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Login_lstvCharacters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
			this.Login_lstvCharacters.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Login_lstvCharacters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Login_lstvCharacters.FullRowSelect = true;
			this.Login_lstvCharacters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.Login_lstvCharacters.HideSelection = false;
			this.Login_lstvCharacters.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Login_lstvCharacters.Location = new System.Drawing.Point(7, 15);
			this.Login_lstvCharacters.MultiSelect = false;
			this.Login_lstvCharacters.Name = "Login_lstvCharacters";
			this.Login_lstvCharacters.ShowGroups = false;
			this.Login_lstvCharacters.Size = new System.Drawing.Size(401, 162);
			this.Login_lstvCharacters.TabIndex = 1;
			this.Login_lstvCharacters.Tag = "Source Sans Pro";
			this.Login_lstvCharacters.TileSize = new System.Drawing.Size(201, 45);
			this.Login_lstvCharacters.UseCompatibleStateImageBehavior = false;
			this.Login_lstvCharacters.View = System.Windows.Forms.View.Details;
			this.Login_lstvCharacters.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging_Cancel);
			this.Login_lstvCharacters.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Name";
			this.columnHeader5.Width = 160;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Level";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader6.Width = 70;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Experience";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 75;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "";
			this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader8.Width = 75;
			// 
			// TabPageV_Control01_Option07_Panel
			// 
			this.TabPageV_Control01_Option07_Panel.Controls.Add(this.TabPageH_Skills);
			this.TabPageV_Control01_Option07_Panel.Controls.Add(this.Skills_lstvSkills);
			this.TabPageV_Control01_Option07_Panel.Controls.Add(this.TabPageH_Skills_Option01_Panel);
			this.TabPageV_Control01_Option07_Panel.Controls.Add(this.TabPageH_Skills_Option02_Panel);
			this.TabPageV_Control01_Option07_Panel.Controls.Add(this.TabPageH_Skills_Option03_Panel);
			this.TabPageV_Control01_Option07_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option07_Panel.Name = "TabPageV_Control01_Option07_Panel";
			this.TabPageV_Control01_Option07_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option07_Panel.TabIndex = 12;
			this.TabPageV_Control01_Option07_Panel.Visible = false;
			// 
			// TabPageH_Skills
			// 
			this.TabPageH_Skills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Skills.Controls.Add(this.TabPageH_Skills_Option03);
			this.TabPageH_Skills.Controls.Add(this.TabPageH_Skills_Option02);
			this.TabPageH_Skills.Controls.Add(this.TabPageH_Skills_Option01);
			this.TabPageH_Skills.Location = new System.Drawing.Point(185, 0);
			this.TabPageH_Skills.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Skills.Name = "TabPageH_Skills";
			this.TabPageH_Skills.Size = new System.Drawing.Size(472, 28);
			this.TabPageH_Skills.TabIndex = 25;
			// 
			// TabPageH_Skills_Option03
			// 
			this.TabPageH_Skills_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Skills_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Skills_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Skills_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Skills_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Skills_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Skills_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Skills_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Skills_Option03.Location = new System.Drawing.Point(315, 0);
			this.TabPageH_Skills_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Skills_Option03.Name = "TabPageH_Skills_Option03";
			this.TabPageH_Skills_Option03.Size = new System.Drawing.Size(157, 26);
			this.TabPageH_Skills_Option03.TabIndex = 14;
			this.TabPageH_Skills_Option03.Tag = "Source Sans Pro";
			this.TabPageH_Skills_Option03.Text = "Party Buff";
			this.TabPageH_Skills_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Skills_Option03.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Skills_Option02
			// 
			this.TabPageH_Skills_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Skills_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Skills_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Skills_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Skills_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Skills_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Skills_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Skills_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Skills_Option02.Location = new System.Drawing.Point(158, 0);
			this.TabPageH_Skills_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Skills_Option02.Name = "TabPageH_Skills_Option02";
			this.TabPageH_Skills_Option02.Size = new System.Drawing.Size(157, 26);
			this.TabPageH_Skills_Option02.TabIndex = 13;
			this.TabPageH_Skills_Option02.Tag = "Source Sans Pro";
			this.TabPageH_Skills_Option02.Text = "Buff";
			this.TabPageH_Skills_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Skills_Option02.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Skills_Option01
			// 
			this.TabPageH_Skills_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Skills_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Skills_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Skills_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Skills_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Skills_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Skills_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Skills_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Skills_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Skills_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Skills_Option01.Name = "TabPageH_Skills_Option01";
			this.TabPageH_Skills_Option01.Size = new System.Drawing.Size(158, 26);
			this.TabPageH_Skills_Option01.TabIndex = 12;
			this.TabPageH_Skills_Option01.Tag = "Source Sans Pro";
			this.TabPageH_Skills_Option01.Text = "Attack";
			this.TabPageH_Skills_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Skills_Option01.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// Skills_lstvSkills
			// 
			this.Skills_lstvSkills.AllowDrop = true;
			this.Skills_lstvSkills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvSkills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvSkills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader34});
			this.Skills_lstvSkills.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Skills_lstvSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvSkills.FullRowSelect = true;
			this.Skills_lstvSkills.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvSkills.HideSelection = false;
			this.Skills_lstvSkills.Location = new System.Drawing.Point(0, 0);
			this.Skills_lstvSkills.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvSkills.Name = "Skills_lstvSkills";
			this.Skills_lstvSkills.ShowItemToolTips = true;
			this.Skills_lstvSkills.Size = new System.Drawing.Size(185, 342);
			this.Skills_lstvSkills.SmallImageList = this.lstimgIcons;
			this.Skills_lstvSkills.TabIndex = 26;
			this.Skills_lstvSkills.Tag = "Source Sans Pro";
			this.Skills_lstvSkills.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvSkills.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvSkills.View = System.Windows.Forms.View.Details;
			this.Skills_lstvSkills.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvSkills.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_RemoveFromSource);
			this.Skills_lstvSkills.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader34
			// 
			this.columnHeader34.Text = "Skill name";
			this.columnHeader34.Width = 168;
			// 
			// TabPageH_Skills_Option01_Panel
			// 
			this.TabPageH_Skills_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_Event);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_Unique);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_Elite);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_PartyGiant);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_PartyChampion);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_PartyGeneral);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_Giant);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_Champion);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_cmbxAttackMobType);
			this.TabPageH_Skills_Option01_Panel.Controls.Add(this.Skills_lstvAttackMobType_General);
			this.TabPageH_Skills_Option01_Panel.Location = new System.Drawing.Point(184, 27);
			this.TabPageH_Skills_Option01_Panel.Name = "TabPageH_Skills_Option01_Panel";
			this.TabPageH_Skills_Option01_Panel.Size = new System.Drawing.Size(473, 315);
			this.TabPageH_Skills_Option01_Panel.TabIndex = 28;
			this.TabPageH_Skills_Option01_Panel.Visible = false;
			// 
			// Skills_lstvAttackMobType_Event
			// 
			this.Skills_lstvAttackMobType_Event.AllowDrop = true;
			this.Skills_lstvAttackMobType_Event.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_Event.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_Event.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader41});
			this.Skills_lstvAttackMobType_Event.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Skills_lstvAttackMobType_Event.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_Event.FullRowSelect = true;
			this.Skills_lstvAttackMobType_Event.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_Event.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_Event.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_Event.Name = "Skills_lstvAttackMobType_Event";
			this.Skills_lstvAttackMobType_Event.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_Event.TabIndex = 40;
			this.Skills_lstvAttackMobType_Event.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_Event.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_Event.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_Event.Visible = false;
			this.Skills_lstvAttackMobType_Event.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_Event.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_Event.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader41
			// 
			this.columnHeader41.Text = "Skill name";
			this.columnHeader41.Width = 168;
			// 
			// Skills_lstvAttackMobType_Unique
			// 
			this.Skills_lstvAttackMobType_Unique.AllowDrop = true;
			this.Skills_lstvAttackMobType_Unique.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_Unique.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_Unique.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader42});
			this.Skills_lstvAttackMobType_Unique.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_Unique.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_Unique.FullRowSelect = true;
			this.Skills_lstvAttackMobType_Unique.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_Unique.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_Unique.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_Unique.Name = "Skills_lstvAttackMobType_Unique";
			this.Skills_lstvAttackMobType_Unique.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_Unique.TabIndex = 39;
			this.Skills_lstvAttackMobType_Unique.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_Unique.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_Unique.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_Unique.Visible = false;
			// 
			// columnHeader42
			// 
			this.columnHeader42.Text = "Skill name";
			this.columnHeader42.Width = 168;
			// 
			// Skills_lstvAttackMobType_Elite
			// 
			this.Skills_lstvAttackMobType_Elite.AllowDrop = true;
			this.Skills_lstvAttackMobType_Elite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_Elite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_Elite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader40});
			this.Skills_lstvAttackMobType_Elite.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_Elite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_Elite.FullRowSelect = true;
			this.Skills_lstvAttackMobType_Elite.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_Elite.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_Elite.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_Elite.Name = "Skills_lstvAttackMobType_Elite";
			this.Skills_lstvAttackMobType_Elite.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_Elite.TabIndex = 37;
			this.Skills_lstvAttackMobType_Elite.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_Elite.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_Elite.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_Elite.Visible = false;
			this.Skills_lstvAttackMobType_Elite.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_Elite.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_Elite.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader40
			// 
			this.columnHeader40.Text = "Skill name";
			this.columnHeader40.Width = 168;
			// 
			// Skills_lstvAttackMobType_PartyGiant
			// 
			this.Skills_lstvAttackMobType_PartyGiant.AllowDrop = true;
			this.Skills_lstvAttackMobType_PartyGiant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_PartyGiant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_PartyGiant.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader39});
			this.Skills_lstvAttackMobType_PartyGiant.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_PartyGiant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_PartyGiant.FullRowSelect = true;
			this.Skills_lstvAttackMobType_PartyGiant.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_PartyGiant.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_PartyGiant.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_PartyGiant.Name = "Skills_lstvAttackMobType_PartyGiant";
			this.Skills_lstvAttackMobType_PartyGiant.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_PartyGiant.TabIndex = 36;
			this.Skills_lstvAttackMobType_PartyGiant.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_PartyGiant.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_PartyGiant.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_PartyGiant.Visible = false;
			this.Skills_lstvAttackMobType_PartyGiant.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_PartyGiant.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_PartyGiant.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader39
			// 
			this.columnHeader39.Text = "Skill name";
			this.columnHeader39.Width = 168;
			// 
			// Skills_lstvAttackMobType_PartyChampion
			// 
			this.Skills_lstvAttackMobType_PartyChampion.AllowDrop = true;
			this.Skills_lstvAttackMobType_PartyChampion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_PartyChampion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_PartyChampion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader38});
			this.Skills_lstvAttackMobType_PartyChampion.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_PartyChampion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_PartyChampion.FullRowSelect = true;
			this.Skills_lstvAttackMobType_PartyChampion.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_PartyChampion.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_PartyChampion.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_PartyChampion.Name = "Skills_lstvAttackMobType_PartyChampion";
			this.Skills_lstvAttackMobType_PartyChampion.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_PartyChampion.TabIndex = 35;
			this.Skills_lstvAttackMobType_PartyChampion.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_PartyChampion.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_PartyChampion.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_PartyChampion.Visible = false;
			this.Skills_lstvAttackMobType_PartyChampion.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_PartyChampion.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_PartyChampion.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader38
			// 
			this.columnHeader38.Text = "Skill name";
			this.columnHeader38.Width = 168;
			// 
			// Skills_lstvAttackMobType_PartyGeneral
			// 
			this.Skills_lstvAttackMobType_PartyGeneral.AllowDrop = true;
			this.Skills_lstvAttackMobType_PartyGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_PartyGeneral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_PartyGeneral.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader37});
			this.Skills_lstvAttackMobType_PartyGeneral.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_PartyGeneral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_PartyGeneral.FullRowSelect = true;
			this.Skills_lstvAttackMobType_PartyGeneral.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_PartyGeneral.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_PartyGeneral.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_PartyGeneral.Name = "Skills_lstvAttackMobType_PartyGeneral";
			this.Skills_lstvAttackMobType_PartyGeneral.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_PartyGeneral.TabIndex = 34;
			this.Skills_lstvAttackMobType_PartyGeneral.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_PartyGeneral.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_PartyGeneral.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_PartyGeneral.Visible = false;
			this.Skills_lstvAttackMobType_PartyGeneral.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_PartyGeneral.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_PartyGeneral.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader37
			// 
			this.columnHeader37.Text = "Skill name";
			this.columnHeader37.Width = 168;
			// 
			// Skills_lstvAttackMobType_Giant
			// 
			this.Skills_lstvAttackMobType_Giant.AllowDrop = true;
			this.Skills_lstvAttackMobType_Giant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_Giant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_Giant.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader36});
			this.Skills_lstvAttackMobType_Giant.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_Giant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_Giant.FullRowSelect = true;
			this.Skills_lstvAttackMobType_Giant.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_Giant.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_Giant.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_Giant.Name = "Skills_lstvAttackMobType_Giant";
			this.Skills_lstvAttackMobType_Giant.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_Giant.TabIndex = 33;
			this.Skills_lstvAttackMobType_Giant.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_Giant.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_Giant.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_Giant.Visible = false;
			this.Skills_lstvAttackMobType_Giant.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_Giant.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_Giant.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader36
			// 
			this.columnHeader36.Text = "Skill name";
			this.columnHeader36.Width = 168;
			// 
			// Skills_lstvAttackMobType_Champion
			// 
			this.Skills_lstvAttackMobType_Champion.AllowDrop = true;
			this.Skills_lstvAttackMobType_Champion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_Champion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_Champion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader24});
			this.Skills_lstvAttackMobType_Champion.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_Champion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_Champion.FullRowSelect = true;
			this.Skills_lstvAttackMobType_Champion.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_Champion.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_Champion.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_Champion.Name = "Skills_lstvAttackMobType_Champion";
			this.Skills_lstvAttackMobType_Champion.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_Champion.TabIndex = 32;
			this.Skills_lstvAttackMobType_Champion.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_Champion.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_Champion.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_Champion.Visible = false;
			this.Skills_lstvAttackMobType_Champion.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_Champion.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_Champion.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader24
			// 
			this.columnHeader24.Text = "Skill name";
			this.columnHeader24.Width = 168;
			// 
			// Skills_cmbxAttackMobType
			// 
			this.Skills_cmbxAttackMobType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_cmbxAttackMobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Skills_cmbxAttackMobType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Skills_cmbxAttackMobType.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Skills_cmbxAttackMobType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_cmbxAttackMobType.FormattingEnabled = true;
			this.Skills_cmbxAttackMobType.Items.AddRange(new object[] {
            "General",
            "Champion",
            "Giant",
            "PartyGeneral",
            "PartyChampion",
            "PartyGiant",
            "Unique",
            "Elite",
            "Event"});
			this.Skills_cmbxAttackMobType.Location = new System.Drawing.Point(6, 7);
			this.Skills_cmbxAttackMobType.MaxDropDownItems = 10;
			this.Skills_cmbxAttackMobType.Name = "Skills_cmbxAttackMobType";
			this.Skills_cmbxAttackMobType.Size = new System.Drawing.Size(185, 25);
			this.Skills_cmbxAttackMobType.TabIndex = 31;
			this.Skills_cmbxAttackMobType.Tag = "Source Sans Pro";
			this.Skills_cmbxAttackMobType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// Skills_lstvAttackMobType_General
			// 
			this.Skills_lstvAttackMobType_General.AllowDrop = true;
			this.Skills_lstvAttackMobType_General.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Skills_lstvAttackMobType_General.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Skills_lstvAttackMobType_General.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader35});
			this.Skills_lstvAttackMobType_General.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Skills_lstvAttackMobType_General.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Skills_lstvAttackMobType_General.FullRowSelect = true;
			this.Skills_lstvAttackMobType_General.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Skills_lstvAttackMobType_General.Location = new System.Drawing.Point(6, 32);
			this.Skills_lstvAttackMobType_General.Margin = new System.Windows.Forms.Padding(0);
			this.Skills_lstvAttackMobType_General.Name = "Skills_lstvAttackMobType_General";
			this.Skills_lstvAttackMobType_General.Size = new System.Drawing.Size(185, 140);
			this.Skills_lstvAttackMobType_General.TabIndex = 11;
			this.Skills_lstvAttackMobType_General.TileSize = new System.Drawing.Size(201, 30);
			this.Skills_lstvAttackMobType_General.UseCompatibleStateImageBehavior = false;
			this.Skills_lstvAttackMobType_General.View = System.Windows.Forms.View.Details;
			this.Skills_lstvAttackMobType_General.Visible = false;
			this.Skills_lstvAttackMobType_General.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
			this.Skills_lstvAttackMobType_General.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop_CopyFromSource);
			this.Skills_lstvAttackMobType_General.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
			// 
			// columnHeader35
			// 
			this.columnHeader35.Text = "Skill name";
			this.columnHeader35.Width = 168;
			// 
			// TabPageH_Skills_Option02_Panel
			// 
			this.TabPageH_Skills_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Skills_Option02_Panel.Location = new System.Drawing.Point(184, 27);
			this.TabPageH_Skills_Option02_Panel.Name = "TabPageH_Skills_Option02_Panel";
			this.TabPageH_Skills_Option02_Panel.Size = new System.Drawing.Size(473, 315);
			this.TabPageH_Skills_Option02_Panel.TabIndex = 30;
			this.TabPageH_Skills_Option02_Panel.Visible = false;
			// 
			// TabPageH_Skills_Option03_Panel
			// 
			this.TabPageH_Skills_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Skills_Option03_Panel.Location = new System.Drawing.Point(184, 27);
			this.TabPageH_Skills_Option03_Panel.Name = "TabPageH_Skills_Option03_Panel";
			this.TabPageH_Skills_Option03_Panel.Size = new System.Drawing.Size(473, 315);
			this.TabPageH_Skills_Option03_Panel.TabIndex = 29;
			this.TabPageH_Skills_Option03_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option02_Panel
			// 
			this.TabPageV_Control01_Option02_Panel.Controls.Add(this.TabPageH_Character);
			this.TabPageV_Control01_Option02_Panel.Controls.Add(this.TabPageH_Character_Option01_Panel);
			this.TabPageV_Control01_Option02_Panel.Controls.Add(this.TabPageH_Character_Option02_Panel);
			this.TabPageV_Control01_Option02_Panel.Controls.Add(this.TabPageH_Character_Option04_Panel);
			this.TabPageV_Control01_Option02_Panel.Controls.Add(this.TabPageH_Character_Option03_Panel);
			this.TabPageV_Control01_Option02_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option02_Panel.Name = "TabPageV_Control01_Option02_Panel";
			this.TabPageV_Control01_Option02_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option02_Panel.TabIndex = 6;
			this.TabPageV_Control01_Option02_Panel.Visible = false;
			// 
			// TabPageH_Character
			// 
			this.TabPageH_Character.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Character.Controls.Add(this.TabPageH_Character_Option04);
			this.TabPageH_Character.Controls.Add(this.TabPageH_Character_Option03);
			this.TabPageH_Character.Controls.Add(this.TabPageH_Character_Option02);
			this.TabPageH_Character.Controls.Add(this.TabPageH_Character_Option01);
			this.TabPageH_Character.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Character.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Character.Name = "TabPageH_Character";
			this.TabPageH_Character.Size = new System.Drawing.Size(657, 28);
			this.TabPageH_Character.TabIndex = 9;
			// 
			// TabPageH_Character_Option04
			// 
			this.TabPageH_Character_Option04.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Character_Option04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Character_Option04.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option04.FlatAppearance.BorderSize = 0;
			this.TabPageH_Character_Option04.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Character_Option04.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Character_Option04.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Character_Option04.Location = new System.Drawing.Point(493, 0);
			this.TabPageH_Character_Option04.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Character_Option04.Name = "TabPageH_Character_Option04";
			this.TabPageH_Character_Option04.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Character_Option04.TabIndex = 15;
			this.TabPageH_Character_Option04.Tag = "Source Sans Pro";
			this.TabPageH_Character_Option04.Text = "Misc.";
			this.TabPageH_Character_Option04.UseVisualStyleBackColor = false;
			this.TabPageH_Character_Option04.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Character_Option03
			// 
			this.TabPageH_Character_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Character_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Character_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Character_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Character_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Character_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Character_Option03.Location = new System.Drawing.Point(329, 0);
			this.TabPageH_Character_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Character_Option03.Name = "TabPageH_Character_Option03";
			this.TabPageH_Character_Option03.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Character_Option03.TabIndex = 14;
			this.TabPageH_Character_Option03.Tag = "Source Sans Pro";
			this.TabPageH_Character_Option03.Text = ". . .";
			this.TabPageH_Character_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Character_Option03.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Character_Option02
			// 
			this.TabPageH_Character_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Character_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Character_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Character_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Character_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Character_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Character_Option02.Location = new System.Drawing.Point(165, 0);
			this.TabPageH_Character_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Character_Option02.Name = "TabPageH_Character_Option02";
			this.TabPageH_Character_Option02.Size = new System.Drawing.Size(164, 26);
			this.TabPageH_Character_Option02.TabIndex = 13;
			this.TabPageH_Character_Option02.Tag = "Source Sans Pro";
			this.TabPageH_Character_Option02.Text = "Potions";
			this.TabPageH_Character_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Character_Option02.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Character_Option01
			// 
			this.TabPageH_Character_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Character_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Character_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Character_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Character_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Character_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Character_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Character_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Character_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Character_Option01.Name = "TabPageH_Character_Option01";
			this.TabPageH_Character_Option01.Size = new System.Drawing.Size(165, 26);
			this.TabPageH_Character_Option01.TabIndex = 12;
			this.TabPageH_Character_Option01.Tag = "Source Sans Pro";
			this.TabPageH_Character_Option01.Text = "Info";
			this.TabPageH_Character_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Character_Option01.Click += new System.EventHandler(this.TabPageH_Option_Click);
			// 
			// TabPageH_Character_Option01_Panel
			// 
			this.TabPageH_Character_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lstvBuffs);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblCoordY);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblCoordX);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblCoords);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblLocation);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblLocationText);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblSP);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblSPText);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblGold);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblGoldText);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblJobLevel);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_pgbJobExp);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_gbxStatPoints);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_lblLevel);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_gbxMessageFilter);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_pgbExp);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_pgbMP);
			this.TabPageH_Character_Option01_Panel.Controls.Add(this.Character_pgbHP);
			this.TabPageH_Character_Option01_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Character_Option01_Panel.Name = "TabPageH_Character_Option01_Panel";
			this.TabPageH_Character_Option01_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Character_Option01_Panel.TabIndex = 10;
			this.TabPageH_Character_Option01_Panel.Visible = false;
			// 
			// Character_lstvBuffs
			// 
			this.Character_lstvBuffs.AllowDrop = true;
			this.Character_lstvBuffs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lstvBuffs.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Character_lstvBuffs.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_lstvBuffs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_lstvBuffs.FullRowSelect = true;
			this.Character_lstvBuffs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.Character_lstvBuffs.LargeImageList = this.lstimgIcons;
			this.Character_lstvBuffs.Location = new System.Drawing.Point(5, 235);
			this.Character_lstvBuffs.Margin = new System.Windows.Forms.Padding(0);
			this.Character_lstvBuffs.MultiSelect = false;
			this.Character_lstvBuffs.Name = "Character_lstvBuffs";
			this.Character_lstvBuffs.ShowItemToolTips = true;
			this.Character_lstvBuffs.Size = new System.Drawing.Size(340, 72);
			this.Character_lstvBuffs.TabIndex = 34;
			this.Character_lstvBuffs.TileSize = new System.Drawing.Size(24, 24);
			this.Character_lstvBuffs.UseCompatibleStateImageBehavior = false;
			this.Character_lstvBuffs.View = System.Windows.Forms.View.Tile;
			this.Character_lstvBuffs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Control_MouseClick);
			// 
			// Character_lblCoordY
			// 
			this.Character_lblCoordY.AutoEllipsis = true;
			this.Character_lblCoordY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lblCoordY.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblCoordY.Location = new System.Drawing.Point(228, 170);
			this.Character_lblCoordY.Name = "Character_lblCoordY";
			this.Character_lblCoordY.Size = new System.Drawing.Size(117, 20);
			this.Character_lblCoordY.TabIndex = 33;
			this.Character_lblCoordY.Tag = "Source Sans Pro";
			this.Character_lblCoordY.Text = "- - -";
			this.Character_lblCoordY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_lblCoordX
			// 
			this.Character_lblCoordX.AutoEllipsis = true;
			this.Character_lblCoordX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lblCoordX.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblCoordX.Location = new System.Drawing.Point(107, 170);
			this.Character_lblCoordX.Name = "Character_lblCoordX";
			this.Character_lblCoordX.Size = new System.Drawing.Size(117, 20);
			this.Character_lblCoordX.TabIndex = 32;
			this.Character_lblCoordX.Tag = "Source Sans Pro";
			this.Character_lblCoordX.Text = "- - -";
			this.Character_lblCoordX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_lblCoords
			// 
			this.Character_lblCoords.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblCoords.Location = new System.Drawing.Point(5, 170);
			this.Character_lblCoords.Name = "Character_lblCoords";
			this.Character_lblCoords.Size = new System.Drawing.Size(102, 20);
			this.Character_lblCoords.TabIndex = 31;
			this.Character_lblCoords.Tag = "Source Sans Pro";
			this.Character_lblCoords.Text = "Coordinates :";
			this.ToolTips.SetToolTip(this.Character_lblCoords, "Game position");
			// 
			// Character_lblLocation
			// 
			this.Character_lblLocation.AutoEllipsis = true;
			this.Character_lblLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lblLocation.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblLocation.Location = new System.Drawing.Point(83, 144);
			this.Character_lblLocation.Name = "Character_lblLocation";
			this.Character_lblLocation.Size = new System.Drawing.Size(262, 20);
			this.Character_lblLocation.TabIndex = 30;
			this.Character_lblLocation.Tag = "Source Sans Pro";
			this.Character_lblLocation.Text = "- - -";
			this.Character_lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_lblLocationText
			// 
			this.Character_lblLocationText.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblLocationText.Location = new System.Drawing.Point(5, 144);
			this.Character_lblLocationText.Name = "Character_lblLocationText";
			this.Character_lblLocationText.Size = new System.Drawing.Size(78, 20);
			this.Character_lblLocationText.TabIndex = 29;
			this.Character_lblLocationText.Tag = "Source Sans Pro";
			this.Character_lblLocationText.Text = "Location :";
			this.ToolTips.SetToolTip(this.Character_lblLocationText, "World location");
			// 
			// Character_lblSP
			// 
			this.Character_lblSP.AutoEllipsis = true;
			this.Character_lblSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lblSP.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblSP.Location = new System.Drawing.Point(234, 118);
			this.Character_lblSP.Name = "Character_lblSP";
			this.Character_lblSP.Size = new System.Drawing.Size(111, 20);
			this.Character_lblSP.TabIndex = 28;
			this.Character_lblSP.Tag = "Source Sans Pro";
			this.Character_lblSP.Text = "- - -";
			this.Character_lblSP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_lblSPText
			// 
			this.Character_lblSPText.AutoSize = true;
			this.Character_lblSPText.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblSPText.Location = new System.Drawing.Point(198, 118);
			this.Character_lblSPText.Name = "Character_lblSPText";
			this.Character_lblSPText.Size = new System.Drawing.Size(36, 20);
			this.Character_lblSPText.TabIndex = 27;
			this.Character_lblSPText.Tag = "Source Sans Pro";
			this.Character_lblSPText.Text = "SP :";
			this.ToolTips.SetToolTip(this.Character_lblSPText, "Skill Points");
			// 
			// Character_lblGold
			// 
			this.Character_lblGold.AutoEllipsis = true;
			this.Character_lblGold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lblGold.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblGold.Location = new System.Drawing.Point(55, 118);
			this.Character_lblGold.Name = "Character_lblGold";
			this.Character_lblGold.Size = new System.Drawing.Size(140, 20);
			this.Character_lblGold.TabIndex = 26;
			this.Character_lblGold.Tag = "Source Sans Pro";
			this.Character_lblGold.Text = "- - -";
			this.Character_lblGold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_lblGoldText
			// 
			this.Character_lblGoldText.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblGoldText.Location = new System.Drawing.Point(5, 118);
			this.Character_lblGoldText.Name = "Character_lblGoldText";
			this.Character_lblGoldText.Size = new System.Drawing.Size(50, 20);
			this.Character_lblGoldText.TabIndex = 25;
			this.Character_lblGoldText.Tag = "Source Sans Pro";
			this.Character_lblGoldText.Text = "Gold :";
			this.ToolTips.SetToolTip(this.Character_lblGoldText, "Gold at inventory");
			// 
			// Character_lblJobLevel
			// 
			this.Character_lblJobLevel.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblJobLevel.Location = new System.Drawing.Point(5, 90);
			this.Character_lblJobLevel.Name = "Character_lblJobLevel";
			this.Character_lblJobLevel.Size = new System.Drawing.Size(90, 22);
			this.Character_lblJobLevel.TabIndex = 24;
			this.Character_lblJobLevel.Tag = "Source Sans Pro";
			this.Character_lblJobLevel.Text = "Job Lv. - - -";
			this.ToolTips.SetToolTip(this.Character_lblJobLevel, "Job Level");
			// 
			// Character_gbxStatPoints
			// 
			this.Character_gbxStatPoints.Controls.Add(this.Character_lblStatPoints);
			this.Character_gbxStatPoints.Controls.Add(this.Character_lblINT);
			this.Character_gbxStatPoints.Controls.Add(this.Character_lblSTR);
			this.Character_gbxStatPoints.Controls.Add(this.Character_btnAddSTR);
			this.Character_gbxStatPoints.Controls.Add(this.Character_lblAddINT);
			this.Character_gbxStatPoints.Controls.Add(this.Character_lblAddSTR);
			this.Character_gbxStatPoints.Controls.Add(this.Character_btnAddINT);
			this.Character_gbxStatPoints.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_gbxStatPoints.ForeColor = System.Drawing.Color.LightGray;
			this.Character_gbxStatPoints.Location = new System.Drawing.Point(5, 190);
			this.Character_gbxStatPoints.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Character_gbxStatPoints.Name = "Character_gbxStatPoints";
			this.Character_gbxStatPoints.Size = new System.Drawing.Size(340, 44);
			this.Character_gbxStatPoints.TabIndex = 20;
			this.Character_gbxStatPoints.TabStop = false;
			this.Character_gbxStatPoints.Tag = "Source Sans Pro";
			this.Character_gbxStatPoints.Text = "Stat Points";
			// 
			// Character_lblStatPoints
			// 
			this.Character_lblStatPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_lblStatPoints.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblStatPoints.Location = new System.Drawing.Point(250, 15);
			this.Character_lblStatPoints.Name = "Character_lblStatPoints";
			this.Character_lblStatPoints.Size = new System.Drawing.Size(84, 22);
			this.Character_lblStatPoints.TabIndex = 23;
			this.Character_lblStatPoints.Tag = "Source Sans Pro";
			this.Character_lblStatPoints.Text = "- - -";
			this.Character_lblStatPoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ToolTips.SetToolTip(this.Character_lblStatPoints, "Stat points remain");
			// 
			// Character_lblINT
			// 
			this.Character_lblINT.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblINT.Location = new System.Drawing.Point(173, 15);
			this.Character_lblINT.Name = "Character_lblINT";
			this.Character_lblINT.Size = new System.Drawing.Size(45, 22);
			this.Character_lblINT.TabIndex = 24;
			this.Character_lblINT.Tag = "Source Sans Pro";
			this.Character_lblINT.Text = "- - -";
			this.Character_lblINT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_lblSTR
			// 
			this.Character_lblSTR.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblSTR.Location = new System.Drawing.Point(51, 15);
			this.Character_lblSTR.Name = "Character_lblSTR";
			this.Character_lblSTR.Size = new System.Drawing.Size(45, 22);
			this.Character_lblSTR.TabIndex = 23;
			this.Character_lblSTR.Tag = "Source Sans Pro";
			this.Character_lblSTR.Text = "- - -";
			this.Character_lblSTR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Character_btnAddSTR
			// 
			this.Character_btnAddSTR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Character_btnAddSTR.Enabled = false;
			this.Character_btnAddSTR.FlatAppearance.BorderSize = 0;
			this.Character_btnAddSTR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Character_btnAddSTR.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Character_btnAddSTR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_btnAddSTR.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_btnAddSTR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_btnAddSTR.Location = new System.Drawing.Point(96, 13);
			this.Character_btnAddSTR.Margin = new System.Windows.Forms.Padding(0);
			this.Character_btnAddSTR.Name = "Character_btnAddSTR";
			this.Character_btnAddSTR.Size = new System.Drawing.Size(28, 26);
			this.Character_btnAddSTR.TabIndex = 22;
			this.Character_btnAddSTR.Tag = "Font Awesome 5 Pro Regular";
			this.Character_btnAddSTR.Text = "";
			this.ToolTips.SetToolTip(this.Character_btnAddSTR, "Add STR");
			this.Character_btnAddSTR.UseCompatibleTextRendering = true;
			this.Character_btnAddSTR.UseVisualStyleBackColor = false;
			this.Character_btnAddSTR.Click += new System.EventHandler(this.Control_Click);
			this.Character_btnAddSTR.MouseEnter += new System.EventHandler(this.Control_Focus_Enter);
			this.Character_btnAddSTR.MouseLeave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Character_lblAddINT
			// 
			this.Character_lblAddINT.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblAddINT.Location = new System.Drawing.Point(128, 15);
			this.Character_lblAddINT.Name = "Character_lblAddINT";
			this.Character_lblAddINT.Size = new System.Drawing.Size(45, 22);
			this.Character_lblAddINT.TabIndex = 21;
			this.Character_lblAddINT.Tag = "Source Sans Pro";
			this.Character_lblAddINT.Text = "INT :";
			this.Character_lblAddINT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Character_lblAddINT, "Intelligence");
			// 
			// Character_lblAddSTR
			// 
			this.Character_lblAddSTR.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblAddSTR.Location = new System.Drawing.Point(6, 15);
			this.Character_lblAddSTR.Name = "Character_lblAddSTR";
			this.Character_lblAddSTR.Size = new System.Drawing.Size(45, 22);
			this.Character_lblAddSTR.TabIndex = 20;
			this.Character_lblAddSTR.Tag = "Source Sans Pro";
			this.Character_lblAddSTR.Text = "STR :";
			this.Character_lblAddSTR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ToolTips.SetToolTip(this.Character_lblAddSTR, "Strength");
			// 
			// Character_btnAddINT
			// 
			this.Character_btnAddINT.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Character_btnAddINT.Enabled = false;
			this.Character_btnAddINT.FlatAppearance.BorderSize = 0;
			this.Character_btnAddINT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Character_btnAddINT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Character_btnAddINT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_btnAddINT.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_btnAddINT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_btnAddINT.Location = new System.Drawing.Point(218, 13);
			this.Character_btnAddINT.Margin = new System.Windows.Forms.Padding(0);
			this.Character_btnAddINT.Name = "Character_btnAddINT";
			this.Character_btnAddINT.Size = new System.Drawing.Size(28, 26);
			this.Character_btnAddINT.TabIndex = 2;
			this.Character_btnAddINT.Tag = "Font Awesome 5 Pro Regular";
			this.Character_btnAddINT.Text = "";
			this.ToolTips.SetToolTip(this.Character_btnAddINT, "Add INT");
			this.Character_btnAddINT.UseCompatibleTextRendering = true;
			this.Character_btnAddINT.UseVisualStyleBackColor = false;
			this.Character_btnAddINT.Click += new System.EventHandler(this.Control_Click);
			this.Character_btnAddINT.MouseEnter += new System.EventHandler(this.Control_Focus_Enter);
			this.Character_btnAddINT.MouseLeave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Character_lblLevel
			// 
			this.Character_lblLevel.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.Character_lblLevel.Location = new System.Drawing.Point(5, 62);
			this.Character_lblLevel.Name = "Character_lblLevel";
			this.Character_lblLevel.Size = new System.Drawing.Size(55, 22);
			this.Character_lblLevel.TabIndex = 19;
			this.Character_lblLevel.Tag = "Source Sans Pro";
			this.Character_lblLevel.Text = "Lv. - - -";
			this.ToolTips.SetToolTip(this.Character_lblLevel, "Level");
			// 
			// Character_gbxMessageFilter
			// 
			this.Character_gbxMessageFilter.Controls.Add(this.Character_cbxMessageEvents);
			this.Character_gbxMessageFilter.Controls.Add(this.Character_cbxMessagePicks);
			this.Character_gbxMessageFilter.Controls.Add(this.Character_cbxMessageUniques);
			this.Character_gbxMessageFilter.Controls.Add(this.Character_cbxMessageExp);
			this.Character_gbxMessageFilter.Controls.Add(this.Character_rtbxMessageFilter);
			this.Character_gbxMessageFilter.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_gbxMessageFilter.ForeColor = System.Drawing.Color.LightGray;
			this.Character_gbxMessageFilter.Location = new System.Drawing.Point(350, 0);
			this.Character_gbxMessageFilter.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Character_gbxMessageFilter.Name = "Character_gbxMessageFilter";
			this.Character_gbxMessageFilter.Size = new System.Drawing.Size(300, 307);
			this.Character_gbxMessageFilter.TabIndex = 18;
			this.Character_gbxMessageFilter.TabStop = false;
			this.Character_gbxMessageFilter.Tag = "Source Sans Pro";
			this.Character_gbxMessageFilter.Text = "Message filtering";
			// 
			// Character_cbxMessageEvents
			// 
			this.Character_cbxMessageEvents.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxMessageEvents.FlatAppearance.BorderSize = 0;
			this.Character_cbxMessageEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxMessageEvents.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxMessageEvents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxMessageEvents.Location = new System.Drawing.Point(143, 14);
			this.Character_cbxMessageEvents.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxMessageEvents.Name = "Character_cbxMessageEvents";
			this.Character_cbxMessageEvents.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxMessageEvents.Size = new System.Drawing.Size(74, 24);
			this.Character_cbxMessageEvents.TabIndex = 22;
			this.Character_cbxMessageEvents.Tag = "Source Sans Pro";
			this.Character_cbxMessageEvents.Text = "Events";
			this.ToolTips.SetToolTip(this.Character_cbxMessageEvents, "Event notices");
			this.Character_cbxMessageEvents.UseVisualStyleBackColor = false;
			this.Character_cbxMessageEvents.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxMessagePicks
			// 
			this.Character_cbxMessagePicks.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxMessagePicks.FlatAppearance.BorderSize = 0;
			this.Character_cbxMessagePicks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxMessagePicks.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxMessagePicks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxMessagePicks.Location = new System.Drawing.Point(217, 14);
			this.Character_cbxMessagePicks.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxMessagePicks.Name = "Character_cbxMessagePicks";
			this.Character_cbxMessagePicks.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxMessagePicks.Size = new System.Drawing.Size(64, 24);
			this.Character_cbxMessagePicks.TabIndex = 21;
			this.Character_cbxMessagePicks.Tag = "Source Sans Pro";
			this.Character_cbxMessagePicks.Text = "Picks";
			this.ToolTips.SetToolTip(this.Character_cbxMessagePicks, "Picked Drops");
			this.Character_cbxMessagePicks.UseVisualStyleBackColor = false;
			this.Character_cbxMessagePicks.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxMessageUniques
			// 
			this.Character_cbxMessageUniques.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxMessageUniques.FlatAppearance.BorderSize = 0;
			this.Character_cbxMessageUniques.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxMessageUniques.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxMessageUniques.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxMessageUniques.Location = new System.Drawing.Point(60, 14);
			this.Character_cbxMessageUniques.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxMessageUniques.Name = "Character_cbxMessageUniques";
			this.Character_cbxMessageUniques.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxMessageUniques.Size = new System.Drawing.Size(83, 24);
			this.Character_cbxMessageUniques.TabIndex = 20;
			this.Character_cbxMessageUniques.Tag = "Source Sans Pro";
			this.Character_cbxMessageUniques.Text = "Uniques";
			this.ToolTips.SetToolTip(this.Character_cbxMessageUniques, "Unique spawns");
			this.Character_cbxMessageUniques.UseVisualStyleBackColor = false;
			this.Character_cbxMessageUniques.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxMessageExp
			// 
			this.Character_cbxMessageExp.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxMessageExp.FlatAppearance.BorderSize = 0;
			this.Character_cbxMessageExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxMessageExp.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxMessageExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxMessageExp.Location = new System.Drawing.Point(2, 14);
			this.Character_cbxMessageExp.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxMessageExp.Name = "Character_cbxMessageExp";
			this.Character_cbxMessageExp.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxMessageExp.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Character_cbxMessageExp.Size = new System.Drawing.Size(58, 24);
			this.Character_cbxMessageExp.TabIndex = 19;
			this.Character_cbxMessageExp.Tag = "Source Sans Pro";
			this.Character_cbxMessageExp.Text = "Exp.";
			this.ToolTips.SetToolTip(this.Character_cbxMessageExp, "Experience & SP Experience");
			this.Character_cbxMessageExp.UseVisualStyleBackColor = false;
			this.Character_cbxMessageExp.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// TabPageH_Character_Option02_Panel
			// 
			this.TabPageH_Character_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Character_Option02_Panel.Controls.Add(this.Character_gbxPotionPet);
			this.TabPageH_Character_Option02_Panel.Controls.Add(this.Character_gbxPotionsPlayer);
			this.TabPageH_Character_Option02_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Character_Option02_Panel.Name = "TabPageH_Character_Option02_Panel";
			this.TabPageH_Character_Option02_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Character_Option02_Panel.TabIndex = 11;
			this.TabPageH_Character_Option02_Panel.Visible = false;
			// 
			// Character_gbxPotionPet
			// 
			this.Character_gbxPotionPet.Controls.Add(this.Character_tbxUsePetHGP);
			this.Character_gbxPotionPet.Controls.Add(this.Character_cbxUsePetHGP);
			this.Character_gbxPotionPet.Controls.Add(this.Character_cbxUsePetsPill);
			this.Character_gbxPotionPet.Controls.Add(this.Character_tbxUseTransportHP);
			this.Character_gbxPotionPet.Controls.Add(this.Character_cbxUseTransportHP);
			this.Character_gbxPotionPet.Controls.Add(this.Character_tbxUsePetHP);
			this.Character_gbxPotionPet.Controls.Add(this.Character_cbxUsePetHP);
			this.Character_gbxPotionPet.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_gbxPotionPet.ForeColor = System.Drawing.Color.LightGray;
			this.Character_gbxPotionPet.Location = new System.Drawing.Point(182, 0);
			this.Character_gbxPotionPet.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Character_gbxPotionPet.Name = "Character_gbxPotionPet";
			this.Character_gbxPotionPet.Size = new System.Drawing.Size(216, 216);
			this.Character_gbxPotionPet.TabIndex = 23;
			this.Character_gbxPotionPet.TabStop = false;
			this.Character_gbxPotionPet.Tag = "Source Sans Pro";
			this.Character_gbxPotionPet.Text = "Pet";
			// 
			// Character_tbxUsePetHGP
			// 
			this.Character_tbxUsePetHGP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUsePetHGP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUsePetHGP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUsePetHGP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUsePetHGP.Location = new System.Drawing.Point(128, 89);
			this.Character_tbxUsePetHGP.MaxLength = 3;
			this.Character_tbxUsePetHGP.Name = "Character_tbxUsePetHGP";
			this.Character_tbxUsePetHGP.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUsePetHGP.TabIndex = 26;
			this.Character_tbxUsePetHGP.Tag = "Source Sans Pro";
			this.Character_tbxUsePetHGP.Text = "0";
			this.Character_tbxUsePetHGP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUsePetHGP.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_cbxUsePetHGP
			// 
			this.Character_cbxUsePetHGP.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUsePetHGP.FlatAppearance.BorderSize = 0;
			this.Character_cbxUsePetHGP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUsePetHGP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUsePetHGP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUsePetHGP.Location = new System.Drawing.Point(2, 89);
			this.Character_cbxUsePetHGP.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUsePetHGP.Name = "Character_cbxUsePetHGP";
			this.Character_cbxUsePetHGP.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUsePetHGP.Size = new System.Drawing.Size(212, 25);
			this.Character_cbxUsePetHGP.TabIndex = 25;
			this.Character_cbxUsePetHGP.Tag = "Source Sans Pro";
			this.Character_cbxUsePetHGP.Text = "Use HGP on pet               %";
			this.ToolTips.SetToolTip(this.Character_cbxUsePetHGP, "Recover starvation from pet when is less or equal than");
			this.Character_cbxUsePetHGP.UseVisualStyleBackColor = false;
			this.Character_cbxUsePetHGP.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUsePetsPill
			// 
			this.Character_cbxUsePetsPill.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUsePetsPill.FlatAppearance.BorderSize = 0;
			this.Character_cbxUsePetsPill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUsePetsPill.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUsePetsPill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUsePetsPill.Location = new System.Drawing.Point(2, 64);
			this.Character_cbxUsePetsPill.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUsePetsPill.Name = "Character_cbxUsePetsPill";
			this.Character_cbxUsePetsPill.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUsePetsPill.Size = new System.Drawing.Size(212, 25);
			this.Character_cbxUsePetsPill.TabIndex = 24;
			this.Character_cbxUsePetsPill.Tag = "Source Sans Pro";
			this.Character_cbxUsePetsPill.Text = "Use abnormal state pills";
			this.Character_cbxUsePetsPill.UseVisualStyleBackColor = false;
			this.Character_cbxUsePetsPill.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_tbxUseTransportHP
			// 
			this.Character_tbxUseTransportHP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUseTransportHP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUseTransportHP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUseTransportHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUseTransportHP.Location = new System.Drawing.Point(156, 39);
			this.Character_tbxUseTransportHP.MaxLength = 3;
			this.Character_tbxUseTransportHP.Name = "Character_tbxUseTransportHP";
			this.Character_tbxUseTransportHP.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUseTransportHP.TabIndex = 23;
			this.Character_tbxUseTransportHP.Tag = "Source Sans Pro";
			this.Character_tbxUseTransportHP.Text = "0";
			this.Character_tbxUseTransportHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUseTransportHP.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_cbxUseTransportHP
			// 
			this.Character_cbxUseTransportHP.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseTransportHP.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseTransportHP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseTransportHP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseTransportHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseTransportHP.Location = new System.Drawing.Point(2, 39);
			this.Character_cbxUseTransportHP.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseTransportHP.Name = "Character_cbxUseTransportHP";
			this.Character_cbxUseTransportHP.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseTransportHP.Size = new System.Drawing.Size(212, 25);
			this.Character_cbxUseTransportHP.TabIndex = 22;
			this.Character_cbxUseTransportHP.Tag = "Source Sans Pro";
			this.Character_cbxUseTransportHP.Text = "Use HP on transport               %";
			this.ToolTips.SetToolTip(this.Character_cbxUseTransportHP, "Use health potion when HP from transport is less or equal than");
			this.Character_cbxUseTransportHP.UseVisualStyleBackColor = false;
			this.Character_cbxUseTransportHP.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_tbxUsePetHP
			// 
			this.Character_tbxUsePetHP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUsePetHP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUsePetHP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUsePetHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUsePetHP.Location = new System.Drawing.Point(118, 14);
			this.Character_tbxUsePetHP.MaxLength = 3;
			this.Character_tbxUsePetHP.Name = "Character_tbxUsePetHP";
			this.Character_tbxUsePetHP.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUsePetHP.TabIndex = 21;
			this.Character_tbxUsePetHP.Tag = "Source Sans Pro";
			this.Character_tbxUsePetHP.Text = "0";
			this.Character_tbxUsePetHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUsePetHP.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_cbxUsePetHP
			// 
			this.Character_cbxUsePetHP.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUsePetHP.FlatAppearance.BorderSize = 0;
			this.Character_cbxUsePetHP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUsePetHP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUsePetHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUsePetHP.Location = new System.Drawing.Point(2, 14);
			this.Character_cbxUsePetHP.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUsePetHP.Name = "Character_cbxUsePetHP";
			this.Character_cbxUsePetHP.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUsePetHP.Size = new System.Drawing.Size(212, 25);
			this.Character_cbxUsePetHP.TabIndex = 20;
			this.Character_cbxUsePetHP.Tag = "Source Sans Pro";
			this.Character_cbxUsePetHP.Text = "Use HP on pet               %";
			this.ToolTips.SetToolTip(this.Character_cbxUsePetHP, "Use health potion when HP from pet is less or equal than");
			this.Character_cbxUsePetHP.UseVisualStyleBackColor = false;
			this.Character_cbxUsePetHP.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_gbxPotionsPlayer
			// 
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_tbxUseHP);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_tbxUseMP);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUsePillPurification);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUseMPGrain);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUsePillUniversal);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_tbxUseMPVigor);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_tbxUseHPVigor);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUseMPVigor);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUseHP);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUseHPGrain);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUseHPVigor);
			this.Character_gbxPotionsPlayer.Controls.Add(this.Character_cbxUseMP);
			this.Character_gbxPotionsPlayer.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_gbxPotionsPlayer.ForeColor = System.Drawing.Color.LightGray;
			this.Character_gbxPotionsPlayer.Location = new System.Drawing.Point(6, 0);
			this.Character_gbxPotionsPlayer.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
			this.Character_gbxPotionsPlayer.Name = "Character_gbxPotionsPlayer";
			this.Character_gbxPotionsPlayer.Size = new System.Drawing.Size(169, 216);
			this.Character_gbxPotionsPlayer.TabIndex = 22;
			this.Character_gbxPotionsPlayer.TabStop = false;
			this.Character_gbxPotionsPlayer.Tag = "Source Sans Pro";
			this.Character_gbxPotionsPlayer.Text = "Player";
			// 
			// Character_tbxUseHP
			// 
			this.Character_tbxUseHP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUseHP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUseHP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUseHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUseHP.Location = new System.Drawing.Point(77, 14);
			this.Character_tbxUseHP.MaxLength = 3;
			this.Character_tbxUseHP.Name = "Character_tbxUseHP";
			this.Character_tbxUseHP.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUseHP.TabIndex = 9;
			this.Character_tbxUseHP.Tag = "Source Sans Pro";
			this.Character_tbxUseHP.Text = "0";
			this.Character_tbxUseHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUseHP.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_tbxUseMP
			// 
			this.Character_tbxUseMP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUseMP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUseMP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUseMP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUseMP.Location = new System.Drawing.Point(77, 64);
			this.Character_tbxUseMP.MaxLength = 3;
			this.Character_tbxUseMP.Name = "Character_tbxUseMP";
			this.Character_tbxUseMP.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUseMP.TabIndex = 12;
			this.Character_tbxUseMP.Tag = "Source Sans Pro";
			this.Character_tbxUseMP.Text = "0";
			this.Character_tbxUseMP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUseMP.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_cbxUsePillPurification
			// 
			this.Character_cbxUsePillPurification.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUsePillPurification.FlatAppearance.BorderSize = 0;
			this.Character_cbxUsePillPurification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUsePillPurification.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUsePillPurification.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUsePillPurification.Location = new System.Drawing.Point(2, 189);
			this.Character_cbxUsePillPurification.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUsePillPurification.Name = "Character_cbxUsePillPurification";
			this.Character_cbxUsePillPurification.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUsePillPurification.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUsePillPurification.TabIndex = 19;
			this.Character_cbxUsePillPurification.Tag = "Source Sans Pro";
			this.Character_cbxUsePillPurification.Text = "Use purification pills";
			this.Character_cbxUsePillPurification.UseVisualStyleBackColor = false;
			this.Character_cbxUsePillPurification.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUseMPGrain
			// 
			this.Character_cbxUseMPGrain.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseMPGrain.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseMPGrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseMPGrain.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseMPGrain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseMPGrain.Location = new System.Drawing.Point(2, 89);
			this.Character_cbxUseMPGrain.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseMPGrain.Name = "Character_cbxUseMPGrain";
			this.Character_cbxUseMPGrain.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseMPGrain.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUseMPGrain.TabIndex = 13;
			this.Character_cbxUseMPGrain.Tag = "Source Sans Pro";
			this.Character_cbxUseMPGrain.Text = "Use MP grains";
			this.ToolTips.SetToolTip(this.Character_cbxUseMPGrain, "Use mana potion grain if is available");
			this.Character_cbxUseMPGrain.UseVisualStyleBackColor = false;
			this.Character_cbxUseMPGrain.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUsePillUniversal
			// 
			this.Character_cbxUsePillUniversal.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUsePillUniversal.FlatAppearance.BorderSize = 0;
			this.Character_cbxUsePillUniversal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUsePillUniversal.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUsePillUniversal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUsePillUniversal.Location = new System.Drawing.Point(2, 164);
			this.Character_cbxUsePillUniversal.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUsePillUniversal.Name = "Character_cbxUsePillUniversal";
			this.Character_cbxUsePillUniversal.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUsePillUniversal.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUsePillUniversal.TabIndex = 18;
			this.Character_cbxUsePillUniversal.Tag = "Source Sans Pro";
			this.Character_cbxUsePillUniversal.Text = "Use universal pills";
			this.Character_cbxUsePillUniversal.UseVisualStyleBackColor = false;
			this.Character_cbxUsePillUniversal.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_tbxUseMPVigor
			// 
			this.Character_tbxUseMPVigor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUseMPVigor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUseMPVigor.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUseMPVigor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUseMPVigor.Location = new System.Drawing.Point(108, 139);
			this.Character_tbxUseMPVigor.MaxLength = 3;
			this.Character_tbxUseMPVigor.Name = "Character_tbxUseMPVigor";
			this.Character_tbxUseMPVigor.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUseMPVigor.TabIndex = 17;
			this.Character_tbxUseMPVigor.Tag = "Source Sans Pro";
			this.Character_tbxUseMPVigor.Text = "0";
			this.Character_tbxUseMPVigor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUseMPVigor.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_tbxUseHPVigor
			// 
			this.Character_tbxUseHPVigor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Character_tbxUseHPVigor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_tbxUseHPVigor.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Character_tbxUseHPVigor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_tbxUseHPVigor.Location = new System.Drawing.Point(108, 114);
			this.Character_tbxUseHPVigor.MaxLength = 3;
			this.Character_tbxUseHPVigor.Name = "Character_tbxUseHPVigor";
			this.Character_tbxUseHPVigor.Size = new System.Drawing.Size(35, 25);
			this.Character_tbxUseHPVigor.TabIndex = 15;
			this.Character_tbxUseHPVigor.Tag = "Source Sans Pro";
			this.Character_tbxUseHPVigor.Text = "0";
			this.Character_tbxUseHPVigor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Character_tbxUseHPVigor.TextChanged += new System.EventHandler(this.Control_TextChanged);
			// 
			// Character_cbxUseMPVigor
			// 
			this.Character_cbxUseMPVigor.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseMPVigor.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseMPVigor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseMPVigor.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseMPVigor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseMPVigor.Location = new System.Drawing.Point(2, 139);
			this.Character_cbxUseMPVigor.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseMPVigor.Name = "Character_cbxUseMPVigor";
			this.Character_cbxUseMPVigor.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseMPVigor.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUseMPVigor.TabIndex = 16;
			this.Character_cbxUseMPVigor.Tag = "Source Sans Pro";
			this.Character_cbxUseMPVigor.Text = "Use vigor MP              %";
			this.ToolTips.SetToolTip(this.Character_cbxUseMPVigor, "Use vigor potion when MP less or equal than");
			this.Character_cbxUseMPVigor.UseVisualStyleBackColor = false;
			this.Character_cbxUseMPVigor.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUseHP
			// 
			this.Character_cbxUseHP.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseHP.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseHP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseHP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseHP.Location = new System.Drawing.Point(2, 14);
			this.Character_cbxUseHP.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseHP.Name = "Character_cbxUseHP";
			this.Character_cbxUseHP.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseHP.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUseHP.TabIndex = 8;
			this.Character_cbxUseHP.Tag = "Source Sans Pro";
			this.Character_cbxUseHP.Text = "Use HP               %";
			this.ToolTips.SetToolTip(this.Character_cbxUseHP, "Use health potion when HP less or equal than");
			this.Character_cbxUseHP.UseVisualStyleBackColor = false;
			this.Character_cbxUseHP.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUseHPGrain
			// 
			this.Character_cbxUseHPGrain.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseHPGrain.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseHPGrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseHPGrain.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseHPGrain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseHPGrain.Location = new System.Drawing.Point(2, 39);
			this.Character_cbxUseHPGrain.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseHPGrain.Name = "Character_cbxUseHPGrain";
			this.Character_cbxUseHPGrain.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseHPGrain.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUseHPGrain.TabIndex = 10;
			this.Character_cbxUseHPGrain.Tag = "Source Sans Pro";
			this.Character_cbxUseHPGrain.Text = "Use HP grains";
			this.ToolTips.SetToolTip(this.Character_cbxUseHPGrain, "Use health potion grain if is available");
			this.Character_cbxUseHPGrain.UseVisualStyleBackColor = false;
			this.Character_cbxUseHPGrain.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUseHPVigor
			// 
			this.Character_cbxUseHPVigor.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseHPVigor.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseHPVigor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseHPVigor.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseHPVigor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseHPVigor.Location = new System.Drawing.Point(2, 114);
			this.Character_cbxUseHPVigor.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseHPVigor.Name = "Character_cbxUseHPVigor";
			this.Character_cbxUseHPVigor.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseHPVigor.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUseHPVigor.TabIndex = 14;
			this.Character_cbxUseHPVigor.Tag = "Source Sans Pro";
			this.Character_cbxUseHPVigor.Text = "Use vigor HP              %";
			this.ToolTips.SetToolTip(this.Character_cbxUseHPVigor, "Use vigor potion when HP less or equal than");
			this.Character_cbxUseHPVigor.UseVisualStyleBackColor = false;
			this.Character_cbxUseHPVigor.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxUseMP
			// 
			this.Character_cbxUseMP.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxUseMP.FlatAppearance.BorderSize = 0;
			this.Character_cbxUseMP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxUseMP.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxUseMP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxUseMP.Location = new System.Drawing.Point(2, 64);
			this.Character_cbxUseMP.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxUseMP.Name = "Character_cbxUseMP";
			this.Character_cbxUseMP.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxUseMP.Size = new System.Drawing.Size(165, 25);
			this.Character_cbxUseMP.TabIndex = 11;
			this.Character_cbxUseMP.Tag = "Source Sans Pro";
			this.Character_cbxUseMP.Text = "Use MP               %";
			this.ToolTips.SetToolTip(this.Character_cbxUseMP, "Use mana potion when MP less or equal than");
			this.Character_cbxUseMP.UseVisualStyleBackColor = false;
			this.Character_cbxUseMP.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// TabPageH_Character_Option04_Panel
			// 
			this.TabPageH_Character_Option04_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Character_Option04_Panel.Controls.Add(this.Character_cbxAcceptRessPartyOnly);
			this.TabPageH_Character_Option04_Panel.Controls.Add(this.Character_cbxAcceptRess);
			this.TabPageH_Character_Option04_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Character_Option04_Panel.Name = "TabPageH_Character_Option04_Panel";
			this.TabPageH_Character_Option04_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Character_Option04_Panel.TabIndex = 12;
			this.TabPageH_Character_Option04_Panel.Visible = false;
			// 
			// Character_cbxAcceptRessPartyOnly
			// 
			this.Character_cbxAcceptRessPartyOnly.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxAcceptRessPartyOnly.FlatAppearance.BorderSize = 0;
			this.Character_cbxAcceptRessPartyOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxAcceptRessPartyOnly.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxAcceptRessPartyOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxAcceptRessPartyOnly.Location = new System.Drawing.Point(6, 25);
			this.Character_cbxAcceptRessPartyOnly.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxAcceptRessPartyOnly.Name = "Character_cbxAcceptRessPartyOnly";
			this.Character_cbxAcceptRessPartyOnly.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxAcceptRessPartyOnly.Size = new System.Drawing.Size(172, 25);
			this.Character_cbxAcceptRessPartyOnly.TabIndex = 10;
			this.Character_cbxAcceptRessPartyOnly.Tag = "Source Sans Pro";
			this.Character_cbxAcceptRessPartyOnly.Text = "Only of party members";
			this.Character_cbxAcceptRessPartyOnly.UseVisualStyleBackColor = false;
			this.Character_cbxAcceptRessPartyOnly.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// Character_cbxAcceptRess
			// 
			this.Character_cbxAcceptRess.Cursor = System.Windows.Forms.Cursors.Default;
			this.Character_cbxAcceptRess.FlatAppearance.BorderSize = 0;
			this.Character_cbxAcceptRess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Character_cbxAcceptRess.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Character_cbxAcceptRess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_cbxAcceptRess.Location = new System.Drawing.Point(0, 0);
			this.Character_cbxAcceptRess.Margin = new System.Windows.Forms.Padding(0);
			this.Character_cbxAcceptRess.Name = "Character_cbxAcceptRess";
			this.Character_cbxAcceptRess.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.Character_cbxAcceptRess.Size = new System.Drawing.Size(150, 25);
			this.Character_cbxAcceptRess.TabIndex = 9;
			this.Character_cbxAcceptRess.Tag = "Source Sans Pro";
			this.Character_cbxAcceptRess.Text = "Accept resurrection";
			this.Character_cbxAcceptRess.UseVisualStyleBackColor = false;
			this.Character_cbxAcceptRess.CheckedChanged += new System.EventHandler(this.Control_CheckedChanged);
			// 
			// TabPageH_Character_Option03_Panel
			// 
			this.TabPageH_Character_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Character_Option03_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Character_Option03_Panel.Name = "TabPageH_Character_Option03_Panel";
			this.TabPageH_Character_Option03_Panel.Size = new System.Drawing.Size(657, 315);
			this.TabPageH_Character_Option03_Panel.TabIndex = 13;
			this.TabPageH_Character_Option03_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option13_Panel
			// 
			this.TabPageV_Control01_Option13_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_cbxOthers);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_cbxPet);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_cbxDrop);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_cbxMob);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_cbxPlayer);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_btnRefresh);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_tbxServerTime);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_lblServerTime);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_lstrObjects);
			this.TabPageV_Control01_Option13_Panel.Controls.Add(this.GameInfo_cbxNPC);
			this.TabPageV_Control01_Option13_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option13_Panel.Name = "TabPageV_Control01_Option13_Panel";
			this.TabPageV_Control01_Option13_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option13_Panel.TabIndex = 20;
			this.TabPageV_Control01_Option13_Panel.Visible = false;
			// 
			// GameInfo_cbxOthers
			// 
			this.GameInfo_cbxOthers.Cursor = System.Windows.Forms.Cursors.Default;
			this.GameInfo_cbxOthers.FlatAppearance.BorderSize = 0;
			this.GameInfo_cbxOthers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_cbxOthers.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_cbxOthers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_cbxOthers.Location = new System.Drawing.Point(552, 311);
			this.GameInfo_cbxOthers.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_cbxOthers.Name = "GameInfo_cbxOthers";
			this.GameInfo_cbxOthers.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.GameInfo_cbxOthers.Size = new System.Drawing.Size(70, 25);
			this.GameInfo_cbxOthers.TabIndex = 11;
			this.GameInfo_cbxOthers.Tag = "Source Sans Pro";
			this.GameInfo_cbxOthers.Text = "Others";
			this.GameInfo_cbxOthers.UseVisualStyleBackColor = false;
			// 
			// GameInfo_cbxPet
			// 
			this.GameInfo_cbxPet.Cursor = System.Windows.Forms.Cursors.Default;
			this.GameInfo_cbxPet.FlatAppearance.BorderSize = 0;
			this.GameInfo_cbxPet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_cbxPet.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_cbxPet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_cbxPet.Location = new System.Drawing.Point(331, 311);
			this.GameInfo_cbxPet.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_cbxPet.Name = "GameInfo_cbxPet";
			this.GameInfo_cbxPet.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.GameInfo_cbxPet.Size = new System.Drawing.Size(50, 25);
			this.GameInfo_cbxPet.TabIndex = 10;
			this.GameInfo_cbxPet.Tag = "Source Sans Pro";
			this.GameInfo_cbxPet.Text = "Pet";
			this.GameInfo_cbxPet.UseMnemonic = false;
			this.GameInfo_cbxPet.UseVisualStyleBackColor = false;
			// 
			// GameInfo_cbxDrop
			// 
			this.GameInfo_cbxDrop.Cursor = System.Windows.Forms.Cursors.Default;
			this.GameInfo_cbxDrop.FlatAppearance.BorderSize = 0;
			this.GameInfo_cbxDrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_cbxDrop.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_cbxDrop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_cbxDrop.Location = new System.Drawing.Point(492, 311);
			this.GameInfo_cbxDrop.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_cbxDrop.Name = "GameInfo_cbxDrop";
			this.GameInfo_cbxDrop.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.GameInfo_cbxDrop.Size = new System.Drawing.Size(60, 25);
			this.GameInfo_cbxDrop.TabIndex = 8;
			this.GameInfo_cbxDrop.Tag = "Source Sans Pro";
			this.GameInfo_cbxDrop.Text = "Drop";
			this.GameInfo_cbxDrop.UseVisualStyleBackColor = false;
			// 
			// GameInfo_cbxMob
			// 
			this.GameInfo_cbxMob.Cursor = System.Windows.Forms.Cursors.Default;
			this.GameInfo_cbxMob.FlatAppearance.BorderSize = 0;
			this.GameInfo_cbxMob.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_cbxMob.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_cbxMob.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_cbxMob.Location = new System.Drawing.Point(381, 311);
			this.GameInfo_cbxMob.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_cbxMob.Name = "GameInfo_cbxMob";
			this.GameInfo_cbxMob.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.GameInfo_cbxMob.Size = new System.Drawing.Size(56, 25);
			this.GameInfo_cbxMob.TabIndex = 7;
			this.GameInfo_cbxMob.Tag = "Source Sans Pro";
			this.GameInfo_cbxMob.Text = "Mob";
			this.GameInfo_cbxMob.UseVisualStyleBackColor = false;
			// 
			// GameInfo_cbxPlayer
			// 
			this.GameInfo_cbxPlayer.Cursor = System.Windows.Forms.Cursors.Default;
			this.GameInfo_cbxPlayer.FlatAppearance.BorderSize = 0;
			this.GameInfo_cbxPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_cbxPlayer.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_cbxPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_cbxPlayer.Location = new System.Drawing.Point(263, 311);
			this.GameInfo_cbxPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_cbxPlayer.Name = "GameInfo_cbxPlayer";
			this.GameInfo_cbxPlayer.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.GameInfo_cbxPlayer.Size = new System.Drawing.Size(68, 25);
			this.GameInfo_cbxPlayer.TabIndex = 6;
			this.GameInfo_cbxPlayer.Tag = "Source Sans Pro";
			this.GameInfo_cbxPlayer.Text = "Player";
			this.GameInfo_cbxPlayer.UseVisualStyleBackColor = false;
			// 
			// GameInfo_btnRefresh
			// 
			this.GameInfo_btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.GameInfo_btnRefresh.FlatAppearance.BorderSize = 0;
			this.GameInfo_btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.GameInfo_btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.GameInfo_btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_btnRefresh.Font = new System.Drawing.Font("Font Awesome 5 Pro Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.GameInfo_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_btnRefresh.Location = new System.Drawing.Point(622, 307);
			this.GameInfo_btnRefresh.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_btnRefresh.Name = "GameInfo_btnRefresh";
			this.GameInfo_btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.GameInfo_btnRefresh.Size = new System.Drawing.Size(33, 33);
			this.GameInfo_btnRefresh.TabIndex = 5;
			this.GameInfo_btnRefresh.Tag = "Font Awesome 5 Pro Light";
			this.GameInfo_btnRefresh.Text = "";
			this.ToolTips.SetToolTip(this.GameInfo_btnRefresh, "Refresh");
			this.GameInfo_btnRefresh.UseCompatibleTextRendering = true;
			this.GameInfo_btnRefresh.UseVisualStyleBackColor = false;
			this.GameInfo_btnRefresh.Click += new System.EventHandler(this.Control_Click);
			// 
			// GameInfo_tbxServerTime
			// 
			this.GameInfo_tbxServerTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.GameInfo_tbxServerTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.GameInfo_tbxServerTime.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_tbxServerTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_tbxServerTime.Location = new System.Drawing.Point(91, 311);
			this.GameInfo_tbxServerTime.Name = "GameInfo_tbxServerTime";
			this.GameInfo_tbxServerTime.ReadOnly = true;
			this.GameInfo_tbxServerTime.Size = new System.Drawing.Size(172, 25);
			this.GameInfo_tbxServerTime.TabIndex = 4;
			this.GameInfo_tbxServerTime.Tag = "Source Sans Pro";
			this.GameInfo_tbxServerTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.GameInfo_tbxServerTime.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.GameInfo_tbxServerTime.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// GameInfo_lblServerTime
			// 
			this.GameInfo_lblServerTime.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_lblServerTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_lblServerTime.Location = new System.Drawing.Point(4, 311);
			this.GameInfo_lblServerTime.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_lblServerTime.Name = "GameInfo_lblServerTime";
			this.GameInfo_lblServerTime.Size = new System.Drawing.Size(87, 25);
			this.GameInfo_lblServerTime.TabIndex = 3;
			this.GameInfo_lblServerTime.Tag = "Source Sans Pro";
			this.GameInfo_lblServerTime.Text = "ServerTime";
			this.GameInfo_lblServerTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GameInfo_lstrObjects
			// 
			this.GameInfo_lstrObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.GameInfo_lstrObjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.GameInfo_lstrObjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_lstrObjects.FullRowSelect = true;
			this.GameInfo_lstrObjects.HideSelection = false;
			this.GameInfo_lstrObjects.Location = new System.Drawing.Point(-1, -1);
			this.GameInfo_lstrObjects.Name = "GameInfo_lstrObjects";
			this.GameInfo_lstrObjects.Size = new System.Drawing.Size(657, 308);
			this.GameInfo_lstrObjects.TabIndex = 2;
			// 
			// GameInfo_cbxNPC
			// 
			this.GameInfo_cbxNPC.Cursor = System.Windows.Forms.Cursors.Default;
			this.GameInfo_cbxNPC.FlatAppearance.BorderSize = 0;
			this.GameInfo_cbxNPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.GameInfo_cbxNPC.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.GameInfo_cbxNPC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.GameInfo_cbxNPC.Location = new System.Drawing.Point(437, 311);
			this.GameInfo_cbxNPC.Margin = new System.Windows.Forms.Padding(0);
			this.GameInfo_cbxNPC.Name = "GameInfo_cbxNPC";
			this.GameInfo_cbxNPC.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.GameInfo_cbxNPC.Size = new System.Drawing.Size(55, 25);
			this.GameInfo_cbxNPC.TabIndex = 9;
			this.GameInfo_cbxNPC.Tag = "Source Sans Pro";
			this.GameInfo_cbxNPC.Text = "NPC";
			this.GameInfo_cbxNPC.UseMnemonic = false;
			this.GameInfo_cbxNPC.UseVisualStyleBackColor = false;
			// 
			// TabPageV_Control01_Option11_Panel
			// 
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option01_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.Chat_panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option08_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option07_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option06_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option05_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option04_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option03_Panel);
			this.TabPageV_Control01_Option11_Panel.Controls.Add(this.TabPageH_Chat_Option02_Panel);
			this.TabPageV_Control01_Option11_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option11_Panel.Name = "TabPageV_Control01_Option11_Panel";
			this.TabPageV_Control01_Option11_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option11_Panel.TabIndex = 19;
			this.TabPageV_Control01_Option11_Panel.Visible = false;
			// 
			// TabPageH_Chat
			// 
			this.TabPageH_Chat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option08);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option07);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option06);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option05);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option04);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option03);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option02);
			this.TabPageH_Chat.Controls.Add(this.TabPageH_Chat_Option01);
			this.TabPageH_Chat.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Chat.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat.Name = "TabPageH_Chat";
			this.TabPageH_Chat.Size = new System.Drawing.Size(657, 28);
			this.TabPageH_Chat.TabIndex = 7;
			// 
			// TabPageH_Chat_Option08
			// 
			this.TabPageH_Chat_Option08.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option08.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option08.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option08.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option08.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option08.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option08.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option08.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option08.Location = new System.Drawing.Point(575, 0);
			this.TabPageH_Chat_Option08.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option08.Name = "TabPageH_Chat_Option08";
			this.TabPageH_Chat_Option08.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option08.TabIndex = 19;
			this.TabPageH_Chat_Option08.Text = "Global";
			this.TabPageH_Chat_Option08.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option08.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option07
			// 
			this.TabPageH_Chat_Option07.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option07.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option07.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option07.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option07.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option07.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option07.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option07.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option07.Location = new System.Drawing.Point(493, 0);
			this.TabPageH_Chat_Option07.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option07.Name = "TabPageH_Chat_Option07";
			this.TabPageH_Chat_Option07.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option07.TabIndex = 18;
			this.TabPageH_Chat_Option07.Text = "Stall";
			this.TabPageH_Chat_Option07.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option07.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option06
			// 
			this.TabPageH_Chat_Option06.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option06.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option06.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option06.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option06.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option06.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option06.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option06.Location = new System.Drawing.Point(411, 0);
			this.TabPageH_Chat_Option06.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option06.Name = "TabPageH_Chat_Option06";
			this.TabPageH_Chat_Option06.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option06.TabIndex = 17;
			this.TabPageH_Chat_Option06.Text = "Academy";
			this.TabPageH_Chat_Option06.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option06.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option05
			// 
			this.TabPageH_Chat_Option05.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option05.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option05.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option05.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option05.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option05.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option05.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option05.Location = new System.Drawing.Point(329, 0);
			this.TabPageH_Chat_Option05.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option05.Name = "TabPageH_Chat_Option05";
			this.TabPageH_Chat_Option05.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option05.TabIndex = 16;
			this.TabPageH_Chat_Option05.Text = "Union";
			this.TabPageH_Chat_Option05.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option05.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option04
			// 
			this.TabPageH_Chat_Option04.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option04.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option04.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option04.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option04.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option04.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option04.Location = new System.Drawing.Point(247, 0);
			this.TabPageH_Chat_Option04.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option04.Name = "TabPageH_Chat_Option04";
			this.TabPageH_Chat_Option04.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option04.TabIndex = 15;
			this.TabPageH_Chat_Option04.Text = "Guild";
			this.TabPageH_Chat_Option04.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option04.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option03
			// 
			this.TabPageH_Chat_Option03.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option03.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option03.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option03.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option03.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option03.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option03.Location = new System.Drawing.Point(165, 0);
			this.TabPageH_Chat_Option03.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option03.Name = "TabPageH_Chat_Option03";
			this.TabPageH_Chat_Option03.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option03.TabIndex = 14;
			this.TabPageH_Chat_Option03.Text = "Party";
			this.TabPageH_Chat_Option03.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option03.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option02
			// 
			this.TabPageH_Chat_Option02.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option02.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option02.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option02.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option02.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option02.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option02.Location = new System.Drawing.Point(83, 0);
			this.TabPageH_Chat_Option02.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option02.Name = "TabPageH_Chat_Option02";
			this.TabPageH_Chat_Option02.Size = new System.Drawing.Size(82, 26);
			this.TabPageH_Chat_Option02.TabIndex = 13;
			this.TabPageH_Chat_Option02.Text = "Private";
			this.TabPageH_Chat_Option02.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option02.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option01
			// 
			this.TabPageH_Chat_Option01.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TabPageH_Chat_Option01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.TabPageH_Chat_Option01.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option01.FlatAppearance.BorderSize = 0;
			this.TabPageH_Chat_Option01.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.TabPageH_Chat_Option01.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
			this.TabPageH_Chat_Option01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TabPageH_Chat_Option01.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.TabPageH_Chat_Option01.Location = new System.Drawing.Point(0, 0);
			this.TabPageH_Chat_Option01.Margin = new System.Windows.Forms.Padding(0);
			this.TabPageH_Chat_Option01.Name = "TabPageH_Chat_Option01";
			this.TabPageH_Chat_Option01.Size = new System.Drawing.Size(83, 26);
			this.TabPageH_Chat_Option01.TabIndex = 12;
			this.TabPageH_Chat_Option01.Text = "All";
			this.TabPageH_Chat_Option01.UseVisualStyleBackColor = false;
			this.TabPageH_Chat_Option01.Click += new System.EventHandler(this.TabPageH_ChatOption_Click);
			// 
			// TabPageH_Chat_Option01_Panel
			// 
			this.TabPageH_Chat_Option01_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option01_Panel.Controls.Add(this.Chat_rtbxAll);
			this.TabPageH_Chat_Option01_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option01_Panel.Name = "TabPageH_Chat_Option01_Panel";
			this.TabPageH_Chat_Option01_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option01_Panel.TabIndex = 8;
			this.TabPageH_Chat_Option01_Panel.Visible = false;
			// 
			// Chat_panel
			// 
			this.Chat_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_panel.Controls.Add(this.Chat_tbxMsgPlayer);
			this.Chat_panel.Controls.Add(this.Chat_cmbxMsgType);
			this.Chat_panel.Controls.Add(this.Chat_tbxMsg);
			this.Chat_panel.Location = new System.Drawing.Point(0, 308);
			this.Chat_panel.Margin = new System.Windows.Forms.Padding(0);
			this.Chat_panel.Name = "Chat_panel";
			this.Chat_panel.Size = new System.Drawing.Size(657, 34);
			this.Chat_panel.TabIndex = 17;
			// 
			// Chat_tbxMsgPlayer
			// 
			this.Chat_tbxMsgPlayer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.Chat_tbxMsgPlayer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.Chat_tbxMsgPlayer.Enabled = false;
			this.Chat_tbxMsgPlayer.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Chat_tbxMsgPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
			this.Chat_tbxMsgPlayer.Location = new System.Drawing.Point(90, 4);
			this.Chat_tbxMsgPlayer.Margin = new System.Windows.Forms.Padding(0);
			this.Chat_tbxMsgPlayer.MaxLength = 16;
			this.Chat_tbxMsgPlayer.Name = "Chat_tbxMsgPlayer";
			this.Chat_tbxMsgPlayer.Size = new System.Drawing.Size(100, 24);
			this.Chat_tbxMsgPlayer.TabIndex = 17;
			// 
			// Chat_cmbxMsgType
			// 
			this.Chat_cmbxMsgType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Chat_cmbxMsgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Chat_cmbxMsgType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Chat_cmbxMsgType.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Chat_cmbxMsgType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_cmbxMsgType.FormattingEnabled = true;
			this.Chat_cmbxMsgType.Items.AddRange(new object[] {
            "All",
            "Private",
            "Party",
            "Guild",
            "Union",
            "Academy",
            "Stall"});
			this.Chat_cmbxMsgType.Location = new System.Drawing.Point(5, 4);
			this.Chat_cmbxMsgType.Margin = new System.Windows.Forms.Padding(0);
			this.Chat_cmbxMsgType.MaxDropDownItems = 5;
			this.Chat_cmbxMsgType.Name = "Chat_cmbxMsgType";
			this.Chat_cmbxMsgType.Size = new System.Drawing.Size(80, 25);
			this.Chat_cmbxMsgType.TabIndex = 16;
			this.Chat_cmbxMsgType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
			// 
			// Chat_tbxMsg
			// 
			this.Chat_tbxMsg.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Chat_tbxMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
			this.Chat_tbxMsg.Location = new System.Drawing.Point(195, 4);
			this.Chat_tbxMsg.Margin = new System.Windows.Forms.Padding(0);
			this.Chat_tbxMsg.MaxLength = 250;
			this.Chat_tbxMsg.Name = "Chat_tbxMsg";
			this.Chat_tbxMsg.Size = new System.Drawing.Size(455, 24);
			this.Chat_tbxMsg.TabIndex = 1;
			this.ToolTips.SetToolTip(this.Chat_tbxMsg, "Press ENTER to send");
			this.Chat_tbxMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
			// 
			// TabPageH_Chat_Option08_Panel
			// 
			this.TabPageH_Chat_Option08_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option08_Panel.Controls.Add(this.Chat_rtbxGlobal);
			this.TabPageH_Chat_Option08_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option08_Panel.Name = "TabPageH_Chat_Option08_Panel";
			this.TabPageH_Chat_Option08_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option08_Panel.TabIndex = 12;
			this.TabPageH_Chat_Option08_Panel.Visible = false;
			// 
			// TabPageH_Chat_Option07_Panel
			// 
			this.TabPageH_Chat_Option07_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option07_Panel.Controls.Add(this.Chat_rtbxStall);
			this.TabPageH_Chat_Option07_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option07_Panel.Name = "TabPageH_Chat_Option07_Panel";
			this.TabPageH_Chat_Option07_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option07_Panel.TabIndex = 15;
			this.TabPageH_Chat_Option07_Panel.Visible = false;
			// 
			// TabPageH_Chat_Option06_Panel
			// 
			this.TabPageH_Chat_Option06_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option06_Panel.Controls.Add(this.Chat_rtbxAcademy);
			this.TabPageH_Chat_Option06_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option06_Panel.Name = "TabPageH_Chat_Option06_Panel";
			this.TabPageH_Chat_Option06_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option06_Panel.TabIndex = 13;
			this.TabPageH_Chat_Option06_Panel.Visible = false;
			// 
			// TabPageH_Chat_Option05_Panel
			// 
			this.TabPageH_Chat_Option05_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option05_Panel.Controls.Add(this.Chat_rtbxUnion);
			this.TabPageH_Chat_Option05_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option05_Panel.Name = "TabPageH_Chat_Option05_Panel";
			this.TabPageH_Chat_Option05_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option05_Panel.TabIndex = 14;
			this.TabPageH_Chat_Option05_Panel.Visible = false;
			// 
			// TabPageH_Chat_Option04_Panel
			// 
			this.TabPageH_Chat_Option04_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option04_Panel.Controls.Add(this.Chat_rtbxGuild);
			this.TabPageH_Chat_Option04_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option04_Panel.Name = "TabPageH_Chat_Option04_Panel";
			this.TabPageH_Chat_Option04_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option04_Panel.TabIndex = 10;
			this.TabPageH_Chat_Option04_Panel.Visible = false;
			// 
			// TabPageH_Chat_Option03_Panel
			// 
			this.TabPageH_Chat_Option03_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option03_Panel.Controls.Add(this.Chat_rtbxParty);
			this.TabPageH_Chat_Option03_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option03_Panel.Name = "TabPageH_Chat_Option03_Panel";
			this.TabPageH_Chat_Option03_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option03_Panel.TabIndex = 9;
			this.TabPageH_Chat_Option03_Panel.Visible = false;
			// 
			// TabPageH_Chat_Option02_Panel
			// 
			this.TabPageH_Chat_Option02_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageH_Chat_Option02_Panel.Controls.Add(this.Chat_rtbxPrivate);
			this.TabPageH_Chat_Option02_Panel.Location = new System.Drawing.Point(0, 27);
			this.TabPageH_Chat_Option02_Panel.Name = "TabPageH_Chat_Option02_Panel";
			this.TabPageH_Chat_Option02_Panel.Size = new System.Drawing.Size(657, 282);
			this.TabPageH_Chat_Option02_Panel.TabIndex = 11;
			this.TabPageH_Chat_Option02_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option12_Panel
			// 
			this.TabPageV_Control01_Option12_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option12_Panel.Controls.Add(this.Minimap_panelMap);
			this.TabPageV_Control01_Option12_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option12_Panel.Name = "TabPageV_Control01_Option12_Panel";
			this.TabPageV_Control01_Option12_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option12_Panel.TabIndex = 21;
			this.TabPageV_Control01_Option12_Panel.Visible = false;
			// 
			// Minimap_panelMap
			// 
			this.Minimap_panelMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Minimap_panelMap.Controls.Add(this.Minimap_btnLoadMap);
			this.Minimap_panelMap.Controls.Add(this.Minimap_panelCoords);
			this.Minimap_panelMap.Location = new System.Drawing.Point(0, 0);
			this.Minimap_panelMap.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_panelMap.Name = "Minimap_panelMap";
			this.Minimap_panelMap.Size = new System.Drawing.Size(655, 340);
			this.Minimap_panelMap.TabIndex = 16;
			// 
			// Minimap_btnLoadMap
			// 
			this.Minimap_btnLoadMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Minimap_btnLoadMap.FlatAppearance.BorderSize = 0;
			this.Minimap_btnLoadMap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.Minimap_btnLoadMap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.Minimap_btnLoadMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Minimap_btnLoadMap.Font = new System.Drawing.Font("Font Awesome 5 Pro Solid", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.Minimap_btnLoadMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_btnLoadMap.Location = new System.Drawing.Point(287, 130);
			this.Minimap_btnLoadMap.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_btnLoadMap.Name = "Minimap_btnLoadMap";
			this.Minimap_btnLoadMap.Size = new System.Drawing.Size(80, 80);
			this.Minimap_btnLoadMap.TabIndex = 19;
			this.Minimap_btnLoadMap.Tag = "Font Awesome 5 Pro Solid";
			this.Minimap_btnLoadMap.Text = "";
			this.Minimap_btnLoadMap.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.ToolTips.SetToolTip(this.Minimap_btnLoadMap, "Load Silkroad World Map");
			this.Minimap_btnLoadMap.UseCompatibleTextRendering = true;
			this.Minimap_btnLoadMap.UseVisualStyleBackColor = false;
			this.Minimap_btnLoadMap.Click += new System.EventHandler(this.Control_Click);
			// 
			// Minimap_panelCoords
			// 
			this.Minimap_panelCoords.Controls.Add(this.Minimap_tbxRegion);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_lblX);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_tbxX);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_lblY);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_tbxY);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_lblRegion);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_lblZ);
			this.Minimap_panelCoords.Controls.Add(this.Minimap_tbxZ);
			this.Minimap_panelCoords.Location = new System.Drawing.Point(346, 0);
			this.Minimap_panelCoords.Name = "Minimap_panelCoords";
			this.Minimap_panelCoords.Size = new System.Drawing.Size(310, 35);
			this.Minimap_panelCoords.TabIndex = 17;
			// 
			// Minimap_tbxRegion
			// 
			this.Minimap_tbxRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Minimap_tbxRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Minimap_tbxRegion.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_tbxRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_tbxRegion.Location = new System.Drawing.Point(249, 5);
			this.Minimap_tbxRegion.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_tbxRegion.Name = "Minimap_tbxRegion";
			this.Minimap_tbxRegion.ReadOnly = true;
			this.Minimap_tbxRegion.Size = new System.Drawing.Size(55, 25);
			this.Minimap_tbxRegion.TabIndex = 15;
			this.Minimap_tbxRegion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Minimap_tbxRegion.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Minimap_tbxRegion.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Minimap_lblX
			// 
			this.Minimap_lblX.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_lblX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_lblX.Location = new System.Drawing.Point(5, 5);
			this.Minimap_lblX.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_lblX.Name = "Minimap_lblX";
			this.Minimap_lblX.Size = new System.Drawing.Size(15, 25);
			this.Minimap_lblX.TabIndex = 8;
			this.Minimap_lblX.Text = "X";
			this.Minimap_lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Minimap_tbxX
			// 
			this.Minimap_tbxX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Minimap_tbxX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Minimap_tbxX.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_tbxX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_tbxX.Location = new System.Drawing.Point(20, 5);
			this.Minimap_tbxX.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_tbxX.Name = "Minimap_tbxX";
			this.Minimap_tbxX.ReadOnly = true;
			this.Minimap_tbxX.Size = new System.Drawing.Size(46, 25);
			this.Minimap_tbxX.TabIndex = 9;
			this.Minimap_tbxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Minimap_tbxX.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Minimap_tbxX.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Minimap_lblY
			// 
			this.Minimap_lblY.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_lblY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_lblY.Location = new System.Drawing.Point(70, 5);
			this.Minimap_lblY.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_lblY.Name = "Minimap_lblY";
			this.Minimap_lblY.Size = new System.Drawing.Size(15, 25);
			this.Minimap_lblY.TabIndex = 10;
			this.Minimap_lblY.Text = "Y";
			this.Minimap_lblY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Minimap_tbxY
			// 
			this.Minimap_tbxY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Minimap_tbxY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Minimap_tbxY.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_tbxY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_tbxY.Location = new System.Drawing.Point(85, 5);
			this.Minimap_tbxY.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_tbxY.Name = "Minimap_tbxY";
			this.Minimap_tbxY.ReadOnly = true;
			this.Minimap_tbxY.Size = new System.Drawing.Size(46, 25);
			this.Minimap_tbxY.TabIndex = 11;
			this.Minimap_tbxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Minimap_tbxY.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Minimap_tbxY.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// Minimap_lblRegion
			// 
			this.Minimap_lblRegion.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_lblRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_lblRegion.Location = new System.Drawing.Point(200, 5);
			this.Minimap_lblRegion.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_lblRegion.Name = "Minimap_lblRegion";
			this.Minimap_lblRegion.Size = new System.Drawing.Size(49, 25);
			this.Minimap_lblRegion.TabIndex = 14;
			this.Minimap_lblRegion.Text = "Region";
			this.Minimap_lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Minimap_lblZ
			// 
			this.Minimap_lblZ.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_lblZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_lblZ.Location = new System.Drawing.Point(135, 5);
			this.Minimap_lblZ.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_lblZ.Name = "Minimap_lblZ";
			this.Minimap_lblZ.Size = new System.Drawing.Size(15, 25);
			this.Minimap_lblZ.TabIndex = 12;
			this.Minimap_lblZ.Text = "Z";
			this.Minimap_lblZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Minimap_tbxZ
			// 
			this.Minimap_tbxZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Minimap_tbxZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Minimap_tbxZ.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.Minimap_tbxZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Minimap_tbxZ.Location = new System.Drawing.Point(150, 5);
			this.Minimap_tbxZ.Margin = new System.Windows.Forms.Padding(0);
			this.Minimap_tbxZ.Name = "Minimap_tbxZ";
			this.Minimap_tbxZ.ReadOnly = true;
			this.Minimap_tbxZ.Size = new System.Drawing.Size(46, 25);
			this.Minimap_tbxZ.TabIndex = 13;
			this.Minimap_tbxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Minimap_tbxZ.Enter += new System.EventHandler(this.Control_Focus_Enter);
			this.Minimap_tbxZ.Leave += new System.EventHandler(this.Control_Focus_Leave);
			// 
			// TabPageV_Control01_Option10_Panel
			// 
			this.TabPageV_Control01_Option10_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option10_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option10_Panel.Name = "TabPageV_Control01_Option10_Panel";
			this.TabPageV_Control01_Option10_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option10_Panel.TabIndex = 18;
			this.TabPageV_Control01_Option10_Panel.Visible = false;
			// 
			// TabPageV_Control01_Option09_Panel
			// 
			this.TabPageV_Control01_Option09_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option09_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option09_Panel.Name = "TabPageV_Control01_Option09_Panel";
			this.TabPageV_Control01_Option09_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option09_Panel.TabIndex = 16;
			this.TabPageV_Control01_Option09_Panel.Visible = false;
			// 
			// btnClientOptions
			// 
			this.btnClientOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnClientOptions.ContextMenuStrip = this.Menu_btnClientOptions;
			this.btnClientOptions.FlatAppearance.BorderSize = 0;
			this.btnClientOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnClientOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnClientOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClientOptions.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnClientOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.btnClientOptions.Location = new System.Drawing.Point(131, 391);
			this.btnClientOptions.Margin = new System.Windows.Forms.Padding(0);
			this.btnClientOptions.Name = "btnClientOptions";
			this.btnClientOptions.Size = new System.Drawing.Size(30, 26);
			this.btnClientOptions.TabIndex = 3;
			this.btnClientOptions.Tag = "Font Awesome 5 Pro Regular";
			this.btnClientOptions.Text = "";
			this.ToolTips.SetToolTip(this.btnClientOptions, "Client Options");
			this.btnClientOptions.UseCompatibleTextRendering = true;
			this.btnClientOptions.UseVisualStyleBackColor = true;
			this.btnClientOptions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Control_MouseClick);
			// 
			// Menu_btnClientOptions
			// 
			this.Menu_btnClientOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_btnClientOptions_ShowHide,
            this.Menu_btnClientOptions_GoClientless});
			this.Menu_btnClientOptions.Name = "Menu_rtbxPackets";
			this.Menu_btnClientOptions.Size = new System.Drawing.Size(153, 70);
			// 
			// Menu_btnClientOptions_ShowHide
			// 
			this.Menu_btnClientOptions_ShowHide.Name = "Menu_btnClientOptions_ShowHide";
			this.Menu_btnClientOptions_ShowHide.Size = new System.Drawing.Size(152, 22);
			this.Menu_btnClientOptions_ShowHide.Text = "Show/Hide";
			this.Menu_btnClientOptions_ShowHide.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_btnClientOptions_GoClientless
			// 
			this.Menu_btnClientOptions_GoClientless.Name = "Menu_btnClientOptions_GoClientless";
			this.Menu_btnClientOptions_GoClientless.Size = new System.Drawing.Size(152, 22);
			this.Menu_btnClientOptions_GoClientless.Text = "Go clientless";
			this.Menu_btnClientOptions_GoClientless.Click += new System.EventHandler(this.Menu_Click);
			// 
			// btnBotStart
			// 
			this.btnBotStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnBotStart.FlatAppearance.BorderSize = 0;
			this.btnBotStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnBotStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnBotStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBotStart.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnBotStart.ForeColor = System.Drawing.Color.Red;
			this.btnBotStart.Location = new System.Drawing.Point(131, 441);
			this.btnBotStart.Margin = new System.Windows.Forms.Padding(0);
			this.btnBotStart.Name = "btnBotStart";
			this.btnBotStart.Size = new System.Drawing.Size(30, 26);
			this.btnBotStart.TabIndex = 1;
			this.btnBotStart.Tag = "Font Awesome 5 Pro Regular";
			this.btnBotStart.Text = "";
			this.ToolTips.SetToolTip(this.btnBotStart, "Start Bot");
			this.btnBotStart.UseCompatibleTextRendering = true;
			this.btnBotStart.UseVisualStyleBackColor = true;
			this.btnBotStart.Click += new System.EventHandler(this.Control_Click);
			// 
			// TabPageV_Control01_Option06_Panel
			// 
			this.TabPageV_Control01_Option06_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabPageV_Control01_Option06_Panel.Location = new System.Drawing.Point(135, 45);
			this.TabPageV_Control01_Option06_Panel.Name = "TabPageV_Control01_Option06_Panel";
			this.TabPageV_Control01_Option06_Panel.Size = new System.Drawing.Size(657, 342);
			this.TabPageV_Control01_Option06_Panel.TabIndex = 11;
			this.TabPageV_Control01_Option06_Panel.Visible = false;
			// 
			// btnAnalyzer
			// 
			this.btnAnalyzer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnAnalyzer.FlatAppearance.BorderSize = 0;
			this.btnAnalyzer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnAnalyzer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnAnalyzer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAnalyzer.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnAnalyzer.ForeColor = System.Drawing.Color.HotPink;
			this.btnAnalyzer.Location = new System.Drawing.Point(131, 417);
			this.btnAnalyzer.Margin = new System.Windows.Forms.Padding(0);
			this.btnAnalyzer.Name = "btnAnalyzer";
			this.btnAnalyzer.Size = new System.Drawing.Size(30, 26);
			this.btnAnalyzer.TabIndex = 2;
			this.btnAnalyzer.Tag = "Font Awesome 5 Pro Regular";
			this.btnAnalyzer.Text = "";
			this.ToolTips.SetToolTip(this.btnAnalyzer, "Go to Packet Analyzer");
			this.btnAnalyzer.UseCompatibleTextRendering = true;
			this.btnAnalyzer.UseVisualStyleBackColor = true;
			this.btnAnalyzer.Click += new System.EventHandler(this.Control_Click);
			// 
			// lblBotState
			// 
			this.lblBotState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.lblBotState.Font = new System.Drawing.Font("Source Sans Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblBotState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblBotState.Location = new System.Drawing.Point(5, 472);
			this.lblBotState.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.lblBotState.Name = "lblBotState";
			this.lblBotState.Size = new System.Drawing.Size(790, 19);
			this.lblBotState.TabIndex = 3;
			this.lblBotState.Tag = "Source Sans Pro";
			this.lblBotState.Text = "xBot - Made by Engels \"JellyBitz\" Quintero";
			this.lblBotState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// NotifyIcon
			// 
			this.NotifyIcon.ContextMenuStrip = this.Menu_NotifyIcon;
			this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
			this.NotifyIcon.Visible = true;
			this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
			// 
			// Menu_NotifyIcon
			// 
			this.Menu_NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_NotifyIcon_HideShow,
            this.Menu_NotifyIcon_About,
            this.Menu_NotifyIcon_Separator01,
            this.Menu_NotifyIcon_Exit});
			this.Menu_NotifyIcon.Name = "Menu_NotifyIcon";
			this.Menu_NotifyIcon.Size = new System.Drawing.Size(108, 76);
			// 
			// Menu_NotifyIcon_HideShow
			// 
			this.Menu_NotifyIcon_HideShow.Name = "Menu_NotifyIcon_HideShow";
			this.Menu_NotifyIcon_HideShow.Size = new System.Drawing.Size(107, 22);
			this.Menu_NotifyIcon_HideShow.Text = "Hide";
			this.Menu_NotifyIcon_HideShow.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_NotifyIcon_About
			// 
			this.Menu_NotifyIcon_About.Name = "Menu_NotifyIcon_About";
			this.Menu_NotifyIcon_About.Size = new System.Drawing.Size(107, 22);
			this.Menu_NotifyIcon_About.Text = "About";
			this.Menu_NotifyIcon_About.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Menu_NotifyIcon_Separator01
			// 
			this.Menu_NotifyIcon_Separator01.Name = "Menu_NotifyIcon_Separator01";
			this.Menu_NotifyIcon_Separator01.Size = new System.Drawing.Size(104, 6);
			// 
			// Menu_NotifyIcon_Exit
			// 
			this.Menu_NotifyIcon_Exit.Name = "Menu_NotifyIcon_Exit";
			this.Menu_NotifyIcon_Exit.Size = new System.Drawing.Size(107, 22);
			this.Menu_NotifyIcon_Exit.Text = "Exit";
			this.Menu_NotifyIcon_Exit.Click += new System.EventHandler(this.Menu_Click);
			// 
			// Character_pgbJobExp
			// 
			this.Character_pgbJobExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(73)))), ((int)(((byte)(33)))));
			this.Character_pgbJobExp.ColorDegradation = 4;
			this.Character_pgbJobExp.Display = xGraphics.xProgressBarDisplay.Percentage;
			this.Character_pgbJobExp.DisplayFont = new System.Drawing.Font("Times New Roman", 12F);
			this.Character_pgbJobExp.DisplayShadow = System.Drawing.Color.Black;
			this.Character_pgbJobExp.DisplayText = null;
			this.Character_pgbJobExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_pgbJobExp.Location = new System.Drawing.Point(95, 90);
			this.Character_pgbJobExp.Name = "Character_pgbJobExp";
			this.Character_pgbJobExp.Size = new System.Drawing.Size(250, 22);
			this.Character_pgbJobExp.TabIndex = 23;
			this.Character_pgbJobExp.Tag = "Source Sans Pro";
			this.ToolTips.SetToolTip(this.Character_pgbJobExp, "Job Experience");
			this.Character_pgbJobExp.Value = ((ulong)(0ul));
			this.Character_pgbJobExp.ValueMaximum = ((ulong)(0ul));
			// 
			// Character_rtbxMessageFilter
			// 
			this.Character_rtbxMessageFilter.AutoScroll = true;
			this.Character_rtbxMessageFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.Character_rtbxMessageFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Character_rtbxMessageFilter.Font = new System.Drawing.Font("Source Sans Pro", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Character_rtbxMessageFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_rtbxMessageFilter.Location = new System.Drawing.Point(1, 40);
			this.Character_rtbxMessageFilter.Margin = new System.Windows.Forms.Padding(1);
			this.Character_rtbxMessageFilter.MaxLines = 2048;
			this.Character_rtbxMessageFilter.Name = "Character_rtbxMessageFilter";
			this.Character_rtbxMessageFilter.ReadOnly = true;
			this.Character_rtbxMessageFilter.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Character_rtbxMessageFilter.Size = new System.Drawing.Size(298, 267);
			this.Character_rtbxMessageFilter.TabIndex = 6;
			this.Character_rtbxMessageFilter.TabStop = false;
			this.Character_rtbxMessageFilter.Tag = "Source Sans Pro";
			this.Character_rtbxMessageFilter.Text = "";
			// 
			// Character_pgbExp
			// 
			this.Character_pgbExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(128)))), ((int)(((byte)(10)))));
			this.Character_pgbExp.ColorDegradation = 5;
			this.Character_pgbExp.Display = xGraphics.xProgressBarDisplay.Percentage;
			this.Character_pgbExp.DisplayFont = new System.Drawing.Font("Times New Roman", 12F);
			this.Character_pgbExp.DisplayShadow = System.Drawing.Color.Black;
			this.Character_pgbExp.DisplayText = null;
			this.Character_pgbExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_pgbExp.Location = new System.Drawing.Point(60, 62);
			this.Character_pgbExp.Name = "Character_pgbExp";
			this.Character_pgbExp.Size = new System.Drawing.Size(285, 22);
			this.Character_pgbExp.TabIndex = 5;
			this.Character_pgbExp.Tag = "Source Sans Pro";
			this.ToolTips.SetToolTip(this.Character_pgbExp, "Experience");
			this.Character_pgbExp.Value = ((ulong)(0ul));
			this.Character_pgbExp.ValueMaximum = ((ulong)(0ul));
			// 
			// Character_pgbMP
			// 
			this.Character_pgbMP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(255)))));
			this.Character_pgbMP.ColorDegradation = 6;
			this.Character_pgbMP.Display = xGraphics.xProgressBarDisplay.Percentage;
			this.Character_pgbMP.DisplayFont = new System.Drawing.Font("Times New Roman", 12F);
			this.Character_pgbMP.DisplayShadow = System.Drawing.Color.Black;
			this.Character_pgbMP.DisplayText = null;
			this.Character_pgbMP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_pgbMP.Location = new System.Drawing.Point(5, 34);
			this.Character_pgbMP.Name = "Character_pgbMP";
			this.Character_pgbMP.Size = new System.Drawing.Size(340, 22);
			this.Character_pgbMP.TabIndex = 4;
			this.Character_pgbMP.Tag = "Source Sans Pro";
			this.ToolTips.SetToolTip(this.Character_pgbMP, "Mana Points");
			this.Character_pgbMP.Value = ((ulong)(0ul));
			this.Character_pgbMP.ValueMaximum = ((ulong)(0ul));
			this.Character_pgbMP.Click += new System.EventHandler(this.Control_Click);
			// 
			// Character_pgbHP
			// 
			this.Character_pgbHP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.Character_pgbHP.ColorDegradation = 6;
			this.Character_pgbHP.Display = xGraphics.xProgressBarDisplay.Percentage;
			this.Character_pgbHP.DisplayFont = new System.Drawing.Font("Times New Roman", 12F);
			this.Character_pgbHP.DisplayShadow = System.Drawing.Color.Black;
			this.Character_pgbHP.DisplayText = null;
			this.Character_pgbHP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Character_pgbHP.Location = new System.Drawing.Point(5, 6);
			this.Character_pgbHP.Name = "Character_pgbHP";
			this.Character_pgbHP.Size = new System.Drawing.Size(340, 22);
			this.Character_pgbHP.TabIndex = 3;
			this.Character_pgbHP.Tag = "Source Sans Pro";
			this.ToolTips.SetToolTip(this.Character_pgbHP, "Health Points");
			this.Character_pgbHP.Value = ((ulong)(0ul));
			this.Character_pgbHP.ValueMaximum = ((ulong)(0ul));
			this.Character_pgbHP.Click += new System.EventHandler(this.Control_Click);
			// 
			// Settings_rtbxPackets
			// 
			this.Settings_rtbxPackets.AutoScroll = false;
			this.Settings_rtbxPackets.BackColor = System.Drawing.Color.Black;
			this.Settings_rtbxPackets.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Settings_rtbxPackets.ContextMenuStrip = this.Menu_rtbxPackets;
			this.Settings_rtbxPackets.DetectUrls = false;
			this.Settings_rtbxPackets.Font = new System.Drawing.Font("Source Sans Pro", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Settings_rtbxPackets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_rtbxPackets.Location = new System.Drawing.Point(178, 5);
			this.Settings_rtbxPackets.Margin = new System.Windows.Forms.Padding(1);
			this.Settings_rtbxPackets.MaxLines = 2048;
			this.Settings_rtbxPackets.Name = "Settings_rtbxPackets";
			this.Settings_rtbxPackets.ReadOnly = true;
			this.Settings_rtbxPackets.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Settings_rtbxPackets.Size = new System.Drawing.Size(472, 247);
			this.Settings_rtbxPackets.TabIndex = 15;
			this.Settings_rtbxPackets.Tag = "Source Sans Pro";
			this.Settings_rtbxPackets.Text = "";
			// 
			// Settings_rtbxHWIDdata
			// 
			this.Settings_rtbxHWIDdata.AutoScroll = false;
			this.Settings_rtbxHWIDdata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.Settings_rtbxHWIDdata.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Settings_rtbxHWIDdata.DetectUrls = false;
			this.Settings_rtbxHWIDdata.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Settings_rtbxHWIDdata.Font = new System.Drawing.Font("Source Sans Pro", 11.85F);
			this.Settings_rtbxHWIDdata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Settings_rtbxHWIDdata.Location = new System.Drawing.Point(0, 0);
			this.Settings_rtbxHWIDdata.MaxLines = 2147483647;
			this.Settings_rtbxHWIDdata.Name = "Settings_rtbxHWIDdata";
			this.Settings_rtbxHWIDdata.ReadOnly = true;
			this.Settings_rtbxHWIDdata.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Settings_rtbxHWIDdata.Size = new System.Drawing.Size(119, 86);
			this.Settings_rtbxHWIDdata.TabIndex = 6;
			this.Settings_rtbxHWIDdata.Tag = "Source Sans Pro";
			this.Settings_rtbxHWIDdata.Text = "";
			this.ToolTips.SetToolTip(this.Settings_rtbxHWIDdata, "HWID saved from Client opcode");
			// 
			// Chat_rtbxAll
			// 
			this.Chat_rtbxAll.AutoScroll = true;
			this.Chat_rtbxAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxAll.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxAll.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxAll.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxAll.MaxLines = 1024;
			this.Chat_rtbxAll.Name = "Chat_rtbxAll";
			this.Chat_rtbxAll.ReadOnly = true;
			this.Chat_rtbxAll.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxAll.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxAll.TabIndex = 0;
			this.Chat_rtbxAll.Text = "";
			// 
			// Chat_rtbxGlobal
			// 
			this.Chat_rtbxGlobal.AutoScroll = true;
			this.Chat_rtbxGlobal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxGlobal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxGlobal.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxGlobal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxGlobal.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxGlobal.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxGlobal.MaxLines = 1024;
			this.Chat_rtbxGlobal.Name = "Chat_rtbxGlobal";
			this.Chat_rtbxGlobal.ReadOnly = true;
			this.Chat_rtbxGlobal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxGlobal.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxGlobal.TabIndex = 7;
			this.Chat_rtbxGlobal.Text = "";
			// 
			// Chat_rtbxStall
			// 
			this.Chat_rtbxStall.AutoScroll = true;
			this.Chat_rtbxStall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxStall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxStall.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxStall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxStall.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxStall.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxStall.MaxLines = 1024;
			this.Chat_rtbxStall.Name = "Chat_rtbxStall";
			this.Chat_rtbxStall.ReadOnly = true;
			this.Chat_rtbxStall.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxStall.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxStall.TabIndex = 6;
			this.Chat_rtbxStall.Text = "";
			// 
			// Chat_rtbxAcademy
			// 
			this.Chat_rtbxAcademy.AutoScroll = true;
			this.Chat_rtbxAcademy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxAcademy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxAcademy.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxAcademy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxAcademy.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxAcademy.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxAcademy.MaxLines = 1024;
			this.Chat_rtbxAcademy.Name = "Chat_rtbxAcademy";
			this.Chat_rtbxAcademy.ReadOnly = true;
			this.Chat_rtbxAcademy.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxAcademy.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxAcademy.TabIndex = 5;
			this.Chat_rtbxAcademy.Text = "";
			// 
			// Chat_rtbxUnion
			// 
			this.Chat_rtbxUnion.AutoScroll = true;
			this.Chat_rtbxUnion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxUnion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxUnion.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxUnion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxUnion.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxUnion.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxUnion.MaxLines = 1024;
			this.Chat_rtbxUnion.Name = "Chat_rtbxUnion";
			this.Chat_rtbxUnion.ReadOnly = true;
			this.Chat_rtbxUnion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxUnion.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxUnion.TabIndex = 4;
			this.Chat_rtbxUnion.Text = "";
			// 
			// Chat_rtbxGuild
			// 
			this.Chat_rtbxGuild.AutoScroll = true;
			this.Chat_rtbxGuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxGuild.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxGuild.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxGuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxGuild.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxGuild.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxGuild.MaxLines = 1024;
			this.Chat_rtbxGuild.Name = "Chat_rtbxGuild";
			this.Chat_rtbxGuild.ReadOnly = true;
			this.Chat_rtbxGuild.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxGuild.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxGuild.TabIndex = 3;
			this.Chat_rtbxGuild.Text = "";
			// 
			// Chat_rtbxParty
			// 
			this.Chat_rtbxParty.AutoScroll = true;
			this.Chat_rtbxParty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxParty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxParty.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxParty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxParty.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxParty.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxParty.MaxLines = 1024;
			this.Chat_rtbxParty.Name = "Chat_rtbxParty";
			this.Chat_rtbxParty.ReadOnly = true;
			this.Chat_rtbxParty.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxParty.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxParty.TabIndex = 2;
			this.Chat_rtbxParty.Text = "";
			// 
			// Chat_rtbxPrivate
			// 
			this.Chat_rtbxPrivate.AutoScroll = true;
			this.Chat_rtbxPrivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
			this.Chat_rtbxPrivate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Chat_rtbxPrivate.Font = new System.Drawing.Font("Source Sans Pro", 9.7F);
			this.Chat_rtbxPrivate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.Chat_rtbxPrivate.Location = new System.Drawing.Point(8, 8);
			this.Chat_rtbxPrivate.Margin = new System.Windows.Forms.Padding(8);
			this.Chat_rtbxPrivate.MaxLines = 1024;
			this.Chat_rtbxPrivate.Name = "Chat_rtbxPrivate";
			this.Chat_rtbxPrivate.ReadOnly = true;
			this.Chat_rtbxPrivate.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.Chat_rtbxPrivate.Size = new System.Drawing.Size(639, 265);
			this.Chat_rtbxPrivate.TabIndex = 1;
			this.Chat_rtbxPrivate.Text = "";
			// 
			// rtbxLogs
			// 
			this.rtbxLogs.AutoScroll = true;
			this.rtbxLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.rtbxLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbxLogs.Font = new System.Drawing.Font("Source Sans Pro", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbxLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.rtbxLogs.Location = new System.Drawing.Point(162, 391);
			this.rtbxLogs.Margin = new System.Windows.Forms.Padding(1);
			this.rtbxLogs.MaxLines = 512;
			this.rtbxLogs.Name = "rtbxLogs";
			this.rtbxLogs.ReadOnly = true;
			this.rtbxLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbxLogs.Size = new System.Drawing.Size(630, 76);
			this.rtbxLogs.TabIndex = 0;
			this.rtbxLogs.TabStop = false;
			this.rtbxLogs.Tag = "Source Sans Pro";
			this.rtbxLogs.Text = "";
			// 
			// Window
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.ClientSize = new System.Drawing.Size(800, 491);
			this.Controls.Add(this.lblBotState);
			this.Controls.Add(this.pnlWindow);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Window";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "Source Sans Pro";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_Closing);
			this.Load += new System.EventHandler(this.Window_Load);
			this.TabPageV_Control01.ResumeLayout(false);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.TabPageV_Control01_Option14_Panel.ResumeLayout(false);
			this.TabPageH_Settings.ResumeLayout(false);
			this.TabPageH_Settings_Option04_Panel.ResumeLayout(false);
			this.Settings_gbxPacketInject.ResumeLayout(false);
			this.Settings_gbxPacketInject.PerformLayout();
			this.Settings_gbxPacketFilter.ResumeLayout(false);
			this.Settings_gbxPacketFilter.PerformLayout();
			this.Menu_lstvOpcodes.ResumeLayout(false);
			this.Menu_rtbxPackets.ResumeLayout(false);
			this.TabPageH_Settings_Option01_Panel.ResumeLayout(false);
			this.TabPageH_Settings_Option01_Panel.PerformLayout();
			this.Menu_lstvHost.ResumeLayout(false);
			this.Menu_lstrSilkroads.ResumeLayout(false);
			this.Settings_gbxHWID.ResumeLayout(false);
			this.Settings_gbxHWID.PerformLayout();
			this.Settings_panelHWIDdata.ResumeLayout(false);
			this.TabPageH_Settings_Option02_Panel.ResumeLayout(false);
			this.Settings_gbxCharacterSelection.ResumeLayout(false);
			this.Settings_gbxCharacterSelection.PerformLayout();
			this.TabPageV_Control01_Option03_Panel.ResumeLayout(false);
			this.TabPageH_Inventory.ResumeLayout(false);
			this.TabPageH_Inventory_Option01_Panel.ResumeLayout(false);
			this.Menu_lstvItems.ResumeLayout(false);
			this.TabPageV_Control01_Option04_Panel.ResumeLayout(false);
			this.TabPageH_Party.ResumeLayout(false);
			this.TabPageH_Party_Option03_Panel.ResumeLayout(false);
			this.TabPageH_Party_Option03_Panel.PerformLayout();
			this.Menu_lstvPartyMatch.ResumeLayout(false);
			this.Party_pnlAutoFormMatch.ResumeLayout(false);
			this.Party_pnlAutoFormMatch.PerformLayout();
			this.TabPageH_Party_Option02_Panel.ResumeLayout(false);
			this.Party_gbxLeaderList.ResumeLayout(false);
			this.Party_gbxLeaderList.PerformLayout();
			this.Menu_lstvLeaderList.ResumeLayout(false);
			this.Party_gbxSetup.ResumeLayout(false);
			this.Party_pnlSetupItem.ResumeLayout(false);
			this.Party_gbxSetupItem.ResumeLayout(false);
			this.Party_pnlSetupExp.ResumeLayout(false);
			this.Party_gbxSetupExp.ResumeLayout(false);
			this.Party_gbxAcceptInvite.ResumeLayout(false);
			this.Party_gbxPlayerList.ResumeLayout(false);
			this.Party_gbxPlayerList.PerformLayout();
			this.Menu_lstvPartyList.ResumeLayout(false);
			this.TabPageH_Party_Option01_Panel.ResumeLayout(false);
			this.Menu_lstvPartyMembers.ResumeLayout(false);
			this.pnlWindow.ResumeLayout(false);
			this.TabPageV_Control01_Option08_Panel.ResumeLayout(false);
			this.TabPageH_Training.ResumeLayout(false);
			this.TabPageH_Training_Option01_Panel.ResumeLayout(false);
			this.TabPageH_Training_Option01_Panel.PerformLayout();
			this.TabPageH_Training_Option03_Panel.ResumeLayout(false);
			this.Training_gbxTrace.ResumeLayout(false);
			this.Training_gbxTrace.PerformLayout();
			this.TabPageV_Control01_Option01_Panel.ResumeLayout(false);
			this.Login_gbxConnection.ResumeLayout(false);
			this.Login_gbxLogin.ResumeLayout(false);
			this.Login_gbxLogin.PerformLayout();
			this.Login_gbxAdvertising.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Login_pbxAds)).EndInit();
			this.Login_gbxServers.ResumeLayout(false);
			this.Login_gbxCharacters.ResumeLayout(false);
			this.TabPageV_Control01_Option07_Panel.ResumeLayout(false);
			this.TabPageH_Skills.ResumeLayout(false);
			this.TabPageH_Skills_Option01_Panel.ResumeLayout(false);
			this.TabPageV_Control01_Option02_Panel.ResumeLayout(false);
			this.TabPageH_Character.ResumeLayout(false);
			this.TabPageH_Character_Option01_Panel.ResumeLayout(false);
			this.TabPageH_Character_Option01_Panel.PerformLayout();
			this.Character_gbxStatPoints.ResumeLayout(false);
			this.Character_gbxMessageFilter.ResumeLayout(false);
			this.TabPageH_Character_Option02_Panel.ResumeLayout(false);
			this.Character_gbxPotionPet.ResumeLayout(false);
			this.Character_gbxPotionPet.PerformLayout();
			this.Character_gbxPotionsPlayer.ResumeLayout(false);
			this.Character_gbxPotionsPlayer.PerformLayout();
			this.TabPageH_Character_Option04_Panel.ResumeLayout(false);
			this.TabPageV_Control01_Option13_Panel.ResumeLayout(false);
			this.TabPageV_Control01_Option13_Panel.PerformLayout();
			this.TabPageV_Control01_Option11_Panel.ResumeLayout(false);
			this.TabPageH_Chat.ResumeLayout(false);
			this.TabPageH_Chat_Option01_Panel.ResumeLayout(false);
			this.Chat_panel.ResumeLayout(false);
			this.Chat_panel.PerformLayout();
			this.TabPageH_Chat_Option08_Panel.ResumeLayout(false);
			this.TabPageH_Chat_Option07_Panel.ResumeLayout(false);
			this.TabPageH_Chat_Option06_Panel.ResumeLayout(false);
			this.TabPageH_Chat_Option05_Panel.ResumeLayout(false);
			this.TabPageH_Chat_Option04_Panel.ResumeLayout(false);
			this.TabPageH_Chat_Option03_Panel.ResumeLayout(false);
			this.TabPageH_Chat_Option02_Panel.ResumeLayout(false);
			this.TabPageV_Control01_Option12_Panel.ResumeLayout(false);
			this.Minimap_panelMap.ResumeLayout(false);
			this.Minimap_panelCoords.ResumeLayout(false);
			this.Minimap_panelCoords.PerformLayout();
			this.Menu_btnClientOptions.ResumeLayout(false);
			this.Menu_NotifyIcon.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private xRichTextBox rtbxLogs;

		private Panel TabPageV_Control01;

		private Label TabPageV_Control01_Option11_Icon;

		private Label TabPageV_Control01_Option03_Icon;

		private Button TabPageV_Control01_Option11;

		private Button TabPageV_Control01_Option03;

		private Panel pnlHeader;

		private Button btnWinMinimize;

		private Button btnWinRestore;

		private Button btnWinExit;

		private Label TabPageV_Control01_Option04_Icon;

		private Button TabPageV_Control01_Option04;

		private Label TabPageV_Control01_Option05_Icon;

		private Button TabPageV_Control01_Option05;

		private Panel TabPageV_Control01_Option14_Panel;

		private Panel TabPageV_Control01_Option03_Panel;

		private Panel TabPageV_Control01_Option04_Panel;

		private Label TabPageV_Control01_Option02_Icon;

		private Button TabPageV_Control01_Option02;

		private Panel TabPageV_Control01_Option05_Panel;

		private Label Settings_lblHost;

		private Label Settings_lblPort;

		private Label lblHeaderIcon;

		private Panel pnlWindow;

		private Label Settings_lblVersion;

		private Label Settings_lblLocale;

		private Button btnBotStart;

		private Label TabPageV_Control01_Option01_Icon;

		private Button TabPageV_Control01_Option01;

		private Label Login_lblPassword;

		private Label Login_lblCharacter;

		private Label Login_lblUsername;

		private Label lblBotState;

		private RadioButton Login_rbnClientless;

		private RadioButton Login_rbnClient;

		public TextBox Settings_tbxVersion;

		public TextBox Settings_tbxLocale;

		public CheckBox Settings_cbxCreateChar;

		public Button Login_btnStart;

		private Label TabPageV_Control01_Option10_Icon;

		private Button TabPageV_Control01_Option10;

		private Label TabPageV_Control01_Option09_Icon;

		private Button TabPageV_Control01_Option09;

		private Label TabPageV_Control01_Option08_Icon;

		private Button TabPageV_Control01_Option08;

		private Label TabPageV_Control01_Option07_Icon;

		private Button TabPageV_Control01_Option07;

		private Label TabPageV_Control01_Option06_Icon;

		private Button TabPageV_Control01_Option06;

		private Panel TabPageV_Control01_Option06_Panel;

		private Panel TabPageV_Control01_Option07_Panel;

		private Panel TabPageV_Control01_Option08_Panel;

		private Panel TabPageH_Party;

		private Button TabPageH_Party_Option01;

		private Panel TabPageH_Party_Option01_Panel;

		private Button TabPageH_Party_Option04;

		private Button TabPageH_Party_Option03;

		private Button TabPageH_Party_Option02;

		private Panel TabPageH_Party_Option02_Panel;

		private Panel TabPageH_Party_Option03_Panel;

		private Panel TabPageH_Party_Option04_Panel;

		private GroupBox Party_gbxPlayerList;

		public TextBox Party_tbxPlayer;

		private ColumnHeader columnHeader1;

		private Panel TabPageH_Settings;

		private Button TabPageH_Settings_Option04;

		private Button TabPageH_Settings_Option03;

		private Button TabPageH_Settings_Option02;

		private Button TabPageH_Settings_Option01;

		private Panel TabPageH_Settings_Option01_Panel;

		private Panel TabPageH_Settings_Option02_Panel;

		private Panel TabPageH_Settings_Option03_Panel;

		private Panel TabPageH_Settings_Option04_Panel;

		private Panel TabPageV_Control01_Option09_Panel;

		private Button btnAnalyzer;

		private Button btnClientOptions;

		public CheckBox Settings_cbxShowPacketClient;

		public CheckBox Settings_cbxShowPacketServer;

		private Label Login_lblServer;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		public ListView Login_lstvServers;

		private ColumnHeader columnHeader4;

		public TextBox Login_tbxPassword;

		public TextBox Login_tbxUsername;

		public ComboBox Login_cmbxServer;

		public ListView Login_lstvCharacters;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		public GroupBox Login_gbxCharacters;

		public GroupBox Login_gbxServers;

		private ColumnHeader columnHeader8;

		public ComboBox Login_cmbxCharacter;

		public Button Login_btnLauncher;

		private Panel TabPageV_Control01_Option01_Panel;

		private GroupBox Login_gbxConnection;

		public GroupBox Login_gbxLogin;

		private Panel TabPageV_Control01_Option10_Panel;

		private Panel TabPageH_Chat_Option01_Panel;

		private Panel TabPageH_Chat;

		private Panel TabPageH_Chat_Option02_Panel;

		private Panel TabPageH_Chat_Option03_Panel;

		private Panel TabPageH_Chat_Option04_Panel;

		private Panel TabPageH_Chat_Option08_Panel;

		private Panel TabPageH_Chat_Option05_Panel;

		private Panel TabPageH_Chat_Option07_Panel;

		private Panel TabPageH_Chat_Option06_Panel;

		private TextBox Chat_tbxMsg;

		public ComboBox Chat_cmbxMsgType;

		private Panel Chat_panel;

		private TextBox Chat_tbxMsgPlayer;

		public xRichTextBox Chat_rtbxAll;

		public xRichTextBox Chat_rtbxParty;

		public xRichTextBox Chat_rtbxPrivate;

		public xRichTextBox Chat_rtbxUnion;

		public xRichTextBox Chat_rtbxGuild;

		public xRichTextBox Chat_rtbxAcademy;

		private GroupBox Settings_gbxCharacterSelection;

		public CheckBox Settings_cbxCreateCharBelow40;

		public CheckBox Settings_cbxDeleteChar40to50;

		public CheckBox Settings_cbxLoadDefaultConfigs;

		public xRichTextBox Chat_rtbxGlobal;

		public xRichTextBox Chat_rtbxStall;

		private xRichTextBox Settings_rtbxPackets;

		private GroupBox Settings_gbxPacketFilter;

		private ColumnHeader columnHeader9;

		private Button Settings_btnAddOpcode;

		private RadioButton Settings_rbnPacketNotShow;

		private Label Settings_lblFilterOpcode;

		private ContextMenuStrip Menu_lstvOpcodes;

		private ToolStripMenuItem Menu_lstvOpcodes_Remove;

		private ToolStripMenuItem Menu_lstvOpcodes_RemoveAll;

		private GroupBox Settings_gbxHWID;

		public TextBox Settings_tbxHWIDServerOp;

		private Label Settings_lblHWIDServerOp;

		public ToolTip ToolTips;

		public ListView Settings_lstvOpcodes;

		public RadioButton Settings_rbnPacketOnlyShow;

		private ContextMenuStrip Menu_rtbxPackets;

		private ToolStripMenuItem Menu_rtbxPackets_Clear;

		private Label lblHeaderText01;

		private ToolStripMenuItem Menu_rtbxPackets_AutoScroll;

		private Panel Minimap_panelMap;

		public TextBox Minimap_tbxRegion;

		private Label Minimap_lblX;

		public TextBox Minimap_tbxX;

		private Label Minimap_lblY;

		public TextBox Minimap_tbxY;

		private Label Minimap_lblRegion;

		private Label Minimap_lblZ;

		public TextBox Minimap_tbxZ;

		public Panel Minimap_panelCoords;

		private Label TabPageV_Control01_Option14_Icon;

		private Button TabPageV_Control01_Option14;

		private Label TabPageV_Control01_Option13_Icon;

		private Button TabPageV_Control01_Option13;

		private Label TabPageV_Control01_Option12_Icon;

		private Button TabPageV_Control01_Option12;

		private GroupBox Settings_gbxPacketInject;

		private Label Settings_lblInjectData;

		private Button Settings_btnInjectPacket;

		private Label Settings_lblInjectOpcode;

		private Panel TabPageV_Control01_Option11_Panel;

		private Panel TabPageV_Control01_Option12_Panel;

		private Panel TabPageV_Control01_Option13_Panel;

		public TreeView GameInfo_lstrObjects;

		private TextBox Settings_tbxInjectOpcode;

		private TextBox Settings_tbxFilterOpcode;

		private ComboBox Settings_cmbxInjectTo;

		private TextBox Settings_tbxInjectData;

		private CheckBox Settings_cbxInjectMassive;

		private CheckBox Settings_cbxInjectEncrypted;

		public TextBox Settings_tbxHWIDClientOp;

		private Label Settings_lblHWIDClientOp;

		private Label Settings_lblSilkroadName;

		public TextBox Settings_tbxSilkroadName;

		private ColumnHeader columnHeader10;

		public TreeView Settings_lstrSilkroads;

		private ContextMenuStrip Menu_lstrSilkroads;

		private ToolStripMenuItem Menu_lstrSilkroads_Remove;

		private ContextMenuStrip Menu_lstvHost;

		private ToolStripMenuItem Menu_lstvHost_remove;

		public ComboBox Login_cmbxSilkroad;

		private Label Login_lblSilkroad;

		private Button Login_btnAddSilkroad;

		public ComboBox Settings_cmbxHWIDServerSendTo;

		public ComboBox Settings_cmbxHWIDClientSaveFrom;

		public CheckBox Settings_cbxHWIDSendOnlyOnce;

		private Panel TabPageV_Control01_Option02_Panel;

		private Panel TabPageH_Character;

		private Button TabPageH_Character_Option04;

		private Button TabPageH_Character_Option03;

		private Button TabPageH_Character_Option02;

		private Button TabPageH_Character_Option01;

		private Panel TabPageH_Character_Option01_Panel;

		private Panel TabPageH_Character_Option03_Panel;

		private Panel TabPageH_Character_Option04_Panel;

		private Panel TabPageH_Character_Option02_Panel;

		public Button Settings_btnAddSilkroad;

		public Button Settings_btnPK2Path;

		public ListView Settings_lstvHost;

		public TextBox Settings_tbxPort;

		private Panel Settings_panelHWIDdata;

		public CheckBox Settings_cbxRandomHost;

		public xProgressBar Character_pgbExp;

		public xProgressBar Character_pgbMP;

		public xProgressBar Character_pgbHP;

		private xRichTextBox Character_rtbxMessageFilter;

		private GroupBox Character_gbxMessageFilter;

		public Button Settings_btnClientPath;

		public Button Minimap_btnLoadMap;

		public Label Character_lblLevel;

		public CheckBox Character_cbxMessageExp;

		public CheckBox Character_cbxMessageUniques;

		public CheckBox Character_cbxMessagePicks;

		public Button Settings_btnLauncherPath;

		public ListView Party_lstvPartyMembers;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		public CheckBox Party_cbxShowFGWInvites;

		private ColumnHeader columnHeader14;

		public CheckBox Settings_cbxSelectFirstChar;

		private Label Settings_lblCustomName;

		public TextBox Settings_tbxCustomName;

		public TextBox Settings_tbxCustomSequence;

		private Label Settings_lblCustomSequence;

		public ComboBox Settings_cmbxCreateCharRace;

		private Label Settings_lblCreateCharRace;

		public ComboBox Settings_cmbxCreateCharGenre;

		private Label Settings_lblCreateCharGenre;

		private ContextMenuStrip Menu_NotifyIcon;

		private ToolStripMenuItem Menu_NotifyIcon_HideShow;

		private ToolStripMenuItem Menu_NotifyIcon_About;

		private ToolStripSeparator Menu_NotifyIcon_Separator01;

		private ToolStripMenuItem Menu_NotifyIcon_Exit;

		public TextBox GameInfo_tbxServerTime;

		private Label GameInfo_lblServerTime;

		private GroupBox Party_gbxAcceptInvite;

		public CheckBox Party_cbxAcceptAll;

		public CheckBox Party_cbxAcceptPartyList;

		public CheckBox Party_cbxAcceptLeaderList;

		private GroupBox Party_gbxSetup;

		public CheckBox Party_cbxRefuseInvitations;

		public CheckBox Party_cbxAcceptOnlyPartySetup;

		private GroupBox Party_gbxLeaderList;

		private ColumnHeader columnHeader15;

		public TextBox Party_tbxLeader;

		private Panel Party_pnlSetupExp;

		private GroupBox Party_gbxSetupExp;

		public CheckBox Party_cbxInviteOnlyPartySetup;

		public CheckBox Party_cbxInviteAll;

		public CheckBox Party_cbxInvitePartyList;

		private Panel Party_pnlSetupItem;

		private GroupBox Party_gbxSetupItem;

		public CheckBox Party_cbxSetupMasterInvite;

		public CheckBox Party_cbxLeavePartyNoneLeader;

		public RadioButton Party_rbnSetupExpFree;

		public RadioButton Party_rbnSetupItemFree;

		public RadioButton Party_rbnSetupExpShared;

		public RadioButton Party_rbnSetupItemShared;

		public Button Party_btnAddPlayer;

		public Button Party_btnAddLeader;

		public ListView Party_lstvPartyList;

		public ListView Party_lstvLeaderList;

		public NotifyIcon NotifyIcon;

		public Label Party_lblCurrentSetup;

		private ContextMenuStrip Menu_lstvPartyMembers;

		private ToolStripMenuItem Menu_lstvPartyMembers_AddTo;

		private ToolStripMenuItem Menu_lstvPartyMembers_AddToPartyList;

		private ToolStripMenuItem Menu_lstvPartyMembers_AddToLeaderList;

		private ColumnHeader columnHeader16;

		public ListView Party_lstvPartyMatch;

		private ColumnHeader columnHeader17;

		private ColumnHeader columnHeader18;

		private ColumnHeader columnHeader19;

		private ColumnHeader columnHeader20;

		private ColumnHeader columnHeader21;

		private ColumnHeader columnHeader22;

		public Button Party_btnRefreshMatch;

		public Button Party_btnLastPage;

		public Button Party_btnNextPage;

		public Label Party_lblPageNumber;

		public TextBox Party_tbxJoinToNumber;

		private Label Party_lblJoinToNumber;

		public Button Party_btnJoinMatch;

		private ToolStripMenuItem Menu_lstvPartyMembers_LeaveParty;

		public CheckBox Login_cbxGoClientless;

		public Label Character_lblAddINT;

		public Label Character_lblAddSTR;

		public Label Character_lblINT;

		public Label Character_lblSTR;

		public GroupBox Character_gbxStatPoints;

		public Button Character_btnAddSTR;

		public Button Character_btnAddINT;

		public Label Character_lblStatPoints;

		public CheckBox Character_cbxMessageEvents;

		public Label Character_lblJobLevel;

		public xProgressBar Character_pgbJobExp;

		public Label Character_lblGold;

		private Label Character_lblGoldText;

		private Label Character_lblSPText;

		public Label Character_lblSP;

		public Label Character_lblLocation;

		private Label Character_lblLocationText;

		public TextBox Character_tbxUseMP;

		public CheckBox Character_cbxUseMP;

		public CheckBox Character_cbxUseHPGrain;

		public TextBox Character_tbxUseHP;

		public CheckBox Character_cbxUseHP;

		public CheckBox Character_cbxUseMPGrain;

		public CheckBox Character_cbxUseHPVigor;

		public TextBox Character_tbxUseHPVigor;

		public TextBox Character_tbxUseMPVigor;

		public CheckBox Character_cbxUseMPVigor;

		public CheckBox Character_cbxUsePillPurification;

		public CheckBox Character_cbxUsePillUniversal;

		public CheckBox Character_cbxAcceptRessPartyOnly;

		public CheckBox Character_cbxAcceptRess;

		private ToolStripMenuItem Menu_rtbxPackets_AddTimestamp;

		private ContextMenuStrip Menu_lstvPartyList;

		private ToolStripMenuItem Menu_lstvPartyList_Remove;

		private ToolStripMenuItem Menu_lstvPartyList_RemoveAll;

		private ContextMenuStrip Menu_lstvLeaderList;

		private ToolStripMenuItem Menu_lstvLeaderList_Remove;

		private ToolStripMenuItem Menu_lstvLeaderList_RemoveAll;

		private Panel TabPageH_Inventory;

		private Button TabPageH_Inventory_Option04;

		private Button TabPageH_Inventory_Option03;

		private Button TabPageH_Inventory_Option02;

		private Button TabPageH_Inventory_Option01;

		private Panel TabPageH_Inventory_Option01_Panel;

		private Panel TabPageH_Inventory_Option03_Panel;

		private Panel TabPageH_Inventory_Option02_Panel;

		private Panel TabPageH_Inventory_Option04_Panel;

		public ListView Inventory_lstvItems;

		private ColumnHeader columnHeader23;

		private ColumnHeader columnHeader26;

		private ColumnHeader columnHeader25;

		private ColumnHeader columnHeader27;

		public Button Inventory_btnRefresh;

		private Label Inventory_lblCapacity;

		private ContextMenuStrip Menu_lstvPartyMatch;

		private ToolStripMenuItem Menu_lstvPartyMatch_JoinToParty;

		private ToolStripMenuItem Menu_lstvPartyMatch_PrivateMsg;

		public Button TabPageH_Chat_Option01;

		public Button TabPageH_Chat_Option04;

		public Button TabPageH_Chat_Option03;

		public Button TabPageH_Chat_Option02;

		public Button TabPageH_Chat_Option06;

		public Button TabPageH_Chat_Option05;

		public Button TabPageH_Chat_Option08;

		public Button TabPageH_Chat_Option07;

		public Label lblHeaderText02;

		public TextBox Character_tbxUsePetHP;

		public CheckBox Character_cbxUsePetHP;

		public GroupBox Character_gbxPotionsPlayer;

		public GroupBox Character_gbxPotionPet;

		public TextBox Character_tbxUseTransportHP;

		public CheckBox Character_cbxUseTransportHP;

		public CheckBox Character_cbxUsePetsPill;

		public CheckBox Character_cbxUsePetHGP;

		public TextBox Character_tbxUsePetHGP;

		public GroupBox Login_gbxAdvertising;

		public CheckBox Login_cbxRelogin;

		private PictureBox Login_pbxAds;

		public Label Character_lblCoordX;

		private Label Character_lblCoords;

		public Label Character_lblCoordY;

		private Panel TabPageH_Training;

		private Button TabPageH_Training_Option02;

		private Button TabPageH_Training_Option01;

		private Panel TabPageH_Training_Option01_Panel;

		private Panel TabPageH_Training_Option02_Panel;

		public xRichTextBox Settings_rtbxHWIDdata;

		private Button TabPageH_Training_Option03;

		private Panel TabPageH_Training_Option03_Panel;

		public GroupBox Training_gbxTrace;

		public ComboBox Training_cmbxTracePlayer;

		public Button Training_btnTraceStart;

		public TextBox Training_tbxTraceDistance;

		public CheckBox Training_cbxTraceDistance;

		private Label Training_lblTracePlayer;

		public CheckBox Training_cbxTraceMaster;

		public CheckBox Login_cbxUseReturnScroll;

		public TextBox Login_tbxCaptcha;

		private Label Login_lblCaptcha;

		public CheckBox Party_cbxActivateLeaderCommands;

		public TextBox Party_tbxMatchTitle;

		private Label Party_lblMatchTitle;

		private Panel Party_pnlAutoFormMatch;

		public TextBox Party_tbxMatchFrom;

		private Label Party_lblMatchFrom;

		private Label Party_lblMatchTo;

		public TextBox Party_tbxMatchTo;

		public CheckBox Party_cbxMatchAutoReform;

		public CheckBox Party_cbxMatchAcceptLeaderList;

		public CheckBox Party_cbxMatchAcceptPartyList;

		public CheckBox Party_cbxMatchAcceptAll;

		public CheckBox Party_cbxMatchRefuse;

		private ToolStripMenuItem Menu_lstvPartyMembers_KickPlayer;
		public CheckBox GameInfo_cbxPet;
		public CheckBox GameInfo_cbxDrop;
		public CheckBox GameInfo_cbxMob;
		public CheckBox GameInfo_cbxPlayer;
		public CheckBox GameInfo_cbxNPC;
		private Button GameInfo_btnRefresh;
		public CheckBox GameInfo_cbxOthers;
		public ListView Skills_lstvSkills;
		private ColumnHeader columnHeader34;
		private Panel TabPageH_Skills;
		private Button TabPageH_Skills_Option03;
		private Button TabPageH_Skills_Option02;
		private Button TabPageH_Skills_Option01;
		private Panel TabPageH_Skills_Option03_Panel;
		private Panel TabPageH_Skills_Option01_Panel;
		private Panel TabPageH_Skills_Option02_Panel;
		public ComboBox Skills_cmbxAttackMobType;
		public ListView Skills_lstvAttackMobType_General;
		private ColumnHeader columnHeader35;
		private ImageList lstimgIcons;
		public ListView Character_lstvBuffs;
		public ListView Skills_lstvAttackMobType_Elite;
		private ColumnHeader columnHeader40;
		public ListView Skills_lstvAttackMobType_PartyGiant;
		private ColumnHeader columnHeader39;
		public ListView Skills_lstvAttackMobType_PartyChampion;
		private ColumnHeader columnHeader38;
		public ListView Skills_lstvAttackMobType_PartyGeneral;
		private ColumnHeader columnHeader37;
		public ListView Skills_lstvAttackMobType_Giant;
		private ColumnHeader columnHeader36;
		public ListView Skills_lstvAttackMobType_Champion;
		private ColumnHeader columnHeader24;
		public ListView Skills_lstvAttackMobType_Unique;
		private ColumnHeader columnHeader42;
		public ListView Skills_lstvAttackMobType_Event;
		private ColumnHeader columnHeader41;
		private ToolStripSeparator Menu_lstvPartyMembers_Separator01;
		private ContextMenuStrip Menu_lstvItems;
		private ToolStripMenuItem Menu_lstvItems_Use;
		private ToolStripMenuItem Menu_lstvItems_Drop;
		private ToolStripSeparator Menu_lstvItems_Separator01;
		private ToolStripMenuItem Menu_lstvItems_Equip;
		public ListView Training_lstvAreas;
		private ColumnHeader columnHeader28;
		private ContextMenuStrip Menu_btnClientOptions;
		private ToolStripMenuItem Menu_btnClientOptions_ShowHide;
		private ToolStripMenuItem Menu_btnClientOptions_GoClientless;
		public Button Training_btnGetCoordinates;
		private Label Training_lblScriptPath;
		public TextBox Training_tbxScriptPath;
		private Label Training_lblRegion;
		public TextBox Training_tbxRegion;
		private Label Training_lblZ;
		public TextBox Training_tbxZ;
		private Label Training_lblY;
		public TextBox Training_tbxY;
		private Label Training_lblX;
		public TextBox Training_tbxX;
		private Button Training_btnLoadScriptPath;
	}
}

