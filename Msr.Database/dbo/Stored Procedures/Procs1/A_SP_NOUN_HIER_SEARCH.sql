






CREATE    PROCEDURE A_SP_NOUN_HIER_SEARCH
	@strWhere nvarchar(1000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_NOUN_HIER_HISTORY '

if len(@strWhere) > 0
		set @sql = @sql + 'WHERE ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)






