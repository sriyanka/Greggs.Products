using Greggs.Products.Api.Business;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Greggs.Products.Api.Controllers
{
    /// <summary>
    /// Controller to get product information.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDataAccess<Product> _repository;
        private readonly ICurrencyConverter _currencyConverter;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IDataAccess<Models.Product> repository, ICurrencyConverter currencyConverter, ILogger<ProductController> logger)
        {
            _logger = logger;
            _repository = repository;
            _currencyConverter = currencyConverter;
        }

        /// <summary>
        /// The endpoint to get product information.
        /// </summary>
        /// <param name="pageStart">The start page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="currency">The currencyType</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Get(int pageStart = 0, int pageSize = 5, string currency = "Pounds")
        {
            try
            {
                _logger?.LogInformation($"Getting Product Data for pageStart: {pageStart} and pageSize: {pageSize}");

                if (!Enum.TryParse(currency, out CurrencyType currencyType))
                    return BadRequest("Unsupported currency type.");

                return Ok(
                    _repository
                    .List(pageStart, pageSize)
                    .Select(product => new Results.Product
                        {
                            Name = product.Name,
                            Price = _currencyConverter.GetCurrencyValue(product.PriceInPounds, currencyType)
                        })
                    );
            }
            catch (Exception exception)
            {
                _logger?.LogError($"Exception occured: {exception.Message}");
                return BadRequest(exception.Message);
            }
        }
    }
}
