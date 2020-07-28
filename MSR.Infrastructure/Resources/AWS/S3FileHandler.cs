using Amazon.S3;
using Amazon.S3.Model;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
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

        public async Task<string> UploadFile(FileModel file, string entityName, int entityId)
        {
            string uniqueName = $"{entityName}-{entityId}-{file.Name}";

            // If there is no data to upload, then we are simply updating the
            // pointers, and not uploading the data.
            if (file.Base64String != null && !String.IsNullOrEmpty(file.Base64String))
            {
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
            }

            return $"{uniqueName}";
        }

        public string GetURL(string key, int expiresInSeconds)
        {
            return _s3Handler.GetPreSignedURL(new GetPreSignedUrlRequest() {
                    BucketName = _s3Information.FileBucketName,
                    Key = key,
                    Expires = DateTime.Now.AddSeconds(expiresInSeconds)
                });
        }
    }
}
