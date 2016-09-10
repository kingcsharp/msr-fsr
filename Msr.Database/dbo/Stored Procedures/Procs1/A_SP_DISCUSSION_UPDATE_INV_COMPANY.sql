/*
STORED PROCEDURE CALLED IN discussion/editDiscussion.asp
*/

CREATE      PROCEDURE A_SP_DISCUSSION_UPDATE_INV_COMPANY
@discussionID nvarchar(50),
@companyID nvarchar(2000),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_DISCUSSION_INV_COMPANY
	([DISCUSSION_ID],[COMPANY_ID], [DRCM],[MODBY])
		VALUES(@discussionID,@companyID, getDate() ,@strNTLogin)
print 'Finished with A_SP_DISCUSSION_UPDATE_INV_COMPANIES'
exec A_SP_DISCUSSION_UPDATE_REALLY_INVITED_COMPANY @discussionID, @companyID, @strNTLogin
