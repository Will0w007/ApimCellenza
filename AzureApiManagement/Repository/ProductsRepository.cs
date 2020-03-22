using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessEntities.Helper;
using DataAccessEntities.Model;
using Microsoft.WindowsAzure.Storage.Table;

namespace RetrieveProductsApi.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IStorageHelper _storageHelper;
        public ProductsRepository(IStorageHelper storageHelper)
        {
            _storageHelper = storageHelper;
        }

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            var table = await _storageHelper.GetTableStorage("Products");
            TableOperation operation = TableOperation.Retrieve<ProductEntity>(id.ToString(), id.ToString());

            TableResult result = await table.ExecuteAsync(operation);

            return (ProductEntity)result.Result;
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts()
        {
            var table = await _storageHelper.GetTableStorage("Products");
            TableQuery<ProductEntity> query = new TableQuery<ProductEntity>();

            List<ProductEntity> results = new List<ProductEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<ProductEntity> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }
    }
}
