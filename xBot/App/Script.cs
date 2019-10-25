using System.Collections.Generic;
namespace xBot.App
{
	public class Script
	{
		List<string> lines;
		public Script()
		{
			this.lines = new List<string>();
		}
		public Script(string[] lines)
		{
			this.lines = new List<string>(lines);
		}
	}
}
