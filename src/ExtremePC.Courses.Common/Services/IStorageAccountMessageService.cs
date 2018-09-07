using System.Threading.Tasks;

namespace ExtremePC.Courses.Common.Services
{
    public interface IStorageAccountMessageService
    {
        Task<bool> AddMessageToQueueAsync(string messageContent);
        Task<string> ReadMessageFromQueueAsync();
    }
}