CREATE VIEW Report_WorkInProgress
AS

SELECT 
ROW_NUMBER() OVER(ORDER BY WoItem ASC) AS Id,
pwo.WoItem,
pwo.DueDate,
('Specification: '+ COALESCE(pwo.ProcName,'') + ' | '+
'  ATTN: ' + 'N/A ' + ' | '+ 
'P.O.: '+ COALESCE(pwo.CustPurchNum,'') + ' | '+
'Kit: ' + COALESCE(pwo.ProductName,'') + ' | '+
'Tool: ' + ' | '+
'MTTN: '+ COALESCE(pwo.CustMttn,'')) AS Details,
Status,
pwo.ProcName
,pwo.CustMttn
FROM Portal_WorkOrders pwo
WHERE pwo.Status IN ('PENDING_PARENT_ACCEPTANCE','REQUESTED')


GO