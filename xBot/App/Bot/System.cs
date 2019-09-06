using System.IO;
using xBot.Game;
using xBot.Network;
using System.Timers;

namespace xBot
{
	public partial class Bot
	{
		#region (Event System Hooks & Game Logic Controller)
		/// <summary>
		/// Cooldown timer.
		/// </summary>
		Timer tUsingHP, tUsingMP, tUsingVigor,
			tUsingUniversal, tUsingPurification,
			tAutoParty, tUsingRecoveryKit;

		private void InitializeTimers()
		{
			// Preparing all neccesary timers
			tUsingHP = new Timer();
			tUsingMP = new Timer();
			tUsingVigor = new Timer();
			tUsingUniversal = new Timer();
			tUsingPurification = new Timer();
			tAutoParty = new Timer();
			tUsingRecoveryKit = new Timer();

			// A second is enought for any potion cooldown
			tUsingHP.Interval = tUsingMP.Interval = tUsingVigor.Interval =
			tUsingUniversal.Interval = tUsingPurification.Interval = 
			tUsingRecoveryKit.Interval = 1000;

			tAutoParty.Interval = 5000;

			// Callbacks
			tUsingHP.Elapsed += CheckUsingHP;
			tUsingMP.Elapsed += CheckUsingMP;
			tUsingVigor.Elapsed += CheckUsingVigor;
			tUsingUniversal.Elapsed += CheckUsingUniversal;
			tUsingPurification.Elapsed += CheckUsingPurification;
			tAutoParty.Elapsed += CheckAutoParty;
			tUsingRecoveryKit.Elapsed += CheckUsingRecoveryKit;
		}
		public void _Event_Connected()
		{
			this.Event_Connected();
		}
		public void _Event_CharacterListing(System.Collections.Generic.List<SRObject> CharacterList)
		{
			Window w = Window.Get;
			if (hasAutoLoginMode)
			{
				WinAPI.InvokeIfRequired(w, () => {
					if (w.Login_cmbxCharacter.Text != "")
					{
						w.Control_Click(w.Login_btnStart, null);
						return;
					}
				});
			}
			else
			{
				// Select first one (Just for UX)
				if (w.Login_cmbxCharacter.Items.Count > 0)
				{
					WinAPI.InvokeIfRequired(w.Login_cmbxCharacter, () => {
						w.Login_cmbxCharacter.SelectedIndex = 0;
					});
				}
			}

			this.Event_CharacterListing(CharacterList);
		}
		public void _Event_Disconnected()
		{
			_inGame = false;
			_hasStall = false;

			Window w = Window.Get;
			Info i = Info.Get;

			// Reset data
			i.Character = null;
			
			EntitySelected = 0;
			i.EntityList.Clear();
			w.Minimap_ObjectPointer_Clear();
			
			PartySetupType = PartyPurposeType = -1;
			i.PartyList.Clear();
			w.Party_Clear();

			// Stop timers
			tUsingHP.Enabled = tUsingMP.Enabled = tUsingVigor.Enabled =
				tUsingUniversal.Enabled = tUsingPurification.Enabled =
				tAutoParty.Enabled = false;

			LoggedFromBot = false;
			_HWIDSent = false;

			this.Event_Disconnected();
		}
		public void _Event_NicknameChecked(bool success)
		{
			if (isCreatingCharacter)
			{
				if (success)
				{
					Window.Get.Log("Nickname available!");
					CreateCharacter();
				}
				else
				{
					Window.Get.Log("Nickname has been already taken!");
					CreateNickname();
				}
			}
		}
		public void _Event_Teleported()
		{
			Info i = Info.Get;
			if (!_inGame)
			{
				Window.Get.SetTitle(i.Server, i.Charname, Proxy.SRO_Client);
				_inGame = true;
				Event_GameJoined();
			}

			this.Event_Teleported();
		}
		public void _Event_Teleporting()
		{
			Window w = Window.Get;
			Info i = Info.Get;

			// Reset entity data
			EntitySelected = 0;
			i.Pets.Clear();
			i.EntityList.Clear();
			w.Minimap_ObjectPointer_Clear();

			// Reset party data
			PartySetupType = PartyPurposeType = -1;
			i.PartyList.Clear();
			w.Party_Clear();

			this.Event_Teleporting();
		}
		public void _Event_Spawn(SRObject entity)
		{
			Info i = Info.Get;
			uint uniqueID = (uint)entity[SRAttribute.UniqueID];
			i.EntityList[uniqueID] = entity;

			Window.Get.Minimap_ObjectPointer_Add((uint)entity[SRAttribute.UniqueID], (string)entity[SRAttribute.Servername], (string)entity[SRAttribute.Name], (int)entity[SRAttribute.X], (int)entity[SRAttribute.Y], (int)entity[SRAttribute.Z], (ushort)entity[SRAttribute.Region]);

			this.Event_Spawn(entity);
		}
		public void _Event_Despawn(uint uniqueID)
		{
			Info i = Info.Get;
			i.EntityList.Remove(uniqueID);

			Window.Get.Minimap_ObjectPointer_Remove(uniqueID);
		}
		public void _Event_PartyLeaved()
		{
			PartySetupType = PartyPurposeType = -1;
			Info.Get.PartyList.Clear();
			Window.Get.Party_Clear();

			this.Event_PartyLeaved();
		}
		public void _Event_StateUpdated(Types.EntityStateUpdate type)
		{
			// Update GUI bars
			Window w = Window.Get;
			switch (type)
			{
				case Types.EntityStateUpdate.HP:
					WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
						w.Character_pgbHP.Value = (uint)Info.Get.Character[SRAttribute.HP];
					});
					break;
				case Types.EntityStateUpdate.MP:
					WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
						w.Character_pgbMP.Value = (uint)Info.Get.Character[SRAttribute.MP];
					});
					break;
				case Types.EntityStateUpdate.HPMP:
					WinAPI.InvokeIfRequired(w.Character_pgbHP, () => {
						w.Character_pgbHP.Value = (uint)Info.Get.Character[SRAttribute.HP];
					});
					WinAPI.InvokeIfRequired(w.Character_pgbMP, () => {
						w.Character_pgbMP.Value = (uint)Info.Get.Character[SRAttribute.MP];
					});
					break;
			}
			this.Event_StateUpdated(type);
		}
		public void _Event_PetSummoned(uint uniqueID)
		{
			Info i = Info.Get;
			i.Pets[uniqueID] = i.EntityList[uniqueID];
		}
		public void _Event_PetUnsummoned(uint uniqueID)
		{
			Info.Get.Pets.Remove(uniqueID);
		}
		public void _Event_PetStateUpdated(uint uniqueID,Types.EntityStateUpdate type)
		{
			SRObject pet = Info.Get.GetEntity(uniqueID);
			if(type == Types.EntityStateUpdate.EntityHPMP)
			{
				if (pet.ID4 == 1 || pet.ID4 == 2 || pet.ID4 == 3)
					CheckUsingRecoveryKit();
			}
			else if (type == Types.EntityStateUpdate.BadStatus)
			{
				if (pet.ID4 == 2)
				{

				}
				else if (pet.ID4 == 3)
				{

				}
			}
		}
    public void _Event_EntitySelected(uint uniqueID)
		{
			EntitySelected = uniqueID;
		}
		public void _Event_MemberLeaved(uint memberID)
		{
			Info i = Info.Get;

			SRObject player = i.GetPartyMember(memberID);
			if ((string)player[SRAttribute.Name] == i.Charname)
			{
				_Event_PartyLeaved();
			}
			else
			{
				i.PartyList.Remove(player);

				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w.Party_lstvPartyMembers, () => {
					w.Party_lstvPartyMembers.Items[memberID.ToString()].Remove();
				});
				Event_MemberLeaved();
			}
		}
		public void _Event_StallOpened(bool isMine)
		{
			_inStall = true;
			if (isMine)
				_hasStall = true;
		}
		public void _Event_StallClosed()
		{
			_inStall = _hasStall = false;
		}
		#endregion

		#region (HWID setup)
		/// <summary>
		/// Gets the context type to save the HWID (Can be Gateway, Agent, or Both)
		/// </summary>
		public string HWIDSaveFrom { get { return _HWIDSaveFrom; } }
		private string _HWIDSaveFrom;
		/// <summary>
		/// Gets the context type to send the HWID (Can be Gateway, Agent, or Both)
		/// </summary>
		public string HWIDSendTo { get { return _HWIDSendTo; } }
		private string _HWIDSendTo;
		private bool _HWIDSent;
		private bool _HWIDSendOnlyOnce;
		/// <summary>
		/// Set the HWID setup.
		/// </summary>
		/// <param name="cOp">Client opcode used to save the HWID packet</param>
		/// <param name="SaveFrom">Save from Gateway/Server/Both</param>
		/// <param name="sOp">Server opcode used to send the HWID packet</param>
		/// <param name="SendTo">Send to from Gateway/Server/Both</param>
		/// <param name="SendOnlyOnce">Send HWID packet only once</param>
		/// <param name="Data">Packet string format</param>
		public void SetHWID(ushort cOp, string SaveFrom, ushort sOp, string SendTo, bool SendOnlyOnce)
		{
			Agent.Opcode.CLIENT_HWID_RESPONSE = Gateway.Opcode.CLIENT_HWID_RESPONSE = cOp;
			Agent.Opcode.SERVER_HWID_REQUEST = Gateway.Opcode.SERVER_HWID_REQUEST = sOp;
			_HWIDSaveFrom = SaveFrom;
			_HWIDSendTo = SendTo;
			_HWIDSendOnlyOnce = SendOnlyOnce;
			_HWIDSent = false;
		}
		/// <summary>
		/// Saves the hwid data to be used later.
		/// </summary>
		public void SaveHWID(byte[] data)
		{
			Window.Get.LogProcess("HWID Detected : " + WinAPI.ToHexString(data));
			File.WriteAllBytes("Data\\" + Info.Get.Silkroad + ".hwid", data);
		}
		/// <summary>
		/// Loads the HWID previously saved. Returns null if is not found.
		/// </summary>
		public byte[] LoadHWID()
		{
			if (_HWIDSendOnlyOnce && _HWIDSent)
				return null;
			if (File.Exists("Data\\" + Info.Get.Silkroad + ".hwid"))
			{
				_HWIDSent = true;
				return File.ReadAllBytes("Data\\" + Info.Get.Silkroad + ".hwid");
			}
			return null;
		}
		#endregion
	}
}
