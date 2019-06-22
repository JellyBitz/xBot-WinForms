using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using xBot.Game;

namespace xBot
{
	/// <summary>
	/// Handle everything about Bot logic.
	/// </summary>
	public class Bot
	{
		private static Bot _this = null;
		/// <summary>
		/// Keep reference from commandLine arguments.
		/// </summary>
		public bool isAutoLogin { get; set; }
		public Proxy Proxy { get; set; }
		public string ClientPath { get; set; }
		public bool LoginFromBot { get; set; }
		/// <summary>
		/// Check if the HWID is loaded.
		/// </summary>
		public bool hasHWID { get { return _HWID != null; } }
		private byte[] _HWID;
		public string SaveHWIDFrom { get { return _SaveFrom; } }
		private string _SaveFrom;
		public string SendHWIDTo { get { return _SendTo; } }
		private string _SendTo;
		private int _HWIDLoadsCounter;
		/// <summary>
		/// Check if the character is in game.
		/// </summary>
		public bool inGame {
			get { return _triggerJoinedToGame; }
		}
		private bool _triggerJoinedToGame;
		private Bot()
		{
			_HWID = null;
    }
		/// <summary>
		/// GetInstance. Secures an unique class creation for being used anywhere at the project.
		/// </summary>
		public static Bot Get
		{
			get
			{
				if (_this == null)
					_this = new Bot();
				return _this;
			}
		}
		public void LogError(string error,Packet p = null)
		{
			string msg = WinAPI.getDate() + error+Environment.NewLine;
			if (p != null)
				msg += "["+p.Opcode.ToString("X4")+"]["+WinAPI.BytesToHexString(p.GetBytes())+"]" + Environment.NewLine;
			File.AppendAllText("dump.log", msg);
		}
		public string getRandomCharname()
		{
			string nick = "";
			List<string> names = new List<string>();
			names.AddRange(new string[] { "Han", "Sol", "Je", "Lan", "Tuk", "Zen", "Jin", "Xan", "Xen", "Xin", "Za", "Ke", "Zoh", "Kin", "Zan", "Zu", "Lid", "Yek", "Ri", "Riu", "Ruk", "Vi", "Vik", "Ki", "Yi", "Bok", "Kah", "Khan", "War", "Ten", "Fu", "Fy", "Wan", "Wi", "Win", "Lin", "Ran", "Min", "Ez", "Kra", "Ken" });
			Random rand = new Random();
			for (int i = 0; i < 4; i++)
			{
				int select = rand.Next(0, names.Count);
				nick += names[select];
				names.RemoveAt(select);
			}
			return nick;
		}
		/// <summary>
		/// Set the HWID setup.
		/// </summary>
		/// <param name="cOp">Client opcode used to save the HWID packet</param>
		/// <param name="SaveFrom">Save from Gateway/Server/Both</param>
		/// <param name="sOp">Server opcode used to send the HWID packet</param>
		/// <param name="SendTo">Send to from Gateway/Server/Both</param>
		/// <param name="SendOnlyOnce">Send HWID packet only once</param>
		/// <param name="Data">Packet string format</param>
		public void SetHWID(ushort cOp,string SaveFrom, ushort sOp, string SendTo,bool SendOnlyOnce)
		{
			Agent.Opcode.CLIENT_HWID = Gateway.Opcode.CLIENT_HWID = cOp;
			Agent.Opcode.SERVER_HWID = Gateway.Opcode.SERVER_HWID = sOp;
			_SaveFrom = SaveFrom;
			_SendTo = SendTo;
			_HWIDLoadsCounter = SendOnlyOnce?-1:0;
		}
		public void SaveHWID(byte[] data)
		{
			Window.Get.LogProcess("HWID Detected : " + WinAPI.BytesToHexString(data));
			File.WriteAllBytes("Data\\" + Info.Get.Silkroad+ ".hwid", data);
		}
		public byte[] LoadHWID()
		{
			if (File.Exists("Data\\"+ Info.Get.Silkroad + ".hwid"))
			{
				if (_HWIDLoadsCounter >= -1)
				{
					if (_HWIDLoadsCounter == -1)
						_HWIDLoadsCounter = -2;
					_HWIDLoadsCounter++;
					return File.ReadAllBytes("Data\\" + Info.Get.Silkroad + ".hwid");
				}
			}
			return null;
		}
		public void CloseSROClient()
		{
			Process p = WinAPI.getSROCientProcess();
			if (p != null)
				p.Kill();
		}
		#region (Game & Bot Events)
		/// <summary>
		/// Called when the account has been logged succesfully and the Agent has been connected.
		/// </summary>
		private void Event_Connected()
		{

		}
		/// <summary>
		/// Called right before all character data is saved & spawn packet is detected.
		/// </summary>
		private void Event_Teleported()
		{
			if (inGame)
			{
				Window.Get.LogProcess("Teleported");
			}
			// Recommended to wait 10 seconds to do some action

		}
		/// <summary>
		/// Just before <see cref="Event_Teleported"/> is called. Generated only once per character login.
		/// </summary>
		private void Event_JoinedToGame()
		{
			Window w = Window.Get;
			w.LogProcess("In Game");
			w.Log("Joined successfully to the game");

			Info i = Info.Get;
			Settings.LoadCharacterSettings(i.Silkroad,i.Server,i.Charname);
		}
		/// <summary>
		/// Called when the Health or Mana from the character has changed.
		/// </summary>
		public void Event_BarUpdated()
		{
			// Check for pots, skills, etc..
		}
		/// <summary>
		/// Called only when the maximum level has been increased.
		/// </summary>
		public void Event_LevelUp()
		{
			// Up stats points, skills, etc..
		}
		#endregion
		#region (Event Hooks & System handler)
		public void _Event_Connected()
		{
			_triggerJoinedToGame = false;
			Event_Connected();
    }
		
		public void _Event_Teleported()
		{
			// Reset data saved previously
			Info.Get.EntityList.Clear();
			Window.Get.Minimap_ObjectPointer_Clear();
			Window.Get.TESTING_Clear();
      if (_triggerJoinedToGame == false)
			{
				_triggerJoinedToGame = true;
				Event_JoinedToGame();
			}
			Event_Teleported();
		}
		#endregion
	}
}