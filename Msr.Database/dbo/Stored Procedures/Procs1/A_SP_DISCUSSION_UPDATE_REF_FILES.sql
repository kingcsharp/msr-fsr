

/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/

CREATE      PROCEDURE A_SP_DISCUSSION_UPDATE_REF_FILES
@ID nvarchar(50),
@docID nvarchar(50),
@strNTLogin nvarchar(50)

AS

if @docID is not null
begin
INSERT INTO A_DISCUSSION_ATTACHMENTS
	([RESPONSE_ID],[DOC_ID], [DRCM],[MODBY])
		VALUES(@ID, @docID, getDate() ,@strNTLogin)
print 'Finished with A_SP_DISCUSSION_UPDATE_REF_FILES'
end 






