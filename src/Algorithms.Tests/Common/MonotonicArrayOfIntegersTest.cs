using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class MonotonicArrayOfIntegersTest
    {
        /*
            Question : 
            Given an array of integers, we would like to determine whether the array is monotonic (non-decreasing/non-increasing) or not.
            Examples:
            // 1 2 5 5 8
            // true
            // 9 4 4 2 2
            // true
            // 1 4 6 3
            // false
        */

        public MonotonicArrayOfIntegersTest()
        {
            // Algorithm
            //  Iterate through entire array from left to right
            //    1st we need to find out whether it's increasing or decreasing
            //      once we find increase/decrease then continue increasing/decreasing check 
        }

        [Theory]
        [InlineData(new int[] { 1 }, true)]
        [InlineData(new int[] { 1, 2 }, true)]
        [InlineData(new int[] { 1, 2, 5, 5, 8 }, true)]
        [InlineData(new int[] { 9, 4, 4, 2, 2 }, true)]
        [InlineData(new int[] { 1, 1, 1, 1, 1 }, true)]
        [InlineData(new int[] { 1, 1, 1, 1, 0 }, true)]
        [InlineData(new int[] { 1, 4, 6, 3 }, false)]
        [InlineData(new int[] { 1, 1, 1, 2, 1 }, false)]
        public void should_check_monotonic(int[] array, bool expectedResult)
        {
            // Arrange & Act
            var result = IsMonotonicSimple(array);
            
            // Assert
            Assert.Equal(expectedResult, result);
        }

        public bool IsMonotonicSimple(int[] array)
        {
            bool increasing = true;
            bool decreasing = true;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i+1])
                {
                    increasing = false;
                }
                else if (array[i] < array[i+1])
                {
                    decreasing = false;
                }
            }
            return increasing || decreasing;
        }

        public bool IsMonotonic(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return false;
            } 
            else if (array.Length == 1)
            {
                return true;
            }
            var previous = array[0];
            for (int i = 1; i < array.Length - 1; i++)
            {
                if (previous > array[i])
                {
                    // decreasing
                    return DecreasingCheck(array[i], array.Skip(i + 1).ToArray());
                }
                else if (previous < array[i])
                {
                    // increasing
                    return IncreasingCheck(array[i], array.Skip(i + 1).ToArray());
                }
                previous = array[i];
            }
            // equivalent
            return true;
        }

        private bool IncreasingCheck(int previous, int[] array)
        {
            foreach(var number in array)
            {
                if (previous > number)
                {
                    return false;
                }
                previous = number;
            }
            return true;
        }
        
        private bool DecreasingCheck(int previous, int[] array)
        {
            foreach(var number in array)
            {
                if (previous < number)
                {
                    return false;
                }
                previous = number;
            }
            return true;
        }
    }
}
