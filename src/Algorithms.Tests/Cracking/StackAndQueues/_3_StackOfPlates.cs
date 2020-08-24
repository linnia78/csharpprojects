using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.StackAndQueues
{
    public class _3_StackOfPlates
    {
        /// <summary>
        /// Create a data structure that stores a set of stacks with same capacity
        ///   Pop should pop as if it was one stack
        ///   Push should push as if it was one stack, create new stack of existing stacks reaches capacity
        ///   PopAt should pop from target stack
        /// </summary>
        public _3_StackOfPlates()
        {

        }

        [Theory]
        [InlineData(3, new int[] { 1, 2, 3, 4, 5, 6, 7}, 0, 3)]
        [InlineData(1, new int[] { 1, 2, 3, 4, 5, 6, 7}, 2, 5)]
        [InlineData(1, new int[] { 1, 2, 3, 4, 5, 6, 7}, 7, 0)]
        public void should_push_and_pop_as_if_normal_stack(int capacity, int[] values, int popTimes, int expectedCountOfStacks)
        {
            // Arrange
            var stack = new SetOfStack(capacity);

            // Act
            AddToStackHelper(stack, values);
            PopHelper(stack, popTimes);

            // Assert
            Assert.Equal(expectedCountOfStacks, stack.GetStacksCount());
            AssertStackItems(stack, values, popTimes);
        }

        [Theory]
        [InlineData(3, new int[] { 1, 2, 3, 4, 5, 6, 7}, new int[] { 0, 1, 2 }, new int[] { 3, 6, 7 }, new int[] {}, new int[] { 1, 2, 4, 5 }, 2)]
        [InlineData(1, new int[] { 1, 2, 3, 4, 5, 6, 7}, new int[] { 0, 1, 2 }, new int[] { 1, 3, 5 }, new int[] { 8, 9, 10 }, new int[] { 2, 4, 6, 7, 8, 9, 10 }, 7)]
        [InlineData(2, new int[] { 1, 2, 3, 4, 5, 6, 7}, new int[] { 0, 0, 0, 0, 0, 0, 0 }, new int[] { 2, 1, 4, 3, 6, 5, 7 }, new int[] { }, new int[] {  }, 0)]
        public void should_pop_at_stack_index(int capacity, int[] values, int[] popAtIndexes, int[] expectedPopAtValues, int[] newValues, int[] expectedValues, int expectedCountOfStacks)
        {
            // Arrange
            var stack = new SetOfStack(capacity);

            // Act
            AddToStackHelper(stack, values);

            // Assert
            AssertPopAtValues(stack, popAtIndexes, expectedPopAtValues);
            AddToStackHelper(stack, newValues);
            Assert.Equal(expectedCountOfStacks, stack.GetStacksCount());
            AssertStackItems(stack, expectedValues, 0);
        }

        private void AssertPopAtValues(SetOfStack stack, int[] popAtIndexes, int[] expectedPopAtvalues)
        {
            for(int i = 0; i < popAtIndexes.Length; i++)
            {
                Assert.Equal(expectedPopAtvalues[i], stack.PopAt(popAtIndexes[i]));
            }
        }
        
        private void PopHelper(SetOfStack stack, int popTimes)
        {
            while(popTimes-- > 0)
            {
                stack.Pop();
            }
        }

        private void AddToStackHelper(SetOfStack stack, int[] values)
        {
            foreach(var value in values)
            {
                stack.Push(value);
            }
        }

        private void AssertStackItems(SetOfStack stack, int[] values, int popTimes)
        {
            for (int i = values.Length - popTimes - 1; i >= 0; i--)
            {
                Assert.Equal(values[i], stack.Pop());
            }
        }

        private class SetOfStack
        {
            private readonly List<Stack> _stacks;
            private int _indexToPushItem;
            private readonly int _capacity;
            public SetOfStack(int capacity)
            {
                if (capacity < 1)
                {
                    throw new ArgumentException("Capacity must be greater than 1");
                }

                _capacity = capacity;
                _stacks = new List<Stack>();
            }

            public int GetStacksCount() => _stacks.Count;

            public void Push(int value)
            {
                // Question ? if popped at x, should go back to fill x?
                // Yes -> fill from start to end

                // If currentStackIndex's stack is full, go to next stack that has room or create new stack if all full
                
                // In the beginning, currentStackIndex would point to null stack, need to check if current stack is null or full
                if (!_stacks.Any())
                {
                    _stacks.Add(new Stack(_capacity));
                }
                
                var currentStack = _stacks[_indexToPushItem];
                if (currentStack.IsFull())
                {
                    _indexToPushItem = FindNextStackIndexWithRoom(_indexToPushItem);
                    if (_indexToPushItem == -1)
                    {
                        // no existing stack with room so create new
                        _stacks.Add(new Stack(_capacity));
                        _indexToPushItem = _stacks.Count - 1;
                    }
                    currentStack = _stacks[_indexToPushItem];
                }

                currentStack.Push(value);
            }

            private int FindNextStackIndexWithRoom(int currentIndex)
            {
                for (int i = currentIndex + 1; i < _stacks.Count; i++)
                {
                    if (!_stacks[i].IsFull())
                    {
                        return i;
                    }
                }

                // no existing stacks are available, return -1 denoting all stacks are full
                return -1;
            } 

            public int Pop()
            {
                // Check if current is null or empty, throw invalid operation
                if (!_stacks.Any())
                {
                    throw new InvalidOperationException("All stacks are empty");
                }
                
                // Always pop from the last stack
                var currentStack = _stacks[_stacks.Count - 1];
                var value = currentStack.Pop();
                
                // If stack becomes empty after pop, remove stack from stacks and update indexToPushItem
                if (currentStack.IsEmpty())
                {
                    _indexToPushItem = _indexToPushItem == 0 ? 0 : _indexToPushItem - 1;
                    _stacks.Remove(currentStack);
                }

                return value;
            }

            public int PopAt(int stackIndex)
            {
                if (!_stacks.Any() || _stacks.Count <= stackIndex)
                {
                    throw new InvalidOperationException("Invalid stack");
                }

                var currentStack = _stacks[stackIndex];
                var value = currentStack.Pop();
                if (currentStack.IsEmpty())
                {
                    _indexToPushItem = FindNextStackIndexWithRoom(-1); // this can be optimized, if _indexToPushItem == stackIndex then FindNextStack from start, if < then doesn't matter, if greater then -1
                    _indexToPushItem = _indexToPushItem == -1 ? 0 : _indexToPushItem;
                    _stacks.Remove(currentStack);
                }

                return value;
            }
        }

        private class Stack
        {
            public Node<int> Top;
            public int Capacity { get; }
            public int Count { get; private set; }
            public Stack(int capacity)
            {
                if (capacity < 1)
                {
                    throw new ArgumentException("Capacity must be greater than 1");
                }
                Capacity = capacity;
            }

            public void Push(int value)
            {
                if (IsFull())
                {
                    throw new InvalidOperationException("Capacity reached");
                }

                var node = new Node<int>(value);
                if (Top != null)
                {
                    node.Next = Top;
                }
                Top = node;
                Count++;
            }

            public int Pop()
            {
                if (Top == null)
                {
                    throw new InvalidOperationException("Stack is empty");
                }
                var value = Top.Value;
                Top = Top.Next;
                Count--;
                return value;
            }

            public bool IsFull() => Count == Capacity;
            public bool IsEmpty() => Top == null;
        }

        
    }
}
