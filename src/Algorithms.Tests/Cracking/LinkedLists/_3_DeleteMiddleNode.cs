using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Tests.DataStructures.Node;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _3_DeleteMiddleNode
    {
        /// <summary>
        /// Delete middle node, can assume that middle is not start nor end
        /// </summary>
        public _3_DeleteMiddleNode()
        {
            
        }

        [Theory]
        [InlineData(new int[]{1, 2, 3}, 2, new int[] { 1, 3 })]
        [InlineData(new int[]{1, 2, 3}, 3, new int[] { 1, 2, 3 })]
        [InlineData(new int[]{1, 2, 3, 4}, 3, new int[] { 1, 2, 4 })]
        public void should_remove_middle_node(int[] array, int kthNode, int[] expected)
        {
            // Arrange
            var head = Node<int>.Create(array);
            var node = head;
            while(kthNode-- > 1)
            {
                node = node.Next;
            }

            // Act
            RemoveNode(node);

            // Assert
            foreach(var value in expected)
            {
                Assert.Equal(value, head.Value);
                head = head.Next;
            }
        }

        private void RemoveNode(Node<int> node)
        {
            if (node.Next != null)
            {
                node.Value = node.Next.Value;
                node.Next = node.Next.Next;
            }
        }
    }
}
