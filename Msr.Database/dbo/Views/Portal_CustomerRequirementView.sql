

CREATE VIEW [dbo].[Portal_CustomerRequirementView]
AS

SELECT 
NEWID() AS Id,
c.Id AS CustomerSubmitId,
sup.ROOT_NAME AS Company,
c.Description,
p.SUPPLIER_ID AS SupplierId,
p.SUPPLIER_NAME AS SupplierName,
p.LocationId,
c.SubmittedDate,
p.PROCEDURE_ID AS ProcedureId,
pv.NAME AS ProcedureName,
p.APP_OBJECT AS PartId,
c.QuoteJson,
c.CustomerRequirementJson,
p.NAME AS ProductName,
c.ProductId,
c.Respresentative,
p.Division,
c.PartKitNo,
c.SubmittedBy,
p.TotalSalePrice,
c.Price,
p.MaterialCost,
c.LeadTime,
c.ProductWorkflowId,
p.REV AS Rev,
p.IsProduct,
p.STATUS,
p.OBJECT_ID AS ObjectId,
pqp.CUST_ID as CustomerId,
Customer.NAME AS CustomerName
FROM  A_V_PRODUCT_SEARCH_DATA p
INNER JOIN [dbo].[Portal_CustomerSubmittedRequirement] c ON c.Id = p.CustomerRequirementId
LEFT JOIN Portal_ProceduresView pv ON p.PROCEDURE_ID = pv.ObjectId
LEFT  JOIN A_V_COMPANIES_DROP_SEARCH sup on sup.ID = p.SUPPLIER_ID
LEFT JOIN A_PRODUCTS_QUICK_PRICE pqp ON P.ID = pqp.PROD_HIST_ID
OUTER APPLY (
SELECT cus.Name FROM A_V_COMPANIES_DROP_SEARCH cus
WHERE cus.Id= pqp.CUST_ID
) AS Customer
GO
