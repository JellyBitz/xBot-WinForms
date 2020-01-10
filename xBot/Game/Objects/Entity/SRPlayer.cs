using System.Text;
using System.Windows.Forms;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Item;

namespace xBot.Game.Objects.Entity
{
	public class SRPlayer:SRModel
	{
		private bool m_LoadSuccess;
		private CaptureTheFlag m_CaptureTheFlagType;

		#region (Properties)
		public byte Scale { get; set; }
		public byte BerserkLevel { get; set; }
		public PVPCape PVPCapeType { get; set; }
		public ExpIcon ExpIconType { get; set; }
		public xList<SRItem> Inventory { get; set; }
		public xList<SRItem> InventoryAvatar { get; set; }
		public SRMask Mask { get; set; }
		public Job JobType { get; set; }
		public byte JobLevel { get; set; }
		public PVPState PVPStateType { get; set; }
		public bool isRiding { get{ return RidingUniqueID != 0; } }
		public uint RidingUniqueID { get; set; }
		public bool inCombat { get; set; }
		public Scrolling ScrollingType { get; set; }
		public Interaction InteractionType { get; set; }
		public string GuildName { get; set; }
		public SRPlayerGuildInfo GuildInfo { get; set; }
		public SRStall Stall { get; set; }
		public byte EquipmentCooldown { get; set; }
		public CaptureTheFlag CaptureTheFlagType {
			get { return m_CaptureTheFlagType; }
			set {
				m_CaptureTheFlagType = value;
				m_LoadSuccess = true;
			}
		}
		public byte unkByte03 { get; set; }
		public xList<SRItemExchange> InventoryExchange { get; set; }
		#endregion

		public SRPlayer(uint ID) : base(ID) { }
		public SRPlayer(string ServerName) : base(ServerName) { }
		public SRPlayer(SRModel value) : base(value) { }

