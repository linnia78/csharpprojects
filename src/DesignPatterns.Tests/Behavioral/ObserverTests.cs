using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class ObserverTests
    {
        /* 
            Pattern     :   Observer
            References  :
                            https://refactoring.guru/design-patterns/observer
                            https://sourcemaking.com/design_patterns/observer
            Real World  :   
                            Subscriptions to RSS feeds
                            Customers subscribing to product updates
        */

        [Fact]
        public void client()
        {
            // Arrange
            var publisher = new CompanyPublisher();
            var customerA = new CustomerObserverA();
            publisher.Subscribe(customerA);
            var customerB = new CustomerObserverB();
            publisher.Subscribe(customerB);

            // Act`
            var result1 = publisher.ReleaseNewProduct("iphone");
            publisher.UnSubscribe(customerB);
            var result2 = publisher.ReleaseNewProduct("pixel");

            // Assert
            Assert.Equal("AiphoneBiphone", result1);
            Assert.Equal("Apixel", result2);
        }

        public interface IObserver
        {
            string Update(string product);
        }

        public class CompanyPublisher
        {
            private readonly List<IObserver> _observers;
            public CompanyPublisher()
            {
                _observers = new List<IObserver>();
            }


            public void Subscribe(IObserver observer)
                => _observers.Add(observer);

            public void UnSubscribe(IObserver observer)
                => _observers.Remove(observer);

            // This method should be void, returning result for testing purposes
            public string ReleaseNewProduct(string product)
            {
                // ...
                return NotifyObservers(product);
            }

            // This method should be void, returning result for testing purposes 
            private string NotifyObservers(string product)
            {
                var sb = new StringBuilder();
                foreach(var observer in _observers)
                {
                    sb.Append(observer.Update(product));
                }
                return sb.ToString();
            }
        }

        public class CustomerObserverA : IObserver
        {
            public string Update(string product)
            {
                return "A" + product;
            }
        }

        public class CustomerObserverB : IObserver
        {
            public string Update(string product)
            {
                return "B" + product;
            }
        }
    }
}
