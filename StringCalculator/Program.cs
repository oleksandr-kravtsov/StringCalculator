using StringCalculatorBLL;
using System;

namespace StringCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputString = args?.Length > 0 ? args[0] : null;

            if (string.IsNullOrEmpty(inputString))
            {
                ShowUsage();
                return;
            }

            if (args?.Length > 1 && args[1] == "-disp")
            {
                var calcResult = Calculator.AddWithFormula(inputString);
                Console.WriteLine("Result is {0}", calcResult.result);
                Console.WriteLine("Formula is {0}", calcResult.formula);
            }
            else
            {
                Console.WriteLine("Result is {0}", Calculator.Add(inputString));
            }
           
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Code Challenge - String Calculator");
            Console.WriteLine("Usage: <input string with numbers> <additional arguments>");
            Console.WriteLine("   -disp    display formula used to calculate result");
        }
    }
}
