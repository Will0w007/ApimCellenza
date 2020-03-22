using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace DataAccessEntities.Helper
{
    public class StorageHelper : IStorageHelper
    {
        private CloudStorageAccount _tableStorage;
        private CloudTableClient _tableClient;

        public StorageHelper(string connectionString)
        {
            _tableStorage = CloudStorageAccount.Parse(connectionString);
            _tableClient = _tableStorage.CreateCloudTableClient();
        }

        public async Task<CloudTable> GetTableStorage(string tableName)
        {
            var table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }        
    }
}
