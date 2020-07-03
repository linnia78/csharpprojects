using System;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class ChainOfResponsibilityTests
    {
        /* 
            Pattern     :   Chain of Responsibility
            References  :
                            https://refactoring.guru/design-patterns/chain-of-responsibility
                            https://sourcemaking.com/design_patterns/chain-of-responsibility
            Real World  :   
                            
            Relations   :   
                            
        */

        [Fact]
        public void client()
        {
            // Arrange
            var handler = new ConcreteHandlerA();
            handler
                .SetNext(new ConcreteHandlerB())
                .SetNext(new ConcreteHandlerC());

            // Act
            var a = handler.Handle(Type.A);
            var b = handler.Handle(Type.B);
            var c = handler.Handle(Type.C);
            var d = handler.Handle(Type.D);

            // Assert
            Assert.Equal("A", a);
            Assert.Equal("B", b);
            Assert.Equal("C", c);
            Assert.Null(d);
        }

        public interface IHandler
        {
            IHandler SetNext(IHandler handler);
            string Handle(Type type);
        }


        public abstract class Handler : IHandler
        {
            protected IHandler Next;
            public abstract Type Type { get; }
            public IHandler SetNext(IHandler handler)
            {
                Next = handler;
                return handler;
            }
            public virtual string Handle(Type type)
            {
                if (Next == null)
                {
                    return null;
                }
                else
                {
                    return Next.Handle(type);
                }
            }
        }

        public enum Type
        {
            A,
            B,
            C,
            D
        }

        public class ConcreteHandlerA : Handler
        {
            public override Type Type => Type.A;

            public override string Handle(Type type)
            {
                if (Type == type)
                {
                    return Type.ToString();
                }
                else
                {
                    return base.Handle(type);
                }
            }
        }

        public class ConcreteHandlerB : Handler
        {
            public override Type Type => Type.B;

            public override string Handle(Type type)
            {
                if (Type == type)
                {
                    return Type.ToString();
                }
                else
                {
                    return base.Handle(type);
                }
            }
        }

        public class ConcreteHandlerC : Handler
        {
            public override Type Type => Type.C;

            public override string Handle(Type type)
            {
                if (Type == type)
                {
                    return Type.ToString();
                }
                else
                {
                    return base.Handle(type);
                }
            }
        }
    }
}
