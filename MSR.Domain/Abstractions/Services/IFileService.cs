using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IFileService
    {
        public Task<bool> CreateFileAsync<T>(T entity, int entityId) where T: class;
    }
}
