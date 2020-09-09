using Amazon.S3;
using Amazon.S3.Model;
using MSR.Domain.Abstractions.AWS;
using MSR.Domain.Exceptions;
using MSR.Domain.Models.Config;
using System;
using System.IO;
using System.Threading.Tasks;
using MSR.Domain.Models;
using MSR.Domain.Helpers;

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
            if (!string.IsNullOrWhiteSpace(file.Base64String))
            {
                var base64File = Base64Helper.Parse(file.Base64String);
                await Upload(base64File.FileContents,base64File.ContentType, _s3Information.FileBucketName, uniqueName);
            }

            return $"{uniqueName}";
        }

        public async Task<string> UploadHelpFile(FileModel file)
        {
            var fileName = await Upload(file.FileContents,file.ContentType, _s3Information.HelpbucketName, file.Name);
            return $"{_s3Information.HelpAWSURL}{fileName}";
        }

        public Task<string> UploadImportFile(FileModel file)
        {
            return Upload(file.FileContents, _s3Information.FileBucketName, _s3Information.FileBucketName, file.Name);
        }

        public string GetURL(string key, int expiresInSeconds)
        {
            return _s3Handler.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = _s3Information.FileBucketName,
                Key = key,
                Expires = DateTime.Now.AddSeconds(expiresInSeconds)
            });
        }

        private async Task<string> Upload(byte[] fileContents, string contentType, string bucketName, string name)
        {
            var putObj = new PutObjectRequest()
            {
                ContentType = contentType,
                BucketName = bucketName,
                Key = name,
                CannedACL = S3CannedACL.PublicRead
            };

            using var ms = new MemoryStream(fileContents);

            putObj.InputStream = ms;
            var response = await _s3Handler.PutObjectAsync(putObj);
            if ((int)response.HttpStatusCode < 200 || (int)response.HttpStatusCode > 299)
            {
                throw new DomainException($"Attempt to Upload File: {name} to S3 failed.");
            }

            return $"{name}"; 
        }

    }
}
