





/*
STORED PROCEDURE CALLED IN disucssion/saveQuestionsSurvey.asp
*/


CREATE          PROCEDURE A_SP_SURVEY_REPLIES_DELETE_FILES 
@surveyID varchar(50),
@responseID varchar(50),
@strNTLogin varchar(50)
AS
DELETE FROM A_SURVEY_ATTACHMENTS WHERE 
SURVEY_ID = @surveyID AND 
RESPONSE_ID = @responseID







