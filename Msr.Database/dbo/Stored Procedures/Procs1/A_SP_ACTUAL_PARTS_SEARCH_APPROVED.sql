





CREATE     PROCEDURE A_SP_ACTUAL_PARTS_SEARCH_APPROVED
	@strWhere nvarchar(1000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_V_ACTUAL_PARTS_APPROVED_DATA '

if len(@strWhere) > 0
		set @sql = @sql + 'WHERE ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)











