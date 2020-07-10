using System;
using System.Text;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class CommandTests
    {
        /* 
            Pattern     :   Command
            References  :
                            https://refactoring.guru/design-patterns/command
                            https://sourcemaking.com/design_patterns/command
            Real World  :   
                            written order to the kitchen
        */

        [Fact]
        public void client()
        {
            // Arrange
            Product product = new Product();
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand(product, "a"));
            Receiver receiver = new Receiver();
            invoker.SetOnFinish(new ComplexCommand(product, receiver, "b", "c"));

            // Act
            invoker.DoSomethingImportant();

            // Assert
            Assert.Equal("aThisbThatc", product.Builder.ToString());
        }

        public interface ICommand
        {
            void Execute();
        }

        public class SimpleCommand : ICommand
        {
            private Product _product;
            private string _payload = string.Empty;

            public SimpleCommand(Product product, string payload)
            {
                _product = product;
                _payload = payload;
            }

            public void Execute()
            {
                _product.Builder.Append(_payload);
            }
        }

        public class Product
        {
            public StringBuilder Builder { get; } = new StringBuilder();
        }

        public class ComplexCommand : ICommand
        {
            private Product _product;
            private Receiver _receiver;

            // Context data, required for launching the receiver's methods.
            private string _a;
            private string _b;

            // Complex commands can accept one or several receiver objects along
            // with any context data via the constructor.
            public ComplexCommand(Product product, Receiver receiver, string a, string b)
            {
                this._product = product;
                this._receiver = receiver;
                this._a = a;
                this._b = b;
            }

            // Commands can delegate to any methods of a receiver.
            public void Execute()
            {
                _product.Builder.Append(this._receiver.AddThis(this._a));
                _product.Builder.Append(this._receiver.AddThat(this._b));
            }
        }

        // The Receiver classes contain some important business logic. They know how
        // to perform all kinds of operations, associated with carrying out a
        // request. In fact, any class may serve as a Receiver.
        public class Receiver
        {
            public string AddThis(string a)
            {
                return "This" + a;
            }

            public string AddThat(string b)
            {
                return "That" + b;
            }
        }

        // The Invoker is associated with one or several commands. It sends a
        // request to the command.
        class Invoker
        {
            private ICommand _onStart;
            private ICommand _onFinish;

            // Initialize commands.
            public void SetOnStart(ICommand command)
            {
                this._onStart = command;
            }

            public void SetOnFinish(ICommand command)
            {
                this._onFinish = command;
            }

            // The Invoker does not depend on concrete command or receiver classes.
            // The Invoker passes a request to a receiver indirectly, by executing a
            // command.
            public void DoSomethingImportant()
            {
                if (this._onStart is ICommand)
                {
                    this._onStart.Execute();
                }

                if (this._onFinish is ICommand)
                {
                    this._onFinish.Execute();
                }
            }
        }
    }
}
