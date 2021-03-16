using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Business
{
    public interface ICurrencyConverter
    {
        decimal GetCurrencyValue(decimal priceInPounds, CurrencyType currency);
    }
}
