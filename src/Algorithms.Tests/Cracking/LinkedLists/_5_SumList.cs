using System;
using System.Collections.Generic;
using System.Text;
using Algorithms.Tests.DataStructures.Node;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _5_SumList
    {
        /// <summary>
        /// two numbers represeneted with linked list, each with single digit in reverse order, calculate sum
        /// A = 7 -> 1 -> 6 = 617
        /// B = 5 -> 9 -> 2 = 295
        /// out 2 -> 1 -> 9
        /// part two, in forward order
        /// A = 6 -> 1 -> 7
        /// B = 2 -> 9 -> 5
        /// out = 9 -> 1 -> 2
        /// </summary>
        public _5_SumList()
        {
            // Algorithm
            // Part 1 - iterative
            // since 1s is at starting position, iterate both list such that both reaches end AND that carry = 0
            // add to result list
            // Part 1 - recursion
            // calculate and iterate, at each iteration set next to recursed result
            // A = 7 -> 1 -> 6
            // B = 5 -> 9 -> 2
            // sum = 12, R = 2, Carry 1
              // sum = 11, R = 1, Carry 1
                // sum = 9, R = 9

            // Part 2
            // reverse the order
            // call Part 1
        }

        [Theory]
        [InlineData(new int[] { 7, 1, 6 }, new int[] { 5, 9, 2 }, new int[] { 2, 1, 9 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, new int[] { 9 }, new int[] { 0, 3, 3, 4 })]
        [InlineData(new int[] { 9 }, new int[] { 1, 2, 3, 4 }, new int[] { 0, 3, 3, 4 })]
        [InlineData(new int[] { 0 }, new int[] { 0 }, new int[] { 0 })]
        [InlineData(new int[] { }, new int[] { }, new int[] { 0 })]
        public void should_add_sum_part_1(int[] a, int[] b, int[] expectedSum)
        {
            // Arrange
            var aHead = Node<int>.Create(a);
            var bHead = Node<int>.Create(b);

            // Act
            var sumIterative = SumPart1Iterative(aHead, bHead);
            var sumRecursion = SumPart1Recursion(aHead, bHead, 0) ?? new Node<int>(0);

            // Assert
            foreach(var value in expectedSum)
            {
                Assert.Equal(value, sumIterative.Value);
                sumIterative = sumIterative.Next;

                Assert.Equal(value, sumRecursion.Value);
                sumRecursion = sumRecursion.Next;
            }
        }

        private Node<int> SumPart1Recursion(Node<int> a, Node<int> b, int carry)
        {
            if (a == null && b == null && carry == 0)
            {
                return null;
            }
            var sum = (a == null ? 0 : a.Value) + (b == null ? 0 : b.Value) + carry;
            carry = sum / 10;

            var next = SumPart1Recursion(a?.Next, b?.Next, carry);
            
            var node = new Node<int>(sum % 10);
            node.Next = next;

            return node;
        }

        private Node<int> SumPart1Iterative(Node<int> a, Node<int> b)
        {
            var carry = 0;
            var result = new Node<int>(0);
            var ptr = (Node<int>)null;
            var sum = 0;

            while(a != null || b != null || carry != 0)
            {
                sum = 
                    (a == null ? 0 : a.Value) + 
                    (b == null ? 0: b.Value) + carry;
                carry = sum / 10;
                if (ptr == null)
                {
                    ptr = result;
                    result.Value = sum % 10;
                }
                else
                {
                    ptr.Next = new Node<int>(sum % 10);
                    ptr = ptr.Next;
                }

                a = a?.Next;
                b = b?.Next;
            }

            return result;
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 5 })]
        [InlineData(new int[] { 1, 2, 9 }, new int[] { 9, 3, 4 }, new int[] { 1, 0, 6, 3 })]
        public void should_add_sum_part_2_count_length_and_pad_zeroes(int[] a, int[] b, int[] expectedSum)
        {
            //// Algorithm 2 - count longest number and pad shorter with zeroes
            // Arrange
            var aHead = Node<int>.Create(a);
            var bHead = Node<int>.Create(b);

            // Act
            var sumPointer = SumPart2_count_pad_zeroes(aHead, bHead);

            // Assert
            foreach(var value in expectedSum)
            {
                Assert.Equal(value, sumPointer.Value);
                sumPointer = sumPointer.Next;
            }
        }

        private Node<int> SumPart2_count_pad_zeroes(Node<int> a, Node<int> b)
        {
            // count the nodes
            var sizeA = CountNodes(a);
            var sizeB = CountNodes(b);

            // pad with zeroes if diff in sizes
            var diffInSize = Math.Abs(sizeA - sizeB);
            var longNodes = sizeA > sizeB ? a : b;
            var shortNodes = sizeA <= sizeB ? a : b;
            if (diffInSize != 0)
            {
                shortNodes = PadZeroesInFront(shortNodes, diffInSize);
            }            
            
            // get sum
            var partialSum = DoMathPart2_Helper(shortNodes, longNodes);
            var head = partialSum.Node;
            if (partialSum.Carry > 0)
            {
                head = new Node<int>(1);
                head.Next = partialSum.Node;
            }
            return head;
        }

        private PartialSum DoMathPart2_Helper(Node<int> a, Node<int> b)
        {
            if (a == null || b == null)
            {
                return new PartialSum(null, 0);
            }

            var partialSum = DoMathPart2_Helper(a.Next, b.Next);
            var sum = a.Value + b.Value + partialSum.Carry;
            var node = new Node<int>(sum % 10);
            node.Next = partialSum.Node;
            var carry = sum / 10;
            return new PartialSum(node, carry);
        }

        private class PartialSum
        {
            public Node<int> Node { get; }
            public int Carry { get; }
            public PartialSum(Node<int> node, int carry)
            {
                Node = node;
                Carry = carry;
            }
        }

        private Node<int> PadZeroesInFront(Node<int> node, int count)
        {
            var head = node;
            var temp = (Node<int>)null;
            while (count-- > 0)
            {
                temp = new Node<int>(0);
                temp.Next = head;
                head = temp;
            }
            return head;
        }

        private int CountNodes(Node<int> node)
        {
            var count = 0;
            while(node != null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 5 })]
        [InlineData(new int[] { 1, 2, 9 }, new int[] { 9, 3, 4 }, new int[] { 1, 0, 6, 3 })]
        public void should_add_sum_part_2_reverse_nodes(int[] a, int[] b, int[] expectedSum)
        {
            //// Algorithm 1 - reverse to do sum and reverse again to return expected result
            // Arrange
            var aHead = Node<int>.Create(a);
            var bHead = Node<int>.Create(b);

            // Act
            var sumPointer = SumPart2_reverse_nodes(aHead, bHead);

            // Assert
            foreach(var value in expectedSum)
            {
                Assert.Equal(value, sumPointer.Value);
                sumPointer = sumPointer.Next;
            }
        }

        private Node<int> SumPart2_reverse_nodes(Node<int> a, Node<int> b)
        {
            // reverse node
            var ra = ReverseNode(a);
            var rb = ReverseNode(b);

            // do math
            var sum = SumPart1Iterative(ra, rb);
            var rsum = ReverseNode(sum);
            return rsum;
        }

        private Node<int> ReverseNode(Node<int> a)
        {
            var pervious = (Node<int>)null;
            var current = a;
            while(current != null)
            {
                var temp = current.Next;
                current.Next = pervious;
                pervious = current;
                current = temp;
            }
            return pervious;
        }
    }
}
