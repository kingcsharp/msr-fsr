







/*
STORED PROCEDURE CALLED IN serviceCalls/viewWithFiltersServiceCalls.asp
*/
CREATE                PROCEDURE A_SP_SERVICE_CALL_SERVICE_CALLS_GET_TOTAL_HOURS_PER_WORKER
@strWhere nvarchar(4000),
@strSort nvarchar(1000),
@strNTLogin nvarchar(50)
AS

--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT
print 'myCo is' +@myCO

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF 
SELECT 
SUPPLIER_NAME, 
			WORKER_NAME, 
			SUM(NORMAL_HOURS) AS TOTAL_NORMAL_HOURS, 
			SUM(OT_HOURS) AS TOTAL_OT_HOURS, 
		   SUM(TOTAL_HOURS) AS TOTAL_HOURS 
FROM         dbo.A_V_SERVICE_CALL_VIEW_DATA
WHERE 
(WORKER_ID='''+ @strNTLogin +''' OR BOSS='''+ @strNTLogin +'''  
OR APPROVER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR PAYER_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +''') 
OR RECIEVABLE_ROLE IN (SELECT ROLE_ID FROM A_APPROVED_ROLE_ASSIGNEES WHERE PERSON = ''' + @strNTLogin +'''))'


runSQL:
if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ')
GROUP BY SUPPLIER_NAME, WORKER_NAME, SUPPLIER_ID, WORKER_ID'

--if len(@strSort) > 0
	--	set @sql = @sql + @strSort

print @sql
exec (@sql)












