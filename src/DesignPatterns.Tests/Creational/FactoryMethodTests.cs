using System;
using System.Net.Http.Headers;
using Xunit;

namespace DesignPatterns.Tests.Creational
{
    public class FactoryMethodTests
    {
        /* 
            Pattern     :   Factory Method
            References  :
                            https://refactoring.guru/design-patterns/factory-method
                            https://sourcemaking.com/design_patterns/factory-method
            Real World  :   
                            Delivery via truck, ship or air
        */

        [Fact]
        public void client()
        {
            // Arrange
            var creatorA = new ConcreteCreatorA();
            var creatorB = new ConcreteCreatorB();

            // Act
            var resultA = DoThis(creatorA);
            var resultB = DoThis(creatorB);

            // Assert
            Assert.Equal("A", resultA);
            Assert.Equal("B", resultB);
        }

        public string DoThis(Creator creator)
        {
            return creator.DoThat();
        }

        public interface IProduct
        {
            string DoSomething();
        }

        public abstract class Creator
        {
            public abstract IProduct FactoryMethod();

            public string DoThat()
            {
                var product = FactoryMethod();
                return product.DoSomething();
            }
        }

        public class ConcreteCreatorA : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ProductA();
            }
        }

        public class ProductA : IProduct
        {
            public string DoSomething()
            {
                return "A";
            }
        }

        public class ConcreteCreatorB : Creator
        {
            public override IProduct FactoryMethod()
            {
                return new ProductB();
            }
        }

        public class ProductB : IProduct
        {
            public string DoSomething()
            {
                return "B";
            }
        }
    }
}
