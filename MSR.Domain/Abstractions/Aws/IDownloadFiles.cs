using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.AWS
{
    public interface IDownloadFiles
    {
        public Task<Stream> DownloadFile(string fileName);
        public Task<Stream> DownloadFile(string fileName, string bucketName);

        public Task<Stream> DownloadFileFromS3(string fileName);

        public string GetURL(string key, int expiresInSeconds);
        public Task<ListObjectsV2Response> GetS3Files(string folderName);
    }
}
