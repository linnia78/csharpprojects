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

        /// DFS In Order Recursive : Left Right Parent
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
        public void should_find_node_using_depth_first_search_in_order_iteratively()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 4, 2, 5, 1, 6, 3 };
            var contains8ExpectedTraversal = new List<int> { 4, 2, 5, 1, 6, 3, 7 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchInOrderIteratively(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchInOrderIteratively(_tree, 8, contains8Traversal);

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

        /// DFS In Order Iterative : Left Parent Right
        public bool DepthFirstSearchInOrderIteratively<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            var stack = new Stack<TreeNode<T>>();
            var current = node;
            while (current != null || stack.Any())
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                else
                {
                    current = stack.Pop();
                    traversal.Add(current.Value);
                    if (current.Value.Equals(target))
                    {
                        return true;
                    }
                    current = current.Right;
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

        /// DFS Pre Order Recursive : Parent Left Right 
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
        public void should_find_node_using_depth_first_search_pre_order_iteratively()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 1, 2, 4, 5, 3 };
            var contains8ExpectedTraversal = new List<int> { 1, 2, 4, 5, 3, 6, 7 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchPreOrderIteratively(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchPreOrderIteratively(_tree, 8, contains8Traversal);

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

        /// DFS Pre Order Iterative : Parent Left Right 
        public bool DepthFirstSearchPreOrderIteratively<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            var stack = new Stack<TreeNode<T>>();
            var current = node;
            while (current != null || stack.Any())
            {
                if (current != null)
                {
                    traversal.Add(current.Value);
                    if (current.Value.Equals(target))
                    {
                        return true;
                    }
                    stack.Push(current);
                    current = current.Left;
                }
                else
                {
                    current = stack.Pop();
                    current = current.Right;
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

        /// DFS Post Order Recursive : Left Right Parent
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

        
        [Fact]
        public void should_find_node_using_depth_first_search_post_order_iteratively1()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 4, 5, 2, 6, 7, 3 };
            var contains8ExpectedTraversal = new List<int> { 4, 5, 2, 6, 7, 3, 1 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchPostOrderIteratively1(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchPostOrderIteratively1(_tree, 8, contains8Traversal);

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

        /// DFS Post Order Iterative : Left Right Parent
        // Two approaches : 
        // 1 - two stacks where stack #2 stores PRL in reverse order
        // 2 - add right to stack, if current.right in stack then add current to stack then right
        private bool DepthFirstSearchPostOrderIteratively1<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            var stack = new Stack<TreeNode<T>>();
            var stack2 = new Stack<TreeNode<T>>();
            var current = node;
            while(current != null || stack.Any())
            {
                if (current != null)
                {
                    stack.Push(current);
                    stack2.Push(current);
                    current = current.Right;
                }
                else
                {
                    current = stack.Pop();
                    current = current.Left;
                }
            }

            while(stack2.Any())
            {
                var current2 = stack2.Pop();
                traversal.Add(current2.Value);
                if (current2.Value.Equals(target))
                {
                    return true;
                }
            }

            return false;
        }

        [Fact]
        public void should_find_node_using_depth_first_search_post_order_iteratively2()
        {
            // Arrange
            var contains3ExpectedTraversal = new List<int> { 4, 5, 2, 6, 7, 3 };
            var contains8ExpectedTraversal = new List<int> { 4, 5, 2, 6, 7, 3, 1 };

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = DepthFirstSearchPostOrderIteratively2(_tree, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = DepthFirstSearchPostOrderIteratively2(_tree, 8, contains8Traversal);

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

        /// DFS Post Order Iterative : Left Right Parent
        // Two approaches : 
        // 1 - two stacks where stack #2 stores PRL in reverse order
        // 2 - add right to stack, if current.right in stack then add current to stack then right
        private bool DepthFirstSearchPostOrderIteratively2<T>(TreeNode<T> node, T target, List<T> traversal)
        {
            var stack = new Stack<TreeNode<T>>();
            var current = node;
            while(current != null || stack.Any())
            {
                if (current != null)
                {
                    if (current.Right != null)
                    {
                        stack.Push(current.Right);
                    }
                    stack.Push(current);
                    current = current.Left;
                }
                else
                {
                    current = stack.Pop();
                    if (stack.Any() && current.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(current);
                        current = current.Right;
                    }
                    else
                    {
                        traversal.Add(current.Value);
                        if (current.Value.Equals(target))
                        {
                            return true;
                        }
                        current = null;
                    }
                }
            }

            return false;
        }
    }
}
