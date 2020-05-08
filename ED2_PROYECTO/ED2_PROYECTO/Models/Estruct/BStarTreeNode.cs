using ED2_PROYECTO.Models.Estruct.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
	//
	public class BStarTreeNode<T> : IEquatable<BStarTreeNode<T>>, IFixedSizeText where T : IFixedSizeText
	{
		public T[] keys;
		public BStarTreeNode<T>[] children = null;
		public int maxNode;
		public T Element { get; set; }
		public int Grado { get; set; }
		public int posicion { get; set; }
		public int Padre { get; set; }
		public static int FixedSize { get; set; }
		public int FixedSizeText { get; set; }
		public string ToFixedSizeString()
		{
			string FixedString = "";

			FixedString += $"{posicion.ToString("00000000000;-0000000000")}|{Padre.ToString("00000000000;-0000000000")}|";

			for (int i = 0; i < Grado; i++)
			{
				FixedString += $"{children[i].ToString() == ("00000000000;-0000000000")}|";
			}

			for (int i = 0; i < Grado - 1; i++)
			{
				if (keys[i] != null)
				{
					FixedString += $"{keys[i].ToFixedSizeString()}|";
				}
				else
				{
					FixedString += $"{keys[0].ToNullFormat()}|";
				}
			}

			FixedString += "\n";

			return FixedString;
		}

		public string ToNullFormat()
		{
			return null;
		}
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
