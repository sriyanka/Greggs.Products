using FluentAssertions;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Get_WhenNotInEuros()
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
                        new Api.Results.Product {Name = "Sausage Roll", Price = 1m},
                        new Api.Results.Product {Name = "Vegan Sausage Roll", Price = 1.1m},
                        new Api.Results.Product {Name = "Steak Bake", Price = 1.2m}
                    };

            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(s => s.List(pageStart, pageSize)).Returns(products);

            var controller = new ProductController(mockDataAccess.Object, null);

            //Act
            var actual = controller.Get(pageStart, pageSize);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Get_WhenInEuros()
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
                        new Api.Results.Product {Name = "Sausage Roll", Price = 1.11m},
                        new Api.Results.Product {Name = "Vegan Sausage Roll", Price = 1.221m},
                        new Api.Results.Product {Name = "Steak Bake", Price = 1.332m}
                    };

            var mockDataAccess = new Mock<IDataAccess<Product>>();
            mockDataAccess.Setup(s => s.List(pageStart, pageSize)).Returns(products);

            var controller = new ProductController(mockDataAccess.Object, null);

            //Act
            var actual = controller.Get(pageStart, pageSize, true);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
