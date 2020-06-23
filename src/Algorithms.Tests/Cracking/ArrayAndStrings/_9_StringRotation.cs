using System;
using Xunit;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _9_StringRotation
    {
        /// <summary>
        /// String Rotation;
        /// Assume you have a method isSubstring which checks if one word is a substring of another.
        /// Given two strings, si and s2, write code to check if s2 is a rotation of si using only one call to isSubstring
        /// [e.g., "waterbottle " is a rotation of 'erbottlewat"), 
        /// </summary>
        public _9_StringRotation()
        {
        }

        [Theory]
        [InlineData("waterbottle", "erbottlewat")]
        [InlineData("aaabbcc", "abbccaa")]
        [InlineData("abc", "abc")]
        [InlineData("abcd", "dabc")]
        public void should_yield_true(string initial, string rotated)
        {
            // Act & Assert
            Assert.True(IsSubstring(initial, rotated));
        }

        [Theory]
        [InlineData("waterbottle", "rebottlewat")]
        [InlineData("aaabbcc", "aabbbcc")]
        [InlineData("a", "b")]
        [InlineData("a", "ab")]
        public void should_yield_false(string initial, string rotated)
        {
            // Act & Assert
            Assert.False(IsSubstring(initial, rotated));
        }

        private bool IsSubstring(string initial, string rotated)
        {
            if (initial.Length != rotated.Length)
            {
                return false;
            }

            // find position in rotated string that matches first character in initial
            for (int i = 0; i < initial.Length; i++)
            {
                if (initial[0] == rotated[i])
                {
                    if (IsRotated(initial, rotated, i))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsRotated(string initial, string rotated, int index)
        {
            for(int i = 0; i < initial.Length; i++)
            {
                if (initial[i] == rotated[index])
                {
                    index = MoveIndex(initial.Length, index);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private int MoveIndex(int maxLength, int current)
        {
            if (current == maxLength - 1)
            {
                return 0;
            }
            return ++current;
        }
    }
}
