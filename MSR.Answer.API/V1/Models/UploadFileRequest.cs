using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class UploadFileRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }

    }
}
