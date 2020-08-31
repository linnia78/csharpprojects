using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _2_MinimalTree
    {
        /// <summary>
        /// Build a binary search tree with minimal height provide a already sorted list of items
        /// </summary>
        public _2_MinimalTree()
        {
        
        }

        [Theory]
        [InlineData(new int[] { 1 }, 1, new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4 }, 3, new int[]{ 1, 2, 3, 4 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 3, new int[] { 1, 2, 3, 4, 5, 6, 7 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 4, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void should_build_binary_search_tree_with_minimal_height(int[] sortedArray, int expectedHeight, int[] expectedInOrderTraversal)
        {
            // Algorithm
            //  Since collection is already sorted we can perform divide and conquer
            //  Repeated divide colleciton in half the set parent node as collection[mid]
            //  while recursively calculate left and right where left is position left - mid -1 and right is mid + 1 to right
            
            // Arrange
            var bst = BuildBst(sortedArray);
            
            // Act
            var height = bst.GetMaxHeight();

            // Assert
            Assert.Equal(expectedHeight, height);
            foreach(var data in bst.InOrderTraversalRecursively().Zip(expectedInOrderTraversal))
            {
                Assert.Equal(data.Second, data.First);
            }
        }

        private TreeNode<int> BuildBst(int[] sortedArray)
        {
            return BuildBst(sortedArray, 0, sortedArray.Length - 1);
        }

        private TreeNode<int> BuildBst(int[] sortedArray, int left, int right)
        {
            if (right < left) { return null; }
            var mid = (left + right) / 2;
            var node = new TreeNode<int>(sortedArray[mid]);
            node.Left = BuildBst(sortedArray, left, mid - 1);
            node.Right = BuildBst(sortedArray, mid + 1, right);
            return node;
        }
    }
}
