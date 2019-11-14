using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xBot.Game.Objects;
using System.Threading.Tasks;

namespace xGraphics
{
	public class xMap : Panel
	{
		#region Private members
		private SRCoord m_ViewPoint;
		private string m_Base_FilePath;
		private byte m_Zoom;
		private Dictionary<string, Bitmap> m_Cache = new Dictionary<string, Bitmap>();
		private Dictionary<uint, xMapControl> m_Markers = new Dictionary<uint, xMapControl>();
		#endregion

		public xMap()
		{
			this.DoubleBuffered = true;
			this.BackColor = Color.Transparent;

			this.m_Zoom = 1;
			this.m_ViewPoint = new SRCoord(0, 0);
			SelectMapLayer(m_ViewPoint.Region);
			LoadAsyncBackgroundImageMap();
		}
		/// <summary>
		/// Get or set the map view.
		/// </summary>
		public SRCoord ViewPoint
		{
			get { return m_ViewPoint; }
			set
			{
				if (!m_ViewPoint.Equals(value))
				{
					if (m_ViewPoint.Region != value.Region || m_ViewPoint.xSector != value.xSector || m_ViewPoint.ySector != value.ySector )
						SelectMapLayer(value.Region);
					m_ViewPoint = value;
					LoadAsyncBackgroundImageMap();
					UpdateAllMarkerLocations();
				}
				else
				{
					UpdateAllMarkerLocations();
				}
			}
		}
		public new Size Size
		{
			get { return base.Size; }
			set
			{
				base.Size = value;
				m_Cache.Clear();
				LoadBackgroundImageMap();
				UpdateAllMarkerLocations();
			}
		}
		/// <summary>
		/// Selects the zoom level
		/// </summary>
		public byte Zoom
		{
			get { return m_Zoom; }
			set
			{
				if (value != m_Zoom)
				{
					m_Zoom = value;
					m_Cache.Clear();
					LoadBackgroundImageMap();
					UpdateAllMarkerLocations();
				}
			}
		}
		/// <summary>
		/// Gets the tile size of the map from zoom level.
		/// </summary>
		public int TileSize
		{
			get { return (int)Math.Round((Size.Width < Size.Height ? Size.Width : Size.Height / (2.0 * m_Zoom + 1)), MidpointRounding.AwayFromZero); }
		}
		#region Map Layer Painting
		private async void LoadAsyncBackgroundImageMap()
		{
			this.BackgroundImage = await Task.Run(() => GetBackgroundImageMap() );
		}
		private void LoadBackgroundImageMap()
		{
			BackgroundImage = GetBackgroundImageMap();
		}
		private Bitmap GetBackgroundImageMap(){
			// Generic map stuffs
			int tileCount = 2 * m_Zoom + 3;
			int tileSize = TileSize;
			// Calculate the sectors range to draw
			int tileAvg = tileCount / 2;
			// Margin to point center
			int relativePosX = (int)Math.Round((ViewPoint.PosX % 192) * tileSize / 192.0 + (ViewPoint.PosX < 0 ? tileSize : 0));
			int relativePosY = (int)Math.Round((ViewPoint.PosY % 192) * tileSize / 192.0 + (ViewPoint.PosY < 0 ? tileSize : 0));
			int marginX = (int)Math.Round(tileSize / 2.0 - tileSize - relativePosX);
			int marginY = (int)Math.Round(tileSize / 2.0 - tileSize * 2 + relativePosY);
			// Draw all sectors involved
			Bitmap backgroundImage = new Bitmap(tileCount * tileSize, tileCount * tileSize);
			Graphics g = Graphics.FromImage(backgroundImage);
			int i = 0;
			for (int ii = tileAvg + ViewPoint.ySector; ii >= -tileAvg + ViewPoint.ySector; ii--)
			{
				int j = 0;
				for (int jj = -tileAvg + ViewPoint.xSector; jj <= tileAvg + ViewPoint.xSector; jj++)
				{
					Bitmap img = null;
					string path = string.Format(m_Base_FilePath, jj, ii);
					// Check if has been loaded
					if (m_Cache.TryGetValue(path, out img))
					{
						// Paint
						g.DrawImage(img, j * tileSize + marginX, i * tileSize + marginY, tileSize, tileSize);
					}
					else
					{
						// Load
						if (File.Exists(path))
						{
							// Resize
							img = new Bitmap((Bitmap)Image.FromFile(path), tileSize, tileSize);
							m_Cache[path] = img;
							// Paint
							g.DrawImage(img, j * tileSize + marginX, i * tileSize + marginY, tileSize, tileSize);
						}
					}
					j++;
				}
				i++;
			}
			return backgroundImage;
    }
		private void SelectMapLayer(ushort Region)
		{
			switch (Region)
			{
				case 32769:
					if (ViewPoint.Z < 115)
						m_Base_FilePath = "Minimap/d/dh_a01_floor01_{0}x{1}.jpg";
					else if (ViewPoint.Z < 230)
						m_Base_FilePath = "Minimap/d/dh_a01_floor02_{0}x{1}.jpg";
					else if (ViewPoint.Z < 345)
						m_Base_FilePath = "Minimap/d/dh_a01_floor03_{0}x{1}.jpg";
					else
						m_Base_FilePath = "Minimap/d/dh_a01_floor04_{0}x{1}.jpg";
					break;
				case 32770:
					m_Base_FilePath = "Minimap/d/qt_a01_floor06_{0}x{1}.jpg";
					break;
				case 32771:
					m_Base_FilePath = "Minimap/d/qt_a01_floor05_{0}x{1}.jpg";
					break;
				case 32772:
					m_Base_FilePath = "Minimap/d/qt_a01_floor04_{0}x{1}.jpg";
					break;
				case 32773:
					m_Base_FilePath = "Minimap/d/qt_a01_floor03_{0}x{1}.jpg";
					break;
				case 32774:
					m_Base_FilePath = "Minimap/d/qt_a01_floor02_{0}x{1}.jpg";
					break;
				case 32775:
					m_Base_FilePath = "Minimap/d/qt_a01_floor01_{0}x{1}.jpg";
					break;
				case 32778:
				case 32779:
				case 32780:
				case 32781:
				case 32782:
				case 32784:
					m_Base_FilePath = "Minimap/d/RN_SD_EGYPT1_01_{0}x{1}.jpg";
					break;
				case 32783:
					m_Base_FilePath = "Minimap/d/RN_SD_EGYPT1_02_{0}x{1}.jpg";
					break;
				case 32785:
					if (ViewPoint.Z < 115)
						m_Base_FilePath = "Minimap/d/fort_dungeon01_{0}x{1}.jpg";
					else if (ViewPoint.Z < 230)
						m_Base_FilePath = "Minimap/d/fort_dungeon02_{0}x{1}.jpg";
					else if (ViewPoint.Z < 345)
						m_Base_FilePath = "Minimap/d/fort_dungeon03_{0}x{1}.jpg";
					else
						m_Base_FilePath = "Minimap/d/fort_dungeon04_{0}x{1}.jpg";
					break;
				case 32786:
					m_Base_FilePath = "Minimap/d/flame_dungeon01_{0}x{1}.jpg";
					break;
				case 32787:
					m_Base_FilePath = "Minimap/d/RN_JUPITER_02_{0}x{1}.jpg";
					break;
				case 32788:
					m_Base_FilePath = "Minimap/d/RN_JUPITER_03_{0}x{1}.jpg";
					break;
				case 32789:
					m_Base_FilePath = "Minimap/d/RN_JUPITER_04_{0}x{1}.jpg";
					break;
				case 32790:
					m_Base_FilePath = "Minimap/d/RN_JUPITER_01_{0}x{1}.jpg";
					break;
				case 32793:
					m_Base_FilePath = "Minimap/d/RN_ARABIA_FIELD_02_BOSS_{0}x{1}.jpg";
					break;
				default:
					m_Base_FilePath = "Minimap/{0}x{1}.jpg";
					break;
			}
		}
		#endregion

