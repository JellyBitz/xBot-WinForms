using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using xBot.Game.Objects.Common;
namespace xBot.Game.Objects.Entity
{
	public class SREntity
	{
		#region (Properties)
		public uint ID { get; set; }
		public string ServerName { get; set; }
		public string Name { get; set; }
		public byte ID1 { get; protected set; }
		public byte ID2 { get; protected set; }
		public byte ID3 { get; protected set; }
		public byte ID4 { get; protected set; }
		#endregion

		#region (Constructor)
		protected SREntity() { }
		/// <summary>
		/// Keeps the data retrieved from database.
		/// </summary>
		protected NameValueCollection m_data;
		private SREntity(uint ID)
		{
			this.ID = ID;

			if (ID == uint.MaxValue)
				return;
			// Search through database
			if ((m_data = DataManager.GetModelData(ID)) != null)
				ID1 = 1;
			else if ((m_data = DataManager.GetItemData(ID)) != null)
				ID1 = 3;
			else if ((m_data = DataManager.GetTeleport(ID)) != null || (m_data = DataManager.GetTeleportLinkByID(ID)) != null)
				ID1 = 4;

			ServerName = m_data["servername"];
			Name = m_data["name"];
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);
		}
		private SREntity(string ServerName)
		{
			this.ServerName = ServerName;

			// Search through database
			if ((m_data = DataManager.GetModelData(ServerName)) != null)
				ID1 = 1;
			else if ((m_data = DataManager.GetItemData(ServerName)) != null)
				ID1 = 3;
			else if ((m_data = DataManager.GetTeleport(ServerName)) != null || (m_data = DataManager.GetTeleportLinkByServerName(ServerName)) != null)
				ID1 = 4;

			ID = uint.Parse(m_data["id"]);
			Name = m_data["name"];
			ID2 = byte.Parse(m_data["tid2"]);
			ID3 = byte.Parse(m_data["tid3"]);
			ID4 = byte.Parse(m_data["tid4"]);
		}
		protected SREntity(SREntity value)
		{
			ID = value.ID;
			ServerName = value.ServerName;
			Name = value.Name;
			ID1 = value.ID1;
			ID2 = value.ID2;
			ID3 = value.ID3;
			ID4 = value.ID4;
			m_data = value.m_data;
		}
		#endregion

		#region (Methods)
		public static SREntity Create(uint ID)
		{
			SREntity obj = new SREntity(ID);
			if (obj.isSkillZone())
				return new SRSkillZone();
			if (obj.isModel())
			{
				SRModel model = new SRModel(obj);
				obj = model;

				if (model.isPlayer())
				{
					SRPlayer player = new SRPlayer(model);
					obj = player;
				}
				else if (model.isNPC())
				{
					SRNpc npc = new SRNpc(model);
					obj = npc;

					if (npc.isMob())
					{
						SRMob mob = new SRMob(npc);
						obj = mob;
					}
					else if (npc.isGuide())
					{
						SRGuide guide = new SRGuide(npc);
						obj = guide;
					}
					else if (npc.isCOS())
					{
						SRCoService cos = new SRCoService(npc);
						obj = cos;

						if (cos.isAttackPet())
						{
							SRAttackPet pet = new SRAttackPet(cos);
							obj = pet;
						}
						else if (cos.isPickPet())
						{
							SRPickPet pet = new SRPickPet(cos);
							obj = pet;
						}
					}
					else if (npc.isFortressCos())
					{
						SRFortressCos fCos = new SRFortressCos(npc);
						obj = fCos;
					}
					else if (npc.isFortressStruct())
					{
						SRFortressStruct fStruct = new SRFortressStruct(npc);
						obj = fStruct;
					}
				}
			}
			else if (obj.isDrop())
			{
				SRDrop drop = new SRDrop(obj);
				obj = drop;
			}
			else if (obj.isTeleport())
			{
				SRTeleport teleport = new SRTeleport(obj);
				obj = teleport;
			}
			return obj;
		}
		public bool isModel()
		{
			return ID1 == 1;
		}
		public bool isDrop()
		{
			return ID1 == 3;
		}
		public bool isTeleport()
		{
			return ID1 == 4;
		}
		public bool isSkillZone()
		{
			return ID == uint.MaxValue;
		}
		#endregion

		#region (World Properties)
		public uint UniqueID { get; set; }
		public SRCoord Position { get; set; }
		public ushort Angle { get; set; }
		public float GetRadianAngle()
		{
			return Angle * (float)Math.PI * 2 / ushort.MaxValue;
		}
		public float GetDegreeAngle()
		{
			return Angle * 360f / ushort.MaxValue;
		}

		public virtual SRCoord GetRealtimePosition()
		{
			return Position;
		}
		public virtual TreeNode ToTreeNode()
		{
			TreeNode root = new TreeNode();
			root.Text = "UID: " + UniqueID + " - Name: " + Name;
			root.Nodes.Add("ServerName: " + ServerName);
			root.Nodes.Add("ID: " + ID);
			root.Nodes.Add("ID's [" + ID1 + "][" + ID2 + "][" + ID3 + "][" + ID4 + "]");
			root.Nodes.Add("Angle: " + Math.Round(GetDegreeAngle(), 2) + "°");
			root.Nodes.Add("Position: " + Position);
			return root;
		}
		#endregion
	}
}
