using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialPortfolioManager
{
    public class Transaction
    {
        public DateTime Date { get; }
        public string Ticker { get; }
        public string Name { get; }
        public decimal Amount { get; }
        public decimal Price { get; }
        public InvestmentType Type { get; }

        public Transaction(string ticker, string name, decimal amount, decimal price, InvestmentType type)
        {
            Date = DateTime.Now;
            Ticker = ticker;
            Name = name;
            Amount = amount;
            Price = price;
            Type = type;
        }

        public decimal TotalValue => Amount * Price;

        public bool IsBuy { get; set; }
    }
}
