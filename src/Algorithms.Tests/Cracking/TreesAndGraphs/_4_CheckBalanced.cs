using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _4_CheckBalanced
    {
        /// <summary>
        /// Implement a function to check f a binary tree is balanced. For the purpose of this question, a balanced tree is 
        /// defined to be a tree such that the heights of the two subtrees of any node never differ by more than one.
        /// </summary>
        public _4_CheckBalanced()
        {
        
        }

        [Fact]
        public void should_check_if_binary_tree_is_balanced()
        {
            // Arrange
            /*
                    1
                   /
                  1
                 /
                1 
            */
            var tree1 = new TreeNode<int>(1);
            tree1.Left = new TreeNode<int>(1);
            tree1.Left.Left = new TreeNode<int>(1);

            // Act
            var result = IsBalanced(tree1);
            
            // Assert
            Assert.False(result != Int32.MinValue);
        }

        [Fact]
        public void should_check_if_binary_tree_is_balanced1()
        {
            // Arrange
            /*
                    1
                   /
                  1
                   \
                    1
            */
            var tree1 = new TreeNode<int>(1);
            tree1.Left = new TreeNode<int>(1);
            tree1.Left.Right = new TreeNode<int>(1);

            // Act
            var result = IsBalanced(tree1);
            
            // Assert
            Assert.False(result != Int32.MinValue);
        }

        [Fact]
        public void should_check_if_binary_tree_is_balanced2()
        {
            // Arrange
            /*
                    1
                   /  \
                  1    1
                 / \
                1   1
            */
            var tree1 = new TreeNode<int>(1);
            tree1.Right = new TreeNode<int>(1);
            tree1.Left = new TreeNode<int>(1);
            tree1.Left.Left = new TreeNode<int>(1);
            tree1.Left.Right = new TreeNode<int>(1);

            // Act
            var result = IsBalanced(tree1);
            
            // Assert
            Assert.True(result != Int32.MinValue);
        }

        [Fact]
        public void should_check_if_binary_tree_is_balanced3()
        {
            // Arrange
            /*
                    1
                   /  \
                  1    1
                 / \
                1   1
               /
              1
            */
            var tree1 = new TreeNode<int>(1);
            tree1.Right = new TreeNode<int>(1);
            tree1.Left = new TreeNode<int>(1);
            tree1.Left.Left = new TreeNode<int>(1);
            tree1.Left.Right = new TreeNode<int>(1);
            tree1.Left.Left.Left = new TreeNode<int>(1);

            // Act
            var result = IsBalanced(tree1);
            
            // Assert
            Assert.False(result != Int32.MinValue);
        }

        public int IsBalanced(TreeNode<int> node)
        {
            if (node == null)
            {
                return 0;
            }
            
            var leftHeight = IsBalanced(node.Left);
            if (leftHeight == Int32.MinValue) { return Int32.MinValue; }

            var rightHeight = IsBalanced(node.Right);
            if (rightHeight == Int32.MinValue) { return Int32.MinValue; }

            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                return Int32.MinValue;
            }
            else
            {
                return Math.Max(leftHeight, rightHeight) + 1;
            }
        }

    }
}
