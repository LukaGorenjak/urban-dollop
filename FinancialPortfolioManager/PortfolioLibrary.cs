using System.Collections.Generic;
using System.Linq;
using FinancialPortfolioManager;

namespace FinancialPortfolioManager.Library
{
    /// <summary>
    /// Primer "lastne knjižnice" za delo s portfeljem in objekti, ki implementirajo IValuable.
    /// </summary>
    public static class PortfolioLibrary
    {
        /// <summary>
        /// Izračuna skupno vrednost vseh IValuable objektov.
        /// </summary>
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

