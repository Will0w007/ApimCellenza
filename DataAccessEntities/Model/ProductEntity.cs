using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace DataAccessEntities.Model
{
    public class ProductEntity : TableEntity
    {
        public ProductEntity(Guid productId)
        {
            this.PartitionKey = productId.ToString();
            this.RowKey = productId.ToString();
        }

        public ProductEntity()
        {
        }

        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public double? ProductPrice { get; set; }
        public string ProductDescription { get; set; }
    }
}
