





/*
STORED PROCEDURE CALLED IN serviceCalls/viewWithFiltersServiceCall.asp
*/
CREATE           PROCEDURE A_SP_SERVICE_CALL_SEARCH_GET_FILTERED_SERVICE_CALLS_IDS
@strWhere nvarchar(4000),
@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
SELECT @myCo = COMPANY 
FROM A_APPROVED_PEOPLE 
WHERE ID=@strNTLogin
print 'myCo is' +@myCO

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT ID 
FROM A_V_SERVICE_CALL_VIEW_DATA 
WHERE 
(WORKER_ID='''+ @strNTLogin +''' OR BOSS='''+ @strNTLogin +'''  
OR APPROVER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR PAYER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR RECIEVABLE_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +'''))'


if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ')'

--if len(@strSort) > 0
	--	set @sql = @sql + @strSort

print @sql
exec (@sql)







