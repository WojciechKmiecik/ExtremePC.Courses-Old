using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremePC.Courses.WebJobs.Services
{
    public class MailingService : IMailingService
    {
        private readonly ILogger<MailingService> logger;

        public MailingService(ILogger<MailingService> logger)
        {
            this.logger = logger;
        }

        public async Task SendConfirmationEmail()
        {
            logger.LogInformation("Confirmation Email sent");
            await Task.Run(() => { var a = 1; }); // stub
        }
        public async Task SendDenyEmail()
        {
            logger.LogInformation("Deny Email sent");
            await Task.Run(() => { var a = 0; }); // stub
        }

    }
}
