using System;
using System.Collections.Generic;
namespace xBot.Game.Objects
{
	/// <summary>
	/// Generic dictionary that can be iterable and handled as array.
	/// </summary>
	public class xDictionary<TKey,TValue>
	{
		private Dictionary<TKey, TValue> m_dictionary;
		private List<TKey> m_enumerator;
		public int Count { get { return m_enumerator.Count; } }

		#region (Constructor)
		public xDictionary()
		{
			m_dictionary = new Dictionary<TKey, TValue>();
			m_enumerator = new List<TKey>();
    }
		public xDictionary(xDictionary<TKey,TValue> value)
		{
			m_dictionary = new Dictionary<TKey, TValue>(value.m_dictionary);
			m_enumerator = new List<TKey>(value.m_enumerator);
		}
		#endregion

		#region (Dictionary Methods)
		public TValue this[TKey ID]
		{
			get
			{
				TValue value = default(TValue);
				m_dictionary.TryGetValue(ID, out value);
				return value;
			}
			set
			{
				if (!m_dictionary.ContainsKey(ID))
					m_enumerator.Add(ID);
				m_dictionary[ID] = value;
			}
		}
		public void RemoveKey(TKey ID)
		{
			m_dictionary.Remove(ID);
			m_enumerator.Remove(ID);
		}
		public void SetKey(TKey ID, TKey NewID)
		{
			TValue reference = m_dictionary[ID];
			m_dictionary.Remove(ID);
			m_dictionary[NewID] = reference;
		}
		public void Clear()
		{
			m_dictionary.Clear();
			m_enumerator.Clear();
    }
		public bool ContainsKey(TKey ID)
		{
			return m_dictionary.ContainsKey(ID);
    }
		#endregion (Dictionary Methods)

		#region (List Methods)
		public TValue GetAt(int index)
		{
			return this[m_enumerator[index]];
    }
		public void RemoveAt(int index)
		{
			RemoveKey(m_enumerator[index]);
			m_enumerator.RemoveAt(index);
    }
		public void InsertAt(int index,TKey ID,TValue value)
		{
      m_enumerator.Insert(index, ID);
			m_dictionary[ID] = value;
		}
		public TValue Find(Predicate<TValue> match)
		{
			for (int i = 0; i < m_enumerator.Count; i++)
			{
				TValue reference = GetAt(i);
				if(match(reference))
					return reference;
			}
			return default(TValue);
		}
		public List<TValue> FindAll(Predicate<TValue> match)
		{
			List<TValue> references = new List<TValue>();
			for (int i = 0; i < m_enumerator.Count; i++)
			{
				TValue reference = GetAt(i);
				if (match(reference))
					references.Add(reference);
      }
			return references;
		}
		#endregion (List Methods)
	}
}
