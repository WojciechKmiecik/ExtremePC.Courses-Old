using ExtremePC.Courses.Models.ConfigOptions;
using ExtremePC.Courses.WebJobs.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebJobs
{
    public class WebJob
    {
        private readonly ILogger<WebJob> _logger;
        private readonly IRegisterStudentService _registerStudentService;
        private readonly IStatisticsCalculationService _statisticsService;
        private readonly CloudStorageAccount _cloudStorageAccount;
        public RegisterStudentJobOptions _jobOptions;

        public WebJob(ILogger<WebJob> logger, IOptions<RegisterStudentJobOptions> options, IRegisterStudentService registerStudentService, IStatisticsCalculationService statisticsService)
        {
            _logger = logger;
            _registerStudentService = registerStudentService;
            _statisticsService = statisticsService;
            _jobOptions = options?.Value;
            _cloudStorageAccount = CloudStorageAccount.Parse(_jobOptions.ConnectionString);
        }

        public async Task CalculateStatistics([TimerTrigger("0 */30 * * * *", RunOnStartup = true)] TimerInfo timerInfo, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempt to calculate statistics");

            await Task.WhenAll(_statisticsService.CalculateCapacityStatistics(),
                _statisticsService.CalculateDetailsStatistics()).ConfigureAwait(false);

            _logger.LogInformation("Attempt to calculate statistics");

        }

        [Timeout("00:02:00", TimeoutWhileDebugging = true)]
        [StorageAccount("registerstudentqueue")]
        public async Task RegisterStudentAsync([QueueTrigger(RegisterStudentJobOptions.RegisterStudentQueue)] CloudQueueMessage message,
            int dequeueCount, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Got message from queue");

            try
            {
                var signupStudent = await _registerStudentService.DeserializeMessage(message.AsString).ConfigureAwait(false);
                if (signupStudent == null)
                {
                    throw new NullReferenceException("Object was null - either empty or deserialized inproperly");
                }
                var signupResult = await _registerStudentService.RegisterStudent(signupStudent).ConfigureAwait(false);
                if (signupResult == false)
                {
                    throw new Exception("unable to signup the student");
                }

            }
            catch (NullReferenceException nre)
            {
                _logger.LogError(nre.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            _logger.LogInformation("Processing message finished");
        }
    }
}
