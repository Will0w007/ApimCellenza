using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RetrieveProductsApi.Repository;

namespace RetrieveProductsApi
{
    public class GetProduct
    {
        private readonly IProductsRepository _productsRepository;
        public GetProduct(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }
        [FunctionName("GetProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Products/{id}")] HttpRequest req,
            ILogger log, Guid id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var products = await _productsRepository.GetProductById(id);

            return new OkObjectResult(products);
        }
    }
}
