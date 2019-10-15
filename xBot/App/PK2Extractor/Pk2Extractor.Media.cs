using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Threading;
using xBot.App.PK2Extractor.PK2ReaderAPI;

namespace xBot.App.PK2Extractor
{
	partial class Pk2Extractor
	{
		private void AddItemIcons()
		{
			string sql = "SELECT icon FROM items GROUP BY icon";
			db.ExecuteQuery(sql);

			string path = GetDirectory(SilkroadName);
			List<NameValueCollection> rows = db.GetResult();

			// Add default icon
			NameValueCollection icon_default = new NameValueCollection();
			icon_default.Add("icon", "icon_default.ddj");
			rows.Add(icon_default);

			// Check and save every icon available
			foreach (NameValueCollection column in rows)
			{
				string iconPath = "icon\\" + column["icon"];
				// Check if the icon exists into the pk2
				Pk2File DDJFile = pk2.GetFile(iconPath);
				if (DDJFile == null)
					continue;
				// Check path if the file already exists
				string saveFilePath = Path.ChangeExtension(Path.GetFullPath(path + iconPath), "png");
				if (File.Exists(saveFilePath))
					continue;
				// Check directory
				string saveFolderPath = Path.GetDirectoryName(saveFilePath);
				if (!Directory.Exists(saveFolderPath))
					Directory.CreateDirectory(saveFolderPath);

				// 20% display
				if (rand.Next(1, 1000) <= 200)
					LogState("Creating " + iconPath);

				// Convert DDJ to DDS to Bitmap
				Bitmap img = DDSReader.FromDDJ(pk2.GetFileBytes(DDJFile));
				// Save as png
				img.Save(saveFilePath, System.Drawing.Imaging.ImageFormat.Png);

				Thread.Sleep(CPU_BREAK);
			}
		}
		private void AddSkillIcons()
		{
			string sql = "SELECT icon FROM skills GROUP BY icon";
			db.ExecuteQuery(sql);

			string path = GetDirectory(SilkroadName);
			List<NameValueCollection> rows = db.GetResult();
			if (rows != null)
			{
				foreach (NameValueCollection column in rows)
				{
					string iconPath = "icon\\" + column["icon"];
					// Check if the icon exists into the pk2
					Pk2File DDJFile = pk2.GetFile(iconPath);
					if (DDJFile == null)
						continue;
					// Check path if the file already exists
					string saveFilePath = Path.ChangeExtension(Path.GetFullPath(path + iconPath),"png");
					if (File.Exists(saveFilePath))
						continue;
					// Check directory
					string saveFolderPath = Path.GetDirectoryName(saveFilePath);
					if (!Directory.Exists(saveFolderPath))
						Directory.CreateDirectory(saveFolderPath);

					// 20% display
					if (rand.Next(1, 1000) <= 200)
						LogState("Creating " + iconPath);

					// Convert DDJ to DDS to Bitmap
					Bitmap img = DDSReader.FromDDJ(pk2.GetFileBytes(DDJFile));
					// Save as png
					img.Save(saveFilePath, System.Drawing.Imaging.ImageFormat.Png);

					Thread.Sleep(CPU_BREAK);
				}
			}
		}
	}
}