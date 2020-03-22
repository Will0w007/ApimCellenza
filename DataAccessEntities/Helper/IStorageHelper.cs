using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntities.Helper
{
    public interface IStorageHelper
    {
        Task<CloudTable> GetTableStorage(string tableName);
    }
}
