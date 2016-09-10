





CREATE    PROCEDURE A_SP_PREPOP_UPDATE_ONE_PREPOP
@newObjID nvarchar(50) OUTPUT,
@messages nvarchar(500) OUTPUT,
@objID nvarchar(50),
@procStepID nvarchar(50),
@strNTLogin nvarchar(50)
AS

set @messages = 'Saving '

if not(@objID is null)
begin
	set @messages = @messages + 'The objId is not null it = ' + @objID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_O_PREPOP_HISTORY WHERE OBJECT_ID = @objID 
	if @tester is null
		set @messages = @messages + 'Error cannot find the Prepop ' + @objID + ' to update IT '
	else
		begin
			print 'Updating the prePop whose objID = ' + @objID
			UPDATE A_PREPOP_HISTORY SET
			DRCM = getDate(),
			MODBY = @strNTLogin,
			PROC_STEP_ID = @procStepID
			WHERE OBJECT_ID = @objID
		end
	set @newObjID = @objID
end
else
	begin
		set @messages = @messages + 'The object ID is null'
		declare @newID as nvarchar(50)
		exec sp_getUniqueID3 @newID OUTPUT
		set @messages = @messages + 'The new ID is ' + isnull(@newID,'NULL??')
		INSERT INTO A_PREPOP_HISTORY (
			ID,
			PROC_STEP_ID ,
			DRCM, 
			MODBY) VALUES(
			@newID ,
			@procStepID,
			getDate(),
			@strNTLogin) 		declare @myObjID as nvarchar(50)
		SELECT @myObjId = OBJECT_ID FROM A_PREPOP_HISTORY WHERE ID = @newID
		set @messages = @messages + 'Making the objectID creating ' + @myObjID
		exec A_SP_OBJECT_MAKE_CREATING @myObjID,@strNTLogin
		set @messages = @messages + 'Done making it Creating ' + @strNTLogin
		select @newID as ID,@myObjID as OBJECT_ID
		set @newObjId = @myObjId
	end







