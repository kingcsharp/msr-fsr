










CREATE          PROCEDURE A_SP_ACTUAL_PARTS_SEARCH
	@strWhere varchar(6000),
	@strSort varchar(2000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as varchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql varchar(8000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *,OBJECT_ID AS OBJ_ID
FROM A_O_ACTUAL_PARTS_HISTORY WHERE 
(CREATING_CO = ''' + @myCo + ''' OR
CUR_OWNER =  ''' + @myCo + '''
)
'

if len(@strWhere) > 0
		set @sql = @sql + ' AND (' + @strWhere + ') '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)
















