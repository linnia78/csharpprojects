using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests.Common
{
    public class TraversalTreeNodeTest
    {
        private readonly ITestOutputHelper _output;

        public TraversalTreeNodeTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void should_traverse_tree()
        {
            // Arrange
            var tree = new TreeNode() { Value = 'A' };
            tree.Left = new TreeNode() { Value = 'B' };
            tree.Right = new TreeNode() { Value = 'C' };
            tree.Right.Left = new TreeNode() { Value = 'D' };
            tree.Right.Right = new TreeNode() { Value = 'E' };
            tree.Right.Right.Right = new TreeNode() { Value = 'F' };
            var expectedResults = new List<string> { "AB", "ACD", "ACEF" };
            var results = new List<string>();
            
            // Act
            TraverseTree(tree, results);

            // Assert
            Assert.True(results.All(x => expectedResults.Any(y => y == x)));
        }

        public void TraverseTree(TreeNode node, List<string> results) => TraverseTree(node, new StringBuilder(), results);

        private void TraverseTree(TreeNode node, StringBuilder output, List<string> results)
        {
            if (node == null)
            {
                results.Add(string.Empty);
            }
            else
            {
                output.Append(node.Value);
                if (node.Left != null)
                {
                    TraverseTree(node.Left, output, results);
                    output.Remove(output.Length - 1, 1);
                }
                if (node.Right != null)
                {
                    TraverseTree(node.Right, output, results);
                    output.Remove(output.Length - 1, 1);
                }

                if (node.Left == null && node.Right == null)
                {
                    results.Add(output.ToString());
                }
            }
        }
    }

    public class TreeNode
    {
        public TreeNode Left;
        public TreeNode Right;
        public char Value;
    }
}
