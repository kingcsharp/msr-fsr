using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.AwsCloudWatch;
using System;

namespace MSR.Domain.Models.Config
{
    public class CloudWatchSinkOptions : ICloudWatchSinkOptions
    {
        public const LogEventLevel DefaultMinimumLogEventLevel = LogEventLevel.Information;

        public const int DefaultBatchSizeLimit = 100;

        public const int DefaultQueueSizeLimit = 10000;

        public const bool DefaultCreateLogGroup = true;

        public const byte DefaultRetryAttempts = 5;

        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(10);

        public LogEventLevel MinimumLogEventLevel { get; set; } = DefaultMinimumLogEventLevel;

        public int BatchSizeLimit { get; set; } = DefaultBatchSizeLimit;

        public int QueueSizeLimit { get; set; } = DefaultQueueSizeLimit;

        public TimeSpan Period { get; set; } = DefaultPeriod;

        public LogGroupRetentionPolicy LogGroupRetentionPolicy { get; set; } = LogGroupRetentionPolicy.Indefinitely;

        public bool CreateLogGroup { get; set; } = DefaultCreateLogGroup;

        public string LogGroupName { get; set; } = "answer3/api";

        public ILogStreamNameProvider LogStreamNameProvider { get; set; } = new DefaultLogStreamProvider();

        public ITextFormatter TextFormatter { get; set; }

        public byte RetryAttempts { get; set; } = DefaultRetryAttempts;
    }
}
