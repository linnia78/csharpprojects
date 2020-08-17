using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Fundamentals
{
    public class BitManipulationTest
    {
        [Fact]
        public void should_evaluate_even()
        {
            // Arrange
            var even = 100;

            // Act
            var isOdd = (even & 1) == 1;
            var isEven = (even & 1) == 0;

            // Assert
            Assert.False(isOdd);
            Assert.True(isEven);
        }

        [Fact]
        public void should_evaluate_odd()
        {
            // Arrange
            var odd = 101;

            // Act
            var isOdd = (odd & 1) == 1;
            var isEven = (odd & 1) == 0;

            // Assert
            Assert.True(isOdd);
            Assert.False(isEven);
        }

        [Fact]
        public void should_compliment_negative_value()
        {
            // Arrange
            var negative37 = -37;

            // Act
            var positive36 = ~negative37;

            // Assert
            Assert.Equal(36, positive36);
        }

        [Fact]
        public void should_evaluate_for_power_of_2()
        {
            // Arrange
            var isPowerOfTwo = (Func<int, bool>)((int z) => z != 0 && (z & z - 1) == 0);

            // Assert
            Assert.True(isPowerOfTwo(2));
            Assert.True(isPowerOfTwo(256));
            Assert.False(isPowerOfTwo(3));
        }

        [Fact]
        public void should_use_variable_as_character_storage()
        {
            // Arrange
            var a = 'a' - 97;
            var bitRepresentationOfA = 1 << a;
            var dataStructure = 0;

            // Act
            dataStructure |= bitRepresentationOfA;

            Assert.Equal(1, dataStructure);
        }
    }
}
