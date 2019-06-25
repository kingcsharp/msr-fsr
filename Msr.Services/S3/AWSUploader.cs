using System.IO;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

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
