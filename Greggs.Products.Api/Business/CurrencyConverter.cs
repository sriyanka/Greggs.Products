using Greggs.Products.Api.Models;
using System;

namespace Greggs.Products.Api.Business
{
    public class CurrencyConverter : ICurrencyConverter
    {
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
