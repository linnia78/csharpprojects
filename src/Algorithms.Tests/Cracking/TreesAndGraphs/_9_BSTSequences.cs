using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _9_BSTSequences
    {
        /// <summary>
        /// A binary search tree was created by traversing through an array from left to right and inserting each element.
        /// Given a binary search tree with distinct elements, print all possible arrays that could have led to this tree.
        /// </summary>
        public _9_BSTSequences()
        {
            
        }

        private class TreeNode
        {
            public int Value;
            public TreeNode Left;
            public TreeNode Right;
            public TreeNode(int value)
            {
                Value = value;
            }

            public TreeNode AddLeft(int value)
            {
                Left = new TreeNode(value);
                return Left;
            }

            public TreeNode AddRight(int value)
            {
                Right = new TreeNode(value);
                return Right;
            }
        }

        [Fact]
        public void should_generate_all_possible_permutations()
        {
            // Arrange
            var tree = new TreeNode(5);
            
            // Act
            var permutations = GeneratePermutations(tree);

            // Assert
            Assert.Equal(1, permutations.Count);
        }

        [Fact]
        public void should_generate_app_possible_permutations()
        {
            // Arrange
            var tree = new TreeNode(5);
            tree.Left = new TreeNode(3);
            tree.Left.Left = new TreeNode(2);
            tree.Left.Right = new TreeNode(4);
            tree.Right = new TreeNode(7);
            tree.Right.Left = new TreeNode(6);
            tree.Right.Right = new TreeNode(8);

            // Act
            var permutations = GeneratePermutations(tree);
            
            // Assert
            Assert.Equal(6, permutations.Count);
        }

        private List<List<int>> GeneratePermutations(TreeNode node)
        {
            var results = new List<List<int>>();
            if (node == null)
            {
                results.Add(new List<int>());
                return results;
            }
            var left = GeneratePermutations(node.Left);
            var right = GeneratePermutations(node.Right);
            var prefix = new List<int>{ node.Value };

            foreach(var l in left)        
            {
                foreach(var r in right)
                {
                    results.AddRange(Combine(l, r, prefix));
                }
            }
            return results;
        }

        private List<List<int>> Combine(List<int> left, List<int> right, List<int> prefix)
        {
            var results = new List<List<int>>();
            if (!left.Any() || !right.Any())
            {
                var clone = new List<int>(prefix);
                clone.AddRange(left);
                clone.AddRange(right);
                results.Add(clone);
                return results;
            }

            // Handles left subtree
            // Repeatedly remove first element to recursively generate permutations
            var l = left[0];
            left.RemoveAt(0);
            prefix.Add(l);
            results.AddRange(Combine(left, right, prefix));
            left.Insert(0, l);
            prefix.RemoveAt(prefix.Count - 1);

            // Handles right subtree
            // Repeated remove first element to recursively generate permutations
            var r = right[0];
            right.RemoveAt(0);
            prefix.Add(r);
            results.AddRange(Combine(left, right, prefix));
            right.Insert(0, r);
            prefix.Remove(prefix.Count - 1);
            return results;
        }
    }
}
