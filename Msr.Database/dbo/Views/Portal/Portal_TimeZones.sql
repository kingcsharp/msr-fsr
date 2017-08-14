CREATE VIEW [dbo].[Portal_TimeZones]
	as
SELECT ID as Id, DESCRIPTION + '(' + cast(DATEADD(hh,G_DIFF,getDate()) AS nvarchar(50)) + ')' AS Description, NUM as Num FROM A_TIME_ZONES
