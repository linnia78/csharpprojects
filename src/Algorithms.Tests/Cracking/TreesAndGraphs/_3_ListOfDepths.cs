using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _3_ListOfDepths
    {
        /// <summary>
        /// Given a binary tree, design an algorithm which creates a linked list of all the nodes at each depth
        /// </summary>
        public _3_ListOfDepths()
        {
        
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 7, 7 }, 
            new int[] { 1, 2, 4, 8, 16, 1 })]
        public void should_return_list_of_linked_list(int[] treeValues, int[] expectedDepthCount)
        {
            // Arrange
            var tree = BuildTreeHelper(treeValues);

            // Act
            var results = GenerateLinkedListAtEachDepthIteratively(tree);
            var results2 = GenerateLinkedListAtEachDepthRecursively(tree);

            // Assert
            foreach(var data in expectedDepthCount.Zip(results).Zip(results2))
            {
                Assert.Equal(data.First.First, data.First.Second.Count);
                Assert.Equal(data.First.First, data.Second.Count);
            }
        }

        private TreeNode<int> BuildTreeHelper(int[] values)
        {
             var root = new TreeNode<int>(values[0]);
             var queue = new Queue<TreeNode<int>>();
             queue.Enqueue(root);
             for (int i = 1; i < values.Length; i += 2)
             {
                 var node = queue.Dequeue();
                node.Left = new TreeNode<int>(values[i]);
                queue.Enqueue(node.Left);
                if (i + 1 < values.Length - 1)
                {
                    node.Right = new TreeNode<int>(values[i+1]);
                    queue.Enqueue(node.Right);
                }
             }
             return root;
        }

        private List<LinkedList<int>> GenerateLinkedListAtEachDepthIteratively(TreeNode<int> node)
        {
            var results = new List<LinkedList<int>>();
            var currentQueue = new Queue<TreeNode<int>>();
            var nextQueue = new Queue<TreeNode<int>>();
            nextQueue.Enqueue(node);
            var depth = 0;
            while(nextQueue.Any())
            {
                results.Add(new LinkedList<int>());
                currentQueue = nextQueue;
                nextQueue = new Queue<TreeNode<int>>();
                while(currentQueue.Any())
                {
                    var currentNode = currentQueue.Dequeue();
                    results[depth].AddFirst(currentNode.Value);

                    if (currentNode.Left != null)
                    {
                        nextQueue.Enqueue(currentNode.Left);
                    }
                    if (currentNode.Right != null)
                    {
                        nextQueue.Enqueue(currentNode.Right);
                    }
                }
                depth++;
            }
            return results;
        }

        private List<LinkedList<int>> GenerateLinkedListAtEachDepthRecursively(TreeNode<int> node)
        {
            var results = new List<LinkedList<int>>();
            GenerateLinkedListAtEachDepthHelper(node, 0, results);
            return results;
        }

        private void GenerateLinkedListAtEachDepthHelper(TreeNode<int> node, int depth, List<LinkedList<int>> results)
        {
            if (node != null)
            {
                if (results.Count < depth + 1)
                {
                    results.Add(new LinkedList<int>());
                }
                GenerateLinkedListAtEachDepthHelper(node.Left, depth + 1, results);
                results[depth].AddFirst(node.Value);
                GenerateLinkedListAtEachDepthHelper(node.Right, depth + 1, results);
            }
        }
    }
}
