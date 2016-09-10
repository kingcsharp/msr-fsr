




/*
STORED PROCEDURE CALLED IN serviceCalls/searchServiceCalls.asp
*/
CREATE  PROCEDURE dbo.A_SP_SERVICE_CALL_RAW_DATA_SEARCH
@supplierName nvarchar(100),
@customerName nvarchar(100),
@workerName nvarchar(50),
@bossName nvarchar(50),
@status varchar(50),
@startDate datetime,
@strNTLogin nvarchar(50)
AS
if @startDate is null
	SELECT TOP 1  @startDate = 
		convert(dateTime,
			convert(varchar(2),START_MONTH) + '/' +
			convert(varchar(2),START_DAY) + '/' +
			convert(varchar(4),START_YEAR)
		)
	FROM A_SERVICE_CALLS_WEEKLY_REPORTS
	ORDER BY START_YEAR ,START_MONTH ,START_DAY 

SELECT top 10 ID,WORKER_NAME,BOSS_NAME,CUSTOMER_NAME,STATUS FROM A_V_SERVICE_CALLS_BASIC_DATA WHERE 
	SUPPLIER_NAME LIKE '%' + isNull(@supplierName,'') + '%' AND
	CUSTOMER_NAME  LIKE '%' + isNull(@customerName,'') + '%' AND
	WORKER_NAME  LIKE '%' + isNull(@workerName,'') + '%' AND
	BOSS_NAME  LIKE '%' + isNull(@bossName,'') + '%' AND
	STATUS  LIKE '%' + isNull(@status,'') + '%' AND
	START_DATE >= @startDate AND START_DATE <= dateAdd(dd,21,@startDate)
	ORDER BY WORKER_LAST_NAME DESC,WORKER_FIRST_NAME DESC, START_DATE




























