





/*
STORED PROCEDURE CALLED IN discussions/viewDiscussions

*/

CREATE           PROCEDURE A_SP_DISCUSSION_UPDATE_VIEW_BY_PEOPLE
@discussionID varchar(50),
@strNTLogin varchar(50)
AS
declare @alreadyViewedTest varchar(50)
SELECT @alreadyViewedTest = ID  
    	FROM A_DISCUSSION_VIEWED_BY_PEOPLE
    	WHERE DISCUSSION_ID = @discussionID AND PERSON_ID = @strNTLogin 
if @alreadyViewedTest is null and @discussionID is not null
begin  
	INSERT INTO A_DISCUSSION_VIEWED_BY_PEOPLE 
	([ID],DISCUSSION_ID,PERSON_ID,ALERT_ME,DRCM,MODBY)
	VALUES(newid(),@discussionID,@strNTLogin,1,getDate(),@strNTLogin)
end
else
begin
	print 'This person already viewd the discussion so we wont add them to table'
	UPDATE A_DISCUSSION_VIEWED_BY_PEOPLE SET DRCM = getDate() WHERE ID = @alreadyViewedTest
end 
 	DELETE 
	FROM A_DISCUSSION_RESPONSE_ALERTS 
	WHERE DISCUSSION_ID=@discussionID
    AND ALERTEE_ID = @strNTLogin