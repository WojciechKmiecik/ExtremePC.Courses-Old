using System;
using System.Threading.Tasks;
using ExtremePC.Courses.Models.ConfigOptions;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ExtremePC.Courses.Common.Services
{
    // this class should be extracted to somekind of ServicesPackage - like Services.MessageService - to make comunication proces easier.
    public class StorageAccountMessageService : IStorageAccountMessageService
    {
        private readonly string _connectionString;
        private readonly string _messageQueueName;

        public StorageAccountMessageService(IOptions<RegisterStudentJobOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _connectionString = options.Value.ConnectionString;
            _messageQueueName = options.Value.RegisterStudentQueueName;
        }
               
        private async Task<CloudQueue> CreateCloudQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue messageQueue = queueClient.GetQueueReference(_messageQueueName);
            return await Task.FromResult(messageQueue).ConfigureAwait(false); 
        }
        public async Task<bool> AddMessageToQueueAsync(string messageContent)
        {
            try
            {
                CloudQueueMessage message = new CloudQueueMessage(messageContent);
                var queue = await CreateCloudQueue().ConfigureAwait(false);
                var result = queue.AddMessageAsync(new CloudQueueMessage(messageContent)).ConfigureAwait(false);
                return await Task.FromResult<bool>(true).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult<bool>(false).ConfigureAwait(false);
            }
        }

        public async Task<string> ReadMessageFromQueueAsync()
        {
            try
            {
                var messageQueue = await CreateCloudQueue().ConfigureAwait(false);

                CloudQueueMessage retrievedMessage = await messageQueue.GetMessageAsync().ConfigureAwait(false);

                var returnString = retrievedMessage.AsString;

                await messageQueue.DeleteMessageAsync(retrievedMessage).ConfigureAwait(false);
                return await Task.FromResult(returnString).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return await Task.FromResult(string.Empty).ConfigureAwait(false);
            }
        }
    }
}
