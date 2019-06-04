using System;
using xBot.Network;
using SecurityAPI;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
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
		public bool LoginWithBot { get; set; }
		private bool _triggerJoinedToGame;
		private byte[] _HWID;
		/// <summary>
		/// Check if the HWID is loaded.
		/// </summary>
		public bool hasHWID { get { return _HWID != null; } }
		private string _SaveFrom;
		public string SaveHWIDFrom { get { return _SaveFrom; } }
		private string _SendTo;
		public string SendHWIDTo { get { return _SendTo; } }
		private int _HWIDLoadsCount;
		/// <summary>
		/// Check if the character is on game.
		/// </summary>
		public bool inGame {
			get { return _triggerJoinedToGame; }
		}
		private Bot()
		{
			_HWID = null;
    }
		public static Bot Get
		{
			get
			{
				if (_this == null)
					_this = new Bot();
				return _this;
			}
		}
		public void LogError(string error)
		{
			File.AppendAllText("dump.log", WindowsAPI.getDate() + error + Environment.NewLine);
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
		public void setHWID(ushort cOp,string SaveFrom, ushort sOp, string SendTo,bool SendOnlyOnce, string Data)
		{
			Agent.Opcode.CLIENT_HWID = cOp;
			Agent.Opcode.SERVER_HWID = sOp;
			_SaveFrom = SaveFrom;
			_SendTo = SendTo;
			_HWIDLoadsCount = SendOnlyOnce?-1:0;
			if (Data != "")
				_HWID = WindowsAPI.HexStringToBytes(Data.Replace(" ", ""));
		}
		public void SaveHWID(byte[] data)
		{
			_HWID = data;
			Window w = Window.Get;
			string hwid = WindowsAPI.BytesToHexString(data);
			Window.InvokeIfRequired(w.General_lstrSilkroads, () => {
				w.General_lstrSilkroads.Nodes[w.Login_cmbxSilkroad.Text].Nodes["HWID"].Nodes["Data"].Text = "Data : "+hwid;
				w.General_lstrSilkroads.Nodes[w.Login_cmbxSilkroad.Text].Nodes["HWID"].Nodes["Data"].Tag = hwid;
			});
			Settings.SaveBotSettings();
		}
		public byte[] LoadHWID()
		{
			if (hasHWID)
			{
				if (_HWIDLoadsCount >= -1)
				{
					if (_HWIDLoadsCount == -1)
						_HWIDLoadsCount = -2;
					_HWIDLoadsCount++;
					return _HWID;
				}
			}
			return null;
		}
		public void CloseSROClient()
		{
			Process p = WindowsAPI.getSROCientProcess();
			if (p != null)
				p.Kill();
		}
		#region (Events)
		private void Event_Connected()
		{

		}
		private void Event_Teleported()
		{
			if (inGame)
			{
				Window.Get.setState("Teleported");
			}
		}
		private void Event_JoinedToGame()
		{
			Window w = Window.Get;
			w.setState("In Game");
			w.Log("Joined successfully to the game");

		}
		#endregion
		#region (Event System Handler)
		public void _Event_Connected()
		{
			_triggerJoinedToGame = false;
			Event_Connected();
    }
		
		public void _Event_Teleported()
		{
			Event_Teleported();
			if (_triggerJoinedToGame == false)
			{
				_triggerJoinedToGame = true;
				Event_JoinedToGame();
			}
		}
		#endregion
	}
}