using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace xBot
{
	/// <summary>
	/// EdxLoader settings, used to call the C++ loader DLL injector (Thanks Drew!)
	/// </summary>
	public class EdxLoader
	{
		// Patches
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
		/// <param name="locale">Silkroad Type, Second argument used by the SR_Client</param>
		/// <param name="division">Division index, third argument used by the SR_Client</param>
		/// <param name="host">Host/IP Index,forth argument used by the SR_Client</param>
		/// <param name="Port">Port client redirection</param>
		/// <param name="redirectTo">Host client redirection</param>
		public void StartClient(bool useRandomValue,byte locale, int division,int host, int Port,string redirectTo = "127.0.0.1")
		{
			// Check if files are not missing
			string LoaderPath = Directory.GetCurrentDirectory() + "\\xBotLoader.exe";
			string DllPath = Directory.GetCurrentDirectory() + "\\xBotLoader.dll";
			if (!File.Exists(LoaderPath) || !File.Exists(DllPath))
			{
				Window w = Window.Get;
				WinAPI.InvokeIfRequired(w, () =>
				{
					System.Windows.Forms.MessageBox.Show(w, "xBotLoader file not found! Try to download everything again.", "xBot", System.Windows.Forms.MessageBoxButtons.OK);
				});
				return;
			}

			// Config used by the DLL file
			StringBuilder cfg = new StringBuilder();
			cfg.AppendLine("[Patches]");
			cfg.AppendLine("English_Patch=no");
			cfg.AppendLine("Multiclient=" + (MultiClient ? "yes" : "no"));
			cfg.AppendLine("Debug_Console=no");
			cfg.AppendLine("Swear_Filter="+(SwearFilter?"yes":"no"));
			cfg.AppendLine("Nude_Patch=no");
			cfg.AppendLine("Zoom_Hack=" + (ZoomHack ? "yes" : "no"));
			cfg.AppendLine("Korean_Captcha=no");
			cfg.AppendLine("No_Hackshield=no");
			cfg.AppendLine("Redirect_Gateway=yes");
			cfg.AppendLine("Redirect_Agent=no");
			cfg.AppendLine("Gateway_Ip="+ redirectTo);
			cfg.AppendLine("Gateway_Port="+Port);
			cfg.AppendLine("Agent_Ip=127.0.0.1");
			cfg.AppendLine("Agent_Port=20191");
			cfg.AppendLine("Hook_Input=no");
			cfg.AppendLine("Patch_Seed=no");
			cfg.AppendLine("Auto_Parse=no");
			cfg.AppendLine("KSRO_750=no");

			// Getting an private temporal app space
			string cfgPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ProjexNET";
			if (!Directory.Exists(cfgPath))
				Directory.CreateDirectory(cfgPath);
			cfgPath += "\\xBot";
			if (!Directory.Exists(cfgPath))
				Directory.CreateDirectory(cfgPath);
			cfgPath += "\\xBotLoader.ini";
			File.WriteAllText(cfgPath, cfg.ToString());
			
			// Execute EdxLoader
			Process Loader = new Process();
			Loader.StartInfo.FileName = LoaderPath;
			Loader.StartInfo.Arguments = (useRandomValue? "--userandom ":"")+"-locale "+ locale+" -division "+division+" -host "+host+" -path \""+ClientPath+"\"";
			Loader.Start();
		}
	}
}
