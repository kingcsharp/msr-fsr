CREATE PROCEDURE DBO.A_Z_UTIL_OBJECT_UPDATE_ROOT_STATUS
@ROOT_TABLE_NAME varchar(100)
AS
print 'First create a temptable with all the IDs in it'
CREATE TABLE #TempItems	(IT varchar(50))
declare @sql varchar(3000)
set @sql = 'INSERT INTO #TempItems SELECT ID FROM ' + @ROOT_TABLE_NAME
exec(@sql)
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT IT FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Looking at Root ' + @it
	IF exists(SELECT ID FROM A_OBJECTS WHERE ROOT = @it AND STATUS LIKE 'APPROVED%')
		set @sql = 'UPDATE ' + @ROOT_TABLE_NAME + ' SET STATUS = ''APPROVED'' WHERE ID = ''' + @it + ''''
	else
		set @sql = 'UPDATE ' + @ROOT_TABLE_NAME + ' SET STATUS = ''DELETED'' WHERE ID = ''' + @it + ''''
	print @sql
	exec(@sql)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
DROP TABLE #TempItems

