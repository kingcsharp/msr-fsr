using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Msr.Commons.Files;
using Msr.Services.Documents;
using Msr.Services.Documents.ViewModels;

namespace Msr.Services.Helpers
{
    public static class FileInputConfigHelper
    {
        public static string GetPreviewConfigValue(List<DocLink> modelDocLinks, string deleteActionUrl, string actionUrl)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var previewConfig = jsonSerialiser.Serialize(modelDocLinks.Select(x => new
            {
                caption = x.NAME,
                type = MimeTypes.GetContentType(x.CONTENTTYPE),
                size = 6666,
                url = deleteActionUrl + "?id=" + x.LINKED_DOC_ID,
                downloadUrl = actionUrl + "?id=" + x.LINKED_DOC_ID,
                key = x.LINKED_DOC_ID
            }));
            return previewConfig;
        }

        public static string GetPreviewValue(List<DocLink> modelDocLinks, DocumentFilesService documentFilesService)
        {
            var preview = string.Join(",",
                modelDocLinks.ToArray().Select(x => string.Format("{0}{1}{0}", "\'",
                    documentFilesService.ConvertImageUrlToBase64(x.SERVER_PATH))));

            return preview;
        }

        public static List<string> GetInitialPreviewValue(string serverPath, DocumentFilesService documentFilesService)
        {
            var initialPreview = new List<string> { documentFilesService.ConvertImageUrlToBase64(serverPath) };
            return initialPreview;
        }

        public static object GetInitialPreviewConfig(List<DocLink> initialPreviewConfigs, string deleteActionUrl, string actionUrl)
        {
            var initialPreviewConfig = initialPreviewConfigs.Select(x => new
            {
                caption = x.NAME,
                type = MimeTypes.GetContentType(x.CONTENTTYPE),
                size = 6666,
                url = deleteActionUrl + "?id=" + x.LINKED_DOC_ID,
                downloadUrl = actionUrl + "?id=" + x.LINKED_DOC_ID,
                key = x.LINKED_DOC_ID
            }).ToArray();
            return initialPreviewConfig;
        }
    }
}