CREATE VIEW [dbo].[Report_Operations_CustomerRevenue]
	AS 
	WITH AllData_CTE (Site,MONTH,CustomerName,Revenue)
AS
(
	SELECT
		wo.LocationName
	,	FORMAT(wo.DueDate,'yyyy-MM','en-US')
	,	ISNULL(CustomerName,'')
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
,	CustomerName
,	SUM(Revenue) AS Revenue
FROM AllData_CTE
GROUP BY MONTH, CustomerName, Site

GO
