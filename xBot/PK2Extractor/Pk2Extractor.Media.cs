using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using xBot.PK2Extractor.PK2ReaderAPI;

namespace xBot.PK2Extractor
{
	partial class Pk2Extractor
	{
		private void AddItemIcons()
		{
			string sql = "SELECT icon FROM items GROUP BY icon";
			db.ExecuteQuery(sql);

			string path = GetDirectory(SilkroadName);
			List<NameValueCollection> rows = db.GetResult();

			// Adding default icons
			NameValueCollection defaultIcons = new NameValueCollection();
			defaultIcons.Add("icon", "icon_default.ddj"); // Not Image
			rows.Add(defaultIcons);
			defaultIcons = new NameValueCollection();
			defaultIcons.Add("icon", "action\\icon_cha_auto_attack.ddj"); // Common attack
			rows.Add(defaultIcons);

			LogState("Checking item icon files...");
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
			}
		}
		private void AddSkillIcons()
		{
			string sql = "SELECT icon FROM skills GROUP BY icon";
			db.ExecuteQuery(sql);

			string path = GetDirectory(SilkroadName);
			List<NameValueCollection> rows = db.GetResult();
			LogState("Checking skill icon files...");
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
			}
		}
		private void AddMinimap()
		{
			string folderPath;

			LogState("Checking minimap images...");
			// Check directory
			folderPath = "Minimap\\";
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);
			// Get files
			Pk2Folder minimap = pk2.GetFolder("Minimap");
			if (minimap != null)
				ExtractAllImages(minimap, folderPath, System.Drawing.Imaging.ImageFormat.Jpeg);

			LogState("Checking minimap dungeon images...");
			// Check directory
			folderPath = "Minimap\\d\\";
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);
			// Get files
			minimap = pk2.GetFolder("Minimap_d");
			if (minimap != null)
				ExtractAllImages(minimap, folderPath, System.Drawing.Imaging.ImageFormat.Jpeg);
		}
		private void ExtractAllImages(Pk2Folder folder, string OutPutPath, System.Drawing.Imaging.ImageFormat format)
		{
			string ext = format == System.Drawing.Imaging.ImageFormat.Jpeg?"jpg":format.ToString().ToLower();
      foreach (Pk2File f in folder.Files)
			{
				// Check path if the file already exists
				string saveFilePath = Path.ChangeExtension(Path.GetFullPath(OutPutPath + f.Name), ext);
				if (File.Exists(saveFilePath))
					continue;

				// 10% display
				if (rand.Next(1, 1000) <= 100)
					LogState("Creating " + f.Name);

				// Convert DDJ to DDS to Bitmap
				Bitmap img = DDSReader.FromDDJ(pk2.GetFileBytes(f));
				// Save as png
				img.Save(saveFilePath,format);
			}
			foreach (Pk2Folder f in folder.SubFolders)
			{
				ExtractAllImages(f, OutPutPath, format);
			}
		}
	}
}