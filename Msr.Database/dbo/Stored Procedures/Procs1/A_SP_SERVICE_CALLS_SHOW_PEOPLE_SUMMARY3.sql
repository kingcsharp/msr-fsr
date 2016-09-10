














CREATE           PROCEDURE dbo.A_SP_SERVICE_CALLS_SHOW_PEOPLE_SUMMARY3
@strWhere varchar(5000),
@status varchar(4000),
@startDate datetime,
@endDate dateTime,
@monthOption varchar(50),
@strOrder varchar(500),
@strNTLogin nvarchar(50)
AS
print 'here'
CREATE TABLE #myRoles (ID varchar(50))
INSERT INTO #myRoles(ID) SELECT ROLE_ID FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS WHERE PERSON = @strNTLogin

CREATE TABLE #myCOS (ID varchar(50))
INSERT INTO #myCOS(ID) 

SELECT DISTINCT o.CREATING_CO  FROM A_V_ROLES_APPROVED_WITH_PEOPLE_IDS r,A_OBJECTS o
	WHERE r.PERSON = @strNTLogin AND r.ROLE_ID = o.ID AND r.IS_ADMIN = 1

CREATE TABLE #myCOS2 (ID varchar(50))
INSERT INTO #myCOS2 SELECT CHILD_COMPANY FROM A_COMPANIES_CHILD_LOOKUP_TABLE l,#myCOS c WHERE c.ID = l.COMPANY
INSERT INTO #myCOS2 SELECT DISTINCT ID FROM #myCOS

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @status,', '
declare @sql as varchar(8000)
set @sql = '
SELECT LOCATION_ID,LOCATION_NAME,PERSON_ID,
	LAST_NAME,
	NAME, LAST_NAME + '', '' + NAME AS FULL_NAME,WORK_TYPE,WORK_TYPE_NAME,
	STATUS,CUSTOMER_NAME,SUPPLIER_NAME,HOURS,convert(varchar(50),ACTUAL_DATE,1) AS DT,HOUR_RATE AS RATE,OT_RATE,TAX_RATE,HIDE,
	WORK_TYPE_NAME + '' ($'' + isNULL(convert(varchar(50),HOUR_RATE),''0'') + '')'' AS WORK_TYPE_NAME_WITH_RATE
FROM A_V_SERVICE_CALL_PERSON_TIME_DATA 
WHERE ' + @strWhere

if @monthOption is null
	set @sql = @sql + ' AND ACTUAL_DATE >= ''' + convert(varchar(50),@StartDate) + ''' AND ACTUAL_DATE <= ''' + convert(varchar(50),@endDate) + ''' '
else
	begin
	declare @myMonth varchar(50)
	declare @myYear varchar(50)
	if @monthOption = 'LAST_MONTH'
		begin
		set @myMonth = convert(varchar,(month(getDate())-1))
		if @myMonth = '12'
			set @myYear = convert(varchar,(year(getDate())-1))
		else
			set @myYear = convert(varchar,(year(getDate())))
		end
	if @monthOption = 'THIS_MONTH'
		begin
		set @myMonth = convert(varchar,(month(getDate())-0))
		set @myYear = convert(varchar,(year(getDate())-0))
		end 



	set @sql = @sql + ' AND month(ACTUAL_DATE) = ' + @myMonth + ' '
	set @sql = @sql + ' AND year(ACTUAL_DATE) = ' + @myYear + ' '
	end
set @sql = @sql + '
AND
 		(STATUS IS NULL OR STATUS IN (SELECT IT FROM #TempItems)) 
AND
 		(
		CUSTOMER_ID IN (SELECT ID FROM #myCOS2) OR
		SUPPLIER_ID IN (SELECT ID FROM #myCOS2) OR
 		PERSON_ID = ''' + @strNTLogin + ''' OR
		BOSS = ''' + @strNTLogin + ''' OR 
		PERSON_ID IN (SELECT SUBORDINATE FROM A_PEOPLE_SUB_LOOKUP_TABLE WHERE BOSS = ''' + @strNTLogin + ''') OR
 		APPROVER_ROLE IN (SELECT ID FROM #myRoles) OR
 		PAYER_ROLE IN (SELECT ID FROM #myRoles) OR
 		RECIEVABLE_ROLE IN (SELECT ID FROM #myRoles)
 		)
AND HOUR_TYPE = ''NORMAL''
' + @strOrder
print @sql
exec(@sql)


















