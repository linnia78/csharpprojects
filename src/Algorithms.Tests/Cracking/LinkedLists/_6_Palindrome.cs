using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.LinkedLists
{
    public class _6_Palindrome
    {
        /// <summary>
        /// Check if linked list is palindrome
        /// </summary>
        public _6_Palindrome()
        {
            
        }

        [Theory]
        [InlineData(new int[] { 1 }, true)]
        [InlineData(new int[] { 1, 2, 1 }, true)]
        [InlineData(new int[] { 1, 2, 2, 1 }, true)]
        [InlineData(new int[] { 1, 2, 3 }, false)]
        [InlineData(new int[] { 1, 2, 3, 1 }, false)]
        public void should_check_if_palindrome_iteratively(int[] array, bool expected)
        {
            // Algorithm
            // create fast pointer which moves at 2 x
            // create slow pointer, while traversing add value to stack
            // when end is reached the slow is at mid
            // continue iteration and compare with stack

            // Arrange
            var node = Node<int>.Create(array);

            // Act
            var isPalindrome = IsPalindromeIteratively(node);

            // Assert
            Assert.Equal(expected, isPalindrome);
        }

        private bool IsPalindromeIteratively(Node<int> node)
        {
            // traverse till mide and add to stack
            var slow = node;
            var fast = node;
            var stack = new Stack<int>();
            while (slow != null && fast != null && fast.Next != null)
            {
                stack.Push(slow.Value);
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            // if fast == null then even / if fast != null then odd
            // if even then compare current with stack
            // if odd then compare starting from next with stack, "skip one"
            if (fast != null)
            {
                slow = slow.Next;
            }

            // iterate till end of loop, return false if mismatch found
            while(slow != null)
            {
                if (stack.Pop() != slow.Value)
                {
                    return false;
                }
                slow = slow.Next;
            }
            return true;
        }

        [Theory]
        [InlineData(new int[] { 1 }, true)]
        [InlineData(new int[] { 1, 2, 1 }, true)]
        [InlineData(new int[] { 1, 2, 2, 1 }, true)]
        [InlineData(new int[] { 1, 2, 3 }, false)]
        [InlineData(new int[] { 1, 2, 3, 1 }, false)]
        public void should_check_if_palindrome_recursively(int[] array, bool expected)
        {
            // Algorithm - get length of linked list
            // Iterate till mid of list by subtracting length by 2
            // if length is 0 then even -> return current node as we would be on the node that needs to be used to compare
            // if length is 1 then odd -> return next node
            // during comparision, compare the recursion returned node and pass back the next node for call stack to compare
            // Arrange
            var node = Node<int>.Create(array);

            // Act
            var isPalindrome = IsPalindromeRecursively(node);

            // Assert
            Assert.Equal(expected, isPalindrome);
        }

        private bool IsPalindromeRecursively(Node<int> node)
        {
            var length = GetSize(node);
            return IsPalindromeIterativelyHelper(node, length).IsMatch;
        }

        private Result IsPalindromeIterativelyHelper(Node<int> current, int length)
        {
            if (length == 0)
            {
                // if 0 then even, pass back current
                return new Result(current, true);
            } 
            else if (length == 1)
            {
                // if 1 then odd, pass back next
                return new Result(current.Next, true);
            }

            var result = IsPalindromeIterativelyHelper(current.Next, length - 2);
            if (result.IsMatch == false)
            {
                return new Result(null, false);
            }
            return new Result(result.Node.Next, result.Node.Value == current.Value);
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

        private class Result
        {
            public Node<int> Node { get; }
            public bool IsMatch { get; }
            public Result(Node<int> node, bool isMatch)
            {
                Node = node;
                IsMatch = isMatch;
            }
        }
    }
}
