using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _6_StringCompression
    {
        /// <summary>
        /* String Compression: 
         * Implement a method to perform basic string compression using the counts of repeated characters. For example, 
         * the string aabcccccaaa would become a2blc5a3, 
         * If the "compressed" string would not become smaller than the original string, your method should return the original string. 
         * You can assume the string has only uppercase and lowercase letters (a - z). 
         */
        /// </summary>
        public _6_StringCompression()
        {

        }

        [Theory]
        [InlineData("abc", "abc")]
        [InlineData("aabbcc", "aabbcc")]
        [InlineData("aabbccc", "a2b2c3")]
        public void should_yield_expected(string input, string expected)
        {
            // Act & Assert
            Assert.Equal(expected, CompressString(input));
        }

        public string CompressString(string input)
        {
            var result = new StringBuilder();
            var currentCount = 0;

            for(int i = 0; i < input.Length; i++)
            {
                currentCount++;
                
                if (currentCount.ToString().Length + result.Length + 1 >= input.Length)
                {
                    return input;
                }

                if (i == input.Length - 1 || input[i] != input[i + 1])
                {
                    result.Append($"{input[i]}{currentCount}");
                    currentCount = 0;
                }
            }

            return result.ToString();
        }
    }
}
