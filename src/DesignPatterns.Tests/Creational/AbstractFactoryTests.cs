using System;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class AbstractFactoryTests
    {
        /* 
            Pattern     :   Abstract Factory
            References  :
                            https://refactoring.guru/design-patterns/abstract-factory
                            https://sourcemaking.com/design_patterns/abstract-factory
            Real World  :   
                            cross platform ui element creation
                            creating various category of items
                            matching color lego creation
                            
        */

        [Fact]
        public void client()
        {
            // Arrange
            var redFactory = new RedLegoFactory();
            var blueFactory = new BlueLegoFactory();

            // Act
            var redResult = Assemble(redFactory);
            var blueResult = Assemble(blueFactory);

            // Assert
            Assert.Equal("red4x4red1x2", redResult);
            Assert.Equal("blue4x4blue1x2", blueResult);
        }

        public string Assemble(ILegoAbstractFactory factory)
        {
            var fourByFour = factory.CreateFourByFour();
            var oneByTwo = factory.CreateOneByTwo();
            return fourByFour.Attach() + oneByTwo.Attach();
        }

        public interface ILegoAbstractFactory
        {
            IFourByFour CreateFourByFour();
            IOneByTwo CreateOneByTwo();
        }

        public interface IFourByFour
        {
            string Attach();
        }

        public interface IOneByTwo
        {
            string Attach();
        }

        public class RedLegoFactory : ILegoAbstractFactory
        {
            public IFourByFour CreateFourByFour()
            {
                return new RedFourByFour();
            }

            public IOneByTwo CreateOneByTwo()
            {
                return new RedOneByTwo();
            }
        }

        public class RedFourByFour : IFourByFour
        {
            public string Attach()
            {
                return "red4x4";
            }
        }

        public class RedOneByTwo : IOneByTwo
        {
            public string Attach()
            {
                return "red1x2";
            }
        }
        public class BlueLegoFactory : ILegoAbstractFactory
        {
            public IFourByFour CreateFourByFour()
            {
                return new BlueFourByFour();
            }

            public IOneByTwo CreateOneByTwo()
            {
                return new BlueOneByTwo();
            }
        }

        public class BlueFourByFour : IFourByFour
        {
            public string Attach()
            {
                return "blue4x4";
            }
        }

        public class BlueOneByTwo : IOneByTwo
        {
            public string Attach()
            {
                return "blue1x2";
            }
        }

    }
}
