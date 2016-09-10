







CREATE     PROCEDURE A_SP_LOCATIONS_SEARCH
@strWHERE nvarchar(1000),
@strSort nvarchar(500),
@strNTLogin nvarchar(50)
as
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

--build the sql for the query
declare @sql as nvarchar(4000)
set @sql = 'SELECT * FROM A_O_LOCATIONS WHERE CREATING_CO = ''' + @myCo + ''' '
if len(@strWhere) > 0
		set @sql = @sql + 'AND ' + @strWhere + ' '
if len(@strSort) > 0
		set @sql = @sql + @strSort
print @sql
EXEC(@SQL)







