using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _1_RouteBetweenNodes
    {
        /// <summary>
        /// Given a directed graph, design an algorithm to find out whether there is a route between two nodes
        /// </summary>
        public _1_RouteBetweenNodes()
        {
            // Algorithm
            //  Find both verticies in graph, if either not found then no path
            //  If both found then starting from first verttex, dfs to find second vertex
            //  If not found then do same dfs for second vertex
            //  If both can't find each other then no path
        }

        [Theory]
        [InlineData(5, new int[] { 0, 1, 1, 0 }, 2, 0)]
        [InlineData(5, new int[] { }, 2, 0)]
        // 0, 1
        // 1, 0 3
        // 2, 3
        // 3, 4
        // 4, 3
        [InlineData(5, new int[] { 0, 1, 1, 0, 1, 3, 2, 3, 3, 4, 4, 3 }, 2, 0)]
        public void should_not_find_route_dfs(int numberOfVerticies, int[] edges, int from, int to)
        {
            // Arrange
            var graph = GraphHelper.Create<int>(numberOfVerticies, edges);

            // Act
            var result = graph.HasRouteDfs(from, to) || graph.HasRouteDfs(to, from);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(5, new int[] { 0, 1, 1, 2, 2, 3, 3, 4, 4, 0 }, 2, 0)]
        [InlineData(5, new int[] { 0, 2 }, 2, 0)]
        [InlineData(5, new int[] { 2, 0 }, 2, 0)]
        // 0, 1
        // 1, 0 3
        // 2, 3
        // 3, 4
        // 4, 3, 1
        [InlineData(5, new int[] { 0, 1, 1, 0, 1, 3, 2, 3, 3, 4, 4, 3, 4, 1 }, 2, 0)]
        public void should_find_route_dfs(int numberOfVerticies, int[] edges, int from, int to)
        {
            // Arrange
            var graph = GraphHelper.Create<int>(numberOfVerticies, edges);

            // Act
            var result = graph.HasRouteDfs(from, to) || graph.HasRouteDfs(to, from);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(5, new int[] { 0, 1, 1, 0 }, 2, 0)]
        [InlineData(5, new int[] { }, 2, 0)]
        // 0, 1
        // 1, 0 3
        // 2, 3
        // 3, 4
        // 4, 3
        [InlineData(5, new int[] { 0, 1, 1, 0, 1, 3, 2, 3, 3, 4, 4, 3 }, 2, 0)]
        public void should_not_find_route_bfs(int numberOfVerticies, int[] edges, int from, int to)
        {
            // Arrange
            var graph = GraphHelper.Create<int>(numberOfVerticies, edges);

            // Act
            var result = graph.HasRouteBfs(from, to) || graph.HasRouteBfs(to, from);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(5, new int[] { 0, 1, 1, 2, 2, 3, 3, 4, 4, 0 }, 2, 0)]
        [InlineData(5, new int[] { 0, 2 }, 2, 0)]
        [InlineData(5, new int[] { 2, 0 }, 2, 0)]
        // 0, 1
        // 1, 0 3
        // 2, 3
        // 3, 4
        // 4, 3, 1
        [InlineData(5, new int[] { 0, 1, 1, 0, 1, 3, 2, 3, 3, 4, 4, 3, 4, 1 }, 2, 0)]
        [InlineData(5, new int[] { 0, 1, 1, 2, 2, 1, 2, 3, 2, 4}, 2, 0)]
        public void should_find_route_bfs(int numberOfVerticies, int[] edges, int from, int to)
        {
            // Arrange
            var graph = GraphHelper.Create<int>(numberOfVerticies, edges);

            // Act
            var result = graph.HasRouteBfs(from, to) || graph.HasRouteBfs(to, from);

            // Assert
            Assert.True(result);
        }
    }

    public class Graph<T>
    {
        public List<GraphNode<T>> Vertices;
        public Graph(int numberOfVerticies)
        {
            Vertices = new List<GraphNode<T>>();
            var ptr = 0;
            while(ptr < numberOfVerticies)
            {
                Vertices.Add(new GraphNode<T>(ptr++));
            }
        }

        public void AddDirectedEdge(int from, int to)
        {
            ValidateVertexIndex(from, to);

            Vertices[from].AddEdge(Vertices[to]);
        }

        public bool HasRouteDfs(int from, int target)
        {
            ValidateVertexIndex(from, target);
            return HasRouteDfsHelper(from, target, new HashSet<int>());
        }

        private bool HasRouteDfsHelper(int from, int target, HashSet<int> visited)
        {
            if (visited.Contains(from))
            {
                return false;
            }
            visited.Add(from);

            foreach (var edge in Vertices[from].Edges)
            {
                if (HasRouteDfsHelper(edge.Id, target, visited))
                {
                    return true;
                }
            }
            
            return from == target;
        }

        public bool HasRouteBfs(int from, int target)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(from);
            while(queue.Count > 0)
            {
                var vertex = Vertices[queue.Dequeue()];
                if (vertex.Id == target)
                {
                    return true;
                }
                
                visited.Add(vertex.Id);
                foreach(var edge in vertex.Edges)
                {
                    if (!visited.Contains(edge.Id))
                    {
                        queue.Enqueue(edge.Id);
                    }
                }
            }
            return false;
        }

        private void ValidateVertexIndex(params int[] verticies)
        {
            foreach(var vertex in verticies)
            {
                if (vertex >= Vertices.Count)
                {
                    throw new IndexOutOfRangeException("Invalid vertex");
                }
            }
        }
    }

    public class GraphNode<T>
    {
        public int Id;
        public List<GraphNode<T>> Edges;
        public GraphNode(int id)
        {
            Id = id;
            Edges = new List<GraphNode<T>>();
        }

        public void AddEdge(GraphNode<T> edge)
        {
            if (!Edges.Any(x => x.Id == edge.Id))
            {
                Edges.Add(edge);
            }
        }
    }

    public class GraphHelper
    {
        public static Graph<T> Create<T>(int numberOfVerticies, int[] edges)
        {
            var graph = new Graph<T>(numberOfVerticies);
            for (int r = 0; r < edges.GetLength(0); r += 2)
            {
                graph.AddDirectedEdge(edges[r], edges[r + 1]);
            }
            return graph;
        }
    }
}
