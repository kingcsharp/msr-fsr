





/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/




CREATE      PROCEDURE A_SP_DISCUSSION_DELETE_INV_COMPANY 
@discussionID nvarchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_DISCUSSION_INV_COMPANY WHERE 
DISCUSSION_ID = @discussionID