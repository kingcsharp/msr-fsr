using System;
using System.Linq;

namespace MSR.Infrastructure.Helpers
{
    public class Base64Helper
    {
        public string ContentType { get; set; }

        public byte[] FileContents { get; set; }
        public static Base64Helper Parse(string base64Content)
        {
            if (string.IsNullOrEmpty(base64Content))
            {
                return null;
            }
            var base64file = new Base64Helper();
            try
            {
                int indexOfSemiColon = base64Content.IndexOf(";", StringComparison.OrdinalIgnoreCase);

                string dataLabel = base64Content.Substring(0, indexOfSemiColon);

                base64file.ContentType = dataLabel.Split(':').Last();

                var startIndex = base64Content.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;

                var fileContents = base64Content.Substring(startIndex);

                base64file.FileContents = Convert.FromBase64String(fileContents);
            }
            catch (Exception)
            {
                return null;
            }

            return base64file;
        }

        public static Base64Helper Parse(string fileContents, string contentType)
        {
            if (string.IsNullOrEmpty(fileContents) || string.IsNullOrEmpty(contentType))
            {
                return null;
            }
            var base64file = new Base64Helper();
            try
            {
                base64file.ContentType = contentType;

                base64file.FileContents = Convert.FromBase64String(fileContents);
            }
            catch (Exception)
            {
                return null;
            }

            return base64file;
        }


        public override string ToString()
        {
            return string.Format("data:{0};base64,{1}", ContentType, Convert.ToBase64String(FileContents));
        }
    }
}
