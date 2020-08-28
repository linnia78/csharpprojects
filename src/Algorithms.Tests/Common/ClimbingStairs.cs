using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class ClimbingStairsTest
    {
        /*
            https://leetcode.com/problems/climbing-stairs/
            Each time you can either climb 1 or 2 steps, how many ways can you climb to the top?
        */
        public ClimbingStairsTest()
        {
            // Algorithm
            //  { 1, 2 } steps for s 
            //  if s = 2 then { 1, 1 }, { 2 }
            //  if s = 3 then ( 1, 1, 1 ), { 2, 1 }, { 1, 2 }
            //  if s = 4 then { 1, 1, 1, 1 }, { 2, 2 }, { 1, 1, 2 }, { 1, 2, 1 }, { 2, 1, 1 }
            //  f(s) = f(s - 1) + f(s - 2)
        }

        [Theory]
        [InlineData(1, new int[] { 1 }, 1)]
        [InlineData(1, new int[] { 1, 2, 3, 4, 5 }, 1)]
        [InlineData(5, new int[] { 1 }, 1)]
        [InlineData(3, new int[] { 1, 2 }, 3)]
        [InlineData(4, new int[] { 1, 2 }, 5)]
        [InlineData(5, new int[] { 1, 2 }, 8)]
        [InlineData(4, new int[] { 1, 2, 4 }, 6)]
        public void should_print_all_permutations(int stairCases, int[] steps, int expectedPermutations)
        {
            // Arrange & Act
            var regular = FindPermutations(stairCases, steps);
            var optimized = FindPermutationsOptimized(stairCases, steps, new Dictionary<int, int>());

            // Assert
            Assert.Equal(expectedPermutations, regular);
            Assert.Equal(expectedPermutations, optimized);
        }


        // Test Case : S = 2 { 1, 2 }
        //  Stack     Step 1,  Step 2   Sum
        //    2         f        t       1
        //    1         t                2
        // Test Case : S = 4 { 1, 2 }
        //  Stack     Step 1,  Step 2   Sum
        //    4         f        f       0                     4
        //    3         f        f       0                    / \
        //    2         f        t       1                   3   2
        //    2         f        t       2                  / \   \
        //    1         t                3                 2   1   1
        //    1         t                4                /
        //    1         t                5               1
        private int FindPermutations(int stairCases, int[] steps)
        {
            if (stairCases < 1) { return 0; }
            
            var sum = 0;
            foreach(var step in steps)
            {
                if (stairCases == step)
                {
                    sum += 1;
                }
                sum += FindPermutations(stairCases - step, steps);
            }
            return sum;
        }

        // O(s) complexity where s = stairCases 
        // O(s) space where s = stairCases
        private int FindPermutationsOptimized(int stairCases, int[] steps, Dictionary<int, int> memo)
        {
            if (stairCases < 1) { return 0; }
            else if (memo.ContainsKey(stairCases)) { return memo[stairCases]; }
            
            var sum = 0;
            foreach(var step in steps)
            {
                if (stairCases == step)
                {
                    sum += 1;
                }
                sum += FindPermutations(stairCases - step, steps);
            }
            memo.Add(stairCases, sum);
            return sum;
        }
    }
}