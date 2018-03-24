



/*
STORED PROCEDURE CALLED IN discussion/viewFilesDiscussion.asp
*/
CREATE   PROCEDURE A_SP_DISCUSSION_GET_FILE_ATTACHMENTS
@responseID nvarchar(50),
@strNTLogin nvarchar(50)
AS

SELECT * FROM A_V_DISCUSSION_GET_FILE_ATTACHMENT_INFO 
where ID = @responseID