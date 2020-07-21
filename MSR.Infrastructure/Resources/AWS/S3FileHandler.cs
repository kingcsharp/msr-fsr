using Amazon.S3;
using Amazon.S3.Model;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Models.Config;
using System;
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

        public async Task<bool> UploadFile(string content, string contentType, string fileName, string location)
        {
            var response = await _s3Handler.PutObjectAsync(new PutObjectRequest()
            {
                ContentBody = content,
                ContentType = contentType,
                BucketName = _s3Information.FileBucketName,
                Key = fileName
            });

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK || response.HttpStatusCode == System.Net.HttpStatusCode.Created; 
        }
    }
}
