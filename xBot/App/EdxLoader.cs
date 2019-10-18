using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace xBot.App
{
	/// <summary>
	/// EdxLoader, used to create the process and inject the unmanaged DLL. (Thanks Drew!)
	/// </summary>
	public class EdxLoader
	{
		#region (Process edition)
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int CloseHandle(IntPtr hObject);
		/// <summary>
		/// Privileges.
		/// </summary>
		const int
			PROCESS_CREATE_THREAD = 0x0002,
			PROCESS_QUERY_INFORMATION = 0x0400,
			PROCESS_VM_OPERATION = 0x0008,
			PROCESS_VM_WRITE = 0x0020,
			PROCESS_VM_READ = 0x0010;
		/// <summary>
		/// Memory allocation.
		/// </summary>
		const uint
			MEM_COMMIT = 0x00001000,
			MEM_RESERVE = 0x00002000,
			PAGE_READWRITE = 4;
		#endregion

		// DLL Patches
		private string ClientPath;
		private bool
			ZoomHack,
			MultiClient,
			SwearFilter;
		public EdxLoader(string ClientPath)
		{
			this.ClientPath = ClientPath;
		}
		/// <summary>
		///  Set patches supported by edxLoader v5.0.3d
		/// </summary>
		/// <param name="ZoomHack">Zoom without limits the game portview</param>
		/// <param name="MultiClient">Avoid multiples SR_Client restriction</param>
		/// <param name="SwearFilter">Avoid bad words chat restriction</param>
		public void SetPatches(bool ZoomHack,bool MultiClient,bool SwearFilter)
		{
			this.ZoomHack = ZoomHack;
			this.MultiClient = MultiClient;
			this.SwearFilter = SwearFilter;
		}
		/// <summary>
		/// Start the executable used to handle all process about DLL injection.
		/// </summary>
		/// <param name="useRandomValue">Using a random value as first argument taken by the SR_Client</param>
		/// <param name="Locale">Silkroad Type, Second argument used by the SR_Client</param>
		/// <param name="Division">Division index, third argument used by the SR_Client</param>
		/// <param name="HostIndex">Host/IP Index,forth argument used by the SR_Client</param>
		/// <param name="RedirectingPort">Port client redirection</param>
		/// <param name="RedirectingHost">Host client redirection</param>
		public Process StartClient(bool useRandomValue,byte Locale, int Division,int HostIndex, int RedirectingPort,string RedirectingHost = "127.0.0.1")
		{
			// Check if files are not missing
			string LoaderPath = Directory.GetCurrentDirectory() + "\\xBotLoader.exe";
			string DllPath = Directory.GetCurrentDirectory() + "\\xBotLoader.dll";
			if (!File.Exists(LoaderPath) || !File.Exists(DllPath))
			{
				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w, () =>{
					System.Windows.Forms.MessageBox.Show(w, "xBotLoader files not found! Try to download it again.", "xBot", System.Windows.Forms.MessageBoxButtons.OK);
				}); 
				return null;
			}
			// Generate config used by the DLL
			CreateDLLSetup(RedirectingHost,RedirectingPort);
			
			// Execute EdxLoader
			Process Loader = new Process();
			Loader.StartInfo.FileName = LoaderPath;
			Loader.StartInfo.Arguments = (useRandomValue ? "--userandom " : "") + "-locale " + Locale + " -division " + Division + " -host " + HostIndex + " -path \"" + ClientPath + "\"";
			Loader.Start();

			Loader.WaitForExit();
			if (Loader.ExitCode > 0)
				return Process.GetProcessById(Loader.ExitCode);
			return null;
		}
		private void CreateDLLSetup(string RedirectingHost, int RedirectingPort)
		{
			StringBuilder cfg = new StringBuilder();
			cfg.AppendLine("[Patches]");
			cfg.AppendLine("English_Patch=no");
			cfg.AppendLine("Multiclient=" + (MultiClient ? "yes" : "no"));
			cfg.AppendLine("Debug_Console=no");
			cfg.AppendLine("Swear_Filter=" + (SwearFilter ? "yes" : "no"));
			cfg.AppendLine("Nude_Patch=no");
			cfg.AppendLine("Zoom_Hack=" + (ZoomHack ? "yes" : "no"));
			cfg.AppendLine("Korean_Captcha=no");
			cfg.AppendLine("No_Hackshield=no");
			cfg.AppendLine("Redirect_Gateway=yes");
			cfg.AppendLine("Redirect_Agent=no");
			cfg.AppendLine("Gateway_Ip=" + RedirectingHost);
			cfg.AppendLine("Gateway_Port=" + RedirectingPort);
			cfg.AppendLine("Agent_Ip=127.0.0.1");
			cfg.AppendLine("Agent_Port=20191");
			cfg.AppendLine("Hook_Input=no");
			cfg.AppendLine("Patch_Seed=no");
			cfg.AppendLine("Auto_Parse=no");
			cfg.AppendLine("KSRO_750=no");

			// Getting a private temporal app space
			string cfgPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\xBot";
			if (!Directory.Exists(cfgPath))
			{
				Directory.CreateDirectory(cfgPath);
			}
			cfgPath += "\\xBotLoader.ini";
			File.WriteAllText(cfgPath, cfg.ToString());
		}
	}
}
