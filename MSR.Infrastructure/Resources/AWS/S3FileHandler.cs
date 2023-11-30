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
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Resources.AWS
{
    public class S3FileHandler : IUploadFiles, IDownloadFiles
    {
        readonly IAmazonS3 _s3Handler;
        readonly S3Information _s3Information;

        public S3FileHandler(IAmazonS3 s3Handler, S3Information s3Information)
        {
            _s3Handler = s3Handler;
            _s3Information = s3Information;
        }

        public async Task<Stream> DowloadFile(string fileName, string bucketName)
        {
            var response = await _s3Handler.GetObjectAsync(new GetObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            });

            return response.ResponseStream;
        }


        public async Task<Stream> DownloadFileFromS3(string fileName)
        {
            try
            {
                // Use _fileDownloader to download the file
                var stream = await DowloadFile(fileName, _s3Information.FileBucketName);

                // Return the file stream as a response
                return stream;
            }
            catch (Exception ex)
            {
                // Handle exceptions related to file download
                throw new DomainException($"Error downloading file: {ex.Message}");
            }
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            try
            {
                // Use _fileDownloader to download the file
                var stream = await DowloadFile(fileName, _s3Information.FileBucketName);

                // Return the file stream as a response
                return stream;
            }
            catch (Exception ex)
            {
                // Handle exceptions related to file download
                throw new DomainException($"Error downloading file: {ex.Message}");
            }
        }

        public async Task<Stream> DownloadFile(string fileName, string bucketName)
        {
            var response = await _s3Handler.GetObjectAsync(new GetObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            });

            return response.ResponseStream;
        }


        public async Task<ListObjectsV2Response> GetS3Files(string folderName)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _s3Information.FileBucketName,
                Prefix = folderName+ "/",
                MaxKeys = 1000,
                //StartAfter= "combinedfinancialdata/combined-export-06-2019.csv" //VEry interesting shit, if u store files with yr names then u can filter by them like this.
            };

            var listObjectsRequest = await _s3Handler.ListObjectsV2Async(request);
            return listObjectsRequest;
        }

        public async Task<string> UploadFile(FileModel file, string entityName, int entityId)
        {
            string uniqueName = $"{entityName}-{entityId}-{file.Name}";
            // If there is no data to upload, then we are simply updating the
            // pointers, and not uploading the data.
            if (!string.IsNullOrWhiteSpace(file.Base64String))
            {
                var base64File = Base64Helper.Parse(file.Base64String);
                await Upload(base64File.FileContents, base64File.ContentType, _s3Information.FileBucketName, uniqueName);
            }

            return GetURL(uniqueName, 6000);
        }

        public async Task<string> UploadFile(MemoryStream stream, FileModel file, string bucketName)
        {

            await Upload(stream.ToArray(), file.ContentType, bucketName, file.Name);

            return GetURL(file.Name, 6000);
        }

        public async Task<string> UploadFile(MemoryStream stream, FileModel file, string entityName, int? entityId)
        {
            string uniqueName = entityId != null ? $"{entityName}-{entityId}-{file.Name}" : $"{entityName}-{file.Name}";

            await Upload(stream.ToArray(), file.ContentType, _s3Information.FileBucketName, uniqueName);

            return GetURL(uniqueName, 6000);
        }

        public async Task<string> UploadHelpFile(FileModel file)
        {
            var fileName = await Upload(file.FileContents, file.ContentType, _s3Information.HelpbucketName, file.Name);
            return $"{_s3Information.HelpAWSURL}{fileName}";
        }

        public Task<string> UploadImportFile(FileModel file)
        {
            return Upload(file.FileContents, _s3Information.FileBucketName, _s3Information.FileBucketName, file.Name);
        }

        public string GetURL(string key, int expiresInSeconds = 3600)
        {
            var s3Key = key;
            if (key.Contains(_s3Information.AWSURL)
                || key.Contains(_s3Information.UnSecureAWSURL)
                || key.Contains(_s3Information.HelpAWSURL))
            {
                //Get the Last part
                s3Key = key.Replace(_s3Information.AWSURL, "").Replace(_s3Information.UnSecureAWSURL, "").Replace(_s3Information.HelpAWSURL, "");
            }

            return _s3Handler.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = _s3Information.FileBucketName,
                Key = s3Key,
                Expires = DateTime.Now.AddSeconds(expiresInSeconds)
            });
        }

        public string GetDownloadUrl(string fileName, int expiresInSeconds = 3600)
        {
            // Generate and return the pre-signed URL for downloading the file
            return GetURL(fileName, expiresInSeconds);
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

        // public Task<Stream> DownloadFile(string fileName, string bucketName)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
