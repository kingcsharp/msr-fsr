using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace MSR.Domain.Abstractions.Services
{
    public interface IXmlService
    {
        Task<FileModel> DownloadFile(int Id);
    }
}
