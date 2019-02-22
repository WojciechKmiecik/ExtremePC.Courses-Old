using ExtremePC.Courses.Common.Entities;
using ExtremePC.Courses.Models.ConfigOptions;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.Threading.Tasks;

namespace ExtremePC.Courses.Common.Services
{
    public class StorageAccountTableService : IStorageAccountTableService
    {
        public string PARTITION_KEY { get; } = "statistics";
        private readonly RegisterStudentJobOptions _options;
        private CloudTable _statisticsCloudTable;

        public StorageAccountTableService(IOptions<RegisterStudentJobOptions> options)
        {
            _options = options.Value;

        }

        private async Task CreateCloudTableAccess()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            _statisticsCloudTable = tableClient.GetTableReference(_options.StatisticsTableName);
            await _statisticsCloudTable.CreateIfNotExistsAsync().ConfigureAwait(false);
        }

        public async Task<T> GetFromTableStorageAsync<T>(string key)
        {
            if (_statisticsCloudTable == null)
            {
                await CreateCloudTableAccess().ConfigureAwait(false);
            }
            try
            {
                var retrieveOperation = TableOperation.Retrieve<StatisticsTableEntity<T>>(PARTITION_KEY, key);

                var result = await _statisticsCloudTable.ExecuteAsync(retrieveOperation).ConfigureAwait(false);
                if (IsCorrectStatusCode(result.HttpStatusCode) && result.Result is StatisticsTableEntity<T> entity)
                {
                    return entity.Value;
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        public async Task<bool> UpsertToTableStorageAsync<T>(StatisticsTableEntity<T> entity)
        {
            if (_statisticsCloudTable == null)
            {
                await CreateCloudTableAccess().ConfigureAwait(false);
            }
            try
            {
                var upsertOperation = TableOperation.InsertOrReplace(entity);

                var result = await _statisticsCloudTable.ExecuteAsync(upsertOperation).ConfigureAwait(false);

                return await Task.FromResult(result != null && IsCorrectStatusCode(result.HttpStatusCode));
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        private bool IsCorrectStatusCode(int httpStatusCode)
        {
            return (httpStatusCode >= (int)HttpStatusCode.OK && httpStatusCode <= (int)HttpStatusCode.PartialContent);
        }

    }
}
