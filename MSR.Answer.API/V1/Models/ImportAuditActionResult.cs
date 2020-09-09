using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class ImportAuditActionResult<T>: AuditActionResult<T>
    {
        public IEnumerable<ImportError> ImportErrors { get; set; }
    }
}
