







/*
STORED PROCEDURE CALLED IN 

MODULE: disucssion/editDiscussion.asp
*/

CREATE      PROCEDURE A_SP_POP_FILL_DISCUSSION_INVITED_PEOPLE
@discussion_ID nvarchar(50),
@strNTLogin nvarchar(50)

AS
SELECT * FROM  A_V_POP_FILL_DISCUSSION_INVITED_PEOPLE
WHERE D_ID=@discussion_ID






