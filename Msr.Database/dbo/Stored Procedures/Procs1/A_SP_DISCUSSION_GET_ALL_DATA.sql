










/*
STORED PROCEDURE CALLED IN survey/editSurvey.asp
*/

CREATE    PROCEDURE A_SP_DISCUSSION_GET_ALL_DATA
@discussionID varchar(50),
@strNTLogin varchar(50)

AS
SELECT * FROM A_V_DISCUSSION_WITH_INITIAL_DATA 
WHERE ID=@discussionID









