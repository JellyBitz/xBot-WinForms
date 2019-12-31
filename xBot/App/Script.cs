using System;
using System.Collections.Generic;
using System.IO;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;

namespace xBot.App
{
	public class Script
	{
		private List<string> m_lines;
		public string FileName { get; private set; }
		public bool Running { get; set; }

		public Script()
		{
			this.FileName = "";
			this.m_lines = new List<string>();
		}
		public Script(string path)
		{
			this.FileName = Path.GetFileName(path);
			string[] lines = File.ReadAllLines(path);
			this.m_lines = new List<string>(lines.Length);
			foreach (string line in lines)
				this.Add(line);
		}
		/// <summary>
		/// Gets the near script to be used as town loop. Returns null if cannot be found.
		/// </summary>
		public static Script GetNearestTownScript(SRCoord position, int maxRange)
		{
			if (Directory.Exists("Town"))
			{
				string[] files = Directory.GetFiles("Town", "*.txt");
				foreach (string file in files)
				{
					Script town = new Script(file);
					if (town.GetNearMovement(position, maxRange) != -1)
						return town;
				}
			}
			return null;
		}
		public void Add(string line)
		{
			// Keep intact if starts as comment
			if (line.StartsWith("//"))
				m_lines.Add(line);
			else
				m_lines.Add(line.ToUpper());
		}
		public void RemoveAt(int index)
		{
			m_lines.RemoveAt(index);
		}
		public void Save(string path)
		{
			File.WriteAllLines(path,this.m_lines.ToArray());
		}
		/// <summary>
		/// Check if is near to the script. Returns the near index or (-1).
		/// </summary>
		public int GetNearMovement(SRCoord position, int maxRange)
		{
			for (int i =0; i < m_lines.Count; i++)
			{
        if (m_lines[i].StartsWith("move"))
				{
					string[] args = m_lines[i].Split(',');
          if (args.Length >= 3)
					{
						// Formats accepted :  "move,PosX,PosY" or "move,Region,X,Y,Z" 
						ushort r;
						int x,y,z;
            if (args.Length == 5 
							&& ushort.TryParse(args[1],out r)
							&& int.TryParse(args[2],out x)
							&& int.TryParse(args[3], out y)
							&& int.TryParse(args[4], out z))
						{
							// take it as Region,X,Z,Y
							if ((new SRCoord(r, x, z, y)).DistanceTo(position) <= maxRange)
								return i;
						}
						else if(args.Length == 3
							&& int.TryParse(args[2], out x)
							&& int.TryParse(args[3], out y))
						{
							// take it as ingame coordinates
							if ((new SRCoord(x, y)).DistanceTo(position) <= maxRange)
								return i;
						}
          }
				}
			}
			return -1;
		}
		/// <summary>
		/// Start running script.
		/// </summary>
		public void Run(int startIndex = 0)
		{
			Window w = Window.Get;
			Bot b = Bot.Get;
			// Parsing script
			for (int j = startIndex; j < m_lines.Count && Running; j++)
			{
				if (m_lines[j].StartsWith("//"))
					continue;
				string[] command = m_lines[j].Split(',');
				// Execute the command
				switch (command[0])
				{
					case "move":
						SRCoord position = Move(command);
						if(position != null) {
							if(!b.WaitMovement(position, 10))
							{
								w.LogProcess("Bot cannot reach the next movement...",Window.ProcessState.Error);
								w.Log("Probably stuck at script");
								b.Stop();
              }
						}
						break;
				}
			}
		}

		#region Script command parsing
		private SRCoord Move(string[] args)
		{
			if (args.Length >= 3)
			{
				// Formats accepted :  "move,PosX,PosY" or "move,Region,X,Y,Z" 
				ushort r;
				int x, y, z;
				if (args.Length == 5
					&& ushort.TryParse(args[1], out r)
					&& int.TryParse(args[4], out z)
					&& int.TryParse(args[2], out x)
					&& int.TryParse(args[3], out y))
				{
					// take it as Region,X,Z,Y
					return new SRCoord(r, x, z, y);
				}
				else if (args.Length == 3
					&& int.TryParse(args[1], out x)
					&& int.TryParse(args[2], out y))
				{
					// take it as ingame coordinates
					return new SRCoord(x, y);
				}
			}
			return null;
		}
		#endregion
	}
}
