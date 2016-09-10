
CREATE      PROCEDURE A_SP_PROCEDURES_UPDATE_ONE_PROCEDURE
@newObjID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@objID as nvarchar(50),
@COMPANY nvarchar(50),
@VERB nvarchar(50),
@NAME nvarchar(500),
@COMMENTS nvarchar(4000),
@STEPS_IN_AP numeric,
@WIP_MSG numeric,
@SECURITY_LEVEL nvarchar(50),
@SYSTEM_ID nvarchar(50),
@DURATION float,
@DURATION_TYPE nvarchar(50),
@strNTLogin nvarchar(50)


AS

if not(@objID is null)
begin
	set @messages = @messages + 'The objId is not null it = ' + @objID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_PROCEDURES WHERE OBJECT_ID = @objID 
	if @tester is null
		set @messages = @messages + 'Error cannot find the Procedure ' + @objID + ' to update IT '
	else
		begin
			print 'Updating the procedure whoes objID = ' + @objID
			UPDATE A_PROCEDURES_HISTORY SET
			VERB = @VERB,
			NAME = @NAME,
			COMMENTS = @COMMENTS,
			STEPS_IN_AP = @STEPS_IN_AP,
			WIP_MSG = @WIP_MSG,
			SECURITY_LEVEL = @SECURITY_LEVEL,
			SYSTEM_ID = @SYSTEM_ID,
			DURATION = @DURATION,
			DURATION_TYPE = @DURATION_TYPE,
			DRCM = getDate(), MODBY = @strNTLogin
			WHERE OBJECT_ID = @objID
		end
	set @newObjID = @objID
end
else
	begin
		set @messages = @messages + 'The object ID is null'
		declare @newID as nvarchar(50)
		exec sp_getUniqueID3 @newID OUTPUT
		INSERT INTO A_PROCEDURES_HISTORY (ID,NAME,VERB,COMMENTS,STEPS_IN_AP,WIP_MSG,DRCM,MODBY,SECURITY_LEVEL,SYSTEM_ID)
		VALUES(@newID,@NAME,@VERB,@COMMENTS,@STEPS_IN_AP,@WIP_MSG,getDate(),@strNTLogin,@SECURITY_LEVEL,@SYSTEM_ID)
		declare @myObjID as nvarchar(50)
		SELECT @myObjId = OBJECT_ID FROM A_PROCEDURES_HISTORY WHERE ID = @newID
		set @newObjId = @myObjId
	end



