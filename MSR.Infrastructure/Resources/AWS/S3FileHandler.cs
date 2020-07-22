using Amazon.S3;
using Amazon.S3.Model;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using System.IO;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.AWS
{
    public class S3FileHandler : IUploadFiles, IDownloadFiles
    {
        IAmazonS3 _s3Handler;
        S3Information _s3Information;

        public S3FileHandler(IAmazonS3 s3Handler, S3Information s3Information)
        {
            _s3Handler = s3Handler;
            _s3Information = s3Information;
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var response = await _s3Handler.GetObjectAsync(new GetObjectRequest
            {
                BucketName = _s3Information.FileBucketName,
                Key = fileName
            });

            return response.ResponseStream;
        }

        public async Task<string> UploadFile(Domain.Models.File file, string entityName, int entityId)
        {
            string uniqueName = $"{entityName}-{entityId}-{file.Name}";
            var response = await _s3Handler.PutObjectAsync(new PutObjectRequest()
            {
                ContentBody = file.Base64String,
                ContentType = file.ContentType,
                BucketName = _s3Information.FileBucketName,
                Key = uniqueName
            });

            if((int)response.HttpStatusCode < 200 || (int)response.HttpStatusCode > 299)
            {
                throw new DomainException($"Attempt to Upload File: {file.Name} to S3 failed.");
            }

            return $"{_s3Information.AWSURL}{uniqueName}";
        }
    }
}
