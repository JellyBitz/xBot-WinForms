namespace xBot.Game.Objects.Entity
{
	public class SRMask:SREntity
	{
		public byte Scale { get; set; }
		//public xList<SRItem> Inventory { get; set; }
		public uint[] Inventory { get; set; }
		public SRMask(uint ID)
		{
			m_data = DataManager.GetModelData(ID);

			this.ID = ID;
			ServerName = m_data["servername"];
			Name = m_data["name"];
			ID1 = 1;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);
		}
	}
}
