using System;
using System.Net.Sockets;
using System.Threading;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class BuilderTests
    {
        /* 
            Pattern     :   Builder
            References  :
                            https://refactoring.guru/design-patterns/builder
                            https://sourcemaking.com/design_patterns/builder
            Real World  :   
                            assemble a lego robot
                            building a house
        */

        [Fact]
        public void client()
        {
            // Arrange
            var builder = new HouseBuilder();
            var director = new Director(builder);

            // Act
            director.BuildFancyHouse();
            var fancyHouse = builder.GetHouse();

            director.BuildHouse();
            var house = builder.GetHouse();

            // Assert
            Assert.Equal("fancyroofwalls", fancyHouse);
            Assert.Equal("roofwalls", house);
        }

        public interface IBuilder
        {
            void Reset();
            void BuildRoof();
            void BuildWalls();
            void AddFancyDecorations();
        }

        public class HouseBuilder : IBuilder
        {
            private string _house;
            public void AddFancyDecorations()
            {
                _house = "fancy" + _house;
            }

            public void BuildRoof()
            {
                _house += "roof";
            }

            public void BuildWalls()
            {
                _house += "walls";
            }

            public void Reset()
            {
                _house = string.Empty;
            }

            public string GetHouse()
            {
                var house = _house;
                Reset();
                return house;
            }
        }

        public class Director
        {
            private IBuilder _builder;
            public Director(IBuilder builder)
            {
                _builder = builder;
            }

            public void BuildHouse()
            {
                _builder.Reset();
                _builder.BuildRoof();
                _builder.BuildWalls();
            }

            public void BuildFancyHouse()
            {
                BuildHouse();
                _builder.AddFancyDecorations();
            }
        }
    }
}
