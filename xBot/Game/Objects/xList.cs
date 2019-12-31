using System;
using System.Collections.Generic;
namespace xBot.Game.Objects
{
	/// <summary>
	/// Generic list that can be expandable and handled as static.
	/// </summary>
	public class xList<T>
	{
		private int m_objectCount;
		private List<T> m_list;
		public int Capacity { get { return m_list.Count; } }
		public int Count { get { return m_objectCount; } }
		public xList()
		{
			m_list = new List<T>();
			m_objectCount = 0;
		}
		public xList(int Capacity)
		{
			m_list = new List<T>(Capacity);
			for (int i = 0; i < m_list.Capacity; i++)
				m_list.Add(default(T));
			m_objectCount = 0;
		}
		public xList(xList<T> value)
		{
			m_list = new List<T>(value.m_list);
			m_objectCount = value.m_objectCount;
		}
		public T this[int index]
		{
			get { return m_list[index]; }
			set {
				if (index >= m_list.Count)
				{
					// Expand the list
					for (int i = m_list.Count; i <= index; i++)
						m_list.Add(default(T));
				}
				// Keep control about real objects at list
				if (EqualityComparer<T>.Default.Equals(value, default(T)))
				{
					if (!EqualityComparer<T>.Default.Equals(m_list[index], default(T)))
						m_objectCount--;
				}
				else
				{
					if (EqualityComparer<T>.Default.Equals(m_list[index], default(T)))
						m_objectCount++;
				}
				// Set new value
				m_list[index] = value;
			}
		}
		public void Add(T value)
		{
			this[Capacity] = value;
		}
		public void RemoveAt(int index)
		{
			m_list.RemoveAt(index);
		}
		public void Clear()
		{
			m_list.Clear();
		}
		public void Resize(int newCapacity)
		{
			if(newCapacity < Capacity)
			{
				for (int i = Capacity-1; i >= newCapacity; i--)
					m_list.RemoveAt(i);
			}
			else if(newCapacity > Capacity)
			{
				for (int i = Capacity; i < newCapacity; i++)
					m_list.Add(default(T));
			}
		}
		public bool Exists(Predicate<T> match)
		{
			return m_list.Exists(match);
		}
		public T Find(Predicate<T> match)
		{
			return m_list.Find(match);
		}
		/// <summary>
		/// Find the first item match from the starting index.
		/// </summary>
		public int FindIndex(Predicate<T> match, int startIndex = 0)
		{
			return FindIndex(match, startIndex, this.Capacity - 1);
		}
		/// <summary>
		/// Find the first item match limited by the indices specified.
		/// </summary>
		public int FindIndex(Predicate<T> match, int startIndex, int endIndex)
		{
			for (int i = startIndex; i <= endIndex; i++)
				if (match(m_list[i]))
					return i;
			return -1;
		}
	}
}
