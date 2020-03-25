using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _2_CheckPermutation
    {
        [Theory]
        [InlineData("abc", "abc")]
        [InlineData("aabb", "aabb")]
        public void should_yield_true(string src, string tgt)
        {
            // Act & Assert
            Assert.True(CheckPermutation(src, tgt));
        }

        [Theory]
        [InlineData("ab", "abc")]
        [InlineData("abd", "abc")]
        public void should_yield_false(string src, string tgt)
        {
            Assert.False(CheckPermutation(src, tgt));
        }

        private bool CheckPermutation(string src, string tgt)
        {
            if (src.Length != tgt.Length)
            {
                return false;
            }

            var countArray = new int[26];

            for (int i = 0; i < src.Length; i++)
            {
                countArray[(int)src[i] - 'a']++;
                countArray[(int)tgt[i] - 'a']--;
            }
            foreach(var count in countArray)
            {
                if(count != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
