using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Tests.Common
{
    public class LRCCacheTest
    {
        /*
            Create a LRU Cache data structure
        */
        [Theory]
        [InlineData(1, new int[] {  1, 2, 3, 4, 5 }, "5", 5, new int[] { 1, 2, 3, 4 }, new string[] { "4", "4" }, "4", 4, new int[] { 4 } )]
        [InlineData(5, new int[] {  1, 2, 3, 4, 5 }, "3", 3, new int[] { 6, 7, 8 }, new string[] { "3", "6" }, "7", 7, new int[] { 7, 6, 3, 8, 5 } )]
        [InlineData(3, new int[] {  3, 2, 1, 2, 3, 5 }, "3", 3, new int[] { 1, 1, 1, 1, 3, 3 }, new string[] { "5", "5" }, "3", 3, new int[] { 3, 5, 1 } )]
        public void should_return_value(int capacity, int[] initialValues, string firstKey, int expectedFirstValue, int[] secondValues, string[] retrievals, string secondKey, int expectedSecondValue, int[] expectedRemainingQueueItems )
        {
            // Arrange
            var cache = new LRUCache<int>(capacity);
            
            // Act & Assert
            foreach(var value in initialValues)
            {
                cache.AddOrUpdate(value.ToString(), value);
            }
            Assert.Equal(expectedFirstValue, cache.Get(firstKey));

            foreach(var value in secondValues)
            {
                cache.AddOrUpdate(value.ToString(), value);
            }

            foreach(var key in retrievals)
            {
                cache.Get(key);
            }
            Assert.Equal(expectedSecondValue, cache.Get(secondKey));


            foreach(var zip in expectedRemainingQueueItems.Zip(cache.GetUsageHistry()))
            {
                Assert.Equal(zip.First, zip.Second);
            }
        }

        public class LRUCache<T>
        {
            private readonly LinkedList<string> _usageHistory;
            private readonly Dictionary<string, T> _dictionary;
            private readonly int _capacity;
            private int _count;
            public LRUCache(int capacity)
            {
                if (capacity < 1) { throw new InvalidOperationException("capacity has to be greater than zero."); }
                _dictionary = new Dictionary<string, T>();
                _usageHistory = new LinkedList<string>();
                _capacity = capacity;
                _count = 0;
            }

            public void AddOrUpdate(string key, T value)
            {
                // Algorithm : 
                //  If dictionary contains key (updating)
                //      update value 
                //      update usage history
                //  else (adding)
                //      If capacity reached 
                //          remove from end of usage history
                //          update dictionary
                //      else
                //          add value
                //          update usage history 
                //          increment count
                if (_dictionary.ContainsKey(key))
                {
                    _dictionary[key] = value;
                    UpdateUsage(key);
                }
                else
                {
                    if (_count == _capacity)
                    {
                        RemoveLast();
                    }
                    _dictionary.Add(key, value);
                    _usageHistory.AddFirst(key);
                    _count++;
                }
                
            }

            private void UpdateUsage(string key)
            {
                if (_usageHistory.First() == key)
                {
                    return;
                }
                _usageHistory.Remove(key);
                _usageHistory.AddFirst(key);
            }

            private void RemoveLast()
            {
                _dictionary.Remove(_usageHistory.Last());
                _usageHistory.RemoveLast();
                _count--;
            }

            public T Get(string key)
            {
                if (!_dictionary.ContainsKey(key))
                {
                    throw new KeyNotFoundException("Unable to find key");
                }
                UpdateUsage(key);
                return _dictionary[key];
            }

            public IEnumerable<T> GetUsageHistry()
            {
                foreach(var key in _usageHistory)
                {
                    yield return _dictionary[key];
                }
            }
        }
    }
}
