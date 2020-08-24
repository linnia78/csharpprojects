using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.StackAndQueues
{
    public class _1_ThreeInOne
    {
        /// <summary>
        /// Build 3 stacks using one array
        /// </summary>
        public _1_ThreeInOne()
        {

        }

        [Theory]
        [InlineData(10, 3, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, 3, 3, 3, 4)]
        [InlineData(10, 3, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4 }, 3, 3, 4, 4)]
        [InlineData(10, 3, new int[] { 1 }, new int[] { }, new int[] { 1, 2 }, 1, 0, 2, 4)]
        [InlineData(20, 4, new int[] { 1 }, new int[] { }, new int[] { 1, 2 }, 1, 0, 2, 5)]
        [InlineData(20, 3, new int[] { 1 }, new int[] { }, new int[] { 1, 2 }, 1, 0, 2, 8)]
        public void should_push_to_stack(int maxCapacity, int numOfStacks, int[] stack1, int[] stack2, int[] stack3, int expectedStack1Count, int expectedStack2Count, int expectedStack3Count, int expectedStack3Capacity)
        {
            // Arrange
            var stackArray = new StackArray<int>(maxCapacity, numOfStacks);

            // Act
            AddToStackHelper(stackArray, 0, stack1);
            AddToStackHelper(stackArray, 1, stack2);
            AddToStackHelper(stackArray, 2, stack3);

            // Assert
            Assert.Equal(expectedStack1Count, stackArray.Count(0));
            Assert.Equal(expectedStack2Count, stackArray.Count(1));
            Assert.Equal(expectedStack3Count, stackArray.Count(2));
            Assert.Equal(expectedStack3Capacity, stackArray.Capacity(2));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 1, 2, 6 }, new int[] { 1, 2, 9 })]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 6 }, new int[] { 1, 9 })]
        public void should_pop_from_respective_stack(int[] stack1, int[] stack2, int[] stack3)
        {
            // Arrange
            var stackArray = new StackArray<int>(10, 3);

            // Act
            AddToStackHelper(stackArray, 0, stack1);
            AddToStackHelper(stackArray, 1, stack2);
            AddToStackHelper(stackArray, 2, stack3);

            // Assert
            VerifyStackHelper(stackArray, 0, stack1);
            VerifyStackHelper(stackArray, 1, stack2);
            VerifyStackHelper(stackArray, 2, stack3);
        }

        [Theory]
        [InlineData(10, 3, new int[] { 1, 2, 3, 4 }, new int[] { }, new int[] { 1 }, 0, new int[0], 5, 2, 3)]
        [InlineData(20, 3, new int[] { 1, 2, 3, 4, 5, 6, 7 }, new int[] { }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 0, new int[0], 9, 3, 8 )]
        [InlineData(20, 4, new int[] { 1, 2, 3, 4, 5 }, new int[] { }, new int[] { 1, 2, 3, 4, 5 }, 2, new int[] { 6, 7, 8 }, 5, 3, 9 )]
        public void should_grow(int maxCapacity, int numOfStacks, int[] stack1, int[] stack2, int[] stack3, int postAddStackNum, int[] postAddStack, int expectedStack1Count, int expectedStack2Count, int expectedStack3Count)
        {
            // Arrange
            var stackArray = new StackArray<int>(maxCapacity, numOfStacks);

            // Act
            AddToStackHelper(stackArray, 0, stack1);
            AddToStackHelper(stackArray, 1, stack2);
            AddToStackHelper(stackArray, 2, stack3);

            AddToStackHelper(stackArray, postAddStackNum, postAddStack);
            switch(postAddStackNum)
            {
                case 0: stack1 = stack1.Concat(postAddStack).ToArray();
                    break;
                case 1: stack2 = stack2.Concat(postAddStack).ToArray();
                    break;
                case 2: stack3 = stack3.Concat(postAddStack).ToArray();
                    break;
            }

            // Assert
            Assert.Equal(expectedStack1Count, stackArray.Capacity(0));
            Assert.Equal(expectedStack2Count, stackArray.Capacity(1));
            Assert.Equal(expectedStack3Count, stackArray.Capacity(2));

            VerifyStackHelper(stackArray, 0, stack1);
            VerifyStackHelper(stackArray, 1, stack2);
            VerifyStackHelper(stackArray, 2, stack3);
        }

        private void VerifyStackHelper(StackArray<int> stackArray, int stackNum, int[] expected)
        {
            for(int i = expected.Length - 1; i >= 0; i--)
            {
                Assert.Equal(expected[i], stackArray.Pop(stackNum));
            }
        }

        private void AddToStackHelper(StackArray<int> stackArray, int stackNum, int[] items)
        {
            foreach(var item in items)
            {
                stackArray.Push(stackNum, item);
            }
        }

        private class StackArray<T>
        {
            private readonly int MaxCapacity;
            private readonly int NumOfStacks;
            private readonly StackInArray<T>[] Stacks;
            private readonly T[] ArrayDataStructure;


            public StackArray(int maxCapacity, int numOfStacks)
            {
                if (numOfStacks > maxCapacity)
                {
                    throw new IndexOutOfRangeException("Number of stacks cannot exceed max capacity");
                }
                MaxCapacity = maxCapacity;
                NumOfStacks = numOfStacks;
                Stacks = new StackInArray<T>[numOfStacks];
                ArrayDataStructure = new T[maxCapacity];
                Initialize();
            }

            private void Initialize()
            {
                for(int i = 0; i < NumOfStacks; i++)
                {
                    var stackSize = (MaxCapacity / NumOfStacks); 
                    var startingPosition = i * stackSize;
                    Stacks[i] = new StackInArray<T>(MaxCapacity, stackSize + (i == NumOfStacks - 1 ? MaxCapacity % NumOfStacks : 0), startingPosition); // add remainders to last stack
                }
            }

            public T Pop(int stackNum)
            {
                return Stacks[stackNum].Pop(ArrayDataStructure);
            }

            public void Push(int stackNum, T item)
            {
                if (Stacks.Sum(x => x.Count) == MaxCapacity)
                {
                    throw new IndexOutOfRangeException("Maximum capacity reached");
                }

                if (Stacks[stackNum].IsFull())
                {
                    // grow by taking 1 / num of stacks of remaining capacity from all other stacks
                    // - 1 if negative + num of stacks to get previous stack
                    // shift while not current stack

                    // while shifting, first get num of spaces to shift
                    // update capacity
                    // update starting position
                    var accumulatedSpaces = 0;
                    var currentStackNum = GetRelativeLeftStack(stackNum - 1);
                    do
                    {
                        accumulatedSpaces = Shrink(currentStackNum, accumulatedSpaces);
                        currentStackNum = GetRelativeLeftStack(currentStackNum - 1);;
                    }
                    while(stackNum != currentStackNum);

                    if (accumulatedSpaces < 1)
                    {
                        throw new Exception("No spaces to grow");
                    }
                    else
                    {
                        Grow(stackNum, accumulatedSpaces);
                    }
                }
                Stacks[stackNum].Push(ArrayDataStructure, item);
            }

            public int Count(int stackNum) => Stacks[stackNum].Count;
            public int Capacity(int stackNum) => Stacks[stackNum].Capactiy;

            private int Shrink(int stackNum, int accumulatedSpaces)
            {
                var freedSpaces = (Stacks[stackNum].Capactiy - Stacks[stackNum].Count) / NumOfStacks;
                accumulatedSpaces += freedSpaces;
                ShiftRight(stackNum, accumulatedSpaces);
                Stacks[stackNum].Capactiy -= freedSpaces;
                Stacks[stackNum].StartingPosition = GetRelativeRightPosition(Stacks[stackNum].StartingPosition + accumulatedSpaces);
                return accumulatedSpaces;
            }

            private void Grow(int stackNum, int spaces)
            {
                Stacks[stackNum].Capactiy += spaces;
            }

            private void ShiftRight(int stackNum, int spaces)
            {
                if (spaces > 0)
                {
                    var currentTop = Stacks[stackNum].TopIndex;
                    for (int i = 0; i < Stacks[stackNum].Count; i++)
                    {
                        ArrayDataStructure[GetRelativeRightPosition(currentTop + spaces - i)] = ArrayDataStructure[GetRelativeLeftPosition(currentTop - i)];
                    }
                }
            }

            private int GetRelativeLeftStack(int i)
            {
                return i % NumOfStacks < 0 ? (i % NumOfStacks) + NumOfStacks : i;
            }

            private int GetRelativeRightPosition(int i)
            {
                return i % MaxCapacity;
            }

            private int GetRelativeLeftPosition(int i)
            {
                return i % MaxCapacity < 0 ? (i % MaxCapacity) + MaxCapacity : i;
            }
        }

        private class StackInArray<T>
        {
            private readonly int MaxStackArraySize;
            // Keeps track of current starting position within array
            //  Index will need to shift if stack before this grows
            public int StartingPosition { get; set; }
            // Capacity will tell us if max has reached and grow respectively
            public int Capactiy { get; set; }
            // Count will tell us how many items in current stack
            public int Count { get; private set; }
            public int TopIndex => (StartingPosition + Count - 1) % MaxStackArraySize ;

            public StackInArray(int maxStackArraySize, int capacity, int startingPosition)
            {
                if (maxStackArraySize < 1)
                {
                    throw new ArgumentException("maxStackArraySize must be greater than 1");
                }
                if (capacity < 1)
                {
                    throw new ArgumentException("capacity must be greater than 1");
                }
                if (maxStackArraySize < startingPosition + 1)
                {
                    throw new ArgumentException("startingPosition cannot be greater than maxStackArraySize");
                }

                MaxStackArraySize = maxStackArraySize;
                Capactiy = capacity;
                Count = 0;
                StartingPosition = startingPosition;
            }

            public T Pop(T[] stackArray)
            {
                if (IsEmpty())
                {
                    throw new IndexOutOfRangeException();
                }
                var item = stackArray[TopIndex];
                Count--;
                return item;
            }

            public void Push(T[] stackArray, T item)
            {
                if (IsFull())
                {
                    throw new IndexOutOfRangeException();
                }
                Count++;
                stackArray[TopIndex] = item;
            }

            public T Peek(T[] stackArray)
            {
                return stackArray[TopIndex];
            }
            
            public bool IsEmpty() => Count == 0;
            public bool IsFull() => Count == Capactiy;
        }
    }
}
