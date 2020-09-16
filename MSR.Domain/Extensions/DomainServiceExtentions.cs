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

namespace MSR.Domain.Extensions
{
    public static class DomainServiceExtentions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<ISendSQSMessages, BusSender>();

            var s3Config = config.GetSection(nameof(S3Information)).Get<S3Information>();
            var sQSInformation = config.GetSection(nameof(SQSInformation)).Get<SQSInformation>();
            services.AddSingleton(sQSInformation);
            services.AddSingleton(s3Config);
            services.AddSingleton<IAmazonS3>(i => new AmazonS3Client(new BasicAWSCredentials(s3Config.AWSAccessKey, s3Config.AWSSecretKey), Amazon.RegionEndpoint.USEast1));
            services.AddSingleton<IAmazonSQS>(i => new AmazonSQSClient(s3Config.AWSAccessKey, s3Config.AWSSecretKey, Amazon.RegionEndpoint.USWest2));
            services.AddTransient<CustomerImportValidator>();
            services.AddTransient<LocationImportValidator>();
            services.AddTransient<PartValidator>();

            return services;
        }
    }
}
