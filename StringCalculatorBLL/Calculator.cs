using System;

namespace StringCalculatorBLL
{
    public class Calculator
    {
        public long Add(string input)
        {
            var numbers = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
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
