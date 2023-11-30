using System;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.S3;
using MSR.Domain.Abstractions.Services;

namespace MSR.Infrastructure.Resources.Services.Aws3Service
{
    public class Aws3Service : IAws3Service
    {
		private readonly string _bucketName;
		private readonly IAmazonS3 _awsS3Client;

		public Aws3Service(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, string region, string bucketName)
		{
			_bucketName = bucketName;
			_awsS3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, RegionEndpoint.GetBySystemName(region));
		}
	}
}
