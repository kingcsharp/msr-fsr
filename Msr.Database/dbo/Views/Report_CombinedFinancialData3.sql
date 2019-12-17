




CREATE VIEW [dbo].[Report_CombinedFinancialData3]
AS


SELECT 
pwo.WoItem AS WONumber,
FORMAT(pwo.StartDate, 'd') AS WOCreationDate,
FORMAT(pwo.DueDate, 'd') AS DueDate,
FORMAT(pwo.OrigDueDate, 'd') AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.ProcName AS specno,
pwo.ProductName AS KitName,
pwo.CompanyPartNumber AS pono,
pwo.CUSTMTTN AS mttn,
FORMAT(pwo.Amount, N'C', N'en-us') AS Amount,
pwo.FillQty,
(LEFT( i.InvoiceClass,2)) +'-'+RIGHT(YEAR(i.InvoiceDate),2) +'-'+ REPLACE(STR(i.Id, 5), SPACE(1), '0') AS InvoiceId, /*changed by Mike re pulse 304234726 */
FORMAT(i.InvoiceDate, 'd') AS InvoiceDate,
FORMAT(pwo.Amount * pwo.FillQty, N'C', N'en-us') AS SubTotal,
FORMAT((pwo.Amount * pwo.FillQty * ISNULL(o.TaxRate,0)) + (pwo.Amount * pwo.FillQty), N'C', N'en-us') AS WTax
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN Portal_InvoiceWorkItem iw ON iw.ItemId = pwo.FillItemId
INNER JOIN Portal_Invoice i ON i.Id = iw.InvoiceId
LEFT JOIN Portal_PurchaseOrders o ON o.ReferencePo = pwo.ReferencePo