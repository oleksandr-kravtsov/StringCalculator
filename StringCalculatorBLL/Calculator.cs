using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculatorBLL
{
    public class Calculator
    {
        private const string NegativeNumbersErrorMessage = "Calculation Failed. Negative numbers in input string: {0}";
        private const int MaxNumberForCalculation = 1000;
        public long Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var delimiters = new List<string>() {",", @"\n"};

            var additionalDelimiter = GetSingleDelimiterIfExist(input);
            if (additionalDelimiter != string.Empty)
            {
                delimiters.Add(additionalDelimiter);
            }

            var numbers = input.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

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
                    if (negativeNumbers.Count == 0 && number <= MaxNumberForCalculation)
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

        private static string GetSingleDelimiterIfExist(string input)
        {
            string additionalDelimiter = string.Empty;
            if (input.StartsWith(@"//"))
            {
                var endDelimiter = input.IndexOf(@"\n", StringComparison.InvariantCulture);

                if (endDelimiter == 3)
                {
                    return input.Substring(2,1);
                }
            }

            return additionalDelimiter;
        }
    }
}
