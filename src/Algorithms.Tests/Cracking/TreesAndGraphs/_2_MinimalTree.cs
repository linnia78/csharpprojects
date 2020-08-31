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

        
       
        public void should_build_binary_search_tree_with_minimal_height()
        {
            // Algorithm
            //  Since collection is already sorted we can perform divide and conquer
            //  Repeated divide colleciton in half the set parent node as collection[mid]
            //  while recursively calculate left and right where left is position left - mid -1 and right is mid + 1 to right
            // Arrange

            // Act

            // Assert
        }

        private TreeNode<int> BuildBst(int[] array)
        {
            return BuildBst(array, 0, array.Length - 1);
        }

        private TreeNode<int> BuildBst(int[] array, int left, int right)
        {
            if (right > left) { return null; }
            var mid = (left + right) / 2;
            var node = new TreeNode<int>(array[mid]);
            node.Left = BuildBst(array, left, mid - 1);
            node.Right = BuildBst(array, mid + 1, right);
            return node;
        }
    }
}
