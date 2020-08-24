using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.StackAndQueues
{
    public class _2_MinStack
    {
        /// <summary>
        /// Create a stack where it implements function Min() where it returns the minimum node
        /// </summary>
        public _2_MinStack()
        {

        }

        [Theory]
        [InlineData(new int[]{ 1, 2, 3 }, 2, new int[]{}, 0, 1, 1)]
        [InlineData(new int[]{ 10, 10, 5 }, 1, new int[]{ 10 }, 1, 10, 10)]
        [InlineData(new int[]{ 5, 4, 3 }, 3, new int[]{ 1 }, 0, 1, 1)]
        public void should_return_correct_popped_value_and_min_value(int[] initialStack, int initialPopTimes, int[] secondPushes, int secondPopTimes, int expectedMinValue, int expectedPoppedValue)
        {
            // Algorithm
            // When pushing, each node would keep track of current min node and has a reference to it
            //   in doing so, when a value is popped, next node automatically has the next highest node

            // Arrange
            var stack = MinStackHelper(null, initialStack);
            MinStackPopper(stack, initialPopTimes);
            MinStackHelper(stack, secondPushes);
            MinStackPopper(stack, secondPopTimes);

            // Act
            var minValue = stack.Min();
            var value = stack.Pop();
            
            // Assert
            Assert.Equal(expectedMinValue, minValue);
            Assert.Equal(expectedPoppedValue, expectedPoppedValue);
        }

        private void MinStackPopper(MinStack stack, int numOfTimes)
        {
            while(numOfTimes-- > 0)
            {
                stack.Pop();
            }
        }

        private MinStack MinStackHelper(MinStack stack, int[] values)
        {
            if (stack == null)
            {
                stack = new MinStack();
            }
            foreach(var value in values)
            {
                stack.Push(value);
            }
            return stack;
        }
        
        private class MinStackData
        {
            public int Value;
            public int MinValue;
        }

        private class MinStack
        {
            private Node<MinStackData> Top;
            public MinStack()
            {

            }

            public void Push(int value)
            {
                if (Top == null)
                {
                    Top = new Node<MinStackData>
                    {
                        Value = new MinStackData{
                            Value = value,
                            MinValue = value
                        }
                    };
                }
                else
                {
                    var node = new Node<MinStackData>();
                    node.Value = new MinStackData{
                        Value = value,
                        MinValue = value < Top.Value.MinValue ? value : Top.Value.MinValue
                    };
                    node.Next = Top;
                    Top = node;
                }
            }

            public int Pop()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException();
                }
                var data = Top.Value;
                Top = Top.Next;
                return data.Value;
            }

            public int Peek()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException();
                }
                return Top.Value.Value;
            }

            public bool IsEmpty() => Top == null;

            public int Min()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException();
                }
                return Top.Value.MinValue;
            }
        }
    }
}