		/// <summary>
		/// Clear the images not used on memory.
		/// </summary>
		public void ClearCache()
		{
			int tileCount = 2 * m_Zoom + 3;
			int minAvg = tileCount / 2;

			int ySectorMin = -minAvg + ViewPoint.ySector;
			int ySectorMax = -minAvg + ViewPoint.ySector;
			int xSectorMin = -minAvg + ViewPoint.xSector;
			int xSectorMax = -minAvg + ViewPoint.xSector;

			List<string> deleteCache = new List<string>();
			foreach (string path in m_Cache.Keys)
			{
				MatchCollection m = Regex.Matches(path, "([0-9]*)x([0-9]*).jpg");
				int temp = int.Parse(m[0].Groups[1].Value);
				if (temp < xSectorMin || temp > xSectorMax)
				{
					deleteCache.Add(path);
				}
				else
				{
					temp = int.Parse(m[0].Groups[2].Value);
					if (temp < ySectorMin || temp > ySectorMax)
						deleteCache.Add(path);
				}
			}
			for (int i = 0; i < deleteCache.Count; i++)
				m_Cache.Remove(deleteCache[i]);
		}

		#region Converting Coords <-> Point
		public Point GetPoint(SRCoord coords)
		{
			// Generic map stuffs
			int tileCount = 2 * m_Zoom + 3;
			int tileSize = TileSize;
			int tileAvg = tileCount / 2;
			// Convertion
			Point p = new Point();
			p.X = (int)Math.Round((coords.PosX - ViewPoint.PosX) / (192.0 / tileSize) + tileSize * tileAvg - tileSize / 2.0);
			p.Y = (int)Math.Round((coords.PosY - ViewPoint.PosY) / (192.0 / tileSize) * (-1) + tileSize * tileAvg - tileSize / 2.0);
			return p;
		}
		public SRCoord GetCoord(Point point)
		{
			// Generic map stuffs
			int tileCount = 2 * m_Zoom + 3;
			int tileSize = TileSize;
			int tileAvg = tileCount / 2;
			// Convertion
			double coordX = (point.X + tileSize / 2.0 - tileSize * tileAvg) * 192 / tileSize + ViewPoint.PosX;
			double coordY = (point.Y + tileSize / 2.0 - tileSize * tileAvg) * 192 / tileSize * (-1) + ViewPoint.PosY;
			if (ViewPoint.inDungeon())
				return new SRCoord(coordX, coordY, ViewPoint.Region, ViewPoint.Z);
			return new SRCoord(coordX, coordY);
		}
		#endregion

