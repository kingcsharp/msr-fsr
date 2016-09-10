


/*
STORED PROCEDURE CALLED IN disucssion/editSurvey.asp
*/

CREATE    PROCEDURE A_SP_SURVEY_UPDATE_OPENING_STATEMENT
@surveyID nvarchar(50),
@RESPONSE nvarchar(2000),
@COLOR nvarchar(50),
@strNTLogin nvarchar(50)

AS
declare @tester as nvarchar
SELECT @tester = ID FROM A_SURVEY_RESPONSE 
WHERE SURVEY_ID = @surveyID AND PARENT_ID is NULL

if @tester is null
	begin
		print 'this is the first time so create it'
		declare @newID as nvarchar(50)
		exec sp_GetUniqueID3 @newID OUTPUT
		INSERT INTO A_SURVEY_RESPONSE 
		(ID,[SURVEY_ID],[RESPONSE],WRITER,COLOR,[DRCM],[MODBY])
		VALUES(
		@newID,@surveyID,@RESPONSE,@strNTLogin,@COLOR,
		getDate(),@strNTLogin)
	end
else
	begin
		print 'This is one  we already have so just update it'
		UPDATE A_SURVEY_RESPONSE 
		set RESPONSE = @RESPONSE,
		COLOR = @COLOR,
		DRCM = getDate(),
		MODBY = @strNTLogin
		WHERE SURVEY_ID = @surveyID and PARENT_ID is null
	end

print 'Finished with A_SP_SURVEY_UPDATE_OPENING_STATEMENT'





