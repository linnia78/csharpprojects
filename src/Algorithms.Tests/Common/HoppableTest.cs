using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class HoppableTest
    {
        /*
            Determine if array is hoppable from start to end
            eg. [4, 0, 2, 1, 2] from start you can hop 4 to last index then 2 more outside of array
        */
        public HoppableTest()
        {
            
        }

        [Theory]
        [InlineData(new int[] { 4, 0, 2, 0, 1 }, true)]
        [InlineData(new int[] { 1, 1, 1, 1, 1 }, true)]
        [InlineData(new int[] { 1, 2, 2, 0, 1 }, true)]
        [InlineData(new int[] { 9, 2, 2, 0, 1 }, true)]
        [InlineData(new int[] { 1, 1, 1, 1, 0 }, false)]
        [InlineData(new int[] { 0, 0, 0, 0, 0 }, false)]
        public void should_determine_if_hoppable(int[] array, bool expectedOutput)
        {
            // Arrange & Act
            var result = IsHoppable(array);

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        private bool IsHoppable(int[] array)
        {
            var currIndex = 0;
            if (array[currIndex] >= array.Length)
            {
                return true;
            }

            while (array[currIndex] > 0)
            {
                var hops = array[currIndex];
                var maxPos = currIndex + 1;
                
                for (int i = 1; i <= hops; i++)
                {
                    // check if current position will yield end result
                    if (array[currIndex + i] + currIndex + i >= array.Length)
                    {
                        return true;
                    }

                    // save next max
                    if (array[maxPos] <= array[currIndex + i])
                    {
                        maxPos = currIndex + i;
                    }
                }
                currIndex = maxPos;
            }
            return false;
        }
    }
}