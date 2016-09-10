





CREATE      PROCEDURE A_SP_WF_SEARCH_WORKFLOWS
	@strWhere nvarchar(1000),
	@strNTLogin nvarchar(50),
	@strSort nvarchar(1000)
AS

declare @myCO as nvarchar(50)
SELECT @myCO = CO FROM A_V_PEOPLE_WITH_COMPANIES p WHERE PERSON = @strNTLogin

declare @sql nvarchar(500)
set @sql = 'SET QUOTED_IDENTIFIER OFF SELECT *
	FROM A_O_WORKFLOWS WHERE ISNULL(HIDE,0) <> 1 AND CREATING_CO = ''' + isNull(@myCO,'NULL') + ''' '

if len(@strWhere) > 0
		set @sql = @sql + ' AND ' + @strWhere + ' '

if len(@strSort) > 0
		set @sql = @sql + @strSort

print @sql
exec (@sql)










