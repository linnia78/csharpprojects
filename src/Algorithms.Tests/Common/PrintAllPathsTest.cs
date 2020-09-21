using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algorithms.Tests.DataStructures.Node;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class PrintAllPathsTest
    {
        private class Graph
        {
            public List<char> Verticies;
            public Dictionary<char, List<char>> Edges;
        }

        [Fact]
        public void should_print_all_path_of_grapth()
        {
            // Arrange
            //   a -> b -> c
            //     \       |
            //       d <-> e -> f
            var graph = new Graph();
            graph.Verticies = new List<char>{ 'a', 'b', 'c', 'd', 'e', 'f' };
            graph.Edges = new Dictionary<char, List<char>>{
                {'a', new List<char>{ 'b', 'd' }},
                {'b', new List<char>{ 'c' }},
                {'c', new List<char>{ 'e'}},
                {'d', new List<char>{ 'e' }},
                {'e', new List<char>{ 'd', 'f' }},
            };
            var expectedList = new List<List<char>> {
                new List<char>{ 'a', 'b', 'c', 'e', 'f' },
                new List<char>{ 'a', 'b', 'c', 'e', 'd', 'e', 'f' },
                new List<char>{ 'a', 'd', 'e', 'f' },
                new List<char>{ 'a', 'd', 'e', 'd', 'e', 'f' }
            };

            // Act
            var allPaths = TraverseGraph(graph, 'a', new List<char>(), new Dictionary<char, VisitState>());

            // Assert
            Assert.Equal(4, allPaths.Count);
            foreach(var path in allPaths)
            {
                Func<List<char>, bool> check = x => {
                    if (x.Count != path.Count) { return false; }
                    for (int i = 0; i < x.Count; i++)
                    {
                        if (x[i] != path[i]) { return false; }
                    }
                    return true;
                };
                var contains = false;
                foreach(var expected in expectedList)
                {
                     contains = check(expected);
                     if (contains)
                     {
                         break;
                     }
                }
                Assert.True(contains);
            }
        }

        // Algorithm, traverse all nodes of graph, when node has no outdegree then it's a leaf
        // Graph may have cycles so keep state of visiting and visited in order to take at least one pass
        public enum VisitState
        {
            Visiting = 0,
            Visited = 1
        }

        private List<List<char>> TraverseGraph(Graph graph, char vertex, List<char> accumulator, Dictionary<char, VisitState> visit)
        {
            var results = new List<List<char>>();
            if (graph == null) { return results; }
            else
            {
                if (visit.ContainsKey(vertex)) { visit[vertex] = VisitState.Visited; }
                else { visit.Add(vertex, VisitState.Visiting); }
                accumulator.Add(vertex);

                if (graph.Edges.ContainsKey(vertex))
                {
                    foreach(var edge in graph.Edges[vertex])
                    {
                        if (!visit.ContainsKey(edge) || visit[edge] != VisitState.Visited)
                        {
                            var response = TraverseGraph(graph, edge, accumulator, visit);
                            accumulator.RemoveAt(accumulator.Count - 1);
                            if (response.Any() && response.First().Any())
                            {
                                results.AddRange(response);
                            }
                        }
                    }
                }
                else
                {
                    results.Add(new List<char>(accumulator));
                }
            }
            visit.Remove(vertex);
            return results;
        }

        [Fact]
        public void should_print_all_path_of_tree()
        {
            // Arrange
            var tree = new TreeNode<char>('a');
            tree.Left = new TreeNode<char>('b');
            tree.Right = new TreeNode<char>('c');
            tree.Left.Left = new TreeNode<char>('d');
            tree.Left.Right = new TreeNode<char>('e');
            
            // Act
            var allPaths = TraverseTree(tree, new List<char>());
            
            // Assert
            Assert.Equal(3, allPaths.Count);
        }

        // Algorithm, traverse all nodes of tree. When leaf is reached add to result

        private List<List<char>> TraverseTree(TreeNode<char> node, List<char> accumulator)
        {
            var results = new List<List<char>>();
            if (node == null) { return results; }

            accumulator.Add(node.Value);
            if (node.Left == null && node.Right == null)
            {
                results.Add(new List<char>(accumulator));
            }
            if (node.Left != null)
            {
                var response = TraverseTree(node.Left, accumulator);
                if (response.Any() && response.First().Any())
                {
                    results.AddRange(response);
                }
                accumulator.RemoveAt(accumulator.Count - 1);
            }
            if (node.Right != null)
            {
                var response = TraverseTree(node.Right, accumulator);
                if (response.Any() && response.First().Any())
                {
                    results.AddRange(response);
                }
                accumulator.RemoveAt(accumulator.Count - 1);
            }
            return results;
        }
        
    }
}
