using System;
namespace xBot.Game.Objects.Common
{
	public class SRCoord
	{
		public int X { get; }
		public int Y { get; }
		public int Z { get; }
		public double PosX { get; }
		public double PosY { get; }
		public ushort Region { get; }
		public byte ySector { get; }
		public byte xSector { get; }
		public SRCoord(double PosX, double PosY)
		{
			this.PosX = PosX;
			this.PosY = PosY;

			this.X = (int)(Math.Abs(PosX) % 192.0 * 10.0);
			if (PosX < 0.0)
				this.X = 1920 - this.X;
			this.Y = (int)(Math.Abs(PosY) % 192.0 * 10.0);
			if (PosY < 0.0)
				this.Y = 1920 - this.Y;

			this.xSector = (byte)Math.Round((PosX - X / 10.0) / 192.0 + 135);
			this.ySector = (byte)Math.Round((PosY - Y / 10.0) / 192.0 + 92);

			this.Region = (ushort)((ySector << 8) | xSector);
		}
		public SRCoord(ushort Region, int X, int Z, int Y)
		{
			this.Region = Region;
			if (inDungeon())
			{
				this.PosX = 128 * 192 + X / 10;
				this.PosY = 128 * 192 + Y / 10;
				this.xSector = (byte)(((128.0 * 192.0 + this.PosX) / 192.0) - 128);
				this.ySector = (byte)(((128.0 * 192.0 + this.PosY) / 192.0) - 128);
			}
			else
			{
				this.xSector = (byte)(Region & 0xFF);
				this.ySector = (byte)(Region >> 8);
				this.PosX = (xSector - 135) * 192 + X / 10;
				this.PosY = (ySector - 92) * 192 + Y / 10;
			}

			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}
		public SRCoord(double PosX, double PosY, ushort Region, int Z = 0)
		{
			this.PosX = PosX;
			this.PosY = PosY;

			this.Region = Region;
			if (inDungeon())
			{
				this.xSector = (byte)(((128.0 * 192.0 + this.PosX) / 192.0) - 128);
				this.ySector = (byte)(((128.0 * 192.0 + this.PosY) / 192.0) - 128);
				this.X = (int)Math.Round((this.PosX - 128.0 * 192.0) * 10.0);
				this.Y = (int)Math.Round((this.PosY - 128.0 * 192.0) * 10.0);
			}
			else
			{
				this.X = (int)(Math.Abs(PosX) % 192.0 * 10.0);
				if (PosX < 0.0)
					this.X = 1920 - this.X;
				this.Y = (int)(Math.Abs(PosY) % 192.0 * 10.0);
				if (PosY < 0.0)
					this.Y = 1920 - this.Y;

				this.xSector = (byte)Math.Round((PosX - X / 10.0) / 192.0 + 135);
				this.ySector = (byte)Math.Round((PosY - Y / 10.0) / 192.0 + 92);
			}

			this.Z = Z;
		}
		public bool Equals(SRCoord Coord)
		{
			if (this.Region == this.Region)
				return this.DistanceTo(Coord) <= 1;
			return false;
		}
		public double DistanceTo(double PosX, double PosY,ushort Region)
		{
			return Math.Sqrt(Math.Pow(PosX - this.PosX, 2.0) + Math.Pow(PosY - this.PosY, 2.0));
		}
		public double DistanceTo(SRCoord Coord)
		{
			return DistanceTo(Coord.PosX, Coord.PosY,Coord.Region);
		}
		public int TimeTo(SRCoord Coord, double SpeedPerMs)
		{
			return (int)Math.Round(DistanceTo(Coord) / SpeedPerMs);
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
			return Region > short.MaxValue;
		}
	}
}