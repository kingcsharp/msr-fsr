


/*
STORED PROCEDURE CALLED IN survey/addsurvey.asp
*/

CREATE    PROCEDURE A_SP_SURVEY_UPDATE_QUESTION
@surveyID nvarchar(50),
@QUESTION nvarchar(2000),
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
		(ID,[SURVEY_ID],[QUESTION],WRITER,[DRCM],[MODBY])
		VALUES(
		@newID,@surveyID,@QUESTION,@strNTLogin,getDate(),@strNTLogin)
	end
else
	begin
		print 'This is one  we already have so just update it'
		UPDATE A_SURVEY_RESPONSE 
		set QUESTION = @QUESTION,
		DRCM = getDate(),
		MODBY = @strNTLogin
		WHERE SURVEY_ID = @surveyID and PARENT_ID is null
	end

print 'Finished with A_SP_SURVEY_UPDATE_SURVEY'





