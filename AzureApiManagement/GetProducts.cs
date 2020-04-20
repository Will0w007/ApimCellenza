using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RetrieveProductsApi.Repository;
using NotificationService;
using System.Linq;

namespace RetrieveProductsApi
{
    public class GetProducts
    {
        private readonly IProductsRepository _productsRepository;
        private readonly INotificationHubService _notificationHubService;
        public GetProducts(IProductsRepository productsRepository, INotificationHubService notificationHubService)
        {
            this._productsRepository = productsRepository;
            this._notificationHubService = notificationHubService;
        }
        [FunctionName("GetProducts")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Products")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var products = await _productsRepository.GetProducts();
            var pushedMessage = $"Produit : {products.FirstOrDefault().RowKey}";
            var result = await _notificationHubService.PushNotification(pushedMessage);
            Console.WriteLine(result);
          
            return new OkObjectResult(products);
        }
    }
}
