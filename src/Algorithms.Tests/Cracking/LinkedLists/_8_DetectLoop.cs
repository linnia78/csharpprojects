using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _8_DetectLoop
    {
        /// <summary>
        // Detect loop in singly linked list and return start of loop        
        /// </summary>
        public _8_DetectLoop()
        {
            
        }

        [Theory]
        [InlineData(new int[]{ 1, 2, 3, 4, 5, 6}, 6)]
        [InlineData(new int[]{ 1, 2, 3, 4, 5, 6}, 5)]
        [InlineData(new int[]{ 1, 2, 3, 4, 5, 6}, 4)]
        [InlineData(new int[]{ 1, 2, 3, 4, 5, 6}, 3)]
        [InlineData(new int[]{ 1, 2, 3, 4, 5, 6}, 2)]
        [InlineData(new int[]{ 1, 2, 3, 4, 5, 6}, 1)]
        public void should_check_if_palindrome_iteratively(int[] array, int expectedValue)
        {
            // Algorithm
            // Use two pointer
            // pointer S moves at 1 step at a time
            // pointer F moves at 2 step at a time
            // pointer S and F will eventually meet in loop after [Loop Size - k] steps because F is already k steps into the loop 
            //    where k is steps from starting node to point of loop start
            // after moving [LoopSize - k] S and F would meet at collision point which is k away from start of point
            //    because at point start of loop
            //      F is at some position in loop K = k % LoopSize
            //      S is at position k (start of loop)
            //      F would have moved 2k to get to K, k = K + M * LoopSize

            // TLDR; at starting point, F is k ahead of S, S is (LoopSize - K) ahead of F, collide after (LoopSize - K) which is k away from start

            // Arrange
            var node = Node<int>.Create(array);
            WrapLinkedListHelper(node, expectedValue);

            // Act
            var startNode = GetStartOfLoop(node);

            // Assert
            Assert.Equal(startNode.Value, expectedValue);
        }

        private void WrapLinkedListHelper(Node<int> node, int wrapAtPosition)
        {
            // move to end of linked list
            var endOfNode = node;
            while(endOfNode != null && endOfNode.Next != null)
            {
                endOfNode = endOfNode.Next;
            }

            // point end of linked list to position
            var wrapNode = node;
            while(wrapAtPosition-- > 1)
            {
                wrapNode = wrapNode.Next;
            }
            endOfNode.Next = wrapNode;
        }

        private Node<int> GetStartOfLoop(Node<int> node)
        {
            var slow = node;
            var fast = node;
            do
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            while(slow.Value != fast.Value);

            var start = node;
            while(start != fast)
            {
                start = start.Next;
                fast = fast.Next;
            }
            return start;
        }

    }
}
