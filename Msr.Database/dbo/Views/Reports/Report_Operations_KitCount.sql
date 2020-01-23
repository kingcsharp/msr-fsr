CREATE VIEW [dbo].[Report_Operations_KitCount]

AS 

SELECT
	NEWID() AS Id
,	wo.LocationName AS Site
,	FORMAT(wo.DueDate,'yyyy-MM','en-US') AS Month
,	wo.ProductName AS KitName
,	COUNT(wo.ProductName) AS Count
FROM dbo.Portal_WorkOrders wo
WHERE wo.DueDate > DATEADD(MONTH,-12,GETDATE())
		AND wo.DueDate IS NOT NULL
		AND wo.LocationName IS NOT NULL
GROUP BY FORMAT(wo.DueDate,'yyyy-MM','en-US'), wo.ProductName, wo.LocationName

GO
