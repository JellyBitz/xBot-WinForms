using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace xBot.App
{
	/// <summary>
	/// API static class to handle windows methods and utility extensions.
	/// </summary>
	public static class WinAPI
	{
		#region (DLL Imports)
		[DllImport("gdi32.dll")]
		public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,IntPtr pdv, [In] ref uint pcFonts);
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
		[DllImport("psapi.dll")]
		public static extern bool EmptyWorkingSet(IntPtr hProcess);
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr parentWindow, IntPtr previousChildWindow, string windowClass, string windowTitle);
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowThreadProcessId(IntPtr window, out int process);
		[DllImport("user32.dll")]
		public static extern int SetWindowText(IntPtr hWnd, string text);
		#endregion

		#region (Utility methods)
		/// <summary>
		/// Gets the current date in constant format like "[hh:mm:ss]".
		/// </summary>
		public static string GetDate()
		{
			return "[" + DateTime.Now.ToString("hh:mm:ss")+"]";
		}
		/// <summary>
		/// Invoque the control if is required, then do the action specified.
		/// </summary>
		public static void InvokeIfRequired(Control control, MethodInvoker action)
		{
			try
			{
				if (control.InvokeRequired)
					control.Invoke(action);
				else
					action();
			}catch{
				// Control closed
			}
		}
		public static IntPtr[] GetProcessWindows(int ProcessId)
		{
			IntPtr[] apRet = (new IntPtr[256]);
			int iCount = 0;
			IntPtr pLast = IntPtr.Zero;
			do
			{
				pLast = FindWindowEx(IntPtr.Zero, pLast, null, null);
				int iProcess_;
				GetWindowThreadProcessId(pLast, out iProcess_);
				if (iProcess_ == ProcessId)
					apRet[iCount++] = pLast;
			} while (pLast != IntPtr.Zero);
			Array.Resize(ref apRet, iCount);
			return apRet;
		}
		/// <summary>
		/// Converts the byte array to hexadecimal string.
		/// </summary>
		/// <para>Example: "00 00 00 00" </para>
		public static string ToHexString(byte[] bytes)
		{
			if (bytes.Length == 0)
				return "";
			StringBuilder hexData = new StringBuilder();
			foreach (byte b in bytes)
				hexData.Append(b.ToString("X2") + " ");
			// remove the last empty space
			return hexData.Remove(hexData.Length - 1, 1).ToString();
		}
		/// <summary>
		/// Converts the hexadecimal string to byte array.
		/// </summary>
		public static byte[] ToByteArray(string hexString)
		{
			hexString = hexString.Replace(" ", "");
			return Enumerable.Range(0, hexString.Length)
											 .Where(x => x % 2 == 0)
											 .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
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
		/// <summary>
		/// Shuffle a list using Fisher-Yates algorithm.
		/// </summary>
		public static List<T> GetShuffle<T>(List<T> List,Random randomize)
		{
			int n = List.Count;
			while (n > 1)
			{
				n--;
				int k = randomize.Next(n + 1);
				T value = List[k];
				List[k] = List[n];
				List[n] = value;
			}
			return List;
		}

		/// <summary>
		/// Restart timer with a new Interval.
		/// </summary>
		public static void ResetTimer(ref System.Timers.Timer Timer, double newInterval)
		{
			Timer.Stop();
			Timer.Interval = newInterval;
			Timer.Start();
		}
		/// <summary>
		/// Delete a directory and all his files recursively.
		/// </summary>
		public static void DirectoryDelete(string Path)
		{
			string[] files = Directory.GetFiles(Path);
			string[] dirs = Directory.GetDirectories(Path);

			foreach (string file in files)
			{
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			foreach (string dir in dirs)
			{
				DirectoryDelete(dir);
			}

			Directory.Delete(Path, false);
		}
		#endregion
	}
}