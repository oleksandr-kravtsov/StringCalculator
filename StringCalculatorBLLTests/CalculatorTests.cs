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
        [InlineData("5000000000,5000000000", 10000000000L)]
        [Theory]
        public void SimplePositiveAddTest(string input, long expectedResult)
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Add(input);

            Assert.Equal(expectedResult, actualResult);
        }

        [InlineData("", 0)]
        [InlineData(",", 0)]
        [InlineData(",20", 20)]
        [InlineData("10,", 10)]
        [InlineData("one", 0)]
        [InlineData("one,two", 0)]
        [InlineData("a10,20", 20)]
        [InlineData("10,20a", 10)]
        [InlineData("01b,01b", 0)]
        [Theory]
        public void SimpleNegativeAddTest(string input, long expectedResult)
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            var actualResult = calculator.Add(input);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}