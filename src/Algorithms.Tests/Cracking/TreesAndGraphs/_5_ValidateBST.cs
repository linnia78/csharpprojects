using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _5_ValidateBST
    {
        /// <summary>
        /// Check if a tree is a BST
        /// </summary>
        public _5_ValidateBST()
        {
        
        }

        [Fact]
        public void should_determine_if_tree_is_BST()
        {
            // Arrange
            /*
                        10
                       5  15
            */
            var tree = new TreeNode<int>(10);
            tree.Left = new TreeNode<int>(5);
            tree.Right = new TreeNode<int>(15);
            
            // Act
            var result1 = IsBST1(tree);
            var result2 = IsBST2(tree);
            var result3 = IsBST3(tree);
            
            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }

        [Fact]
        public void should_determine_if_tree_is_BST2()
        {
            // Arrange
            /*
                        10
                     5      15
                   3   10
                         11
            */
            var tree = new TreeNode<int>(10);
            tree.Left = new TreeNode<int>(5);
            tree.Left.Left = new TreeNode<int>(3);
            tree.Left.Right = new TreeNode<int>(10);
            tree.Left.Right.Right = new TreeNode<int>(11);
            tree.Right = new TreeNode<int>(15);
            
            // Act
            var result1 = IsBST1(tree);
            var result2 = IsBST2(tree);
            var result3 = IsBST3(tree);
            
            // Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        private bool IsBST3(TreeNode<int> node)
            => IsBST3(node, Int32.MinValue, Int32.MaxValue);

        private bool IsBST3(TreeNode<int> node, int min, int max)
        {
            if (node == null) { return true; }
            if (!IsBST3(node.Left, min, node.Value)) { return false; }
            if (!(node.Value >= min && node.Value < max))
            {
                return false;
            }
            if (!IsBST3(node.Right, node.Value, max)) { return false; }
            return true;
        }

        private bool IsBST1(TreeNode<int> node)
        {
            var temp = Int32.MinValue;
            return IsBST1(node, ref temp);
        }
        private bool IsBST1(TreeNode<int> node, ref int prev)
        {
            if (node == null) { return true; }
            if (!IsBST1(node.Left, ref prev)) { return false; }
            if (!(prev <= node.Value)) { return false; }
            prev = node.Value;
            if (!IsBST1(node.Right, ref prev)) { return false; }
            return true;
        }

        private bool IsBST2(TreeNode<int> node)
        {
            if (node == null) { return true; }
            var left = GetMax(node.Left);
            var right = GetMin(node.Right);
            if (!(IsBST2(node.Left) && IsBST2(node.Right)))
            {
                return false;
            }
            return node.Value >= left && node.Value < right;
        }

        private int GetMax(TreeNode<int> node)
        {
            if (node == null) { return Int32.MinValue; }
            return Math.Max(Math.Max(GetMax(node.Left), GetMax(node.Right)), node.Value);
        }

        private int GetMin(TreeNode<int> node)
        {
            if (node == null) { return Int32.MaxValue; }
            return Math.Min(Math.Min(GetMin(node.Left), GetMin(node.Right)), node.Value);
        }
    }
}
