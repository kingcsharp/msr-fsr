using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon.S3.IO;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Msr.Models.Archive;

namespace Msr.Services.S3
{
    public class AWSFileHandler : ICloudUploader
    {
        public void UploadToCloud(HttpPostedFileBase file, string bucketName, string keyName)
        {
            UploadToCloud(file.InputStream, bucketName, keyName);
        }

        public void UploadToCloud(Stream file, string bucketName, string keyName)
        {
            TransferUtility fileTransferUtility = new TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.USEast1));

            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest()
            {
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = keyName,
                InputStream = file
            };

            fileTransferUtility.Upload(request);
        }

        public List<ArchiveData> GetArchiveFilesFromS3Directory(string bucketName, string folderName)
        {
            using (var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                S3DirectoryInfo dir = new S3DirectoryInfo(client, bucketName, folderName);
                var archiveData = dir.EnumerateFiles().ToList().Select(i => new ArchiveData()
                {
                    CreateDate = i.LastWriteTimeUtc,
                    DownloadURL = $"combinedfinancialdata|{i.Name}",
                    FileName = i.Name,
                    FileSize = i.Length
                });
                return archiveData.ToList();
            }           
        }
       
        public Stream DownloadFromCloud(string bucketName, string keyName)
        {
            using (var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };
                
                GetObjectResponse response = client.GetObject(request);
                return response.ResponseStream;
            }
        }
    }
}
