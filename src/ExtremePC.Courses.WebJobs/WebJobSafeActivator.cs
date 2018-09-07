using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremePC.Courses.WebJobs
{
    internal class WebJobSafeActivator : IJobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public WebJobSafeActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateInstance<T>()
        {
            try
            {
                return _serviceProvider.GetRequiredService<T>();

            }
            catch (Exception ex)
            {
                var logger = _serviceProvider.GetRequiredService<ILogger>();
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
