using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculatorBLL
{
    public class Calculator
    {
        private const string NegativeNumbersErrorMessage = "Calculation Failed. Negative numbers in input string: {0}";
        public long Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }
            var numbers = input.Split(new[] {",",@"\n"}, StringSplitOptions.RemoveEmptyEntries);

            return SumInternal(numbers);
        }

        private static long SumInternal(IEnumerable<string> numbers)
        {
            long sum = 0;
            var negativeNumbers = new List<long>();
            foreach (var s in numbers)
            {
                if (long.TryParse(s, out var number))
                {
                    //in case we have some negative numbers, we should throw exception
                    if (negativeNumbers.Count == 0)
                    {
                        sum += number;
                    }

                    if (number < 0)
                    {
                        negativeNumbers.Add(number);
                    }
                }
            }

            if (negativeNumbers.Count > 0)
            {
                throw new ArgumentException(
                    string.Format(NegativeNumbersErrorMessage, string.Join(",", negativeNumbers)));
            }

            return sum;
        }
    }
}
