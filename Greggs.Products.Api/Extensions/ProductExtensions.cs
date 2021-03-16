using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Greggs.Products.Api.Extensions
{
    public static class ProductExtensions
    {
        private const decimal exchangeRate = 1.11m;

        public static IEnumerable<Results.Product> GetResult(this IEnumerable<Product> products, bool inEuros)
        {
            return products.Select(product => new Results.Product
            {
                Name = product.Name,
                Price = product.PriceInPounds * (inEuros ? exchangeRate : 1)
            });
        }
    }
}
