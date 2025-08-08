using System;
using System.Globalization;
using System.Windows.Forms;
using xBot.App;

namespace xBot
{
	static class Program
	{
		/// <summary>
		/// Punto de entrada principal para la aplicación.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Set  default locale for thread/ui as English
			// made just to avoid tolower/toupper issues with the app in other locales
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(Window.Get);
		}
	}
}
