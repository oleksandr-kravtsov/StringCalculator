using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculatorBLL;

namespace StringCalculatorBLLTests
{
    internal class TestCalculatorSettings: ICalculatorSettings
    {
        public bool DisplayFormula { get; set; }
        public bool DoNotIgnoreNegatives { get; set; }
        public long? UpperBoundForNumbers { get; set; }
        public char? AlternateDelimiter { get; set; }
        public string InputString { get; set; }
    }
}
