using System;
using System.Collections.Generic;

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

            var value = mimeTypes[fileExtension.ToLower()];

            return value;
        }
    }
}
