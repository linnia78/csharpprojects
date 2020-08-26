using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.StackAndQueues
{
    public class _4_QueueViaStacks
    {
        /// <summary>
        /// Create a queue from two stacks
        /// </summary>
        public _4_QueueViaStacks()
        {

        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, 3, new int[] { }, 0, new int[] { 4 })]
        [InlineData(new int[] { 1 }, 0, new int[] { 1, 2, 3, 4 }, 1, new int[] { 1, 2, 3, 4 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, 4, new int[] { 1, 2, 3, 4 }, 1, new int[] { 2, 3, 4 })]
        public void should_queue_and_dequeue(int[] firstValues, int firstDequeueTimes, int[] secondValues, int secondDequeueTimes, int[] expectedValues)
        {
            // Algorithm
            //      Create a PushStack and a PopStack
            //      Enqueue always pushes to push stack
            //      Dequeue when PopStack is empty, flush all of PushStack to fill PopStack in turns flipping LIFO to create FIFO
            
            // Arrange
            var queue = new QueueViaStacks();

            // Act
            EnqueueHelper(queue, firstValues);
            DequeueHelper(queue, firstDequeueTimes);
            EnqueueHelper(queue, secondValues);
            DequeueHelper(queue, secondDequeueTimes);
            
            // Assert
            foreach(var expected in expectedValues)
            {
                Assert.Equal(expected, queue.Dequeue());
            }
        }

        private void EnqueueHelper(QueueViaStacks queue, int[] values)
        {
            foreach(var value in values)
            {
                queue.Enqueue(value);
            }
        }

        private void DequeueHelper(QueueViaStacks queue, int times)
        {
            while(times-- > 0)
            {
                queue.Dequeue();
            }
        }

        private class QueueViaStacks
        {
            private readonly Stack<int> _popStack = new Stack<int>();
            private readonly Stack<int> _pushStack = new Stack<int>();

            public bool IsEmpty() => _popStack.Count == 0 &&  _pushStack.Count == 0;
            public void Enqueue(int value)
            {
                _pushStack.Push(value);
            }

            public int Dequeue()
            {
                if (IsEmpty())
                {
                    throw new InvalidOperationException("Queue is empty");
                }

                if (_popStack.Count == 0)
                {
                    FlushPushStackOnToPopStack();
                }

                return _popStack.Pop();
            }

            public int Count() => _popStack.Count + _pushStack.Count;

            private void FlushPushStackOnToPopStack()
            {
                while (_pushStack.Count > 0)
                {
                    _popStack.Push(_pushStack.Pop());
                }
            }
        }


        
    }
}
