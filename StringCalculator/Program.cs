using CommandLine;
using StringCalculatorBLL;
using System;
using Unity;

namespace StringCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CalculatorSettings>(args)
                .WithParsed(RunApplication);
        }

        private static void RunApplication(CalculatorSettings settings)
        {
            var container = UnityConfig.RegisterComponents();
            RegisterSettings(container, settings);

            var calculator = container.Resolve<ICalculator>();
            var calcResult = calculator.Add();
            Console.WriteLine("Result is {0}", calcResult.Result);

            if (settings.DisplayFormula)
            {
                Console.WriteLine("Formula is {0}", calcResult.Formula);
            }
        }

        private static void RegisterSettings(IUnityContainer container, ICalculatorSettings setting)
        {
            container.RegisterInstance<ICalculatorSettings>(setting);
        }
    }
}
