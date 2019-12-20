



CREATE VIEW [dbo].[Report_CombinedFinancialData]
AS
SELECT 
ROW_NUMBER() OVER(ORDER BY WoItem ASC) AS Id,
pwo.CustPurchNum AS WONumber,
FORMAT(pwo.StartDate,'MM/dd/yyyy') AS WOCreationDate,
FORMAT(pwo.DueDate,'MM/dd/yyyy') AS DueDate,
FORMAT(pwo.OrigDueDate,'MM/dd/yyyy') AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.ProcName AS specno,
pwo.WoItem AS KitName,
pwo.ReferencePo AS pono,
pwo.CustMttn AS mttn,
pwo.Amount,
pwo.FillQty,
i.Id AS InvoiceId,
FORMAT(i.InvoiceDate,'MM/dd/yyyy') AS InvoiceDate,
CONVERT(VARCHAR,i.SubTotal) AS SubTotal,
CONVERT(VARCHAR,i.Tax) AS WTax
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN Portal_InvoiceWorkItem iw ON iw.ItemId = pwo.FillItemId
INNER JOIN Portal_Invoice i ON i.Id = iw.InvoiceId

GO