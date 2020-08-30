using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.DataStructures
{
    public class KLargestNumberTest
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
        [InlineData(new int[] { 1, 2, 8, 7, 9, 8, 9, 3, 10, 3, 10, 10 }, new int[] { 10, 10, 10, 9, 9, 8, 8, 7 })]
        [InlineData(new int[] { 10 }, new int[] { 10 })]
        [InlineData(new int[] { 10, 10, 10, 10, 10 }, new int[] { 10, 10, 10, 10, 10 })]
        public void should_return_k_largest_numbers(int[] initialValues, int[] kExpectedValues)
        {
            // Arrange
            var heap = new Heap(initialValues);

            // Act
            heap.Heapify();

            // Assert
            foreach(var expectedValue in kExpectedValues)
            {
                Assert.Equal(expectedValue, heap.ExtractMax());
            }
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 4, 5, 6, 7 }, new int[] { 7, 6, 5 })]
        [InlineData(new int[] { 4, 5, 1, 9 }, new int[] { 9, 9, 9, 9 }, new int[] { 9, 9, 9, 9, 9 })]
        [InlineData(new int[] { 3, 3, 3, 3, 3 }, new int[] { 3, 3, 3, 3, 3 }, new int[] { 3, 3, 3, 3, 3 })]
        public void should_return_k_largest_numbers_after_insert(int[] initialValues, int[] insertValues, int[] kExpectedValues)
        {
            // Arrange
            var heap = new Heap(initialValues);

            // Act
            heap.Heapify();
            foreach(var value in insertValues)
            {
                heap.Insert(value);
            }

            // Assert
            foreach(var expectedValue in kExpectedValues)
            {
                Assert.Equal(expectedValue, heap.ExtractMax());
            }
        }
    }
    public class Heap
    {
        private int[] _datas;
        private int _count;
        public Heap() { }
        public Heap(int[] datas)
        {
            _datas = datas;
            _count = _datas.Length;
        }

        public void Insert(int data)
        {
            if (_count == _datas.Length)
            {
                var newArray = new int[_count * 2];
                Array.Copy(_datas, newArray, _count);
                _datas = newArray;
            }
            _datas[_count] = data;
            HeapifyFromBottomUp(_count);
            _count++;
        }

        private void HeapifyFromBottomUp(int nodeIndex)
        {
            var parent = (nodeIndex - 1) / 2;
            if (parent >= 0 && _datas[nodeIndex] > _datas[parent])
            {
                Swap(parent, nodeIndex);
                HeapifyFromBottomUp(parent);
            }
        }

        public void Heapify() 
        { 
            // Algorithm : to heapify an array O(n) algorithm is to start from lowest non-leaf nodes. (index at n/2 - 1) where n is number of nodes
            //  heapify in reverse order from lowest non-leaf nodes
            //    recursively heapify from node (top down)
            var lastNonLeafNodeIndex = (_datas.Length / 2) - 1;
            for (int i = lastNonLeafNodeIndex; i >= 0; i--)
            {
                HeapifyFromTopDown(i);
            }
        }

        public int ExtractMax()
        {
            if (_datas.Length < 1)
            {
                throw new InvalidOperationException("Collection is empty");
            }
            int max = _datas[0];
            _count--;
            _datas[0] = _datas[_count];
            HeapifyFromTopDown(0);
            return max;
        }

        private void HeapifyFromTopDown(int nodeIndex)
        {
            var left = nodeIndex * 2 + 1;
            var right = nodeIndex * 2 + 2;
            var currentMaxIndex = nodeIndex;
            if (left < _count && _datas[left] > _datas[currentMaxIndex])
            {
                currentMaxIndex = left;
            }
            if (right < _count && _datas[right] > _datas[currentMaxIndex])
            {
                currentMaxIndex = right;
            }
            if (currentMaxIndex != nodeIndex)
            {
                Swap(currentMaxIndex, nodeIndex);
                HeapifyFromTopDown(currentMaxIndex);
            }
        }

        private void Swap(int index1, int index2)
        {
            var temp = _datas[index1];
            _datas[index1] = _datas[index2];
            _datas[index2] = temp;            
        }
    }

}
