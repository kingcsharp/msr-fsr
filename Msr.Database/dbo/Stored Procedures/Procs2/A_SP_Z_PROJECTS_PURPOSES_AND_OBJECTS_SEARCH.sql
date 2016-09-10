


CREATE    PROCEDURE A_SP_Z_PROJECTS_PURPOSES_AND_OBJECTS_SEARCH
@strList varchar(4000),
@strType varchar (50),
@strSearchType varchar (100),
@strAlias varchar (20),
@sql varchar (8000) output,
@strNTLogin varchar (50)
AS 
CREATE TABLE #TempItems (IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strList,','
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
 print 'Adding a search for this tag = ' + @it
 set @sql = @sql + 'AND EXISTS (SELECT ID FROM '
 if @strSearchType = 'OBJECT' set @sql = @sql + ' A_OBJECT_ITEM_LINK '
 if @strSearchType = 'PURPOSES' set @sql = @sql + ' A_BUSINESS_PURPOSES_ITEM_LINK '
 if @strSearchType = 'PROJECT' set @sql = @sql + ' A_PROJECT_ITEM_LINK '
 set @sql = @sql + ' WHERE ITEM_TYPE = ''' + @strType + ''' AND ITEM_ID = ' + @strAlias + '.ID AND '
 if @strSearchType = 'OBJECT' set @sql = @sql + ' OBJECT_ID '
 if @strSearchType = 'PURPOSES' set @sql = @sql + ' BUSINESS_PURPOSE_ID '
 if @strSearchType = 'PROJECT' set @sql = @sql + ' PROJECT_ID '
 set @sql = @sql + ' = ''' + ltrim(@it) + ''')'
 Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs



