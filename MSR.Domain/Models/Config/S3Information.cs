namespace MSR.Domain.Models.Config
{
    public class S3Information
    {
        public string FileBucketName { get; set; }
        public string AWSURL { get; set; }
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
    }
}
