using FluentAssertions;
using Greggs.Products.Api.Business;
using Greggs.Products.Api.Models;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void GetCurrencyValue_WhenPounds()
        {
            //Arrange
            var price = 1.12m;
            var currencyConverter = new CurrencyConverter();

            //Act
            var actual = currencyConverter.GetCurrencyValue(price, CurrencyType.Pounds);

            //Assert
            actual.Should().Be(1.12m);
        }

        [Fact]
        public void GetCurrencyValue_WhenEuros()
        {
            //Arrange
            var price = 1.12m;
            var currencyConverter = new CurrencyConverter();

            //Act
            var actual = currencyConverter.GetCurrencyValue(price, CurrencyType.Euros);

            //Assert
            actual.Should().Be(1.2432m);
        }
    }
}
