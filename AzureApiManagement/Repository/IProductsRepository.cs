using DataAccessEntities.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RetrieveProductsApi.Repository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
        Task<ProductEntity> GetProductById(Guid id);
    }
}
