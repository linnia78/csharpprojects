using System;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class PrototypeTests
    {
        /* 
            Pattern     :   Prototype
            References  :
                            https://refactoring.guru/design-patterns/prototype
                            https://sourcemaking.com/design_patterns/prototype
            Real World  :   
                            cell divide : clone from within
        */

        [Fact]
        public void client()
        {
            // Arrange
            var pooky = new Dog("Romina", "Pooky");
            pooky.Weight = 10;

            // Act
            var shallow = pooky.ShallowCopy();
            var deep = pooky.DeepCopy();

            // Assert
            Assert.Equal("RominaPooky10Mammal", Output(shallow));
            Assert.Equal("RominaPooky10Mammal", Output(deep));

            // modify shallow copy
            pooky.AnimalType = AnimalType.Fish;
            pooky.Info.Name = "Nemo";
            pooky.Owner = "Allen";
            pooky.Weight = 5;
            Assert.Equal("AllenNemo5Fish", Output(pooky));
            Assert.Equal("RominaNemo10Mammal", Output(shallow));
            Assert.Equal("RominaPooky10Mammal", Output(deep));
        }

        public string Output(Dog dog)
        {
            return $"{dog.Owner}{dog.Info.Name}{dog.Weight}{dog.AnimalType}";
        }

        public enum AnimalType
        { 
            Mammal,
            Fish
        }

        public class Dog
        {
            private AnimalType _animalType = AnimalType.Mammal;
            public AnimalType AnimalType { get { return _animalType; } set { _animalType = value; } }
            public int Weight;
            public string Owner;
            public Info Info;
            public Dog(string owner, string name)
            {
                Owner = owner;
                Info = new Info { Name = name };
            }

            public Dog ShallowCopy()
            {
                return (Dog)this.MemberwiseClone();
            }

            public Dog DeepCopy()
            {
                var clone = ShallowCopy();
                clone.Info = new Info { Name = Info.Name };
                Owner = new string(Owner);
                return clone;
            }
        }

        public class Info
        {
            public string Name { get; set; }
        }
    }
}
