﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _4_PalindromePermutation
    {
        /// <summary>
        /* Palindrome Permutation: Given a string, write a function to check if it is a permutation of a palindrome. 
         * A palindrome is a word or phrase that is the same forwards and backwards. 
         * A permutation is a rearrangement of letters. 
         * The palindrome does not need to be limited to just dictionary words. 
         * 
         * EXAMPLE 
         * Input: Tact Coa 
         * Output: True (permutations: "taco cat", "atco eta", etc.)
         */
        /// </summary>
        public _4_PalindromePermutation()
        {

        }

        [Theory]
        [InlineData("abaccaab")]
        [InlineData("a b c b a")]
        [InlineData("a")]
        public void should_yield_true(string input)
        {
            // Act & Assert
            Assert.True(IsPalindromePermutation(input));
        }

        [Theory]
        [InlineData("abccca")]
        [InlineData("abc baa")]
        [InlineData("abc")]
        public void should_yield_false(string input)
        {
            // Act & Assert
            Assert.False(IsPalindromePermutation(input));
        }

        private bool IsPalindromePermutation(string input)
        {
            var countArray = new int[26];
            var oddCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != ' ')
                {
                    var charIndex = (int)input[i] - 'a';
                    countArray[charIndex]++;
                    if (countArray[charIndex] % 2 == 1)
                    {
                        oddCount++;
                    }
                    else
                    {
                        oddCount--;
                    }
                }
            }

            return oddCount <= 1;
        }
    }
}
