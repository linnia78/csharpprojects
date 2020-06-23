using System;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class BridgeTests
    {
        /* 
            Pattern     :   
            References  :
                            https://refactoring.guru/design-patterns/
                            https://sourcemaking.com/design_patterns/
            Real World  :   
                            remote control - various devices have a remote control, abstraction is the remote control, implementation is the device
                            shape and color - color is an attribute of shape, rather than defining a ColorShape combination for all possiblity
            Relations   :   
                            very similar to adapater, difference is upfront
                            structure also similar to state, strategy but solving a different problem
        */

        [Fact]
        public void client()
        {
            // Arrange
            var obj1 = new Abstraction(new Implementation1());
            var obj2 = new Abstraction(new Implementation2());

            // Act
            var result1 = start(obj1);
            var result2 = start(obj2);

            // Assert
            Assert.Equal(nameof(Implementation1), result1);
            Assert.Equal(nameof(Implementation2), result2);
        }

        private string start(Abstraction abstraction)
        {
            return abstraction.Feature();
        }

        public class Abstraction
        {
            private IImplementation _implementation;
            public Abstraction(IImplementation implementation)
            {
                _implementation = implementation;
            }

            public string Feature()
            {
                return _implementation.DoThis();
            }
        }

        public interface IImplementation
        {
            string DoThis();
        }

        public class Implementation1 : IImplementation
        {
            public string DoThis()
            {
                return nameof(Implementation1);
            }
        }

        public class Implementation2 : IImplementation
        {
            public string DoThis()
            {
                return nameof(Implementation2);
            }
        }
    }
}
