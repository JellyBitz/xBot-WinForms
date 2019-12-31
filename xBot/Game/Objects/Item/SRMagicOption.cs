using System;
using System.Collections.Specialized;
namespace xBot.Game.Objects.Item
{
	public class SRMagicOption
	{
		public uint ID { get; set; }
		public string ServerName { get; set; }
		public string Name { get; set; }
		public uint Value { get; set; }
		public uint ValueMax { get; }
		public SRMagicOption(uint ID)
		{
			NameValueCollection data = DataManager.GetMagicOption(ID);

			this.ID = ID;
      ServerName = data["servername"];
			Name = data["name"];
			ValueMax = uint.Parse(data["maxvalue"]);
		}
		public SRMagicOption(string ServerName)
		{
			NameValueCollection data = DataManager.GetMagicOption(ServerName);
			
			ID = uint.Parse(data["id"]);
			this.ServerName = ServerName;
			Name = data["name"];
			ValueMax = uint.Parse(data["maxvalue"]);
		}
		public string GetFullName()
		{
			if (Name != "")
			{
				switch (ServerName)
				{
					case "MATTR_INT":
					case "MATTR_STR":
          case "MATTR_HP":
					case "MATTR_MP":
          case "MATTR_EVADE_BLOCK":
					case "MATTR_EVADE_CRITICAL":
						return Name + " " + Value + (ValueMax > 0 ? " (" + Math.Round(Value * 100f / ValueMax) + "%)" : "");
					case "MATTR_DUR":
					case "MATTR_ER":
					case "MATTR_HR":
					case "MATTR_RESIST_FROSTBITE":
					case "MATTR_RESIST_ESHOCK":
					case "MATTR_RESIST_BURN":
					case "MATTR_RESIST_POISON":
					case "MATTR_RESIST_ZOMBIE":
						return Name + " +" + Value + "%";
					case "MATTR_SOLID":
					case "MATTR_LUCK":
					case "MATTR_ASTRAL":
					case "MATTR_ATHANASIA":
						return Name + " " + Value + "/" + ValueMax + " times";
				}
			}
			return "";
		}
	}
}
