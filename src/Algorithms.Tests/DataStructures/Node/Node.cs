using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Tests.DataStructures.Node
{
    public class Node<T>
    {
        public Node<T> Next;
        public T Value;
        public Node(T value)
        {
            Value = value;
        }

        public void Add(T value)
        {
            Next = new Node<T>(value);
        }

    }
}
