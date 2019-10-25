using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace xBot.Game.Objects
{
	/// <summary>
	/// Creates a dictionary that can be iterable.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SRObjectDictionary<T>
	{
		#region Propierties
		private Dictionary<T, SRObject> Objects;
		private List<T> Enumerator;
		/// <summary>
		/// <see cref="SRObject"/> counter.
		/// </summary>
		public int Count { get { return Objects.Count; } }
		public SRObject this[T UniqueID]
		{
			get
			{
				SRObject obj = null;
				Objects.TryGetValue(UniqueID,out obj);
				return obj;
			}
			set
			{
				Objects[UniqueID] = value;
				Enumerator.Add(UniqueID);
			}
		}
		#endregion
		#region
		/// <summary>
		/// Creates a <see cref="Dictionary{TKey, TValue}"/> with <see cref="SRObject"/> as <see cref="{TValue}"/>.
		/// </summary>
		public SRObjectDictionary()
		{
			Objects = new Dictionary<T, SRObject>();
			Enumerator = new List<T>();
		}
		public SRObjectDictionary(SRObjectDictionary<T> dict)
		{
			Objects = new Dictionary<T, SRObject>(dict.Objects);
			Enumerator = new List<T>(dict.Enumerator);
		}
		public void SetKey(T oldUniqueID, T newUniqueID)
		{
			SRObject reference = Objects[oldUniqueID];
			Objects.Remove(oldUniqueID);
			Objects[newUniqueID] = reference;
		}
		public void RemoveKey(T UniqueID)
		{
			Objects.Remove(UniqueID);
			Enumerator.Remove(UniqueID);
		}
		public bool ContainsKey(T UniqueID)
		{
			return Objects.ContainsKey(UniqueID);
		}
		public void Clear()
		{
			Objects.Clear();
			Enumerator.Clear();
		}
		public SRObject Find(Predicate<SRObject> match)
		{
			for(int i=0;i<Enumerator.Count;i++){
				SRObject obj = Objects[Enumerator[i]];
				if(match(obj))
					return obj;
			}
			return null;
		}
		public SRObjectCollection FindAll(Predicate<SRObject> match)
		{
			SRObjectCollection result = new SRObjectCollection();
			for(int i=0;i<Enumerator.Count;i++){
				SRObject obj = Objects[Enumerator[i]];
				if(match(obj))
					result.Add(obj);
			}
			return result;
		}
		public SRObject ElementAt(int index)
		{
			if(index < Enumerator.Count)
				return this[Enumerator[index]];
			return null;
		}
		public SRObject[] ToArray()
		{
			SRObject[] array = new SRObject[Objects.Count];
			int i = 0;
			foreach (SRObject obj in Objects.Values)
				array[i++] = obj;
			return array;
		}
		public TreeNode[] ToNodes()
		{
			TreeNode[] nodes = new TreeNode[Objects.Count];
			int i = 0;
			foreach (SRObject value in Objects.Values)
				nodes[i++] = value.ToNode();
			return nodes;
		}
		#endregion
	}
}