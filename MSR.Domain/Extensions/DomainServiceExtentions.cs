using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Intigration;
using MSR.Domain.Intigration.Abstractions;
using MSR.Domain.Models.Config;
using Microsoft.Extensions.Configuration;
using Amazon.S3;
using Amazon.Runtime;

namespace MSR.Domain.Extensions
{
    public static class DomainServiceExtentions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<ISendSQSMessages, BusSender>();
            services.AddScoped<IProcessSQSMessages, BusTerminal>();

            var s3Config = config.GetSection(nameof(S3Information)).Get<S3Information>();
            var sQSInformation = config.GetSection(nameof(SQSInformation)).Get<SQSInformation>();
            services.AddSingleton(sQSInformation);
            services.AddSingleton(s3Config);
            services.AddScoped<IAmazonS3>(i => new AmazonS3Client(new BasicAWSCredentials(s3Config.AWSAccessKey, s3Config.AWSSecretKey), Amazon.RegionEndpoint.USEast1));
            services.AddScoped<IAmazonSQS>(i => new AmazonSQSClient(s3Config.AWSAccessKey, s3Config.AWSSecretKey, Amazon.RegionEndpoint.USEast1));

            return services;
        }
    }
}
