






CREATE   PROCEDURE A_SP_REGIONS_SEARCH_PAGE
@strWHERE nvarchar(1000),
@strSort nvarchar(500),
@strNTLogin nvarchar(50)
as
--build the sql for the query
declare @sql as nvarchar(4000)
set @sql = 'SELECT * FROM A_O_REGIONS '
if len(@strWhere) > 0
		set @sql = @sql + 'WHERE ' + @strWhere + ' '
if len(@strSort) > 0
		set @sql = @sql + @strSort

EXEC(@SQL)






