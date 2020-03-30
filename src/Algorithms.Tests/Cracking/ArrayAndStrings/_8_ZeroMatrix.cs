using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _8_ZeroMatrix
    {
        /// <summary>
        /// Zero Matrix: 
        /// Write an algorithm such that if an element in an MxN matrix is 0, 
        /// its entire row and column are set to 0. 
        /// </summary>
        public _8_ZeroMatrix()
        {

        }

        [Fact]
        public void should_set_zero_matrix()
        {
            // Arrage
            var _1x1 = new int[,] { { 0 } };
            var _2x2 = new int[,] { { 1, 2 }, { 3, 0 } };
            var _3x3 = new int[,] { { 0, 2, 0 }, { 4, 5, 6 }, { 0, 8, 0 } };
            var _4x4 = new int[,] { { 0, 2, 0, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } };

            // Act
            var _1x1Result = SetZeroMatrix(_1x1);
            var _2x2Result = SetZeroMatrix(_2x2);
            var _3x3Result = SetZeroMatrix(_3x3);
            var _4x4Result = SetZeroMatrix(_4x4);

            // Assert
            Assert.True(IsIdentical(_1x1Result, new int[,] { { 0 } }));
            Assert.True(IsIdentical(_2x2Result, new int[,] { { 1, 0 }, { 0, 0 } }));
            Assert.True(IsIdentical(_3x3Result, new int[,] { { 0, 0, 0 }, { 0, 5, 0 }, { 0, 0, 0 } }));
            Assert.True(IsIdentical(_4x4Result, new int[,] { { 0, 0, 0, 0 }, { 0, 6, 0, 8 }, { 0, 10, 0, 12 }, { 0, 14, 0, 16 } }));

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

        private (HashSet<int> zeroRows, HashSet<int> zeroCols) FindZeroes(int[,] matrix)
        {
            var zeroCols = new HashSet<int>();
            var zeroRows = new HashSet<int>();
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == 0)
                    {
                        zeroRows.Add(r);
                        zeroCols.Add(c);
                    }
                }
            }
            return (zeroRows, zeroCols);
        }

        private int[,] SetZeroMatrix(int[,] matrix)
        {
            var (zeroRows, zeroCols) = FindZeroes(matrix);
            if (zeroRows.Any() || zeroCols.Any())
            {
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (zeroRows.Contains(r) || zeroCols.Contains(c))
                        {
                            matrix[r, c] = 0;
                        }
                    }
                }
            }
            return matrix;
        }
    }
}
