using System;

namespace StringCalculatorBLL
{
    public class Calculator
    {
        public long Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }
            var numbers = input.Split(new[] {",",@"\n"}, StringSplitOptions.RemoveEmptyEntries);
            long sum = 0;
            foreach (var s in numbers)
            {
                if (long.TryParse(s, out var number))
                {
                    sum += number;
                }
            }

            return sum;
        }
    }
}
