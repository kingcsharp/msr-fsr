



CREATE VIEW [dbo].[Report_CombinedFinancialData]
AS
SELECT 
ROW_NUMBER() OVER(ORDER BY WoItem ASC) AS Id,
pwo.WoItem AS WONumber,
pwo.ReferencePo AS PONumber,
pwo.StartDate AS WOCreationDate,
pwo.DueDate AS DueDate,
pwo.OrigDueDate AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.COMPANYPARTNUMBER as CustomerPartNumber,
pwo.ProcName AS SpecificationNumber,
pwo.ProductName AS KitName,
pwo.CustMttn AS MTTN,
FORMAT(pwo.Amount,'C') AS Amount,
pwo.FillQty,
CONVERT(VARCHAR,CONCAT(i.InvoiceClass,'-',RIGHT(YEAR(i.InvoiceDate), 2),'-',  RIGHT(CONCAT('00000', i.Id), 5))) AS InvoiceId,
i.InvoiceDate AS InvoiceDate,
FORMAT(ROUND(pwo.Amount * pwo.FillQty,2),'C') AS SubTotal,
FORMAT(ROUND(CAST(pwo.Amount * pwo.FillQty AS float) + (CAST(pwo.Amount * pwo.FillQty AS float) * ISNULL(CAST(i.Tax / 100 AS float),0)),2),'C') AS WTax
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN Portal_InvoiceWorkItem iw ON iw.ItemId = pwo.FillItemId
INNER JOIN Portal_Invoice i ON i.Id = iw.InvoiceId

GO