











/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/
CREATE   PROCEDURE A_SP_DISCUSSION_RESPONSE_CHECK_FOR_CHILD
@responseID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT top 1 * FROM A_DISCUSSION_RESPONSE 
WHERE PARENT_ID=@responseID