using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculatorBLL
{
    public class Calculator
    {
        private const string NegativeNumbersErrorMessage = "Calculation Failed. Negative numbers in input string: {0}";
        private const string DelimiterRegexPattern = @"\[(.*?)\]";
        private const int MaxNumberForCalculation = 1000;

        public static long Add(string input)
        {
            return AddInternal(input).result;
        }


        public static (long result, string formula) AddWithFormula(string input)
        {
            return AddInternal(input, true);
        }

        private static (long result, string formula) AddInternal(string input, bool buildFormula = false)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return (0, string.Empty);
            }

            var delimiters = new List<string>() { ",", "\n" };

            var additionalDelimiters = GetAdditionalDelimitersIfExist(input);
            if (additionalDelimiters.delimiters?.Count > 0)
            {
                delimiters.AddRange(additionalDelimiters.delimiters);

                //remove format for custom delimiters if there are more characters after \n
                if (additionalDelimiters.startInputString < input.Length)
                {
                    input = input.Substring(additionalDelimiters.startInputString);
                }
                else
                {
                    // \n is last character, return 
                    return (0, string.Empty);

                }
            }

            var numbers = input.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            return SumInternal(numbers);
        }

        private static (long result, string formula) SumInternal(IEnumerable<string> numbers, bool buildFormula = false)
        {
            long sum = 0;
            var negativeNumbers = new List<long>();
            var sbDisplay = new StringBuilder();
            foreach (var s in numbers)
            {
                if (long.TryParse(s, out var number))
                {
                    //in case we have some negative numbers, we should throw exception
                    if (negativeNumbers.Count == 0 && number <= MaxNumberForCalculation)
                    {
                        sum += number;
                        sbDisplay.Append(number + " + ");
                    }
                    else
                    {
                        sbDisplay.Append("0 + ");
                    }

                    if (number < 0)
                    {
                        negativeNumbers.Add(number);
                    }
                }
                else
                {
                    sbDisplay.Append("0 + ");
                }
            }

            if (negativeNumbers.Count > 0)
            {
                throw new ArgumentException(
                    string.Format(NegativeNumbersErrorMessage, string.Join(",", negativeNumbers)));
            }

            if (sbDisplay.Length > 0)
            {
                sbDisplay.Remove(sbDisplay.Length - 2, 2);
                sbDisplay.Append("= " + sum);
            }

            return (sum, sbDisplay.ToString());
        }

        private static (ICollection<string> delimiters, int startInputString) GetAdditionalDelimitersIfExist(string input)
        {
            if (input.StartsWith("//"))
            {
                var endDelimiter = input.IndexOf("\n", StringComparison.InvariantCulture);

                //single character length delimiter
                if (endDelimiter == 3)
                {
                    return (new[] {input.Substring(2, 1)}, endDelimiter+1);
                }

                //minimum pattern \\[]\n
                if (endDelimiter > 3)
                {
                    return (ParseMultipleDelimiters(input.Substring(2, endDelimiter - 2)), endDelimiter + 1);
                }
            }

            return (Enumerable.Empty<string>().ToArray(), 0);
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
