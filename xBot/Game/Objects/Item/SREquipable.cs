using System;
using System.Text;
using xBot.Game.Objects.Common;

namespace xBot.Game.Objects.Item
{
	public class SREquipable : SRItem
	{
		public byte Plus { get; set; }
		public ulong Variance { get; internal set; }
		public uint Durability { get; internal set; }
		public xList<SRMagicOption> MagicOptions { get; internal set; }
		internal xList<SRSocket> Sockets { get; set; }
		public xList<SRAdvancedElixir> AdvancedElixirs { get; internal set; }

		#region (Constructor)
		public SREquipable(SRItem value) : base(value) {
			Quantity = 1;
		}
    public SREquipable(SREquipable value) : base(value) {
			Plus = value.Plus;
			Variance = value.Variance;
			Durability = value.Durability;
			MagicOptions = value.MagicOptions;
			Sockets = value.Sockets;
			AdvancedElixirs = value.AdvancedElixirs;
		}
		#endregion

		#region (Methods)
		public bool isJob()
		{
			return ID3 == 7;
		}
		public bool isAvatar()
		{
			return ID3 == 13;
		}
		public Rarity GetRarity()
		{
			Rarity rare = Rarity.None;
			if (ServerName.Contains("_RARE"))
			{
				if (this.ServerName.Contains("_HONOR"))
					rare |= Rarity.HONOR;
        if (this.ServerName.Contains("_A_"))
					rare |= Rarity.SOS;
				if (this.ServerName.Contains("_B_"))
					rare |= Rarity.SOM;
				if (this.ServerName.Contains("_C_"))
					rare |= Rarity.SUN;
			}
			return rare;
		}
		public Genre GetGenre()
		{
			if (ServerName.Contains("_M_"))
				return Genre.Male;
			else if (ServerName.Contains("_W_"))
				return Genre.Female;
			return Genre.None;
		}
		public override string GetFullName()
		{
			StringBuilder result = new StringBuilder(Name);
			if (Plus > 0)
				result.Append(" (+" + Plus + ")");
			if (AdvancedElixirs != null && AdvancedElixirs.Count > 0)
				result.Append("[Adv. +" + AdvancedElixirs[0].Value + "]");
			Rarity rarity = GetRarity();
			if (rarity != Rarity.None)
				result.Append(" [" + rarity + "]");
			return result.ToString();
		}
		public override string GetTooltip()
		{
			StringBuilder result = new StringBuilder(Name);
			// Name
			if (Plus > 0)
				result.Append(" (+" + Plus + ")");
			Rarity rarity = GetRarity();
			if (rarity != Rarity.None)
				result.Append(" [" + rarity + "]");
			result.AppendLine();
			result.AppendLine();
			// Race & Type
			SRTypes.Equipable type = (SRTypes.Equipable)ID3;
			switch (type)
			{
				case SRTypes.Equipable.Garment:
				case SRTypes.Equipable.Protector:
				case SRTypes.Equipable.Armor:
					result.AppendLine("Race: Chinese (" + GetGenre() + ")");
					result.AppendLine("Type: " + type);
					break;
				case SRTypes.Equipable.Robe:
				case SRTypes.Equipable.LightArmor:
				case SRTypes.Equipable.HeavyArmor:
					result.AppendLine("Race: European (" + GetGenre() + ")");
					result.AppendLine("Type: " + type);
					break;
				case SRTypes.Equipable.Shield:
					if (ID4 == 1)
						result.AppendLine("Race: Chinese");
					else
						result.AppendLine("Race: European");
					break;
				case SRTypes.Equipable.AccesoriesCH:
				case SRTypes.Equipable.AccesoriesEU:
					if (type == SRTypes.Equipable.AccesoriesCH)
						result.AppendLine("Race: Chinese");
					else
						result.AppendLine("Race: European");
					result.AppendLine("Type: " + (SRTypes.AccesoriesPart)ID4);
					break;
				case SRTypes.Equipable.Weapon:
					result.AppendLine("Type: " + (SRTypes.Weapon)ID4);
					break;
				case SRTypes.Equipable.Avatar:
					result.AppendLine("Type: Avatar " + ((SRTypes.AvatarPart)ID4));
					break;
				default:
					result.AppendLine("Type: " + type);
					break;
			}
			result.AppendLine();
			// Stats
			byte bitMask = 0x1F;
			float bitPercent = 100f / bitMask;
			switch (type)
			{
				case SRTypes.Equipable.Garment:
				case SRTypes.Equipable.Protector:
				case SRTypes.Equipable.Armor:
				case SRTypes.Equipable.Robe:
				case SRTypes.Equipable.LightArmor:
				case SRTypes.Equipable.HeavyArmor:
					result.AppendLine("Physical defense (+" + (byte)((Variance >> 15 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical defense (+" + (byte)((Variance >> 20 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Durability (+" + (byte)((Variance & bitMask) * bitPercent) + "%)");
					result.AppendLine("Parry rate (+" + (byte)((Variance >> 25 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Physical reinforce (+" + (byte)((Variance >> 5 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical reinforce (+" + (byte)((Variance >> 10 & bitMask) * bitPercent) + "%)");
					break;
				case SRTypes.Equipable.Shield:
					result.AppendLine("Physical defense (+" + (byte)((Variance >> 20 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical defense (+" + (byte)((Variance >> 25 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Durability (+" + (byte)((Variance & bitMask) * bitPercent) + "%)");
					result.AppendLine("Block rate (+" + +(byte)((Variance >> 15 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Physical reinforce (+" + (byte)((Variance >> 5 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical reinforce (+" + (byte)((Variance >> 10 & bitMask) * bitPercent) + "%)");
					break;
				case SRTypes.Equipable.AccesoriesCH:
				case SRTypes.Equipable.AccesoriesEU:
					result.AppendLine("Physical absortion (+" + (byte)((Variance & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical absortion (+" + (byte)((Variance >> 5 & bitMask) * bitPercent) + "%)");
					break;
				case SRTypes.Equipable.Weapon:
					result.AppendLine("Physical attack (+" + (byte)((Variance >> 20 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical attack (+" + (byte)((Variance >> 25 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Durability (+" + (byte)((Variance & bitMask) * bitPercent) + "%)");
					result.AppendLine("Hit rate (+" + (byte)((Variance >> 15 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Critical (+" + (byte)((Variance >> 30 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Physical reinforce (+" + (byte)((Variance >> 5 & bitMask) * bitPercent) + "%)");
					result.AppendLine("Magical reinforce (+" + (byte)((Variance >> 10 & bitMask) * bitPercent) + "%)");
					break;
			}
			result.AppendLine();
			// Magic options
			if (MagicOptions.Count > 0)
			{
				for (byte j = 0; j < MagicOptions.Count; j++)
				{
					string text = MagicOptions[j].GetFullName();
					if (text != "")
						result.AppendLine(text);
				}
				result.AppendLine();
			}
			// Adv. Elixir
			if (AdvancedElixirs.Count > 0)
				result.Append("Adv. Elixir (+" + AdvancedElixirs[0].Value + ")");
			return result.ToString();
		}
		public override SRItem Clone()
		{
			return new SREquipable(this);
		}
		#endregion

		[Flags]
		public enum Rarity:byte
		{
			None = 0,
			SOS = 1,
			SOM = 2,
			SUN = 4,
			HONOR = 8,
			NOVA = 16
		}
		public enum Genre : byte
		{
			None = 0,
			Male = 1,
			Female = 2
		}
	}
}
