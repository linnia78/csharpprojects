
using Algorithms.Tests.DataStructures.Node;
using System.Collections.Generic;
using Xunit;

namespace Algorithms.Tests.Fundamentals
{
    public class ReverseStackTest
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 2, 1 })]
        [InlineData(new int[] { 1 }, new int[] { 1 })]
        public void should_reverse_iteratively(int[] array, int[] expected)
        {
            // Arrange
            var node = Node<int>.Create(array);

            // Act
            node = ReverseIteratively(node);

            // Assert
            foreach(var value in expected)
            {
                Assert.Equal(value, node.Value);
                node = node.Next;
            }
        }

        private Node<int> ReverseIteratively(Node<int> node)
        {
            var previous = (Node<int>)null;
            var current = node;
            while(current != null)
            {
                var next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            return previous;
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 2, 1 })]
        [InlineData(new int[] { 1 }, new int[] { 1 })]
        public void should_reverse_recursively(int[] array, int[] expected)
        {
            // Arrange
            var node = Node<int>.Create(array);

            // Act
            node = ReverseRecursively(node);

            // Assert
            foreach(var value in expected)
            {
                Assert.Equal(value, node.Value);
                node = node.Next;
            }
        }

        private Node<int> ReverseRecursively(Node<int> node)
        {
            if (node == null || node.Next == null)
                return node;

            var newHead = ReverseRecursively(node.Next);
            node.Next.Next = node;
            node.Next = null;
            return newHead;
        }
    }
}
