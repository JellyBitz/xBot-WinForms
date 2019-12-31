using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using xBot.App;
using xBot.Game;
using xBot.Game.Objects;
using xBot.Game.Objects.Common;
using xBot.Game.Objects.Entity;

namespace xGraphics
{
	public class xMap : Panel
	{
		#region Private members
		private Dictionary<string, xMapTile> m_Sectors = new Dictionary<string, xMapTile>();
		private SRCoord m_ViewPoint;
		private string m_FilePath;
		private byte m_Zoom;
		private Size m_TileSize;
		private int m_TileCount;
		private xDictionary<uint, xMapControl> m_Markers = new xDictionary<uint, xMapControl>();
		#endregion

		public xMap()
		{
			// Initialize
			base.DoubleBuffered = true;
			this.m_ViewPoint = new SRCoord(0, 0);
			this.m_Zoom = 0;
			base.Size = new Size(600, 600);
			this.m_TileSize = new Size((int)Math.Round(base.Width / (2.0 * m_Zoom + 1), MidpointRounding.AwayFromZero), (int)Math.Round(base.Height / (2.0 * m_Zoom + 1), MidpointRounding.AwayFromZero));
			this.m_TileCount = 2 * m_Zoom + 3;
			// Create layer
			SelectMapLayer(m_ViewPoint.Region);
			UpdateTiles();
		}

		#region Properties
		/// <summary>
		/// Get the camera center.
		/// </summary>
		public SRCoord ViewPoint
		{
			get { return m_ViewPoint; }
		}
		/// <summary>
		/// Resize map.
		/// </summary>
		public new Size Size
		{
			get { return base.Size; }
			set
			{
				base.Size = value;
				this.m_TileSize = new Size((int)Math.Round(base.Width / (2.0 * m_Zoom + 1), MidpointRounding.AwayFromZero), (int)Math.Round(base.Height / (2.0 * m_Zoom + 1), MidpointRounding.AwayFromZero));
				RemoveTiles();
				UpdateTiles();
				UpdateMarkerLocations();
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
					m_TileSize = new Size((int)Math.Round(base.Width / (2.0 * m_Zoom + 1), MidpointRounding.AwayFromZero), (int)Math.Round(base.Height / (2.0 * m_Zoom + 1), MidpointRounding.AwayFromZero));
					m_TileCount = 2 * m_Zoom + 3;
					RemoveTiles();
					UpdateTiles();
					UpdateMarkerLocations();
				}
			}
		}
		/// <summary>
		/// The size from tiles actually on map.
		/// </summary>
		public Size TileSize { get { return m_TileSize; } }
		/// <summary>
		/// The tile quantity visible on map.
		/// </summary>
		public int TileCount { get { return m_TileCount; } }
		#endregion

