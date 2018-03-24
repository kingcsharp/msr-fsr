




/*
STORED PROCEDURE CALLED IN disucssion/searchDiscussion.asp
*/

CREATE PROCEDURE A_SP_DISCUSSION_CLOSE
@discussionID nvarchar(50),
@strNTLogin nvarchar(50)

AS
UPDATE A_DISCUSSIONS SET STATUS = 'CLOSED' WHERE ID =@discussionID