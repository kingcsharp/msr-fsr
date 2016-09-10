








CREATE        PROCEDURE A_SP_OBJECT_GET_MAIN_DATA
	@strID nvarchar(50),
	@strNTLogin nvarchar(50)
as
declare @myTable as nvarchar(50)
declare @myID as nvarchar(50)
print 'GETTING THE MAIN DATA FOR object ID = ' + @strID
SELECT @myTable = OBJ_TABLE, @myID = OBJ_ID FROM A_OBJECTS WHERE ID = @strID
declare @sql as nvarchar(1000)
set @sql = 'SELECT ''' + @myTable + ''' AS TABLE_NAME,h.*,
o.CREATE_DATE AS CREATE_DATE, 
o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, o.CREATING_CO AS CREATING_CO, 
o.STATUS AS STATUS, o.REV AS REV, o.WFS_ID AS WFS_ID, 
o.LOCKED_BY_NAME AS LOCKED_BY_NAME, o.CREATING_CO_NAME AS CREATING_CO_NAME
 FROM ' + @myTable + ' h
INNER JOIN
A_OBJECTS o ON h.OBJECT_ID = o.ID
WHERE h.ID = ''' + @myID + ''''
print @sql
exec(@sql)










