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

namespace UpdateProductsApi
{
    public class DeleteProduct
    {
        private readonly IProductsRepository _productsRepository;
        public DeleteProduct(IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        [FunctionName("DeleteProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Products/{id}")] HttpRequest req,
            ILogger log, Guid id)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

             await _productsRepository.DeleteProduct(id);

            return new  OkResult();
        }
    }
}
