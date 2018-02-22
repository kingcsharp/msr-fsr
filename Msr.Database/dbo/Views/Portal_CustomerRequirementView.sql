CREATE VIEW [dbo].[Portal_CustomerRequirementView]
AS

SELECT c.*,p.STATUS AS ProductStatus,
pv.NAME AS ProcedureName
FROM [dbo].[Portal_CustomerSubmittedRequirement] c 
LEFT JOIN A_V_PRODUCT_SEARCH_DATA p ON p.ROOT = c.ProductId 
LEFT JOIN Portal_ProceduresView pv ON c.ProcedureId = pv.ObjectId