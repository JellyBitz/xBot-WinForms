using System;
using System.Collections.Generic;
using System.IO;
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
		public static void InvokeIfRequired(this Control control, MethodInvoker action)
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
		/// Converts the hexadecimal string to byte array.
		/// </summary>
		public static byte[] ToByteArray(this string HexadecimalString)
		{
			HexadecimalString = HexadecimalString.Replace(" ", "");
			byte[] result = new byte[HexadecimalString.Length / 2];
			int j = 0;
      for (int i = 0; i < HexadecimalString.Length; i+=2)
				result[j++] = Convert.ToByte(HexadecimalString.Substring(i, 2), 16);
			return result;
    }
		/// <summary>
		/// StreamReader support for using differents limiters.
		/// </summary>
		public static string ReadLine(this StreamReader StreamReader, string delimiter)
		{
			char nextChar;
			StringBuilder line = new StringBuilder();
			int matchIndex = 0;

			while (StreamReader.Peek() > 0)
			{
				nextChar = (char)StreamReader.Read();
				line.Append(nextChar);
				if (nextChar == delimiter[matchIndex])
				{
					if (matchIndex == delimiter.Length - 1)
					{
						return line.ToString().Substring(0, line.Length - delimiter.Length);
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
		public static void Shuffle<T>(this List<T> List)
		{
			Random rand = new Random();

			int n = List.Count;
			while (n > 1)
			{
				n--;
				int k = rand.Next(n + 1);
				T value = List[k];
				List[k] = List[n];
				List[n] = value;
			}
		}
		/// <summary>
		/// Restart timer with a new Interval.
		/// </summary>
		public static void ResetTimer(this System.Timers.Timer Timer, double newInterval)
		{
			Timer.Stop();
			Timer.Interval = newInterval;
			Timer.Start();
		}
		/// <summary>
		/// Try to delete a file, return success.
		/// </summary>
		public static bool FileTryDelete(string Path)
		{
			try
			{
				File.SetAttributes(Path, FileAttributes.Normal);
				File.Delete(Path);
				return true;
			}
			catch { return false; }
		}
		/// <summary>
		/// Try to delete a directory with all his files recursively.
		/// </summary>
		public static void DirectoryTryDelete(string Path)
		{
			// Delete every file
			string[] temp = Directory.GetFiles(Path);
			foreach (string file in temp)
			{
				FileTryDelete(file);
      }
			// Check every folder inside Path recursively
			temp = Directory.GetDirectories(Path);
			foreach (string directory in temp)
				DirectoryTryDelete(directory);

			// Delete Path folder
			try {
				Directory.Delete(Path, false);
			}catch { }
		}
		#endregion
	}
}