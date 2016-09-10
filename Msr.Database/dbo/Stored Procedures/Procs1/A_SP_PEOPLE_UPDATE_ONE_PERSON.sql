



CREATE                 PROCEDURE A_SP_PEOPLE_UPDATE_ONE_PERSON
@newObjID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@objID varchar(50),
@LOGIN nvarchar(50),
@NAME nvarchar(100),
@PASSWORD nvarchar(50),
@LAST_NAME nvarchar(100),
@LANG varchar(50),
@COMPANY varchar(50),
@POSITION varchar(50),
@BOSS varchar(50),
@TIME_ZONE varchar(50),
@HIRE_DATE datetime,
@STATUS varchar(50),
@SCREEN_TYPE varchar(50),
@IS_HEAD varchar(5),
@strNTLogin varchar(50)


AS
print 'updating this person'
set @messages = 'Time Zone = ' + @TIME_ZONE
if not(@objID is null)
begin
	set @messages = @messages + 'The objId is not null it = ' + @objID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_PEOPLE WHERE OBJECT_ID = @objID 
	if @tester is null
		set @messages = @messages + 'Error cannot find the Person ' + @objID + ' to update IT '
	else
		begin
			print 'Updating the person whoes objID = ' + @objID
			UPDATE A_PEOPLE_HISTORY SET
			LOGIN = @LOGIN, NAME = @NAME, LAST_NAME = @LAST_NAME, LANG = @LANG, COMPANY = @COMPANY,
			BOSS = @BOSS, TIME_ZONE = @TIME_ZONE, HIRE_DATE = @HIRE_DATE, SYSTEM_STATUS = @STATUS, MODBY = @strNTLogin,
			CO_POSITION = @POSITION, DRCM = getDate(),IS_HEAD = @IS_HEAD,
			SCREEN_TYPE = @SCREEN_TYPE
			WHERE OBJECT_ID = @objID
			select @tester as ID
		end
	set @newObjID = @objID
end
else
	begin
		set @messages = @messages + 'The object ID is null'
		declare @newID as nvarchar(50)
		exec sp_getUniqueID3 @newID OUTPUT
		INSERT INTO A_PEOPLE_HISTORY ([ID], [LOGIN], [NAME], [BOSS], 
		[SOURCE], [LAST_NAME], [LANG], [HIRE_DATE], 
		[DRCM], [MODBY], [COMPANY], [TIME_ZONE], [SYSTEM_STATUS], 
		[CO_POSITION], [IS_HEAD], [SCREEN_TYPE], [CHANGE_PASS]
		) 
		VALUES
		(@newID, @LOGIN, @NAME, @BOSS, 'A_SP_UPDATE_ONE_PERSON',
		@LAST_NAME, @LANG, @HIRE_DATE, getDate(), @strNTLogin, @COMPANY,
		@TIME_ZONE, @STATUS, @POSITION, @IS_HEAD, @SCREEN_TYPE, 1)

		declare @myObjID as nvarchar(50)
		SELECT @myObjId = OBJECT_ID FROM A_PEOPLE_HISTORY WHERE ID = @newID
		set @messages = @messages + 'Making the objectID creating ' + @myObjID
		exec A_SP_OBJECT_MAKE_CREATING @myObjID,@strNTLogin
		set @messages = @messages + 'Done making it Creating ' + @strNTLogin
		select @newID as ID,@myObjID as OBJECT_ID
		set @newObjId = @myObjId
		
		INSERT INTO A_PEOPLE_NEW_UNENCRYPTED_PASSWORDS 
	  			([ID],[OBJECT_ID],[HISTORY_REF_ID],[PASSWORD],DRCM,MODBY)
		VALUES 	(newID(),@myObjID,@newID,@PASSWORD,GETDATE(),@strNTlogin)
	
		
	end


















