CREATE VIEW [dbo].[Portal_CustomerRequirementView]
AS

SELECT c.*,p.STATUS AS ProductStatus  FROM [dbo].[Portal_CustomerSubmittedRequirement] c 
LEFT JOIN A_V_PRODUCT_SEARCH_DATA p ON p.ROOT = c.ProductId 