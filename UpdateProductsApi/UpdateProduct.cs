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
    public class UpdateProduct
    {
        private readonly IProductsRepository _productsRepository;
        public UpdateProduct(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }
        [FunctionName("UpdateProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "Products")] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(requestBody);
            var result = await _productsRepository.UpdateProduct(product);

            return new OkObjectResult(result);

        }
    }
}
