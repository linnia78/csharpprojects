using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _5_OneAway
    {
        /// <summary>
        /* One Away: 
         * There are three types of edits that can be performed on strings: 
         * insert a character, remove a character, or replace a character. 
         * Given two strings, write a function to check if they are one edit (or zero edits) away. 
         * 
         * EXAMPLE 
            pale, pie -> true 
            pales, pale -> true 
            pale, bale -> true 
            pale, bake -> false 
         */
        /// </summary>
        public _5_OneAway()
        {

        }

        [Theory]
        [InlineData("same", "same")]
        [InlineData("insert", "insezrt")]
        [InlineData("replace", "replacz")]
        [InlineData("removez", "remove")]
        public void should_yield_true(string input, string target)
        {
            // Act & Assert
            Assert.True(IsOneAway(input, target));
        }

        [Theory]
        [InlineData("notsame", "emaston")]
        [InlineData("doubleinsert", "doulbeinsertzz")]
        [InlineData("doublereplace", "douzlezeplace")]
        [InlineData("doulberemove", "doublereve")]
        [InlineData("removeinsert", "removzinser")]
        public void should_yield_false(string input, string target)
        {
            // Act & Assert
            Assert.False(IsOneAway(input, target));
        }

        private bool IsOneAway(string input, string target)
        {
            if (Math.Abs(input.Length - target.Length) > 1)
            {
                return false;
            }

            var longStr = input.Length > target.Length ? input : target;
            var shortStr = input.Length > target.Length ? target : input;
            var isEqualStrLength = longStr.Length == shortStr.Length;

            var hasDifference = false;
            int i = 0, j = 0;

            while(i < shortStr.Length)
            {
                if (longStr[i] != shortStr[j])
                {
                    if (hasDifference)
                    {
                        return false;
                    }
                    hasDifference = true;

                    if (!isEqualStrLength)
                    {
                        i++;
                        if (longStr[i] != shortStr[j])
                        {
                            return false;
                        }
                    }
                }

                i++;
                j++;
            }

            return true;
        }
    }
}
