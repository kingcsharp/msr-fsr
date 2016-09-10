

CREATE PROCEDURE A_SP_POPFILL_PEOPLE_BY_ID_LIST
@pl varchar(8000),
@strNTLogin varchar(50)
AS
CREATE TABLE #TempReturn (ID varchar(50),FULL_NAME nvarchar(1000))

CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @pl,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Inserting Person = ' + @it
	INSERT INTO #TempReturn(ID,FULL_NAME) SELECT ID,FULL_NAME FROM A_V_PEOPLE_APPROVED_DATA WHERE ID = ltrim(@it)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
SELECT * FROM #TempReturn


