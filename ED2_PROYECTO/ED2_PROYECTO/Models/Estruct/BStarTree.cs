using ED2_PROYECTO.Models.Estruct.Disk;
using ED2_PROYECTO.Models.Estruct.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
	public class BStarTree<T> where T : IComparable, IFixedSizeText 
	{
		internal BStarTreeNode<T> root;
		internal int maxNodeSize;
		internal int rootSize;
		public int PosicionDisponible { get; set; }
		public string RutaArbol { get; set; }
		public static List<T> st = new List<T>();

		//ya esta---------------------------------------------
		public BStarTree(int grado, string ruta, string archivo)
		{
			this.root = null;
			this.maxNodeSize = grado - 1;
			this.rootSize = (int)(2 * (Math.Floor((double)(2 * grado - 1) / 3)) + 1);
			RutaArbol = ruta + archivo;
			BWriter<T>.EvaluarRuta(ruta);
			BWriter<T>.EscribirRaiz(RutaArbol, int.MinValue);
			BWriter<T>.EscribirPosicionDisponible(RutaArbol, 1);
			PosicionDisponible = 1;
		}

		public virtual bool insertIntoNode(BStarTreeNode<T> node, T element)
		{
			//T[] elementList = node.keys;
			List<T> elementList = new List<T>();
			int r = 0;
			while (node.keys[r] != null)
			{
				elementList.Add(node.keys[r]);
				r++;
			}
			int numElements;
			T[] minimalList;

			if (node == root)
			{
				minimalList = new T[elementList.Count() + 1];
				numElements = rootSize;
			}
			else
			{
				minimalList = new T[elementList.Count() + 1];
				numElements = maxNodeSize;
			}

			for (int i = 0; i < elementList.Count(); i++)
			{
				minimalList[i] = elementList[i];
			}

			minimalList[minimalList.Length - 1] = element;
			Array.Sort(minimalList);
			node.keys = new T[numElements];
			for (int x = 0; x < numElements; x++)
			{
				if (x < minimalList.Length)
				{
					node.keys[x] = minimalList[x];
				}
				else
				{
					node.keys[x] = default(T);
				}
			}

			return true;
		}

		public virtual BStarTreeNode<T> findParent(BStarTreeNode<T> searchNode)
		{
			return findParent(searchNode, root);
		}

		public virtual BStarTreeNode<T> findParent(BStarTreeNode<T> searchNode, BStarTreeNode<T> @base)
		{
			if (searchNode == null || @base == null || searchNode == @base)
			{
				return null;
			}

			bool isChild = false;

			for (int x = 0; x < @base.children.Length; x++)
			{
				if (@base.children[x] == searchNode)
				{
					isChild = true;
				}
			}

			if (!isChild)
			{
				for (int x = 0; x < @base.children.Length; x++)
				{
					BStarTreeNode<T> possibleParent = findParent(searchNode, @base.children[x]);
					if (possibleParent != null)
					{
						return possibleParent;
					}
				}
				return null;
			}
			else
			{
				return @base;
			}
		}

		public virtual void redistribute(BStarTreeNode<T> node)
		{
			int numNodes = 0; //convertNodeToIntegers(node).length;
			int minimumSize = (int)(Math.Floor((double)(maxNodeSize * 2 - 1) / 3));
			bool hasMoved;
			do
			{
				//System.out.println("Iteration");
				hasMoved = false;
				for (int x = 0; x < node.children.Length; x++)
				{
					if (maxNodeSize - spacesLeftInNode(node.children[x]) < minimumSize)
					{
						T newRoot = convertNodeToIntegers(node.children[x - 1])[convertNodeToIntegers(node.children[x]).Length - 1];
						T oldRoot = convertNodeToIntegers(node)[x - 1];
						deleteFromNode(node.children[x - 1], convertNodeToIntegers(node.children[x - 1])[convertNodeToIntegers(node.children[x - 1]).Length - 1]);
						deleteFromNode(node, oldRoot);
						insertIntoNode(node, newRoot);
						insertIntoNode(node.children[x], oldRoot);
					}
				}
			} while (hasMoved == true);
		}

		//public virtual bool deleteFromNode(BStarTreeNode<T> node, T elementt)
		//{
		//	return (deleteFromNode(node, (elementt)));
		//}

		public virtual bool deleteFromNode(BStarTreeNode<T> node, T element)
		{
			T[] elementList = convertNodeToIntegers(node);
			int numElements;
			T[] minimalList;

			if (node == root)
			{
				minimalList = new T[rootSize - spacesLeftInNode(node) - 1];
				numElements = rootSize;
			}
			else
			{
				minimalList = new T[maxNodeSize - spacesLeftInNode(node) - 1];
				numElements = maxNodeSize;
			}
			int i = 0;
			int j = 0;
			while (j < elementList.Length && i < minimalList.Length)
			{
				if (!elementList[j].Equals(element))
				{
					minimalList[i] = elementList[j];
					i++;
				}
				j++;
			}

			Array.Sort(minimalList);

			return true;
		}

		//public virtual bool insertElement(T element)
		//{
		//	return insertElement((element));
		//}

		public virtual BStarTreeNode<T> findToInsert(T element)
		{
			int rootReferences = 0;
			if (root.children != null)
			{
				for (int x = 0; x < root.children.Length; x++)
				{
					if (root.children[x] != null)
					{
						rootReferences++;
					}
				}
			}

			if (spacesLeftInNode(root) > 0 && rootReferences == 0)
			{
				return root;
			}
			else
			{
				return findToInsert(element, root);
			}
		}

		public virtual BStarTreeNode<T> findToInsert(T element, BStarTreeNode<T> node)
		{
			if (node != null)
			{
				int i = 1;
				for (; i <= convertNodeToIntegers(node).Length && convertNodeToIntegers(node)[i - 1].CompareTo(element) < 0; i++)
					if ((i > convertNodeToIntegers(node).Length || convertNodeToIntegers(node)[i - 1].CompareTo(element) > 0) && node.children != null && node.children.Length >= i)
					{
						if (node.children != null && node.children[i - 1] != null)
						{
							return findToInsert(element, node.children[i - 1]);
						}
						else
						{
							return node;
						}
					}
					else
					{
						return node;
					}
			}
			else
			{
				return null;
			}
			return node; //ver
		}

		//inicia inserción
		public virtual bool insertElement(T element)
		{
			root.posicion = BReader<T>.LeerRaiz(RutaArbol);
			if (root == null)
			{
				root = new BStarTreeNode<T>(element, rootSize);
				root.initializeChildren();
				BWriter<T>.EscribirNodo(RutaArbol, root, PosicionDisponible);
				return true;
			}
			else
			{

				BStarTreeNode<T> nodeToInsertInto = findToInsert(element);
				if (spacesLeftInNode(nodeToInsertInto) == 0)
				{
					if (findToInsert(element) == root)
					{
						return splitRootInsert(element);
					}
					else
					{
						if (siblingsHaveSpace(nodeToInsertInto))
						{
							return reorderElementInsert(element, nodeToInsertInto);
						}
						else
						{
							return splitNodeInsert(findToInsert(element), element);
						}
					}
				}
				else
				{
					return insertIntoNode(findToInsert(element), element);
				}
			}
		}

		public virtual bool reorderElementInsert(T element, BStarTreeNode<T> node)
		{
			BStarTreeNode<T> parent = findParent(node);
			int x = 0;
			for (; x < parent.children.Length && parent.children[x] != node; x++)
			{
				;
			}
			if (x > 0 && spacesLeftInNode(parent.children[x - 1]) > 0)
			{
				T shiftKey = convertNodeToIntegers(node)[0];
				T temp = convertNodeToIntegers(parent)[x - 1];
				deleteFromNode(parent, temp);
				deleteFromNode(node, convertNodeToIntegers(node)[0]);
				insertIntoNode(parent, shiftKey);
				insertIntoNode(node, element);
				insertIntoNode(parent.children[x - 1], temp);
			}
			else if (x < maxNodeSize - 1 && spacesLeftInNode(parent.children[x + 1]) > 0)
			{

			}
			return true;
		}

		public virtual bool siblingsHaveSpace(BStarTreeNode<T> node)
		{
			int x = 0;
			BStarTreeNode<T> parent = findParent(node);
			for (; x < parent.children.Length && parent.children[x] != node; x++)
			{
				;
			}


			if (x == parent.children.Length)
			{
				return false;
			}

			if (x == 0)
			{
				if (parent.children[1] != null)
				{
					return (spacesLeftInNode(parent.children[1]) > 0);
				}
			}
			else if (x == parent.children.Length - 1 || parent.children[x + 1] == null)
			{

				if (parent.children[x - 1] != null)
				{
					return (spacesLeftInNode(parent.children[x - 1]) > 0);
				}
			}
			else
			{
				if (parent.children[x - 1] != null && parent.children[x + 1] != null)
				{
					return (spacesLeftInNode(parent.children[x - 1]) > 0 || spacesLeftInNode(parent.children[x + 1]) > 0);
				}
			}
			return false;
		}

		//public virtual bool deleteElement(T element)
		//{
		//	return deleteElement(new int?(element));
		//}

		public virtual bool deleteElement(T element)
		{
			BStarTreeNode<T> node = BStarSearch(element);

			if (node == null)
			{
				return false;
			}

			int minimumSize = (int)(Math.Floor((double)(maxNodeSize * 2 - 1) / 3));

			bool hasChildren = false;
			if (node.children != null)
			{
				for (int x = 0; x < node.children.Length; x++)
				{
					if (node.children[x] != null)
					{
						hasChildren = true;
					}
				}
			}

			if (node != null && node != root)
			{
				if (hasChildren == false && convertNodeToIntegers(node).Length > minimumSize)
				{
					deleteFromNode(node, element);
				}
				else
				{
					if (siblingsHaveSpare(node))
					{
						return reorderDelete(element, node);
					}
					else
					{
						return mergeDelete(element, node);
					}
				}
			}
			return false;
		}

		public virtual bool reorderDelete(T element, BStarTreeNode<T> node)
		{
			int minimumSize = (int)(Math.Floor((double)(maxNodeSize * 2 - 1) / 3));
			BStarTreeNode<T> parent = findParent(node);
			int x = 0;
			for (; x < parent.children.Length && parent.children[x] != node; x++)
			{
				;
			}

			if (x > 0 && countSpacesFilled(parent.children[x - 1]) > minimumSize)
			{
				T shiftKey = convertNodeToIntegers(node)[0];
				T temp = convertNodeToIntegers(parent)[x - 1];

				deleteFromNode(parent, temp);

				deleteFromNode(node, convertNodeToIntegers(node)[0]);

				insertIntoNode(parent, shiftKey);
				insertIntoNode(node, element);
				insertIntoNode(parent.children[x - 1], temp);
			}
			else if (x < maxNodeSize - 1 && countSpacesFilled(parent.children[x + 1]) > 0)
			{
				T shiftKey = convertNodeToIntegers(parent.children[x + 1])[0];
				T temp = convertNodeToIntegers(parent)[x];

				deleteFromNode(parent, temp);
				deleteFromNode(parent.children[x + 1], shiftKey);
				deleteFromNode(node, convertNodeToIntegers(node)[x]);

				insertIntoNode(parent, shiftKey);
				insertIntoNode(node, temp);
			}
			return true;
		}
		public virtual bool mergeDelete(T element, BStarTreeNode<T> node)
		{


			BStarTreeNode<T> parent = findParent(node);

			int numChild = -1;

			for (int x = 0; x < parent.children.Length; x++)
			{
				if (parent.children[x] == node)
				{
					numChild = x;
				}
			}

			if (numChild == -1)
			{
				return false;
			}

			deleteFromNode(node, element);
			T[] elementList = new T[maxNodeSize];
			T[] nodeContents = convertNodeToIntegers(node);

			for (int x = 0; x < nodeContents.Length; x++)
			{
				elementList[x] = nodeContents[x];
			}

			int counter = 0;
			BStarTreeNode<T> sibling = null;
			BStarTreeNode<T> newNode = new BStarTreeNode<T>(maxNodeSize);
			int? newParent;

			if (numChild == 0)
			{
				sibling = parent.children[numChild + 1];
			}
			else if (numChild == maxNodeSize - 1)
			{
				sibling = parent.children[numChild - 1];
			}
			else if (numChild > 0 && spacesLeftInNode(parent.children[numChild - 1]) >= convertNodeToIntegers(node).Length)
			{
				sibling = parent.children[numChild - 1];
			}
			else if (numChild < maxNodeSize - 1 && spacesLeftInNode(parent.children[numChild + 1]) >= convertNodeToIntegers(node).Length)
			{
				sibling = parent.children[numChild - 1];
			}

			for (int x = nodeContents.Length; x < maxNodeSize; x++)
			{
				elementList[x] = convertNodeToIntegers(sibling)[counter];
				counter++;

			}
			Array.Sort(elementList);
			for (int x = 0; x < elementList.Length; x++)
			{
				insertIntoNode(newNode, elementList[x]);
			}

			if (sibling == parent.children[numChild + 1])
			{
				parent.children[numChild] = newNode;
				T temp = convertNodeToIntegers(parent)[numChild];
				deleteFromNode(parent, convertNodeToIntegers(parent)[numChild]);

				insertIntoNode(newNode, temp);

			}
			else
			{
				parent.children[numChild - 1] = newNode;
				T temp = convertNodeToIntegers(parent)[numChild];
				deleteFromNode(parent, convertNodeToIntegers(parent)[numChild - 1]);
				insertIntoNode(newNode, temp);
			}
			return true;
		}

		public virtual BStarTreeNode<T> BStarSearch(T key)
		{
			return BStarSearch(key, root);
		}

		public virtual bool siblingsHaveSpare(BStarTreeNode<T> node)
		{
			int x = 0;
			int minSize = (int)(Math.Floor((double)(maxNodeSize * 2 - 1) / 3));
			BStarTreeNode<T> parent = findParent(node);
			for (; x < parent.children.Length && parent.children[x] != node; x++)
			{
				;
			}


			if (x == parent.children.Length)
			{
				return false;
			}

			if (x == 0)
			{
				if (parent.children[1] != null)
				{
					return (countSpacesFilled(parent.children[1]) > minSize);
				}
			}
			else if (x == parent.children.Length - 1 || parent.children[x + 1] == null)
			{
				if (parent.children[x - 1] != null)
				{
					return (countSpacesFilled(parent.children[x - 1]) > minSize);
				}
			}
			else
			{
				if (parent.children[x - 1] != null && parent.children[x + 1] != null)
				{
					return (countSpacesFilled(parent.children[x - 1]) > minSize || countSpacesFilled(parent.children[x + 1]) > minSize);
				}
			}
			return false;
		}

		public virtual BStarTreeNode<T> BStarSearch(T key, BStarTreeNode<T> node)
		{
			if (node != null)
			{
				int i = 1;
				for (; i <= convertNodeToIntegers(node).Length && convertNodeToIntegers(node)[i - 1].CompareTo(key) < 0; i++)
				{
					;
				}
				if (i > convertNodeToIntegers(node).Length || convertNodeToIntegers(node)[i - 1].CompareTo(key) > 0)
				{
					return BStarSearch(key, node.children[i - 1]);
				}
				else
				{
					return node;
				}
			}
			else
			{
				return null;
			}
		}

		public virtual string search(T element)
		{
			return search((element));
		}

		//public virtual string search(T element)
		//{
		//	return search(element, root);
		//}
		public virtual T[] search(T element, BStarTreeNode<T> node)
		{
			if (node != null)
			{

				int i = 1;
				for (; i <= convertNodeToIntegers(node).Length && element.CompareTo(convertNodeToIntegers(node)[i - 1]) > 0; i++) ;

				if (i > convertNodeToIntegers(node).Length || convertNodeToIntegers(node)[i - 1].CompareTo(element) > 0)
				{
					if (node.children != null && node.children.Length > 0 && i < node.children.Length - 1)
					{
						return search(element, node.children[i - 1]);
					}
					else
					{
						return node.keys;
					}
				}
				else
				{
					return node.keys;
				}
			}
			else
			{
				return node.keys;
			}
		}

		public virtual int height()
		{
			return height(root);
		}

		public virtual int height(BStarTreeNode<T> node)
		{
			if (node == null)
			{
				return 0;
			}

			if (node.children == null)
			{
				return 1;
			}

			int[] heights = new int[node.children.Length];

			for (int x = 0; x < node.children.Length; x++)
			{
				heights[x] = height(node.children[x]);
			}
			int maxHeight = -1;
			for (int x = 0; x < node.children.Length; x++)
			{
				if (heights[x] > maxHeight)
				{
					maxHeight = heights[x];
				}
			}
			return 1 + maxHeight;
		}

		public virtual int fullness()
		{
			if (root == null)
			{
				return 0;
			}

			return (int)((double)(countSpacesFilled()) / countSpacesInTree() * 100);
		}

		public virtual string breadthFirst()
		{
			string treeString = "";

			Queue<T> queue = new Queue<T>();

			Node<T> nodePointer = new Node<T>(root);

			if (nodePointer != null)
			{
				queue.enqueue(nodePointer);
				while (!queue.Empty)
				{
					nodePointer = queue.dequeue();
					Console.WriteLine(nodePointer.elem.keys);
					treeString += nodePointer.elem.keys;
					if (nodePointer.elem.children != null)
					{
						for (int x = 0; x < nodePointer.elem.children.Length; x++)
						{
							if (nodePointer.elem.children[x] != null)
							{
								queue.enqueue(nodePointer.elem.children[x]);
							}
						}
					}
				}
			}
			return treeString;
		}

		public virtual string TreeString
		{
			get
			{
				return getTreeString(root);
			}
		}

		public virtual string getTreeString(BStarTreeNode<T> node)
		{
			if (node == null)
			{
				return "";
			}

			string nodeString = "";

			for (int x = 0; x < convertNodeToIntegers(node).Length; x++)
			{
				nodeString += getTreeString(node.children[x]);
				nodeString += convertNodeToIntegers(node)[x];
			}
			nodeString += TreeString;
			return nodeString;
		}

		public virtual bool splitRootInsert(T element)
		{
			if (root == null)
			{
				return false;
			}

			T[] rootArray = new T[convertNodeToIntegers(root).Length + 1];
			T[] currentElements = convertNodeToIntegers(root);

			for (int x = 0; x < currentElements.Length; x++)
			{
				rootArray[x] = currentElements[x];
			}
			rootArray[rootArray.Length - 1] = element;
			Array.Sort(rootArray);

			int numLeft = (int)(Math.Ceiling((double)(currentElements.Length) / 2));

			BStarTreeNode<T> leftNode = new BStarTreeNode<T>(maxNodeSize);
			BStarTreeNode<T> rightNode = new BStarTreeNode<T>(maxNodeSize);

			for (int x = 0; x < numLeft; x++)
			{
				insertIntoNode(leftNode, rootArray[x]);
			}

			//string nullString = "";
			for (int x = 0; x < rootSize; x++)
			{
				//nullString += "[]";
				root.keys[x] = default(T);
			}

			//root.keys = null;

			insertIntoNode(root, rootArray[numLeft]);

			for (int x = numLeft + 1; x < rootArray.Length; x++)
			{
				insertIntoNode(rightNode, rootArray[x]);
			}
			root.children[0] = leftNode;
			root.children[1] = rightNode;
			return true;
		}
		public virtual bool splitNodeInsert(BStarTreeNode<T> node, T element)
		{
			BStarTreeNode<T> parent = findParent(node);

			int numChild = -1;

			for (int x = 0; x < parent.children.Length; x++)
			{
				if (parent.children[x] == node)
				{
					numChild = x;
				}
			}

			if (numChild == -1)
			{
				return false;
			}
			//System.out.println("Parent is " + convertNodeToIntegers(parent)[numChild-1]);
			T[] elementList = new T[maxNodeSize + 1];
			T[] nodeContents = convertNodeToIntegers(node);

			for (int x = 0; x < maxNodeSize; x++)
			{
				elementList[x] = nodeContents[x];
			}
			elementList[elementList.Length - 1] = element;
			Array.Sort(elementList);

			BStarTreeNode<T> newNodeLeft = new BStarTreeNode<T>(maxNodeSize);
			BStarTreeNode<T> newNodeRight = new BStarTreeNode<T>(maxNodeSize);
			int splitter = (int)(Math.Floor((2 * (maxNodeSize + 1) - 1) / (double)(3)));
			insertIntoNode(parent, elementList[splitter]);

			for (int x = 0; x < splitter; x++)
			{
				insertIntoNode(newNodeLeft, elementList[x]);
			}
			for (int x = splitter + 1; x < elementList.Length; x++)
			{
				insertIntoNode(newNodeRight, elementList[x]);
			}
			//System.out.println("Num child " + numChild);

			parent.children[numChild] = newNodeLeft;
			if (numChild + 1 < parent.children.Length)
			{
				parent.children[numChild + 1] = newNodeRight;
			}
			//redistribute(parent);
			return true;
		}

		public virtual int spacesLeftInNode(BStarTreeNode<T> node)
		{
			if (node != null)
			{
				int contador = 0;

				for (int i = 0; i < node.keys.Length; i++)
				{
					if (node.keys[i] != null)
					{
						contador++;
					}
				}
				if (contador == node.keys.Length)
				{
					return 0;
				}
				else
				{
					return node.keys.Length - 1;
				}

			}
			else
			{
				return 0;
			}
		}

		public virtual int countSpacesInTree()
		{
			return countSpacesInTree(root);
		}
		public virtual int countSpacesInTree(BStarTreeNode<T> node)
		{
			int currentNodeSpaces = node.keys.Length - 1;
			int childNodeSpaces = 0;
			if (node.children != null && node.children.Length > 0)
			{
				for (int i = 0; i < maxNodeSize; i++)
				{
					if (node.children[i] != null)
					{
						childNodeSpaces += (int)(node.children[i].keys.Length - 1) / 2;
					}
				}
			}
			return currentNodeSpaces + childNodeSpaces;
		}

		public virtual int countSpacesFilled()
		{
			return countSpacesFilled(root);
		}
		public virtual int countSpacesFilled(BStarTreeNode<T> node)
		{
			if (node == null)
			{
				return 0;
			}
			int spacesFilled = 0;
			int childrenSpacesFilled = 0;
			if (node == root)
			{
				spacesFilled = rootSize - spacesLeftInNode(node);
			}
			else
			{
				spacesFilled = maxNodeSize - spacesLeftInNode(node);
			}

			if (node.children != null && node.children.Length > 0)
			{
				for (int i = 0; i < maxNodeSize; i++)
				{
					if (node.children[i] != null)
					{
						childrenSpacesFilled += maxNodeSize - spacesLeftInNode(node.children[i]);
					}
				}
			}
			return spacesFilled + childrenSpacesFilled;
		}

		//editar
		public virtual T[] convertNodeToIntegers(BStarTreeNode<T> node)
		{
			int i = 0;
			if (node == null)
			{
				return null;
			}
			T[] splitNum = node.keys;
			return splitNum;
			//splitNum = splitNum.Replace("[", "");
			//splitNum = splitNum.Replace("]", " ");

			//var st = new StreamTokenizer(splitNum);

			//T[] numbers = new T[st.Count()];
			//while (st.Count > 1)
			//{
			//	numbers[i] = splitNum[i];
			//	i++;
			//}
			//T[] valueArray = null;
			//int notNullCounter = 0;

			//for (int x = 0; x < numbers.Length; x++)
			//{
			//	if (!string.ReferenceEquals(numbers[x], " "))
			//	{
			//		notNullCounter++;
			//	}
			//}

			//valueArray = new T[notNullCounter];
			//for (int x = 0; x < valueArray.Length; x++)
			//{

			////	valueArray[x] = numbers[x];

			//}

		}

	}
}
