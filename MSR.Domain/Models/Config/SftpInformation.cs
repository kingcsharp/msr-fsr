using System;
namespace MSR.Domain.Models.Config
{
	public class TransmissionInformation
	{
		public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RemoteDirectory { get; set; }
        public string S3Bucket { get; set; }
        public bool IsEnabled { get; set; }
    }
}

