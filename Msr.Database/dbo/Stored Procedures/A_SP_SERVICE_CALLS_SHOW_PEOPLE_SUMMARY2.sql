



CREATE     PROCEDURE dbo.A_SP_SERVICE_CALLS_SHOW_PEOPLE_SUMMARY2
@strWhere varchar(5000),
@status varchar(4000),
@startDate datetime,
@endDate dateTime,
@strNTLogin nvarchar(50)
AS
CREATE TABLE 
#peeps(P_ID varchar(50),P_LAST_NAME nvarchar(100),P_NAME nvarchar(100),WT_ID varchar(50),WT_NAME varchar(200),STAT varchar(50),CUST_NAME nvarchar(200),SUP_NAME nvarchar(200))

declare @dayCounter dateTime,@sql varchar(4000),@myAdd varchar(50),@pSQL varchar(8000)
set @dayCounter = @startDate
while @dayCounter <= @endDate
	begin
	set @myAdd = convert(varchar(50),year(@dayCounter)) + '_' + 
			convert(varchar(50),month(@dayCounter)) + '_' + convert(varchar(50),day(@dayCounter))
	print 'Adding column for date = ' + @myAdd
	set @sql = 'ALTER TABLE #peeps ADD DAY_' + @myAdd + ' float NULL '
	print @sql
	exec(@sql)
	set @pSql = isNull(@pSql+', ','') +  'DAY_' + @myAdd + ' = ' + 
		'(SELECT isNULL(SUM(HOURS),0) FROM A_V_SERVICE_CALL_PERSON_TIME_DATA WHERE 
		PERSON_ID = P_ID 
		AND STATUS = STAT
		AND WORK_TYPE = WT_ID
		AND ACTUAL_DATE = ''' +
		convert(varchar(50),month(@dayCounter)) + '/' 
		+ convert(varchar(50),day(@dayCounter)) + '/' 
		+ convert(varchar(50),year(@dayCounter)) + ''')'
	set @dayCounter = dateAdd(dd,1,@dayCounter)	
	end

CREATE TABLE #myRoles (ID varchar(50))
INSERT INTO #myRoles(ID) SELECT ROLE_ID FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE PERSON = @strNTLogin

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @status,', '

set @sql = 
'INSERT INTO #PEEPS(P_ID,P_LAST_NAME,P_NAME,WT_ID,WT_NAME,STAT,CUST_NAME,SUP_NAME) 
	SELECT DISTINCT PERSON_ID,LAST_NAME,NAME,WORK_TYPE,WORK_TYPE_NAME,STATUS,CUSTOMER_NAME,SUPPLIER_NAME 
		FROM A_V_SERVICE_CALL_PERSON_TIME_DATA 
WHERE ' + @strWhere + '
AND ACTUAL_DATE >= ''' + convert(varchar(50),@StartDate) + ''' AND ACTUAL_DATE <= ''' + convert(varchar(50),@endDate) + '''
AND
 		(STATUS IS NULL OR STATUS IN (SELECT IT FROM #TempItems)) 
AND
 		(
 		PERSON_ID = ''' + @strNTLogin + ''' OR
 		BOSS = ''' + @strNTLogin + ''' OR
 		APPROVER_ROLE IN (SELECT ID FROM #myRoles) OR
 		PAYER_ROLE IN (SELECT ID FROM #myRoles) OR
 		RECIEVABLE_ROLE IN (SELECT ID FROM #myRoles)
 		)
'
print @sql
exec(@sql)


set @pSQL = 'UPDATE #peeps SET ' + @pSQL
print 'pSQL = ' + @pSQL
exec(@pSQL)
 
declare @curs as cursor,@it varchar(50),@oneSql varchar(8000)
set @curs = CURSOR FOR SELECT P_ID FROM #peeps
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin

	fetch next from @curs into @it
	end



SELECT * FROM #peeps ORDER BY P_LAST_NAME,P_NAME,P_ID,STAT