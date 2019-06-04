using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace xBot.Game
{
	// All about reading game data
	public class Data
	{
		public string gatewayHost;
		public ushort gatewayPort;
		public byte Locale { get; set; }
		public uint Version { get; set; }
		private Data()
		{
		}
		private static Data _this = null;
		public static Data Get
		{
			get
			{
				if (_this == null)
					_this = new Data();
				return _this;
			}
		}
		/// <summary>
		/// Get data line found with the ID specified.
		/// </summary>
		/// <param name="ID">ID to search in the file</param>
		/// <param name="Column">Column to compare the given ID</param>
		/// <param name="Path">Full path of the file</param>
		/// <returns>Null if data from ID is not found</returns>
		public string[] getDataFromFile(string identifier, int column, string path, string filename)
		{
			using (StreamReader reader = new StreamReader(path + "\\" + filename))
			{
				string line;
				while ((line = WindowsAPI.ReadToString(reader, "\r\n")) != null)
				{
					// Data is enabled on the game
					if (line.StartsWith("1\t"))
					{
						string[] data = line.Split(new string[] { "	" }, StringSplitOptions.RemoveEmptyEntries);
						if (data[column] == identifier)
							return data;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// Get data line found with the ID specified.
		/// </summary>
		/// <param name="ID">ID to search in the file</param>
		/// <param name="Column">Column to compare the given ID</param>
		/// <param name="Path">Full path of the file that contain multiples files</param>
		/// <returns>Null if data from ID is not found</returns>
		public string[] getDataFromFiles(string identifier, int column, string path, string filename)
		{
			string[] lines = File.ReadAllLines(path + "\\" + filename);
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Trim() != "")
				{
					string[] data = getDataFromFile(identifier, column, path, lines[i]);
					if (data != null)
					{
						return data;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// Get the experience maximum from the level specified.
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public ulong getExpMax(byte level)
		{
			return ulong.Parse(getDataFromFile(level.ToString(), 0, "Data\\Media\\server_dep\\silkroad\\textdata", "leveldata.txt")[1]);
		}
	}
}