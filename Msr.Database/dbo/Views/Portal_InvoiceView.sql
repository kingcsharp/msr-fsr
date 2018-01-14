create view [dbo].[Portal_InvoiceView]
AS

SELECT  (LEFT( p.InvoiceClass,2)) +'-'+RIGHT(YEAR(p.InvoiceDate),2) +'-'+ REPLACE(STR(p.Id, 5), SPACE(1), '0')  as InvoiceNumber,
p.Id,p.Client,
p.Description,
p.Status,
p.CustPo,
p.InvoiceDate,
p.TotalDue,
p.Tax,
p.PaymentsAndCredits,
p.Debits,
p.LateFees,
p.Items,
p.Supplier,
p.InvoiceClass
FROM 
Portal_Invoice AS p

GO
