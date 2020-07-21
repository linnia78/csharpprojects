using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class IteratorTests
    {
        /* 
            Pattern     :   Iterator
            References  :
                            https://refactoring.guru/design-patterns/iterator
                            https://sourcemaking.com/design_patterns/iterator
            Real World  :   
                            different ways of traversing a collection
                            maze
                            
        */

        [Fact]
        public void client()
        {
            // Arrange
            var collection = new Collection(new List<int> { 3, 6, 1, 7, 2, 9 });
            var index = 0;

            // Act
            var first = 0;
            var last = 0;
            var third = 0;
            foreach(var item in collection)
            {
                if (index == 0)
                {
                    first = (int)item;
                }
                else if (index == 2)
                {
                    third = (int)item;
                }
                else if (index == collection.Count - 1)
                {
                    last = (int)item;
                }
                index++;
            }

            // Assert
            Assert.Equal(1, first);
            Assert.Equal(3, third);
            Assert.Equal(9, last);
        }

        public class Collection : IEnumerable
        {
            private List<int> _list;
            public Collection(List<int> list)
            {
                _list = list;
            }

            public IEnumerator GetEnumerator()
            {
                return new SortedIterator(this._list);
            }

            public int Count => _list.Count;
        }

        public class SortedIterator : IEnumerator
        {
            private int index = -1;
            private readonly List<int> _list;

            public object Current => _list[index];

            public SortedIterator(List<int> list)
            {
                _list = list.OrderBy(x => x).ToList();
            }

            public bool MoveNext()
            {
                return index++ < _list.Count - 1;
            }

            public void Reset()
            {
                index = 0;
            }
        }
    }
}
