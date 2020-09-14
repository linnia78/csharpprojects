using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _6_Successor
    {
        /// <summary>
        /// Write an algorithm to find the "next" node (in order successor) of a given node in a binary search tree.true
        /// Each node has a link to its parent
        /// </summary>
        public _6_Successor()
        {
            // Algorithm : In Order = LPR
            // Next node is on the R subtree
            //   it will be the left most node of R subtree
            // If no R subtree
            //   then it's the first parent(ancesort)'s parent where it's the left child
            //   ie.         1
            //            2     3
            //          4   5
            //   at node 5, it's succssor is 1.  The first parent where node 5's ancesort is the left child

        }

        [Fact]
        public void should_determine_if_tree_is_BST()
        {
            // Arrange
            //               1
            //            2     3
            //          4   5     6
            //                7
            //              8
            var tree = new SpecialNode(1);
            tree.AddLeft(2);
            tree.AddRight(3);
            tree.Left.AddLeft(4);
            tree.Left.AddRight(5);
            tree.Right.AddRight(6);
            tree.Left.Right.AddRight(7);
            tree.Left.Right.Right.AddLeft(8);

            // Act
            var successor1 = GetSuccessor(tree);
            var successor2 = GetSuccessor(tree.Left);
            var successor3 = GetSuccessor(tree.Right);
            var successor4 = GetSuccessor(tree.Left.Left);
            var successor5 = GetSuccessor(tree.Left.Right);
            var successor6 = GetSuccessor(tree.Right.Right);
            var successor7 = GetSuccessor(tree.Left.Right.Right);
            var successor8 = GetSuccessor(tree.Left.Right.Right.Left);

            // Assert
            Assert.Equal(3, successor1.Value);
            Assert.Equal(5, successor2.Value);
            Assert.Equal(6, successor3.Value);
            Assert.Equal(2, successor4.Value);
            Assert.Equal(8, successor5.Value);
            Assert.Equal(null, successor6);
            Assert.Equal(1, successor7.Value);
            Assert.Equal(7, successor8.Value);
        }

        [Fact]
        public void should_determine_if_tree_is_BST2()
        {
            // Arrange
            //              1
            //            2     
            //          3   
            var tree = new SpecialNode(1);
            tree.AddLeft(2);
            tree.Left.AddLeft(3);

            // Act
            var successor1 = GetSuccessor(tree);
            var successor2 = GetSuccessor(tree.Left);
            var successor3 = GetSuccessor(tree.Left.Left);

            // Assert
            Assert.Equal(null, successor1);
            Assert.Equal(1, successor2.Value);
            Assert.Equal(2, successor3.Value);
        }

        [Fact]
        public void should_determine_if_tree_is_BST3()
        {
            // Arrange
            //              1
            //                2     
            //                  3   
            var tree = new SpecialNode(1);
            tree.AddRight(2);
            tree.Right.AddRight(3);

            // Act
            var successor1 = GetSuccessor(tree);
            var successor2 = GetSuccessor(tree.Right);
            var successor3 = GetSuccessor(tree.Right.Right);

            // Assert
            Assert.Equal(2, successor1.Value);
            Assert.Equal(3, successor2.Value);
            Assert.Equal(null, successor3);
        }

        public SpecialNode GetSuccessor(SpecialNode node)
        {
            if (node == null) { return null; }
            if (node.Right != null)
            {
                return Leftmost(node.Right);
            }
            else
            {
                var parent = node.Parent;
                var current = node;
                while(parent != null && parent.Left != current)
                {
                    parent = parent.Parent;
                    current = current.Parent;
                }
                return parent;
            }

        }

        private SpecialNode Leftmost(SpecialNode node)
        {
            if (node == null)
            {
                return null;
            }
            while(node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public class SpecialNode
        {
            public int Value;
            public SpecialNode Left;
            public SpecialNode Right;
            public SpecialNode Parent;
            public SpecialNode(int value)
            {
                Value = value;
            }

            public void AddLeft(int value)
            {
                Left = new SpecialNode(value);
                Left.Parent = this;
            }

            public void AddRight(int value)
            {
                Right = new SpecialNode(value);
                Right.Parent = this;
            }
        }
    }
}
