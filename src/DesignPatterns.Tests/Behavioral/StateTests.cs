using System;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class StateTests
    {
        /* 
            Pattern     :   State
            References  :
                            https://refactoring.guru/design-patterns/state
                            https://sourcemaking.com/design_patterns/state
            Real World  :   
                            iphone screen, different state motion gestures behave differently
                            
        */

        [Fact]
        public void client()
        {
            // Arrange
            var romina = new PersonContext();

            // Act
            var result1 = romina.ExpressCurrentMind();
            romina.DrinkAlcohol();
            var result2 = romina.ExpressCurrentMind();
            romina.DayDream();
            var result3 = romina.ExpressCurrentMind();
            romina.Travel();
            romina.DayDream();
            var result4 = romina.ExpressCurrentMind();

            // Assert
            Assert.Equal("huh", result1);
            Assert.Equal("wild", result2);
            Assert.Equal("huh", result3);
            Assert.Equal("joy", result4);
        }

        public interface IStateOfMind
        {
            string ExpressCurrentMind();
            void DrinkAlcohol();
            void Travel();
            void DayDream();
        }

        public class ClearState : IStateOfMind
        {
            private readonly PersonContext _context;

            public ClearState(PersonContext context)
            {
                _context = context;
            }

            public void DayDream() { }

            public void DrinkAlcohol()
            {
                _context.ChangeState(new WildState(_context));
            }

            public string ExpressCurrentMind()
            {
                return "huh";
            }

            public void Travel()
            {
                _context.ChangeState(new JoyState(_context));
            }
        }

        public class JoyState : IStateOfMind
        {
            private PersonContext _context;
            public JoyState(PersonContext context)
            {
                _context = context;
            }

            public void DayDream() { }
            public void DrinkAlcohol() { }
            public void Travel() { }
            public string ExpressCurrentMind() => "joy";
        }

        public class WildState : IStateOfMind
        {
            public string ExpressCurrentMind() => "wild";
            private PersonContext _context;
            public WildState(PersonContext context)
            {
                _context = context;
            }
            public void DrinkAlcohol() { }

            public void Travel()
            {
                _context.ChangeState(new JoyState(_context));
            }

            public void DayDream()
            {
                _context.ChangeState(new ClearState(_context));
            }
        }

        public class PersonContext : IStateOfMind
        {
            private IStateOfMind _state;
            public PersonContext()
            {
                _state = new ClearState(this);
            }

            public void ChangeState(IStateOfMind state)
            {
                _state = state;
            }

            public void DayDream()
            {
                _state.DayDream();
            }

            public void DrinkAlcohol()
            {
                _state.DrinkAlcohol();
            }

            public string ExpressCurrentMind()
            {
                return _state.ExpressCurrentMind();
            }

            public void Travel()
            {
                _state.Travel();
            }
        }
    }
}
