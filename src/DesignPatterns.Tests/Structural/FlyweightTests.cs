using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace DesignPatterns.Tests.Structural
{
    public class FlyweightTests
    {
        /* 
            Pattern     :   Flyweight
            References  :
                            https://refactoring.guru/design-patterns/flyweight
                            https://sourcemaking.com/design_patterns/flyweight
            Real World  :   
                            game : forest and trees
                            game : bullets
                            
        */

        [Fact]
        public void client()
        {
            // Arrange
            var body = new Body();
            body.GrowHair(1, 1, 10, Color.Black, Curvature.Curved);
            body.GrowHair(2, 1, 4, Color.Black, Curvature.Curved);

            // Act
            var output = body.Output();

            // Assert
            Assert.Equal("Black, Curved, 1, 1, 10" + Environment.NewLine + "Black, Curved, 2, 1, 4" + Environment.NewLine, output);
        }

        public class Hair
        {
            public int X { get; }
            public int Y { get; }
            public int Length { get; }
            public HairFlyweight Shared { get; }
            public Hair(
                int x, int y, int length, HairFlyweight shared)
            {
                X = x;
                Y = y;
                Length = length;
                Shared = shared;
            }

            public string Output()
            {
                return Shared.Output(X, Y, Length);
            }
        }

        public class HairFactory
        {
            public static Dictionary<string, HairFlyweight> Cache = new Dictionary<string, HairFlyweight>();

            public static HairFlyweight GetFlyWeight(Color color, Curvature curvature)
            {
                var key = color.ToString() + curvature.ToString();
                if (!Cache.ContainsKey(key))
                {
                    Cache.Add(key, new HairFlyweight(color, curvature));
                }
                return Cache[key];
            }
        }

        public class HairFlyweight
        {
            public Color Color { get; }
            public Curvature Curvature { get; }
            public HairFlyweight(
                Color color,
                Curvature curvature)
            {
                Color = color;
                Curvature = curvature;
            }

            public string Output(int x, int y, int length)
            {
                return Color.ToString() + ", " + Curvature.ToString() + ", " + x + ", " + y + ", " + length;
            }
        }

        public class Body 
        {
            private List<Hair> Hairs = new List<Hair>();
            public void GrowHair(int x, int y, int length, Color color, Curvature curvature)
            {
                var shared = HairFactory.GetFlyWeight(color, curvature);
                Hairs.Add(new Hair(x, y, length, shared));
            }

            public string Output()
            {
                var output = new StringBuilder();
                foreach(var hair in Hairs)
                {
                    output.AppendLine(hair.Output());
                }
                return output.ToString();
            }
        }

        public enum Color
        {
            White,
            Black,
            Blonde,
            Brown
        }

        public enum Curvature
        {
            Straight,
            Curved,
            Jagged
        }
    }
}
