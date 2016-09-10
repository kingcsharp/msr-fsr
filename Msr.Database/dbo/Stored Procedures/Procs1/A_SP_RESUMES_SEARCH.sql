CREATE PROCEDURE dbo.A_SP_RESUMES_SEARCH
@strWHERE nvarchar(1000),
@strSort nvarchar(500),
@strNTLogin nvarchar(50)
as
declare @rootCo varchar(50)
SELECT @rootCo = ROOT_COMPANY FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = @strNTLogin
--build the sql for the query
declare @sql as nvarchar(4000)
set @sql = 'SELECT * FROM A_RESUMES WHERE ROOT_CO = ''' + @rootCo + ''' '
if len(@strWhere) > 0
		set @sql = @sql + 'AND ' + @strWhere + ' '
if len(@strSort) > 0
		set @sql = @sql + @strSort
print @sql
EXEC(@SQL)




