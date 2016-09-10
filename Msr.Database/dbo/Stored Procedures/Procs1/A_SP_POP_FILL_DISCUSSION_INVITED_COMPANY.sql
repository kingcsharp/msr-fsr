









/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/

CREATE        PROCEDURE A_SP_POP_FILL_DISCUSSION_INVITED_COMPANY
@discussionID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_POP_FILL_DISCUSSION_INVITED_COMPANY
WHERE D_ID=@discussionID








