using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests.Common
{
    public class TraversalDirectedGraphTest
    {
        [Fact]
        public void should_traverse_tree()
        {
            // Arrange
            var a = new GraphNode('A');
            var b = new GraphNode('B');
            var c = new GraphNode('C');
            var d = new GraphNode('D');
            var e = new GraphNode('E');
            var f = new GraphNode('F');
            a.Verticies.Add(b);
            b.Verticies.Add(c);
            c.Verticies.Add(b);
            b.Verticies.Add(d);
            b.Verticies.Add(e);
            e.Verticies.Add(f);
            a.Verticies.Add(f);
            var expectedResults = new List<string>{ "ABD", "AF", "ABEF", "ABCBD", "ABCBEF" };

            // Act
            var results = new List<string>();
            DirectedGraphTraversal(a, new StringBuilder(), results, new Dictionary<char, Visit>());

            // Assert
            Assert.True(results.All(x => expectedResults.Any(y => y == x)));
        }

        [Fact]
        public void should_traverse_tree2()
        {
            // Arrange
            var a = new GraphNode('A');
            var b = new GraphNode('B');
            var c = new GraphNode('C');
            var d = new GraphNode('D');
            var e = new GraphNode('E');
            a.Verticies.Add(b);
            a.Verticies.Add(c);
            a.Verticies.Add(d);
            b.Verticies.Add(e);
            c.Verticies.Add(e);
            d.Verticies.Add(e);
            var expectedResults = new List<string>{ "ABE", "ACE", "ADE" };

            // Act
            var results = new List<string>();
            DirectedGraphTraversal(a, new StringBuilder(), results, new Dictionary<char, Visit>());

            // Assert
            Assert.True(results.All(x => expectedResults.Any(y => y == x)));
        }

        [Fact]
        public void should_traverse_tree3()
        {
            // Arrange
            var a = new GraphNode('A');
            var b = new GraphNode('B');
            var c = new GraphNode('C');
            var d = new GraphNode('D');
            var e = new GraphNode('E');
            var f = new GraphNode('F');
            a.Verticies.Add(b);
            b.Verticies.Add(c);
            c.Verticies.Add(e);
            e.Verticies.Add(f);
            e.Verticies.Add(d);
            d.Verticies.Add(e);
            a.Verticies.Add(d);
            var expectedResults = new List<string>{ "ABE", "ACE", "ADE" };

            // Act
            var results = new List<string>();
            DirectedGraphTraversal(a, new StringBuilder(), results, new Dictionary<char, Visit>());

            // Assert
            Assert.True(results.All(x => expectedResults.Any(y => y == x)));
        }

        private enum Visit
        {
            Visiting,
            Visited,
            None
        }

        private void DirectedGraphTraversal(GraphNode node, StringBuilder output, List<string> results, Dictionary<char, Visit> visitedState)
        {
            if (node == null) { results.Add(string.Empty); }
            else
            {
                if (visitedState.ContainsKey(node.Value)) { visitedState[node.Value] = Visit.Visited; }
                else { visitedState.Add(node.Value, Visit.Visiting); }

                output.Append(node.Value);
                if (node.Verticies.Any())
                {
                    foreach(var n in node.Verticies)
                    {
                        if (!visitedState.ContainsKey(n.Value) || visitedState[n.Value] != Visit.Visited)
                        {
                            DirectedGraphTraversal(n, output, results, visitedState);
                            output.Remove(output.Length - 1, 1);
                        }
                    }
                }
                else
                {
                    results.Add(output.ToString());
                }

                visitedState.Remove(node.Value);
            }
        }

        public class GraphNode
        {
            public char Value;
            public List<GraphNode> Verticies;
            public GraphNode(char value)
            {
                Value = value;
                Verticies = new List<GraphNode>();
            }
        }
    }
}