using Algorithms.Tests.DataStructures.Node;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace Algorithms.Tests.Cracking.StackAndQueues
{
    public class _6_AnimalShelter
    {
        /// <summary>
        /// Create a data structure that supports operations : enqueue, dequeueAny, dequeueDog and dequeueCat using linked list
        /// </summary>
        public _6_AnimalShelter()
        {
            // Algorithm
            //      General idea, use two linked lists
            //          one for dog
            //          one for cat
            //      with a linked list AddLast, RemoveFirst
            //      enqueue will check type to determine which list to add
            //      dequeueAny will rotate between dog and cat
        }

        [Theory]
        [InlineData(new object[]{ AnimalType.Cat, "Kitty", AnimalType.Cat, "Cat", AnimalType.Cat, "Red" }, new string[] { "Kitty", "Cat", "Red" })]
        [InlineData(new object[]{ AnimalType.Cat, "Kitty" }, new string[] { "Kitty" })]
        [InlineData(new object[]{ AnimalType.Dog, "Pooky", AnimalType.Cat, "Kitty", AnimalType.Dog, "Pooky" }, new string[] { "Kitty" })]
        public void should_dequeue_cats(object[] values, string[] expectedNames)
        {
            // Arrange
            var shelter = new AnimalShelter();

            // Act
            EnqueueHelper(shelter, values);

            // Assert
            AssertNames(shelter, s => s.DequeueCat(), expectedNames);
        }

        [Theory]
        [InlineData(new object[]{ AnimalType.Dog, "Pooky", AnimalType.Dog, "Pompom", AnimalType.Dog, "Poomi" }, new string[] { "Pooky", "Pompom", "Poomi" })]
        [InlineData(new object[]{ AnimalType.Dog, "Pooky" }, new string[] { "Pooky" })]
        [InlineData(new object[]{ AnimalType.Cat, "Kitty", AnimalType.Dog, "Pooky", AnimalType.Cat, "Kitty" }, new string[] { "Pooky" })]
        public void should_dequeue_dogs(object[] values, string[] expectedNames)
        {
            // Arrange
            var shelter = new AnimalShelter();

            // Act
            EnqueueHelper(shelter, values);

            // Assert
            AssertNames(shelter, s => s.DequeueDog(), expectedNames);
        }

        [Theory]
        [InlineData(new object[]{ AnimalType.Cat, "Kitty", AnimalType.Cat, "Cat", AnimalType.Cat, "Red" }, new string[] { "Kitty", "Cat", "Red" })]
        [InlineData(new object[]{ AnimalType.Dog, "Pooky", AnimalType.Dog, "Pompom", AnimalType.Dog, "Poomi" }, new string[] { "Pooky", "Pompom", "Poomi" })]
        [InlineData(new object[]{ AnimalType.Dog, "Pooky", AnimalType.Dog, "Pompom", AnimalType.Dog, "Poomi", AnimalType.Cat, "Kitty", AnimalType.Cat, "Cat", AnimalType.Cat, "Red"  }, new string[] { "Pooky", "Kitty", "Pompom", "Cat", "Poomi", "Red" })]
        [InlineData(new object[]{ AnimalType.Cat, "Kitty", AnimalType.Cat, "Cat", AnimalType.Cat, "Red", AnimalType.Dog, "Pooky"  }, new string[] { "Pooky", "Kitty", "Cat", "Red" })]
        [InlineData(new object[]{ AnimalType.Dog, "Pooky", AnimalType.Dog, "Pompom", AnimalType.Dog, "Poomi", AnimalType.Cat, "Kitty" }, new string[] { "Pooky", "Kitty", "Pompom", "Poomi" })]
        public void should_dequeue_any(object[] values, string[] expectedNames)
        {
            // Arrange
            var shelter = new AnimalShelter();

            // Act
            EnqueueHelper(shelter, values);

            // Assert
            AssertNames(shelter, s => s.DequeueAny(), expectedNames);
        }

        private void AssertNames(AnimalShelter shelter, Func<AnimalShelter, Animal> dequeueFunc, string[] expectedNames)
        {
            foreach(var expectedName in expectedNames)
            {
                var name = dequeueFunc(shelter).Name;
                Assert.Equal(expectedName, name);
            }
        }

        private void EnqueueHelper(AnimalShelter shelter, object[] values)
        {
            for (int i = 0; i < values.Length / 2; i++)
            {
                if (((ValueType)values[i*2]).Equals(AnimalType.Dog))
                {
                    shelter.Enqueue(new Dog { Name = values[(i*2) + 1] as string });
                }
                else
                {
                    shelter.Enqueue(new Cat { Name = values[(i*2) + 1] as string });
                }
            }
        }

        public class AnimalShelter
        {
            private readonly LinkedList<Dog> _dogs;
            private readonly LinkedList<Cat> _cats;
            private int _rotateFlag = 0; // 0 = dog, 1 = cat
            public AnimalShelter()
            {
                _dogs = new LinkedList<Dog>();
                _cats = new LinkedList<Cat>();
            }

            public void Enqueue(Animal animal)
            {
                if (animal is Dog)
                {
                    _dogs.AddLast(animal as Dog);
                }
                else
                {
                    _cats.AddLast(animal as Cat);
                }
            }

            public Animal DequeueAny()
            {
                Animal result;
                if (_dogs.Any() && (_rotateFlag == 0 || (_rotateFlag == 1 && !_cats.Any())))
                {
                    result = _dogs.First.Value;
                    _dogs.RemoveFirst();
                    Flip();
                    return result;
                }
                else if (_cats.Any() && (_rotateFlag == 1 || (_rotateFlag == 0 && !_dogs.Any())))
                {
                    result = _cats.First.Value;
                    _cats.RemoveFirst();
                    Flip();
                    return result;
                }
                
                throw new InvalidOperationException("There are no more cats and dogs");
            }

            private void Flip() => _rotateFlag = (_rotateFlag + 1) % 2;

            public Cat DequeueCat()
            {
                if (!_cats.Any())
                {
                    throw new InvalidOperationException("There are no more cats");
                }
                var value = _cats.First.Value;
                _cats.RemoveFirst();
                return value;
            }

            public Dog DequeueDog()
            {
                if (!_dogs.Any())
                {
                    throw new InvalidOperationException("There are no more dogs");
                }
                var value = _dogs.First.Value;
                _dogs.RemoveFirst();
                return value;
            }
        }

        public class Animal
        {
            public string Name { get; set; }
        }
        
        public class Dog : Animal { }
        public class Cat : Animal { }
        public enum AnimalType
        {
            Dog,
            Cat
        }
    }
}
