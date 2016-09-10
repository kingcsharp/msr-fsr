
/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/

CREATE        PROCEDURE A_SP_DISCUSSION_UPDATE_OPENING_STATEMENT
@newID varchar(50) OUTPUT,
@messages varchar(500) OUTPUT,
@discussionID varchar(50),
@RESPONSE nvarchar(4000),
@COLOR varchar(50),
@strNTLogin varchar(50)
AS
declare @tester as varchar
SELECT @tester = ID FROM A_DISCUSSION_RESPONSE 
WHERE DISCUSSION_ID = @discussionID AND PARENT_ID is NULL
if @tester is null
	begin
		print 'this is the first time so create it'
		--declare @newID as nvarchar(50)
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_DISCUSSION_RESPONSE 
		(ID,[DISCUSSION_ID],[RESPONSE],WRITER,COLOR,[DRCM],[MODBY])
		VALUES(
		@newID,@discussionID,@RESPONSE,@strNTLogin,@COLOR,
		getDate(),@strNTLogin)
	end
else
	begin
		print 'This is one  we already have so just update it'
		UPDATE A_DISCUSSION_RESPONSE 
		set RESPONSE = @RESPONSE,
		COLOR = @COLOR,
		DRCM = getDate(),
		MODBY = @strNTLogin
		WHERE DISCUSSION_ID = @discussionID and PARENT_ID is null
	end
print 'Add opending statement color to A_DISCUSSION_COLORS so they can use it in the replies'


if @COLOR is not null 
begin
	print 'inserting my color for the dicussion into A_DISUCSSION_COLORS'
   INSERT INTO A_DISCUSSION_COLORS
   (ID, DISCUSSION_ID, COLOR, PERSON_ID, DRCM, MODBY)
	VALUES (newID(), @discussionID, @COLOR, @strNTLogin, getDate(), @strNTLogin)
end 


print 'Finished with A_SP_DISCUSSION_UPDATE_OPENING_STATEMENT'


