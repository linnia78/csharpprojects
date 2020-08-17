using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Tests.DataStructures.Node;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _4_Partition
    {
        /// <summary>
        /// Partition linked list such that data < x is to the left and data >= x is to the right
        //  input 3, 5, 8, 5, 10, 2, 1 with x = 5
        //  output 3, 1, 2, 10, 5, 5, 8
        /// </summary>
        public _4_Partition()
        {
            // Algorithm
            // Brute force : bubble sort, continously swap by first finding data >= x then swap with first data where data < x.
            // If no data < x found when an data >= x was found then partition is complete

            // Better : set head and tail to root (original head)
            // use current head and tail as starting position and expand to left and right respectively. 
            // <- HEAD | TAIL -> traverse the linked list and grow head and tail respectively.
            // lastly set tail to null
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3)]
        [InlineData(new int[] { 1 }, 3)]
        [InlineData(new int[] { 1, 2, 4, 5 }, 3)]
        [InlineData(new int[] { 1, 2, 3, 3, 4, 5 }, 3)]
        [InlineData(new int[] { 5, 4, 3, 2, 1, 3, 2, 10, 3, 1 }, 3)]
        public void should_partition_by_x(int[] array, int x)
        {
            // Arrange
            var head = Node<int>.Create(array);

            // Act
            head = PartitionByX(head, x);

            // Assert
            var flag = true;
            while(head !=  null)
            {
                if (flag)
                {
                    flag = head.Value < x;
                }
                else
                {
                    Assert.True(head.Value >= x);
                }

                head = head.Next;
            }
        }

        private Node<int> PartitionByX(Node<int> root, int x)
        {
            var head = root;
            var tail = root;
            var node = root;

            while(node != null)
            {
                var next = node.Next;
                if (node.Value < x)
                {
                    // add to head, since adding to the left, need to set node to current
                    node.Next = head;
                    // then move head to the left
                    head = node;
                }
                else
                {
                    // add to tail, since adding to the right, set next = node
                    tail.Next = node;
                    // point to next as well since tail will be set to null when everything is done
                    tail = node;
                }
                node = next;
            }
            tail.Next = null;

            return head;
        }
    }
}
