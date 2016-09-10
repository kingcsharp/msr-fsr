



CREATE   PROCEDURE A_SP_EQUIP_EXP_UPDATE_EQUIP_EXP
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID nvarchar(50),
@ID nvarchar(50),
@PERSON_ID nvarchar(50),
@PART_ID nvarchar(50),
@FIRST_EXPOSURE_DATE nvarchar(50),
@LAST_EXPOSURE_DATE nvarchar(50),
@HW_INSTALL_EXP_LEVEL smallInt,
@PROCESS_SETUP_EXP_LEVEL smallInt,
@OPERATION_EXP_LEVEL smallInt,
@SM_EXP_LEVEL smallInt,
@UM_EXP_LEVEL smallInt,
@FORMALLY_TRAINED varchar(50),
@CERTIFIED varchar(50),
@COMMENTS varchar(8000),
@REFERENCE_FILES varchar(8000),
@strNTLogin varchar(50)
AS
print 'Updating an Equipment Experience'
if @objID is null
	begin
		print 'ID is Null we need to create this Equip Exp'
		exec sp_GetUniqueID3 @newID OUTPUT
		set @ID = @newID
		INSERT INTO A_EQUIP_EXP_HISTORY(ID,MODBY,DRCM) VALUES(@newID,@strNTLogin,getDATE())
		SELECT @newID = OBJECT_ID FROM A_EQUIP_EXP_HISTORY WHERE ID = @newID
	end
else
	begin
		print 'The ID is not null so we are just updating Equip Exp objectID='+@objID
		set @newID = @objID
	end


print'Updating the Equip Exp history Data'
UPDATE A_EQUIP_EXP_HISTORY SET
PERSON_ID = @PERSON_ID,
PART_ID = @PART_ID,
FIRST_EXPOSURE_DATE = @FIRST_EXPOSURE_DATE,
LAST_EXPOSURE_DATE = @LAST_EXPOSURE_DATE,
HW_INSTALL_EXP_LEVEL = @HW_INSTALL_EXP_LEVEL,
PROCESS_SETUP_EXP_LEVEL = @PROCESS_SETUP_EXP_LEVEL,
OPERATION_EXP_LEVEL = @OPERATION_EXP_LEVEL,
SM_EXP_LEVEL = @SM_EXP_LEVEL,
UM_EXP_LEVEL = @UM_EXP_LEVEL,
FORMALLY_TRAINED = @FORMALLY_TRAINED,
CERTIFIED = @CERTIFIED,
COMMENTS = @COMMENTS
WHERE OBJECT_ID = @newID


print 'Updating reference File List for the Equip Exp'
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




