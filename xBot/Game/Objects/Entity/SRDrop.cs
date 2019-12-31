using System;
using System.Collections.Specialized;

namespace xBot.Game.Objects.Entity
{
	public class SRDrop : SREntity
	{
		public bool hasOwner { get { return OwnerJoinID != 0; } }
		public uint OwnerJoinID { get; set; }
		public string OwnerName { get; set; }
		public byte Rarity { get; set; }
		public byte DropSourceType { get; set; }
		public uint DropUniqueID { get; set; }
		public byte Plus { get; set; }
		public uint Gold { get; set; }

		#region (Constructor)
		public SRDrop(uint ID)
		{
			m_data = DataManager.GetItemData(ID);

			this.ID = ID;
			ServerName = m_data["servername"];
			Name = m_data["name"];
			ID1 = 3;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);
		}
		public SRDrop(string ServerName)
		{
			m_data = DataManager.GetItemData(ServerName);

			ID = uint.Parse(m_data["id"]);
			this.ServerName = ServerName;;
			Name = m_data["name"];
			ID1 = 3;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);
		}
		public SRDrop(SREntity value) : base(value)
		{

		}
		#endregion

		#region (Methods)
		public bool isEquipable()
		{
			return ID2 == 1;
		}
		public bool isEtc()
		{
			return ID2 == 3;
		}
		// Class Level 2
		public bool isGold()
		{
			return ID3 == 5 && ID4 == 0;
    }
		public bool isTradeGoods()
		{
			return ID3 == 8;
		}
		public bool isQuest()
		{
			return ID3 == 9;
    }
		#endregion
	}
}
