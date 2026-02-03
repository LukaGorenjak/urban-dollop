using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialPortfolioManager
{
    internal class StockInvestment : Investment
    {
        public override InvestmentType Type => InvestmentType.Stock;

        public StockInvestment(string ticker, string name, decimal amount, decimal buyPrice)
            : base(name, amount, buyPrice)
        {
            Ticker = ticker;
        }

        public override decimal GetValue()
        {
            return CurrentPrice * Amount;
        }
    }
}
