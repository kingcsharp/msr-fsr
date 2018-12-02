CREATE VIEW Report_CombinedFinancialData
AS

SELECT 
pwo.CustPurchNum AS WONumber,
pwo.StartDate AS WOCreationDate,
pwo.DueDate AS DueDate,
pwo.OrigDueDate AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.ProcName AS specno,
pwo.WoItem AS KitName,
pwo.ReferencePo AS pono,
pwo.CustMttn AS mttn,
pwo.Amount,
pwo.FillQty,
i.Id AS InvoiceId,
i.InvoiceDate AS InvoiceDate,
i.SubTotal AS SubTotal,
i.Tax AS WTax
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN Portal_InvoiceWorkItem iw ON iw.ItemId = pwo.FillItemId
INNER JOIN Portal_Invoice i ON i.Id = iw.InvoiceId

GO