





/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/

CREATE   PROCEDURE A_SP_DISCUSSION_UPDATE_INV_ROLES
@discussionID nvarchar(50),
@roleID nvarchar(2000),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_DISCUSSION_INV_ROLE
	([DISCUSSION_ID],[ROLE_ID], [DRCM],[MODBY])
		VALUES(@discussionID,@roleID, getDate() ,@strNTLogin)
print 'Finished with A_SP_DISCUSSION_UPDATE_INV_ROLES'








