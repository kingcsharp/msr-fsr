using Amazon.S3.IO;
using Msr.Models.Archive;
using Msr.Services.S3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Answer.Web.Controllers.API
{
    [RoutePrefix("api/Archive")]
    public class ArchiveApiController : ApiController
    {
        private readonly AWSFileHandler _awsHandler;

        public ArchiveApiController()
        {
            _awsHandler = new AWSFileHandler();
        }

        [HttpGet, Route("CombinedFinancialData")]
        public List<ArchiveData> GetCombinedFinancialArchiveData()
        {
            var files = _awsHandler.GetArchiveFilesFromS3Directory(ConfigurationManager.AppSettings.Get("AWSBuketName"), "combinedfinancialdata"); 
            
            
            return files;
        }

        [HttpGet, Route("Download")]
        public HttpResponseMessage DownloadArchiveFile(string downloadURL)
        {
            var fileStream =  _awsHandler.DownloadFromCloud(ConfigurationManager.AppSettings.Get("AWSBuketName"), downloadURL.Replace("|","/"));
            
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = downloadURL.Split('|')[1]
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return response;
        }
    }
}
