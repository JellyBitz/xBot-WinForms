using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace xBot.App
{
	public class Fonts
	{
		/// <summary>
		/// Unique instance of this class.
		/// </summary>
		private static Fonts _this = null;
		private PrivateFontCollection myFonts = new PrivateFontCollection();
		private Fonts()
		{
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
		public static Fonts Get
		{
			get
			{
				if (_this == null)
					_this = new Fonts();
				return _this;
			}
		}
		public Font Load(Font prototype, string fontName="")
		{
			if (string.IsNullOrEmpty(fontName))
				fontName = prototype.FontFamily.Name;
			for (int i = 0; i < myFonts.Families.Length; i++)
				if (myFonts.Families[i].Name == fontName)
					return new Font(myFonts.Families[i], prototype.Size, prototype.Style, prototype.Unit, prototype.GdiCharSet, prototype.GdiVerticalFont);
			return prototype;
		}
		public static string UniToChar(int unicode)
		{
			string unicodeString = char.ConvertFromUtf32(unicode);
			return unicodeString;
		}
	}
}
