
using MSR.Domain.Models;
using MSR.Domain.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.Abstractions
{
    public interface IProductViewService
    {
        Task<byte[]> DownloadFile(string format, ProductDownloadFilter filters);
        Task<ICollection<ProductModel>> GetProductsByWorkOrderAsync(int workOrderId);
    }
}
