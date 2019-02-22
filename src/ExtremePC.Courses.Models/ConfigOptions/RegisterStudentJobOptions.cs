namespace ExtremePC.Courses.Models.ConfigOptions
{
    public class RegisterStudentJobOptions
    {
        public const string RegisterStudentQueueKey = "registerstudentqueue";
        public const string RegisterStudentQueue = "%" + RegisterStudentQueueKey + "%";
        public string ConnectionString { get; set; } = string.Empty;
        public int VisibilityTimeoutInSeconds { get; set; } = 30;
        public int MaxPollingIntervalInSeconds { get; set; } = 30;
        public int BatchSize { get; set; } = 16;
        public string RegisterStudentQueueName { get; set; }
        public int MaxDequeueCount { get; set; } = 3;
        public string StatisticsTableName { get; set; }

    }
}
