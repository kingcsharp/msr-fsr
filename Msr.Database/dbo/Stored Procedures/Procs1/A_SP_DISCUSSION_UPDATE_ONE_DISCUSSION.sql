





/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/
CREATE               PROCEDURE A_SP_DISCUSSION_UPDATE_ONE_DISCUSSION
@newID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@ID varchar(50),
@SUBJECT nvarchar(1000),
@STATEMENT nvarchar(4000),
@strNTLogin varchar(50)
AS

if not(@ID is null)
begin
	print 'The Id is not null it = ' + @ID
	declare @tester as nvarchar(50)
	SELECT @tester = ID FROM A_DISCUSSIONS WHERE ID = @ID 
	if @tester is null
		print 'Error cannot find the Discussion ' + @ID
	else
	begin
		print 'Updating the discussion whose ID = ' + @ID
		UPDATE A_DISCUSSIONS SET
		SUBJECT = @SUBJECT,MODBY=@strNTLogin,DRCM = getDate()
		WHERE ID = @ID
	end
	set @newID = @ID
	exec A_SP_DISCUSSION_UPDATE_VIEW_BY_PEOPLE @newID, @strNTLogin
end
else
begin
	set @messages = @messages + 'The ID is null'
	exec sp_getUniqueID3 @newID OUTPUT
	INSERT INTO A_DISCUSSIONS ([ID],[INITIATOR],[DATE_CREATED],[SUBJECT],[DRCM],[MODBY],[STATUS])
	VALUES
	(@newID, @strNTLogin, getDate(), @SUBJECT, getDate(), @strNTLogin, 'ACTIVE')
	set @messages = @messages + 'Done making it '
end







