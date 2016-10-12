CREATE VIEW [dbo].[Portal_WorkOrders]
AS

SELECT 
 [ID] AS Id
,[DRCM] AS Drcm
,[STATUS] AS Status
,[NAME] AS Name
FROM [dbo].[A_COMPANIES]

GO