		#region (Methods)
		public virtual bool hasJobMode()
		{
			if(m_LoadSuccess)
				return GuildInfo == null;
			return Inventory.Exists(item => item != null && item.isEquipable() && ((SREquipable)item).isJob());
		}
		public string GetFullName()
		{
			StringBuilder fullName = new StringBuilder(Name);
			if(GuildInfo == null)
			{
				fullName.Append(" (" + JobType + ")");
      }
			else
			{
				if(GuildInfo.GuildID != 0)
				{
					fullName.Append(" [" + GuildName);
					if(GuildInfo.GuildMemberName != "")
						fullName.Append(" * " + GuildName);
					fullName.Append("]");
				}
			}
			return fullName.ToString();
		}
		public override TreeNode ToTreeNode()
		{
			TreeNode root = base.ToTreeNode();
			root.Nodes.Add("Scale: " + Scale);
			root.Nodes.Add("BerserkLevel: " + BerserkLevel);
			root.Nodes.Add("PVPCapeType: " + PVPCapeType);
			root.Nodes.Add("ExpIconType: " + ExpIconType);
			// Inventory
			TreeNode inventory = new TreeNode("Inventory");
      for (int i = 0; i < Inventory.Capacity; i++)
			{
				TreeNode item = new TreeNode();
				if (Inventory[i] == null)
				{
					item.Text = "Empty";
				}
				else
				{
					item.Text = Inventory[i].Name;
					item.Nodes.Add("ServerName: " + Inventory[i].ServerName);
					item.Nodes.Add("ID: " + Inventory[i].ID);
					item.Nodes.Add("ID's [" + Inventory[i].ID1 + "][" + Inventory[i].ID2 + "][" + Inventory[i].ID3 + "][" + Inventory[i].ID4 + "]");
					item.Nodes.Add("Quantity: " + Inventory[i].Quantity + " / " + Inventory[i].QuantityMax);
				}
				inventory.Nodes.Add(item);
			}
			root.Nodes.Add(inventory);
			// Inventory
			inventory = new TreeNode("Inventory Avatar");
			for (int i = 0; i < InventoryAvatar.Capacity; i++)
			{
				TreeNode item = new TreeNode();
				if (InventoryAvatar[i] == null)
				{
					item.Text = "Empty";
				}
				else
				{
					item.Text = InventoryAvatar[i].Name;
					item.Nodes.Add("ServerName: " + InventoryAvatar[i].ServerName);
					item.Nodes.Add("ID: " + InventoryAvatar[i].ID);
					item.Nodes.Add("ID's [" + InventoryAvatar[i].ID1 + "][" + InventoryAvatar[i].ID2 + "][" + InventoryAvatar[i].ID3 + "][" + InventoryAvatar[i].ID4 + "]");
					item.Nodes.Add("Quantity: " + InventoryAvatar[i].Quantity + " / " + InventoryAvatar[i].QuantityMax);
				}
				inventory.Nodes.Add(item);
			}
			root.Nodes.Add(inventory);
			root.Nodes.Add("JobType: " + JobType);
			root.Nodes.Add("JobLevel: " + JobLevel);
			root.Nodes.Add("PVPStateType: " + PVPStateType);
			if(isRiding)
				root.Nodes.Add("RidingUniqueID: " + RidingUniqueID);
			root.Nodes.Add("inCombat: " + inCombat);
			root.Nodes.Add("ScrollingType: " + ScrollingType);
			root.Nodes.Add("InteractionType: " + InteractionType);
			root.Nodes.Add("GuildName: " + GuildName);
			if (GuildInfo != null)
			{
				root.Nodes.Add("GuildID: " + GuildInfo.GuildID);
				root.Nodes.Add("GuildMemberName: " + GuildInfo.GuildMemberName);
				root.Nodes.Add("GuildLastCrestRev: " + GuildInfo.GuildLastCrestRev);
				root.Nodes.Add("UnionID: " + GuildInfo.UnionID);
				root.Nodes.Add("UnionLastCrestRev: " + GuildInfo.UnionLastCrestRev);
				root.Nodes.Add("isFriendly: " + GuildInfo.isFriendly);
				root.Nodes.Add("GuildMemberAuthorityType: " + GuildInfo.GuildMemberAuthorityType);
			}
			if(Stall != null)
			{
				root.Nodes.Add("Stall.Title: " + Stall.Title);
				root.Nodes.Add("Stall.DecorationID: " + Stall.DecorationID);
				if (Stall.Inventory != null)
				{
					root.Nodes.Add("Stall.Note: " + Stall.Note);
					inventory = new TreeNode("Stall.Inventory");
					for (int i = 0; i < Stall.Inventory.Capacity; i++)
					{
						TreeNode item = new TreeNode();
						if (Stall.Inventory[i] == null)
						{
							item.Text = "Empty";
						}
						else
						{
							item.Text = Stall.Inventory[i].Item.Name;
							item.Nodes.Add("ServerName: " + Stall.Inventory[i].Item.ServerName);
							item.Nodes.Add("ID: " + Stall.Inventory[i].Item.ID);
							item.Nodes.Add("ID's [" + Stall.Inventory[i].Item.ID1 + "][" + Stall.Inventory[i].Item.ID2 + "][" + Stall.Inventory[i].Item.ID3 + "][" + Stall.Inventory[i].Item.ID4 + "]");
							item.Nodes.Add("Quantity: " + Stall.Inventory[i].Item.Quantity + " / " + Stall.Inventory[i].Item.QuantityMax);
						}
						inventory.Nodes.Add(item);
					}
					root.Nodes.Add(inventory);
				}
			}
			root.Nodes.Add("EquipmentCooldown: " + EquipmentCooldown);
			root.Nodes.Add("CaptureTheFlagType: " + CaptureTheFlagType);
			root.Nodes.Add("unkByte03: " + unkByte03);
			return root;
		}
		#endregion

		public enum PVPCape : byte
		{
			None = 0,
			Red = 1,
			Gray = 2,
			Blue = 3,
			White = 4,
			Yellow = 5
		}
		public enum ExpIcon : byte
		{
			Beginner = 0,
			Helpful = 1,
			BeginnerAndHelpful = 2
		}
		public enum Job : byte
		{
			None = 0,
			Trader = 1,
			Thief = 2,
			Hunter = 3
		}
		public enum PVPState : byte
		{
			Neutral = 0,
			Assaulter = 1,
			PlayerKiller = 2
		}
		public enum Scrolling
		{
			None = 0,
			ReturnScroll = 1,
			BanditReturnScroll = 2
		}
		public enum Interaction : byte
		{
			None = 0,
			OnExchangeProbably = 2,
			OnStall = 4,
			OnShop = 6
		}
		public enum CaptureTheFlag : byte
		{
			None = 0xFF,
			Red = 1,
			Blue = 2
		}
		public class SRPlayerGuildInfo
		{
			public uint GuildID { get; set; }
			public string GuildMemberName { get; set; }
			public uint GuildLastCrestRev { get; set; }
			public uint UnionID { get; set; }
			public uint UnionLastCrestRev { get; set; }
			public bool isFriendly { get; set; }
			public GuildMemberAuthority GuildMemberAuthorityType { get; set; }
			public enum GuildMemberAuthority
			{
				None = 0xFF
			}
		}
	}
}
