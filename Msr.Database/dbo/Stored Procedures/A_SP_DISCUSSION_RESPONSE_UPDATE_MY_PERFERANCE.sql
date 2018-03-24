




/*
STORED PROCEDURE CALLED IN disucssion/viewDiscussion.asp
*/
CREATE               PROCEDURE A_SP_DISCUSSION_RESPONSE_UPDATE_MY_PERFERANCE
@newID varchar(50) OUTPUT,
@message varchar(50) OUTPUT,
@discussionID varchar(50),
@alertMe varchar(50),
@strNTLogin varchar(50)
AS

print 'The first insert occurs in A_SP_DISCUSSION_UPDATE_VIEW_BY_PEOPLE so this is just an update query..'
print 'updating my table A_DISCUSSION_RESPONSE_ALERT_PERFERANCE '
UPDATE A_DISCUSSION_VIEWED_BY_PEOPLE 
SET ALERT_ME = @alertMe,
	DRCM = getDate(),
	MODBY = @strNTLogin 
 WHERE DISCUSSION_ID = @discussionID
		AND PERSON_ID = @strNTLogin

print 'Finished with A_SP_DISCUSSION_UPDATE_MY_PERFERANCE'