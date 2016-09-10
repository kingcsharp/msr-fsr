











CREATE             PROCEDURE A_SP_ROLES_SEARCH
	@strWhere nvarchar(2000),
	@strSort nvarchar(2000),
	@strNTLogin nvarchar(50)
AS

--Get my company
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
--we have to build this SLQ so make an SQL variable
declare @sql as nvarchar(4000)
if @strwhere = ''
	set @strWhere = null

set @sql = 'SELECT DISTINCT SECURITY_LEVEL,SECURITY_LEVEL_NAME, ROOT,ID,ROLE_NAME,OBJ_ID,STATUS,LOCKED_BY,UNLOCKED_BY,
	CREATED_BY,CREATING_CO,REV,WFS_ID,HIDDEN,SOURCE,LOCKED_BY_NAME FROM A_V_ROLES_WITH_ASSIGNEES 
WHERE CREATING_CO = ''' + @myCO + '''' + isNull('AND ' + @strWhere,'') + ' ' + @strSort
print @sql
exec(@sql)












