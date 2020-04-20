using DataAccessEntities.Helper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NotificationService;
using RetrieveProductsApi.Repository;
using System;

[assembly: FunctionsStartup(typeof(RetrieveProductsApi.Startup))]
namespace RetrieveProductsApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IStorageHelper>((options) =>
            new StorageHelper(Environment.GetEnvironmentVariable("StorageConnectionString", EnvironmentVariableTarget.Process)));
            builder.Services.Configure<NotificationHubSettings>(c =>
            {
                c.ConnectionString = Environment.GetEnvironmentVariable("HubConnectionString", EnvironmentVariableTarget.Process);
                c.NotificationHubName = Environment.GetEnvironmentVariable("HubName", EnvironmentVariableTarget.Process);
            });
            builder.Services.AddSingleton<INotificationHubService, NotificationHubService>();
            builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
        }
    }
}
