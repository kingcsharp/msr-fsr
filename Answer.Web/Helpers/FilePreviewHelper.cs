using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Answer.Web.Helpers
{
    public static class FilePreviewHelper
    {
        public static IHtmlString FilePreview(string name, string docId)
        {
            string imagePreviewHtml;
            var type = Path.GetExtension(name);
            switch (type)
            {
                case ".xls":
                    imagePreviewHtml =
                        $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/icon-xls.png' /></span>&nbsp";
                    break;
                case ".jpg":
                case ".png":
                case ".jpeg":
                case ".gif":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/jpg.png' /></span>&nbsp";
                    break;

                case ".docx":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/icon-doc.png' /></span>&nbsp";
                    break;

                case ".xlsx":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/icon-xls.png' /></span>&nbsp";
                    break;

                case ".ppt":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/icon-ppt.png' /></span>&nbsp";
                    break;

                case ".pdf":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/icon-pdf.png' /></span>&nbsp";
                    break;

                case ".txt":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/txt.png' /></span>&nbsp";
                    break;

                case ".zip":
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/icon-zip.png' /></span>&nbsp";
                    break;

                default:
                    imagePreviewHtml = $"<span class='data-toggle file-prev' data-call-back-item='{docId}'  data-target='#view-images' title='View File'><img style='width: auto; display: inherit;' src='/assets/img/default.png' /></span>&nbsp";
                    break;

            }

            return new HtmlString(imagePreviewHtml);
        }
    }
}
