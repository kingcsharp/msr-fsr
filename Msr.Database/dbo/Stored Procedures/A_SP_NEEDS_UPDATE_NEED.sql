




CREATE     PROCEDURE A_SP_NEEDS_UPDATE_NEED
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID nvarchar(50),
@ID nvarchar(50),
@DESCRIPTION nvarchar(4000),
@CUSTOMER_CO nvarchar(50),
@REFERENCE_FILES varchar(8000),
@TIMEFRAME varchar(50),
@IMPORTANCE varchar(50),
@NEED_SIZE varchar(50),
@ADVERTISING_START_DATE nvarchar(50),
@ADVERTISING_STOP_DATE nvarchar(50),
@COMPANIES_TO_VIEW varchar(8000),
@PEOPLE_TO_VIEW varchar(8000),
@ADVERSTISE_PUBLICLY varchar(50),
@CONTACT_NAME nvarchar(50),
@CONTACT_EMAIL nvarchar(50),
@CONTACT_PHONE nvarchar(50),
@strNTLogin varchar(50)
AS
print 'Updating a Need'
if @objID is null
	begin
		print 'ID is Null we need to create this Need'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_NEEDS_HISTORY(ID,MODBY,DRCM) VALUES(@newID,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_NEEDS_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Need objectID='+@objID
		set @newID = @objID
	end


print'Updating the Need history Data'
UPDATE A_NEEDS_HISTORY SET
DESCRIPTION = @DESCRIPTION,
CUSTOMER_CO = @CUSTOMER_CO,
TIMEFRAME = @TIMEFRAME,
IMPORTANCE = @IMPORTANCE,
NEED_SIZE = @NEED_SIZE,
ADVERTISING_START_DATE = @ADVERTISING_START_DATE,
ADVERTISING_STOP_DATE = @ADVERTISING_STOP_DATE,
ADVERSTISE_PUBLICLY = @ADVERSTISE_PUBLICLY,
CONTACT_NAME = @CONTACT_NAME,
CONTACT_EMAIL = @CONTACT_EMAIL,
CONTACT_PHONE = @CONTACT_PHONE
WHERE OBJECT_ID = @newID


print 'Updating reference File List for the Need'
print 'First delete all the ones we used to have'
DELETE FROM A_DOCUMENT_LINK WHERE OBJECT_ID = @newID
print 'making a cursor to go through the ref files string'
CREATE TABLE #TempItems	(IT varchar(50))
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @REFERENCE_FILES,', '
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Reference File = ' + @it
	exec A_SP_FILES_CREATE_LINK @newID,@it,null,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


print 'Delete all companies allowed to view this need'
DELETE FROM A_NEEDS_COMPANIES_ALLOWED WHERE NEED_ID = @ID
print 'Now add the new companies allowed to view'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @COMPANIES_TO_VIEW,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding company allowed to view need = ' + @it
	INSERT INTO A_NEEDS_COMPANIES_ALLOWED (ID,NEED_ID,CO_ID,DRCM,MODBY)
	VALUES (newID(),@ID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Delete all people allowed to view this need'
DELETE FROM A_NEEDS_PEOPLE_ALLOWED WHERE NEED_ID = @ID
print 'Now add the new people allowed to view'
DELETE FROM #TempItems
INSERT INTO #TempItems Exec A_SP_Z_SPLIT @PEOPLE_TO_VIEW,', '
set @curs = Cursor For SELECT * FROM #TempItems
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding people allowed to view need = ' + @it
	INSERT INTO A_NEEDS_PEOPLE_ALLOWED (ID,NEED_ID,PERSON_ID,DRCM,MODBY)
	VALUES (newID(),@ID,ltrim(@it),getDate(),@strNTLogin)
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs