using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace xBot.Game.Objects
{
	/// <summary>
	/// Class to keep control of dinamyc & static arrays where the index matter.
	/// </summary>
	public class SRObjectCollection
	{
		#region Propierties
		/// <summary>
		/// List acting as dynamic and static.
		/// </summary>
		private List<SRObject> Objects;
		/// <summary>
		/// Maximum capacity.
		/// </summary>
		public int Capacity { get { return Objects.Count; } }
		/// <summary>
		/// Counter of objects not null.
		/// </summary>
		public int Count { get; private set; }
		/// <summary>
		/// Gets or sets a value.
		/// </summary>
		public SRObject this[int index]
		{
			get
			{
				return Objects[index];
			}
			set
			{
				if (index >= Objects.Count)
				{
					// Expand the list
					for (int i = Objects.Count; i <= index; i++)
						Objects.Add(null);
				}
				// Keep control about real objects at list
				if (value == null && Objects[index] != null)
				{
					Count--;
				}
				else if (value != null && Objects[index] == null)
				{
					Count++;
				}
				// Set new value
				Objects[index] = value;
			}
		}
		#endregion
		#region Constructor
		/// <summary>
		/// Creates a <see cref="List{T}"/> that will be handled as static.
		/// </summary>
		public SRObjectCollection()
		{
			Objects = new List<SRObject>();
			Count = 0;
		}
		/// <summary>
		/// Create a <see cref="List{T}"/> that will be handled as static.
		/// <para>If the Capacity is excedded will be automagically expanded to his new maximum index.</para>
		/// </summary>
		/// <param name="Capacity">Maximum length of the list</param>
		public SRObjectCollection(uint Capacity)
		{
			Objects = new List<SRObject>((int)Capacity);
			for (int i = 0; i < Capacity; i++)
				Objects.Add(null);
			Count = 0;
		}
		public SRObjectCollection(SRObject[] Array)
		{
			Count = 0;
			Objects = new List<SRObject>(Array);
			for (int i = 0; i < Array.Length; i++)
				if (Array[i] != null)
					Count++;
		}
		/// <summary>
		/// Only for clonation.
		/// </summary>
		private SRObjectCollection(SRObjectCollection collection)
		{
			Objects = new List<SRObject>(collection.Objects);
			Count = collection.Count;
		}
		/// <summary>
		/// Creates a deep copy from the current object.
		/// </summary>
		public SRObjectCollection Clone()
		{
			return new SRObjectCollection(this);
		}
		#endregion
		#region Methods
		public void Add(SRObject value)
		{
			this[Capacity] = value;
		}
		public void Remove(SRObject value)
		{
			Objects.Remove(value);
		}
		public void RemoveAt(int index)
		{
			if (index < Objects.Count)
			{
				if (Objects[index] != null)
					Count--;
				Objects.RemoveAt(index);
			}
		}
		public SRObject Last()
		{
			return this[Capacity-1];
		}
		public void Clear()
		{
			Objects.Clear();
			Count = 0;
		}
		public bool Exists(Predicate<SRObject> match)
		{
			return Objects.Exists(match);
		}
		public SRObject Find(Predicate<SRObject> match)
		{
			return Objects.Find(match);
		}
		public int FindIndex(Predicate<SRObject> match, int startIndex = 0)
		{
			return FindIndex(match, startIndex, this.Capacity - 1);
		}
		public int FindIndex(Predicate<SRObject> match, int startIndex, int endIndex)
		{
			for (int i = startIndex; i <= endIndex; i++)
				if (match(Objects[i]))
					return i;
			return -1;
		}
		public int FindIndexLimited(Predicate<SRObject> match, int startIndex, int maxIndex)
		{
			for (int i = startIndex; i <= maxIndex && i < Objects.Count; i++)
				if (match(Objects[i]))
					return i;
			return -1;
		}
		public TreeNode[] ToNodes()
		{
			TreeNode[] nodes = new TreeNode[Objects.Count];
			int i = 0;
			foreach (SRObject value in Objects)
			{
				if (value == null)
					nodes[i] = new TreeNode("Empty");
				else
					nodes[i] = value.ToNode();
				i++;
			}
			return nodes;
		}
		#endregion
	}
}
