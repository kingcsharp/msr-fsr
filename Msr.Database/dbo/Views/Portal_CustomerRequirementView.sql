CREATE VIEW [dbo].[Portal_CustomerRequirementView]
AS

SELECT 
NEWID() AS Id,
c.Id AS CustomerSubmitId,
c.Company,
c.Description,
p.SUPPLIER_ID AS SupplierId,
c.LocationId,
c.SubmittedDate,
c.Price,
p.PROCEDURE_ID AS ProcedureId,
pv.NAME AS ProcedureName,
c.PartId,
c.QuoteJson,
c.CustomerRequirementJson,
p.NAME AS ProductName,
c.ProductId,
c.Respresentative,
c.CustomerId,
c.Division,
c.PartKitNo,
c.SubmittedBy,
c.TotalSalePrice,
c.MaterialCost,
c.LeadTime,
c.Status,
c.ProductWorkflowId,
p.REV AS Rev,
p.STATUS AS ProductStatus,p.OBJECT_ID AS PObjectId
FROM [dbo].[Portal_CustomerSubmittedRequirement] c 
LEFT JOIN A_V_PRODUCT_SEARCH_DATA p ON p.ROOT = c.ProductId 
LEFT JOIN Portal_ProceduresView pv ON p.PROCEDURE_ID = pv.ObjectId
GO

