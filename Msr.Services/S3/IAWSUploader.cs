using System.IO;
using System.Web;

namespace Msr.Services.S3
{
    public interface ICloudUploader
    {
        void UploadToCloud(HttpPostedFileBase file, string bucketName, string keyName);
        void UploadToCloud(Stream file, string bucketName, string keyName);

        Stream DownloadFromCloud(string bucketName, string keyName);
    }
}