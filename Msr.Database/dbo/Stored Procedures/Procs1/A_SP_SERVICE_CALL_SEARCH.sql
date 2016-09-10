





/*
STORED PROCEDURE CALLED IN serviceCalls/searchServiceCalls.asp
*/
CREATE                             PROCEDURE A_SP_SERVICE_CALL_SEARCH
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
declare @dateString varchar(100)

declare @fieldList varchar(1000)
declare @standardPermissions varchar(1000)

set @fieldList ='*,START_DATE_STRING + '' ('' + convert(nvarchar(2),datepart(ww,START_DATE_STRING)) + '')'' AS DT_WITH_WW,
''WSCR_'' + STATUS AS PRINT_STAGE ' 

exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
print @myCO
declare @sql nvarchar(4000)

print 'Doing default search'
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT ' +@fieldList +  
'FROM A_V_SERVICE_CALL_SEARCH_DATA WHERE 
(WORKER_ID='''+ @strNTLogin +''' OR BOSS='''+ @strNTLogin +'''  
OR APPROVER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR PAYER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR RECIEVABLE_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +'''))'


runSQL:
if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ''

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)




























