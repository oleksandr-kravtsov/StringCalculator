namespace StringCalculatorBLL
{
    public interface ICalculatorSettings
    {
        bool DisplayFormula { get; }
        bool DoNotIgnoreNegatives { get; }
        long? UpperBoundForNumbers { get; }
        char? AlternateDelimiter { get; }
        string InputString { get; }
    }
}
