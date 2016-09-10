

CREATE     PROCEDURE A_SP_TT_VERBS_SELECT
	@strWhere nvarchar(1000),
	@strSort nvarchar(1000),
	@strNTLogin nvarchar(50)
AS
--find out my Company
declare @myCO as nvarchar(50)
exec A_SP_GET_PERSON_COMPANY @strNTLogin,@myCO OUTPUT

declare @sql nvarchar(4000)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_APPROVED_VERBS WHERE CREATING_CO = ''' + @myCo + ''' '

if len(@strWhere) > 0
		set @sql = @sql + ' AND  ' + @strWhere + ' '



if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)











