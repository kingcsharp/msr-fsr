using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.Abstractions
{
    public interface IProductViewService
    {
        Task<byte[]> DownloadFile(string format); 
    }
}
