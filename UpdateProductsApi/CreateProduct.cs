using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UpdateProductsApi.Repository;
using UpdateProductsApi.ModelMapping;

namespace UpdateProductsApi
{
    public class CreateProduct
    {
        private readonly IProductsRepository _productsRepository;
        public CreateProduct(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        [FunctionName("CreateProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(requestBody);
            var result = await _productsRepository.CreateProduct(product);

            return new OkObjectResult(result);
        }
    }
}
