
CREATE  PROCEDURE DBO.A_SP_TASK_UPDATE_RELATED_ITEM_LIST
@list varchar(8000),
@tblName varchar(100),
@fieldName varchar(50),
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @sql varchar(8000)
CREATE TABLE #TempItems	(IT varchar(50))
if @list is not null
	begin
	print 'Inserting the items into a temp table in sp A_SP_TASK_UPDATE_RELATED_ITEM_LIST'
	INSERT INTO #TempItems Exec A_SP_Z_SPLIT @list,','
	set @sql = 'INSERT INTO ' + @tblName + 
		' (ID,TASK_ID,' + @fieldName + ',DRCM,MODBY)
		SELECT newID(),''' + @taskID + ''',
		ltrim(IT),getDate(),''' + @strNTLogin + ''' FROM #TempItems'
	print @sql
	SELeCT * FROM #TempItems
	exec(@sql)
	end
DELETE FROM #TempItems
DROP TABLE #TempItems

