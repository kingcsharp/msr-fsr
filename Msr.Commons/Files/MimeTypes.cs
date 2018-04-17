using System;
using System.Collections.Generic;
using System.Linq;

namespace Msr.Commons.Files
{
    public static class MimeTypes
    {
        public static string GetTypes(string fileExtension)
        {
            var mimeTypes = new Dictionary<string, string>
            {
                {"pdf", "application/pdf"},
                {"docx", "vnd.ms-word"},
                {"txt", "text/plain"},
                {"xls", "application/vnd.ms-excel"},
                {"xlsx", "application/vnd.ms-powerpoint"},
                {"ppt", "application/zip"},
                {"zip", "application/pdf"},
                {"jpg", "jpg"},
                {"png", "png"},
                {"jpeg", "jpeg"},
                {"gif", "gif"}
            };

            var mimeType = mimeTypes.SingleOrDefault(x => x.Key == fileExtension.ToLower());
            
            return mimeType.Value;
        }

        public static string GetContentType(string value)
        {
            var contentType = "";

            switch (value)
            {
                case "image/jpeg":
                    contentType = "image";
                    break;
                case "image/png":
                    contentType = "image";
                    break;
                case "image/gif":
                    contentType = "image";
                    break;
                case "image/bitmap":
                    contentType = "image";
                    break;
                case "application/pdf":
                    contentType = "pdf";
                    break;
                case "text/plain":
                    contentType = "text";
                    break;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":

                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":

                case "application/vnd.ms-excel":
                    contentType = "office";
                    break;
                default:
                    contentType = "other";
                    break;
            }

            return contentType;
        }
    }

}
