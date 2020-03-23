using Algorithms.Tests.DataStructures.Node;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms.Tests.Fundamentals
{
    public class DepthFirstSearchTest
    {
        private TreeNode<int> _tree;
        public DepthFirstSearchTest()
        {
            _tree = new TreeNode<int>(1);
            var left = _tree.AddChild(2);
            left.AddChild(4);
            left.AddChild(5);
            var right = _tree.AddChild(3);
            right.AddChild(6);
            right.AddChild(7);
        }

        [Fact]
        public void should_find_node_using_depth_first_search_in_order()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 4, 2, 5, 1, 6, 3 };
            var contains8ExpectedTraversal = new List<int> { 4, 2, 5, 1, 6, 3, 7 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchInOrder(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchInOrder(_tree, 8, contains8Traversal);

            // Assert
            Assert.True(contains3);
            for (int i = 0; i < contains3ExpectedTraversal.Count; i++)
            {
                Assert.Equal(contains3ExpectedTraversal[i], contains3Traversal[i]);
            }

            Assert.False(contains8);
            for (int i = 0; i < contains8ExpectedTraversal.Count; i++)
            {
                Assert.Equal(contains8ExpectedTraversal[i], contains8Traversal[i]);
            }
        }

        /// <summary>
        /// Left Root Right
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="target"></param>
        /// <param name="traversal"></param>
        /// <returns></returns>
        private bool DepthFirstSearchInOrder<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            if (node.Children != null)
            {
                if (DepthFirstSearchInOrder(node.Children[0], target, traversal))
                {
                    return true;
                }
            }

            traversal.Add(node.Value);
            if (node.Value.Equals(target))
            {
                return true;
            }

            if (node.Children != null)
            {
                if (DepthFirstSearchInOrder(node.Children[1], target, traversal))
                {
                    return true;
                }
            }

            return false;
        }

        [Fact]
        public void should_find_node_using_depth_first_search_pre_order()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 1, 2, 4, 5, 3 };
            var contains8ExpectedTraversal = new List<int> { 1, 2, 4, 5, 3, 6, 7 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchPreOrder(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchPreOrder(_tree, 8, contains8Traversal);

            // Assert
            Assert.True(contains3);
            for (int i = 0; i < contains3ExpectedTraversal.Count; i++)
            {
                Assert.Equal(contains3ExpectedTraversal[i], contains3Traversal[i]);
            }

            Assert.False(contains8);
            for (int i = 0; i < contains8ExpectedTraversal.Count; i++)
            {
                Assert.Equal(contains8ExpectedTraversal[i], contains8Traversal[i]);
            }
        }

        /// <summary>
        /// Root Left Right
        /// </summary>
        /// <returns></returns>
        private bool DepthFirstSearchPreOrder<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            traversal.Add(node.Value);
            if (node.Value.Equals(target))
            {
                return true;
            }

            if (node.Children != null)
            {
                if (DepthFirstSearchPreOrder(node.Children[0], target, traversal))
                {
                    return true;
                }
            }

            if (node.Children != null)
            {
                if (DepthFirstSearchPreOrder(node.Children[1], target, traversal))
                {
                    return true;
                }
            }

            return false;
        }

        [Fact]
        public void should_find_node_using_depth_first_search_post_order()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 4, 5, 2, 6, 7, 3 };
            var contains8ExpectedTraversal = new List<int> { 4, 5, 2, 6, 7, 3, 1 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchPostOrder(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchPostOrder(_tree, 8, contains8Traversal);

            // Assert
            Assert.True(contains3);
            for (int i = 0; i < contains3ExpectedTraversal.Count; i++)
            {
                Assert.Equal(contains3ExpectedTraversal[i], contains3Traversal[i]);
            }

            Assert.False(contains8);
            for (int i = 0; i < contains8ExpectedTraversal.Count; i++)
            {
                Assert.Equal(contains8ExpectedTraversal[i], contains8Traversal[i]);
            }
        }

        /// <summary>
        /// Left Right Root
        /// </summary>
        /// <returns></returns>
        private bool DepthFirstSearchPostOrder<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            if (node.Children != null)
            {
                if (DepthFirstSearchPostOrder(node.Children[0], target, traversal))
                {
                    return true;
                }
            }

            if (node.Children != null)
            {
                if (DepthFirstSearchPostOrder(node.Children[1], target, traversal))
                {
                    return true;
                }
            }

            traversal.Add(node.Value);
            if (node.Value.Equals(target))
            {
                return true;
            }

            return false;
        }
    }
}
