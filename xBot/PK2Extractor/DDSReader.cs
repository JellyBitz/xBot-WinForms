using System;
using System.Drawing;
using System.IO;

namespace xBot.PK2Extractor
{
	public static class DDSReader
	{
		public static Bitmap FromFile(string FileName)
		{
			return DevIL.DevIL.LoadBitmap(FileName);
    }
		public static Bitmap FromDDJ(string FileName)
		{
			byte[] DDJStream = File.ReadAllBytes(FileName);
			string tempFile = Path.GetTempFileName();
			byte[] DDSStream = ToDDS(DDJStream);
			File.WriteAllBytes(tempFile, DDSStream);
			Bitmap bmp = DevIL.DevIL.LoadBitmap(tempFile);
			File.Delete(tempFile);
			return bmp;
		}
		public static Bitmap FromDDJ(byte[] DDJBuffer)
		{
			string tempFile = Path.GetTempFileName();
			byte[] DDSBuffer = ToDDS(DDJBuffer);
			File.WriteAllBytes(tempFile, DDSBuffer);
			Bitmap bmp = DevIL.DevIL.LoadBitmap(tempFile);
			File.Delete(tempFile);
			return bmp;
		}
		private static byte[] ToDDS(byte[] DDJBuffer)
		{
			byte[] DDSBuffer = new byte[DDJBuffer.Length - 20];
			Array.Copy(DDJBuffer, 20, DDSBuffer, 0, DDSBuffer.Length);
			return DDSBuffer;
		}
	}
}
