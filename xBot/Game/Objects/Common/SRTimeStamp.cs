using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xBot.Game.Objects.Common
{
	public class SRTimeStamp
	{
		public DateTime DateTime { get; }
		public SRTimeStamp(uint SRTimeStamp)
		{
			int year = (int)(SRTimeStamp & 63) + 2000;
			int month = (int)(SRTimeStamp >> 6) & 15;
			int day = (int)(SRTimeStamp >> 10) & 31;
			int hour = (int)(SRTimeStamp >> 15) & 31;
			int minute = (int)(SRTimeStamp >> 20) & 63;
			int second = (int)(SRTimeStamp >> 26) & 63;
			DateTime = new DateTime(year, month, day, hour, minute, second);
		}
	}
}
