using System;
using System.Text;
using StringCalculatorBLL;
using Xunit;

namespace StringCalculatorBLLTests
{
    public class CalculatorTests
    {
        [InlineData("20", 20)]
        [InlineData("0,0", 0)]
        [InlineData("1,5000", 1)]
        [InlineData("5000000000,5000000000", 0)]
        [Theory]
        public void SimplePositiveAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData(null, 0)]
        [InlineData(",", 0)]
        [InlineData("\n", 0)]
        [InlineData(@"6\r5", 0)]
        [InlineData(",20", 20)]
        [InlineData("10,", 10)]
        [InlineData("one", 0)]
        [InlineData("one,two", 0)]
        [InlineData("a10,20", 20)]
        [InlineData("a10\n20", 20)]
        [InlineData("10,20a", 10)]
        [InlineData("01b,01b", 0)]
        [Theory]
        public void SimpleNegativeAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("20,30,50", 100)]
        [InlineData("1\n2,3", 6)]
        [InlineData("20,30,50\n80", 180)]
        [InlineData("2,1001\n6", 8)]
        [InlineData("1,10,1000", 1011)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
        [InlineData("5000000000,5000000000,5000000000,5000000000", 0)]
        [Theory]
        public void MultiplePositiveAddTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData(",,,,,,", 0)]
        [InlineData("\n\n\n\n\n\n\n\n\n\n", 0)]
        [InlineData(",,,20", 20)]
        [InlineData("\n\n\n\n\n20", 20)]
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
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [InlineData("-20")]
        [InlineData("0,-5","-5")]
        [InlineData("0\n-5", "-5")]
        [InlineData("-50,-100")]
        [InlineData("-50\n-100", "-50,-100")]
        [InlineData("\n20,30,50\n-110","-110")]
        [InlineData("-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,-11,-12")]
        [InlineData("1,2,3,4,-5,6\n7,8,9","-5")]
        [Theory]
        public void ExceptionWhenNegativeNumbersTest(string input, string expectedExceptionList = "")
        {
            //Act
            var exception = Assert.Throws<ArgumentException>(() => Calculator.Add(input));

            //Assert
            Assert.Contains(expectedExceptionList, exception.Message);
        }


        [Fact]
        public void TestBigString()
        {
            var maxNumber = 1000;
            //Arrange
            var sb = new StringBuilder();
            for(var i=1; i < 10000; i++)
            {
                sb.Append(i+",");
            }
            var expected = maxNumber * (maxNumber + 1) / 2;

            //Act
            var actualResult = Calculator.Add(sb.ToString());
            
            //Assert
            Assert.Equal(expected, actualResult);
        }


        [InlineData("//;\n2;5", 7)]
        [InlineData("//[\n2[3", 5)]
        [InlineData("//]\n2]4", 6)]
        [InlineData("// \n8 1", 9)]
        [InlineData("//#\n2#5,3\n10", 20)]
        [InlineData("//!\n2!5,3\n10;3", 10)]
        [Theory]
        public void CustomSingleCharacterPositiveTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("/;\n2;5;3", 0)]
        [InlineData("//\n1,2", 3)]
        [InlineData("//1,2", 2)]
        [InlineData("//", 0)]
        [InlineData("//;\r2;5;3", 0)]
        [InlineData("//;#\n2;5;3", 0)]
        [Theory]
        public void CustomSingleCharacterNegativeTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("//[***]\n11***22***33", 66)]
        [InlineData("//[      ]\n8      8\n", 16)]
        [InlineData("//[~!@#$%^&*(]\n50~!@#$%^&*(,20\n5,", 75)]
        [Theory]
        public void CustomMultipleCharacterPositiveTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [InlineData("//[]\n1,2,3", 6)]
        [InlineData("//[*]1;2\n3", 3)]
        [InlineData("//[^^\n30^^40,1", 1)]
        [InlineData("//___]\n30___40,2", 2)]
        [Theory]
        public void CustomMultipleCharacterNegativeTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("//[*][!!][r9r]\n11r9r22*33!!44", 110)]
        [InlineData("//[][][]\n40,50", 90)]
        [InlineData("//[[[[[]\n40[[[[50,5", 95)]
        [Theory]
        public void MultipleDelimitersPositiveTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("//[]]]]]]]\n40]]]]]]50,5", 5)]
        [InlineData("/[qwe]\n1qwe2,3", 3)]
        [Theory]
        public void MultipleDelimitersNegativeTest(string input, long expectedResult)
        {
            //Act
            var actualResult = Calculator.Add(input);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }


        [InlineData("[[*][![!][r9r]", new [] {"[*","![!","r9r"})]
        [InlineData("[!@#$%]", new[] { "!@#$%" })]
        [InlineData("[]", new[] {""})]
        [Theory]
        public void ParseMultipleDelimitersPositiveTests(string input, string[] outputDelimiters)
        {
            //Act
            var result = Calculator.ParseMultipleDelimiters(input);

            //Assert
            Assert.Equal(outputDelimiters, result);
        }


        [InlineData("//[&&]\n1&&2,3&4", 3, "1 + 2 + 0 = 3")]
        [InlineData("10,aa,a7,5", 15, "10 + 0 + 0 + 5 = 15")]
        [InlineData("//[&&][!!!]\n", 0, "")]
        [InlineData("//[&&][!!!]\n ", 0, "0 = 0")]
        [Theory]
        public void TestAddWithFormula(string input, long expectedResult, string formula)
        {
            //Act
            var actualResult = Calculator.AddWithFormula(input);

            //Assert
            Assert.Equal(expectedResult, actualResult.result);
            Assert.Equal(formula, actualResult.formula);
        }

    }
}