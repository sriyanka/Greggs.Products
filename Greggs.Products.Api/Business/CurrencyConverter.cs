using Greggs.Products.Api.Models;
using System;

namespace Greggs.Products.Api.Business
{
    /// <summary>
    /// Concrete implementation of currency converter.
    /// </summary>
    public class CurrencyConverter : ICurrencyConverter
    {
        /// <summary>
        /// Gets the equivalent currency value.
        /// </summary>
        /// <param name="priceInPounds">The price in pounds.</param>
        /// <param name="currency">The curreny type to be converted to.</param>
        /// <returns>The converted value.</returns>
        public decimal GetCurrencyValue(decimal priceInPounds, CurrencyType currency)
        {
            switch (currency)
            {
                case CurrencyType.Pounds:
                    return priceInPounds;

                case CurrencyType.Euros:
                    return priceInPounds * 1.11m;

                default:
                    throw new Exception("Type not supported");
            }
        }
    }
}
