






/*
STORED PROCEDURE CALLED IN disucssion/replyDiscussion.asp
*/
CREATE                 PROCEDURE A_SP_DISCUSSION_UPDATE_ONE_REPLY
@newID varchar(50) OUTPUT,
@message varchar(50) OUTPUT,
@discussionID varchar(50),
@parentID varchar(50),
@response nvarchar(4000),
@color varchar(50),
@strNTLogin varchar(50)
AS
declare @initiatorAlerteeID varchar(50)
declare @writerAlerteeID varchar(50)
declare @shouldAlertWriter varchar(50)
declare @hasColorTest varchar(50)

exec sp_GetUniqueID3 @newID OUTPUT
print 'Inserting the response now as ID = ' + @newID
INSERT INTO A_DISCUSSION_RESPONSE 
(ID, DISCUSSION_ID, RESPONSE,PARENT_ID,COLOR,WRITER,DRCM,MODBY)
VALUES(@newID,@discussionID,@response,@parentID,@color,@strNTLogin,getDate(),@strNTLogin)


print 'Does this person have a color?'

SELECT @hasColorTest = ID
FROM A_DISCUSSION_COLORS
WHERE @discussionID = DISCUSSION_ID AND PERSON_ID = @strNTLogin

if @hasColorTest is null
begin
   print 'This person does not have a color so inserting them into the table'
   INSERT INTO A_DISCUSSION_COLORS
   (ID, DISCUSSION_ID,COLOR,PERSON_ID,DRCM,MODBY)
	VALUES (newID(), @discussionID, @color, @strNTLogin, getDate(), @strNTLogin)
end 

print 'We will always notify the initiator when someone responds. Getting the initiator.....'
SELECT @initiatorAlerteeID = INITIATOR FROM A_DISCUSSIONS WHERE ID=@discussionID
print 'Inserting RS_TYPE = INITIATOR who is' + @initiatorAlerteeID
INSERT INTO A_DISCUSSION_RESPONSE_ALERTS 
(ID,DISCUSSION_ID,R_TYPE, ALERTEE_ID, DRCM,MODBY)
VALUES
(newID(),@discussionID, 'INITIATOR', @initiatorAlerteeID, getDate(),@strNTLogin)

print 'Notify all the invitees that want to be for now on'

print 'Inserting RS_TYPE = NOFIY '
INSERT INTO A_DISCUSSION_RESPONSE_ALERTS 
([ID],DISCUSSION_ID,R_TYPE, ALERTEE_ID, DRCM,MODBY)
(SELECT newID(),DISCUSSION_ID,'NOTIFY',PERSON_ID,GETDATE(),@strNTLogin
FROM A_DISCUSSION_VIEWED_BY_PEOPLE 
WHERE DISCUSSION_ID = @discussionID AND ALERT_ME = 1 
--AND @strNTlogin NOT IN (SELECT ALERTEE_ID FROM A_DISCUSSION_RESPONSE_ALERTS WHERE DISCUSSION_ID = @discussionID AND  ALERTEE_ID = @strNTlogin AND R_TYPE = 'NOTIFY')
)

print 'Inserting RS_TYPE = WRITER who is' + isNull(@writerAlerteeID,'')
SELECT @writerAlerteeID = WRITER FROM A_DISCUSSION_RESPONSE WHERE ID=@parentID
INSERT INTO A_DISCUSSION_RESPONSE_ALERTS 
([ID],DISCUSSION_ID,R_TYPE, ALERTEE_ID, DRCM,MODBY)
VALUES (newID(),@discussionID, 'WRITER', @writerAlerteeID, getDate(),@strNTLogin)


exec A_SP_DISCUSSION_SEND_EMAIL @discussionID,1,@strNTLogin
-- 
-- 
-- 
-- 
-- declare @sql varchar(4000)
-- 
-- 
-- set @sql ='SELECT ALERTEE_ID AS ID FROM A_DISCUSSION_RESPONSE_ALERTS
-- WHERE DISCUSSION_ID =''' + @discussionID +''' and ALERTEE_ID <> ''' + @strNTLogin + '''' 
-- declare @so varchar(8000)
-- exec A_SP_Z_UTIL_GET_COMMA_ID_LIST @sql,@so OUTPUT
-- print 'so = ' + @so
-- 
-- 
-- 
-- declare @linkPath varchar (1000)
-- declare @emailSubject varchar(1000) 
-- set @linkPath = 'asp/discussions/searchDiscussion.asp?DISCUSSION_NUMBER_MATCH_EXACTLY=TRUE&amp;STATUS=ALL&amp;DISCUSSION_NUMBER=' +@discussionID
-- set @emailSubject = 'ANSWER discussion update alert'
-- exec A_SP_ADMIN_EMAIL_QUE_ADD_PEOPLE_AND_ROLES_MSG
-- null,
-- @so,
-- @linkPath,
-- @emailSubject,
-- @strNTLogin
-- 
-- 


print 'Finished with A_SP_DISCUSSION_UPDATE_ONE_REPLY'