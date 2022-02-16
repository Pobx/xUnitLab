using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestCalculator
{
    public class TestBasicMath
    {
        [Fact]
        public void SubtractNumbers()
        {
            //Arrange
            double expected = 2;

            //Action
            BasicMath cal1 = new BasicMath();
            double actual = cal1.Subtract(5, 3);

            //Assertion
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(4, 0, 0)]
        [InlineData(0, 4, 0)]
        [InlineData(10, 5, 2)]
        public void DivideNumbers(double a, double b, double expected)
        {
            //Action
            BasicMath cal1 = new BasicMath();
            cal1.Divide(a, b);

            //Asserttion
            Assert.Equal(expected, cal1.Divide(a, b));
        }
    }
}
