using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Entity
{
	public class SRNpc:SRModel
	{

		public byte[] TalkOptions { get; set; }
		public bool hasTalk { get { return TalkOptions != null; } }

		#region (Constructor)
		public SRNpc(uint ID) : base(ID) { }
		public SRNpc(string ServerName) : base(ServerName) { }
		public SRNpc(SRModel value) : base(value) { }
		#endregion

		#region (Methods)
		public bool isMob()
		{
			return ID3 == 1;
		}
		public bool isGuide()
		{
			return ID3 == 2;
		}
		public bool isCOS()
		{
			return ID3 == 3;
		}
		public bool isFortressCos() {
			return ID3 == 4;
		}
		public bool isFortressStruct()
		{
			return ID3 == 5;
		}
		#endregion
	}
}
