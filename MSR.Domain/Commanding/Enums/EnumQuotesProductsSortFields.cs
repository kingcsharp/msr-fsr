using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSR.Domain.Commanding.Enums
{
    public enum EnumQuotesProductsSortFields
    {
        [Description("SubmittedDate")]
        SubmittedDate,
        [Description("Company")]
        Company,
        [Description("SubmittedByFullName")]
        SubmittedByFullName,
        [Description("DivisionFab")]
        DivisionFab,
        [Description("PartKitNo")]
        PartKitNo,
        [Description("SegregationType")]
        SegregationType,
        [Description("ProcedureName")]
        ProcedureName,
        [Description("ProductName")]
        ProductName,
        [Description("Representative")]
        Representative,
        [Description("Revision")]
        Revision,
        [Description("EquipmentCost")]
        EquipmentCost,
        [Description("MaterialCost")]
        MaterialCost,
        [Description("SalesTax")]
        SalesTax,
        [Description("TotalPrice")]
        TotalPrice,
        [Description("CycleTime")]
        CycleTime,
        [Description("LastUpdateOn")]
        LastUpdateOn,
        [Description("LastUpdatedBy")]
        LastUpdatedBy
    }
}
