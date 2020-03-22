using DataAccessEntities.Helper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
            builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
        }
    }
}
