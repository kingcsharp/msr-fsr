CREATE VIEW [dbo].[Report_Operations_KitRevenue]

AS 

WITH AllData_CTE (Site, Month, KitName, Revenue)
AS
(
	SELECT
		wo.LocationName
	,	FORMAT(wo.DueDate,'yyyy-MM','en-US')
	,	wo.ProductName
	,	wo.Amount
	FROM dbo.Portal_WorkOrders wo
	WHERE wo.DueDate > DATEADD(MONTH,-12,GETDATE())
			AND wo.DueDate IS NOT NULL
			AND wo.LocationName IS NOT NULL
)
SELECT
	NEWID() AS Id
,	Site
,	MONTH
,	KitName
,	SUM(Revenue) AS Revenue
FROM AllData_CTE
GROUP BY MONTH, KitName, Site
GO
