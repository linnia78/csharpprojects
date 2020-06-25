using System;
using System.Collections.Generic;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class ProxyTests
    {
        /* 
            Pattern     :   Proxy
            References  :
                            https://refactoring.guru/design-patterns/proxy
                            https://sourcemaking.com/design_patterns/proxy
            Real World  :   
                            bank payment - credit card and money
            Relations   :   
                            structurally similar to decorator but different intent, proxy usually manage life of its service where as decocrator controlled by client
        */

        [Fact]
        public void client()
        {
            // Arrange
            var service = new Proxy(new Service());

            // Act
            var result = service.DoSomeWork(1);
            var result2 = service.DoSomeWork(1);
            var result3 = service.DoSomeWork(2);

            // Assert
            Assert.Equal("some1", result2);
            Assert.Equal("some2", result3);
        }

        public interface IService
        {
            string DoSomeWork(int key);
        }

        public class Service : IService
        {
            public string DoSomeWork(int key)
            {
                return "some" + key;
            }
        }

        public class Proxy : IService
        {
            private readonly IService _service;
            private readonly Dictionary<int, string> _cache;
            public Proxy(
                IService service)
            {
                _service = service;
                _cache = new Dictionary<int, string>();
            }

            public string DoSomeWork(int key)
            {
                if (!_cache.ContainsKey(key))
                {
                    _cache.Add(key, _service.DoSomeWork(key));
                }

                return _cache[key];
            }
        }

    }
}
