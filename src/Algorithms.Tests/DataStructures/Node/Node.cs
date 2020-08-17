using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Tests.DataStructures.Node
{
    public class Node<T>
    {
        public Node<T> Next;
        public T Value;
        public Node()
        {

        }
        public Node(T value)
        {
            Value = value;
        }

        public void Add(T value)
        {
            Next = new Node<T>(value);
        }

        public static Node<T> Create(T[] array)
        {
            if (array.Length < 1)
            {
                return null;
            }

            var head = new Node<T>(array[0]);
            var current = head;
            foreach(var a in array.Skip(1))
            {
                current.Next = new Node<T>(a);
                current = current.Next;
            }
            return head;
        }
    }

}
