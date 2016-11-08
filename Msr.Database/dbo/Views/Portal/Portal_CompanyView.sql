CREATE VIEW [dbo].Portal_CompanyView
AS

SELECT 
 [ID] AS Id
,[DRCM] AS Drcm
,[STATUS] AS Status
,[NAME] AS Name
FROM [dbo].[A_COMPANIES]

GO