


CREATE   PROCEDURE DBO.A_Z_TEXT_DROP_DOWN_LOOKUP_REMOVE_FROM_LIST
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@strList varchar(8000),
@key varchar(50),
@strNTLogin varchar(50)
AS
print 'Removing the drop down list'

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strList,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'REmoving the item ' + @it + ' from the list with key = ' + @key + ' for person = ' + @strNTLogin
	DELETE FROM  A_Z_TEXT_DROP_DOWN_LOOK_UP  WHERE PERSON_ID = @strNTLogin AND LOOKUP = @key AND VAL = @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

