using System.Collections.Generic;
using System.Collections.Specialized;

namespace xBot.Game.Objects.Entity
{
	public class SRTeleport: SREntity
	{
		public byte unkByte01 { get; set; }
		public byte unkByte02 { get; set; }
		public byte unkByte03 { get; set; }
		public Portal PortalType { get; set; }
		public uint unkUInt01 { get; internal set; }
		public uint unkUInt02 { get; internal set; }
		public uint OwnerUniqueID { get; internal set; }
		public string OwnerName { get; internal set; }
		public uint unkUInt03 { get; internal set; }
		public byte unkByte04 { get; internal set; }
		public string TeleportName { get; }
		public List<SRTeleportOption> TeleportOptions { get; }

		public SRTeleport(uint ID)
		{
			if ((m_data = DataManager.GetTeleport(ID)) == null)
				m_data = DataManager.GetTeleportLinkByID(ID);

			this.ID = ID;
			ServerName = m_data["servername"];
			Name = m_data["name"];
			ID1 = 1;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);
			
			List<NameValueCollection> linkData = DataManager.GetTeleportLinks(this.ID);
			TeleportOptions = new List<SRTeleportOption>();
			if (linkData.Count > 0)
			{
				TeleportName = linkData[0]["name"];
				foreach (NameValueCollection link in linkData)
				{
					SRTeleportOption Teleport = new SRTeleportOption();
					Teleport.ID = uint.Parse(link["destinationid"]);
					Teleport.Name = link["destination"];
					Teleport.ServerName = link["servername"];

					TeleportOptions.Add(Teleport);
				}
			}
			else
			{
				TeleportName = this.Name;
      }
		}
		public SRTeleport(string ServerName)
		{
			if ((m_data = DataManager.GetTeleport(ServerName)) != null) {
				m_data = DataManager.GetTeleportLinkByServerName(ServerName);
      }

			ID = uint.Parse(m_data["id"]);
			this.ServerName = ServerName;
			Name = m_data["name"];
			ID1 = 4;
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);

			List<NameValueCollection> linkData = DataManager.GetTeleportLinks(this.ID);
			TeleportOptions = new List<SRTeleportOption>();
			if (linkData.Count > 0)
			{
				TeleportName = linkData[0]["name"];
				foreach (NameValueCollection link in linkData)
				{
					SRTeleportOption Teleport = new SRTeleportOption();
					Teleport.ID = uint.Parse(link["destinationid"]);
					Teleport.Name = link["destination"];
					Teleport.ServerName = link["servername"];

					TeleportOptions.Add(Teleport);
				}
			}
			else
			{
				TeleportName = this.Name;
			}
		}

		public SRTeleport(SREntity value) : base(value)
		{
			List<NameValueCollection> linkData = DataManager.GetTeleportLinks(this.ID);
			TeleportOptions = new List<SRTeleportOption>();
			if (linkData.Count > 0)
			{
				TeleportName = linkData[0]["name"];
				foreach (NameValueCollection link in linkData)
				{
					SRTeleportOption Teleport = new SRTeleportOption();
					Teleport.ID = uint.Parse(link["destinationid"]);
					Teleport.Name = link["destination"];
					Teleport.ServerName = link["servername"];

					TeleportOptions.Add(Teleport);
				}
			}
			else
			{
				TeleportName = this.Name;
			}
		}

		public enum Portal:byte
		{
			None = 0,
			Regular = 1,
			Dimensional = 6 
		}
	}
}
