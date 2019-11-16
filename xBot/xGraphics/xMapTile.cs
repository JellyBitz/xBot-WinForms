using System.Windows.Forms;

namespace xGraphics
{
	public class xMapTile : PictureBox
	{
		public int SectorX { get; }
		public int SectorY { get; }
		public xMapTile(int SectorX, int SectorY)
		{
			this.SectorX = SectorX;
			this.SectorY = SectorY;
		}
	}
}