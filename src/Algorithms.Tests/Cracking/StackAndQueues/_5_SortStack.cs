using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.StackAndQueues
{
    public class _5_SortStack
    {
        /// <summary>
        /// Create a queue from two stacks
        /// </summary>
        public _5_SortStack()
        {

        }

        [Theory]
        [InlineData(new int[] { 4, 3, 2, 1 }, 1 )]
        [InlineData(new int[] { 1, 2, 3, 4 }, 1 )]
        [InlineData(new int[] { 1, 2, 4, 3 }, 1 )]
        [InlineData(new int[] { 1, 2, 3, 3, 4, 5, 19, -4, 5 }, 1 )]

        [InlineData(new int[] { 4, 3, 2, 1 }, 2 )]
        [InlineData(new int[] { 1, 2, 3, 4 }, 2 )]
        [InlineData(new int[] { 1, 2, 4, 3 }, 2 )]
        [InlineData(new int[] { 1, 2, 3, 3, 4, 5, 19, -4, 5 }, 2 )]
        public void should_sort_stack(int[] values, int order)
        {
            // Algorithm
            //   for every value in main stack
            //      compare with temp stack's top value
            //          if temp stack is > temp then push it back to main
            //   *Algorithm iterates through all N in main stack and buil a tempStack of sorted numbers
            
            // Arrange
            var stack = new SortStack();
            foreach(var value in values)
            {
                stack.Push(value);
            }

            // Act
            stack.Sort(order);
            
            // Assert
            var prev = stack.First();
            foreach(var value in stack.Skip(1))
            {
                if (order == 1)
                {
                    Assert.True(prev <= value);
                }
                else
                {
                    Assert.True(prev >= value);
                }
            }
        }


        public class SortStack : Stack<int>
        {
            // order == 1 : ascending, descending otherwise
            public void Sort(int order)
            {
                if (order == 1)
                {
                    SortAscending();
                }
                else
                {
                    SortDescending();
                }
            } 

            private void SortAscending()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty");
                }
                
                var temp = 0;
                var tempStack = new Stack<int>();
                while(!IsEmpty())
                {
                    temp = Pop();
                    while(tempStack.Count > 0 && tempStack.Peek() > temp)
                    {
                        Push(tempStack.Pop());
                    }
                    tempStack.Push(temp);
                }

                while(tempStack.Count > 0)
                {
                    Push(tempStack.Pop());
                }
            }

            private void SortDescending()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Stack is empty");
                }

                var temp = 0;
                var tempStack = new Stack<int>();
                while(!IsEmpty())
                {
                    temp = Pop();
                    while(tempStack.Count > 0 && tempStack.Peek() < temp)
                    {
                        Push(tempStack.Pop());
                    }
                    tempStack.Push(temp);
                }

                while(tempStack.Count > 0)
                {
                    Push(tempStack.Pop());
                }
            }

            private bool IsEmpty() => Count == 0;
        }
        
    }
}
