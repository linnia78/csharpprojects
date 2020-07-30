using System;
using System.Collections.Generic;
using Xunit;

namespace DesignPatterns.Tests.Behavioral
{
    public class MementoTests
    {
        /* 
            Pattern     :   Memento
            References  :
                            https://refactoring.guru/design-patterns/memento
                            https://sourcemaking.com/design_patterns/memento
            Real World  :   
                            Undo
        */

        [Fact]
        public void client()
        {
            // Arrange
            var originator = new Originator("start");
            var stateTracker = new StateTracker(originator);

            // Act
            stateTracker.Undo();
            var firstResult = originator.Output();
            
            stateTracker.Track();
            originator.Update("update");
            var secondResult = originator.Output();

            stateTracker.Track();
            originator.Update("third");
            var thirdResult = originator.Output();

            stateTracker.Undo();
            stateTracker.Undo();
            var fourthResult = originator.Output();

            // Assert
            Assert.Equal("start", firstResult);
            Assert.Equal("update", secondResult);
            Assert.Equal("third", thirdResult);
            Assert.Equal("start", fourthResult);
        }

        public interface IMemento
        {
            string GetState();
        }

        public class Originator
        {
            private string _state;
            public Originator(string state)
            {
                _state = state;
            }

            public IMemento SaveState()
            {
                return new ConcreteMemento(_state);
            }

            public void Update(string newState)
            {
                _state = newState;
            }

            public void Restore(string state)
            {
                _state = state;
            }

            public string Output()
            {
                return _state;
            }
        }

        public class StateTracker
        {
            private Stack<IMemento> _history;
            private Originator _originator;
            public StateTracker(Originator originator)
            {
                _originator = originator;
                _history = new Stack<IMemento>();
            }

            public void Track()
            {
                _history.Push(_originator.SaveState());
            }

            public void Undo()
            {
                if (_history.Count == 0)
                {
                    return;
                }

                var memento = _history.Pop();
                _originator.Restore(memento.GetState());
            }
        }

        public class ConcreteMemento : IMemento
        {
            private string _savedState;
            public ConcreteMemento(string state)
            {
                _savedState = state;
            }

            public string GetState()
            {
                return _savedState;
            }
        }
    }
}
