using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests
{
    public class PalindromeTest
    {
        [Fact]
        public void should_evaluate_palindrome()
        {
            // Arrange & Act
            var isPalindrome1 = IsPalindrome("level");
            var isPalindrome2 = IsPalindrome("raar");

            var isNotPalindrome1 = IsPalindrome("levll");
            var isNotPalindrome2 = IsPalindrome("rear");

            // Assert
            Assert.True(isPalindrome1);
            Assert.True(isPalindrome2);
            Assert.False(isNotPalindrome1);
            Assert.False(isNotPalindrome2);
        }


        public bool IsPalindrome(string str)
        {
            for(int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] != str[str.Length - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
