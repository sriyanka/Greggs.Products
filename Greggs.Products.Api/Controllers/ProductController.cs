using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Extensions;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Greggs.Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDataAccess<Product> _repository;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IDataAccess<Models.Product> repository, ILogger<ProductController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<Results.Product> Get(int pageStart = 0, int pageSize = 5, bool inEuros = false)
        {
            try
            {
                _logger?.LogInformation($"Getting Product Data for pageStart: {pageStart} and pageSize: {pageSize}");
                return _repository.List(pageStart, pageSize).GetResult(inEuros);
            }
            catch(Exception exception)
            {
                _logger.LogError($"Exception occured: {exception.Message}");
                throw; 
            }
        }
    }
}
