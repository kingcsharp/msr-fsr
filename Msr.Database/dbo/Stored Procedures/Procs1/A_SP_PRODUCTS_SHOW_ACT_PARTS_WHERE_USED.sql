CREATE PROCEDURE DBO.A_SP_PRODUCTS_SHOW_ACT_PARTS_WHERE_USED
@strWhere varchar(4000),
@strOrder varchar(1000),
@strNTLogin varchar(50)
AS
declare @sql varchar(8000)
set @sql = 'SELECT * FROM A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON '
set @sql = @sql + isNull(' WHERE ' + @strWhere, ' ')
set @sql = @sql + isNull(@strOrder, ' ')

print @sql

exec(@sql)
