using System;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.Http;
using Msr.Documents.Api.Models;

namespace Msr.Documents.Api.Controllers
{
    public class DocController : ApiController
    {
        [Route("api/doc/getfilebyid")]
        [HttpGet]
        public DocResponse GetFileById(string filePath)
        {
            var response = new DocResponse();
            try
            {
                var localFilePath = Path.Combine(ConfigurationManager.AppSettings["RootFolder"], filePath);

                var bytes = File.ReadAllBytes(localFilePath);

                var file = Convert.ToBase64String(bytes);

                response.DocData = file;

            }
            catch (Exception e)
            {
                response.Error = e.Message;
            }

            return response;
        }
    }
}
