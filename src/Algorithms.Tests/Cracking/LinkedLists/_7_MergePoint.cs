using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _7_MergePoint
    {
        /// <summary>
        /// Find merge point between two singly linked list
        /// Guaranteed that there's a merge point
        /// </summary>
        public _7_MergePoint()
        {
            
        }

        [Theory]
        //   1 - null
        //  /
        // 1 - null
        [InlineData(new int[] { 1 }, new int[] { 1 }, 1)]
        //       1 - 2 - 3 - 4 - 5 - null
        //          /
        // 1 - 2 - 3
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3 }, 2)]
        //               1 - 2 - 3 - null
        //                  /
        // 1 - 2 - 3 - 4 - 5
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4, 5 }, 2)]
        // 1 - 2 - 3 - 4 - 5 - null
        //        /
        //   1 - 2
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2 }, 3)]
        public void should_check_if_palindrome_iteratively(int[] a, int[] b, int mergeAtIndex)
        {
            // Algorithm
            // get size of both list
            // traverse longer list to node by x where x = difference in length
            // traverse both node until they match

            // Arrange
            var nodeA = Node<int>.Create(a);
            var nodeB = Node<int>.Create(b);
            var expected = MergeTestHelper(nodeA, nodeB, mergeAtIndex);

            // Act
            var mergePoint = FindMergeNode(nodeA, nodeB);

            // Assert
            Assert.Equal(expected, mergePoint);
        }

        private Node<int> MergeTestHelper(Node<int> target, Node<int> source, int mergeAtIndex)
        {
            // merge source into target at target's "mergeAtIndex"
            var pointer = target;
            while(mergeAtIndex-- > 1 && pointer != null)
            {
                pointer = pointer.Next;
            }

            var endOfSource = source;
            while(endOfSource != null && endOfSource.Next != null)
            {
                endOfSource = endOfSource.Next;
            }

            endOfSource.Next = pointer;
            return pointer;
        }

        private Node<int> FindMergeNode(Node<int> a, Node<int> b)
        {
            // Get size differenc
            var aSize = GetSize(a);
            var bSize = GetSize(b);
            var sizeDifference = Math.Abs(aSize - bSize);

            // Move pointer forward for longer list so starting position is the same
            var longNode = aSize > bSize ? a : b;
            var shortNode = aSize <= bSize ? a : b;
            while(sizeDifference-- > 0)
            {
                longNode = longNode.Next;
            }

            // Find merge node
            while(longNode != null && shortNode != null)
            {
                if (longNode == shortNode)
                {
                    return longNode;
                }

                longNode = longNode.Next;
                shortNode = shortNode.Next;
            }
            return null; // technically will never be reached because of constraint that a merge exists
        }

        private int GetSize(Node<int> node)
        {
            var count = 0;
            while(node != null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }

    }
}
