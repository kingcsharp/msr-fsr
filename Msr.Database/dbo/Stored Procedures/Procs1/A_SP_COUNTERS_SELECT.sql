








CREATE     PROCEDURE A_SP_COUNTERS_SELECT
@strWHERE nvarchar(1000),
@strSort nvarchar(500),
@strNTLogin nvarchar(50)
as
--build the sql for the query
declare @sql as nvarchar(4000)
set @sql = 'SELECT * FROM A_V_COUNTERS_BY_APPROVED_ID '
if len(@strWhere) > 0
		set @sql = @sql + 'WHERE ' + @strWhere + ' '
if len(@strSort) > 0
		set @sql = @sql + @strSort
print @SQL
EXEC(@SQL)








