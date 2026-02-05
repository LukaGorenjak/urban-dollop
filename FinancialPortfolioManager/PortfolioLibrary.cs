using System.Collections.Generic;
using System.Linq;
using FinancialPortfolioManager;

namespace FinancialPortfolioManager.Library
{

    public static class PortfolioLibrary
    {

        public static decimal CalculateTotalValue(IEnumerable<IValuable> valuables)
        {
            if (valuables == null)
            {
                return 0m;
            }

            return valuables.Sum(v => v.GetValue());
        }
    }
}

