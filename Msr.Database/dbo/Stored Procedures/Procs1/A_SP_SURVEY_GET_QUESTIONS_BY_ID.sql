


















/*
STORED PROCEDURE CALLED IN survey/listQuestions.asp
*/
CREATE         PROCEDURE A_SP_SURVEY_GET_QUESTIONS_BY_ID
@surveyID nvarchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * FROM A_V_SURVEY_GET_QUESTIONS 
WHERE S_ID=@surveyID
ORDER BY NUM
















