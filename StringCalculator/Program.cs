using StringCalculatorBLL;
using System;

namespace StringCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputString = args?.Length > 0 ? args[0] : null;
            

            var calculator = new Calculator();
            var result = calculator.Add(inputString);

            Console.WriteLine("Result is {0}", result);
        }
    }
}
