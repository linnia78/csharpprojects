using System;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class StrategyTests
    {
        /* 
            Pattern     :   Strategy
            Definitino  :   a behavioral design pattern that lets you define a family of algorithms, 
                            put each of them into a separate class, and make their objects interchangeable.
            References  :
                            https://refactoring.guru/design-patterns/strategy
                            https://sourcemaking.com/design_patterns/strategy
            Real World  :   
                            gps with different form of vehidlce
                            
        */

        [Fact]
        public void client()
        {
            // Arrange
            var context = new Context();

            // Act
            var result1 = context.DoSomething();
            context.SetStrategy(new RunStrategy());
            var result2 = context.DoSomething();
            context.SetStrategy(new JogStrategy());
            var result3 = context.DoSomething();

            // Assert
            Assert.Equal("walk", result1);
            Assert.Equal("run", result2);
            Assert.Equal("jog", result3);
        }

        public interface IStrategy
        {
            string DoSomething();
        }

        public class WalkStrategy : IStrategy
        {
            public string DoSomething()
            {
                return "walk";
            }
        }

        public class JogStrategy : IStrategy
        {
            public string DoSomething()
            {
                return "jog";
            }
        }

        public class RunStrategy : IStrategy
        {
            public string DoSomething()
            {
                return "run";
            }
        }

        public class Context : IStrategy
        {
            private IStrategy _strategy;
            public Context()
            {
                _strategy = new WalkStrategy();
            }

            public string DoSomething() => _strategy.DoSomething();            

            public void SetStrategy(IStrategy strategy)
                => _strategy = strategy;
        }
    }
}
