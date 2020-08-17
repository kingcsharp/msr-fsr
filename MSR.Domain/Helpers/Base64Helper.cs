using System;
using System.Linq;
using System.Text;

namespace MSR.Domain.Helpers
{
    public class Base64Helper
    {
        public string ContentType { get; set; }

        public static string ByteOrderMarkUtf8 => Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        public byte[] FileContents { get; set; }
        public static Base64Helper Parse(string base64Content)
        {
            if (string.IsNullOrWhiteSpace(base64Content))
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
            return $"data:{ContentType};base64,{Convert.ToBase64String(FileContents)}";
        }
    }
}
