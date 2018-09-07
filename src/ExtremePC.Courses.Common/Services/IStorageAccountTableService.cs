using System.Threading.Tasks;
using ExtremePC.Courses.Common.Entities;

namespace ExtremePC.Courses.Common.Services
{
    public interface IStorageAccountTableService
    {
        string PARTITION_KEY { get; }
        Task<T> GetFromTableStorageAsync<T>(string key);
        Task<bool> UpsertToTableStorageAsync<T>(StatisticsTableEntity<T> entity);
    }
}