using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Algorithms.Tests.DataStructures.Node;

namespace Algorithms.Tests.Cracking.TreesAndGraphs
{
    public class _7_BuildOrder
    {
        /// <summary>
        /// Given a list of dependencies where 2nd project depends on first, determine a possible build order
        /// eg. projects: a, b, c, d, e, f
        //      dependencies: (a, d) (f, b) (b, d) (f, a) (d, c)
        //      output: f, e, a, b, d, c
        /// </summary>
        public _7_BuildOrder()
        {
            // Algorithm
            //   Iterate from a to f
            //      build all of A's dependencies
            //      keep track of projects built
            //      continue until end of projects

            //   a -> f
            //   a -> b
            //   b -> f
            //   c -> d
            //   d -> b
            //   e
            //   f
        }

        public class DependencyGraph
        {
            public List<char> Projects;
            public Dictionary<char, List<char>> Dependencies;
            public DependencyGraph()
            {
                Projects = new List<char>();
                Dependencies = new Dictionary<char, List<char>>();
            }


        }

        [Fact]
        public void should_determine_build_order()
        {
            // Arrange
            var graph = new DependencyGraph();
            graph.Projects = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f' };
            graph.Dependencies = new Dictionary<char, List<char>>{ 
                { 'a', new List<char> { 'f', 'b' }},
                { 'b', new List<char> { 'f' }},
                { 'c', new List<char> { 'd' }},
                { 'd', new List<char> { 'b' }}
            };

            // Act
            var buildOrder = GetBuildOrderDFS(graph);

            // Assert
            Assert.Equal(6, buildOrder.Count);
            Assert.True(graph.Projects.All(x => buildOrder.Any(y => y == x)));
        }

        [Fact]
        public void should_detect_circular_reference()
        {
            // Arrange
            var graph = new DependencyGraph();
            graph.Projects = new List<char> { 'a', 'b', 'c' };
            graph.Dependencies = new Dictionary<char, List<char>>{ 
                { 'a', new List<char> { 'c', 'b' }},
                { 'b', new List<char> { 'c' }},
                { 'c', new List<char> { 'b' }},
            };

            // Act
            var buildOrder = GetBuildOrderDFS(graph);

            // Assert
            Assert.Null(buildOrder);
        }

        public enum BuildState
        {
            Built = 1,
            Building = 2
        }

        private List<char> GetBuildOrderDFS(DependencyGraph graph)
        {
            if (graph == null) { return null; }
            var buildStates = new Dictionary<char, BuildState>();
            var buildOrder = new List<char>();
            foreach(var project in graph.Projects)
            {
                if (!GetBuildOrderDFS(graph, project, buildOrder, buildStates))
                {
                    return null;
                }
            }
            return buildOrder;
        }
        private bool GetBuildOrderDFS(DependencyGraph graph, char project, List<char> buildOrder, Dictionary<char, BuildState> buildStates)
        {
            if (graph == null) { return true; }
            if (buildStates.ContainsKey(project)) 
            { 
                if (buildStates[project] == BuildState.Building)
                {
                    return false;
                }
                return true;
            }
            buildStates.Add(project, BuildState.Building);

            if (graph.Dependencies.ContainsKey(project))
            {
                foreach(var dependency in graph.Dependencies[project])
                {
                    if (!GetBuildOrderDFS(graph, dependency, buildOrder, buildStates)) { return false; }
                }
            }
            
            buildStates[project] = BuildState.Built;
            buildOrder.Add(project);

            return true;
        }
    }
}
