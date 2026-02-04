using System.Windows.Forms;

namespace FinancialPortfolioManager
{
    public partial class Form1 : Form
    {
        public Portfolio portfolio1 = new Portfolio();

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StockDisplayLabel_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Portfolio.ChangeCurrency();
            portfolioValueLabel.Text = portfolio1.GetTotalValue().ToString("N2") + " " + Portfolio.currency;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an investment type!");
                return;
            }

            if (string.IsNullOrWhiteSpace(tickerTextBox.Text) ||
                string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                string.IsNullOrWhiteSpace(priceTextBox.Text) ||
                string.IsNullOrWhiteSpace(amountTextBox.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (!decimal.TryParse(priceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Invalid price");
                return;
            }

            if (!decimal.TryParse(amountTextBox.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Invalid amount");
                return;
            }

            decimal totalCost = price * amount;

            if (!portfolio1.Withdraw(totalCost))
            {
                MessageBox.Show("Not enough cash!");
                return;
            }

            Investment investment;
            if (comboBox1.SelectedItem.ToString() == "Stock")
            {
                investment = new StockInvestment(
                    tickerTextBox.Text.Trim().ToUpper(),
                    nameTextBox.Text,
                    amount,
                    price
                );
            }
            else
            {
                investment = new CryptoInvestment(
                    tickerTextBox.Text.Trim().ToUpper(),
                    nameTextBox.Text,
                    amount,
                    price
                );
            }

            var tx = new Transaction(
                tickerTextBox.Text.Trim().ToUpper(),
                nameTextBox.Text,
                amount,
                price,
                investment.Type
            )
            {
                IsBuy = true
            };

            portfolio1.AddTransaction(tx, true);
            portfolio1.AddInvestment(investment);

            UpdateInvestmentsList();
            UpdateTransactionsList();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            portfolioValueLabel.Text =
                portfolio1.GetTotalValue().ToString("N2") + " " + Portfolio.currency;

            UpdateAllocationDisplay();
            UpdateInvestmentsList();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void UpdateInvestmentsList()
        {
            //investmentsListBox.Items.Clear();

            //investmentsListBox.Items.Add(
            //    $"Cash: {portfolio1.CashBalance:N2} {Portfolio.currency}"
            //);

            //foreach (var investment in portfolio1.GetInvestments())
            //{
            //    investmentsListBox.Items.Add(
            //        $"{investment.Name} | {investment.Amount} @ {investment.BuyPrice:N2} {Portfolio.currency}"
            //    );
            //}

            investmentsListBox.Items.Clear();


            investmentsListBox.Items.Add(
                $"Cash: {portfolio1.CashBalance:N2} {Portfolio.currency}"
            );

            foreach (var inv in portfolio1.GetInvestments())
            {
                investmentsListBox.Items.Add(
                    $"{inv.Ticker} | {inv.Amount} | Avg {inv.BuyPrice:N2} {Portfolio.currency}"
                );
            }
        }

        private void UpdateTransactionsList()
        {
            transactionsListBox.Items.Clear();

            foreach (var tx in portfolio1.GetTransactions())
            {
                string action = tx.IsBuy ? "BUY" : "SELL";
                transactionsListBox.Items.Add(
                    $"{tx.Date:dd.MM HH:mm} | {action} {tx.Ticker} x{tx.Amount} @ {tx.Price:N2} ({tx.TotalValue:N2})"
                );
            }
        }

        private void depositButton_Click(object sender, EventArgs e)
        {
            portfolio1.Deposit(numericUpDownAmount1.Value);
            UpdateInvestmentsList();
        }

        private void withdrawButton_Click(object sender, EventArgs e)
        {
            if (!portfolio1.Withdraw(numericUpDownAmount1.Value))
                MessageBox.Show("Not enough cash!");

            //portfolio1.Withdraw(numericUpDownAmount1.Value);
            UpdateInvestmentsList();
        }

        private void UpdateAllocationDisplay()
        {
            label1.Text =
                $"{portfolio1.GetPercentageByType(InvestmentType.Stock):N1}%";

            label2.Text =
                $"{portfolio1.GetPercentageByType(InvestmentType.Crypto):N1}%";

            label3.Text =
                $"{portfolio1.GetPercentageByType(InvestmentType.Cash):N1}%";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.Black)
            {
                this.BackColor = Color.White;
                groupBox1.BackColor = Color.White;
                groupBox2.BackColor = Color.White;
                groupBox3.BackColor = Color.White;
            }
            else
            {
                this.BackColor = Color.Black;
                groupBox1.ForeColor = Color.Black;
                groupBox2.ForeColor = Color.Black;
                groupBox3.ForeColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select an investment type!");
                return;
            }

            if (string.IsNullOrWhiteSpace(tickerTextBox.Text) ||
                string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                string.IsNullOrWhiteSpace(priceTextBox.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (!decimal.TryParse(priceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Invalid price");
                return;
            }

            if (!decimal.TryParse(amountTextBox.Text, out decimal amountToSell) || amountToSell <= 0)
            {
                MessageBox.Show("Invalid amount to sell.");
                return;
            }
            string ticker = tickerTextBox.Text.Trim().ToUpper();
            string name = nameTextBox.Text.Trim();
            string typeString = comboBox1.SelectedItem.ToString();

            // Find the investment to sell
            var investment = portfolio1.GetInvestments()
                .FirstOrDefault(inv => inv.Ticker == ticker && inv.Name == name && inv.Type.ToString() == typeString);

            if (investment == null)
            {
                MessageBox.Show("You do not own this investment.");
                return;
            }

            // Use a small tolerance for decimal comparison
            const decimal tolerance = 0.00001m;

            if (amountToSell <= 0 || amountToSell - investment.Amount > tolerance)
            {
                MessageBox.Show("Invalid amount to sell.");
                return;
            }

            // If selling (almost) all, treat as full sale
            if (Math.Abs(investment.Amount - amountToSell) < tolerance)
            {
                amountToSell = investment.Amount; // Ensure exact
                portfolio1.RemoveInvestment(investment);
            }
            else
            {
                investment.Amount -= amountToSell;
            }

            // Add cash to portfolio
            decimal totalProceeds = price * amountToSell;
            portfolio1.Deposit(totalProceeds);

            // Add transaction record
            var tx = new Transaction(ticker, name, amountToSell, price, investment.Type)
            {
                IsBuy = false
            };
            portfolio1.AddTransaction(tx, false);

            UpdateInvestmentsList();
            UpdateTransactionsList();
        }
    }
}
