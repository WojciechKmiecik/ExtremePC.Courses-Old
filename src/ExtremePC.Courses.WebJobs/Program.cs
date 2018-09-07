using ExtremePC.Courses.WebJobs.Resolvers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.Azure.WebJobs.Host.Executors;
using ExtremePC.Courses.Models.ConfigOptions;
using ExtremePC.Courses.Common.Services;
using ExtremePC.Courses.WebJobs.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using ExtremePC.Courses.Database;
using ExtremePC.Courses.Database.Directory;

namespace ExtremePC.Courses.WebJobs
{
    internal class Program
    {
        private static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var provider = services.BuildServiceProvider();

            ConfigureLogging(services, provider);
            ConfigureServiceAccount(provider);
        }

        private static void ConfigureLogging(ServiceCollection services, ServiceProvider provider)
        {
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
            var logConfig = Configuration.GetSection("Logging");
            loggerFactory.AddConsole(logConfig);
            if (Debugger.IsAttached)
            {
                loggerFactory.AddDebug(LogLevel.Debug);
            }
            loggerFactory.AddFile("Logs/ExtremePC.Courses.WebJobs-{Date}.txt");
            
        }

        private static void ConfigureServiceAccount(ServiceProvider provider)
        {
            var environment = provider.GetRequiredService<IHostingEnvironment>();
            //ConfigureLoggingAndTelemetry(provider, environment); // Same story -> If in azure, then to configure
            var registerStudentJobOptions = provider.GetService<IOptions<RegisterStudentJobOptions>>().Value;
            var nameResolverDictionary = CreateNameResolverDictionary(registerStudentJobOptions);

            var config = new JobHostConfiguration(registerStudentJobOptions.ConnectionString)
            {
                NameResolver = new ConfigNameResolver(nameResolverDictionary),
                JobActivator = new WebJobSafeActivator(provider),
                Queues =
                {
                    MaxDequeueCount = registerStudentJobOptions.MaxDequeueCount,
                    MaxPollingInterval = TimeSpan.FromSeconds(registerStudentJobOptions.MaxPollingIntervalInSeconds),
                    VisibilityTimeout = TimeSpan.FromSeconds(registerStudentJobOptions.VisibilityTimeoutInSeconds),
                    BatchSize = registerStudentJobOptions.BatchSize
                }
            };
            

            if (Debugger.IsAttached)
            {
                config.UseDevelopmentSettings();
            }

            config.UseTimers();

            var jobHost = new JobHost(config);

            jobHost.RunAndBlock();


        }
        private static Dictionary<string, string> CreateNameResolverDictionary(RegisterStudentJobOptions registerStudentJobOptions)
        {
            return
                new Dictionary<string, string>
                {
                    [RegisterStudentJobOptions.RegisterStudentQueueKey] = registerStudentJobOptions.RegisterStudentQueueName
                };
        }
        public static ServiceCollection ConfigureServices()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            configBuilder.AddEnvironmentVariables();



            Configuration = configBuilder.Build();

            // rebuild specific config for environment (e.g. development, test, acc, prod)
            if (!string.IsNullOrEmpty(Configuration["ASPNETCORE_ENVIRONMENT"]))
            {
                configBuilder.AddJsonFile($"appsettings.{Configuration["ASPNETCORE_ENVIRONMENT"]}.json", true);
                Configuration = configBuilder.Build();
            }

            var hostingEnv = new Microsoft.Extensions.Hosting.Internal.HostingEnvironment
            {
                EnvironmentName = Configuration["ASPNETCORE_ENVIRONMENT"]
            };


            var services = new ServiceCollection();


            services.AddOptions()
                
                .AddSingleton<IHostingEnvironment>(provider => hostingEnv);
            //.AddMetricsLogger()   // If it will be on azure then i would install those packages
            //.AddApplicationInsightsTelemetry(Configuration);
            

            RegisterIoc(services, Configuration);

            return services;
        }

        private static void RegisterIoc(ServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<RegisterStudentJobOptions>(Configuration.GetSection("RegisterStudentJobOptions"));
            services.AddDbContext<CoursesDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IStorageAccountMessageService, StorageAccountMessageService>();
            services.AddTransient<IStorageAccountTableService, StorageAccountTableService>();
            services.AddTransient<IMailingService, MailingService>();
            
            services.AddTransient<IRegisterStudentService, RegisterStudentService>();
            services.AddTransient<IStatisticsTableRepository, StatisticsTableRepository>();
            services.AddTransient<IStatisticsCalculationService, StatisticsCalculationService>();
            services.TryAdd(ServiceDescriptor.Transient<ILoggerFactory, LoggerFactory>());
            services.TryAdd(ServiceDescriptor.Transient(typeof(ILogger<>), typeof(Logger<>)));
            services.AddTransient<WebJob>();
        }
    }
}
