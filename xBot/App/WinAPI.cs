using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xBot.Network;

namespace xBot
{
	/// <summary>
	/// API static class to handle windows methods and utility extensions.
	/// </summary>
	public static class WinAPI
	{
		#region (DLL Imports)
		[DllImport("gdi32.dll")]
		public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
						IntPtr pdv, [In] ref uint pcFonts);
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		public const int WM_VSCROLL = 277;
		public const int SB_PAGEBOTTOM = 7;
		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 2;
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		public const int SW_HIDE = 0;
		public const int SW_SHOW = 5;
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr parentWindow, IntPtr previousChildWindow, string windowClass, string windowTitle);
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowThreadProcessId(IntPtr window, out int process);
		#endregion
		/// <summary>
		/// Gets the current date in constant format like "[hh:mm:ss]".
		/// </summary>
		public static string getDate()
		{
			return "[" + (DateTime.Now.Hour < 10 ? "0" : "") + DateTime.Now.Hour + ":" + (DateTime.Now.Minute < 10 ? "0" : "") + DateTime.Now.Minute + ":" + (DateTime.Now.Second < 10 ? "0" : "") + DateTime.Now.Second + "]";
		}
		public static void InvokeIfRequired(Control control, MethodInvoker action)
		{
			if (control.InvokeRequired)
				control.Invoke(action);
			else
				action();
		}
		/// <summary>
		/// Returns SRO_Client process that is using the current agent/gateway port.
		/// </summary>
		/// <returns></returns>
		public static Process getSROCientProcess()
		{
			try
			{
				int gatewayPort = -1, agentPort = -1;
				try
				{
					if (Bot.Get.Proxy.Gateway != null && Bot.Get.Proxy.Gateway.Local != null && Bot.Get.Proxy.Gateway.Local.Socket != null)
						gatewayPort = int.Parse(Bot.Get.Proxy.Gateway.Local.Socket.RemoteEndPoint.ToString().Split(':')[1]);
				}
				catch{ }
				try
				{
					if (Bot.Get.Proxy.Agent != null && Bot.Get.Proxy.Agent.Local != null && Bot.Get.Proxy.Agent.Local.Socket != null)
						agentPort = int.Parse(Bot.Get.Proxy.Agent.Local.Socket.RemoteEndPoint.ToString().Split(':')[1]);
				}
				catch{ }
				if (gatewayPort == -1 && agentPort == -1)
					return null;

				Process p = new Process();
				ProcessStartInfo ps = new ProcessStartInfo();
				ps.FileName = "netstat.exe";
				ps.Arguments = "-a -n -o";
				ps.UseShellExecute = false;
				ps.CreateNoWindow = true;
				ps.WindowStyle = ProcessWindowStyle.Hidden;
				ps.RedirectStandardInput = true;
				ps.RedirectStandardOutput = true;
				ps.RedirectStandardError = true;
				p.StartInfo = ps;
				p.Start();

				StreamReader stdOutput = p.StandardOutput;
				StreamReader stdError = p.StandardError;

				string content = stdOutput.ReadToEnd() + stdError.ReadToEnd();
				string exitStatus = p.ExitCode.ToString();

				if (exitStatus != "0")
				{
					// Command Errored. Handle Here If Need Be
				}

				//Get The Rows
				string[] rows = Regex.Split(content, "\r\n");
				foreach (string row in rows)
				{
					//Split it baby
					string[] tokens = Regex.Split(row, "\\s+");
					if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
					{
						try
						{
							string localAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");
							int pID = tokens[1] == "UDP" ? Convert.ToInt16(tokens[4]) : Convert.ToInt16(tokens[5]);
							Process process = Process.GetProcessById(pID);
							int processPort = int.Parse(localAddress.Split(':')[1]);
							if (!process.ProcessName.ToLower().StartsWith("xbot"))
							{
								if (processPort == gatewayPort || processPort == agentPort )
								{
									return process;
								}
							}
						}catch { }
					}
				}
			}
			catch { }
			return null;
		}
		public static IntPtr[] GetProcessWindows(int process)
		{
			IntPtr[] apRet = (new IntPtr[256]);
			int iCount = 0;
			IntPtr pLast = IntPtr.Zero;
			do
			{
				pLast = FindWindowEx(IntPtr.Zero, pLast, null, null);
				int iProcess_;
				GetWindowThreadProcessId(pLast, out iProcess_);
				if (iProcess_ == process) apRet[iCount++] = pLast;
			} while (pLast != IntPtr.Zero);
			System.Array.Resize(ref apRet, iCount);
			return apRet;
		}
		public static string BytesToHexString(byte[] bytes)
		{
			if (bytes.Length == 0)
				return "";
			StringBuilder hexData = new StringBuilder();
			foreach (byte b in bytes)
				hexData.Append(b.ToString("X2") + " ");
			// remove the last empty space
			return hexData.Remove(hexData.Length - 1, 1).ToString();
		}
		public static byte[] HexStringToBytes(string hex)
		{
			return Enumerable.Range(0, hex.Length)
											 .Where(x => x % 2 == 0)
											 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
											 .ToArray();
		}
		/// <summary>
		/// StreamReader extension support for using differents limiters.
		/// </summary>
		/// <param name="sr">StreamReader loaded</param>
		/// <param name="SplitString">Delemiters</param>
		public static string ReadToString(StreamReader sr, string split)
		{
			char nextChar;
			StringBuilder line = new StringBuilder();
			int matchIndex = 0;

			while (sr.Peek() > 0)
			{
				nextChar = (char)sr.Read();
				line.Append(nextChar);
				if (nextChar == split[matchIndex])
				{
					if (matchIndex == split.Length - 1)
					{
						return line.ToString().Substring(0, line.Length - split.Length);
					}
					matchIndex++;
				}
				else
				{
					matchIndex = 0;
				}
			}
			return line.Length == 0 ? null : line.ToString();
		}
	}
}