using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Tests
{
    public class PlaygroundTests
    {
        [Fact]
        public void test()
        {
            var a = new Test() { MyProperty = 1, Next = new Test { MyProperty = 2 } };
            var b = a;
            a = a.Next;
            temp(ref a);

            Console.WriteLine(b.MyProperty);

            var x = 10;
            var y = x;
            x = 20;


            var sum = x + y;
        }

        private void temp(ref Test t)
        {
            t = new Test();
        }
    }


    public class Test
    {
        public int MyProperty { get; set; }
        public Test Next { get; set; }
    }
}
