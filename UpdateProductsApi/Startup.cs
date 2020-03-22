using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UpdateProductsApi.Repository;
using System;
using DataAccessEntities.Helper;

[assembly: FunctionsStartup(typeof(UpdateProductsApi.Startup))]
namespace UpdateProductsApi
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
