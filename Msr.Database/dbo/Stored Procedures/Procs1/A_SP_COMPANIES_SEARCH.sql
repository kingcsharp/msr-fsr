

CREATE                    PROCEDURE A_SP_COMPANIES_SEARCH
	@strWhere varchar(4000),
	@strSort varchar(1000),
	@strNTLogin varchar(50)
AS

--Get my company
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
--we have to build this SLQ so make an SQL variable
declare @sql as nvarchar(4000)
set @sql = 'SELECT DISTINCT 
ID,EXTERNAL_ID,NAME,CO_TYPE,OBJECT_ID,STATUS,LOCKED_BY,UNLOCKED_BY,
CREATED_BY,CREATING_CO,REV,WFS_ID,
LOCKED_BY_NAME,OBJECT_ID as OBJ_ID,ROOT AS ROOT, CHILDREN_COUNT, 
TOP_COMPANY, picRecord, ROOT_CO_NAME, PARENT_NAME
FROM A_O_COMPANIES
WHERE ' + @strWhere + ' AND ( 
(CREATING_CO = ''' + @myCo + ''') OR ([ROOT] = ''' + @myCo + ''') OR PARENT IS NULL
)' + @strSort
print @sql
exec(@sql)