		#region Markers Behavior
		public void AddMarker(uint UniqueID, xMapControl Marker)
		{
			this.m_Markers[UniqueID] = Marker;
			this.Controls.Add(Marker);
		}
		public void RemoveMarker(uint UniqueID)
		{
			xMapControl Marker;
			if (this.m_Markers.TryGetValue(UniqueID, out Marker))
			{
				this.m_Markers.Remove(UniqueID);
				this.Controls.Remove(Marker);
			}
		}
		public void ClearMarkers()
		{
			foreach (xMapControl Marker in this.m_Markers.Values)
				this.Controls.Remove(Marker);
			this.m_Markers.Clear();
		}
		public void UpdateAllMarkerLocations()
		{
			// Generic map stuffs
			int tileSize = TileSize;
			// Convertion formula not required to recalculate
			double a = tileSize * (m_Zoom + (3 / 2)) - tileSize / 2.0;
			double b = (192.0 / tileSize);
			// Update all markers
			foreach (xMapControl Marker in this.m_Markers.Values)
			{
				// Convertion SRCoord -> Point
				SRCoord coords = ((SRObject)Marker.Tag).GetPosition();
				Point location = new Point((int)Math.Round((coords.PosX - ViewPoint.PosX) / b + a), (int)Math.Round((coords.PosY - ViewPoint.PosY) / b * (-1) + a));
				// Fix center
				location.X -= Marker.Image.Size.Width / 2;
				location.Y -= Marker.Image.Size.Height / 2;
				// Update only if is required to avoid trigger repaint on control
				if(Marker.Location.X != location.X && Marker.Location.Y != location.Y)
				{
					Marker.Location = location;
				}
			}
		}

		#endregion
	}
}
