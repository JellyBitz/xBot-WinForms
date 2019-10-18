using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace xBot.App
{
	public class Fonts
	{
		private static PrivateFontCollection myFonts = null;
		private static void LoadFonts()
		{
			myFonts = new PrivateFontCollection();
			string[] fontFilenames = new string[]
			{
				"fa_regular_400",
				"fa_solid_900",
				"fa_light_300",
				"fa_brands_400",
				"source_sans_pro_regular"
			};
			for (int i = 0; i < fontFilenames.Length; i++)
			{
				byte[] fontData = (byte[])(Properties.Resources.ResourceManager.GetObject(fontFilenames[i]));
				IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
				Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
				uint dummy = 0;
				myFonts.AddMemoryFont(fontPtr, fontData.Length);
				WinAPI.AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref dummy);
				Marshal.FreeCoTaskMem(fontPtr);
			}
		}
		public static Font GetFont(Font prototype, string fontName = "")
		{
			if (myFonts == null)
				LoadFonts();
			if (string.IsNullOrEmpty(fontName))
				fontName = prototype.FontFamily.Name;
			for (int i = 0; i < myFonts.Families.Length; i++)
				if (myFonts.Families[i].Name == fontName)
					return new Font(myFonts.Families[i], prototype.Size, prototype.Style, prototype.Unit, prototype.GdiCharSet, prototype.GdiVerticalFont);
			return prototype;
		}
	}
}
