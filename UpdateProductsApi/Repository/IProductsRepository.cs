using DataAccessEntities.Model;
using System;
using System.Threading.Tasks;
using UpdateProductsApi.ModelMapping;

namespace UpdateProductsApi.Repository
{
    public interface IProductsRepository
    {
        Task<ProductEntity> CreateProduct(Product product);
        Task<ProductEntity> UpdateProduct(Product product);
        Task DeleteProduct(Guid id);
    }
}
