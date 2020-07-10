using System;
using System.ComponentModel;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class CompositeTests
    {
        /* 
            Pattern     :   Composite
            References  :
                            https://refactoring.guru/design-patterns/composite
                            https://sourcemaking.com/design_patterns/composite
            Real World  :   products and boxes
                            reporting structure
                            arithmetic operations, operand operator operand
            Relations   :   
                            
        */

        [Fact]
        public void should_do_simple_addition()
        {
            // Arrange
            var leaf = new Operand(10);
            var math = new Operator(OperatorType.Addition, leaf, leaf);

            // Act
            var result = math.Operation();

            // Assert
            Assert.Equal(20, result);
        }

        [Fact]
        public void should_do_complex_arithmetic()
        {
            // Arrange
            var ten = new Operand(10);
            var five = new Operand(5);
            var addition = new Operator(OperatorType.Addition, ten, five);
            var six = new Operand(6);
            var two = new Operand(2);
            var subtraction = new Operator(OperatorType.Subtraction, six, two);
            var multiplication = new Operator(OperatorType.Multiplication, subtraction, addition);

            // Act
            var result = multiplication.Operation();

            // Assert
            Assert.Equal(60, result);
        }

        public abstract class Component
        {
            public abstract int Operation();
            public abstract bool IsComposite(); // aka IsOperator
        }

        public enum OperatorType
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        public class Operator : Component 
        {
            private OperatorType _type;
            private Component _left;
            private Component _right;
            public Operator(
                OperatorType type,
                Component left,
                Component right)
            {
                _type = type;
                _left = left;
                _right = right;
            }
            public override bool IsComposite() => true;

            public override int Operation()
            {
                return _type switch
                {
                    OperatorType.Addition => _left.Operation() + _right.Operation(),
                    OperatorType.Subtraction => _left.Operation() - _right.Operation(),
                    OperatorType.Multiplication => _left.Operation() * _right.Operation(),
                    OperatorType.Division => _left.Operation() / _right.Operation(),
                    _ => throw new NotImplementedException()
                };
            }
        }

        public class Operand : Component
        {
            public override bool IsComposite() => false;
            private int _value;
            public Operand(int value)
            {
                _value = value;
            }
            public override int Operation()
            {
                return _value;
            }
        }
    }
}
