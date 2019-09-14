using StringCalculatorBLL;
using Xunit;

namespace StringCalculatorBLLTests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator = new Calculator();
        public CalculatorTests()
        {
            
        }

        [InlineData("20", 20)]
        [InlineData("-20", -20)]
        [InlineData("0,-5", -5)]
        [InlineData(@"0\n-5", -5)]
        [InlineData("0,0", 0)]
        [InlineData("-50,-100", -150)]
        [InlineData(@"-50\n-100", -150)]
        [InlineData("1,5000", 5001)]
        [InlineData("5000000000,5000000000", 10000000000L)]
        [Theory]
        public void SimplePositiveAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = _calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData(null, 0)]
        [InlineData(",", 0)]
        [InlineData(@"\n", 0)]
        [InlineData(@"6\r5", 0)]
        [InlineData(",20", 20)]
        [InlineData("10,", 10)]
        [InlineData("one", 0)]
        [InlineData("one,two", 0)]
        [InlineData("a10,20", 20)]
        [InlineData(@"a10\n20", 20)]
        [InlineData("10,20a", 10)]
        [InlineData("01b,01b", 0)]
        [Theory]
        public void SimpleNegativeAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = _calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("20,30,50", 100)]
        [InlineData(@"1\n2,3", 6)]
        [InlineData(@"20,30,50\n80", 180)]
        [InlineData(@"\n20,30,50\n-110", -10)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
        [InlineData("-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12", -78)]
        [InlineData("5000000000,5000000000,5000000000,5000000000", 20000000000L)]
        [Theory]
        public void MultiplePositiveAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = _calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData(",,,,,,", 0)]
        [InlineData(@"\n\n\n\n\n\n\n\n\n\n", 0)]
        [InlineData(",,,20", 20)]
        [InlineData(@"\n\n\n\n\n20", 20)]
        [InlineData("10,,,,", 10)]
        [InlineData(",,,50,,,", 50)]
        [InlineData("one,two,qwe,rty", 0)]
        [InlineData("a10,20,c30", 20)]
        [InlineData("10,20a,z30", 10)]
        [InlineData("01b,01b,11b", 0)]
        [Theory]
        public void MultipleNegativeAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = _calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

    }
}