using System.Collections.Generic;
namespace xBot.Game
{
	public class Info
	{
		private static Info _this = null;
		/// <summary>
		/// Full reference to the character selected to play.
		/// </summary>
		public SRObject Character { get; set; }
		/// <summary>
		/// All characters displayed on character selection.
		/// </summary>
		public List<SRObject> CharacterList { get; }
		/// <summary>
		/// Track any entity that spawn closer.
		/// </summary>
		public List<SRObject> EntityList { get; }
		/// <summary>
		/// Graphic reference used to display the moon.
		/// </summary>
		public ushort Moonphase { get; internal set; }
		/// <summary>
		/// Graphic reference to display day/night times.
		/// </summary>
		public byte Hour { get; internal set; }
		/// <summary>
		/// Graphic reference to display day/night times.
		/// </summary>
		public byte Minute { get; internal set; }
		private Info()
		{
			Character = null;
			CharacterList = new List<SRObject>();
			EntityList = new List<SRObject>();
		}
		public static Info Get
		{
			get
			{
				if (_this == null)
					_this = new Info();
				return _this;
			}
		}
		/// <summary>
		/// Get an entity by his unique ID.
		/// </summary>
		/// <param name="UniqueID">Spawn object reference</param>
		/// <returns><see cref="null"/> if cannot be found</returns>
		public SRObject getEntity(uint UniqueID)
		{
			return EntityList.Find(spawn => ((uint)spawn[SRAttribute.UniqueID] == UniqueID));
		}
	}
}
