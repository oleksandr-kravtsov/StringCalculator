using StringCalculatorBLL;
using Xunit;

namespace StringCalculatorBLLTests
{
    public class CalculatorTests
    {
        [InlineData("20", 20)]
        [InlineData("-20", -20)]
        [InlineData("0,-5", -5)]
        [InlineData("0,0", 0)]
        [InlineData("-50,-100", -150)]
        [InlineData("1,5000", 5001)]
        [InlineData("5000000000,5000000000", 100000000000L)]
        [Theory]
        public void SimpleAddTest(string input, long expectedResult)
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Add(input);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}