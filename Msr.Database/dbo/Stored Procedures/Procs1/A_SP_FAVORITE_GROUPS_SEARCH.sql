




CREATE  PROCEDURE A_SP_FAVORITE_GROUPS_SEARCH 
	@strWhere nvarchar(800),
	@strNTLogin nvarchar(50),
	@sort nvarchar(500)
AS

declare @sql nvarchar(500)
set @sql = 'SELECT ID,FAV_TYPE,GROUP_NAME,count(ITEM) as QTY FROM A_V_FAVORITES_COMPLETE '
set @sql = @sql + ' WHERE PERSON = ''' + @strNTLogin + ''' '
if len(@strWhere) > 0
	set @sql = @sql + ' AND ' + @strWhere
set @sql = @sql + ' GROUP BY ID,FAV_TYPE,GROUP_NAME,PERSON '
set @sql = @sql + @sort

print @sql
exec (@sql)





