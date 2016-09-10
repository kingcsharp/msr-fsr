












CREATE              PROCEDURE A_SP_COMPANIES_SELECT_SEARCH
	@strWhere nvarchar(255),
	@strSort nvarchar(200),
	@strNTLogin nvarchar(50)
AS

--Get my company
declare @myCo as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCo OUTPUT
--we have to build this SLQ so make an SQL variable
declare @sql as nvarchar(4000)
set @sql = 'SELECT DISTINCT * FROM A_APPROVED_COMPANIES
WHERE ' + @strWhere + ' AND ( 
([CO_TYPE] = ''DEPARTMENT'' AND CREATING_CO = ''' + @myCo + ''') OR
([CO_TYPE] = ''COMPANY'')
)' + @strSort
print @sql
exec(@sql)













