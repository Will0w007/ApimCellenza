using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccessEntities.Helper;
using DataAccessEntities.Model;
using Microsoft.WindowsAzure.Storage.Table;
using UpdateProductsApi.ModelMapping;

namespace UpdateProductsApi.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IStorageHelper _storageHelper;
        public ProductsRepository(IStorageHelper storageHelper)
        {
            _storageHelper = storageHelper;
        }

        public async Task<ProductEntity> CreateProduct(Product product)
        {
            var table = await _storageHelper.GetTableStorage("Products");
            var entity = new ProductEntity(Guid.NewGuid())
            {
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductPrice = product.Price,
                ProductCategory = product.Category
            };
            TableOperation operation = TableOperation.Insert(entity);

            var response = await table.ExecuteAsync(operation);

            return (ProductEntity)response.Result;

        }

        public async Task<ProductEntity> UpdateProduct(Product product)
        {
            var table = await _storageHelper.GetTableStorage("Products");
            var existingEntity = await GetProduct(product.Id.Value, table);
            var entity = new ProductEntity(product.Id.Value)
            {
                ProductName = product.Name ?? existingEntity.ProductName,
                ProductDescription = product.Description ?? existingEntity.ProductDescription,
                ProductPrice = product.Price ?? existingEntity.ProductPrice,
                ProductCategory = product.Category ?? existingEntity.ProductCategory
            };
            //Operation  
            TableOperation operation = TableOperation.InsertOrReplace(entity);

            //Execute  
            var response =  await table.ExecuteAsync(operation);

            return (ProductEntity)response.Result;
        }

        public async Task DeleteProduct(Guid id)
        {
            var table = await _storageHelper.GetTableStorage("Products");
            var product = await GetProduct(id, table);
            TableOperation operation = TableOperation.Delete(product);
            await table.ExecuteAsync(operation);
        }

        private async Task<ProductEntity> GetProduct(Guid id, CloudTable table)
        {
            TableOperation operation = TableOperation.Retrieve<ProductEntity>(id.ToString(), id.ToString());
            TableResult result = await table.ExecuteAsync(operation);
            return (ProductEntity)result.Result;
        }
    }
}