		#region Layer update
		/// <summary>
		/// Selects the map layer linked from region.
		/// </summary>
		private void SelectMapLayer(ushort Region)
		{
			switch (Region)
			{
				case 32769:
					if (m_ViewPoint.Z < 115)
						m_FilePath = "Minimap/d/dh_a01_floor01_{0}x{1}.jpg";
					else if (m_ViewPoint.Z < 230)
						m_FilePath = "Minimap/d/dh_a01_floor02_{0}x{1}.jpg";
					else if (m_ViewPoint.Z < 345)
						m_FilePath = "Minimap/d/dh_a01_floor03_{0}x{1}.jpg";
					else
						m_FilePath = "Minimap/d/dh_a01_floor04_{0}x{1}.jpg";
					break;
				case 32770:
					m_FilePath = "Minimap/d/qt_a01_floor06_{0}x{1}.jpg";
					break;
				case 32771:
					m_FilePath = "Minimap/d/qt_a01_floor05_{0}x{1}.jpg";
					break;
				case 32772:
					m_FilePath = "Minimap/d/qt_a01_floor04_{0}x{1}.jpg";
					break;
				case 32773:
					m_FilePath = "Minimap/d/qt_a01_floor03_{0}x{1}.jpg";
					break;
				case 32774:
					m_FilePath = "Minimap/d/qt_a01_floor02_{0}x{1}.jpg";
					break;
				case 32775:
					m_FilePath = "Minimap/d/qt_a01_floor01_{0}x{1}.jpg";
					break;
				case 32778:
				case 32779:
				case 32780:
				case 32781:
				case 32782:
				case 32784:
					m_FilePath = "Minimap/d/RN_SD_EGYPT1_01_{0}x{1}.jpg";
					break;
				case 32783:
					m_FilePath = "Minimap/d/RN_SD_EGYPT1_02_{0}x{1}.jpg";
					break;
				case 32785:
					if (m_ViewPoint.Z < 115)
						m_FilePath = "Minimap/d/fort_dungeon01_{0}x{1}.jpg";
					else if (m_ViewPoint.Z < 230)
						m_FilePath = "Minimap/d/fort_dungeon02_{0}x{1}.jpg";
					else if (m_ViewPoint.Z < 345)
						m_FilePath = "Minimap/d/fort_dungeon03_{0}x{1}.jpg";
					else
						m_FilePath = "Minimap/d/fort_dungeon04_{0}x{1}.jpg";
					break;
				case 32786:
					m_FilePath = "Minimap/d/flame_dungeon01_{0}x{1}.jpg";
					break;
				case 32787:
					m_FilePath = "Minimap/d/RN_JUPITER_02_{0}x{1}.jpg";
					break;
				case 32788:
					m_FilePath = "Minimap/d/RN_JUPITER_03_{0}x{1}.jpg";
					break;
				case 32789:
					m_FilePath = "Minimap/d/RN_JUPITER_04_{0}x{1}.jpg";
					break;
				case 32790:
					m_FilePath = "Minimap/d/RN_JUPITER_01_{0}x{1}.jpg";
					break;
				case 32793:
					m_FilePath = "Minimap/d/RN_ARABIA_FIELD_02_BOSS_{0}x{1}.jpg";
					break;
				default:
					m_FilePath = "Minimap/{0}x{1}.jpg";
					break;
			}
		}
		/// <summary>
		/// Update all tiles.
		/// </summary>
		public void UpdateTiles()
		{
			// Calculate the sectors range to draw
			int tileAvg = m_TileCount / 2;
			// Margin to point center
			int relativePosX = (int)Math.Round((m_ViewPoint.PosX % 192) * m_TileSize.Width / 192.0 + (m_ViewPoint.PosX < 0 ? m_TileSize.Width : 0));
			int relativePosY = (int)Math.Round((m_ViewPoint.PosY % 192) * m_TileSize.Height / 192.0 + (m_ViewPoint.PosY < 0 ? m_TileSize.Height : 0));
			int marginX = (int)Math.Round(m_TileSize.Width / 2.0 - m_TileSize.Width - relativePosX);
			int marginY = (int)Math.Round(m_TileSize.Height / 2.0 - m_TileSize.Height * 2 + relativePosY);

			this.InvokeIfRequired(()=> {
				// Locate/Resize all sectors involved
				int i = 0;
				for (int sectorY = tileAvg + m_ViewPoint.ySector; sectorY >= -tileAvg + m_ViewPoint.ySector; sectorY--)
				{
					int j = 0;
					for (int sectorX = -tileAvg + m_ViewPoint.xSector; sectorX <= tileAvg + m_ViewPoint.xSector; sectorX++)
					{
						xMapTile sector = null;
						string path = string.Format(m_FilePath, sectorX, sectorY);
						Point sectorLocation = new Point(j * m_TileSize.Width + marginX, i * m_TileSize.Height + marginY);

						// Check if has been loaded
						if (m_Sectors.TryGetValue(path, out sector))
						{
							// Just update it if changes
							if (sector.Location.X != sectorLocation.X || sector.Location.Y != sectorLocation.Y)
								sector.Location = sectorLocation;
							if (m_TileSize.Width != sector.Size.Width || m_TileSize.Height != sector.Size.Height)
								sector.Size = m_TileSize;
							sector.Visible = true;
						}
						else
						{
							// Create tile
							sector = new xMapTile(sectorX, sectorY);
							sector.Name = path;
							sector.Size = m_TileSize;
							sector.Location = sectorLocation;
							sector.MouseClick += new MouseEventHandler(this.xMapTile_MouseClick);
							// Try to Load
							sector.LoadAsyncTile(path, m_TileSize);
							// Add
							m_Sectors[path] = sector;
							this.Controls.Add(sector);
							sector.SendToBack();
						}
						j++;
					}
					i++;
				}
			});
		}
		/// <summary>
		/// Clear the tiles not visibles from memory.
		/// </summary>
		public void ClearCache()
		{
			int minAvg = m_TileCount / 2;
			int ySectorMin = -minAvg + ViewPoint.ySector;
			int ySectorMax = -minAvg + ViewPoint.ySector;
			int xSectorMin = -minAvg + ViewPoint.xSector;
			int xSectorMax = -minAvg + ViewPoint.xSector;

			List<string> deleteCache = new List<string>();
			foreach (xMapTile tile in m_Sectors.Values)
			{
				if (tile.SectorX < xSectorMin || tile.SectorX > xSectorMax
					|| tile.SectorY < ySectorMin || tile.SectorY > ySectorMax)
				{
					deleteCache.Add(tile.Name);
				}
			}
			if (deleteCache.Count>0)
			{
				this.InvokeIfRequired(() => {
					for (int i = 0; i < deleteCache.Count; i++)
					{
						this.Controls.RemoveByKey(deleteCache[i]);
						m_Sectors.Remove(deleteCache[i]);
					}
				});
			}
		}
		/// <summary>
		/// Clear all tiles not visibles from map.
		/// </summary>
		private void ClearTiles()
		{
			int minAvg = m_TileCount / 2;

			int ySectorMin = -minAvg + ViewPoint.ySector;
			int ySectorMax = -minAvg + ViewPoint.ySector;
			int xSectorMin = -minAvg + ViewPoint.xSector;
			int xSectorMax = -minAvg + ViewPoint.xSector;

			List<string> notVisible = new List<string>();
			foreach (xMapTile tile in m_Sectors.Values)
			{
				if (tile.SectorX < xSectorMin || tile.SectorX > xSectorMax
					|| tile.SectorY < ySectorMin || tile.SectorY > ySectorMax)
				{
					tile.InvokeIfRequired(() => {
						tile.Visible = false;
					});
				}
			}
		}
		private void RemoveTiles()
		{
			this.InvokeIfRequired(() => {
				foreach (xMapTile tile in m_Sectors.Values){
					this.Controls.RemoveByKey(tile.Name);
				}
			});
			m_Sectors.Clear();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Set the camera center, all map is updated after that.
		/// Invoke not required.
		/// </summary>
		public void SetView(SRCoord ViewPoint)
		{
			if (!m_ViewPoint.Equals(ViewPoint))
			{
				// Update layer
				if (m_ViewPoint.Region != ViewPoint.Region && ViewPoint.inDungeon())
				{
					SelectMapLayer(ViewPoint.Region);
					RemoveTiles();
					m_ViewPoint = ViewPoint;
				}
				else
				{
					m_ViewPoint = ViewPoint;
					ClearTiles();
				}
				UpdateTiles();
			}
			UpdateMarkerLocations();
		}
		#endregion

		#region Converting Coords <-> Point
		public Point GetPoint(SRCoord coords)
		{
			// Generic map stuffs
			int tileAvg = m_TileCount / 2;
			// Convertion
			Point p = new Point();
			p.X = (int)Math.Round((coords.PosX - ViewPoint.PosX) / (192.0 / m_TileSize.Width) + m_TileSize.Width * tileAvg - m_TileSize.Width / 2.0);
			p.Y = (int)Math.Round((coords.PosY - ViewPoint.PosY) / (192.0 / m_TileSize.Height) * (-1) + m_TileSize.Height * tileAvg - m_TileSize.Height / 2.0);
			return p;
		}
		public SRCoord GetCoord(Point point)
		{
			// Generic map stuffs
			int tileAvg = m_TileCount / 2;
			// Convertion
			double coordX = (point.X + m_TileSize.Width / 2.0 - m_TileSize.Width * tileAvg) * 192 / m_TileSize.Width + ViewPoint.PosX;
			double coordY = (point.Y + m_TileSize.Height / 2.0 - m_TileSize.Height * tileAvg) * 192 / m_TileSize.Height * (-1) + ViewPoint.PosY;
			if (ViewPoint.inDungeon())
				return new SRCoord(coordX, coordY, ViewPoint.Region, ViewPoint.Z);
			return new SRCoord(coordX, coordY);
		}
		#endregion

		#region Markers Behavior
		public void AddMarker(uint UniqueID, xMapControl Marker)
		{
			Marker.Name = this.Name + "_" + UniqueID;
			this.InvokeIfRequired(()=> {
				Controls.Add(Marker);
				Controls.SetChildIndex(Marker, 1);
				m_Markers[UniqueID] = Marker;
			});
		}
		public void RemoveMarker(uint UniqueID)
		{
			xMapControl Marker = m_Markers[UniqueID];
			if (Marker != null)
			{
				this.InvokeIfRequired(() => {
					this.Controls.RemoveByKey(Marker.Name);
				});
				this.m_Markers.RemoveKey(UniqueID);
			}
		}
		public void ClearMarkers()
		{
			this.InvokeIfRequired(() => {
				for (int i = 0; i < m_Markers.Count; i++)
					this.Controls.RemoveByKey(m_Markers.GetAt(i).Name);
			});
			this.m_Markers.Clear();
		}
		public void UpdateMarkerLocations()
		{
			// Convertion formula not required to recalculate everytime
			double a_x = m_TileSize.Width * (m_Zoom + (3 / 2)) - m_TileSize.Width / 2.0;
			double a_y = m_TileSize.Height * (m_Zoom + (3 / 2)) - m_TileSize.Height / 2.0;
			double b_x = (192.0 / m_TileSize.Width);
			double b_y = (192.0 / m_TileSize.Height);
			// Update all markers
			this.InvokeIfRequired(()=> {
				for (int i = 0; i < m_Markers.Count; i++)
				{
					xMapControl Marker = m_Markers.GetAt(i);
					// Convertion SRCoord -> Point
					SRCoord coords = ((SREntity)Marker.Tag).GetRealtimePosition();
					Point location = new Point((int)Math.Round((coords.PosX - ViewPoint.PosX) / b_x + a_x), (int)Math.Round((coords.PosY - ViewPoint.PosY) / b_y * (-1) + a_y));
					// Fix center
					location.X -= Marker.Image.Size.Width / 2;
					location.Y -= Marker.Image.Size.Height / 2;
					// Update only if is required to avoid trigger repaint on control
					if (Marker.Location.X != location.X && Marker.Location.Y != location.Y){
						Marker.Location = location;
					}
				}
			});
		}
		#endregion

		#region Events
		private void xMapTile_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				xMapTile t = (xMapTile)sender;
				if (InfoManager.inGame)
					Bot.Get.MoveTo(GetCoord(new Point(t.Location.X + e.Location.X, t.Location.Y + e.Location.Y)));
			}
		}
		#endregion
	}
}