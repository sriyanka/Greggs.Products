using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Business
{
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Gets the equivalent currency value.
        /// </summary>
        /// <param name="priceInPounds">The price in pounds.</param>
        /// <param name="currency">The curreny type to be converted to.</param>
        /// <returns>The converted value.</returns>
        decimal GetCurrencyValue(decimal priceInPounds, CurrencyType currency);
    }
}
