using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class JumpingNumberTest
    {
        /*
            Generate all serise of jumping numbers <= x
            Jumping numbers are any number by themselves or with a difference of 1 with adjacent numbers
            eg. 5 = { 0, 1, 2, 3, 4, 5 }
            101 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 21, 23, 32, 34, 43, 45, 54, 56, 65, 67, 76, 78, 87, 89, 98, 101 }
        */
        public JumpingNumberTest()
        {
            
        }

        [Theory]
        [InlineData(20, new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 } )]
        [InlineData(101, new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 21, 23, 32, 34, 43, 45, 54, 56, 65, 67, 76, 78, 87, 89, 98, 101 })]
        [InlineData(5, new int[] {0, 1, 2, 3, 4, 5 })]
        public void should_generate_jumping_numbers_dfs(int max, int[] expectedJumpingNumbers)
        {
            // Algorithm
            // for every number 1-9, recursively append number less than and greater than itself such that the result is less than x, add to collection
            //  base case is jumping number < x, if yes add to list, continue, if not exit
            //  continue with taking the digit place and recursively callitself by appending +1 and -1 eg. 12 -> 121 and 123 or 9 -> 98 and 90? 0 should be excluded
            
            // zero is a special case?!

            // pseudocode
            // function generate(jumpingNumber, max)
            //  if (jumpingNumber > max) return new List<int>();
            //  generate +1, if 0 then exlcude, else append
            //  generate -1, append 
            //  return new List<int> { jumpingNumber }.Add()

            // Arrange & Act
            var results = GenerateJumpingNumbersDfs(max);

            Assert.All(results, x => expectedJumpingNumbers.Contains(x));
            Assert.Equal(expectedJumpingNumbers.Length, results.Count);
        }

        private List<int> GenerateJumpingNumbersDfs(int max)
        {
            var list = new List<int>() { 0 };
            for (int i = 1; i < 10; i++)
            {
                list.AddRange(GenerateJumpingNumbersDfsHelper(i, max));
            }
            return list;
        }

        private List<int> GenerateJumpingNumbersDfsHelper(int jumpingNumber, int max)
        {
            var list = new List<int>();
            if (jumpingNumber > max) { return list; }
            list.Add(jumpingNumber);
            
            var nextLower = (jumpingNumber % 10) - 1;
            if (nextLower > -1)
            {
                list.AddRange(GenerateJumpingNumbersDfsHelper((jumpingNumber * 10) + nextLower, max));
            }

            var nextGreater = (jumpingNumber % 10) + 1;
            if (nextGreater < 10)
            {
                list.AddRange(GenerateJumpingNumbersDfsHelper((jumpingNumber * 10) + nextGreater, max));
            }

            return list;
        }

        [Theory]
        [InlineData(20, new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 } )]
        [InlineData(101, new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 21, 23, 32, 34, 43, 45, 54, 56, 65, 67, 76, 78, 87, 89, 98, 101 })]
        [InlineData(5, new int[] {0, 1, 2, 3, 4, 5 })]
        public void should_generate_jumping_numbers_bfs(int max, int[] expectedJumpingNumbers)
        {
            // Arrange & Act
            var results = GenerateJumpingNumbersBfs(max);

            Assert.All(results, x => expectedJumpingNumbers.Contains(x));
            Assert.Equal(expectedJumpingNumbers.Length, results.Count);
        }

        private List<int> GenerateJumpingNumbersBfs(int max)
        {
            var results = new List<int>{ 0 };
            var queue = new Queue<int>();
            for (int i = 1; i < 10 && i <= max; i++)
            {
                queue.Enqueue(i);
            }

            while(queue.Any())
            {
                var jumpingNumber = queue.Dequeue();
                if (jumpingNumber <= max)
                {
                    results.Add(jumpingNumber);
                    var digit = jumpingNumber % 10;
                    if (digit < 9)
                    {
                        queue.Enqueue((jumpingNumber * 10) + digit + 1);
                    }
                    if (digit > 0)
                    {
                        queue.Enqueue((jumpingNumber * 10) + digit - 1);
                    }
                }
            }
            return results;
        }
    }
}
