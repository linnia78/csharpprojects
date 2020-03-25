using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.Tests.Cracking.ArrayAndStrings
{
    public class _1_IsUnique
    {
        private ITestOutputHelper _output;
        private int _iterations = 0;
        /// <summary>
        /// Is Unique: Implement an algorithm to determine if a string has all unique characters. 
        /// What if you cannot use additional data structures?
        /// </summary>
        public _1_IsUnique(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Theory]
        [InlineData("abcdefga")]
        [InlineData("aa")]
        public void no_extra_data_structure_true(string str)
        {
            // Act & Assert
            Assert.True(IsUniqueWithoutExtractSpace(str));
            _output.WriteLine($"Iterations : {_iterations}");
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        public void no_extra_data_structure_false(string str)
        {
            // Act & Assert
            Assert.False(IsUniqueWithoutExtractSpace(str));
            _output.WriteLine($"Iterations : {_iterations}");
        }

        private bool IsUniqueWithoutExtractSpace(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                for(int j = i + 1; j < str.Length; j++)
                {
                    _iterations++;
                    if (str[i] == str[j])
                    { 
                        return true; 
                    }
                }
            }
            return false;
        }

        [Theory]
        [InlineData("abcdefga")]
        [InlineData("aa")]
        public void dictionary_true(string str)
        {
            // Act & Assert
            Assert.True(IsUniqueWithDictionary(str));
            _output.WriteLine($"Iterations : {_iterations}");
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        public void dictionary_false(string str)
        {
            // Act & Assert
            Assert.False(IsUniqueWithDictionary(str));
            _output.WriteLine($"Iterations : {_iterations}");
        }

        private bool IsUniqueWithDictionary(string str)
        {
            var chars = new HashSet<char>();
            foreach(var @char in str.ToCharArray())
            {
                _iterations++;
                if (chars.Contains(@char))
                {
                    return true;
                }
                else
                {
                    chars.Add(@char);
                }
            }
            return false;
        }

        [Theory]
        [InlineData("abcdefga")]
        [InlineData("aa")]
        public void bit_manipulation_true(string str)
        {
            // Act & Assert
            Assert.True(IsUniqueWithBitManipulation(str));
            _output.WriteLine($"Iterations : {_iterations}");
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        public void bit_manipulation_false(string str)
        {
            // Act & Assert
            Assert.False(IsUniqueWithBitManipulation(str));
            _output.WriteLine($"Iterations : {_iterations}");
        }

        private bool IsUniqueWithBitManipulation(string str)
        {
            var binaryDataStructure = 0;
            foreach(char c in str)
            {
                _iterations++;
                var position = (int)Math.Pow(2, (int)c - (int)'a');
                if ((binaryDataStructure & position) > 0)
                {
                    return true;
                }
                binaryDataStructure |= position;
            }
            return false;
        }
    }
}
