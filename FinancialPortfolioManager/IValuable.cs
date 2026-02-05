using System;

namespace FinancialPortfolioManager
{

    public interface IValuable
    {

        decimal GetValue();

        string Summary { get; }
    }
}

