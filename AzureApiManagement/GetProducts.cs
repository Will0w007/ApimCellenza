using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RetrieveProductsApi.Repository;

namespace RetrieveProductsApi
{
    public class GetProducts
    {
        private readonly IProductsRepository _productsRepository;
        public GetProducts(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }
        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var products = await _productsRepository.GetProducts();
          
            return new OkObjectResult(products);
        }
    }
}
