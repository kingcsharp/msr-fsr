



/*
STORED PROCEDURE CALLED IN disucssion/editDiscussion.asp
*/
CREATE     PROCEDURE A_SP_DISCUSSION_UPDATE_INV_PEOPLE
@discussionID nvarchar(50),
@peopleID nvarchar(2000),
@strNTLogin nvarchar(50)

AS
INSERT INTO A_DISCUSSION_INV_PEOPLE
	([DISCUSSION_ID],[PEOPLE_ID], [DRCM],[MODBY])
		VALUES(@discussionID,@peopleID, getDate() ,@strNTLogin)
print 'Finished with A_SP_DISCUSSION_UPDATE_INV_PEOPLE'