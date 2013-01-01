using System;
namespace xBot.Game.Objects
{
	public class SRCoord
	{
		public int X { get; }
		public int Y { get; }
		public int Z { get; }
		public double PosX { get; }
		public double PosY { get; }
		public ushort Region { get; }
		public SRCoord(double PosX, double PosY)
		{
			this.PosX = PosX;
			this.PosY = PosY;

			X = (int)Math.Round(PosX) % 192 * 10;
			Y = (int)Math.Round(PosY) % 192 * 10;
			Z = 0;

			byte xSector = (byte)(((int)Math.Round(PosX) - (int)Math.Round(PosX) % 192) / 192 + 135);
			byte ySector = (byte)(((int)Math.Round(PosY) - (int)Math.Round(PosY) % 192) / 192 + 92);
			Region = (ushort)((ySector << 8) | xSector);
		}

		public SRCoord(double PosX, double PosY, ushort Region, int Z = 0)
		{
			this.PosX = PosX;
			this.PosY = PosY;

			X = (int)Math.Round(PosX) % 192 * 10;
			Y = (int)Math.Round(PosY) % 192 * 10;
			this.Z = Z;

			this.Region = Region;
		}
		public SRCoord(ushort Region, int X, int Z, int Y)
		{
			byte ySector = (byte)(Region >> 8);
			byte xSector = (byte)(Region & 0xFF);

			this.Region = Region;
			if (inDungeon())
			{
				PosX = (xSector - 128) * 192 + X / 10;
				PosY = (ySector - 128) * 192 + Y / 10;
			}
			else
			{
				PosX = (xSector - 135) * 192 + X / 10;
				PosY = (ySector - 92) * 192 + Y / 10;
			}
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
		public double DistanceTo(double PosX, double PosY)
		{
			return Math.Sqrt(Math.Pow(PosX - this.PosX, 2.0) + Math.Pow(PosY - this.PosY, 2.0));
		}
		public double DistanceTo(SRCoord Coord)
		{
			return DistanceTo(Coord.PosX, Coord.PosY);
		}
		public override string ToString()
		{
			return "( X : " + Math.Round(PosX) + " - Y : " + Math.Round(PosY) + " ) [ R : " + Region + " - X : " + X + " - Y : " + Y + " - Z : " + Z + "]";
		}
		public bool inDungeon()
		{
      return inDungeon(Region);
		}
		public static bool inDungeon(ushort Region)
		{
			return Region >= short.MaxValue;
		}
	}
}
