using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _8_FirstCommonAncestor
    {
        /// <summary>
        /// Design an algorithm and write code to find the first common ancestor of two noeds in a binary tree.
        /// Avoid storing additional nodes in a data structure. Tree is not a BST
        /// </summary>
        public _8_FirstCommonAncestor()
        {
            
        }

        private class TreeNode
        {
            public int Value;
            public TreeNode Left;
            public TreeNode Right;
            public TreeNode Parent;
            public TreeNode(int value)
            {
                Value = value;
            }

            public TreeNode AddLeft(int value)
            {
                Left = new TreeNode(value);
                Left.Parent = this;
                return Left;
            }

            public TreeNode AddRight(int value)
            {
                Right = new TreeNode(value);
                Right.Parent = this;
                return Right;
            }
        }

        [Fact]
        public void should_find_first_common_ancestor1()
        {
            // Arrange
            /*
                    1
                   2 3
            */
            var tree = new TreeNode(1);
            tree.AddLeft(2);
            tree.AddRight(3);

            // Act
            var pancestor1 = FindFirstCommonAncestorWithParent(tree.Left, tree.Right);
            var pancestor2 = FindFirstCommonAncestorWithParent(tree, tree.Right);
            var pancestor3 = FindFirstCommonAncestorWithParent(tree.Left, tree);

            var ancestor1 = FindFirstCommonAncestorWithoutParent(tree, tree.Left, tree.Right);
            var ancestor2 = FindFirstCommonAncestorWithoutParent(tree, tree, tree.Right);
            var ancestor3 = FindFirstCommonAncestorWithoutParent(tree, tree.Left, tree);

            // Assert
            Assert.Equal(1, pancestor1.Value);
            Assert.Equal(1, pancestor2.Value);
            Assert.Equal(1, pancestor3.Value);

            Assert.Equal(1, ancestor1.Value);
            Assert.Equal(1, ancestor2.Value);
            Assert.Equal(1, ancestor3.Value);
        }

        [Fact]
        public void should_find_first_common_ancestor2()
        {
            // Arrange
            /*
                        1
                      /   \
                    2       3
                  /   \       \
                 4     5        6
                   \             \
                    7             8         
            */
            var tree = new TreeNode(1);
            tree.AddLeft(2);
            tree.AddRight(3);
            var node4 = tree.Left.AddLeft(4);
            var node5 = tree.Left.AddRight(5);
            var node6 = tree.Right.AddRight(6);
            var node7 = tree.Left.Left.AddRight(7);
            var node8 = tree.Right.Right.AddRight(8);

            // Act
            var pancestor1 = FindFirstCommonAncestorWithParent(tree.Left, tree.Left);
            var pancestor2 = FindFirstCommonAncestorWithParent(node7, node8);
            var pancestor3 = FindFirstCommonAncestorWithParent(node7, node5);
            var pancestor4 = FindFirstCommonAncestorWithParent(node4, node6);
            var pancestor5 = FindFirstCommonAncestorWithParent(node4, node7);

            var ancestor1 = FindFirstCommonAncestorWithoutParent(tree, tree.Left, tree.Left);
            var ancestor2 = FindFirstCommonAncestorWithoutParent(tree, node7, node8);
            var ancestor3 = FindFirstCommonAncestorWithoutParent(tree, node7, node5);
            var ancestor4 = FindFirstCommonAncestorWithoutParent(tree, node4, node6);
            var ancestor5 = FindFirstCommonAncestorWithoutParent(tree, node4, node7);

            // Assert
            Assert.Equal(2, pancestor1.Value);
            Assert.Equal(1, pancestor2.Value);
            Assert.Equal(2, pancestor3.Value);
            Assert.Equal(1, pancestor4.Value);
            Assert.Equal(4, pancestor5.Value);

            Assert.Equal(2, ancestor1.Value);
            Assert.Equal(1, ancestor2.Value);
            Assert.Equal(2, ancestor3.Value);
            Assert.Equal(1, ancestor4.Value);
            Assert.Equal(4, ancestor5.Value);
        }

        private TreeNode FindFirstCommonAncestorWithoutParent(TreeNode root, TreeNode node1, TreeNode node2)
        {
            if (root == null) { return null; }
            if (root.Value == node1.Value || root.Value == node2.Value)
            {
                return root;
            }
            var node1AtLeft = IsInSubtree(root.Left, node1);
            var node2AtLeft = IsInSubtree(root.Left, node2);
            if (node1AtLeft && node2AtLeft)
            {
                return FindFirstCommonAncestorWithoutParent(root.Left, node1, node2);
            }
            else if (!node1AtLeft && !node2AtLeft)
            {
                return FindFirstCommonAncestorWithoutParent(root.Right, node1, node2);
            }
            else
            {
                return root;
            }
        }

        private bool IsInSubtree(TreeNode tree, TreeNode target)
        {
            if (tree == null) { return false; }
            if (tree.Value == target.Value)
            {
                return true;
            }
            return IsInSubtree(tree.Left, target) || IsInSubtree(tree.Right, target);
        }

        private TreeNode FindFirstCommonAncestorWithParent(TreeNode node1, TreeNode node2)
        {
            var depth1 = GetDepth(node1);
            var depth2 = GetDepth(node2);
            var diff = Math.Abs(depth1 - depth2);
            if (depth1 > depth2)
            {
                node1 = MoveUp(node1, diff);
            }
            else if (depth2 > depth1)
            {
                node2 = MoveUp(node2, diff);
            }

            while(node1 != node2)
            {
                node1 = node1.Parent;
                node2 = node2.Parent;
            }
            return node1;
        }

        private TreeNode MoveUp(TreeNode node, int times)
        {
            while(times-- > 0)
            {
                node = node.Parent;
            }
            return node;
        }

        private int GetDepth(TreeNode node)
        {
            if (node == null) return 0;
            return GetDepth(node.Parent) + 1;
        }
    }
}
