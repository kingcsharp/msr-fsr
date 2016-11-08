alter view dbo.Portal_TimeZoneView
AS
SELECT
Id,
DESCRIPTION + '(' + cast(DATEADD(hh,G_DIFF,getDate()) AS nvarchar(50)) + ')' AS Description, NUM as Num
FROM A_TIME_ZONES










