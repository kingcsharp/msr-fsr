using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.SQSEventing;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Domain.Models.Config;
using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon.Runtime;
using MSR.Domain.Validators;
using Amazon.CloudWatchLogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs.Model;
using System.Linq;
using Serilog.Sinks.AwsCloudWatch;

namespace MSR.Domain.Extensions
{
    public static class DomainServiceExtentions
    {
        public static async Task createLogGroupInAWSCloudWatch(IAmazonCloudWatchLogs cloudWatchClient, string logGroupName)
        {
            var tags = new Dictionary<string, string>
            {
                { "Client", "MSR-FSR" },
                { "Application", "Answer" }
            };
            var describeLogGroupsRequest = new DescribeLogGroupsRequest
            {
                LogGroupNamePrefix = logGroupName
            };

            var describeLogGroupsResponse = await cloudWatchClient.DescribeLogGroupsAsync(describeLogGroupsRequest);

            bool isExistLogGroup = describeLogGroupsResponse.LogGroups.Any(group => group.LogGroupName == logGroupName);

            if (isExistLogGroup == false)
            {
                var createLogGroupRequest = new CreateLogGroupRequest
                {
                    LogGroupName = logGroupName,
                    Tags = tags
                };
                await cloudWatchClient.CreateLogGroupAsync(createLogGroupRequest);
            }

            var retentionPolicyRequest = new PutRetentionPolicyRequest
            {
                LogGroupName = logGroupName,
                RetentionInDays = (int)LogGroupRetentionPolicy.TwoWeeks
            };

            await cloudWatchClient.PutRetentionPolicyAsync(retentionPolicyRequest);
        }
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<ISendSQSMessages, BusSender>();

            var s3Config = config.GetSection(nameof(S3Information)).Get<S3Information>();
            var sQSInformation = config.GetSection(nameof(SQSInformation)).Get<SQSInformation>();
            var SQSXMLInformation = config.GetSection(nameof(XMLSQSInformation)).Get<XMLSQSInformation>();
            services.AddSingleton(sQSInformation);
            services.AddSingleton(SQSXMLInformation);
            services.AddSingleton(s3Config);
            services.AddSingleton<IAmazonS3>(i => new AmazonS3Client(new BasicAWSCredentials(s3Config.AWSAccessKey, s3Config.AWSSecretKey), Amazon.RegionEndpoint.USEast1));
            services.AddSingleton<IAmazonSQS>(i => new AmazonSQSClient(s3Config.AWSAccessKey, s3Config.AWSSecretKey, Amazon.RegionEndpoint.USWest2));
            services.AddSingleton<IAmazonCloudWatchLogs>(i => new AmazonCloudWatchLogsClient(s3Config.AWSAccessKey, s3Config.AWSSecretKey, Amazon.RegionEndpoint.USWest2));
            services.AddTransient<CustomerImportValidator>();
            services.AddTransient<LocationImportValidator>();
            services.AddTransient<PartValidator>();
            services.AddTransient<ProcedureValidator>();
            services.AddTransient<QuoteImportValidator>();
            services.AddTransient<CycleCountImportValidator>();

            return services;
        }
    }
}
