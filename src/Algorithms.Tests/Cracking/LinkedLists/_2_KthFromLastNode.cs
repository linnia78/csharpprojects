using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Tests.DataStructures.Node;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _2_KthFromLastNode
    {
        /// <summary>
        /// Get kth node starting from end of linked list
        /// </summary>
        public _2_KthFromLastNode()
        {
            
        }

        [Theory]
        [InlineData(new int[] {1, 2, 3, 4}, 2, 3)]
        [InlineData(new int[] {1, 2, 3, 4}, 1, 4)]
        [InlineData(new int[] {1, 2, 3, 4}, 4, 1)]
        [InlineData(new int[] {1, 2, 3, 4}, 5, null)]
        public void should_return_kth_recursively(int[] array, int kth, int? expected)
        {
            // Arrange
            var node = Node<int>.Create(array);
            
            // Act
            var result = GetKthRecursively(node, ref kth);
            
            // Assert
            Assert.Equal(expected, result?.Value);
        }

        public Node<int> GetKthRecursively(Node<int> node, ref int kth)
        {
            if (node == null)
            {
                return null;
            }
            var result = GetKthRecursively(node.Next, ref kth);
            kth--;
            if (kth == 0)
            {
                result = node;
            }
            return result;
        }

        [Theory]
        [InlineData(new int[] {1, 2, 3, 4}, 2, 3)]
        [InlineData(new int[] {1, 2, 3, 4}, 1, 4)]
        [InlineData(new int[] {1, 2, 3, 4}, 4, 1)]
        [InlineData(new int[] {1, 2, 3, 4}, 5, null)]
        public void should_return_kth_iteratively(int[] array, int kth, int? expected)
        {
            // Arrange
            var node = Node<int>.Create(array);
            
            // Act
            var result = GetKthIteratively(node, kth);
            
            // Assert
            Assert.Equal(expected, result?.Value);
        }
        
        private Node<int> GetKthIteratively(Node<int> node, int kth)
        {
            // Move fast ahead k times
            // Move current and fast until fast reaches the end
            // Return current
            var fast = node;
            while(kth-- > 1)
            {
                if (fast.Next == null)
                {
                    return null;
                }
                fast = fast.Next;
            }

            while(fast.Next != null)
            {
                node = node.Next;
                fast = fast.Next;
            }

            return node;
        }
    }
}
