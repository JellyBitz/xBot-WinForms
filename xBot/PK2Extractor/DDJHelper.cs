using Pfim;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace xBot.PK2Extractor
{
    public static class DDJHelper
    {
        /// <summary>
        /// Get bitmap from a DDJ stream.
        /// </summary>
        public static Bitmap GetBitmap(Stream streamddj)
        {
            using (streamddj)
            {
                // Make sure this stream is a DDJ or DDS
                if (!FixStreamAsDDS(streamddj))
                    return null;

                // Try to load DDS format
                try
                {
                    return ConvertStreamDDJToBitmap(streamddj);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            return null;
        }
        #region Private Helpers
        /// <summary>
        /// Convert stream with DDS file format into bitmap.
        /// </summary>
        private static Bitmap ConvertStreamDDJToBitmap(Stream streamddj)
        {
            using (IImage image = Pfimage.FromStream(streamddj))
            {
                // Find pixel format being use, covering just DXT1~DXT5
                var pixelFormat = PixelFormat.Undefined;
                switch (image.Format)
                {
                    case Pfim.ImageFormat.Rgb24:
                        pixelFormat = PixelFormat.Format24bppRgb;
                        break;
                    case Pfim.ImageFormat.Rgba32:
                        pixelFormat = PixelFormat.Format32bppArgb;
                        break;
                    case Pfim.ImageFormat.R5g6b5:
                        pixelFormat = PixelFormat.Format16bppRgb565;
                        break;
                    case Pfim.ImageFormat.R5g5b5a1:
                        pixelFormat = PixelFormat.Format16bppArgb1555;
                        break;
                    default:
                        throw new NotSupportedException($"DDS format not supported [{image.Format}]");
                }
                // Create bitmap
                Bitmap bitmap = new Bitmap(image.Width, image.Height, pixelFormat);
                BitmapData rectData = bitmap.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.WriteOnly,
                    pixelFormat);
                // Copy Pfim's buffer straight into System.Drawing's allocated memory
                Marshal.Copy(image.Data, 0, rectData.Scan0, image.DataLen);
                // Unlock and return it
                bitmap.UnlockBits(rectData);
                return bitmap;
            }
        }
        /// <summary>
        /// Make sure this stream is a DDJ otherwise fix it to be DDS.
        /// </summary>
        private static bool FixStreamAsDDS(Stream stream)
        {
            if (stream == null) return false;
            var remainingBytes = stream.Length - stream.Position;
            if (remainingBytes < 12)
                return false;

            var br = new BinaryReader(stream);

            // Check if this is just a DDS named .ddj (supported by SRO)
            var signature = new string(br.ReadChars(4));
            br.BaseStream.Seek(-4, SeekOrigin.Current);
            if (signature == "DDS ")
                return true;

            // Check if this is a DDJ
            signature = new string(br.ReadChars(12));
            br.BaseStream.Seek(-12, SeekOrigin.Current);
            if (signature == "JMXVDDJ 1000")
            {
                br.BaseStream.Seek(12 + 8, SeekOrigin.Current);
                return true;
            }

            return false;
        }
        #endregion
    }
}
