using System;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class FacadeTests
    {
        /* 
            Pattern     :   Facade
            References  :
                            https://refactoring.guru/design-patterns/facade
                            https://sourcemaking.com/design_patterns/facade
            Real World  :   
                            operator for a business (an entry point to a complex sub system)
        */

        [Fact]
        public void client()
        {
            // Arrange
            var facade = new Facade(new SubSystem1(), new SubSystem2());

            // Act
            var result = facade.DoSomething();

            // Assert
            Assert.Equal("thatthis", result);
        }


        public class Facade
        {
            private SubSystem1 _subSystem1;
            private SubSystem2 _subSystem2;

            public Facade(
                SubSystem1 subSystem1,
                SubSystem2 subSystem2)
            {
                _subSystem1 = subSystem1;
                _subSystem2 = subSystem2;
            }

            public string DoSomething()
            {
                return _subSystem1.Part1()
                    + _subSystem1.Part2()
                    + _subSystem1.Part3()
                    + _subSystem1.Part4()
                    + _subSystem2.Stage1()
                    + _subSystem2.Stage2()
                    + _subSystem2.Stage3()
                    + _subSystem2.Stage4();
            }
        }

        public class SubSystem1
        {
            public string Part1() => "t";
            public string Part2() => "h";
            public string Part3() => "a";
            public string Part4() => "t";
        }

        public class SubSystem2
        {
            public string Stage1() => "t";
            public string Stage2() => "h";
            public string Stage3() => "i";
            public string Stage4() => "s";
        }
    }
}
