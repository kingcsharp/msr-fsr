








/*
STORED PROCEDURE CALLED IN survey/deleteQuestionSurvey.asp
*/



CREATE        PROCEDURE A_SP_SURVEY_DELETE_QUESTION 
@responseID nvarchar(50),
@surveyID varchar(50),
@strNTLogin nvarchar(50)
AS
DELETE FROM A_SURVEY_REPLIES WHERE 
ID = @responseID

exec A_SP_SURVEY_ORDER_QUESTIONS @surveyID, @strNTLogin 












