using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumInvoiceSortFilters
    {
        [Description("Id")]
        Id,
        [Description("CustomerName")]
        CustomerName,
        [Description("Description")]
        Description,
        [Description("InvoiceNumber")]
        InvoiceNumber,
        [Description("Amount")]
        Amount,
        [Description("DueDate")]
        DueDate,
        [Description("CreatedOn")]
        CreatedOn,
        [Description("CreatedByName")]
        CreatedByName,
        [Description("LastUpdatedOn")]
        LastUpdatedOn,
        [Description("LastUpdatedByName")]
        LastUpdatedByName
    }
}
