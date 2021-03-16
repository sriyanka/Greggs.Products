using FluentAssertions;
using Greggs.Products.Api.Business;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Get_WhenValidCurrency_ReturnsResult()
        {
            //Arrange
            var pageStart = 1;
            var pageSize = 10;
            var products = new List<Product>()
                    {
                        new Product {Name = "Sausage Roll", PriceInPounds = 1m},
                        new Product {Name = "Vegan Sausage Roll", PriceInPounds = 1.1m},
                        new Product {Name = "Steak Bake", PriceInPounds = 1.2m}
                    };

            var expected = new List<Api.Results.Product>()
                    {
                        new Api.Results.Product {Name = "Sausage Roll", Price = 2m},
                        new Api.Results.Product {Name = "Vegan Sausage Roll", Price = 2.2m},
                        new Api.Results.Product {Name = "Steak Bake", Price = 12.2m}
                    };

            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(s => s.List(pageStart, pageSize)).Returns(products);

            var mockCurrencyConverter = new Mock<ICurrencyConverter>();
            mockCurrencyConverter.Setup(s => s.GetCurrencyValue(1m, CurrencyType.Euros)).Returns(2m);
            mockCurrencyConverter.Setup(s => s.GetCurrencyValue(1.1m, CurrencyType.Euros)).Returns(2.2m);
            mockCurrencyConverter.Setup(s => s.GetCurrencyValue(1.2m, CurrencyType.Euros)).Returns(12.2m);

            var controller = new ProductController(mockDataAccess.Object, mockCurrencyConverter.Object, null);

            //Act
            var actual = controller.Get(pageStart, pageSize, "Euros");

            //Assert
            actual.Should().BeOfType<OkObjectResult>();

            ((ObjectResult)actual).Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Get_WhenInValidCurrency_ReturnsBadRequest()
        {
            //Arrange
            var pageStart = 1;
            var pageSize = 10;

            var controller = new ProductController(null, null, null);

            //Act
            var actual = controller.Get(pageStart, pageSize, "Invalid");

            //Assert
            actual.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Get_WhenException_ReturnsBadRequest()
        {
            //Arrange
            var pageStart = 1;
            var pageSize = 10;

            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(s => s.List(pageStart, pageSize)).Throws(new Exception());

            var controller = new ProductController(mockDataAccess.Object, null, null);

            //Act
            var actual = controller.Get(pageStart, pageSize, "Pounds");

            //Assert
            actual.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
