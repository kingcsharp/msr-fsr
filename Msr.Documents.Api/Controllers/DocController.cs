using System;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.Http;
using ImageResizer;
using Msr.Documents.Api.Models;

namespace Msr.Documents.Api.Controllers
{
    public class DocController : ApiController
    {
        [Route("api/doc/getfilebyid")]
        [HttpGet]
        public DocResponse GetFileById(string filePath, int? height)
        {
            var response = new DocResponse();
            try
            {
                var localFilePath = Path.Combine(ConfigurationManager.AppSettings["RootFolder"], filePath);

                string ext = Path.GetExtension(localFilePath).ToLower();

                if (height.HasValue && ext ==".jpg" || ext ==".jpeg" || ext==".png")
                {
                    using (var stream = new MemoryStream())
                    {
                        var image = ImageBuilder.Current.Build(localFilePath, new ResizeSettings("height=" + height));

                        if (ext == ".jpg" || ext == ".jpeg")
                        {
                            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        } else if (ext == ".png")
                        {
                            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        response.DocData = Convert.ToBase64String(stream.ToArray());
                    }
                }
                else
                {
                    var bytes = File.ReadAllBytes(localFilePath);
                    var file = Convert.ToBase64String(bytes);
                    response.DocData = file;
                }
            }
            catch (Exception e)
            {
                response.Error = e.Message;
            }

            return response;
        }
    }
}
