using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using StringCalculatorBLL;

namespace StringCalculator
{
    public class CalculatorSettings : ICalculatorSettings
    {
        [Option('f', "display", Required = false, HelpText = "Display calculation formula")]
        public  bool DisplayFormula { get; private set; }
        [Option('n', Required = false, HelpText = "Do not ignore negatives")]
        public bool DoNotIgnoreNegatives { get; private set; }
        [Option('u', Required = false, HelpText = "Upper bound for numbers")]
        public long? UpperBoundForNumbers { get; private set; }
        [Option('d', Required = false, HelpText = "Alternate delimiter")]
        public char? AlternateDelimiter { get; private set; }

        [Value(0)]
        public string InputString { get; private set; }

        [Usage(ApplicationAlias = "StringCalculator")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example("Simple calculate numbers from input string", new CalculatorSettings { InputString = "5,6,11" }),
                new Example("With custom delimiters", new CalculatorSettings { InputString = @"//[{delimiter1}][{delimiter2}]...\n{numbers}" })
            };

        public CalculatorSettings():this(null,null,false,false)
        {
        }

        public CalculatorSettings(long? upperBoundForNumbers, char? alternateDelimiter,
            bool displayFormula, bool doNotIgnoreNegatives)
        {
            DisplayFormula = displayFormula;
            DoNotIgnoreNegatives = doNotIgnoreNegatives;
            UpperBoundForNumbers = upperBoundForNumbers;
            AlternateDelimiter = alternateDelimiter;
        }
    }
}
