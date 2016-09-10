






/*
STORED PROCEDURE CALLED IN survey/saveSurvey.asp
*/
CREATE                       PROCEDURE A_SP_SURVEY_UPDATE_ONE_SURVEY
@newID varchar(50) OUTPUT,
@msg varchar(1000) OUTPUT,
@surveyID varchar(50),
@SUBJECT nvarchar(1000),
@strNTLogin varchar(50)
AS
if @surveyID is null
begin
	print 'we are updating a New survey'
	exec SP_GETUNIQUEID3 @newID OUTPUT 
	INSERT INTO A_SURVEYS ([ID],[OWNER], [DATE_CREATED],[DRCM],[MODBY],[STATUS])
	VALUES
	(@newID,  @strNTLogin, getDate(), getDate(),@strNTLogin, 'CREATING')
	set @surveyID=@newID
end 
set @newID = @surveyID
print 'we are updating the meeting with a meeting id of ' +@surveyID
	UPDATE A_SURVEYS 
	set SUBJECT=@SUBJECT,
	DRCM = getDate(),
	MODBY = @strNTLogin
	WHERE ID = @surveyID 



