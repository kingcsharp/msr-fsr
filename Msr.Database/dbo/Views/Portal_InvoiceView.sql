create view [dbo].[Portal_InvoiceView]
AS

SELECT  
(LEFT( i.InvoiceClass,2)) +'-'+RIGHT(YEAR(i.InvoiceDate),2) +'-'+ REPLACE(STR(i.Id, 5), SPACE(1), '0')  as InvoiceNumber,
i.Id,
i.Description,
i.Status,
i.CustPo,
i.InvoiceDate,
i.SubTotal,
i.Total,
i.Tax,
i.PaymentsAndCredits,
i.Debits,
i.LateFees,
i.Items,
i.Supplier,
i.InvoiceClass,
c.ROOT_NAME AS Client
FROM 
Portal_Invoice AS i
left JOIN A_V_COMPANIES_DROP_SEARCH c ON c.ID = i.client

GO
