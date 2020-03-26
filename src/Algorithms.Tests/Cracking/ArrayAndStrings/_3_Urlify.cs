using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _3_Urlify
    {
        /// <summary>
        /* URLify: Write a method to replace all spaces in a string with '%20'. 
         * You may assume that the string has sufficient space at the end to hold the additional characters, and that you are given the "true" length of the string. 
         * (Note: If implementing in Java, please use a character array so that you can perform this operation in place.) 
         * 
         * EXAMPLE Input: 
         * "Mr 3ohn Smith 13 
         * 
         * Output: 
         * "Mr%203ohn%20Smith" 
         */
        /// </summary>
        public _3_Urlify()
        {

        }

        [Theory]
        [InlineData("abc de f", "abc%2fde%2ff")]
        [InlineData("a  b", "a%2f%2fb")]
        [InlineData("abcdefg", "abcdefg")]
        public void should_urlify(string input, string expected)
        {
            // Arrange
            var givenTotalLength = expected.Length;
            // Act & Assert
            Assert.Equal(expected, Urlify(input, givenTotalLength));
        }

        private string Urlify(string input, int length)
        {
            var characters = new char[length];
            var cIndex = length - 1;
            for(int i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == ' ')
                {
                    characters[cIndex--] = 'f';
                    characters[cIndex--] = '2';
                    characters[cIndex--] = '%';
                }
                else
                {
                    characters[cIndex--] = input[i];
                }
            }
            return new string(characters);
        }
    }
}
