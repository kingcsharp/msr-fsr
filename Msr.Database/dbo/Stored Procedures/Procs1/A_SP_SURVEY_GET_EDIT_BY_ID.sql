

















/*
STORED PROCEDURE CALLED IN survey/updateQuestionSurvey.asp
*/
CREATE      PROCEDURE A_SP_SURVEY_GET_EDIT_BY_ID
@responseID nvarchar(50),
@strNTLogin nvarchar(50)
AS
SELECT * FROM A_V_SURVEY_GET_QUESTIONS 
WHERE R_ID=@responseID















