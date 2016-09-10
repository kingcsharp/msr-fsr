


CREATE  PROCEDURE A_SP_UPDATE_TABLE_FIELD
@curVal nvarchar(50),
@root nvarchar(50),
@table nvarchar(50),
@field nvarchar(50)
as
declare @sql as nvarchar(1000)
set @sql = 'UPDATE ' + @table + ' SET ' + @field + ' = 
''' + @curVal + ''' WHERE ' + @field + ' IN (SELECT OBJ_ID FROM A_OBJECTS WHERE ROOT = ''' + @ROOT + ''')'
print 'sql = ' + @sql
exec(@sql)



