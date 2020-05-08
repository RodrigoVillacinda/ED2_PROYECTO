using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
	public class BStarTreeNode<T> : IEquatable<BStarTreeNode<T>>
	{
		public T[] keys;
		public BStarTreeNode<T>[] children = null;
		public int maxNode;
		public T Element { get; set; }

		public BStarTreeNode(int m)
		{
			keys = new T[m];
			for (int x = 0; x < m; x++)
			{
				keys[x] = keys[x];
			}
			maxNode = m;
			initializeChildren();
		}

		public BStarTreeNode(T element, int m)
		{
			keys = new T[m];
			for (int x = 0; x < m; x++)
			{
				keys[x] = keys[x];
			}
			maxNode = m;
			initializeChildren();

			Element = element;
			maxNode = m;
			keys[0] = element;
			for (int x = 1; x < m; x++)
			{
				keys[x] = keys[x];
			}
			initializeChildren();
		}

		public bool Equals(BStarTreeNode<T> other)
		{
			return this.keys.Equals(other.keys);
		}

		public virtual void initializeChildren()
		{
			if (children == null)
			{
				children = new BStarTreeNode<T>[maxNode];
			}

			for (int x = 0; x < maxNode; x++)
			{
				children[x] = null;
			}
		}
	}

}
