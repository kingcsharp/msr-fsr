






/*
STORED PROCEDURE CALLED IN serviceCalls/viewServiceCalls.asp
*/
CREATE                     PROCEDURE A_SP_SERVICE_CALL_SEARCH_SERVICE_CALLS_BY_WORK_WEEK
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
print 'myCo is' +@myCO


declare @fieldList varchar(4000)
set @fieldList =
'ID,
BOSS,
SUPPLIER_NAME,
STANDARD_SEARCH,
WORK_TYPE,
CASE WHEN (TOTAL_0 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_0)
end TOTAL_0,			
CASE WHEN (TOTAL_1 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_1)
end TOTAL_1,			
CASE WHEN (TOTAL_2 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_2)
end TOTAL_2,			
CASE WHEN (TOTAL_3 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_3)
end TOTAL_3,			
CASE WHEN (TOTAL_4 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_4)
end TOTAL_4,			
CASE WHEN (TOTAL_5 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_5)
end TOTAL_5,			
CASE WHEN (TOTAL_6 = 0.0) THEN  ''''
ELSE convert(varchar(50),TOTAL_6)
end TOTAL_6,			
CUSTOMER_NAME,
SUPPLIER_ID,
CUSTOMER_ID,
APPROVER_ROLE,
PAYER_ROLE,
RECIEVABLE_ROLE,
STATUS,
WORKER_ID,
MACHINE_NAME,
FULL_NAME,
NORMAL_HOURS,
OT_HOURS,
START_DAY,
START_MONTH,
START_YEAR,
TOTAL_HOURS,
CASE WHEN (NT_0 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_0)
end NT_0,			
CASE WHEN (NT_1 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_1)
end NT_1,			
CASE WHEN (NT_2 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_2)
end NT_2,			
CASE WHEN (NT_3 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_3)
end NT_3,			
CASE WHEN (NT_4 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_4)
end NT_4,			
CASE WHEN (NT_5 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_5)
end NT_5,			
CASE WHEN (NT_6 = 0.0) THEN  ''''
ELSE convert(varchar(50),NT_6)
end NT_6,			
CASE WHEN (OT_1 = 0.0) THEN  ''''
ELSE convert(varchar(50),OT_1)
end OT_1,			
CASE WHEN (OT_2 = 0.0) THEN  ''''
ELSE convert(varchar(50),OT_2)
end OT_2,			
CASE WHEN (OT_3 = 0.0) THEN  ''''
ELSE convert(varchar(50),OT_3)
end OT_3,			
CASE WHEN (OT_4 = 0.0) THEN  ''''
ELSE convert(varchar(50),OT_4)
end OT_4,			
CASE WHEN (OT_5 = 0.0) THEN  ''''
ELSE convert(varchar(50),OT_5)
end OT_5,			
CASE WHEN (OT_6 = 0.0) THEN  ''''
ELSE convert(varchar(50),OT_6)
end OT_6,			
WORKER_NAME,
APPROVER_NAME,
PAYER_NAME,
START_DATE,
BOSS_NAME,
REASON_TYPE,
STATUS,
CASE WHEN (NORMAL_HOURS = 0.0) THEN  ''''
ELSE convert(varchar(50),convert(decimal(5,2),NORMAL_HOURS))
end NORMAL_HOURS_2,			
CASE WHEN (OT_HOURS = 0.0) THEN  ''''
ELSE convert(varchar(50),convert(decimal(5,2),OT_HOURS))
end OT_HOURS_2,			
CASE WHEN (TOTAL_HOURS = 0.0) THEN  ''''
ELSE convert(varchar(50),convert(decimal(5,2),TOTAL_HOURS))
end TOTAL_HOURS_2,			
convert(varchar(50), START_DATE,1) + '' ('' + convert(nvarchar(2),datepart(ww,START_DATE)) + '')'' AS DT_WITH_WW,
convert(nvarchar(2),datepart(ww,START_DATE)) AS WW '

--convert(decimal(5,2), OT_HOURS) AS OT_HOURS_2, 
--convert(decimal(5,2), TOTAL_HOURS) AS TOTAL_HOURS_2,
--convert(varchar(50), START_DATE,1) + '' ('' + convert(nvarchar(2),datepart(ww,START_DATE)) + '')'' AS DT_WITH_WW,
--convert(nvarchar(2),datepart(ww,START_DATE)) AS WW

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF 
SELECT  ' +@fieldList + 
'FROM A_V_SERVICE_CALL_VIEW_DATA WHERE 
(WORKER_ID='''+ @strNTLogin +''' OR BOSS='''+ @strNTLogin +'''  
OR APPROVER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR PAYER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR RECIEVABLE_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +'''))'


runSQL:
if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ')'

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)

















