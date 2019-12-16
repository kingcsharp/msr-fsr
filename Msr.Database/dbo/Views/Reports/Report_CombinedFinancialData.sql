


CREATE VIEW [dbo].[Report_CombinedFinancialData]
AS
SELECT        ROW_NUMBER() OVER(ORDER BY WoItem ASC) AS Id, pwo.WoItem AS WONumber, FORMAT(ISNULL(pwo.StartDate, ''), 'd', 'en-US') AS WOCreationDate, FORMAT(ISNULL(pwo.DueDate, ''), 'd', 'en-US') AS DueDate, FORMAT(ISNULL(pwo.OrigDueDate, ''), 'd', 'en-US') AS ShipDate, 
                         pwo.LocationName AS MSRFSRFacility, pwo.CustomerName, pwo.ProcName AS specno, pwo.WoItem AS KitName, pwo.ReferencePo AS pono, pwo.CUSTMTTN AS mttn, pwo.Amount, pwo.FillQty, i.Id AS InvoiceId, 
                         FORMAT(ISNULL(i.InvoiceDate, ''), 'd', 'en-US') AS InvoiceDate, FORMAT(pwo.FillQty * pwo.Amount, '$#,#.00') AS SubTotal, FORMAT(pwo.FillQty * pwo.Amount + pwo.FillQty * pwo.Amount * ISNULL(i.Tax, 0), '$#,#.00') 
                         AS WTax
FROM            dbo.Portal_WorkOrders AS pwo INNER JOIN
                         dbo.Portal_InvoiceWorkItem AS iw ON iw.ItemId = pwo.FillItemId INNER JOIN
                         dbo.Portal_Invoice AS i ON i.Id = iw.InvoiceId
WHERE pwo.StartDate > '2019-08-01'

GO