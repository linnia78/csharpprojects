using Algorithms.Tests.DataStructures.Node;
using System.Collections.Generic;
using Xunit;

namespace Algorithms.Tests.Fundamentals
{
    public class BredethFirstSearchTest
    {
        [Fact]
        public void should_find_node_using_bredeth_first_search()
        {
            // Arrange
            var root = new TreeNode<int>(1);
            var left = root.AddChild(2);
            left.AddChild(4);
            left.AddChild(5);
            var right = root.AddChild(3);
            right.AddChild(6);
            right.AddChild(7);

            // Act
            var contains3Traversal = new List<int>();
            var contains3 = BreadthFirstSearch(root, 3, contains3Traversal);
            var contains8Traversal = new List<int>();
            var contains8 = BreadthFirstSearch(root, 8, contains8Traversal);

            // Assert
            Assert.True(contains3);
            for (int i = 1; i < 3; i++)
            {
                Assert.Equal(i, contains3Traversal[i - 1]);
            }
            
            Assert.False(contains8);
            for (int i = 1; i < 7; i++)
            {
                Assert.Equal(i, contains8Traversal[i - 1]);
            }
        }

        private bool BreadthFirstSearch<T>(TreeNode<T> tree, T target, List<T> traversal)
        {
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(tree);

            while(queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                traversal.Add(currentNode.Value);

                if (currentNode.Value.Equals(target))
                {
                    return true;
                }
                else if (currentNode.Children != null)
                {
                    foreach(var node in currentNode.Children)
                    {
                        queue.Enqueue(node);
                    }
                }
            }
            return false;
        }
    }
}
