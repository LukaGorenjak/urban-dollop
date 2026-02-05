using System;

namespace FinancialPortfolioManager
{
    /// <summary>
    /// Vmesnik, ki predstavlja objekt z denarno vrednostjo.
    /// </summary>
    public interface IValuable
    {
        /// <summary>
        /// Vrne trenutno vrednost objekta.
        /// </summary>
        decimal GetValue();

        /// <summary>
        /// Kratek opis za prikaz na uporabniškem vmesniku.
        /// </summary>
        string Summary { get; }
    }
}

