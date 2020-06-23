using System;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class AdapterTests
    {
        /* 
            Pattern     :   Adapter
            References  :
                            https://refactoring.guru/design-patterns/adapter
                            https://sourcemaking.com/design_patterns/adapter
            Real World  :   
                            plug and outlet adapater - plug and outlet are not changeable so a intermediate adapater is required
            Relations   :   
                            very similar to bridge, difference is adapater attempts to adapt two incompatible classes
                            structure also similar to state, strategy but solving a different problem
        */

        [Fact]
        public void client()
        {
            // Arrange
            var obj = new Adapter(new Adaptee()); // Adaptee is wrapped in adapater because client method for start() requires function of DoThis() where adaptee do not have

            // Act
            var result = start(obj);

            // Assert
            Assert.Equal("that", result);
        }

        private string start(ITarget target)
        {
            return target.DoThis();
        }

        public interface ITarget
        {
            string DoThis();
        }

        public class Adapter : ITarget
        {
            private Adaptee _adaptee;
            public Adapter(Adaptee adaptee)
            {
                _adaptee = adaptee;
            }

            public string DoThis()
            {
                return _adaptee.DoThat();
            }
        }

        public class Adaptee
        {
            public string DoThat()
            {
                return "that";
            }
        }
    }
}
