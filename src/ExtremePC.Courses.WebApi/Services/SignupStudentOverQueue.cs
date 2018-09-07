using ExtremePC.Courses.Common.Services;
using ExtremePC.Courses.Contracts;
using ExtremePC.Courses.Models.ConfigOptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebApi.Services
{
    public class SignupStudentOverQueue : ISignupStudentOverQueue
    {
        private readonly IStorageAccountMessageService _messageService;
        private ILogger<SignupStudentOverQueue> _logger;
        private RegisterStudentJobOptions registerStudentJobOptions;
        public SignupStudentOverQueue(IOptions<RegisterStudentJobOptions> options, IStorageAccountMessageService messageService, ILogger<SignupStudentOverQueue> logger)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            registerStudentJobOptions = options.Value;
            _messageService = messageService;
            _logger = logger;
        }
        public async Task<bool> SignUpOverQueueAsync(SignupStudent student)
        {
            try
            {
                _logger.LogInformation("Attempt to publish on queue");
                var messageContent = JsonConvert.SerializeObject(student);
                var result = await _messageService.AddMessageToQueueAsync(messageContent).ConfigureAwait(false);
                _logger.LogInformation("Publish successful");
                return await Task.FromResult<bool>(result).ConfigureAwait(false);
            }
            catch (Exception)
            {
                _logger.LogError("Can't publish on queue");
                return await Task.FromResult<bool>(false).ConfigureAwait(false);
            }
        }
    }
}
