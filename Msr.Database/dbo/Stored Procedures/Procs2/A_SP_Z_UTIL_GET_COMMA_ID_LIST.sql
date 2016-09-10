

--This will return the list of IDs from the SQL you send in
--  for example send in SELECT ID FROM A_PEOPLE and you will get a list of
-- people 1,2,3,4,5


CREATE   PROCEDURE dbo.A_SP_Z_UTIL_GET_COMMA_ID_LIST
@strSQL varchar(8000),
@so varchar(8000) OUTPUT
AS
CREATE TABLE #Z_UTTempItems	(IT varchar(50))
INSERT INTO #Z_UTTempItems EXEC(@strSQL)


Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT IT FROM #Z_UTTempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	set @so = isnull(@so + ',','') + @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs










