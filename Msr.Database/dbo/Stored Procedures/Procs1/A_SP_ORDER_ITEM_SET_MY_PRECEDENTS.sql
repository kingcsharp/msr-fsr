CREATE PROCEDURE DBO.A_SP_ORDER_ITEM_SET_MY_PRECEDENTS
@ID varchar(50),
@strPrecList varchar(8000),
@strNTLogin varchar(50)
AS
print 'Updating Precedents for ' + @ID
print 'First delete all the ones we used to have'
DELETE FROM A_ORDER_ITEM_PRECEDENTS WHERE FOL = @ID
print 'making a cursor to go through the ref files string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @strPrecList,','
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Precedent = ' + @it
	INSERT INTO A_ORDER_ITEM_PRECEDENTS (ID,PREV,FOL,DRCM,MODBY)
		VALUES(newID(),@it,@ID,getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
