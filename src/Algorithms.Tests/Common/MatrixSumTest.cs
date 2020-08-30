using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class MatrixSumTest
    {
        /*
            Calculate sum of matrix in constant time
        */

        public MatrixSumTest()
        {

        }

        [Theory]
        [InlineData(3, 3, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1}, new int[] { 0, 0, 0, 0 }, 1, new int[]{ 1, 0, 1, 0 }, 1, new int[]{ 2, 2, 2, 2 }, 1, new int[] { 1, 1, 1, 1 }, 1)]
        [InlineData(3, 3, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1}, new int[] { 0, 0, 1, 1 }, 4, new int[]{ 1, 0, 1, 1 }, 2, new int[]{ 0, 0, 2, 2 }, 9, new int[] { 1, 1, 2, 2 }, 4)]
        [InlineData(3, 3, new int[] { 0, 1, 2, 
                                      0, 1, 2, 
                                      0, 1, 2}, new int[] { 0, 0, 1, 1 }, 2, new int[]{ 1, 0, 1, 1 }, 1, new int[]{ 0, 0, 2, 2 }, 9, new int[] { 1, 1, 2, 2 }, 6)]
        [InlineData(4, 5, new int[] { 1, 1, 1, 1, 1, 
                                      1, 1, 1, 1, 1,
                                      1, 1, 1, 1, 1,
                                      1, 1, 1, 1, 1}, new int[] { 0, 0, 1, 1 }, 4, new int[]{ 1, 0, 1, 1 }, 2, new int[]{ 0, 0, 3, 4 }, 20, new int[] { 1, 1, 3, 3 }, 9)]
        public void should_calculate_matrix_sum(int rows, int cols, int[] matrixValues, int[] point1, int expectedSum1, int[] point2, int expectedSum2, int[] point3, int expectedSum3, int[] point4, int expectedSum4)
        {
            // Algorithm
            //  BuildMatrix should build sum matrix
            //   sum matrix starts from r = 0, c = 0
            //      (r, c) = (r, c) + (r -1, c) if exists + (r, c - 1) if exists - (r - 1, c - 1) if exists
            
            // Arrange
            var matrix = BuildMatrix(rows, cols, matrixValues);
            BuildSumMatrix(matrix);

            // Act
            var sum1 = CalculateSum(matrix, point1.Take(2).ToArray(), point1.Skip(2).ToArray());
            var sum2 = CalculateSum(matrix, point2.Take(2).ToArray(), point2.Skip(2).ToArray());
            var sum3 = CalculateSum(matrix, point3.Take(2).ToArray(), point3.Skip(2).ToArray());
            var sum4 = CalculateSum(matrix, point4.Take(2).ToArray(), point4.Skip(2).ToArray());
            
            // Assert
            Assert.Equal(expectedSum1, sum1);
            Assert.Equal(expectedSum2, sum2);
            Assert.Equal(expectedSum3, sum3);
            Assert.Equal(expectedSum4, sum4);
        }   

        private int CalculateSum(int[,] matrix, int[] topLeft, int[] bottomRight)
        {
            var bottomRow = bottomRight[0];
            var bottomCol = bottomRight[1];
            var topRow = topLeft[0];
            var topCol = topLeft[1];
            var sum = matrix[bottomRow, bottomCol];

            if (topRow - 1 >= 0)
            {
                sum -= matrix[topRow - 1, bottomCol];
            }

            if (topCol - 1 >= 0)
            {
                sum -= matrix[bottomRow, topCol - 1];
            }

            if (topRow - 1 >= 0 && topCol - 1 >= 0)
            {   
                sum += matrix[topRow - 1, topCol - 1];
            }
            return sum;
        }

        private int[,] BuildMatrix(int rows, int cols, int[] matrixValues)
        {
            var matrix = new int[rows, cols];
            for(int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    matrix[r, c] = matrixValues[(r * (rows - 1)) + r + c];
                }
            }
            return matrix;
        }

        private void BuildSumMatrix(int[,] matrix)
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (c - 1 >= 0)
                    {
                        matrix[r, c] += matrix[r, c - 1];
                    }

                    if (r - 1 >= 0)
                    {
                        matrix[r, c] += matrix[r - 1, c];
                    }
                    
                    if (r - 1 >= 0 && c - 1 >= 0)
                    {
                        matrix[r, c] -= matrix[r - 1, c - 1];
                    }
                }
            }
        }
    }
}
