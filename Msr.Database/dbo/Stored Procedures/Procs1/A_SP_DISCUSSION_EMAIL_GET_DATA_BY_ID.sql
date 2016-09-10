

/*
STORED PROCEDURE CALLED IN discussion/emailDiscussion.asp
*/

CREATE        PROCEDURE A_SP_DISCUSSION_EMAIL_GET_DATA_BY_ID
@discussionID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * 
FROM A_V_DISCUSSION_SEARCH
WHERE ID=@discussionID


