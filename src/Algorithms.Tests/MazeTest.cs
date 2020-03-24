using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests
{
    public class MazeTest
    {
        private int _iterations;
        public MazeTest()
        {
            _iterations = 0;
        }

        [Fact]
        public void should_find_path_to_cell()
        {
            // Arrange
            /* 
               *1  1  1
                0  0  1
                0  0  1*
            */
            var row = 3;
            var column = 3;
            var start = new Cell { Row = 0, Column = 0 };
            var target = new Cell { Row = 2, Column = 2 };
            bool[,] maze = new bool[row, column];
            maze[start.Row, start.Column] = true;
            maze[0, 1] = true;
            maze[0, 2] = true;
            maze[1, 2] = true;
            maze[target.Row, target.Column] = true;

            var visited = new bool?[row, column];
            // Act
            var hasPath = HasPath(maze, start, target, visited);

            // Assert
            Assert.True(hasPath);
            System.Diagnostics.Debug.WriteLine("Iterations : " + _iterations);
            PrintPath(visited);
        }

        [Fact]
        public void should_not_find_path_to_cell()
        {
            // Arrange
            /* 
               *1  0  1
                1  0  1
                1  0  1*
            */
            var row = 3;
            var column = 3;
            var start = new Cell { Row = 0, Column = 0 };
            var target = new Cell { Row = 2, Column = 2 };
            bool[,] maze = new bool[row, column];
            maze[start.Row, start.Column] = true;
            maze[1, 0] = true;
            maze[2, 0] = true;
            maze[0, 2] = true;
            maze[1, 2] = true;
            maze[target.Row, target.Column] = true;

            var visited = new bool?[row, column];
            // Act
            var hasPath = HasPath(maze, start, target, visited);

            // Assert
            Assert.False(hasPath);
            System.Diagnostics.Debug.WriteLine("Iterations : " + _iterations);
            PrintPath(visited);
        }

        [Fact]
        public void should_find_path_even_when_potential_loop_exists()
        {
            // Arrange
            /* 
                1 *1  0  1
                1  1  1  1
                1  1  0  1
                1  0  1* 1
            */
            var row = 4;
            var column = 4;
            var start = new Cell { Row = 0, Column = 1 };
            var target = new Cell { Row = 3, Column = 2 };
            bool[,] maze = new bool[row, column];
            maze[start.Row, start.Column] = true;
            maze[0, 0] = true;
            maze[0, 3] = true;
            maze[1, 0] = true;
            maze[1, 1] = true;
            maze[1, 2] = true;
            maze[1, 3] = true;
            maze[2, 0] = true;
            maze[2, 1] = true;
            maze[2, 3] = true;
            maze[3, 0] = true;
            maze[3, 3] = true;
            maze[target.Row, target.Column] = true;

            var visited = new bool?[row, column];
            // Act
            var hasPath = HasPath(maze, start, target, visited);

            // Assert
            Assert.True(hasPath);
            System.Diagnostics.Debug.WriteLine("Iterations : " + _iterations);
            PrintPath(visited);
        }

        private void PrintPath(bool?[,] visited)
        {
            for (int row = 0; row < visited.GetLength(0); row++)
            {
                for (int column = 0; column < visited.GetLength(1); column ++)
                {
                    System.Diagnostics.Debug.Write(
                        (
                            visited[row, column] == null
                                ? " "
                                : visited[row, column] == true
                                    ? "1" : "0") + " ");
                }
                System.Diagnostics.Debug.WriteLine(string.Empty);
            }
        }

        private bool HasPath(bool[,] maze, Cell visiting, Cell target, bool?[,] visited)
        {
            _iterations++;
            if (visiting.Row < 0 || visiting.Row >= maze.GetLength(0) || visiting.Column < 0 || visiting.Column >= maze.GetLength(1))
            {
                return false;
            }
            if (visited[visiting.Row, visiting.Column].HasValue)
            {
                return false;
            }

            visited[visiting.Row, visiting.Column] = maze[visiting.Row, visiting.Column];
            if (visiting.Row == target.Row && visiting.Column == target.Column)
            {
                return true;
            }

            if (maze[visiting.Row, visiting.Column])
            {
                // up = visiting.Column - 1
                return HasPath(maze, new Cell { Row = visiting.Row, Column = visiting.Column - 1 }, target, visited)

                // down = visiting.Column + 1
                || HasPath(maze, new Cell { Row = visiting.Row, Column = visiting.Column + 1 }, target, visited)

                // left = visiting.Row - 1
                || HasPath(maze, new Cell { Row = visiting.Row - 1, Column = visiting.Column }, target, visited)

                // right = visiting.Row + 1
                || HasPath(maze, new Cell { Row = visiting.Row + 1, Column = visiting.Column }, target, visited);
            }
            return false;
        }
    }

    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
