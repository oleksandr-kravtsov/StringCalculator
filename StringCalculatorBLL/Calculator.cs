using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorBLL
{
    public class Calculator
    {
        private const string NegativeNumbersErrorMessage = "Calculation Failed. Negative numbers in input string: {0}";
        private const string DelimiterRegexPattern = @"\[(.*?)\]";
        private const int MaxNumberForCalculation = 1000;
        
        public long Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var delimiters = new List<string>() {",", "\n"};

            var additionalDelimiters = GetSingleDelimiterIfExist(input);
            if (additionalDelimiters?.Count > 0)
            {
                delimiters.AddRange(additionalDelimiters);
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

        private static ICollection<string> GetSingleDelimiterIfExist(string input)
        {
            if (input.StartsWith("//"))
            {
                var endDelimiter = input.IndexOf("\n", StringComparison.InvariantCulture);

                //single character length delimiter
                if (endDelimiter == 3)
                {
                    return new[] {input.Substring(2, 1)};
                }

                //minimum pattern \\[]\n
                if (endDelimiter > 3)
                {
                    return ParseMultipleDelimiters(input.Substring(2, endDelimiter - 2));
                }
            }

            return Enumerable.Empty<string>().ToArray();
        }

        internal static ICollection<string> ParseMultipleDelimiters(string input)
        {
            var matches = Regex.Matches(input, DelimiterRegexPattern);

            var additionalDelimiters = new List<string>();

            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    additionalDelimiters.Add(match.Groups[1].Value);
                }
            }

            return additionalDelimiters;
        }


    }
}
