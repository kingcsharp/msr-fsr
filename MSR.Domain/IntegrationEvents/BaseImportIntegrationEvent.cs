using MSR.Domain.Intigration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.IntegrationEvents
{
    public class BaseImportIntegrationEvent : IntegrationEvent
    {
        public string CsvData { get; set; }
    }
}
