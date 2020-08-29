using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class PermutationTest
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


        [Fact]
        public void should_generate_unique_permutations()
        {
            // Arrange
            var aExpected = new string[] { "a" };
            var aaExpected = new string[] { "aa", };
            var aabExpected = new string[] { "aab", "aba", "baa" };
            var aabbExpected = new string[] { "aabb", "abab", "abba", "baba", "bbaa", "baab" };
            var aabbcExpected = new string[] { "baabc", "baacb", "babac", "babca", "bacab", "bacba"
                                                , "bbaac", "bbaca", "bbcaa"
                                                , "bcaab", "bcaba", "bcbaa"
                                                , "ababc", "abacb", "abbac", "abbca", "abcab", "abcba"
                                                , "aabbc", "aabcb", "aacbb"
                                                , "acbba", "acbab", "acabb"
                                                , "caabb", "cbaba", "cbbaa", "cabab", "cabba", "cbaab" };

            // Act
            var aResults = GenerateUniquePermutations("a");
            var aaResults = GenerateUniquePermutations("aa");
            var aabResults = GenerateUniquePermutations("aab");
            var aabbResults = GenerateUniquePermutations("aabb");
            var aabbcResults = GenerateUniquePermutations("aacbb");

            // Assert
            Assert.Single(aResults);
            Assert.Collection(aResults, x => aExpected.Any(z => z == x));

            Assert.Equal(1, aaExpected.Count());
            Assert.True(aaResults.All(x => aaExpected.Any(z => z == x)));

            Assert.Equal(3, aabResults.Count());
            Assert.True(aabResults.All(x => aabExpected.Any(z => z == x)));

            Assert.Equal(6, aabbResults.Count());
            Assert.True(aabbResults.All(x => aabbExpected.Any(z => z == x)));

            Assert.Equal(30, aabbcResults.Count());
            Assert.True(aabbcResults.All(x => aabbcExpected.Any(z => z == x)));
        }

        private List<string> GenerateUniquePermutations(string str)
        {
            var answers = new List<string>();
            GenerateUniquePermutationsHelper(str.ToCharArray(), 0, str.Length - 1, answers);
            return answers;
        }

        private void GenerateUniquePermutationsHelper(char[] chars, int left, int right, List<string> answers)
        {
            if (left == right)
            {
                answers.Add(new string(chars));
            }
            else
            {
                var memo = new HashSet<char>();
                for(int i = left; i <= right; i++)
                {
                    if (!memo.Contains(chars[i]))
                    {
                        Swap(chars, left, i);
                        GenerateUniquePermutationsHelper(chars, left + 1, right, answers);
                        Swap(chars, left, i);
                        memo.Add(chars[i]);
                    }
                }
            }
        }

        private void Swap(char[] chars, int left, int right)
        {
            var temp = chars[left];
            chars[left] = chars[right];
            chars[right] = temp;
        }
    }
}
