using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialPortfolioManager
{
    internal class CryptoInvestment : Investment
    {
        public override InvestmentType Type => InvestmentType.Crypto;

        public CryptoInvestment(string ticker, string name, decimal amount, decimal buyPrice)
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
