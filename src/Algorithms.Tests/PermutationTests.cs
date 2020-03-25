using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests
{
    public class PermutationTests
    {
        [Fact]
        public void should_generate_permutations()
        {
            // Arrange
            var aExpected = new string[] { "a" };
            var abExpected = new string[] { "ab", "ba" };
            var abcExpected = new string[] { "abc", "acb", "bac", "bca", "cab", "cba" };
            var abcdExpected = new string[] { "dabc", "dacb", "dbac", "dbca", "dcab", "dcba"
                                            , "adbc", "adcb", "bdac", "bdca", "cdab", "cdba"
                                            , "abdc", "acdb", "badc", "bcda", "cadb", "cbda"
                                            , "abcd", "acbd", "bacd", "bcad", "cabd", "cbad"
                                            };

            // Act
            var aResults = GeneratePermutations("a");
            var abResults = GeneratePermutations("ab");
            var abcResults = GeneratePermutations("abc");
            var abcdResults = GeneratePermutations("abcd");

            // Assert
            Assert.Single(aResults);
            Assert.Collection(aResults, x => aExpected.Any(z => z == x));

            Assert.Equal(2, abResults.Count());
            Assert.True(abResults.All(x => abExpected.Any(z => z == x)));

            Assert.Equal(6, abcResults.Count());
            Assert.True(abcResults.All(x => abcExpected.Any(z => z == x)));

            Assert.Equal(24, abcdResults.Count());
            Assert.True(abcdResults.All(x => abcdExpected.Any(z => z == x)));
        }

        private IEnumerable<string> GeneratePermutations(string str)
        {
            var answers = new List<string>();
            GeneratePermutationsHelper(str, 0, str.Length - 1, answers);
            return answers;
        }

        private void GeneratePermutationsHelper(string str, int left, int right, List<string> answers)
        {
            if (left == right)
            {
                answers.Add(str);
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    str = Swap(str, left, i);
                    GeneratePermutationsHelper(str, left + 1, right, answers);

                    // backtracking
                    str = Swap(str, left, i);
                }
            }
            
        }

        private string Swap (string str, int left, int right)
        {
            var chars = str.ToCharArray();
            var temp = chars[left];
            chars[left] = chars[right];
            chars[right] = temp;
            return new string(chars);
        }
    }
}
