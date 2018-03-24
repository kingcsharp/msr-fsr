


/*
STORED PROCEDURE CALLED IN serviceCalls/viewWithFiltersServiceCalls.asp
*/
CREATE       PROCEDURE A_SP_SERVICE_CALL_GET_GRAND_TOTALS
@strWhere varchar(1000),
@strNTLogin varchar(50)
AS
declare @myCO as nvarchar(50)
SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin
print 'myCo is' +@myCO

CREATE TABLE #TSearchFilteredData (
ID varchar(50)
)

INSERT INTO #TSearchFilteredData (ID)
exec A_SP_SERVICE_CALL_SEARCH_GET_FILTERED_SERVICE_CALLS_IDS @strWhere, @strNTlogin

declare @sql varchar(2000)
declare @fieldList varchar(1000)

set @sql = 'SELECT 
			CASE WHEN (SUM(TOTAL_0) = 0.0) THEN  ''''
			ELSE isNull(convert(varchar(50),SUM(TOTAL_0)),"")
			end TOTAL_0,			
			CASE WHEN (SUM(TOTAL_1) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50), SUM(TOTAL_1)),"")
			end TOTAL_1,			
			CASE WHEN (SUM(TOTAL_2) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(TOTAL_2)),"")
			end TOTAL_2,			
			CASE WHEN (SUM(TOTAL_3) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(TOTAL_3)),"")
			end TOTAL_3,			
			CASE WHEN (SUM(TOTAL_4) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(TOTAL_4)),"")
			end TOTAL_4,			
			CASE WHEN (SUM(TOTAL_5) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(TOTAL_5)),"")
			end TOTAL_5,			
			CASE WHEN (SUM(TOTAL_6) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(TOTAL_6)),"")
			end TOTAL_6,			
			CASE WHEN (SUM(NORMAL_HOURS) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(NORMAL_HOURS)),"")
			end NORMAL_HOURS,
			CASE WHEN (SUM(OT_HOURS) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(OT_HOURS)),"")
			end OT_HOURS,
			CASE WHEN (SUM(TOTAL_HOURS) = 0.0) THEN '''' 
			ELSE isNull(convert(varchar(50),SUM(TOTAL_HOURS)),"")
			end HOURS_TOTAL
			FROM #TSearchFilteredData F LEFT OUTER JOIN
    		A_V_SERVICE_CALL_VIEW_DATA V ON F.ID = V.ID'
	
print @sql
exec(@sql)