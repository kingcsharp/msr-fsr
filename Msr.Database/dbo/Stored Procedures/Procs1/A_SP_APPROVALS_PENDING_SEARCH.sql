




CREATE    PROCEDURE A_SP_APPROVALS_PENDING_SEARCH
@strWHERE nvarchar(1000),
@strSort nvarchar(500),
@strNTLogin nvarchar(50)
as
--build the sql for the query
declare @sql as nvarchar(4000)
set @sql = 'SELECT * FROM A_V_APPROVALS_PENDING WHERE PERSON_ID = ''' + @strNTLogin + ''' '
if len(@strWhere) > 0
		set @sql = @sql + 'AND ' + @strWhere + ' '
if len(@strSort) > 0
		set @sql = @sql + @strSort

EXEC(@SQL)





