using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xBot.Game.Objects.Common;

namespace xBot.Game.Objects.Entity
{
	public class SRSkillZone : SREntity
	{
		public SRSkill Skill { get; private set; }
		public ushort unkUShort01 { get; set; }
		public uint SkillID
		{
			get
			{
				if (Skill != null)
					return Skill.ID;
				return 0;
      }
			set
			{
				Skill = new SRSkill(value);
				Name = Skill.Name + " (Skill)";
				ServerName = Skill.ServerName;
			}
		}
		public SRSkillZone()
		{
			ID = uint.MaxValue;
    }
	}
}
