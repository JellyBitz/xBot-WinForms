using System;
using System.Collections.Specialized;
using System.Text;
using xBot.Game.Objects.Common;

namespace xBot.Game.Objects.Item
{
	public class SRItem
	{
		#region (Properties)
		public uint ID { get; protected set; }
		public string ServerName { get; protected set; }
		public string Name { get; protected set; }
		public byte ID1 { get; protected set; }
		public byte ID2 { get; protected set; }
		public byte ID3 { get; protected set; }
		public byte ID4 { get; protected set; }
		public string Icon { get; set; }
		public ushort QuantityMax { get; set; }
		public ushort Quantity { get; set; }
		public byte LevelRequired { get; set; }
		public SRRentable Rentable { get; internal set; }
		#endregion

		#region (Constructor)
		private SRItem(uint ID)
		{
			NameValueCollection data = DataManager.GetItemData(ID);

			if(data == null)
			{
				int aaa = 0;
				aaa++;
			}

			this.ID = ID;
			ServerName = data["servername"];
			Name = data["name"];
			ID1 = 3;
			ID2 = byte.Parse(data["tid2"]);
			ID3 = byte.Parse(data["tid3"]);
			ID4 = byte.Parse(data["tid4"]);

			Icon = data["icon"];
			Quantity = 1;
			QuantityMax = ushort.Parse(data["stack"]);
			LevelRequired = byte.Parse(data["level"]);
		}
		private SRItem(string ServerName)
		{
		  NameValueCollection	data = DataManager.GetItemData(ServerName);

			this.ID = uint.Parse(data["id"]);
			this.ServerName = ServerName;;
			Name = data["name"];
			ID1 = 3;
			ID2 = byte.Parse(data["tid2"]);
			ID3 = byte.Parse(data["tid3"]);
			ID4 = byte.Parse(data["tid4"]);

			Icon = data["icon"];
      QuantityMax = ushort.Parse(data["stack"]);
			LevelRequired = byte.Parse(data["level"]);
		}
		protected SRItem(SRItem value)
		{
			ID = value.ID;
			ServerName = value.ServerName;
			Name = value.Name;
			ID1 = value.ID1;
			ID2 = value.ID2;
			ID3 = value.ID3;
			ID4 = value.ID4;

			Icon = value.Icon;
			Quantity = value.Quantity;
			QuantityMax = value.QuantityMax;
			LevelRequired = value.LevelRequired;
			Rentable = value.Rentable;
		}
		#endregion

		#region (Methods)
		public static SRItem Create(uint ID,SRRentable Rentable)
		{
			SRItem item = new SRItem(ID);
			item.Rentable = Rentable;
			if (item.isEquipable())
			{
				SREquipable equipable = new SREquipable(item);
				item = equipable;
			}
			else if (item.isCoS())
			{
				SRCoS cos = new SRCoS(item);
				item = cos;
			}
			else if (item.isEtc())
			{
				SREtc etc = new SREtc(item);
				item = etc;
			}
			return item;
		}
		public static SRItem Create(string ServerName, SRRentable Rentable = null)
		{
			SRItem item = new SRItem(ServerName);
			if (Rentable == null)
				Rentable = new SRRentable(0);
			item.Rentable = Rentable;
			if (item.isEquipable())
			{
				SREquipable equipable = new SREquipable(item);
				item = equipable;
			}
			else if (item.isCoS())
			{
				SRCoS cos = new SRCoS(item);
				item = cos;
			}
			else if (item.isEtc())
			{
				SREtc etc = new SREtc(item);
				item = etc;
			}
			return item;
		}
		public bool isEquipable()
		{
			return ID2 == 1;
		}
		public bool isCoS()
		{
			return ID2 == 2;
		}
		public bool isEtc()
		{
			return ID2 == 3;
		}
		public ushort GetUsageType()
		{
			return (ushort)((ushort)Rentable.ID | ID1 << 2 | ID2 << 5 | ID3 << 7 | ID4 << 11);
		}
		public bool isType(byte ID2, byte ID3, byte ID4)
		{
			return this.ID2 == ID2 && this.ID3 == ID3 && this.ID4 == ID4;
		}

		public string GetQuantityText()
		{
			return (QuantityMax > 1) ? " (" + Quantity + "/" + QuantityMax + ")" : "";
		}
		public virtual string GetFullName()
		{
			return Name;
		}
		public virtual string GetTooltip()
		{
			return GetFullName();
		}
		public virtual SRItem Clone()
		{
			return new SRItem(this);
		}
		#endregion
	}
}
