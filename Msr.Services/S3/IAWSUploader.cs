using Amazon.S3.IO;
using Msr.Models.Archive;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Msr.Services.S3
{
    public interface ICloudUploader
    {
        void UploadToCloud(HttpPostedFileBase file, string bucketName, string keyName);
        void UploadToCloud(Stream file, string bucketName, string keyName);

        Stream DownloadFromCloud(string bucketName, string keyName);
        List<ArchiveData> GetArchiveFilesFromS3Directory(string bucketName, string folderName);
    }
}