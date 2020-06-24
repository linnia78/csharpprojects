using System;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class DecoratorTests
    {
        /* 
            Pattern     :   Decorator
            References  :
                            https://refactoring.guru/design-patterns/decorator
                            https://sourcemaking.com/design_patterns/decorator
            Real World  :   
                            combos at mcdonalds
                            ice cream
        */

        [Fact]
        public void client()
        {
            // Arrange
            var hamburger = new HamburgerConcreteComponent();
            var combo1 = new CokeDecorator(hamburger);
            var combo2 = new FriesDecorator(combo1);

            // Act
            var combo1Price = combo1.Price();
            var combo2Price = combo2.Price();

            // Assert
            Assert.Equal(7.98m, combo1Price);
            Assert.Equal(10.97m, combo2Price);
        }

        public interface IItemComponent
        {
            decimal Price();
        }

        public class HamburgerConcreteComponent : IItemComponent
        {
            public decimal Price()
            {
                return 5.99m;
            }
        }

        public abstract class BaseItemDecorator : IItemComponent
        {
            protected IItemComponent _item;
            public BaseItemDecorator(IItemComponent item)
            {
                _item = item;
            }
            public abstract decimal Price();
        }

        public class FriesDecorator : BaseItemDecorator
        {
            public FriesDecorator(IItemComponent item) : base(item) { }
            public override decimal Price()
            {
                return _item.Price() + 2.99m;
            }
        }

        public class CokeDecorator : BaseItemDecorator
        {
            public CokeDecorator(IItemComponent item) : base(item) { }
            public override decimal Price()
            {
                return _item.Price() + 1.99m;
            }
        }
    }
}
