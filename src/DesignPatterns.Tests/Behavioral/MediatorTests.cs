using System;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class MediatorTests
    {
        /* 
            Pattern     :   Mediator
            References  :
                            https://refactoring.guru/design-patterns/mediator
                            https://sourcemaking.com/design_patterns/mediator
            Real World  :   
                            air traffic control tower
            Similarity  :   
                            Mediator and Facade are similar, they organize collaboration between lots of tightly coupled classes
        */

        [Fact]
        public void client()
        {
            // Arrange
            var componentA = new ComponentA();
            var componentB = new ComponentB();
            var mediator = new ConcreteMediator(componentA, componentB);

            // Act
            var thisThat = componentA.DoThis();
            var that = componentB.DoThat();

            // Assert
            Assert.Equal("thisthat", thisThat);
            Assert.Equal("that", that);
        }

        public interface IMediator
        {
            string Send(object sender);
        }

        public class ConcreteMediator : IMediator
        {
            private readonly ComponentA _componentA;
            private readonly ComponentB _componentB;
            public ConcreteMediator(ComponentA componentA, ComponentB componentB)
            {
                _componentA = componentA;
                _componentA.SetMediator(this);
                _componentB = componentB;
                _componentB.SetMediator(this);
            }

            public string Send(object sender)
            {
                if (sender is ComponentA)
                {
                    return _componentB.DoThat();
                }

                return string.Empty;
            }
        }

        public abstract class BaseComponent
        {
            protected IMediator _mediator;
            public BaseComponent(IMediator mediator)
            {
                _mediator = mediator;
            }

            public BaseComponent() {}

            public void SetMediator(IMediator mediator)
            {
                _mediator = mediator;
            }
        }

        public class ComponentA : BaseComponent
        {
            public string DoThis()
            {
                return "this" + _mediator.Send(this); // communicates indirectly via mediator
            }
        }

        public class ComponentB : BaseComponent
        {
            public string DoThat()
            {
                return "that";
            }
        }
    }
}
