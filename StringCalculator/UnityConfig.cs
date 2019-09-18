using StringCalculatorBLL;
using Unity;

namespace StringCalculator
{
    internal class UnityConfig
    {
        internal static UnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ICalculator, Calculator>();

            return container;
        }
    }
}
