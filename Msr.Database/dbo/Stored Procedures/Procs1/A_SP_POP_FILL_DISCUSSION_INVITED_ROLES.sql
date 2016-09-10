








/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/

CREATE       PROCEDURE A_SP_POP_FILL_DISCUSSION_INVITED_ROLES
@discussionID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_POP_FILL_DISCUSSION_INVITED_roles
WHERE D_ID=@discussionID







