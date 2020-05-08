using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ED2_PROYECTO.Models.Estruct
{
    public class Node<T> : IEquatable<Node<T>> 
    {
        public Node<T> next = null;
        public BStarTreeNode<T> elem;

        public Node(BStarTreeNode<T> node)
        {
            elem = node;
        }

        public bool Equals(Node<T> other)
        {
            return this.next.Equals(other.next);
        }
    }
}
