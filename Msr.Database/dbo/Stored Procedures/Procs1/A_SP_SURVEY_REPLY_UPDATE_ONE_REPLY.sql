

/*
STORED PROCEDURE CALLED IN survey/saveReplyasp
*/
CREATE     PROCEDURE A_SP_SURVEY_REPLY_UPDATE_ONE_REPLY
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@ID varchar(50),
@PARENTID varchar(50),
@ROOT varchar(50),
@TEXT varchar(4000),
@COLOR varchar(50),
@strNTLogin varchar(50)
AS
declare @initiatorAlerteeID varchar(50)

exec SP_GETUNIQUEID3 @newID OUTPUT 
INSERT INTO A_SURVEY_REPLIES ([ID],[TEXT],[PARENT],ROOT,[COLOR],[AUTHOR],[MODBY],[DRCM])
VALUES
(@newID, @TEXT, @PARENTID, @ROOT, @COLOR, @strNTLogin, @strNTLogin, getDate())

print 'now processing my colors'
declare @hasColorTest varchar(50)
SELECT @hasColorTest = ID
FROM A_SURVEY_COLORS
WHERE @ROOT = 	SURVEY_ID AND PERSON_ID = @strNTLogin

if @hasColorTest is null
begin
   print 'This person does not have a color so inserting them into the table'
   INSERT INTO A_SURVEY_COLORS
   (ID, SURVEY_ID, COLOR, PERSON_ID, DRCM, MODBY)
	VALUES (newID(), @ROOT, @color, @strNTLogin, getDate(), @strNTLogin)
end 



-- The code below is for the main menu warnings

declare @alreadyReplied varchar(50)

SELECT @alreadyReplied = ID 
FROM A_SURVEY_RESPONSE_ALERTS
WHERE SURVEY_ID =@ROOT 

if @alreadyReplied is null 
begin
	print 'Getting the initiator'
	SELECT @initiatorAlerteeID = OWNER FROM A_SURVEYS WHERE ID=@ROOT
	print 'Inserting owner ' + @initiatorAlerteeID
	INSERT INTO A_SURVEY_RESPONSE_ALERTS 
	(ID,SURVEY_ID, ALERTEE_ID, DRCM,MODBY)
	VALUES
	(newID(),@ROOT,  @initiatorAlerteeID, getDate(),@strNTLogin)
end 
