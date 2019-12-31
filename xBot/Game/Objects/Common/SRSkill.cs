using System;
using System.Collections.Specialized;
using System.Diagnostics;
using static xBot.Game.Objects.Common.SRTypes;

namespace xBot.Game.Objects.Common
{
	public class SRSkill
	{
		private Stopwatch m_CooldownTimer;
		public uint ID { get; set; }
		public string ServerName { get; set; }
		public string Name { get; set; }
		public uint GroupID { get; set; }
		public string GroupName { get; set; }
		public uint Cooldown { get; set; }
		public uint DurationMax { get; set; }
		public int CastingTime { get; set; }
		public uint MPUsage { get; set; }
		public bool isTargetRequired { get; }
		public byte Level { get; set; }
		public string Params { get; set; }
		public string Icon { get; set; }
		public Weapon RequiredWeaponPrimary { get; set; }
		public Weapon RequiredWeaponSecondary { get; set; }
		public bool Enabled { get; set; }
		public bool isCastingEnabled {
			get {
				if (m_CooldownTimer != null)
				{
					if (m_CooldownTimer.ElapsedMilliseconds < Cooldown)
						return false;
					m_CooldownTimer.Stop();
				}
				return true;
			}
		}
		public SRSkill(uint ID)
		{
			NameValueCollection data = DataManager.GetSkillData(ID);

			this.ID = ID;
			ServerName = data["servername"];
			Name = data["name"];
			GroupID = uint.Parse(data["group_id"]);
			GroupName = data["group_name"];
			Cooldown = uint.Parse(data["cooldown"]);
			DurationMax = uint.Parse(data["duration"]);
			CastingTime = int.Parse(data["casttime"]);
			MPUsage = uint.Parse(data["mana"]);
			isTargetRequired = data["target_required"] == "1";
			Level = byte.Parse(data["level"]);
			Params = data["params"];
			Icon = data["icon"];
			RequiredWeaponPrimary = (Weapon)byte.Parse(data["weapon_first"]);
			RequiredWeaponSecondary = (Weapon)byte.Parse(data["weapon_second"]);
    }
		public SRSkill(string ServerName)
		{
			NameValueCollection data = DataManager.GetSkillData(ServerName);

			this.ServerName = ServerName;
			ID = uint.Parse(data["id"]);
			Name = data["name"];
			GroupID = uint.Parse(data["group_id"]);
			GroupName = data["group_name"];
			Cooldown = uint.Parse(data["cooldown"]);
			DurationMax = uint.Parse(data["duration"]);
			CastingTime = int.Parse(data["casttime"]);
			MPUsage = uint.Parse(data["mana"]);
			isTargetRequired = data["target_required"] == "1";
			Level = byte.Parse(data["level"]);
			Params = data["params"];
			Icon = data["icon"];
			RequiredWeaponPrimary = (Weapon)byte.Parse(data["weapon_first"]);
			RequiredWeaponSecondary = (Weapon)byte.Parse(data["weapon_second"]);
		}

		public void StartCooldown()
		{
			if (m_CooldownTimer == null)
				m_CooldownTimer = Stopwatch.StartNew();
			else
				m_CooldownTimer.Restart();
		}
		public bool isAttackingSkill()
		{
			if (MPUsage > 0 && DurationMax == 0)
				return true;
			return false;
		}
	}
}
