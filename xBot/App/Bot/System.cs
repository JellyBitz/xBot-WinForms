using System.IO;
using xBot.Game;
using xBot.Network;

namespace xBot
{
	public partial class Bot
	{
		#region (Event System Hooks & Game Logic Controller)
		public void _Event_Connected()
		{
			this.Event_Connected();
		}
		public void _Event_Disconnected()
		{
			_inGame = false;
			// Stop timers
			tUsingHP.Enabled = tUsingMP.Enabled = tUsingVigor.Enabled =
				tUsingUniversal.Enabled = tUsingPurification.Enabled = false;

			PartySetupType = PartyPurposeType = -1;

			LoggedFromBot = false;
			_HWIDSent = false;

			this.Event_Disconnected();
		}
		public void _Event_Teleported()
		{
			if (!_inGame)
			{
				Window w = Window.Get;
				string wTitle = "xBot - [" + Info.Get.Server + "] " + Info.Get.Charname;
				// Client title
				if (Proxy.SRO_Client != null)
					WinAPI.SetWindowText(Proxy.SRO_Client.MainWindowHandle, wTitle);
				// Bot title
				WinAPI.InvokeIfRequired(w, () => {
					w.Text = wTitle;
					w.NotifyIcon.Text = wTitle;
				});
				_inGame = true;
				Event_GameJoined();
			}
			this.Event_Teleported();
		}
		public void _Event_Teleporting()
		{
			Window w = Window.Get;
			Info i = Info.Get;

			// Clar entity data
			i.EntityList.Clear();
			w.GameInfo_Clear();
			w.Minimap_ObjectPointer_Clear();

			// Clear party data
			w.Party_Clear();
			i.PartyList.Clear();

			this.Event_Teleporting();
		}
		public void _Event_Spawn(SRObject entity)
		{
			Info.Get.EntityList.Add(entity);

			Window w = Window.Get;
			w.GameInfo_AddSpawn(entity);
			w.Minimap_ObjectPointer_Add((uint)entity[SRAttribute.UniqueID], (string)entity[SRAttribute.Servername], (string)entity[SRAttribute.Name], (int)entity[SRAttribute.X], (int)entity[SRAttribute.Y], (int)entity[SRAttribute.Z], (ushort)entity[SRAttribute.Region]);

			this.Event_Spawn(entity);
		}
		public void _Event_Despawn(uint uniqueID)
		{
			Info i = Info.Get;
			i.EntityList.Remove(i.GetEntity(uniqueID));

			Window w = Window.Get;
			w.GameInfo_RemoveSpawn(uniqueID);
			w.Minimap_ObjectPointer_Remove(uniqueID);
		}
		public void _Event_PartyLeaved()
		{
			PartySetupType = PartyPurposeType = -1;
			Info.Get.PartyList.Clear();
			Window.Get.Party_Clear();

			this.Event_PartyLeaved();
		}
		public void _Event_StateUpdated()
		{
			tUsingHP.Start();
			tUsingMP.Start();
			tUsingVigor.Start();
			tUsingUniversal.Start();
			tUsingPurification.Start();
			this.Event_StateUpdated();
		}
		public void _Event_EntitySelected(uint uniqueID)
		{
			_EntitySelected = uniqueID;
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
			Window.Get.LogProcess("HWID Detected : " + WinAPI.BytesToHexString(data));
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
