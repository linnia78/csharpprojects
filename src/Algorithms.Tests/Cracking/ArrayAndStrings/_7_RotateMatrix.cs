using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _7_RotateMatrix
    {
        /// <summary>
        /// Rotate Matrix: Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes, 
        /// write a method to rotate the image by 90 degrees. 
        /// Can you do this in place? 
        /// </summary>
        public _7_RotateMatrix()
        {

        }

        [Fact]
        public void should_rotate_1x1_matrix()
        {
            // Arrange
            var original = new int[,] { { 1 } };
            var expected = new int[,] { { 1 } };

            // Act
            var result = RotateMatrix(original);

            // Assert
            OutputMatrix(result);
            Assert.True(IsIdentical(expected, result));
        }


        [Fact]
        public void should_rotate_2x2_matrix()
        {
            // Arrange
            var original = new int[,] { { 1, 2 }, { 3, 4 } };
            var expected = new int[,] { { 3, 1 }, { 4, 2 } };

            // Act
            var result = RotateMatrix(original);

            // Assert
            OutputMatrix(result);
            Assert.True(IsIdentical(expected, result));
        }

        [Fact]
        public void should_rotate_3x3_matrix()
        {
            // Arrange
            var original = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var expected = new int[,] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };

            // Act
            var result = RotateMatrix(original);
            
            // Assert
            OutputMatrix(result);
            Assert.True(IsIdentical(expected, result));
        }

        [Fact]
        public void should_rotate_4x4_matrix()
        {
            // Arrange
            var original = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } };
            var expected = new int[,] { { 13, 9, 5, 1 }, { 14, 10, 6, 2 }, { 15, 11, 7, 3 }, { 16, 12, 8, 4 } };

            // Act
            var result = RotateMatrix(original);

            // Assert
            OutputMatrix(result);
            Assert.True(IsIdentical(expected, result));
        }

        private bool IsIdentical(int[,] first, int[,] second)
        {
            for (int r = 0; r < first.GetLength(0); r++)
            {
                for (int c = 0; c < first.GetLength(0); c++)
                {
                    if (first[r, c] != second[r, c])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void OutputMatrix(int[,] matrix)
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(0); c++)
                {
                    System.Diagnostics.Debug.Write(matrix[r, c] + " ");
                }
                System.Diagnostics.Debug.WriteLine(string.Empty);
            }
        }

        private int[,] RotateMatrix(int[,] matrix)
        {
            for (int depth = 0; depth < matrix.GetLength(0) / 2; depth++)
            {
                var end = matrix.GetLength(0) - 1 - depth;
                for (int current = depth; current < end; current++)
                {
                    var offset = current - depth;
                    Swap(matrix, current, end, offset);
                }
            }
            return matrix;
        }

        private void Swap(int[,] matrix, int position, int end, int offset)
        {
            var temp = matrix[position - offset, position];
            matrix[position - offset, position] = matrix[end - offset, position - offset];
            matrix[end - offset, position - offset] = matrix[end, end - offset];
            matrix[end, end - offset] = matrix[position, end];
            matrix[position, end] = temp;
        }

    }
}
